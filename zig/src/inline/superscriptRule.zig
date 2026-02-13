const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const isEscaped = @import("../utils/isEscaped.zig").isEscaped;
const testTagMarks = @import("tagMarksRule.zig").testTagMarks;

pub fn testSuperscript(state: *InlineParserState, parent: *MarkdownNode) bool {
    if (state.i < state.src.len and state.src[state.i] == '^' and !isEscaped(state.src, state.i)) {
        return testTagMarks("superscript", '^', state, parent);
    }
    return false;
}

pub const superscriptRule = InlineRule{
    .name = "superscript",
    .@"test" = testSuperscript,
};
