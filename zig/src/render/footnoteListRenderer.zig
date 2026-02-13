const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const renderChildren = @import("renderChildren.zig").renderChildren;

pub fn renderFootnoteList(state: *RendererState) void {
    state.output.appendSlice("<section class=\"footnotes\">\n<ol>\n") catch unreachable;

    var number: usize = 1;
    for (state.footnotes.items) |node| {
        const label = number;
        number += 1;

        const id = try std.fmt.allocPrint(state.output.allocator, "fn{d}", .{label});
        defer state.output.allocator.free(id);
        const href = try std.fmt.allocPrint(state.output.allocator, "#fnref{d}", .{label});
        defer state.output.allocator.free(href);

        state.output.writer().print("<li id=\"{s}\">", .{id}) catch unreachable;
        renderChildren.renderChildren(node, state, true);

        if (state.output.items.len >= 5 and std.mem.eql(u8, state.output.items[state.output.items.len - 5 ..], "</p>\n")) {
            state.output.shrinkRetainingCapacity(state.output.items.len - 5);
        }

        state.output.writer().print(" <a href=\"{s}\" class=\"footnote-backref\">↩</a></p>\n</li>\n", .{href}) catch unreachable;
    }

    state.output.appendSlice("</ol>\n</section>") catch unreachable;
}
