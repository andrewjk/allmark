const std = @import("std");

const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const ConsoleRendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const ansiBlue = @import("./renderToConsole.zig").ansiBlue;
const ansiUnderline = @import("./renderToConsole.zig").ansiUnderline;
const ansiDim = @import("./renderToConsole.zig").ansiDim;
const ansiReset = @import("./renderToConsole.zig").ansiReset;
const renderChildrenConsole = @import("./renderToConsole.zig").renderChildrenConsole;

pub const consoleLinkRenderer = Renderer{
    .name = "link",
    .render = render,
};

pub fn render(node: *const MarkdownNode, state: *ConsoleRendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    state.output.appendSlice(state.allocator, ansiUnderline ++ ansiBlue) catch unreachable;
    renderChildrenConsole(node, state, true) catch unreachable;
    state.output.appendSlice(state.allocator, ansiReset) catch unreachable;

    if (node.info) |info| {
        state.output.append(state.allocator, ' ') catch unreachable;
        state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
        state.output.append(state.allocator, '(') catch unreachable;
        state.output.appendSlice(state.allocator, info) catch unreachable;
        state.output.append(state.allocator, ')') catch unreachable;
        state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
    }
}
