const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const render = @import("headingRenderer.zig").render;

pub const headingUnderlineRenderer = Renderer{
    .name = "html_underline",
    .render = render.render,
};
