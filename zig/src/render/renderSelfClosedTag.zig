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
    state.output.writer().print("<{s} />", .{tag}) catch unreachable;
    renderUtils.endNewLine(node, state);
}
