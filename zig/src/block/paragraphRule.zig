const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const getEndOfLine = @import("../utils/getEndOfLine.zig").getEndOfLine;
const isSpaceFn = @import("../utils/isSpace.zig").isSpace;
const newNode = @import("../utils/newNode.zig").newNode;

pub fn testStart(state: *BlockParserState, parent: *MarkdownNode) bool {
    if (parent.acceptsContent) {
        return false;
    }

    if (std.mem.eql(u8, parent.type, "paragraph") and !parent.blankAfter) {
        return false;
    }

    const endOfLine = getEndOfLine(state);
    const content = state.src[state.i..endOfLine];

    const hasNonSpace = for (content) |c| {
        if (!isSpaceFn(c)) break true;
    } else false;

    if (!hasNonSpace) {
        return false;
    }

    const paragraph = newNode(state.allocator, "paragraph", true, state.i, state.line, 1, "", 0, null) catch unreachable;
    paragraph.*.content = content;
    state.i = endOfLine;

    if (state.hasBlankLine and parent.children != null and parent.children.?.len > 0) {
        const last_child = parent.children.?[parent.children.?.len - 1];
        last_child.?.blankAfter = true;
        state.hasBlankLine = false;
    }

    parent.children.?.append(paragraph) catch unreachable;
    state.openNodes.append(paragraph) catch unreachable;

    return true;
}

pub fn testContinue(state: *BlockParserState, _node: *MarkdownNode) bool {
    _ = _node;
    if (state.hasBlankLine) {
        return false;
    }

    return true;
}

pub const paragraphRule = BlockRule{
    .name = "paragraph",
    .testStart = testStart,
    .testContinue = testContinue,
};
