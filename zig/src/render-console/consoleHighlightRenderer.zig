const std = @import("std");

const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const ConsoleRendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const ansiBlack = @import("./renderToConsole.zig").ansiBlack;
const ansiYellowBg = @import("./renderToConsole.zig").ansiYellowBg;
const ansiReset = @import("./renderToConsole.zig").ansiReset;
const renderChildrenConsole = @import("./renderToConsole.zig").renderChildrenConsole;

pub const consoleHighlightRenderer = Renderer{
    .name = "highlight",
    .render = render,
};

pub fn render(node: *const MarkdownNode, state: *ConsoleRendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    const style = ansiYellowBg ++ ansiBlack;

    state.output.appendSlice(state.allocator, style) catch unreachable;
    renderChildrenConsole(node, state, true) catch unreachable;
    state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
}
