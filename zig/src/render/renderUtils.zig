const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;

pub fn startNewLine(node: *const MarkdownNode, state: *RendererState) void {
    if (state.output.items.len > 0 and node.block and state.output.getLastOrNull()) |last| {
        if (last != '\n') {
            state.output.append('\n') catch unreachable;
        }
    }
}

pub fn innerNewLine(node: *const MarkdownNode, state: *RendererState) void {
    if (node.block and node.children) |children| {
        if (children.len > 0 and children[0].block) {
            state.output.append('\n') catch unreachable;
        }
    }
}

pub fn endNewLine(node: *const MarkdownNode, state: *RendererState) void {
    _ = node;
    state.output.append('\n') catch unreachable;
}
