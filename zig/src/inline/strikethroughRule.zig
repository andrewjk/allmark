const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const testTagMarks = @import("tagMarksRule.zig").testTagMarks;

pub fn testStrikethrough(state: *InlineParserState, parent: *MarkdownNode) bool {
    if (!state.isEscaped and state.i < state.src.len and state.src[state.i] == '~') {
        return testTagMarks("strikethrough", '~', state, parent, strikethroughRule.precedence.?);
    }
    return false;
}

pub const strikethroughRule = InlineRule{
    .name = "strikethrough",
    .@"test" = testStrikethrough,
    .precedence = 5,
};
