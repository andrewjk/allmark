const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const renderChildren = @import("renderChildren.zig").renderChildren;
const renderUtils = @import("renderUtils.zig");

pub fn render(node: *const MarkdownNode, state: *RendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    renderUtils.startNewLine(node, state);
    state.output.writer().print("<div class=\"markdown-alert markdown-alert-{s}\n<p class=\"markdown-alert-title\">{s}</p>", .{ node.markup, node.markup }) catch unreachable;
    renderChildren.renderChildren(node, state, true);
    state.output.appendSlice("</div>") catch unreachable;
    renderUtils.endNewLine(node, state);
}

pub const alertRenderer = Renderer{
    .name = "alert",
    .render = render,
};
