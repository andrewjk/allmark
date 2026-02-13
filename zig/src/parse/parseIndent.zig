const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const isNewLine = @import("../utils/isNewLine.zig").isNewLine;
const isSpace = @import("../utils/isSpace.zig").isSpace;

pub fn parseIndent(state: *BlockParserState) void {
    if (isSpace(state.src[state.i])) {
        while (state.i < state.src.len) {
            const char = state.src[state.i];
            if (char == ' ') {
                state.indent += 1;
            } else if (char == '\t') {
                state.indent += 4 - (state.indent % 4);
            } else if (isNewLine(char)) {
                state.hasBlankLine = true;
                break;
            } else {
                break;
            }
            state.i += 1;
        }
    }
}
