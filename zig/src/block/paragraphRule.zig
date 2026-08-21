const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const getLineEnding = @import("../utils/getLineEnding.zig").getLineEnding;
const isSpaceFn = @import("../utils/isSpace.zig").isSpace;
const newBlock = @import("../utils/newBlock.zig").newBlock;
const appendChild = @import("../utils/appendChild.zig").appendChild;

pub fn testStart(state: *BlockParserState, parent: *MarkdownNode, end_of_line: usize) bool {
    if (parent.acceptsContent) {
        return false;
    }

    if (std.mem.eql(u8, parent.type, "paragraph") and !parent.blankAfter) {
        return false;
    }

    const content_src = state.src[state.i..end_of_line];
    const line_ending = getLineEnding(state, end_of_line);
    const content = state.allocator.alloc(u8, content_src.len + line_ending.len) catch unreachable;
    @memcpy(content[0..content_src.len], content_src);
    @memcpy(content[content_src.len..], line_ending);

    const hasNonSpace = for (content_src) |c| {
        if (!isSpaceFn(c)) break true;
    } else false;

    if (!hasNonSpace) {
        state.allocator.free(content);
        return false;
    }

    const paragraph = newBlock(state.allocator, "paragraph", state.i, state.line, "", 0) catch unreachable;
    paragraph.*.content = content;
    paragraph.*.content_allocated = true;
    paragraph.*.children = state.allocator.alloc(*MarkdownNode, 0) catch unreachable;

    if (state.hasBlankLine and parent.children != null and parent.children.?.len > 0) {
        const last_child = parent.children.?[parent.children.?.len - 1];
        last_child.blankAfter = true;
        state.hasBlankLine = false;
    }

    appendChild(state.allocator, parent, paragraph) catch unreachable;
    state.openNodes.append(state.allocator, paragraph) catch unreachable;

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
