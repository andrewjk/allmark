const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const isNumeric = @import("../utils/isAlphaNumeric.zig").isNumeric;
const isSpace = @import("../utils/isSpace.zig").isSpace;
const newInline = @import("../utils/newInline.zig").newInline;
const appendChild = @import("../utils/appendChild.zig").appendChild;
const movePastMarker = @import("../utils/movePastMarker.zig").movePastMarker;

pub fn testStart(state: *BlockParserState, parent: *MarkdownNode) bool {
    if (!std.mem.eql(u8, parent.type, "list_item")) {
        return false;
    }

    if (state.i >= state.src.len) return false;
    if (state.i + 3 >= state.src.len) return false;

    if (state.src[state.i] == '[' and state.src[state.i + 2] == ']' and state.indent <= 3) {
        const x = state.src[state.i + 1];
        if (isSpace(state.src[state.i + 3])) {
            // GitHub doesn't support task lists in block quotes
            var inBlockQuote = false;
            for (state.openNodes.items) |n| {
                if (std.mem.eql(u8, n.type, "block_quote")) {
                    inBlockQuote = true;
                    break;
                }
            }
            if (inBlockQuote) return false;

            const markup = std.fmt.allocPrint(state.allocator, "[{c}]", .{x}) catch return false;
            defer state.allocator.free(markup);
            const task = newInline(state.allocator, "list_task_item", state.i, state.line, markup, 0) catch unreachable;
            task.length = 3;

            appendChild(state.allocator, parent, task) catch unreachable;
            movePastMarker(3, state);

            return false;
        }
    }

    return false;
}

pub fn testContinue(state: *BlockParserState, node: *MarkdownNode) bool {
    _ = state;
    _ = node;
    return false;
}

pub const listTaskItemRule = BlockRule{
    .name = "list_task_item",
    .testStart = testStart,
    .testContinue = testContinue,
};
