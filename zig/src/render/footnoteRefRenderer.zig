const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;

pub fn render(node: *const MarkdownNode, state: *RendererState, decode: ?bool) void {
    _ = decode;
    if (node.info) |info| {
        state.footnoteRefs.put(info, node) catch unreachable;
    }
}

pub const footnoteRefRenderer = Renderer{
    .name = "footnote_ref",
    .render = render,
};
