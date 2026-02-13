const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const isSpaceFn = @import("../utils/isSpace.zig").isSpace;

pub fn testStart(state: *BlockParserState, _parent: *MarkdownNode) bool {
    _ = _parent;
    if (state.i < state.src.len and isSpaceFn(state.src[state.i])) {
        while (state.i < state.src.len) {
            const char = state.src[state.i];
            if (char == ' ') {
                state.indent += 1;
            } else if (char == '\t') {
                state.indent += 4 - (state.indent % 4);
            } else {
                break;
            }
            state.i += 1;
        }
    }

    return false;
}

pub fn testContinue(_state: *BlockParserState, _node: *MarkdownNode) bool {
    _ = _state;
    _ = _node;
    return false;
}

pub const indentRule = BlockRule{
    .name = "indent",
    .testStart = testStart,
    .testContinue = testContinue,
};
