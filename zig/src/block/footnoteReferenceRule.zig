const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const getEndOfLine = @import("../utils/getEndOfLine.zig").getEndOfLine;
const isEscaped = @import("../utils/isEscaped.zig").isEscaped;
const isNewLine = @import("../utils/isNewLine.zig").isNewLine;
const isSpace = @import("../utils/isSpace.zig").isSpace;
const newBlock = @import("../utils/newBlock.zig").newBlock;
const normalizeLabel = @import("../utils/normalizeLabel.zig").normalizeLabel;
const parseBlock = @import("../parse/parseBlock.zig").parseBlock;
const appendChild = @import("../utils/appendChild.zig").appendChild;

pub fn testStart(state: *BlockParserState, parent: *MarkdownNode) bool {
    if (parent.acceptsContent) {
        return false;
    }

    if (state.i >= state.src.len) return false;

    const char = state.src[state.i];
    if (!state.isEscaped and state.indent <= 3 and char == '[') {
        if (std.mem.eql(u8, parent.type, "paragraph") and !parent.blankAfter) {
            return false;
        }

        const start = state.i;
        var footnoteStart = state.i + 1;

        // Check for ^ that indicates a footnote (not a regular link reference)
        if (state.src[footnoteStart] != '^') {
            return false;
        }
        footnoteStart += 1;

        var label: []const u8 = "";
        var label_end: usize = footnoteStart;
        var i = footnoteStart;
        while (i < state.src.len) : (i += 1) {
            if (!isEscaped(state.src, i)) {
                if (state.src[i] == ']') {
                    label = state.src[footnoteStart..i];
                    label_end = i;
                    i += 1; // Move past ']'
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
            if (!isSpace(c)) break true;
        } else false;

        if (!hasNonSpace) {
            return false;
        }

        if (i >= state.src.len or state.src[i] != ':') {
            return false;
        }

        i += 1; // Move past ':'
        var start_content = i;

        while (start_content < state.src.len and isSpace(state.src[start_content])) : (start_content += 1) {}

        state.i = start_content;

        const normalized = normalizeLabel(state.allocator, label) catch return false;

        if (state.footnotes.get(normalized) != null) {
            state.allocator.free(normalized);
            return true;
        }

        const ref = newBlock(state.allocator, "footnote_ref", start, state.line, "", 0) catch unreachable;

        const footnoteRef = @import("../types/FootnoteReference.zig").FootnoteReference{
            .label = normalized,
            .content = ref,
        };

        state.footnotes.put(normalized, footnoteRef) catch return false;

        if (state.hasBlankLine and parent.children != null and parent.children.?.len > 0) {
            const last_child = parent.children.?[parent.children.?.len - 1];
            last_child.blankAfter = true;
            state.hasBlankLine = false;
        }

        appendChild(state.allocator, parent, ref) catch unreachable;
        state.openNodes.append(state.allocator, ref) catch unreachable;

        state.hasBlankLine = false;
        parseBlock(state, ref);

        ref.length = state.i - ref.index;

        return true;
    }

    if (state.hasBlankLine and state.indent >= 4 and parent.children != null and parent.children.?.len > 0) {
        const lastChild = parent.children.?[parent.children.?.len - 1];
        if (std.mem.eql(u8, lastChild.type, "footnote_ref")) {
            state.indent = 0;
            parseBlock(state, lastChild);
            return true;
        }
    }

    return false;
}

pub fn testContinue(state: *BlockParserState, node: *MarkdownNode) bool {
    if (state.hasBlankLine) {
        return false;
    }

    // Find the most recent open paragraph node that's a child of this footnote
    var paragraphNode: ?*MarkdownNode = null;
    var i = state.openNodes.items.len;
    while (i > 0) : (i -= 1) {
        const openNode = state.openNodes.items[i - 1];
        if (std.mem.eql(u8, openNode.type, "paragraph")) {
            paragraphNode = openNode;
            break;
        }
    }

    if (paragraphNode) |openNode| {
        // Continue if indent >= 4 (could be a code block or lazy continuation)
        // or if the paragraph ends with hard break (two spaces)
        // or if there's a non-footnote link reference
        if (state.indent >= 4) {
            state.maybeContinue = true;
            node.maybeContinuing = true;
            return true;
        }

        if (openNode.content.len >= 3 and std.mem.eql(u8, openNode.content[openNode.content.len - 3 ..], "  \n")) {
            state.maybeContinue = true;
            node.maybeContinuing = true;
            return true;
        }

        if (state.src[state.i] == '[' and (state.i + 1 < state.src.len and state.src[state.i + 1] != '^')) {
            state.maybeContinue = true;
            node.maybeContinuing = true;
            return true;
        }
    }

    return false;
}

pub const footnoteReferenceRule = BlockRule{
    .name = "footnote_ref",
    .testStart = testStart,
    .testContinue = testContinue,
};
