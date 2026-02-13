const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;

pub fn render(node: *const MarkdownNode, state: *RendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;
    state.output.appendSlice(node.content) catch unreachable;
}

pub const htmlBlockRenderer = Renderer{
    .name = "html_block",
    .render = render,
};

pub const htmlSpanRenderer = Renderer{
    .name = "html_span",
    .render = render,
};
