const std = @import("std");

pub fn movePastMarker(
    marker_length: usize,
    state: *@import("../types/BlockParserState.zig").BlockParserState,
) void {
    state.i += marker_length;
    if (state.i + 1 < state.src.len and state.src[state.i] == '\t' and state.src[state.i + 1] == '\t') {
        state.indent = 6;
        state.i += 2;
    } else if (state.i < state.src.len and state.src[state.i] == ' ') {
        state.indent = 0;
        state.i += 1;
    }
}
