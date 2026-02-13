const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const isEscaped = @import("../utils/isEscaped.zig").isEscaped;
const isSpace = @import("../utils/isSpace.zig").isSpace;
const closeNode = @import("../utils/closeNode.zig").closeNode;
const movePastMarker = @import("../utils/movePastMarker.zig").movePastMarker;
const newNode = @import("../utils/newNode.zig").newNode;

pub fn testStart(state: *BlockParserState, parent: *MarkdownNode) bool {
    if (parent.acceptsContent) return false;

    if (state.i >= state.src.len) return false;
    const char = state.src[state.i];

    if (state.indent <= 3 and char == '>' and !isEscaped(state.src, state.i)) {
        var effective_parent = parent;
        var closed_node: ?*MarkdownNode = null;

        if (std.mem.eql(u8, parent.type, "paragraph")) {
            const idx = state.openNodes.items.len;
            if (idx > 0) {
                effective_parent = state.openNodes.items[idx - 1];
                closed_node = state.openNodes.pop();
            }
        }

        if (closed_node) |cn| {
            closeNode(state, cn);
        }

        const quote = newNode(state.allocator, "block_quote", true, state.i, state.line, 1, &[_]u8{'>'}, state.indent + 1, null) catch unreachable;
        effective_parent.children.?.append(quote) catch unreachable;
        state.openNodes.append(quote) catch unreachable;

        movePastMarker(1, state);
        state.hasBlankLine = false;

        return true;
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
                if (children.len > 0) children[children.len - 1] else null;
                break :blk null;
            } else null;

            if (last_child) |lc| {
                lc.blankAfter = true;
            }
            state.hasBlankLine = false;
        }
    }
}

pub const blockQuoteRule = BlockRule{
    .name = "block_quote",
    .testStart = testStart,
    .testContinue = testContinue,
    .closeNode = closeNodeFn,
};
