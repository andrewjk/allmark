const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const isAlphaNumeric = @import("../utils/isAlphaNumeric.zig").isAlphaNumeric;
const isNewLine = @import("../utils/isNewLine.zig").isNewLine;
const isSpace = @import("../utils/isSpace.zig").isSpace;
const newText = @import("../utils/newText.zig").newText;
const appendChild = @import("../utils/appendChild.zig").appendChild;

pub fn testText(state: *InlineParserState, parent: *MarkdownNode) bool {
    if (state.i >= state.src.len) return false;

    const char = state.src[state.i];

    var lastNode: *MarkdownNode = undefined;
    if (parent.children == null or parent.children.?.len == 0) {
        lastNode = newText(state.allocator, state.parentIndex + state.i, state.line, "", 0) catch return false;
        appendChild(state.allocator, parent, lastNode) catch return false;
    } else {
        lastNode = parent.children.?[parent.children.?.len - 1];
        if (!std.mem.eql(u8, lastNode.type, "text")) {
            lastNode = newText(state.allocator, state.parentIndex + state.i, state.line, "", 0) catch return false;
            appendChild(state.allocator, parent, lastNode) catch return false;
        } else if (isNewLine(char)) {
            var end = lastNode.content.len;
            while (end > 0 and isSpace(lastNode.content[end - 1])) {
                end -= 1;
            }
            if (end < lastNode.content.len) {
                const trimmed = state.allocator.dupe(u8, lastNode.content[0..end]) catch unreachable;
                state.allocator.free(lastNode.content);
                lastNode.*.content = trimmed;
                lastNode.*.content_allocated = true;
            }
        }
    }

    if (isAlphaNumeric(state.src[state.i])) {
        const start = state.i;
        state.i += 1;
        while (state.i < state.src.len and isAlphaNumeric(state.src[state.i])) {
            state.i += 1;
        }
        const slice = state.src[start..state.i];
        const old_content = lastNode.content;
        const new_content = state.allocator.alloc(u8, old_content.len + slice.len) catch return false;
        @memcpy(new_content[0..old_content.len], old_content);
        @memcpy(new_content[old_content.len..], slice);
        if (lastNode.content_allocated) {
            state.allocator.free(old_content);
        }
        lastNode.*.content = new_content;
        lastNode.*.content_allocated = true;
    } else {
        state.i += 1;
        const old_content = lastNode.content;
        const new_content = state.allocator.alloc(u8, old_content.len + 1) catch return false;
        @memcpy(new_content[0..old_content.len], old_content);
        new_content[old_content.len] = char;
        if (lastNode.content_allocated) {
            state.allocator.free(old_content);
        }
        lastNode.*.content = new_content;
        lastNode.*.content_allocated = true;
    }
    lastNode.*.length = lastNode.content.len;

    return true;
}

pub const textRule = InlineRule{
    .name = "text",
    .@"test" = testText,
};
