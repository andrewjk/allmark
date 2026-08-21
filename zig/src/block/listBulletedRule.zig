const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const isNumeric = @import("../utils/isAlphaNumeric.zig").isNumeric;
const isNewLine = @import("../utils/isNewLine.zig").isNewLine;
const isSpace = @import("../utils/isSpace.zig").isSpace;
const getMarkup = @import("./listRule.zig").getMarkup;
const testListStart = @import("./listRule.zig").testListStart;
const testListContinue = @import("./listRule.zig").testListContinue;
const isLooseList = @import("./listRule.zig").isLooseList;
const movePastMarker = @import("../utils/movePastMarker.zig").movePastMarker;

pub fn testStart(state: *BlockParserState, parent: *MarkdownNode, end_of_line: usize) bool {
    if (parent.acceptsContent) {
        return false;
    }

    const info = getMarkup(state);
    if (info == null) return false;

    return testListStart(state, parent, end_of_line, info);
}

pub fn testContinue(state: *BlockParserState, node: *MarkdownNode) bool {
    const info = getMarkup(state);
    return testListContinue(state, node, info);
}

fn closeNode(state: *BlockParserState, node: *MarkdownNode) void {
    _ = state;
    node.loose = isLooseList(node);
}

pub const listBulletedRule = BlockRule{
    .name = "list_bulleted",
    .testStart = testStart,
    .testContinue = testContinue,
    .closeNode = closeNode,
};
