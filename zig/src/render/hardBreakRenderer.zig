const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;

pub fn render(node: *const MarkdownNode, state: *RendererState, decode: ?bool) void {
    _ = node;
    _ = decode;
    state.output.appendSlice(state.allocator, "<br />\n") catch unreachable;
}

pub const hardBreakRenderer = Renderer{
    .name = "hard_break",
    .render = render,
};
