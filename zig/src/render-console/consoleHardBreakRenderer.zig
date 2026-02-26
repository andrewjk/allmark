const std = @import("std");

const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const ConsoleRendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;

pub const consoleHardBreakRenderer = Renderer{
    .name = "hard_break",
    .render = render,
};

pub fn render(node: *const MarkdownNode, state: *ConsoleRendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = node;
    _ = first;
    _ = last;
    _ = decode;

    state.output.append(state.allocator, '\n') catch unreachable;
}
