const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const renderUtils = @import("renderUtils.zig");

pub fn render(node: *const MarkdownNode, state: *RendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    renderUtils.startNewLine(node, state);
    const alt = getChildText(state.output.allocator, node);
    defer state.output.allocator.free(alt);

    const title = if (node.title) |t| blk: {
        const title_buf = try std.ArrayList(u8).initCapacity(state.output.allocator, t.len + 8);
        try title_buf.writer().print(" title=\"{s}\"", .{t});
        break :blk title_buf.toOwnedSlice();
    } else "";

    state.output.writer().print("<img src=\"{s}\" alt=\"{s}\"{s} />", .{ node.info, alt, title }) catch unreachable;
    renderUtils.endNewLine(node, state);
}

pub const imageRenderer = Renderer{
    .name = "image",
    .render = render,
};

fn getChildText(allocator: std.mem.Allocator, node: *const MarkdownNode) []const u8 {
    var buffer = std.ArrayList(u8).init(allocator);
    defer buffer.deinit();

    if (node.children) |children| {
        for (children) |child| {
            if (std.mem.eql(u8, child.type, "text")) {
                try buffer.appendSlice(child.markup);
            } else {
                const text = getChildText(allocator, child);
                defer allocator.free(text);
                try buffer.appendSlice(text);
            }
        }
    }

    return buffer.toOwnedSlice();
}
