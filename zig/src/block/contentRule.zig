const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const getLineEnding = @import("../utils/getLineEnding.zig").getLineEnding;

pub fn testStart(state: *BlockParserState, parent: *MarkdownNode, end_of_line: usize) bool {
    const content = state.src[state.i..end_of_line];
    const line_ending = getLineEnding(state, end_of_line);

    var new_content = std.ArrayList(u8).initCapacity(state.allocator, parent.content.len + content.len + 10) catch unreachable;

    if (parent.acceptsContent) {
        new_content.appendSlice(state.allocator, parent.content) catch unreachable;
        if (state.hasBlankLine) {
            state.hasBlankLine = false;
        } else {
            const spaces = state.indent;
            var i: usize = 0;
            while (i < spaces) : (i += 1) {
                new_content.append(state.allocator, ' ') catch unreachable;
            }
        }
        new_content.appendSlice(state.allocator, content) catch unreachable;
    } else {
        new_content.appendSlice(state.allocator, parent.content) catch unreachable;
        new_content.appendSlice(state.allocator, state.spaces) catch unreachable;
        state.spaces = "";
        new_content.appendSlice(state.allocator, content) catch unreachable;
    }
    new_content.appendSlice(state.allocator, line_ending) catch unreachable;

    if (parent.content_allocated) {
        state.allocator.free(parent.content);
    }
    parent.content = new_content.toOwnedSlice(state.allocator) catch unreachable;
    new_content.deinit(state.allocator);
    parent.content_allocated = true;

    return true;
}

pub fn testContinue(state: *BlockParserState, node: *MarkdownNode) bool {
    _ = state;
    _ = node;
    return false;
}

pub const contentRule = BlockRule{
    .name = "content",
    .testStart = testStart,
    .testContinue = testContinue,
};
