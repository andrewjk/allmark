const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const RuleSet = @import("../types/RuleSet.zig").RuleSet;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const isSpace = @import("../utils/isSpace.zig").isSpace;
const isNewLine = @import("../utils/isNewLine.zig").isNewLine;
const newBlock = @import("../utils/newBlock.zig").newBlock;
const newInline = @import("../utils/newInline.zig").newInline;
const parseLine = @import("./parseLine.zig").parseLine;
const parseBlockInlines = @import("./parseBlockInlines.zig").parseBlockInlines;
const extractFrontMatter = @import("../utils/extractFrontMatter.zig").extractFrontMatter;

pub fn parse(allocator: std.mem.Allocator, src: []const u8, rules: RuleSet) !*MarkdownNode {
    const document = newBlock(allocator, "document", 0, 1, "", 0) catch unreachable;

    var i: usize = 0;
    var index: usize = 0;
    while (index < src.len) : (index += 1) {
        if (!isSpace(src[index])) {
            break;
        } else if (isNewLine(src[index])) {
            i = index + 1;
        }
    }

    var frontmatter: ?[]const u8 = null;
    if (i < src.len and src[i] == '-') {
        frontmatter = extractFrontMatter(allocator, document, src, index) catch null;
        if (frontmatter) |fm| {
            i = index + fm.len;
        }
    }

    var openNodes = std.ArrayList(*MarkdownNode).initCapacity(allocator, 1) catch unreachable;
    openNodes.append(allocator, document) catch unreachable;

    var rulesMap = std.StringHashMap(*const BlockRule).init(allocator);
    for (rules.blocks) |rule| {
        try rulesMap.put(rule.name, rule);
    }

    var state = BlockParserState{
        .allocator = allocator,
        .rules = rules.blocks,
        .rulesMap = rulesMap,
        .src = src,
        .i = i,
        .line = 1,
        .lineStart = 0,
        .indent = 0,
        .isEscaped = false,
        .maybeContinue = false,
        .hasBlankLine = false,
        .openNodes = openNodes,
        .refs = std.StringHashMap(@import("../types/LinkReference.zig").LinkReference).init(allocator),
        .footnotes = std.StringHashMap(@import("../types/FootnoteReference.zig").FootnoteReference).init(allocator),
    };
    defer state.openNodes.deinit(allocator);
    defer {
        var iter = state.refs.iterator();
        while (iter.next()) |entry| {
            state.allocator.free(entry.key_ptr.*);
            state.allocator.free(entry.value_ptr.url);
            state.allocator.free(entry.value_ptr.title);
        }
        state.refs.deinit();
    }
    defer {
        var iter = state.footnotes.iterator();
        while (iter.next()) |entry| {
            // Free the label (same as key, but stored separately in FootnoteReference)
            state.allocator.free(entry.value_ptr.label);
        }
        state.footnotes.deinit();
    }
    defer {
        var iter = state.rulesMap.iterator();
        while (iter.next()) |_| {
            // No need to free the key - it's a string literal
        }
        state.rulesMap.deinit();
    }

    while (state.i < state.src.len) {
        const start_i = state.i;
        parseLine(&state);
        // Debug
        // Safety check: ensure we made progress
        if (state.i == start_i) {
            state.i += 1; // Force progress to avoid infinite loop
        }
    }

    var j = state.openNodes.items.len;
    while (j > 0) : (j -= 1) {
        const openNode = state.openNodes.items[j - 1];
        openNode.length = state.i - openNode.index;
        if (state.rulesMap.get(openNode.type)) |rule| {
            if (rule.closeNode) |closeFn| {
                closeFn(&state, openNode);
            }
        }
    }

    parseBlockInlines(allocator, document, rules.inlines, state.refs, state.footnotes) catch |err| {
        std.debug.print("Error parsing block inlines: {s}\n", .{@errorName(err)});
    };

    if (frontmatter) |fm| {
        document.info = fm;
    }

    return document;
}
