const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const isNumeric = @import("../utils/isAlphaNumeric.zig").isNumeric;
const isSpace = @import("../utils/isSpace.zig").isSpace;

pub fn testStart(state: *BlockParserState, parent: *MarkdownNode, _end_of_line: usize) bool {
    _ = state;
    _ = parent;
    _ = _end_of_line;
    return false;
}

pub fn testContinue(state: *BlockParserState, node: *MarkdownNode) bool {
    if (state.i >= state.src.len) {
        return false;
    }

    const char = state.src[state.i];

    if (state.indent >= node.subindent) {
        state.indent -= node.subindent;
        return true;
    }

    var i = state.openNodes.items.len;
    var itemNode: ?*MarkdownNode = null;
    while (i > 1) {
        i -= 1;
        const openNode = state.openNodes.items[i];
        if (std.mem.eql(u8, openNode.type, "list_item")) {
            itemNode = openNode;
        } else if (std.mem.eql(u8, openNode.type, "list_ordered")) {
            var end = state.i;
            while (end < state.src.len and isNumeric(state.src[end])) {
                end += 1;
            }
            const delimiter = if (end < state.src.len) state.src[end] else 0;
            if (state.indent <= 3 and state.indent < itemNode.?.subindent and end > state.i and delimiter == node.delimiter[0]) {
                return false;
            }
        } else if (std.mem.eql(u8, openNode.type, "list_bulleted")) {
            if (state.indent <= 3 and state.indent < itemNode.?.subindent and char == node.delimiter[0]) {
                return false;
            }
        }
    }

    if (state.hasBlankLine) {
        return true;
    }

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

pub const listItemRule = BlockRule{
    .name = "list_item",
    .testStart = testStart,
    .testContinue = testContinue,
};
