const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const getEndOfLine = @import("../utils/getEndOfLine.zig").getEndOfLine;
const isEscaped = @import("../utils/isEscaped.zig").isEscaped;

pub fn parseBlock(state: *BlockParserState, parent: *MarkdownNode) void {
    state.isEscaped = isEscaped(state.src, state.i);

    for (state.rules) |rule| {
        const handled = rule.testStart(state, parent);

        if (handled) {
            return;
        }
    }

    // No rule handled this line, skip to the end of line
    state.i = getEndOfLine(state);
}
