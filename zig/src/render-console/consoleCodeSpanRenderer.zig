const std = @import("std");

const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const ConsoleRendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const ansiGreen = @import("./renderToConsole.zig").ansiGreen;
const ansiReset = @import("./renderToConsole.zig").ansiReset;
const renderChildrenConsole = @import("./renderToConsole.zig").renderChildrenConsole;

pub const consoleCodeSpanRenderer = Renderer{
    .name = "code_span",
    .render = render,
};

pub fn render(node: *const MarkdownNode, state: *ConsoleRendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    state.output.appendSlice(state.allocator, ansiGreen) catch unreachable;
    renderChildrenConsole(node, state, true) catch unreachable;
    state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
}
