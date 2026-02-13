const std = @import("std");
const mvzr = @import("mvzr");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;
const LinkReference = @import("../types/LinkReference.zig").LinkReference;
const FootnoteReference = @import("../types/FootnoteReference.zig").FootnoteReference;
const newNode = @import("../utils/newNode.zig").newNode;
const parseInline = @import("parseInline.zig").parseInline;

pub fn parseBlockInlines(
    allocator: std.mem.Allocator,
    parent: *MarkdownNode,
    rules: std.StringHashMap(*const InlineRule),
    refs: std.StringHashMap(LinkReference),
    footnotes: std.StringHashMap(FootnoteReference),
) !void {
    if (std.mem.eql(u8, parent.type, "html_block")) {
        return;
    }

    if (std.mem.eql(u8, parent.type, "code_block")) {
        var content = parent.content;
        if (containsNonWhitespace(content)) {
            content = try stripLeadingTrailingBlankLines(allocator, content);

            if (content.len > 0 and content[content.len - 1] != '\n') {
                var content_with_newline = try std.ArrayList(u8).initCapacity(allocator, content.len + 1);
                defer content_with_newline.deinit();
                try content_with_newline.appendSlice(content);
                try content_with_newline.append('\n');
                const new_content = try content_with_newline.toOwnedSlice();
                defer allocator.free(new_content);
                content = new_content;
            }
        }
        const text = try newNode(allocator, "text", false, parent.index, parent.line, 1, content, 0, null);
        if (parent.children == null) {
            parent.children = try allocator.create([]*MarkdownNode);
            parent.children.?.* = try allocator.alloc(*MarkdownNode, 0);
        }
        parent.children.?.* = try appendToSlice(allocator, parent.children.?, text);
        return;
    } else if (std.mem.eql(u8, parent.type, "code_fence")) {
        var content = parent.content;
        if (containsNonWhitespace(content)) {
            if (parent.indent > 0) {
                content = try removeIndentation(allocator, content, parent.indent);
            }
            content = try stripLeadingBlankLines(allocator, content);

            if (content.len > 0 and content[content.len - 1] != '\n') {
                var content_with_newline = try std.ArrayList(u8).initCapacity(allocator, content.len + 1);
                defer content_with_newline.deinit();
                try content_with_newline.appendSlice(content);
                try content_with_newline.append('\n');
                const new_content = try content_with_newline.toOwnedSlice();
                defer allocator.free(new_content);
                content = new_content;
            }
        }
        const text = try newNode(allocator, "text", false, parent.index, parent.line, 1, content, 0, null);
        if (parent.children == null) {
            parent.children = try allocator.create([]*MarkdownNode);
            parent.children.?.* = try allocator.alloc(*MarkdownNode, 0);
        }
        parent.children.?.* = try appendToSlice(allocator, parent.children.?, text);
        return;
    }

    var state = InlineParserState{
        .rules = rules,
        .src = std.mem.trimRight(u8, parent.content, " \n\t\r"),
        .i = 0,
        .line = parent.line,
        .lineStart = 0,
        .indent = 0,
        .delimiters = std.ArrayList(@import("../types/Delimiter.zig").Delimiter).init(allocator),
        .refs = refs,
        .footnotes = footnotes,
    };
    defer state.delimiters.deinit();

    try parseInline(allocator, &state, parent);

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
        if (c != ' ' and c != '\n' and c != '\t' and c != '\r') {
            return true;
        }
    }
    return false;
}

fn stripLeadingTrailingBlankLines(allocator: std.mem.Allocator, s: []const u8) ![]const u8 {
    const trimmed = try std.ArrayList(u8).initCapacity(allocator, s.len);
    defer trimmed.deinit();

    var skip_leading = true;
    var i: usize = 0;
    while (i < s.len) : (i += 1) {
        if (skip_leading and isBlankLine(s, i)) {
            while (i < s.len and s[i] != '\n') : (i += 1) {}
            continue;
        }
        skip_leading = false;
        try trimmed.append(s[i]);
    }

    var result = trimmed.toOwnedSlice() catch s;
    if (result.ptr != s.ptr) {
        return result;
    }
    return s;
}

fn stripLeadingBlankLines(allocator: std.mem.Allocator, s: []const u8) ![]const u8 {
    _ = allocator;
    var i: usize = 0;
    while (i < s.len and isBlankLine(s, i)) {
        while (i < s.len and s[i] != '\n') : (i += 1) {}
        if (i < s.len) i += 1;
    }
    if (i > 0) {
        return s[i..];
    }
    return s;
}

fn isBlankLine(s: []const u8, start: usize) bool {
    var i = start;
    while (i < s.len and (s[i] == ' ' or s[i] == '\t')) : (i += 1) {}
    return i < s.len and (s[i] == '\n' or s[i] == '\r');
}

fn removeIndentation(allocator: std.mem.Allocator, s: []const u8, indent: i32) ![]const u8 {
    const result = try std.ArrayList(u8).initCapacity(allocator, s.len);
    defer result.deinit();

    var i: usize = 0;
    var line_start: usize = 0;
    while (i < s.len) : (i += 1) {
        if (s[i] == '\n' or s[i] == '\r') {
            try result.append(s[i]);
            if (s[i] == '\r' and i + 1 < s.len and s[i + 1] == '\n') {
                try result.append(s[i + 1]);
                i += 1;
            }
            line_start = result.items.len;

            var j: i32 = 0;
            while (i + 1 < s.len and j < indent) : (j += 1) {
                const next = s[i + 1];
                if (next == ' ') {
                    i += 1;
                } else if (next == '\t') {
                    i += 1;
                } else {
                    break;
                }
            }
        } else if (result.items.len > line_start) {
            try result.append(s[i]);
        } else {
            try result.append(s[i]);
        }
    }

    return result.toOwnedSlice();
}

fn appendToSlice(allocator: std.mem.Allocator, slice: []*MarkdownNode, item: *MarkdownNode) ![]*MarkdownNode {
    const new_slice = try allocator.alloc(*MarkdownNode, slice.len + 1);
    std.mem.copy(*MarkdownNode, new_slice, slice);
    new_slice[slice.len] = item;
    allocator.free(slice);
    return new_slice;
}
