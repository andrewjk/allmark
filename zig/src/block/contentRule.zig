const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const getEndOfLine = @import("../utils/getEndOfLine.zig").getEndOfLine;
const newNode = @import("../utils/newNode.zig").newNode;

pub fn testStart(state: *BlockParserState, parent: *MarkdownNode) bool {
    const end_of_line = getEndOfLine(state);
    const content = state.src[state.i..end_of_line];

    if (parent.acceptsContent) {
        if (state.hasBlankLine) {
            try parent.content.writer().print("\n", .{});
        } else {
            const padding = try std.fmt.allocPrint(state.allocator, " ", .{state.indent});
            defer state.allocator.free(padding);
            try parent.content.writer().print("{s}", .{padding});
        }
        try parent.content.writer().print("{s}", .{content});
    } else {
        try parent.content.writer().print("{s}", .{content});
    }

    state.hasBlankLine = false;
    state.i = end_of_line;

    return true;
}

pub fn testContinue(_state: *BlockParserState, _node: *MarkdownNode) bool {
    return false;
}

pub const contentRule = BlockRule{
    .name = "content",
    .testStart = testStart,
    .testContinue = testContinue,
};
