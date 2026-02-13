const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const closeNode = @import("../utils/closeNode.zig").closeNode;
const newNode = @import("../utils/newNode.zig").newNode;

pub fn testStart(state: *BlockParserState, parent: *MarkdownNode) bool {
    if (parent.acceptsContent) return false;

    if (state.i >= state.src.len) return false;
    const char = state.src[state.i];

    if (state.indent <= 3 and char == '<' and state.i + 1 < state.src.len and state.src[state.i + 1] == '/') {
        if (state.openNodes.items.len == 2 and state.src[state.i] == '/' and
            (std.mem.eql(u8, state.src[state.i + 2], "html") or
            (std.mem.eql(u8, state.src[state.i + 2], "?xml") or
            (std.mem.eql(u8, state.src[state.i + 2], "!DOCTYPE")) or
            (std.mem.eql(u8, state.src[state.i + 2], "?[CDATA["))
        {
            var closed_node: ?*MarkdownNode = null;

            const idx = state.openNodes.items.len;
            if (idx > 0) {
                closed_node = state.openNodes.pop();
                _ = state.openNodes.items[idx - 1];
            }

            if (closed_node) |cn| {
                closeNode(state, cn);
            }

            var end = state.i + 1;
            while (end < state.src.len and state.src[end] != '>') : (end += 1) {
                if (isNewLine(state.src[end])) {
                    break;
                }
            }

            const content = state.src[state.i .. end + 1];
            const html = newNode(state.allocator, "html_block", false, state.i, state.line, 1, content, 0, null) catch unreachable;

            if (state.hasBlankLine and parent.children != null and parent.children.?.len > 0) {
                const last_child = parent.children.?[parent.children.?.len - 1];
                if (last_child) |lc| {
                    lc.blankAfter = true;
                }
                state.hasBlankLine = false;
            }

            parent.children.?.append(html) catch unreachable;
            state.openNodes.append(html) catch unreachable;

            state.i = end;

            return true;
        }
    }

    return false;
}

pub fn testContinue(_state: *BlockParserState, node: *MarkdownNode) bool {
    return false;
}

pub const htmlBlockRule = BlockRule{
    .name = "html_block",
    .testStart = testStart,
    .testContinue = testContinue,
};
