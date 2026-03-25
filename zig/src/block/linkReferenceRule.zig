const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const getEndOfLine = @import("../utils/getEndOfLine.zig").getEndOfLine;
const isEscaped = @import("../utils/isEscaped.zig").isEscaped;
const isNewLine = @import("../utils/isNewLine.zig").isNewLine;
const newBlock = @import("../utils/newBlock.zig").newBlock;
const normalizeLabel = @import("../utils/normalizeLabel.zig").normalizeLabel;
const parseLinkBlock = @import("../utils/parseLinkBlock.zig").parseLinkBlock;
const isSpaceFn = @import("../utils/isSpace.zig").isSpace;
const appendChild = @import("../utils/appendChild.zig").appendChild;

pub fn testStart(state: *BlockParserState, parent: *MarkdownNode) bool {
    if (parent.acceptsContent) {
        return false;
    }

    if (state.i >= state.src.len) return false;

    const char = state.src[state.i];
    if (state.indent <= 3 and char == '[' and !isEscaped(state.src, state.i)) {
        if (std.mem.eql(u8, parent.type, "paragraph") and !parent.blankAfter) {
            return false;
        }

        const start = state.i;
        const linkStart = state.i + 1;

        var label: []const u8 = "";
        var i = linkStart;
        while (i < state.src.len) : (i += 1) {
            if (!isEscaped(state.src, i)) {
                if (state.src[i] == ']') {
                    label = state.src[linkStart..i];
                    i += 1;
                    break;
                }

                if (state.src[i] == '[') {
                    return false;
                }
            }
        }

        if (label.len == 0) {
            return false;
        }

        const hasNonSpace = for (label) |c| {
            if (!isSpaceFn(c)) break true;
        } else false;

        if (!hasNonSpace) {
            return false;
        }

        if (i >= state.src.len or state.src[i] != ':') {
            return false;
        }

        const linkInfo = parseLinkBlock(state, i + 1) catch return false;
        if (linkInfo == null) {
            return false;
        }

        const normalized = normalizeLabel(state.allocator, label) catch return false;
        defer state.allocator.free(normalized);

        if (state.refs.get(normalized) != null) {
            if (linkInfo.?.url.len > 0) state.allocator.free(linkInfo.?.url);
            if (linkInfo.?.title.len > 0) state.allocator.free(linkInfo.?.title);
            return true;
        }

        const dupe = state.allocator.dupe(u8, normalized) catch unreachable;
        state.refs.put(dupe, linkInfo.?) catch return false;

        const ref = newBlock(state.allocator, "link_ref", start, state.line, "", 0) catch unreachable;

        if (state.hasBlankLine and parent.children != null and parent.children.?.len > 0) {
            const last_child = parent.children.?[parent.children.?.len - 1];
            last_child.blankAfter = true;
            state.hasBlankLine = false;
        }

        appendChild(state.allocator, parent, ref) catch unreachable;

        if (state.i > 0 and !isNewLine(state.src[state.i - 1])) {
            state.i = getEndOfLine(state);
        }

        ref.length = state.i - ref.index;

        return true;
    }

    return false;
}

pub fn testContinue(_state: *BlockParserState, _node: *MarkdownNode) bool {
    _ = _state;
    _ = _node;
    return false;
}

pub const linkReferenceRule = BlockRule{
    .name = "link_ref",
    .testStart = testStart,
    .testContinue = testContinue,
};
