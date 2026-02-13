const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const renderChildrenFn = @import("renderChildren.zig").renderChildren;
const renderUtils = @import("renderUtils.zig");

pub fn renderTag(
    node: *const MarkdownNode,
    state: *RendererState,
    tag: []const u8,
    decode: bool,
) void {
    renderUtils.startNewLine(node, state);
    const open_tag = std.fmt.allocPrint(state.allocator, "<{s}>", .{tag}) catch unreachable;
    defer state.allocator.free(open_tag);
    state.output.appendSlice(state.allocator, open_tag) catch unreachable;

    if (node.block) {
        if (node.children == null or node.children.?.len == 0) {
            state.output.append(state.allocator, '\n') catch unreachable;
        } else {
            renderUtils.innerNewLine(node, state);
        }
    }
    renderChildrenFn(node, state, decode);

    const close_tag = std.fmt.allocPrint(state.allocator, "</{s}>", .{tag}) catch unreachable;
    defer state.allocator.free(close_tag);
    state.output.appendSlice(state.allocator, close_tag) catch unreachable;
    renderUtils.endNewLine(node, state);
}
