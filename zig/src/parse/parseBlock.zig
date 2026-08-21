const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const isEscaped = @import("../utils/isEscaped.zig").isEscaped;

pub fn parseBlock(state: *BlockParserState, parent: *MarkdownNode, end_of_line: usize) void {
    state.isEscaped = isEscaped(state.src, state.i);

    for (state.rules) |rule| {
        const handled = rule.testStart(state, parent, end_of_line);

        if (handled) {
            return;
        }
    }
}