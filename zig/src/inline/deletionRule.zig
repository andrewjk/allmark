const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const testCriticMarks = @import("criticMarksRule.zig").testCriticMarks;

pub fn testDeletion(state: *InlineParserState, parent: *MarkdownNode) bool {
    return testCriticMarks("deletion", '-', state, parent);
}

pub const deletionRule = InlineRule{
    .name = "deletion",
    .@"test" = testDeletion,
};
