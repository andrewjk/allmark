const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const isNewLine = @import("../utils/isNewLine.zig").isNewLine;
const newInline = @import("../utils/newInline.zig").newInline;
const appendChild = @import("../utils/appendChild.zig").appendChild;

pub fn testHardBreak(state: *InlineParserState, parent: *MarkdownNode) bool {
    if (state.i < state.src.len) {
        if (state.src[state.i] == '\\' and state.i + 1 < state.src.len and isNewLine(state.src[state.i + 1])) {
            var bump: usize = 2;
            if (state.src[state.i + 1] == '\r' and
                state.i + 2 < state.src.len and
                state.src[state.i + 2] == '\n')
            {
                bump = 3;
            }
            const hb = newInline(state.allocator, "hard_break", state.parentIndex + state.i, state.line, "\\", 0) catch unreachable;
            hb.*.length = 2;
            appendChild(state.allocator, parent, hb) catch unreachable;
            state.i += bump;
            return true;
        } else if (state.src[state.i] == ' ') {
            var end = state.i;
            var i = state.i + 1;
            while (i < state.src.len) {
                if (isNewLine(state.src[i])) {
                    end = i;
                    break;
                } else if (state.src[i] == ' ') {
                    i += 1;
                    continue;
                } else {
                    return false;
                }
                i += 1;
            }

            if (end - state.i >= 2) {
                var bump: usize = 1;
                if (end < state.src.len and
                    state.src[end] == '\r' and
                    end + 1 < state.src.len and
                    state.src[end + 1] == '\n')
                {
                    bump = 2;
                }
                const hb = newInline(state.allocator, "hard_break", state.parentIndex + state.i, state.line, "\\", 0) catch unreachable;
                hb.*.length = end - state.i;
                appendChild(state.allocator, parent, hb) catch unreachable;
                state.i = end + bump;
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
