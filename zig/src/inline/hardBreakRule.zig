const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const newInline = @import("../utils/newInline.zig").newInline;
const appendChild = @import("../utils/appendChild.zig").appendChild;

pub fn testHardBreak(state: *InlineParserState, parent: *MarkdownNode) bool {
    if (state.i < state.src.len) {
        if (state.src[state.i] == '\\') {
            var end = state.i + 2;
            var next_char: u8 = undefined;
            if (state.i + 1 < state.src.len) {
                next_char = state.src[state.i + 1];
                if (next_char == '\r') {
                    if (state.i + 2 < state.src.len) {
                        next_char = state.src[state.i + 2];
                        end += 1;
                    }
                }
            }
            if (next_char == '\n') {
                const hb = newInline(state.allocator, "hard_break", state.parentIndex + state.i, state.line, "\\", 0) catch unreachable;
                hb.*.length = 2;
                appendChild(state.allocator, parent, hb) catch unreachable;
                state.i = end;
                return true;
            }
        } else if (state.src[state.i] == ' ') {
            var spaces: usize = 1;
            var end = state.src.len;
            var i = state.i + 1;
            while (i < state.src.len) {
                if (state.src[i] == '\n') {
                    end = i;
                    break;
                } else if (state.src[i] == '\r') {
                    // Keep going...
                } else if (state.src[i] == ' ') {
                    spaces += 1;
                } else {
                    return false;
                }
                i += 1;
            }

            if (spaces >= 2) {
                const hb = newInline(state.allocator, "hard_break", state.parentIndex + state.i, state.line, "  ", 0) catch unreachable;
                hb.*.length = spaces;
                appendChild(state.allocator, parent, hb) catch unreachable;
                state.i = end + 1;
                return true;
            }
        }
    }

    return false;
}

pub const hardBreakRule = InlineRule{
    .name = "hard_break",
    .@"test" = testHardBreak,
};
