const std = @import("std");

const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const ConsoleRendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;

pub const consoleCommentRenderer = Renderer{
    .name = "comment",
    .render = render,
};

pub fn render(node: *const MarkdownNode, state: *ConsoleRendererState, decode: ?bool) void {
    _ = decode;

    state.output.appendSlice(state.allocator, "<!--") catch unreachable;
    state.output.appendSlice(state.allocator, node.content) catch unreachable;
    state.output.appendSlice(state.allocator, "-->") catch unreachable;
}

pub const commentRenderer = struct {
    name: []const u8 = "comment",
    render: consoleCommentRenderer,
};
