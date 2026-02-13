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

    var title = std.ArrayList(u8).initCapacity(state.allocator, node.markup.len) catch unreachable;
    defer title.deinit(state.allocator);

    if (node.markup.len > 0) {
        title.append(state.allocator, std.ascii.toUpper(node.markup[0])) catch unreachable;
        if (node.markup.len > 1) {
            title.appendSlice(state.allocator, node.markup[1..]) catch unreachable;
        }
    }

    const open_div = std.fmt.allocPrint(state.allocator, "<div class=\"markdown-alert markdown-alert-{s}\">\n<p class=\"markdown-alert-title\">{s}</p>", .{ node.markup, title.items }) catch unreachable;
    defer state.allocator.free(open_div);
    state.output.appendSlice(state.allocator, open_div) catch unreachable;
    renderChildren(node, state, true);
    state.output.appendSlice(state.allocator, "</div>") catch unreachable;
    renderUtils.endNewLine(node, state);
}

pub const alertRenderer = Renderer{
    .name = "alert",
    .render = render,
};
