const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const renderTag = @import("renderTag.zig").renderTag;

pub fn render(node: *const MarkdownNode, state: *RendererState, decode: ?bool) void {
    _ = decode;
    renderTag(node, state, "td", true);
}

pub const tableCellRenderer = Renderer{
    .name = "table_cell",
    .render = render,
};
