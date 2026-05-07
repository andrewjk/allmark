const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const isNewLine = @import("../utils/isNewLine.zig").isNewLine;
const isSpace = @import("../utils/isSpace.zig").isSpace;
const decodeEntities = @import("../utils/decodeEntities.zig").decodeEntities;
const escapeBackslashes = @import("../utils/escapeBackslashes.zig").escapeBackslashes;
const closeNode = @import("../utils/closeNode.zig").closeNode;
const getEndOfLine = @import("../utils/getEndOfLine.zig").getEndOfLine;
const newBlock = @import("../utils/newBlock.zig").newBlock;

fn appendToSlice(allocator: std.mem.Allocator, slice: []*MarkdownNode, item: *MarkdownNode) ![]*MarkdownNode {
    const new_slice = try allocator.alloc(*MarkdownNode, slice.len + 1);
    std.mem.copyForwards(*MarkdownNode, new_slice, slice);
    new_slice[slice.len] = item;
    allocator.free(slice);
    return new_slice;
}

pub fn testStart(state: *BlockParserState, parent: *MarkdownNode) bool {
    var effective_parent = parent;
    if (effective_parent.acceptsContent) {
        return false;
    }

    if (state.i >= state.src.len) return false;

    const char = state.src[state.i];
    if (state.indent <= 3 and (char == '`' or char == '~')) {
        var matched: usize = 1;
        var end = state.i + 1;
        var haveSpace = false;
        while (end < state.src.len) : (end += 1) {
            const nextChar = state.src[end];
            if (nextChar == char) {
                if (haveSpace) {
                    return false;
                }
                matched += 1;
            } else if (isNewLine(nextChar)) {
                break;
            } else if (isSpace(state.src[end])) {
                haveSpace = true;
            } else {
                break;
            }
        }

        if (matched >= 3) {
            var closedNode: ?*MarkdownNode = null;

            const markup = state.allocator.alloc(u8, matched) catch unreachable;
            defer state.allocator.free(markup);
            @memset(markup, char);

            var info: []const u8 = "";
            var info_allocated = false;
            const infoStart = state.i + matched;
            if (end < state.src.len and (state.src[end] == '\n')) {
                end += 1;
            } else {
                end = getEndOfLine(state);
                info = state.src[infoStart..end];

                if (char == '`' and std.mem.indexOfScalar(u8, info, '`') != null) {
                    return false;
                }

                const decoded = decodeEntities(state.allocator, info) catch unreachable;
                const escaped = escapeBackslashes(state.allocator, decoded) catch unreachable;
                state.allocator.free(decoded);
                info = escaped;
                info_allocated = true;
            }
            defer if (info_allocated) state.allocator.free(info);

            if (state.maybeContinue) {
                state.maybeContinue = false;
                var i: usize = state.openNodes.items.len;
                while (i > 1) {
                    i -= 1;
                    const node = state.openNodes.items[i];
                    if (node.maybeContinuing) {
                        node.maybeContinuing = false;
                        closedNode = node;
                        state.openNodes.shrinkRetainingCapacity(i);
                        break;
                    }
                }
                if (state.openNodes.items.len > 0) {
                    effective_parent = state.openNodes.items[state.openNodes.items.len - 1];
                }
            }

            if (std.mem.eql(u8, effective_parent.type, "paragraph")) {
                closedNode = state.openNodes.pop();
                if (state.openNodes.items.len > 0) {
                    effective_parent = state.openNodes.items[state.openNodes.items.len - 1];
                }
            }

            if (closedNode != null) {
                closeNode(state, closedNode.?);
            }

            const code = newBlock(state.allocator, "code_fence", state.i, state.line, markup, state.indent) catch unreachable;
            code.*.acceptsContent = true;
            code.*.info = state.allocator.dupe(u8, info) catch unreachable;

            state.i = end;

            if (state.hasBlankLine) {
                if (effective_parent.children) |children| {
                    if (children.len > 0) {
                        const last_child = children[children.len - 1];
                        last_child.blankAfter = true;
                    }
                    state.hasBlankLine = false;
                }
            }

            if (effective_parent.children == null) {
                effective_parent.children = state.allocator.alloc(*MarkdownNode, 0) catch unreachable;
            }
            effective_parent.children = appendToSlice(state.allocator, effective_parent.children.?, code) catch unreachable;
            state.openNodes.append(state.allocator, code) catch unreachable;

            return true;
        }
    }

    return false;
}

pub fn testContinue(state: *BlockParserState, node: *MarkdownNode) bool {
    if (state.hasBlankLine) {
        const padding = state.allocator.alloc(u8, @as(usize, @intCast(state.indent))) catch unreachable;
        defer state.allocator.free(padding);
        @memset(padding, ' ');
        const newContent = std.fmt.allocPrint(state.allocator, "{s}{s}", .{ node.content, padding }) catch unreachable;
        if (node.content_allocated) {
            state.allocator.free(node.content);
        }
        node.*.content = newContent;
        node.*.content_allocated = true;
        return true;
    }

    const char = state.src[state.i];
    if (state.indent <= 3 and (char == '`' or char == '~')) {
        if (node.markup.len > 0 and node.markup[0] == char) {
            var endMatched: usize = 0;
            var end = state.i;
            while (end < state.src.len) : (end += 1) {
                const nextChar = state.src[end];
                if (nextChar == char) {
                    endMatched += 1;
                } else {
                    break;
                }
            }

            if (endMatched >= node.markup.len) {
                while (end < state.src.len) : (end += 1) {
                    const nextChar = state.src[end];
                    if (isNewLine(nextChar)) {
                        break;
                    } else if (!isSpace(nextChar)) {
                        return true;
                    }
                }

                state.i = end;
                return false;
            }
        }
    }

    return true;
}

pub const codeFenceRule = BlockRule{
    .name = "code_fence",
    .testStart = testStart,
    .testContinue = testContinue,
};
