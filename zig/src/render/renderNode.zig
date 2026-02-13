const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const renderChildren = @import("renderChildren.zig").renderChildren;
const renderTag = @import("renderTag.zig").renderTag;
const renderUtils = @import("renderUtils.zig");

pub fn renderNode(
    node: *const MarkdownNode,
    state: *RendererState,
    first: bool,
    last: bool,
    decode: bool,
) void {
    if (state.renderers.get(node.type)) |renderer| {
        renderer.render(node, state, first, last, decode);
    }
}
