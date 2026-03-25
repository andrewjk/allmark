const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const renderChildrenFn = @import("renderChildren.zig").renderChildren;

pub const footnoteListRenderer = Renderer{
    .name = "footnote_list",
    .render = render,
};

pub fn renderFootnoteList(state: *RendererState) void {
    render(null, state, null, null, null);
}

pub fn render(_node: ?*const MarkdownNode, state: *RendererState, decode: ?bool) void {
    _ = _node;
    _ = decode;
    state.output.appendSlice(state.allocator, "<section class=\"footnotes\">\n<ol>\n") catch unreachable;

    var number: usize = 1;
    for (state.footnotes.items) |node| {
        const label = number;
        number += 1;

        const id = std.fmt.allocPrint(state.allocator, "fn{d}", .{label}) catch unreachable;
        defer state.allocator.free(id);
        const href = std.fmt.allocPrint(state.allocator, "#fnref{d}", .{label}) catch unreachable;
        defer state.allocator.free(href);

        const open_tag = std.fmt.allocPrint(state.allocator, "<li id=\"{s}\">", .{id}) catch unreachable;
        defer state.allocator.free(open_tag);
        state.output.appendSlice(state.allocator, open_tag) catch unreachable;

        renderChildrenFn(node, state, true);

        if (state.output.items.len >= 5 and std.mem.eql(u8, state.output.items[state.output.items.len - 5 ..], "</p>\n")) {
            const new_len = state.output.items.len - 5;
            state.output.shrinkRetainingCapacity(new_len);
        }

        const close_tag = std.fmt.allocPrint(state.allocator, " <a href=\"{s}\" class=\"footnote-backref\">↩</a></p>\n</li>\n", .{href}) catch unreachable;
        defer state.allocator.free(close_tag);
        state.output.appendSlice(state.allocator, close_tag) catch unreachable;
    }

    state.output.appendSlice(state.allocator, "</ol>\n</section>") catch unreachable;
}
