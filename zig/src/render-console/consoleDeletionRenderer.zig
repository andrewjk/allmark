const std = @import("std");

const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const ConsoleRendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const ansiDim = @import("./renderToConsole.zig").ansiDim;
const ansiStrikethrough = @import("./renderToConsole.zig").ansiStrikethrough;
const ansiStrikethroughReset = @import("./renderToConsole.zig").ansiStrikethroughReset;
const ansiReset = @import("./renderToConsole.zig").ansiReset;
const renderChildrenConsole = @import("./renderToConsole.zig").renderChildrenConsole;

pub const consoleDeletionRenderer = Renderer{
    .name = "deletion",
    .render = render,
};

pub fn render(node: *const MarkdownNode, state: *ConsoleRendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
    state.output.appendSlice(state.allocator, ansiStrikethrough) catch unreachable;
    renderChildrenConsole(node, state, true) catch unreachable;
    state.output.appendSlice(state.allocator, ansiStrikethroughReset) catch unreachable;
    state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
}

pub const deletionRenderer = struct {
    name: []const u8 = "deletion",
    render: consoleDeletionRenderer,
};
