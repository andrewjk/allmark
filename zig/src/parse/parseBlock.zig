const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const getEndOfLine = @import("../utils/getEndOfLine.zig").getEndOfLine;

pub fn parseBlock(state: *BlockParserState, parent: *MarkdownNode) void {
    var it = state.rules.iterator();
    while (it.next()) |entry| {
        const handled = entry.value_ptr.*.testStart(state, parent);

        if (handled) {
            return;
        }
    }

    // No rule handled this line, skip to the end of line
    state.i = getEndOfLine(state);
}
