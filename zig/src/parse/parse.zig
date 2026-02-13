const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const RuleSet = @import("../types/RuleSet.zig").RuleSet;
const isNewLine = @import("../utils/isNewLine.zig").isNewLine;
const newNode = @import("../utils/newNode.zig").newNode;
const parseLine = @import("./parseLine.zig").parseLine;
const parseBlockInlines = @import("./parseBlockInlines.zig").parseBlockInlines;

pub fn parse(allocator: std.mem.Allocator, src: []const u8, rules: RuleSet, debug: ?bool) !*MarkdownNode {
    const document = newNode(allocator, "document", true, 0, 1, 1, "", 0, null) catch unreachable;

    var i: usize = 0;
    while (i < src.len) : (i += 1) {
        if (!isNewLine(&.{src[i]})) {
            break;
        }
    }

    var state = BlockParserState{
        .rules = rules.blocks,
        .src = src,
        .i = i,
        .line = 1,
        .lineStart = 0,
        .indent = 0,
        .maybeContinue = false,
        .hasBlankLine = false,
        .openNodes = std.ArrayList(*MarkdownNode).initCapacity(allocator, 1) catch unreachable,
        .refs = std.StringHashMap(@import("../types/LinkReference.zig").LinkReference).init(allocator),
        .footnotes = std.StringHashMap(@import("../types/FootnoteReference.zig").FootnoteReference).init(allocator),
        .debug = debug,
    };
    defer state.openNodes.deinit();
    defer state.refs.deinit();
    defer state.footnotes.deinit();

    while (state.i < state.src.len) {
        parseLine(&state);
    }

    parseBlockInlines(allocator, document, rules.inlines, state.refs, state.footnotes);

    return document;
}
