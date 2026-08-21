const std = @import("std");

pub fn getEndOfLine(state: *@import("../types/BlockParserState.zig").BlockParserState) usize {
    var end_of_line = state.i;
    while (end_of_line < state.src.len) {
        const code = state.src[end_of_line];
        if (code == '\n') {
            end_of_line += 1;
            state.lineStart = end_of_line;
            break;
        } else if (code == '\r') {
            end_of_line += 1;
            if (end_of_line < state.src.len and state.src[end_of_line] == '\n') {
                end_of_line += 1;
            }
            state.lineStart = end_of_line;
            break;
        }
        end_of_line += 1;
    }
    return end_of_line;
}