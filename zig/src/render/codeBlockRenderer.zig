const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const renderChildren = @import("renderChildren.zig").renderChildren;
const renderUtils = @import("renderUtils.zig");

pub fn render(node: *const MarkdownNode, state: *RendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    renderUtils.startNewLine(node, state);

    const lang = if (node.info) |info| info_val: {
        const trimmed = std.mem.trim(u8, info, &std.ascii.whitespace);
        const space_idx = std.mem.indexOfScalar(u8, trimmed, ' ') orelse trimmed.len;
        const lang_str = trimmed[0..space_idx];
        break :info_val state.output.allocator.dupe(u8, lang_str) catch unreachable;
    } else "";

    state.output.writer().print("<pre><code{s}>", .{lang}) catch unreachable;
    renderChildren.renderChildren(node, state, false);
    state.output.appendSlice("</code></pre>") catch unreachable;
    renderUtils.endNewLine(node, state);
}

pub const codeBlockRenderer = Renderer{
    .name = "code_block",
    .render = render,
};
