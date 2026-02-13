const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const renderChildren = @import("renderChildren.zig").renderChildren;
const renderUtils = @import("renderUtils.zig");

pub fn renderTag(
    node: *const MarkdownNode,
    state: *RendererState,
    tag: []const u8,
    decode: bool,
) void {
    renderUtils.startNewLine(node, state);
    state.output.writer().print("<{s}>", .{tag}) catch unreachable;
    if (node.block) {
        if (node.children == null or node.children.?.len == 0) {
            state.output.append('\n') catch unreachable;
        } else {
            renderUtils.innerNewLine(node, state);
        }
    }
    renderChildren.renderChildren(node, state, decode);
    state.output.writer().print("</{s}>", .{tag}) catch unreachable;
    renderUtils.endNewLine(node, state);
}
