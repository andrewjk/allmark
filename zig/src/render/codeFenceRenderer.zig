const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const render = @import("codeBlockRenderer.zig").render;

pub const codeFenceRenderer = Renderer{
    .name = "code_fence",
    .render = render.render,
};
