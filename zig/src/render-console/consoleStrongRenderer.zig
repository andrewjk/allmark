const std = @import("std");

const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const ConsoleRendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const ansiBold = @import("console.zig").ansiBold;
const ansiYellow = @import("console.zig").ansiYellow;
const ansiReset = @import("console.zig").ansiReset;
const renderChildrenConsole = @import("console.zig").renderChildrenConsole;

pub const consoleStrongRenderer = Renderer{
    .name = "strong",
    .render = render,
};

pub fn render(node: *const MarkdownNode, state: *ConsoleRendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    const style = ansiBold ++ ansiYellow;

    state.output.appendSlice(state.allocator, style) catch unreachable;
    renderChildrenConsole(node, state, true) catch unreachable;
    state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
}
