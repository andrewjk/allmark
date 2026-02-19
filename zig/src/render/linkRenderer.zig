const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const renderChildrenFn = @import("renderChildren.zig").renderChildren;
const renderUtils = @import("renderUtils.zig");
const escapeHtml = @import("../utils/escapeHtml.zig").escapeHtml;

pub fn render(node: *const MarkdownNode, state: *RendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    renderUtils.startNewLine(node, state);
    const title = if (node.title) |t| blk: {
        const title_str = std.fmt.allocPrint(state.allocator, " title=\"{s}\"", .{t}) catch unreachable;
        break :blk title_str;
    } else "";
    defer if (title.len > 0) state.allocator.free(title);

    const href_raw = node.info orelse "";
    const href = escapeHtml(state.allocator, href_raw) catch unreachable;
    defer state.allocator.free(href);
    const open_tag = std.fmt.allocPrint(state.allocator, "<a href=\"{s}\"{s}>", .{ href, title }) catch unreachable;
    defer state.allocator.free(open_tag);
    state.output.appendSlice(state.allocator, open_tag) catch unreachable;
    renderChildrenFn(node, state, true);
    state.output.appendSlice(state.allocator, "</a>") catch unreachable;
    renderUtils.endNewLine(node, state);
}

pub const linkRenderer = Renderer{
    .name = "link",
    .render = render,
};
