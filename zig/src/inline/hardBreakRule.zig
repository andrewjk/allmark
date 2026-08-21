const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const newInline = @import("../utils/newInline.zig").newInline;
const appendChild = @import("../utils/appendChild.zig").appendChild;
const consumeSpaces = @import("../utils/consumeSpaces.zig").consumeSpaces;
const isNewLine = @import("../utils/isNewLine.zig").isNewLine;
const isSpace = @import("../utils/isSpace.zig").isSpace;

pub fn testHardBreak(state: *InlineParserState, parent: *MarkdownNode) bool {
    if (state.i < state.src.len) {
        if (state.src[state.i] == '\\') {
            var next_char: u8 = 0;
            if (state.i + 1 < state.src.len) {
                next_char = state.src[state.i + 1];
            }
            if (isNewLine(next_char)) {
                const hb = newInline(state.allocator, "hard_break", state.parentIndex + state.i, state.line, "\\", 0) catch unreachable;
                hb.*.length = 2;
                appendChild(state.allocator, parent, hb) catch unreachable;
                handleHardBreakEnd(state, state.i + 2);
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
                    end = i;
                    if (i + 1 < state.src.len and state.src[i + 1] == '\n') {
                        end += 1;
                    }
                    break;
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
                handleHardBreakEnd(state, end + 1);
                return true;
            }
        }
    }

    return false;
}

fn handleHardBreakEnd(state: *InlineParserState, end: usize) void {
    state.i = end;
    state.line += 1;
    state.lineStart = state.i;

    // "Spaces at the end of the line and beginning of the next line are removed"
    if (state.i < state.src.len and isSpace(state.src[state.i])) {
        const space = consumeSpaces(state.allocator, state.src, state.i) catch unreachable;
        state.i += space.len;
        state.allocator.free(space);
    }
}

pub const hardBreakRule = InlineRule{
    .name = "hard_break",
    .@"test" = testHardBreak,
};
