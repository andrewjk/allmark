const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const isEscaped = @import("../utils/isEscaped.zig").isEscaped;
const isSpace = @import("../utils/isSpace.zig").isSpace;
const closeNode = @import("../utils/closeNode.zig").closeNode;
const movePastMarker = @import("../utils/movePastMarker.zig").movePastMarker;
const newBlock = @import("../utils/newBlock.zig").newBlock;
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

    if (state.i >= state.src.len) return false;
    const char = state.src[state.i];

    if (state.indent <= 3 and char == '>' and !isEscaped(state.src, state.i)) {
        var effective_parent = parent;
        var closed_node: ?*MarkdownNode = null;

        if (std.mem.eql(u8, parent.type, "paragraph")) {
            const idx = state.openNodes.items.len;
            if (idx > 1) {
                effective_parent = state.openNodes.items[idx - 2];
                closed_node = state.openNodes.pop();
            }
        }

        if (closed_node) |cn| {
            closeNode(state, cn);
        }

        const quote = newBlock(state.allocator, "block_quote", state.i, state.line, &[_]u8{'>'}, state.indent + 1) catch unreachable;
        if (effective_parent.children == null) {
            effective_parent.children = state.allocator.alloc(*MarkdownNode, 0) catch unreachable;
        }
        effective_parent.children = appendToSlice(state.allocator, effective_parent.children.?, quote) catch unreachable;
        state.openNodes.append(state.allocator, quote) catch unreachable;

        movePastMarker(1, state);
        state.hasBlankLine = false;
        parseBlock(state, quote);

        return true;
    }

    return false;
}

pub fn testContinue(state: *BlockParserState, node: *MarkdownNode) bool {
    if (state.i >= state.src.len) return false;
    const char = state.src[state.i];

    if (state.indent <= 3 and char == '>') {
        movePastMarker(1, state);
        return true;
    }

    if (state.hasBlankLine) return false;

    const idx = state.openNodes.items.len;
    if (idx > 0) {
        const open_node = state.openNodes.items[idx - 1];
        if (std.mem.eql(u8, open_node.type, "paragraph")) {
            state.maybeContinue = true;
            node.maybeContinuing = true;
            return true;
        }
    }

    return false;
}

pub fn closeNodeFn(state: *BlockParserState, node: *MarkdownNode) void {
    if (state.hasBlankLine) {
        const last_child = if (node.children) |children| blk: {
            if (children.len > 0) {
                break :blk children[children.len - 1];
            } else {
                break :blk null;
            }
        } else null;

        if (last_child) |lc| {
            lc.blankAfter = true;
        }
        state.hasBlankLine = false;
    }
}

pub const blockQuoteRule = BlockRule{
    .name = "block_quote",
    .testStart = testStart,
    .testContinue = testContinue,
    .closeNode = closeNodeFn,
};
