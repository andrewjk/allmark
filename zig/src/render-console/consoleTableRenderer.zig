const std = @import("std");

const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const ansiDim = @import("console.zig").ansiDim;
const ansiReset = @import("console.zig").ansiReset;

pub const consoleTableRenderer = Renderer{
    .name = "table",
    .render = render,
};

pub fn render(node: *const MarkdownNode, state: *RendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    if (state.output.items.len > 0 and state.output.items[state.output.items.len - 1] != '\n') {
        state.output.append(state.allocator, '\n') catch unreachable;
    }

    const children = node.children orelse return;

    if (children.len == 0) return;

    const headerRow = children[0];
    const dataRows = children[1..];

    const headerCells = headerRow.children orelse &.{};

    var maxColumns: usize = headerCells.len;
    for (dataRows) |row| {
        if (row.children) |rowCells| {
            if (rowCells.len > maxColumns) {
                maxColumns = rowCells.len;
            }
        }
    }

    var columnWidths = state.allocator.alloc(usize, maxColumns) catch unreachable;
    defer state.allocator.free(columnWidths);
    @memset(columnWidths, 0);

    var cellTexts = std.ArrayList([]const []const u8).initCapacity(state.allocator, dataRows.len + 1) catch unreachable;
    defer {
        for (cellTexts.items) |row| {
            for (row) |text| {
                state.allocator.free(text);
            }
            state.allocator.free(row);
        }
        cellTexts.deinit(state.allocator);
    }

    var headerTexts = state.allocator.alloc([]const u8, maxColumns) catch unreachable;
    @memset(headerTexts, "");
    cellTexts.append(state.allocator, headerTexts) catch unreachable;

    {
        var i: usize = 0;
        while (i < headerCells.len) : (i += 1) {
            const text = getTextFromNode(headerCells[i], state.allocator);
            headerTexts[i] = text;
            columnWidths[i] = @max(columnWidths[i], text.len + 2);
        }
    }

    {
        var r: usize = 0;
        while (r < dataRows.len) : (r += 1) {
            const row = dataRows[r];
            const rowCells = row.children orelse &.{};
            const rowTexts = state.allocator.alloc([]const u8, maxColumns) catch unreachable;
            @memset(rowTexts, "");
            cellTexts.append(state.allocator, rowTexts) catch unreachable;

            var c: usize = 0;
            while (c < rowCells.len) : (c += 1) {
                const text = getTextFromNode(rowCells[c], state.allocator);
                rowTexts[c] = text;
                columnWidths[c] = @max(columnWidths[c], text.len + 2);
            }
        }
    }

    const makeLine = struct {
        fn func(left: []const u8, mid: []const u8, right: []const u8, sep: []const u8, s: *RendererState, widths: []const usize) !void {
            s.output.appendSlice(s.allocator, ansiDim) catch unreachable;
            s.output.appendSlice(s.allocator, left) catch unreachable;
            for (0..widths.len) |i| {
                var dashes = std.ArrayList(u8).initCapacity(s.allocator, widths[i]) catch unreachable;
                defer dashes.deinit(s.allocator);
                for (0..widths[i]) |_| {
                    dashes.appendSlice(s.allocator, "─") catch unreachable;
                }
                const dashStr = dashes.items;
                s.output.appendSlice(s.allocator, dashStr) catch unreachable;
                if (i < widths.len - 1) {
                    s.output.appendSlice(s.allocator, if (i == 0) mid else sep) catch unreachable;
                }
            }
            s.output.appendSlice(s.allocator, right) catch unreachable;
            s.output.appendSlice(s.allocator, ansiReset) catch unreachable;
            s.output.append(s.allocator, '\n') catch unreachable;
        }
    }.func;

    try makeLine("┌", "┬", "┐", "┼", state, columnWidths);

    if (headerCells.len > 0) {
        state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
        state.output.appendSlice(state.allocator, "│") catch unreachable;
        state.output.appendSlice(state.allocator, ansiReset) catch unreachable;

        for (0..headerCells.len) |i| {
            const text = cellTexts.items[0][i];
            state.output.append(state.allocator, ' ') catch unreachable;
            state.output.appendSlice(state.allocator, text) catch unreachable;
            const padding = columnWidths[i] - text.len - 1;
            for (0..padding) |_| {
                state.output.append(state.allocator, ' ') catch unreachable;
            }
            state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
            state.output.appendSlice(state.allocator, "│") catch unreachable;
            state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
        }
        state.output.append(state.allocator, '\n') catch unreachable;
    }

    try makeLine("├", "┼", "┤", "┼", state, columnWidths);

    {
        var r: usize = 0;
        while (r < dataRows.len) : (r += 1) {
            state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
            state.output.appendSlice(state.allocator, "│") catch unreachable;
            state.output.appendSlice(state.allocator, ansiReset) catch unreachable;

            const rowTexts = cellTexts.items[r + 1];
            for (0..columnWidths.len) |c| {
                const text = if (c < rowTexts.len) rowTexts[c] else "";
                state.output.append(state.allocator, ' ') catch unreachable;
                state.output.appendSlice(state.allocator, text) catch unreachable;
                const padding = columnWidths[c] - text.len - 1;
                for (0..padding) |_| {
                    state.output.append(state.allocator, ' ') catch unreachable;
                }
                state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
                state.output.appendSlice(state.allocator, "│") catch unreachable;
                state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
            }
            state.output.append(state.allocator, '\n') catch unreachable;
        }
    }

    try makeLine("└", "┴", "┘", "┴", state, columnWidths);
}

fn getTextFromNode(node: *const MarkdownNode, allocator: std.mem.Allocator) []const u8 {
    if (std.mem.eql(u8, node.type, "text")) {
        return node.markup;
    }
    if (node.content.len > 0) {
        return allocator.dupe(u8, node.content) catch unreachable;
    }
    if (node.children) |children| {
        var buffer = std.ArrayList(u8).initCapacity(allocator, 64) catch unreachable;
        for (children) |child| {
            const text = getTextFromNode(child, allocator);
            buffer.appendSlice(allocator, text) catch unreachable;
        }
        return buffer.toOwnedSlice(allocator) catch unreachable;
    }
    return "";
}
