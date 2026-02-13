const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const renderUtils = @import("renderUtils.zig");

pub fn renderSelfClosedTag(
    node: *const MarkdownNode,
    state: *RendererState,
    tag: []const u8,
) void {
    renderUtils.startNewLine(node, state);
    const html = std.fmt.allocPrint(state.allocator, "<{s} />", .{tag}) catch unreachable;
    defer state.allocator.free(html);
    state.output.appendSlice(state.allocator, html) catch unreachable;
    renderUtils.endNewLine(node, state);
}
