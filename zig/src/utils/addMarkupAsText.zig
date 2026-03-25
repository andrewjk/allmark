const std = @import("std");
const appendChild = @import("appendChild.zig").appendChild;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const newText = @import("newText.zig").newText;

pub fn addMarkupAsText(
    allocator: std.mem.Allocator,
    markup: []const u8,
    state: *@import("../types/InlineParserState.zig").InlineParserState,
    parent: *@import("../types/MarkdownNode.zig").MarkdownNode,
) !void {
    if (parent.children == null or parent.children.?.len == 0) {
        const text_node = try newText(allocator, state.i, state.line, "", 0);
        try appendChild(allocator, parent, text_node);
    }

    const children = parent.children.?;
    const lastNode = children[children.len - 1];
    const haveText = std.mem.eql(u8, lastNode.type, "text");
    const text_node = if (haveText) lastNode else blk: {
        const tn = try newText(allocator, state.i, state.line, "", 0);
        try appendChild(allocator, parent, tn);
        break :blk tn;
    };

    const old_content = text_node.content;
    text_node.content = try std.fmt.allocPrint(allocator, "{s}{s}", .{ old_content, markup });
    if (text_node.content_allocated) {
        allocator.free(old_content);
    }
    text_node.content_allocated = true;
    state.i += markup.len;
}
