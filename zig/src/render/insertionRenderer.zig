const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const renderTag = @import("renderTag.zig").renderTag;

const renderChildren = @import("renderChildren.zig").renderChildren;
const renderUtils = @import("renderUtils.zig");

pub fn render(node: *const MarkdownNode, state: *RendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;
    renderUtils.startNewLine(node, state);
    state.output.appendSlice(state.allocator, "<ins class=\"markdown-insertion\">") catch unreachable;
    renderChildren(node, state, true);
    state.output.appendSlice(state.allocator, "</ins>") catch unreachable;
    renderUtils.endNewLine(node, state);
}

pub const insertionRenderer = Renderer{
    .name = "insertion",
    .render = render,
};
