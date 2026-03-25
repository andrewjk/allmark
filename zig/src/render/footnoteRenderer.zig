const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const renderNode = @import("renderNode.zig").renderNode;

pub fn render(node: *const MarkdownNode, state: *RendererState, decode: ?bool) void {
    _ = decode;

    var found_index: ?usize = null;
    if (node.info) |node_info| {
        for (state.footnotes.items, 0..) |fn_ref, i| {
            if (fn_ref.info) |info| {
                if (std.mem.eql(u8, info, node_info)) {
                    found_index = i;
                    break;
                }
            }
        }
    }

    const label: usize = if (found_index) |idx| idx + 1 else blk: {
        state.footnotes.append(state.allocator, node) catch unreachable;
        break :blk state.footnotes.items.len;
    };

    const id = std.fmt.allocPrint(state.allocator, "fnref{d}", .{label}) catch unreachable;
    defer state.allocator.free(id);
    const href = std.fmt.allocPrint(state.allocator, "#fn{d}", .{label}) catch unreachable;
    defer state.allocator.free(href);

    const footnote_tag = std.fmt.allocPrint(state.allocator, "<sup class=\"footnote-ref\"><a href=\"{s}\" id=\"{s}\">{d}</a></sup>", .{ href, id, label }) catch unreachable;
    defer state.allocator.free(footnote_tag);
    state.output.appendSlice(state.allocator, footnote_tag) catch unreachable;
}

pub const footnoteRenderer = Renderer{
    .name = "footnote",
    .render = render,
};
