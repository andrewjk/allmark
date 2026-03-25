const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const RuleSet = @import("../types/RuleSet.zig").RuleSet;
const isNewLine = @import("../utils/isNewLine.zig").isNewLine;
const newBlock = @import("../utils/newBlock.zig").newBlock;
const newInline = @import("../utils/newInline.zig").newInline;
const parseLine = @import("./parseLine.zig").parseLine;
const parseBlockInlines = @import("./parseBlockInlines.zig").parseBlockInlines;

pub fn parse(allocator: std.mem.Allocator, src: []const u8, rules: RuleSet) !*MarkdownNode {
    const document = newBlock(allocator, "document", 0, 1, "", 0) catch unreachable;

    var i: usize = 0;
    while (i < src.len) : (i += 1) {
        if (!isNewLine(src[i])) {
            break;
        }
    }

    var openNodes = std.ArrayList(*MarkdownNode).initCapacity(allocator, 1) catch unreachable;
    openNodes.append(allocator, document) catch unreachable;

    var state = BlockParserState{
        .allocator = allocator,
        .rules = rules.blocks,
        .src = src,
        .i = i,
        .line = 1,
        .lineStart = 0,
        .indent = 0,
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
        if (state.rules.get(openNode.type)) |rule| {
            if (rule.closeNode) |closeFn| {
                closeFn(&state, openNode);
            }
        }
    }

    parseBlockInlines(allocator, document, rules.inlines, state.refs, state.footnotes) catch |err| {
        std.debug.print("Error parsing block inlines: {s}\n", .{@errorName(err)});
    };

    return document;
}
