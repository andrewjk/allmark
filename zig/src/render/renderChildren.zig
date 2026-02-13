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
            const trim = !(std.mem.eql(u8, node.type, "code_block") or
                std.mem.eql(u8, node.type, "code_fence") or
                std.mem.eql(u8, node.type, "code_span"));

            for (children, 0..) |child, i| {
                const first = i == 0;
                const last = i == children.len - 1;
                renderNode(child, state, if (trim) first else false, if (trim) last else false, decode);
            }
        }
    }
}
