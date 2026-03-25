const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const isNumeric = @import("../utils/isAlphaNumeric.zig").isNumeric;
const isNewLine = @import("../utils/isNewLine.zig").isNewLine;
const isSpace = @import("../utils/isSpace.zig").isSpace;
const testListStart = @import("./listRule.zig").testListStart;
const testListContinue = @import("./listRule.zig").testListContinue;
const movePastMarker = @import("../utils/movePastMarker.zig").movePastMarker;
const ListInfo = @import("./listRule.zig").ListInfo;

pub fn getMarkup(state: *BlockParserState) ?ListInfo {
    if (state.i >= state.src.len) return null;

    var end = state.i;
    while (end < state.src.len and isNumeric(state.src[end])) {
        end += 1;
    }

    const numbers_len = end - state.i;
    if (numbers_len == 0 or numbers_len >= 10) return null;
    if (end >= state.src.len) return null;

    const delimiter = state.src[end];
    if (delimiter != '.' and delimiter != ')') return null;

    const next_is_space = (end + 1 >= state.src.len) or isSpace(state.src[end + 1]);
    if (!next_is_space) return null;

    const markup = state.src[state.i .. end + 1];
    const is_blank = (end + 1 >= state.src.len) or isNewLine(state.src[end + 1]);

    return ListInfo{
        .delimiter = delimiter,
        .markup = markup,
        .is_blank = is_blank,
        .type = "list_ordered",
    };
}

pub fn testStart(state: *BlockParserState, parent: *MarkdownNode) bool {
    if (parent.acceptsContent) {
        return false;
    }

    const info = getMarkup(state);
    if (info == null) return false;

    return testListStart(state, parent, info);
}

pub fn testContinue(state: *BlockParserState, node: *MarkdownNode) bool {
    const info = getMarkup(state);
    return testListContinue(state, node, info);
}

pub const listOrderedRule = BlockRule{
    .name = "list_ordered",
    .testStart = testStart,
    .testContinue = testContinue,
};
