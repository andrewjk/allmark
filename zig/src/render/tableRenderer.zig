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
    state.output.appendSlice(state.allocator, "<table>\n<thead>\n<tr>\n") catch unreachable;

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
            state.output.appendSlice(state.allocator, "</tr>\n</thead>\n<tbody>\n") catch unreachable;

            var row_idx: usize = 1;
            while (row_idx < children.len) : (row_idx += 1) {
                const row = children[row_idx];
                state.output.appendSlice(state.allocator, "<tr>\n") catch unreachable;
                if (row.children) |cells| {
                    for (cells) |cell| {
                        renderTableCell(cell, state, "td");
                    }
                }
                state.output.appendSlice(state.allocator, "</tr>\n") catch unreachable;
            }
            state.output.appendSlice(state.allocator, "</tbody>\n") catch unreachable;
        } else {
            state.output.appendSlice(state.allocator, "</tr>\n</thead>\n") catch unreachable;
        }
    }

    state.output.appendSlice(state.allocator, "</table>") catch unreachable;
    renderUtils.endNewLine(node, state);
}

pub const tableRenderer = Renderer{
    .name = "table",
    .render = render,
};

fn renderTableCell(node: *const MarkdownNode, state: *RendererState, tag: []const u8) void {
    renderUtils.startNewLine(node, state);

    const align_str = if (node.info) |info| blk: {
        if (info.len > 0) {
            const buf = std.fmt.allocPrint(state.allocator, " align=\"{s}\"", .{info}) catch unreachable;
            break :blk buf;
        }
        break :blk "";
    } else "";
    defer if (node.info != null and node.info.?.len > 0) state.allocator.free(align_str);

    const open_tag = std.fmt.allocPrint(state.allocator, "<{s}{s}>", .{ tag, align_str }) catch unreachable;
    defer state.allocator.free(open_tag);
    state.output.appendSlice(state.allocator, open_tag) catch unreachable;

    renderUtils.innerNewLine(node, state);
    renderChildren(node, state, true);

    const close_tag = std.fmt.allocPrint(state.allocator, "</{s}>", .{tag}) catch unreachable;
    defer state.allocator.free(close_tag);
    state.output.appendSlice(state.allocator, close_tag) catch unreachable;

    renderUtils.endNewLine(node, state);
}
