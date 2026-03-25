const std = @import("std");

const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const ConsoleRendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const ansiItalic = @import("console.zig").ansiItalic;
const ansiYellow = @import("console.zig").ansiYellow;
const ansiReset = @import("console.zig").ansiReset;
const renderChildrenConsole = @import("console.zig").renderChildrenConsole;

pub const consoleEmphasisRenderer = Renderer{
    .name = "emphasis",
    .render = render,
};

pub fn render(node: *const MarkdownNode, state: *ConsoleRendererState, decode: ?bool) void {
    _ = decode;

    const style = ansiItalic ++ ansiYellow;

    state.output.appendSlice(state.allocator, style) catch unreachable;
    renderChildrenConsole(node, state, true) catch unreachable;
    state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
}
