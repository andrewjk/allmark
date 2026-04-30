const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const testTagMarks = @import("tagMarksRule.zig").testTagMarks;

pub fn testSubscript(state: *InlineParserState, parent: *MarkdownNode) bool {
    if (!state.isEscaped and state.i < state.src.len and state.src[state.i] == '~') {
        if (state.i + 1 < state.src.len and state.src[state.i + 1] == '~') {
            return false;
        }
        return testTagMarks("subscript", '~', state, parent, subscriptRule.precedence.?);
    }
    return false;
}

pub const subscriptRule = InlineRule{
    .name = "subscript",
    .@"test" = testSubscript,
    .precedence = 5,
};
