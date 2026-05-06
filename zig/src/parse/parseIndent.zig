const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const isSpace = @import("../utils/isSpace.zig").isSpace;

pub fn parseIndent(state: *BlockParserState) void {
    if (state.i < state.src.len and isSpace(state.src[state.i])) {
        while (state.i < state.src.len) {
            const char = state.src[state.i];
            if (char == ' ') {
                state.indent += 1;
            } else if (char == '\t') {
                state.indent += 4 - @mod(state.indent, 4);
            } else if (char == '\n') {
                state.hasBlankLine = true;
                break;
            } else if (char == '\r') {
                if (state.i + 1 >= state.src.len or state.src[state.i + 1] != '\n') {
                    state.hasBlankLine = true;
                    break;
                }
            } else {
                break;
            }
            state.i += 1;
        }
    }
}
