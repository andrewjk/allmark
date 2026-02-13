const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;

pub fn parseBlock(state: *BlockParserState, parent: *MarkdownNode) void {
    var ruleIter = state.rules.valueIterator();
    while (ruleIter.next()) |rule| {
        const handled = rule.testStart(state, parent);

        if (handled) {
            return;
        }
    }
}
