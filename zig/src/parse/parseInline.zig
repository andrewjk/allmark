const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;

pub fn parseInline(allocator: std.mem.Allocator, state: *InlineParserState, parent: *MarkdownNode) !void {
    _ = allocator;
    while (state.i < state.src.len) {
        const char = state.src[state.i];
        if (char == '\r' or char == '\n') {
            if (char == '\r' and state.i + 1 < state.src.len and state.src[state.i + 1] == '\n') {
                state.i += 1;
            }

            state.line += 1;
            state.lineStart = state.i;
        }

        var ruleIter = state.rules.valueIterator();
        while (ruleIter.next()) |rule| {
            if (rule.@"test"(state, parent)) {
                break;
            }
        }
    }
}
