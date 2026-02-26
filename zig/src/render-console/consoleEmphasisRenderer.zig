const std = @import("std");

const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const ConsoleRendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const ansiItalic = @import("./renderToConsole.zig").ansiItalic;
const ansiYellow = @import("./renderToConsole.zig").ansiYellow;
const ansiReset = @import("./renderToConsole.zig").ansiReset;
const renderChildrenConsole = @import("./renderToConsole.zig").renderChildrenConsole;

pub const consoleEmphasisRenderer = Renderer{
    .name = "emphasis",
    .render = render,
};

pub fn render(node: *const MarkdownNode, state: *ConsoleRendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    const style = ansiItalic ++ ansiYellow;

    state.output.appendSlice(state.allocator, style) catch unreachable;
    renderChildrenConsole(node, state, true) catch unreachable;
    state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
}
