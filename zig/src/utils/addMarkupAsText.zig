const std = @import("std");
const appendChild = @import("appendChild.zig").appendChild;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;

pub fn addMarkupAsText(
    allocator: std.mem.Allocator,
    markup: []const u8,
    state: *@import("../types/InlineParserState.zig").InlineParserState,
    parent: *@import("../types/MarkdownNode.zig").MarkdownNode,
) !void {
    const newNode = @import("newNode.zig").newNode;

    if (parent.children == null or parent.children.?.len == 0) {
        const text_node = try newNode(allocator, "text", false, state.i, state.line, 1, "", 0, null);
        try appendChild(allocator, parent, text_node);
    }

    const children = parent.children.?;
    const lastNode = children[children.len - 1];
    const haveText = std.mem.eql(u8, lastNode.type, "text");
    const text_node = if (haveText) lastNode else blk: {
        const tn = try newNode(allocator, "text", false, state.i, state.line, 1, "", 0, null);
        try appendChild(allocator, parent, tn);
        break :blk tn;
    };

    const old_markup = text_node.markup;
    text_node.markup = try std.fmt.allocPrint(allocator, "{s}{s}", .{ old_markup, markup });
    if (text_node.markup_allocated) {
        allocator.free(old_markup);
    }
    text_node.markup_allocated = true;
    state.i += markup.len;
}
