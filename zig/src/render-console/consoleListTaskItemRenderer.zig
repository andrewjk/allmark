const std = @import("std");

const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const ansiDim = @import("../render-console/renderToConsole.zig").ansiDim;
const ansiReset = @import("../render-console/renderToConsole.zig").ansiReset;

pub fn render(node: *const MarkdownNode, state: *RendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    const is_checked = if (node.markup.len > 1 and node.markup[1] != ' ')
        true
    else
        false;

    const emoji = if (is_checked) "✓" else " ";
    state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
    state.output.append(state.allocator, '[') catch unreachable;
    state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
    state.output.appendSlice(state.allocator, emoji) catch unreachable;
    state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
    state.output.append(state.allocator, ']') catch unreachable;
    state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
    state.output.append(state.allocator, ' ') catch unreachable;
}

pub const consoleListTaskItemRenderer = Renderer{
    .name = "list_task_item",
    .render = render,
};
