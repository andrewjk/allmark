const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const testCriticMarks = @import("criticMarksRule.zig").testCriticMarks;

pub fn testInsertion(state: *InlineParserState, parent: *MarkdownNode) bool {
    return testCriticMarks("insertion", '+', state, parent);
}

pub const insertionRule = InlineRule{
    .name = "insertion",
    .@"test" = testInsertion,
};
