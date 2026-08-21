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

    // Skip document -- it's always going to continue
    var i: usize = 1;
    while (i < state.openNodes.items.len) {
        const node = state.openNodes.items[i];
        const rule = state.rulesMap.get(node.type);
        if (rule.?.testContinue(state, node)) {
            parseIndent(state);
        } else {
            var j = state.openNodes.items.len;
            while (j > i) {
                j -= 1;
                const open_node = state.openNodes.items[j];
                closeNode(state, open_node);
            }
            state.openNodes.shrinkRetainingCapacity(i);
            break;
        }
        i += 1;
    }

    // Get the end of the line
    var end_of_line = state.i;
    var next_index = state.src.len;
    while (end_of_line < state.src.len) {
        const code = state.src[end_of_line];
        if (code == '\n') {
            next_index = end_of_line + 1;
            break;
        } else if (code == '\r') {
            next_index = end_of_line + 1;
            if (end_of_line + 1 < state.src.len and state.src[end_of_line + 1] == '\n') {
                next_index += 1;
            }
            break;
        }
        end_of_line += 1;
    }

    const parent = state.openNodes.items[state.openNodes.items.len - 1];
    parseBlock(state, parent, end_of_line);

    // NOTE: a rule can move state.i past the next line
    // (e.g. for a HTML block or link reference containing a newline)
    if (state.i < next_index) {
        state.i = next_index;
        state.lineStart = next_index;
    }
}