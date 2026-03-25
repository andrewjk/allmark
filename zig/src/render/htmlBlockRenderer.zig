const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;

pub fn render(node: *const MarkdownNode, state: *RendererState, decode: ?bool) void {
    _ = decode;
    state.output.appendSlice(state.allocator, node.content) catch unreachable;
}

pub const htmlBlockRenderer = Renderer{
    .name = "html_block",
    .render = render,
};
