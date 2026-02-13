const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const isNewLine = @import("../utils/isNewLine.zig").isNewLine;
const newNode = @import("../utils/newNode.zig").newNode;
const closeNode = @import("../utils/closeNode.zig").closeNode;
const parseBlock = @import("../parse/parseBlock.zig").parseBlock;

fn appendToSlice(allocator: std.mem.Allocator, slice: []*MarkdownNode, item: *MarkdownNode) ![]*MarkdownNode {
    const new_slice = try allocator.alloc(*MarkdownNode, slice.len + 1);
    std.mem.copyForwards(*MarkdownNode, new_slice, slice);
    new_slice[slice.len] = item;
    allocator.free(slice);
    return new_slice;
}

pub fn testStart(state: *BlockParserState, parent: *MarkdownNode) bool {
    if (parent.acceptsContent) return false;

    if (std.mem.eql(u8, parent.type, "paragraph") and !parent.blankAfter) {
        return false;
    }

    if (state.i >= state.src.len) return false;

    const char = state.src[state.i];
    if (state.indent >= 4 and !isNewLine(char)) {
        var closed_node: ?*MarkdownNode = null;
        var effective_parent = parent;

        const is_list = std.mem.eql(u8, effective_parent.type, "list_ordered") or std.mem.eql(u8, effective_parent.type, "list_bulleted");
        if (is_list) {
            const idx = state.openNodes.items.len;
            if (idx > 0) {
                closed_node = state.openNodes.pop();
                effective_parent = state.openNodes.items[state.openNodes.items.len - 1];
            }
        }

        if (state.maybeContinue) {
            state.maybeContinue = false;
            var i: usize = state.openNodes.items.len;
            while (i > 1) : (i -= 1) {
                const iter_node = state.openNodes.items[i - 1];
                if (iter_node.maybeContinuing) {
                    iter_node.maybeContinuing = false;
                    closed_node = iter_node;
                    state.openNodes.shrinkRetainingCapacity(i - 1);
                    break;
                }
            }
            effective_parent = state.openNodes.items[state.openNodes.items.len - 1];
        }

        if (closed_node) |cn| {
            closeNode(state, cn);
        }

        const code_indent = state.indent - 4;
        const code = newNode(state.allocator, "code_block", true, state.i, state.line, 1, "    ", code_indent, null) catch unreachable;
        code.acceptsContent = true;

        if (code_indent > 0) {
            const code_indent_usize = @as(usize, @intCast(code_indent));
            const padding = state.allocator.alloc(u8, code_indent_usize) catch unreachable;
            @memset(padding[0..code_indent_usize], ' ');
            code.*.content = padding;
            code.*.content_allocated = true;
        } else {
            code.*.content = "";
            code.*.content_allocated = false;
        }

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

        state.indent = 0;
        state.hasBlankLine = false;
        parseBlock(state, code);

        return true;
    }

    return false;
}

pub fn testContinue(state: *BlockParserState, node: *MarkdownNode) bool {
    if (state.hasBlankLine and state.indent >= 4) {
        const padding = @max(state.indent, 4) - 4;
        const old_content = node.content;
        const spaces = state.allocator.alloc(u8, padding) catch unreachable;
        defer state.allocator.free(spaces);
        @memset(spaces[0..padding], ' ');
        const new_content = std.fmt.allocPrint(state.allocator, "{s}{s}", .{ old_content, spaces }) catch unreachable;
        if (node.content_allocated) {
            state.allocator.free(old_content);
        }
        node.content = new_content;
        node.content_allocated = true;
    }

    if (state.indent >= 4) {
        state.indent -= 4;
        return true;
    }

    if (state.hasBlankLine) {
        return true;
    }

    return false;
}

pub const codeBlockRule = BlockRule{
    .name = "code_block",
    .testStart = testStart,
    .testContinue = testContinue,
};
