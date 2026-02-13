const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const renderNode = @import("renderNode.zig").renderNode;

pub fn render(node: *const MarkdownNode, state: *RendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    var found = false;
    for (state.footnotes.items) |fn_ref| {
        if (std.mem.eql(u8, fn_ref.info, node.info)) {
            found = true;
            break;
        }
    }

    if (!found) {
        try state.footnotes.append(node);
    }

    const label = state.footnotes.items.len;
    const id = try std.fmt.allocPrint(state.output.allocator, "fnref{d}", .{label});
    defer state.output.allocator.free(id);
    const href = try std.fmt.allocPrint(state.output.allocator, "#fn{d}", .{label});
    defer state.output.allocator.free(href);

    state.output.writer().print("<sup class=\"footnote-ref\"><a href=\"{s}\" id=\"{s}\">{d}</a></sup>", .{ href, id, label }) catch unreachable;
}

pub const footnoteRenderer = Renderer{
    .name = "footnote",
    .render = render,
};
