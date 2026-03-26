const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const renderChildrenFn = @import("renderChildren.zig").renderChildren;
const renderUtils = @import("renderUtils.zig");

pub fn render(node: *const MarkdownNode, state: *RendererState, decode: ?bool) void {
    _ = decode;

    var level: usize = 0;
    if (std.mem.indexOfScalar(u8, node.markup, '=')) |_| {
        level = 1;
    } else if (std.mem.indexOfScalar(u8, node.markup, '-')) |_| {
        level = 2;
    }

    renderUtils.startNewLine(node, state);
    const open_tag = std.fmt.allocPrint(state.allocator, "<h{d}>", .{level}) catch unreachable;
    defer state.allocator.free(open_tag);
    state.output.appendSlice(state.allocator, open_tag) catch unreachable;
    renderUtils.innerNewLine(node, state);
    renderChildrenFn(node, state, true);
    const close_tag = std.fmt.allocPrint(state.allocator, "</h{d}>", .{level}) catch unreachable;
    defer state.allocator.free(close_tag);
    state.output.appendSlice(state.allocator, close_tag) catch unreachable;
    renderUtils.endNewLine(node, state);
}

pub const headingUnderlineRenderer = Renderer{
    .name = "heading_underline",
    .render = render,
};
