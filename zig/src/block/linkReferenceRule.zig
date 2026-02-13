const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const getEndOfLine = @import("../utils/getEndOfLine.zig").getEndOfLine;
const isEscaped = @import("../utils/isEscaped.zig").isEscaped;
const isNewLine = @import("../utils/isNewLine.zig").isNewLine;
const newNode = @import("../utils/newNode.zig").newNode;
const normalizeLabel = @import("../utils/normalizeLabel.zig").normalizeLabel;
const parseLinkBlock = @import("../utils/parseLinkBlock.zig").parseLinkBlock;
const isSpaceFn = @import("../utils/isSpace.zig").isSpace;

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

        const start = state.i + 1;

        var label: []const u8 = "";
        var i = start;
        while (i < state.src.len) : (i += 1) {
            if (!isEscaped(state.src, i)) {
                if (state.src[i] == ']') {
                    label = state.src[start..i];
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
            if (isSpaceFn(c)) break true;
        } else false;

        if (!hasNonSpace) {
            return false;
        }

        if (state.src[i] != ':') {
            return false;
        }

        const linkInfo = parseLinkBlock(state, i + 1) orelse return null;
        if (linkInfo == null) {
            return false;
        }

        const normalized = try normalizeLabel(state.allocator, label);
        defer state.allocator.free(normalized);

        if (state.refs.get(normalized) != null) {
            return true;
        }

        try state.refs.put(normalized, linkInfo.*);

        const ref = newNode(state.allocator, "link_ref", true, state.i, state.line, 1, "", 0, null) catch unreachable;

        if (state.hasBlankLine and parent.children != null and parent.children.?.len > 0) {
            const last_child = parent.children.?[parent.children.?.len - 1];
            last_child.?.blankAfter = true;
            state.hasBlankLine = false;
        }

        parent.children.?.append(ref) catch unreachable;

        if (!isNewLine(&.{state.src[state.i - 1]})) {
            state.i = getEndOfLine(state);
        }

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
