const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const isEscaped = @import("../utils/isEscaped.zig").isEscaped;
const isSpace = @import("../utils/isSpace.zig").isSpace;
const closeNode = @import("../utils/closeNode.zig").closeNode;
const getEndOfLine = @import("../utils/getEndOfLine.zig").getEndOfLine;
const newBlock = @import("../utils/newBlock.zig").newBlock;
const appendChild = @import("../utils/appendChild.zig").appendChild;

pub fn testStart(state: *BlockParserState, parent: *MarkdownNode) bool {
    if (parent.acceptsContent) return false;

    if (state.i >= state.src.len) return false;
    const char = state.src[state.i];

    if (state.indent <= 3 and char == '#' and !isEscaped(state.src, state.i)) {
        var level: usize = 1;
        var j = state.i + 1;
        while (j < state.src.len and state.src[j] == '#') : (j += 1) {
            level += 1;
            if (level >= 7) break;
        }

        if (level < 7 and isSpace(state.src[j])) {
            var closed_node: ?*MarkdownNode = null;
            var effective_parent = parent;

            // If there's an open paragraph, close it
            if (std.mem.eql(u8, parent.type, "paragraph")) {
                closed_node = state.openNodes.pop();
                effective_parent = state.openNodes.items[state.openNodes.items.len - 1];
            }

            if (closed_node) |cn| {
                closeNode(state, cn);
            }

            const markup = state.allocator.alloc(u8, level) catch unreachable;
            defer state.allocator.free(markup);
            @memset(markup[0..level], '#');

            const heading = newBlock(state.allocator, "heading", state.i, state.line, markup, 0) catch unreachable;

            if (state.hasBlankLine) {
                if (effective_parent.children) |children| {
                    if (children.len > 0) {
                        const last_child = children[children.len - 1];
                        last_child.blankAfter = true;
                    }
                    state.hasBlankLine = false;
                }
            }

            appendChild(state.allocator, effective_parent, heading) catch unreachable;
            // Don't add heading to openNodes - it doesn't accept block children
            // Heading content is stored in the content field and processed as inlines later

            // Advance past the # characters (like TypeScript: state.i += level)
            state.i = j;
            const eol = getEndOfLine(state);

            // Strip trailing spaces and optional closing # characters
            var end = eol - 1;

            // First pass: skip trailing spaces
            while (end >= state.i) {
                if (!isSpace(state.src[end])) break;
                end -= 1;
            }

            // Second pass: skip trailing # characters (optional closing sequence)
            while (end >= state.i) {
                if (state.src[end] != '#') {
                    // Check if the char before # is a backslash or non-space
                    if (state.src[end] == '\\' or !isSpace(state.src[end])) {
                        end = eol - 1; // Reset to end
                    }
                    break;
                }
                end -= 1;
            }

            if (state.i <= end) {
                heading.content = state.src[state.i .. end + 1];
            } else {
                heading.content = "";
            }
            state.i = eol;
            heading.length = state.i - heading.index;

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

pub const headingRule = BlockRule{
    .name = "heading",
    .testStart = testStart,
    .testContinue = testContinue,
};
