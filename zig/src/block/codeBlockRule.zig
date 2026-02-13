const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const isNewLine = @import("../utils/isNewLine.zig").isNewLine;
const newNode = @import("../utils/newNode.zig").newNode;

pub fn testStart(state: *BlockParserState, parent: *MarkdownNode) bool {
    if (parent.acceptsContent) return false;

    if (std.mem.eql(u8, parent.type, "paragraph") and !parent.blankAfter) {
        return false;
    }

    if (state.i >= state.src.len) return false;
    const char = state.src[state.i];

    if (state.indent < 4 and !isNewLine(char)) {
        var closed_node: ?*MarkdownNode = null;

        const is_list = std.mem.eql(u8, parent.type, "list_ordered") or std.mem.eql(u8, parent.type, "list_bulleted");
        if (is_list) {
            const idx = state.openNodes.items.len;
            if (idx > 0) {
                closed_node = state.openNodes.pop();
            }
        }

        if (state.maybeContinue) {
            state.maybeContinue = false;
            var i: usize = state.openNodes.items.len;
            while (i > 1) : (i -= 1) {
                const iter_node = state.openNodes.items[i];
                if (iter_node.maybeContinuing) {
                    iter_node.maybeContinuing = false;
                    closed_node = iter_node;
                    state.openNodes.shrinkRetainingCapacity(i);
                    break;
                }
            }
        }

        if (closed_node) |cn| {
            closeNode(state, cn);
        }

        const code_indent = state.indent - 4;
        const code = newNode(state.allocator, "code_block", true, state.i, state.line, 1, "    ", code_indent, null) catch unreachable;
        code.acceptsContent = true;

        if (state.hasBlankLine) {
            if (parent.children) |children| {
                if (children.len > 0) {
                    const last_child = children[children.len - 1];
                    last_child.blankAfter = true;
                }
                state.hasBlankLine = false;
            }
        }

        parent.children.?.append(code) catch unreachable;
        state.openNodes.append(code) catch unreachable;

        state.indent = 0;
        state.hasBlankLine = false;

        return true;
    }

    return false;
}

pub fn testContinue(state: *BlockParserState, node: *MarkdownNode) bool {
    if (state.hasBlankLine and state.indent >= 4) {
        const padding = try std.math.max(state.indent, 4) - 4;
        var old_content = node.content;
        const spaces = try state.allocator.alloc(u8, padding);
        defer state.allocator.free(spaces);
        @memset(spaces, ' ', padding);
        const new_content = try std.fmt.allocPrint(state.allocator, "{s}{s}", .{ old_content, spaces });
        defer state.allocator.free(new_content);

        node.content = new_content;
    }

    if (state.indent >= 4) {
        state.indent -= 4;
        return true;
    }

    if (state.hasBlankLine) {
        return true;
    }

    return false;
}

pub const codeBlockRule = BlockRule{
    .name = "code_block",
    .testStart = testStart,
    .testContinue = testContinue,
};
