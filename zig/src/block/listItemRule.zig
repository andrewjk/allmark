const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const isNumeric = @import("../utils/isAlphaNumeric.zig").isNumeric;
const isSpace = @import("../utils/isSpace.zig").isSpace;
const newNode = @import("../utils/newNode.zig").newNode;

pub fn testStart(_state: *BlockParserState, _parent: *MarkdownNode) bool {
    return false;
}

pub fn testContinue(state: *BlockParserState, node: *MarkdownNode) bool {
    if (state.i >= state.src.len) return false;
    const char = state.src[state.i];

    if (state.indent >= node.subindent) {
        state.indent -= node.subindent;
        return true;
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
