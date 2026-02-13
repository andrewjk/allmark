const std = @import("std");
const isSpace = @import("../utils/isSpace.zig").isSpace;

const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;

pub fn render(node: *const MarkdownNode, state: *RendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    const checked = if (node.markup.len > 1 and !isSpace(node.markup[1]))
        " checked=\"\""
    else
        "";

    state.output.writer().print("<input type=\"checkbox\"{s} disabled=\"\" /> ", .{checked}) catch unreachable;
}

pub const listTaskItemRenderer = Renderer{
    .name = "list_task_item",
    .render = render,
};
