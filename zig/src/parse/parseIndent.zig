const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const isSpace = @import("../utils/isSpace.zig").isSpace;

pub fn parseIndent(state: *BlockParserState) void {
    if (state.i < state.src.len and isSpace(state.src[state.i])) {
        const start = state.i;
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
                state.hasBlankLine = true;
                if (state.i + 1 < state.src.len and state.src[state.i + 1] == '\n') {
                    state.i += 1;
                }
                break;
            } else {
                break;
            }
            state.i += 1;
        }
        state.spaces = state.src[start..state.i];
    }
}
