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

pub fn render(node: *const MarkdownNode, state: *RendererState, decode: ?bool) void {
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

    var columnAlignments = state.allocator.alloc([]const u8, maxColumns) catch unreachable;
    defer state.allocator.free(columnAlignments);
    @memset(columnAlignments, "");

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
            columnAlignments[i] = headerCells[i].info orelse "";
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

    var targetWidths = columnWidths;
    var fitWidthsOwned: ?[]usize = null;
    var twOwned: ?[]usize = null;
    defer {
        if (fitWidthsOwned) |fw| state.allocator.free(fw);
        if (twOwned) |tw| state.allocator.free(tw);
    }

    if (state.line_width) |lineWidth| {
        const totalWidth: usize = 1 + sum(columnWidths) + maxColumns;
        if (totalWidth > lineWidth) {
            const fitWidths = fitColumns(state.allocator, columnWidths, lineWidth, maxColumns, cellTexts.items);
            fitWidthsOwned = fitWidths;

            var tw = state.allocator.alloc(usize, maxColumns) catch unreachable;
            twOwned = tw;
            @memset(tw, 2);

            for (0..cellTexts.items.len) |r| {
                for (0..maxColumns) |c| {
                    const text = if (c < cellTexts.items[r].len) cellTexts.items[r][c] else "";
                    const lines = wrapText(state.allocator, text, fitWidths[c] - 2);
                    defer freeWrappedLines(state.allocator, lines);
                    for (lines) |line| {
                        tw[c] = @max(tw[c], line.len + 2);
                    }
                }
            }
            targetWidths = tw;
        }
    }

    const makeLine = struct {
        fn func(left: []const u8, mid: []const u8, right: []const u8, s: *RendererState, widths: []const usize) !void {
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
                    s.output.appendSlice(s.allocator, mid) catch unreachable;
                }
            }
            s.output.appendSlice(s.allocator, right) catch unreachable;
            s.output.appendSlice(s.allocator, ansiReset) catch unreachable;
            s.output.append(s.allocator, '\n') catch unreachable;
        }
    }.func;

    try makeLine("┌", "┬", "┐", state, targetWidths);

    if (headerCells.len > 0) {
        renderRow(state, cellTexts.items, 0, targetWidths, maxColumns, columnAlignments);
    }

    try makeLine("├", "┼", "┤", state, targetWidths);

    {
        var r: usize = 0;
        while (r < dataRows.len) : (r += 1) {
            renderRow(state, cellTexts.items, r + 1, targetWidths, maxColumns, columnAlignments);
        }
    }

    try makeLine("└", "┴", "┘", state, targetWidths);
}

fn renderRow(
    state: *RendererState,
    cellTexts: []const []const []const u8,
    rowIdx: usize,
    targetWidths: []const usize,
    maxColumns: usize,
    columnAlignments: []const []const u8,
) void {
    const allocator = state.allocator;

    var cellLines = allocator.alloc([]const []const u8, maxColumns) catch unreachable;
    defer allocator.free(cellLines);

    var maxLines: usize = 1;

    for (0..maxColumns) |c| {
        const text = if (rowIdx < cellTexts.len and c < cellTexts[rowIdx].len) cellTexts[rowIdx][c] else "";
        const lines = wrapText(allocator, text, targetWidths[c] - 2);
        cellLines[c] = lines;
        maxLines = @max(maxLines, lines.len);
    }
    defer {
        for (cellLines) |lines| {
            freeWrappedLines(allocator, lines);
        }
    }

    var line: usize = 0;
    while (line < maxLines) : (line += 1) {
        state.output.appendSlice(allocator, ansiDim) catch unreachable;
        state.output.appendSlice(allocator, "│") catch unreachable;
        state.output.appendSlice(allocator, ansiReset) catch unreachable;

        for (0..maxColumns) |c| {
            const text = if (line < cellLines[c].len) cellLines[c][line] else "";
            const alignment = columnAlignments[c];
            renderPaddedCell(state, text, targetWidths[c], alignment) catch unreachable;
        }
        state.output.append(allocator, '\n') catch unreachable;
    }
}

fn fitColumns(
    allocator: std.mem.Allocator,
    columnWidths: []const usize,
    lineWidth: usize,
    numColumns: usize,
    cellTexts: []const []const []const u8,
) []usize {
    const available = lineWidth - 1 - numColumns;
    var targetWidths = allocator.dupe(usize, columnWidths) catch unreachable;

    const minWidths = allocator.alloc(usize, columnWidths.len) catch unreachable;
    defer allocator.free(minWidths);

    for (0..columnWidths.len) |colIdx| {
        var maxWordLen: usize = 1;
        for (cellTexts) |row| {
            const text = if (colIdx < row.len) row[colIdx] else "";
            var start: usize = 0;
            for (text, 0..) |ch, i| {
                if (ch == ' ') {
                    const wordLen = i - start;
                    maxWordLen = @max(maxWordLen, wordLen);
                    start = i + 1;
                }
            }
            const wordLen = text.len - start;
            maxWordLen = @max(maxWordLen, wordLen);
        }
        minWidths[colIdx] = maxWordLen + 2;
    }

    while (sum(targetWidths) > available) {
        var maxIdx: usize = 0;
        for (1..targetWidths.len) |i| {
            if (targetWidths[i] > targetWidths[maxIdx]) maxIdx = i;
        }
        if (targetWidths[maxIdx] <= minWidths[maxIdx]) break;
        targetWidths[maxIdx] -= 1;
    }

    return targetWidths;
}

