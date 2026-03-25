const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const renderSelfClosedTag = @import("renderSelfClosedTag.zig").renderSelfClosedTag;

pub fn render(node: *const MarkdownNode, state: *RendererState, decode: ?bool) void {
    _ = decode;
    renderSelfClosedTag(node, state, "hr");
}

pub const thematicBreakRenderer = Renderer{
    .name = "thematic_break",
    .render = render,
};
