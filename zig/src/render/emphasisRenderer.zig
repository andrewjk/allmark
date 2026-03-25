const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const renderTagFn = @import("renderTag.zig").renderTag;

pub fn render(node: *const MarkdownNode, state: *RendererState, decode: ?bool) void {
    _ = decode;
    renderTagFn(node, state, "em", true);
}

pub const emphasisRenderer = Renderer{
    .name = "emphasis",
    .render = render,
};
