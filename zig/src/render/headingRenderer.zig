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

    var level: usize = 0;
    if (node.markup.len > 0 and node.markup[0] == '#') {
        level = node.markup.len;
    } else if (std.mem.indexOfScalar(u8, node.markup, '=')) |_| {
        level = 1;
    } else if (std.mem.indexOfScalar(u8, node.markup, '-')) |_| {
        level = 2;
    }

    renderUtils.startNewLine(node, state);
    state.output.writer().print("<h{d}>", .{level}) catch unreachable;
    renderUtils.innerNewLine(node, state);
    renderChildren.renderChildren(node, state, true);
    state.output.writer().print("</h{d}>", .{level}) catch unreachable;
    renderUtils.endNewLine(node, state);
}

pub const headingRenderer = Renderer{
    .name = "heading",
    .render = render,
};
