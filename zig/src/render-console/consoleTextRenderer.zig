const std = @import("std");

const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const ConsoleRendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;

pub const consoleTextRenderer = Renderer{
    .name = "text",
    .render = render,
};

pub fn render(node: *const MarkdownNode, state: *ConsoleRendererState, decode: ?bool) void {
    _ = decode;

    const text = node.content;
    state.output.appendSlice(state.allocator, text) catch unreachable;
}
