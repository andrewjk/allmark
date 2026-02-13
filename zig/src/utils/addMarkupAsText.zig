const std = @import("std");

pub fn addMarkupAsText(
    allocator: std.mem.Allocator,
    markup: []const u8,
    state: *@import("../types/InlineParserState.zig").InlineParserState,
    parent: *@import("../types/MarkdownNode.zig").MarkdownNode,
) !void {
    const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
    const newNode = @import("newNode.zig").newNode;

    const children = parent.children orelse {
        parent.children = try std.ArrayList(*MarkdownNode).initCapacity(allocator, 1);
        parent.children.?.appendAssumeCapacity(
            try newNode(allocator, "text", false, state.i, state.line, 1, "", 0),
        );
    };

    const lastNode = children[children.len - 1];
    const haveText = std.mem.eql(u8, lastNode.type, "text");
    const text_node = if (haveText) lastNode else try newNode(allocator, "text", false, state.i, state.line, 1, "", 0);

    text_node.markup = try std.fmt.allocPrint(allocator, "{s}{s}", .{ text_node.markup, markup });
    if (!haveText) {
        try children.append(text_node);
    }
    state.i += markup.len;
}
