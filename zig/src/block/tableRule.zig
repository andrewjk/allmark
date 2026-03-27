const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const closeNode = @import("../utils/closeNode.zig").closeNode;
const getEndOfLine = @import("../utils/getEndOfLine.zig").getEndOfLine;
const isEscaped = @import("../utils/isEscaped.zig").isEscaped;
const isNewLine = @import("../utils/isNewLine.zig").isNewLine;
const isSpace = @import("../utils/isSpace.zig").isSpace;
const newBlock = @import("../utils/newBlock.zig").newBlock;
const appendChild = @import("../utils/appendChild.zig").appendChild;

pub fn testStart(state: *BlockParserState, parent: *MarkdownNode) bool {
    if (parent.acceptsContent) {
        return false;
    }

    if (state.i >= state.src.len) return false;

    if (parent.children != null and parent.children.?.len > 0) {
        const lastNode = parent.children.?[parent.children.?.len - 1];
        if (!state.hasBlankLine and std.mem.eql(u8, lastNode.type, "table")) {
            const endOfLine = getEndOfLine(state);

            const header = lastNode.children.?[0];
            const headers = state.allocator.alloc([]const u8, header.children.?.len) catch return false;
            defer state.allocator.free(headers);

            var hi: usize = 0;
            while (hi < header.children.?.len) : (hi += 1) {
                const cell = header.children.?[hi];
                headers[hi] = cell.info orelse "";
            }

            var rowLength = endOfLine - state.i;
            if (state.src[endOfLine - 1] == '\n') {
                rowLength -= 1;
            }

            const row = newBlock(state.allocator, "table_row", state.i, state.line, "", 0) catch unreachable;
            row.length = rowLength;
            appendChild(state.allocator, lastNode, row) catch unreachable;

            const rowSrc = state.src[state.i .. state.i + rowLength];
            var pipePositions = std.ArrayList(usize).initCapacity(state.allocator, 0) catch return false;
            defer pipePositions.deinit(state.allocator);
            loadPipePositions(state.allocator, &pipePositions, rowSrc) catch unreachable;

            var rowContent = std.mem.trim(u8, rowSrc, &std.ascii.whitespace);

            var rowStart: usize = 0;
            var rowEnd: usize = rowContent.len;
            if (rowContent.len > 0 and rowContent[0] == '|') {
                rowStart = 1;
            }
            if (rowContent.len > 0 and rowContent[rowContent.len - 1] == '|') {
                rowEnd = rowContent.len - 1;
            }
            rowContent = rowContent[rowStart..rowEnd];

            var rowParts = std.ArrayList([]const u8).initCapacity(state.allocator, 0) catch return false;
            defer rowParts.deinit(state.allocator);

            var partStart: usize = 0;
            var partEnd: usize = 0;
            while (partEnd < rowContent.len) : (partEnd += 1) {
                if (rowContent[partEnd] == '|' and !isEscaped(rowContent, partEnd)) {
                    const part = rowContent[partStart..partEnd];
                    rowParts.append(state.allocator, part) catch return false;
                    partStart = partEnd + 1;
                }
            }
            if (partStart < rowContent.len) {
                const part = rowContent[partStart..];
                rowParts.append(state.allocator, part) catch return false;
            }

            const numParts = headers.len;
            var ri: usize = 0;
            while (ri < numParts) : (ri += 1) {
                const text = if (ri < rowParts.items.len) rowParts.items[ri] else "";
                parseTableCell(state.allocator, row, state, ri, text, headers[ri], pipePositions.items) catch unreachable;
            }

            lastNode.length = endOfLine - lastNode.index;

            state.i = endOfLine;
            return true;
        }
    }

    const char = state.src[state.i];
    if (state.indent <= 3 and (char == '|' or char == '-' or char == ':')) {
        var cells = std.ArrayList([]const u8).initCapacity(state.allocator, 0) catch return false;
        defer cells.deinit(state.allocator);

        cells.append(state.allocator, if (char == ':') "left" else "") catch return false;

        var end = state.i + 1;
        var lastChar = char;
        while (end < state.src.len) : (end += 1) {
            const nextChar = state.src[end];
            if (nextChar == '|') {
                cells.append(state.allocator, "") catch return false;
                lastChar = nextChar;
            } else if (nextChar == '-') {
                lastChar = nextChar;
            } else if (nextChar == ':') {
                const x = cells.items.len - 1;
                if (lastChar == '|') {
                    cells.items[x] = "left";
                } else {
                    cells.items[x] = if (cells.items[x].len > 0) "center" else "right";
                }
                lastChar = nextChar;
            } else if (isNewLine(nextChar)) {
                end += 1;
                break;
            } else if (isSpace(state.src[end])) {
                continue;
            } else {
                return false;
            }
        }
        if (lastChar == '|') {
            _ = cells.pop();
        }

        const haveParagraph = std.mem.eql(u8, parent.type, "paragraph") and !parent.blankAfter and parent.content.len > 0;
        if (haveParagraph) {
            var headerCellCount: usize = 1;
            var headerContent = std.mem.trim(u8, parent.content, &std.ascii.whitespace);

            var headerStart: usize = 0;
            var headerEnd: usize = headerContent.len;
            if (headerContent.len > 0 and headerContent[0] == '|') {
                headerStart = 1;
            }
            if (headerContent.len > 0 and headerContent[headerContent.len - 1] == '|') {
                headerEnd = headerContent.len - 1;
            }
            headerContent = headerContent[headerStart..headerEnd];

            var hi: usize = 0;
            while (hi < headerContent.len) : (hi += 1) {
                if (headerContent[hi] == '|' and !isEscaped(headerContent, hi)) {
                    headerCellCount += 1;
                }
            }

            if (cells.items.len != headerCellCount) {
                return false;
            }

            var closedNode: ?*MarkdownNode = null;

            if (state.maybeContinue) {
                state.maybeContinue = false;
                var idx: usize = state.openNodes.items.len;
                while (idx > 1) : (idx -= 1) {
                    const node = state.openNodes.items[idx - 1];
                    if (node.maybeContinuing) {
                        node.maybeContinuing = false;
                        closedNode = node;
                        state.openNodes.shrinkRetainingCapacity(idx - 1);
                        break;
                    }
                }
            }

            if (closedNode != null) {
                closeNode(state, closedNode.?);
            }

            var headerLength = parent.content.len;
            if (parent.content.len > 0 and parent.content[parent.content.len - 1] == '\n') {
                headerLength -= 1;
            }

            const header = newBlock(state.allocator, "table_header", parent.index, state.line, "", 0) catch unreachable;
            header.length = headerLength;
            appendChild(state.allocator, parent, header) catch unreachable;

            const headerSrc = parent.content[0..headerLength];
            var pipePositions = std.ArrayList(usize).initCapacity(state.allocator, 0) catch return false;
            defer pipePositions.deinit(state.allocator);
            loadPipePositions(state.allocator, &pipePositions, headerSrc) catch unreachable;

            var headerParts = std.ArrayList([]const u8).initCapacity(state.allocator, 0) catch return false;
            defer headerParts.deinit(state.allocator);

            var partStart: usize = 0;
            var partEnd: usize = 0;
            while (partEnd < headerContent.len) : (partEnd += 1) {
                if (headerContent[partEnd] == '|' and !isEscaped(headerContent, partEnd)) {
                    headerParts.append(state.allocator, headerContent[partStart..partEnd]) catch return false;
                    partStart = partEnd + 1;
                }
            }
            if (partStart < headerContent.len) {
                headerParts.append(state.allocator, headerContent[partStart..]) catch return false;
            }

            var hci: usize = 0;
            while (hci < headerParts.items.len) : (hci += 1) {
                const text = headerParts.items[hci];
                const header_cell = if (hci < cells.items.len) cells.items[hci] else "";
                parseTableCell(state.allocator, header, state, hci, text, header_cell, pipePositions.items) catch unreachable;
            }

            const oldType = parent.type;
            const oldContent = parent.content;
            const oldContentAllocated = parent.content_allocated;
            parent.type = state.allocator.dupe(u8, "table") catch unreachable;
            state.allocator.free(oldType);
            if (oldContentAllocated) {
                state.allocator.free(oldContent);
            }
            parent.content = "";
            parent.content_allocated = false;
            parent.markup = state.allocator.dupe(u8, state.src[state.i..end]) catch unreachable;
            parent.markup_allocated = true;
            state.i = end;
            return true;
        }
    }

    return false;
}

