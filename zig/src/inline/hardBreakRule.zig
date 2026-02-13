const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const isNewLine = @import("../utils/isNewLine.zig").isNewLine;
const newNode = @import("../utils/newNode.zig").newNode;

pub fn testHardBreak(state: *InlineParserState, parent: *MarkdownNode) bool {
    if (state.i < state.src.len and state.src[state.i] == '\\' and state.i + 1 < state.src.len and isNewLine(&.{state.src[state.i + 1]})) {
        const hb = newNode(state.allocator, "hard_break", false, state.i, state.line, 1, "\\", 0, null) catch unreachable;
        state.i += 2;
        const children = parent.children orelse {
            parent.children = std.ArrayList(*MarkdownNode).initCapacity(state.allocator, 1) catch unreachable;
            parent.children.?.appendAssumeCapacity(hb);
            return true;
        };
        children.append(hb) catch unreachable;
        return true;
    }

    return false;
}

pub const hardBreakRule = InlineRule{
    .name = "hard_break",
    .@"test" = testHardBreak,
};
