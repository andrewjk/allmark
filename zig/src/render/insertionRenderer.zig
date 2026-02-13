const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const renderTag = @import("renderTag.zig").renderTag;

pub fn render(node: *const MarkdownNode, state: *RendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;
    renderTag.renderTag(node, state, "ins", true);
}

pub const insertionRenderer = Renderer{
    .name = "insertion",
    .render = render,
};
