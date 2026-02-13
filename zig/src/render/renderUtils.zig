const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;

pub fn startNewLine(node: *const MarkdownNode, state: *RendererState) void {
    if (state.output.items.len > 0 and node.block) {
        const last = state.output.items[state.output.items.len - 1];
        if (last != '\n') {
            state.output.append(state.allocator, '\n') catch unreachable;
        }
    }
}

pub fn innerNewLine(node: *const MarkdownNode, state: *RendererState) void {
    if (!node.block) return;
    if (node.children) |children| {
        if (children.len > 0 and children[0].block) {
            state.output.append(state.allocator, '\n') catch unreachable;
        }
    }
}

pub fn endNewLine(node: *const MarkdownNode, state: *RendererState) void {
    if (node.block) {
        state.output.append(state.allocator, '\n') catch unreachable;
    }
}
