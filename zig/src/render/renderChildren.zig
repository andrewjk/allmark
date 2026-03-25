const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const renderNode = @import("renderNode.zig").renderNode;

pub fn renderChildren(
    node: *const MarkdownNode,
    state: *RendererState,
    decode: bool,
) void {
    if (node.children) |children| {
        if (children.len > 0) {
            for (children) |child| {
                renderNode(child, state, decode);
            }
        }
    }
}
