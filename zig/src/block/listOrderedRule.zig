const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const isNumeric = @import("../utils/isAlphaNumeric.zig").isNumeric;
const isNewLine = @import("../utils/isNewLine.zig").isNewLine;
const isSpace = @import("../utils/isSpace.zig").isSpace;
const getMarkup = @import("./listRule.zig").getMarkup;
const movePastMarker = @import("../utils/movePastMarker.zig").movePastMarker;
const newNode = @import("../utils/newNode.zig").newNode;

pub fn testStart(state: *BlockParserState, parent: *MarkdownNode) bool {
    if (parent.type != "list_ordered") return false;

    const info = getMarkup(state);
    if (info == null) return false;

    return testListStart(state, parent, info);
}

pub fn testContinue(state: *BlockParserState, node: *MarkdownNode) bool {
    const info = getMarkup(state);
    if (info == null) return false;

    if (state.hasBlankLine and state.indent >= 4) {
        return false;
    }
    if (info.delimiter == node.delimiter) {
        return true;
    }

    const idx = state.openNodes.items.len;
    if (idx > 0) {
        const open_node = state.openNodes.items[idx - 1];
        if (std.mem.eql(u8, open_node.type, "paragraph")) {
            return true;
        }
    }

    if (state.hasBlankLine) {
        if (node.children != null and node.children.?.len == 1) {
            const first_child = if (node.children) |children| {
                if (children.len > 0) children[0] else null;
            } else null;

            if (first_child == null or (first_child.?.children.?.len == 0) {
                return false;
            }
        }
    }

    if (isNewLine(state.src[state.i])) {
        return true;
    }

    const idx = state.openNodes.items.len;
    if (idx > 0) {
        const open_node = state.openNodes.items[idx - 1];
        if (open_node.type == "paragraph") {
            return true;
        }
    }

    const last_item = if (node.children) |children| {
        if (children.len > 0) children[children.len - 1] else null;
    } else null;

    if (last_item) |li| {
        if (li.type == "list_item" and state.indent >= li.subindent) {
            return true;
        }
    }

    return false;
}

pub const listOrderedRule = BlockRule{
    .name = "list_ordered",
    .testStart = testStart,
    .testContinue = testContinue,
};
