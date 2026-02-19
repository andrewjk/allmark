const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const testCriticMarks = @import("criticMarksRule.zig").testCriticMarks;

pub fn testComment(state: *InlineParserState, parent: *MarkdownNode) bool {
    return testCriticMarks("comment", '>', state, parent, '<');
}

pub const commentRule = InlineRule{
    .name = "comment",
    .@"test" = testComment,
};
