const std = @import("std");
const BlockParserState = @import("../types/BlockState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const isNumeric = @import("../utils/isAlphaNumeric.zig").isNumeric;
const isSpace = @import("../utils/isSpace.zig").isSpace;
const newNode = @import("../utils/newNode.zig").newNode;

pub fn testStart(state: *BlockParserState, parent: *MarkdownNode) bool {
    if (parent.type != "list_item") return false;

    if (state.i >= state.src.len) return false;

    if (state.src[state.i] == '[' and state.src[state.i + 2] == ']' and state.indent <= 3) {
        const x = state.src[state.i + 1];
        if (isSpace(state.src[state.i + 3]) {
            const markup = try std.fmt.allocPrint(state.allocator, "[{c}]", .{x});
            defer state.allocator.free(markup);
            const task = newNode(state.allocator, "list_task_item", false, state.i, state.line, 1, markup, 0, null) catch unreachable;

            parent.children.?.append(task) catch unreachable;
            state.i += 3;

            return true;
        }
    }

    return false;
}

pub fn testContinue(_state: *BlockParserState, _node: *MarkdownNode) bool {
    return false;
}

pub const listTaskItemRule = BlockRule{
    .name = "list_task_item",
    .testStart = testStart,
    .testContinue = testContinue,
};
