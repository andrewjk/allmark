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
    state.output.appendSlice("<table>\n<thead>\n<tr>\n") catch unreachable;

    if (node.children) |children| {
        if (children.len > 0) {
            const header_row = children[0];
            if (header_row.children) |cells| {
                for (cells) |cell| {
                    renderTableCell(cell, state, "th");
                }
            }
        }

        if (children.len > 1) {
            state.output.appendSlice("</tr>\n</thead>\n<tbody>\n") catch unreachable;

            var row_idx: usize = 1;
            while (row_idx < children.len) : (row_idx += 1) {
                const row = children[row_idx];
                state.output.appendSlice("<tr>\n") catch unreachable;
                if (row.children) |cells| {
                    for (cells) |cell| {
                        renderTableCell(cell, state, "td");
                    }
                }
                state.output.appendSlice("</tr>\n") catch unreachable;
            }
            state.output.appendSlice("</tbody>\n") catch unreachable;
        } else {
            state.output.appendSlice("</tr>\n</thead>\n") catch unreachable;
        }
    }

    state.output.appendSlice("</table>") catch unreachable;
    renderUtils.endNewLine(node, state);
}

pub const tableRenderer = Renderer{
    .name = "table",
    .render = render,
};

fn renderTableCell(node: *const MarkdownNode, state: *RendererState, tag: []const u8) void {
    renderUtils.startNewLine(node, state);

    const align_str = if (node.info) |info| blk: {
        const buf = try std.ArrayList(u8).initCapacity(state.output.allocator, info.len + 9);
        try buf.writer().print(" align=\"{s}\"", .{info});
        break :blk buf.toOwnedSlice();
    } else "";

    state.output.writer().print("<{s}{s}>", .{ tag, align_str }) catch unreachable;
    renderUtils.innerNewLine(node, state);
    renderChildren.renderChildren(node, state, true);
    state.output.writer().print("</{s}>", .{tag}) catch unreachable;
    renderUtils.endNewLine(node, state);
}
