const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const renderTag = @import("renderTag.zig").renderTag;

const renderChildren = @import("renderChildren.zig").renderChildren;
const renderUtils = @import("renderUtils.zig");

pub fn render(node: *const MarkdownNode, state: *RendererState, decode: ?bool) void {
    _ = decode;
    renderUtils.startNewLine(node, state);
    state.output.appendSlice(state.allocator, "<span class=\"markdown-comment\">") catch unreachable;
    renderChildren(node, state, true);
    state.output.appendSlice(state.allocator, "</span>") catch unreachable;
    renderUtils.endNewLine(node, state);
}

pub const commentRenderer = Renderer{
    .name = "comment",
    .render = render,
};
