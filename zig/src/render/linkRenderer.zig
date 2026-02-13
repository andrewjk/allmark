const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const renderChildren = @import("renderChildren.zig").renderChildren;
const renderUtils = @import("renderUtils.zig");

pub fn render(node: *const MarkdownNode, state: *RendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    renderUtils.startNewLine(node, state);
    const title = if (node.title) |t| blk: {
        const title_buf = try std.ArrayList(u8).initCapacity(state.output.allocator, t.len + 9);
        try title_buf.writer().print(" title=\"{s}\"", .{t});
        break :blk title_buf.toOwnedSlice();
    } else "";

    state.output.writer().print("<a href=\"{s}{s}\">", .{ node.info, title }) catch unreachable;
    renderChildren.renderChildren(node, state, true);
    state.output.appendSlice("</a>") catch unreachable;
    renderUtils.endNewLine(node, state);
}

pub const linkRenderer = Renderer{
    .name = "link",
    .render = render,
};
