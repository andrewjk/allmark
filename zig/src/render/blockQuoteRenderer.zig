const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const renderTagFn = @import("renderTag.zig").renderTag;

pub fn render(node: *const MarkdownNode, state: *RendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;
    renderTagFn(node, state, "blockquote", true);
}

pub const blockQuoteRenderer = Renderer{
    .name = "block_quote",
    .render = render,
};
