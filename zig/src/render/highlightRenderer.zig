const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const renderTag = @import("renderTag.zig").renderTag;

pub fn render(node: *const MarkdownNode, state: *RendererState, decode: ?bool) void {
    _ = decode;
    renderTag(node, state, "mark", true);
}

pub const highlightRenderer = Renderer{
    .name = "highlight",
    .render = render,
};
