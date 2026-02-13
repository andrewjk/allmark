const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const isEscaped = @import("../utils/isEscaped.zig").isEscaped;
const isSpace = @import("../utils/isSpace.zig").isSpace;
const closeNode = @import("../utils/closeNode.zig").closeNode;
const getEndOfLine = @import("../utils/getEndOfLine.zig").getEndOfLine;
const newNode = @import("../utils/newNode.zig").newNode;

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

            const idx = state.openNodes.items.len;
            if (idx > 0) {
                closed_node = state.openNodes.pop();
            }

            if (closed_node) |cn| {
                closeNode(state, cn);
            }

            const markup = try state.allocator.alloc(u8, level);
            defer state.allocator.free(markup);
            @memset(markup, '#', level);

            const heading = newNode(state.allocator, "heading", true, state.i, state.line, 1, markup, 0, null) catch unreachable;

            if (state.hasBlankLine) {
                if (parent.children) |children| {
                    if (children.len > 0) {
                        const last_child = children[children.len - 1];
                        last_child.blankAfter = true;
                    }
                    state.hasBlankLine = false;
                }
            }

            parent.children.?.append(heading) catch unreachable;
            state.openNodes.append(heading) catch unreachable;

            const eol = getEndOfLine(state);
            var k = eol - 1;
            while (k >= state.i) : (k -= 1) {
                if (!isSpace(state.src[k])) break;
                if (state.src[k] != '\\' and !isSpace(state.src[k])) {
                    k -= 1;
                    break;
                }
            }

            const content_end = k + 1;
            heading.content = state.src[state.i..content_end];
            state.i = content_end;

            return true;
        }
    }

    return false;
}

pub fn testContinue(_state: *BlockParserState, _node: *MarkdownNode) bool {
    return false;
}

pub const headingRule = BlockRule{
    .name = "heading",
    .testStart = testStart,
    .testContinue = testContinue,
};
