const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;
const parseInline = @import("./parseInline.zig").parseInline;
const newText = @import("../utils/newText.zig").newText;
const appendChild = @import("../utils/appendChild.zig").appendChild;

pub fn parseBlockInlines(
    allocator: std.mem.Allocator,
    parent: *MarkdownNode,
    rules: []const *const InlineRule,
    refs: std.StringHashMap(@import("../types/LinkReference.zig").LinkReference),
    footnotes: std.StringHashMap(@import("../types/FootnoteReference.zig").FootnoteReference),
) !void {
    if (std.mem.eql(u8, parent.type, "html_block")) {
        return;
    }

    if (std.mem.eql(u8, parent.type, "code_block")) {
        var content = parent.content;
        var should_free_stripped = false;
        if (containsNonWhitespace(content)) {
            content = try stripLeadingTrailingBlankLines(allocator, content);
            should_free_stripped = (content.ptr != parent.content.ptr);

            if (content.len > 0 and content[content.len - 1] != '\n') {
                var content_with_newline = try std.ArrayList(u8).initCapacity(allocator, content.len + 1);
                defer content_with_newline.deinit(allocator);
                try content_with_newline.appendSlice(allocator, content);
                try content_with_newline.append(allocator, '\n');
                const new_content = try content_with_newline.toOwnedSlice(allocator);
                if (should_free_stripped) {
                    allocator.free(content);
                }
                content = new_content;
                should_free_stripped = true;
            }
        }
        const text = try newText(allocator, parent.index, parent.line, content, 0);
        if (parent.children == null) {
            parent.children = try allocator.alloc(*MarkdownNode, 0);
        }
        parent.children = try appendToSlice(allocator, parent.children.?, text);
        if (should_free_stripped) {
            allocator.free(content);
        }
        return;
    } else if (std.mem.eql(u8, parent.type, "code_fence")) {
        var content = parent.content;
        var should_free_content = false;
        if (containsNonWhitespace(content)) {
            if (parent.indent > 0) {
                const new_content = try removeIndent(allocator, content, @as(usize, @intCast(parent.indent)));
                if (should_free_content) {
                    allocator.free(content);
                }
                content = new_content;
                should_free_content = true;
            }
        }
        if (content.len > 0 and content[content.len - 1] != '\n') {
            var content_with_newline = try std.ArrayList(u8).initCapacity(allocator, content.len + 1);
            defer content_with_newline.deinit(allocator);
            try content_with_newline.appendSlice(allocator, content);
            try content_with_newline.append(allocator, '\n');
            const new_content = try content_with_newline.toOwnedSlice(allocator);
            if (should_free_content) {
                allocator.free(content);
            }
            content = new_content;
            should_free_content = true;
        }
        const text = try newText(allocator, parent.index, parent.line, content, 0);
        if (parent.children == null) {
            parent.children = try allocator.alloc(*MarkdownNode, 0);
        }
        parent.children = try appendToSlice(allocator, parent.children.?, text);
        if (should_free_content) {
            allocator.free(content);
        }
        return;
    }

    var trimmed_content = parent.content;
    while (trimmed_content.len > 0 and std.ascii.isWhitespace(trimmed_content[0])) {
        trimmed_content = trimmed_content[1..trimmed_content.len];
    }
    while (trimmed_content.len > 0 and std.ascii.isWhitespace(trimmed_content[trimmed_content.len - 1])) {
        trimmed_content = trimmed_content[0 .. trimmed_content.len - 1];
    }

    var rulesMap = std.StringHashMap(*const InlineRule).init(allocator);
    for (rules) |rule| {
        try rulesMap.put(rule.name, rule);
    }
    defer {
        var iter = rulesMap.iterator();
        while (iter.next()) |_| {}
        rulesMap.deinit();
    }

    const Delimiter = @import("../types/Delimiter.zig").Delimiter;
    const delimiters_list = std.ArrayList(Delimiter).initCapacity(allocator, 0) catch unreachable;
    var inline_state = InlineParserState{
        .allocator = allocator,
        .rules = rules,
        .src = trimmed_content,
        .i = 0,
        .line = parent.line,
        .lineStart = 0,
        .indent = 0,
        .isEscaped = false,
        .delimiters = delimiters_list,
        .refs = refs,
        .footnotes = footnotes,
        .parentIndex = parent.index,
    };
    defer inline_state.delimiters.deinit(allocator);

    parseInline(allocator, &inline_state, parent) catch |err| {
        std.debug.print("Error parsing inlines for {s}: {s}\n", .{ parent.type, @errorName(err) });
    };

    if (parent.children) |children| {
        for (children) |child| {
            if (child.block) {
                try parseBlockInlines(allocator, child, rules, refs, footnotes);
            }
        }
    }
}

fn containsNonWhitespace(s: []const u8) bool {
    for (s) |c| {
        if (!std.ascii.isWhitespace(c)) {
            return true;
        }
    }
    return false;
}

fn isBlankLine(s: []const u8, i: usize) bool {
    var j = i;
    while (j < s.len and s[j] != '\n' and s[j] != '\r') : (j += 1) {
        if (!std.ascii.isWhitespace(s[j])) {
            return false;
        }
    }
    return true;
}

fn stripLeadingTrailingBlankLines(allocator: std.mem.Allocator, s: []const u8) ![]const u8 {
    _ = allocator;

    if (s.len == 0) {
        return s;
    }

    var first_non_blank: usize = 0;
    var i: usize = 0;
    while (i < s.len) : (i += 1) {
        if (s[i] == '\n' or s[i] == '\r') {
            first_non_blank = i;
        } else if (!std.ascii.isWhitespace(s[i])) {
            break;
        }
    }

    var last_non_blank: usize = s.len - 1;
    var j: usize = s.len - 1;
    while (j >= 0) : (j -= 1) {
        if (s[j] == '\n' or s[j] == '\r') {
            last_non_blank = j;
        } else if (!std.ascii.isWhitespace(s[j])) {
            break;
        }
    }

    return s[first_non_blank .. last_non_blank + 1];
}

fn appendToSlice(allocator: std.mem.Allocator, slice: []*MarkdownNode, item: *MarkdownNode) ![]*MarkdownNode {
    const new_slice = try allocator.alloc(*MarkdownNode, slice.len + 1);
    std.mem.copyForwards(*MarkdownNode, new_slice, slice);
    new_slice[slice.len] = item;
    allocator.free(slice);
    return new_slice;
}

fn removeIndent(allocator: std.mem.Allocator, s: []const u8, indent: usize) ![]const u8 {
    var result = try std.ArrayList(u8).initCapacity(allocator, s.len);
    defer result.deinit(allocator);

    var i: usize = 0;
    while (i < s.len) {
        const at_line_start = (i == 0 or s[i - 1] == '\n' or s[i - 1] == '\r');
        if (at_line_start) {
            var spaces_to_remove: usize = 0;
            var j: usize = i;
            while (j < s.len and spaces_to_remove < indent and s[j] == ' ') : (j += 1) {
                spaces_to_remove += 1;
            }
            i = j;
        }
        try result.append(allocator, s[i]);
        i += 1;
    }

    return try result.toOwnedSlice(allocator);
}
