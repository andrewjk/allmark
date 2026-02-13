const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const isNewLine = @import("../utils/isNewLine.zig").isNewLine;
const isSpace = @import("../utils/isSpace.zig").isSpace;
const closeNode = @import("../utils/closeNode.zig").closeNode;
const getEndOfLine = @import("../utils/getEndOfLine.zig").getEndOfLine;
const newNode = @import("../utils/newNode.zig").newNode;

pub fn testStart(state: *BlockParserState, parent: *MarkdownNode) bool {
    if (parent.acceptsContent) return false;

    if (state.i >= state.src.len) return false;
    const char = state.src[state.i];

    if (state.indent <= 3 and (char == '-' or char == '_' or char == '*')) {
        var matched: usize = 1;
        var end = state.i + 1;
        while (end < state.src.len) : (end += 1) {
            if (state.src[end] == char) {
                matched += 1;
            } else if (isNewLine(state.src[end])) {
                break;
            } else if (!isSpace(state.src[end])) {
                break;
            }
        }

        if (matched >= 3) {
            var closed_node: ?*MarkdownNode = null;

            if (state.maybeContinue) {
                state.maybeContinue = false;
                var i: usize = state.openNodes.items.len;
                while (i > 1) : (i -= 1) {
                    const node = state.openNodes.items[i];
                    if (node.maybeContinuing) {
                        node.maybeContinuing = false;
                        closed_node = node;
                        state.openNodes.shrinkRetainingCapacity(i);
                        break;
                    }
                }
                parent = state.openNodes.items[state.openNodes.items.len - 1];
            }

            if (parent.type == "paragraph") {
                const idx = state.openNodes.items.len;
                if (idx > 0) {
                    closed_node = state.openNodes.pop();
                }
            }

            if (closed_node) |cn| {
                closeNode(state, cn);
            }

            if (state.hasBlankLine and parent.children != null and parent.children.?.len > 0) {
                const last_child = parent.children.?[parent.children.?.len - 1];
                if (last_child) |lc| {
                    lc.blankAfter = true;
                }
                state.hasBlankLine = false;
            }

            const markup = state.src[state.i..end];
            const tbr = newNode(state.allocator, "thematic_break", true, state.i, state.line, 1, markup, 0, null) catch unreachable;

            parent.children.?.push(tbr) catch unreachable;
            state.openNodes.append(tbr) catch unreachable;

            state.i = end;

            return true;
        }
    }

    return false;
}

pub fn testContinue(_state: *BlockParserState, _node: *MarkdownNode) bool {
    return false;
}

pub const thematicBreakRule = BlockRule{
    .name = "thematic_break",
    .testStart = testStart,
    .testContinue = testContinue,
};
