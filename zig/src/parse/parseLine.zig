const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const closeNode = @import("../utils/closeNode.zig").closeNode;
const parseBlock = @import("parseBlock.zig").parseBlock;
const parseIndent = @import("parseIndent.zig").parseIndent;

pub fn parseLine(state: *BlockParserState) void {
    state.indent = 0;
    state.line += 1;
    state.lineStart = state.i;
    state.maybeContinue = false;

    parseIndent(state);

    var i: usize = 1;
    while (i < state.openNodes.items.len) {
        const node = state.openNodes.items[i];
        const rule = state.rules.get(node.type).?;
        if (rule.testContinue(state, node)) {
            parseIndent(state);
        } else {
            closeNode(state, node);
            state.openNodes.shrinkRetainingCapacity(i);
            break;
        }
        i += 1;
    }

    const parent = state.openNodes.items[state.openNodes.items.len - 1];
    parseBlock(state, parent);
}
