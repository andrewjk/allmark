const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;

pub fn getLineEnding(state: *BlockParserState, end_of_line: usize) []const u8 {
    if (end_of_line < state.src.len and state.src[end_of_line] == '\n') {
        return "\n";
    }
    if (end_of_line < state.src.len and state.src[end_of_line] == '\r') {
        if (end_of_line + 1 < state.src.len and state.src[end_of_line + 1] == '\n') {
            return "\r\n";
        }
        return "\r";
    }
    return "";
}