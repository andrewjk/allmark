const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const closeNode = @import("../utils/closeNode.zig").closeNode;
const getEndOfLine = @import("../utils/getEndOfLine.zig").getEndOfLine;
const isEscaped = @import("../utils/isEscaped.zig").isEscaped;
const isNewLine = @import("../utils/isNewLine.zig").isNewLine;
const isSpace = @import("../utils/isSpace.zig").isSpace;
const newNode = @import("../utils/newNode.zig").newNode;
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

            const row = newNode(state.allocator, "table_row", true, state.i, state.line, 1, "", 0, null) catch unreachable;
            appendChild(state.allocator, lastNode, row) catch unreachable;

            var rowContent = state.src[state.i..endOfLine];
            rowContent = std.mem.trim(u8, rowContent, &std.ascii.whitespace);

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
                const cell = newNode(state.allocator, "table_cell", true, state.i, state.line, 1, "", 0, null) catch unreachable;
                var text = if (ri < rowParts.items.len) rowParts.items[ri] else "";
                text = std.mem.trim(u8, text, &std.ascii.whitespace);

                var escapedText = std.ArrayList(u8).initCapacity(state.allocator, 0) catch return false;
                defer escapedText.deinit(state.allocator);
                var ti: usize = 0;
                while (ti < text.len) : (ti += 1) {
                    if (ti + 1 < text.len and text[ti] == '\\' and text[ti + 1] == '|') {
                        escapedText.append(state.allocator, '|') catch return false;
                        ti += 1;
                    } else {
                        escapedText.append(state.allocator, text[ti]) catch return false;
                    }
                }
                cell.content = state.allocator.dupe(u8, escapedText.items) catch unreachable;
                cell.content_allocated = true;
                cell.info = if (ri < headers.len) state.allocator.dupe(u8, headers[ri]) catch unreachable else null;
                appendChild(state.allocator, row, cell) catch unreachable;
            }

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

            const header = newNode(state.allocator, "table_header", true, state.i, state.line, 1, "", 0, null) catch unreachable;
            appendChild(state.allocator, parent, header) catch unreachable;

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
                const cell = newNode(state.allocator, "table_cell", true, state.i, state.line, 1, "", 0, null) catch unreachable;
                var text = std.mem.trim(u8, headerParts.items[hci], &std.ascii.whitespace);

                var escapedText = std.ArrayList(u8).initCapacity(state.allocator, 0) catch return false;
                defer escapedText.deinit(state.allocator);
                var ti: usize = 0;
                while (ti < text.len) : (ti += 1) {
                    if (ti + 1 < text.len and text[ti] == '\\' and text[ti + 1] == '|') {
                        escapedText.append(state.allocator, '|') catch return false;
                        ti += 1;
                    } else {
                        escapedText.append(state.allocator, text[ti]) catch return false;
                    }
                }
                cell.content = state.allocator.dupe(u8, escapedText.items) catch unreachable;
                cell.content_allocated = true;
                if (hci < cells.items.len) {
                    cell.info = state.allocator.dupe(u8, cells.items[hci]) catch unreachable;
                }
                appendChild(state.allocator, header, cell) catch unreachable;
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

pub const tableRule = BlockRule{
    .name = "table",
    .testStart = testStart,
    .testContinue = testContinue,
};
