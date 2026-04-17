const std = @import("std");

const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RuleSet = @import("../types/RuleSet.zig").RuleSet;
const ConsoleRendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const renderChildrenConsole = @import("console.zig").renderChildrenConsole;

pub const consoleParagraphRenderer = Renderer{
    .name = "paragraph",
    .render = render,
};

pub fn render(node: *const MarkdownNode, state: *ConsoleRendererState, decode: ?bool) void {
    _ = decode;

    renderChildrenConsole(node, state, true) catch unreachable;
    state.output.appendSlice(state.allocator, "\n\n") catch unreachable;
}
