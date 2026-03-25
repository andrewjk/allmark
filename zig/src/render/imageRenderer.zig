const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const renderUtils = @import("renderUtils.zig");

pub fn render(node: *const MarkdownNode, state: *RendererState, decode: ?bool) void {
    _ = decode;

    renderUtils.startNewLine(node, state);
    const alt = getChildText(state.allocator, node);
    defer state.allocator.free(alt);

    const title = if (node.title) |t| blk: {
        const title_str = std.fmt.allocPrint(state.allocator, " title=\"{s}\"", .{t}) catch unreachable;
        break :blk title_str;
    } else "";
    defer if (title.len > 0) state.allocator.free(title);

    const src = node.info orelse "";
    const img_tag = std.fmt.allocPrint(state.allocator, "<img src=\"{s}\" alt=\"{s}\"{s} />", .{ src, alt, title }) catch unreachable;
    defer state.allocator.free(img_tag);
    state.output.appendSlice(state.allocator, img_tag) catch unreachable;
    renderUtils.endNewLine(node, state);
}

pub const imageRenderer = Renderer{
    .name = "image",
    .render = render,
};

fn getChildText(allocator: std.mem.Allocator, node: *const MarkdownNode) []const u8 {
    var buffer = std.ArrayList(u8).empty;
    errdefer buffer.deinit(allocator);

    if (node.children) |children| {
        for (children) |child| {
            if (std.mem.eql(u8, child.type, "text")) {
                buffer.appendSlice(allocator, child.markup) catch unreachable;
            } else {
                const text = getChildText(allocator, child);
                defer allocator.free(text);
                buffer.appendSlice(allocator, text) catch unreachable;
            }
        }
    }

    return buffer.toOwnedSlice(allocator) catch unreachable;
}
