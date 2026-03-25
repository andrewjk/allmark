const std = @import("std");

const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const ConsoleRendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;

pub const consoleHtmlRenderer = Renderer{
    .name = "html",
    .render = render,
};

fn render(node: *const MarkdownNode, state: *ConsoleRendererState, decode: ?bool) void {
    _ = decode;

    state.output.appendSlice(state.allocator, node.content) catch unreachable;
}