fn wrapText(allocator: std.mem.Allocator, text: []const u8, maxWidth: usize) []const []const u8 {
    if (text.len <= maxWidth) {
        var result = allocator.alloc([]const u8, 1) catch unreachable;
        result[0] = text;
        return result;
    }
    var words = std.ArrayList([]const u8).initCapacity(allocator, 8) catch unreachable;
    defer words.deinit(allocator);

    var start: usize = 0;
    for (text, 0..) |ch, i| {
        if (ch == ' ') {
            if (start < i) words.append(allocator, text[start..i]) catch unreachable;
            start = i + 1;
        }
    }
    if (start < text.len) words.append(allocator, text[start..]) catch unreachable;

    var lines = allocator.alloc([]const u8, words.items.len) catch unreachable;
    var lineCount: usize = 0;

    var currentLine = std.ArrayList(u8).initCapacity(allocator, maxWidth) catch unreachable;

    for (words.items) |word| {
        if (currentLine.items.len == 0) {
            currentLine.appendSlice(allocator, word) catch unreachable;
        } else if (currentLine.items.len + 1 + word.len <= maxWidth) {
            currentLine.append(allocator, ' ') catch unreachable;
            currentLine.appendSlice(allocator, word) catch unreachable;
        } else {
            lines[lineCount] = currentLine.toOwnedSlice(allocator) catch unreachable;
            lineCount += 1;
            currentLine = std.ArrayList(u8).initCapacity(allocator, maxWidth) catch unreachable;
            currentLine.appendSlice(allocator, word) catch unreachable;
        }
    }
    if (currentLine.items.len > 0) {
        lines[lineCount] = currentLine.toOwnedSlice(allocator) catch unreachable;
        lineCount += 1;
    } else {
        currentLine.deinit(allocator);
    }

    return allocator.realloc(lines, lineCount) catch unreachable;
}

fn freeWrappedLines(allocator: std.mem.Allocator, lines: []const []const u8) void {
    if (lines.len > 1) {
        for (lines) |line| {
            allocator.free(line);
        }
    }
    allocator.free(lines);
}

fn sum(widths: []const usize) usize {
    var total: usize = 0;
    for (widths) |w| total += w;
    return total;
}

fn getTextFromNode(node: *const MarkdownNode, allocator: std.mem.Allocator) []const u8 {
    if (std.mem.eql(u8, node.type, "text")) {
        return allocator.dupe(u8, node.content) catch unreachable;
    }
    if (node.children) |children| {
        var buffer = std.ArrayList(u8).initCapacity(allocator, 64) catch unreachable;
        for (children) |child| {
            if (std.mem.eql(u8, child.type, "text") or std.mem.eql(u8, child.type, "table_cell_content")) {
                buffer.appendSlice(allocator, child.content) catch unreachable;
            } else if (child.children) |grandchildren| {
                for (grandchildren) |grandchild| {
                    if (std.mem.eql(u8, grandchild.type, "text") or std.mem.eql(u8, grandchild.type, "table_cell_content")) {
                        buffer.appendSlice(allocator, grandchild.content) catch unreachable;
                    }
                }
            }
        }
        return buffer.toOwnedSlice(allocator) catch unreachable;
    }
    if (node.content.len > 0) {
        return allocator.dupe(u8, node.content) catch unreachable;
    }
    return "";
}

fn renderPaddedCell(state: *RendererState, text: []const u8, width: usize, alignment: []const u8) !void {
    state.output.append(state.allocator, ' ') catch unreachable;

    const innerWidth = width - 2;

    if (std.mem.eql(u8, alignment, "right")) {
        const padding = innerWidth - text.len;
        for (0..padding) |_| {
            state.output.append(state.allocator, ' ') catch unreachable;
        }
        state.output.appendSlice(state.allocator, text) catch unreachable;
        state.output.append(state.allocator, ' ') catch unreachable;
    } else if (std.mem.eql(u8, alignment, "center")) {
        const padding = innerWidth - text.len;
        const leftPad = padding / 2;
        const rightPad = padding - leftPad + 1;
        for (0..leftPad) |_| {
            state.output.append(state.allocator, ' ') catch unreachable;
        }
        state.output.appendSlice(state.allocator, text) catch unreachable;
        for (0..rightPad) |_| {
            state.output.append(state.allocator, ' ') catch unreachable;
        }
    } else {
        state.output.appendSlice(state.allocator, text) catch unreachable;
        const padding = innerWidth - text.len;
        for (0..padding) |_| {
            state.output.append(state.allocator, ' ') catch unreachable;
        }
        state.output.append(state.allocator, ' ') catch unreachable;
    }

    state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
    state.output.appendSlice(state.allocator, "│") catch unreachable;
    state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
}
