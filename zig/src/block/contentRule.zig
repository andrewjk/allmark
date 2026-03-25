const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const getEndOfLine = @import("../utils/getEndOfLine.zig").getEndOfLine;

pub fn testStart(state: *BlockParserState, parent: *MarkdownNode) bool {
    const end_of_line = getEndOfLine(state);
    const content = state.src[state.i..end_of_line];

    var new_content = std.ArrayList(u8).initCapacity(state.allocator, parent.content.len + content.len + 10) catch unreachable;

    if (parent.acceptsContent) {
        new_content.appendSlice(state.allocator, parent.content) catch unreachable;
        if (!state.hasBlankLine) {
            const spaces = state.indent;
            var i: usize = 0;
            while (i < spaces) : (i += 1) {
                new_content.append(state.allocator, ' ') catch unreachable;
            }
        }
        new_content.appendSlice(state.allocator, content) catch unreachable;

        if (parent.content_allocated) {
            state.allocator.free(parent.content);
        }
        parent.content = new_content.toOwnedSlice(state.allocator) catch unreachable;
        new_content.deinit(state.allocator);
        parent.content_allocated = true;

        state.hasBlankLine = false;
    } else {
        new_content.deinit(state.allocator);
        var new_content2 = std.ArrayList(u8).initCapacity(state.allocator, parent.content.len + content.len + 1) catch unreachable;
        new_content2.appendSlice(state.allocator, parent.content) catch unreachable;
        new_content2.appendSlice(state.allocator, content) catch unreachable;
        if (parent.content_allocated) {
            state.allocator.free(parent.content);
        }
        parent.content = new_content2.toOwnedSlice(state.allocator) catch unreachable;
        new_content2.deinit(state.allocator);
        parent.content_allocated = true;
    }

    state.i = end_of_line;

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