pub fn testContinue(_state: *BlockParserState, _node: *MarkdownNode) bool {
    _ = _state;
    _ = _node;
    return false;
}

fn loadPipePositions(allocator: std.mem.Allocator, pipePositions: *std.ArrayList(usize), line: []const u8) !void {
    var haveEndPipe = false;
    var i: usize = 0;
    while (i < line.len) : (i += 1) {
        if (line[i] == '|' and !isEscaped(line, i)) {
            try pipePositions.append(allocator, i);
            haveEndPipe = true;
        } else if (!isSpace(line[i])) {
            if (pipePositions.items.len == 0) {
                try pipePositions.append(allocator, 0);
            }
            haveEndPipe = false;
        }
    }
    if (!haveEndPipe) {
        try pipePositions.append(allocator, if (line.len > 0) line.len - 1 else 0);
    }
}

fn parseTableCell(allocator: std.mem.Allocator, row: *MarkdownNode, state: *BlockParserState, index: usize, text: []const u8, header: []const u8, pipePositions: []const usize) !void {
    const cellStart = if (index < pipePositions.len) pipePositions[index] else 0;
    const cellEnd = if (index + 1 < pipePositions.len) pipePositions[index + 1] else 0;
    const cellLength = if (cellEnd > cellStart) cellEnd - cellStart + 1 else 0;

    const trimmed = std.mem.trim(u8, text, &std.ascii.whitespace);
    const contentStart = if (trimmed.len > 0)
        row.index + cellStart + indexOf(text, trimmed) + 1
    else
        row.index + cellStart;

    const cell = newBlock(allocator, "table_cell", row.index + cellStart, state.line, "", 0) catch unreachable;
    cell.length = cellLength;
    cell.info = if (header.len > 0) allocator.dupe(u8, header) catch unreachable else null;
    appendChild(allocator, row, cell) catch unreachable;

    var escapedText = std.ArrayList(u8).initCapacity(allocator, 0) catch unreachable;
    defer escapedText.deinit(allocator);
    var ti: usize = 0;
    while (ti < trimmed.len) : (ti += 1) {
        if (ti + 1 < trimmed.len and trimmed[ti] == '\\' and trimmed[ti + 1] == '|') {
            try escapedText.append(allocator, '|');
            ti += 1;
        } else {
            try escapedText.append(allocator, trimmed[ti]);
        }
    }

    const content = newBlock(allocator, "table_cell_content", contentStart, state.line, "", 0) catch unreachable;
    content.content = allocator.dupe(u8, escapedText.items) catch unreachable;
    content.content_allocated = true;
    appendChild(allocator, cell, content) catch unreachable;
}

fn indexOf(haystack: []const u8, needle: []const u8) usize {
    if (needle.len == 0) return 0;
    if (needle.len > haystack.len) return 0;

    var i: usize = 0;
    while (i <= haystack.len - needle.len) : (i += 1) {
        if (std.mem.eql(u8, haystack[i .. i + needle.len], needle)) {
            return i;
        }
    }
    return 0;
}

pub const tableRule = BlockRule{
    .name = "table",
    .testStart = testStart,
    .testContinue = testContinue,
};
