const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const isSpace = @import("../utils/isSpace.zig").isSpace;
const closeNode = @import("../utils/closeNode.zig").closeNode;
const getEndOfLine = @import("../utils/getEndOfLine.zig").getEndOfLine;
const newBlock = @import("../utils/newBlock.zig").newBlock;
const appendChild = @import("../utils/appendChild.zig").appendChild;

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
            } else if (state.src[end] == '\n') {
                end += 1;
                break;
            } else if (state.src[end] == '\r') {
                end += 1;
                if (end < state.src.len and state.src[end] == '\n') {
                    end += 1;
                }
                break;
            } else if (!isSpace(state.src[end])) {
                return false;
            }
        }

        if (matched >= 3) {
            var closed_node: ?*MarkdownNode = null;
            var effective_parent = parent;

            if (state.maybeContinue) {
                state.maybeContinue = false;
                var i: usize = state.openNodes.items.len;
                while (i > 0) : (i -= 1) {
                    const node = state.openNodes.items[i - 1];
                    if (node.maybeContinuing) {
                        node.maybeContinuing = false;
                        closed_node = node;
                        state.openNodes.shrinkRetainingCapacity(i - 1);
                        break;
                    }
                }
                effective_parent = state.openNodes.items[state.openNodes.items.len - 1];
            }

            if (std.mem.eql(u8, effective_parent.type, "paragraph")) {
                const idx = state.openNodes.items.len;
                if (idx > 0) {
                    closed_node = state.openNodes.pop();
                }
                effective_parent = state.openNodes.items[state.openNodes.items.len - 1];
            }

            if (effective_parent.delimiter.len > 0) {
                if (std.mem.eql(u8, effective_parent.type, "list_item") and !state.hasBlankLine and char == effective_parent.delimiter[0]) {
                    if (state.openNodes.items.len >= 2) {
                        _ = state.openNodes.pop();
                        closed_node = state.openNodes.pop();
                        effective_parent = state.openNodes.items[state.openNodes.items.len - 1];
                    }
                }
            }

            if (std.mem.eql(u8, effective_parent.type, "list_bulleted") or std.mem.eql(u8, effective_parent.type, "list_ordered")) {
                if (state.openNodes.items.len >= 1) {
                    closed_node = state.openNodes.pop();
                    effective_parent = state.openNodes.items[state.openNodes.items.len - 1];
                }
            }

            if (closed_node) |cn| {
                closeNode(state, cn);
            }

            if (state.hasBlankLine and effective_parent.children != null and effective_parent.children.?.len > 0) {
                const last_child = effective_parent.children.?[effective_parent.children.?.len - 1];
                last_child.blankAfter = true;
                state.hasBlankLine = false;
            }

            const markup = state.src[state.i..end];
            const tbr = newBlock(state.allocator, "thematic_break", state.i, state.line, markup, 0) catch unreachable;
            tbr.length = end - state.i;

            appendChild(state.allocator, effective_parent, tbr) catch unreachable;

            state.i = end;

            return true;
        }
    }

    return false;
}

pub fn testContinue(state: *BlockParserState, node: *MarkdownNode) bool {
    _ = state;
    _ = node;
    return false;
}

pub const thematicBreakRule = BlockRule{
    .name = "thematic_break",
    .testStart = testStart,
    .testContinue = testContinue,
};
