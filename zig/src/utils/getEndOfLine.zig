const std = @import("std");

pub fn getEndOfLine(state: *@import("../types/BlockParserState.zig").BlockParserState) usize {
    var end_of_line = state.i;
    while (end_of_line < state.src.len) {
        if (isNewLine(state.src[end_of_line])) {
            end_of_line += 1;
            state.lineStart = end_of_line;
            break;
        }
        end_of_line += 1;
    }
    return end_of_line;
}

fn isNewLine(char: u8) bool {
    return char == '\r' or char == '\n';
}
