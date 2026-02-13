const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const isAlphaNumeric = @import("../utils/isAlphaNumeric.zig").isAlphaNumeric;
const isNewLine = @import("../utils/isNewLine.zig").isNewLine;
const newNode = @import("../utils/newNode.zig").newNode;
const appendChild = @import("../utils/appendChild.zig").appendChild;

pub fn testText(state: *InlineParserState, parent: *MarkdownNode) bool {
    if (state.i >= state.src.len) return false;

    const char = state.src[state.i];

    var lastNode: *MarkdownNode = undefined;
    if (parent.children == null or parent.children.?.len == 0) {
        lastNode = newNode(state.allocator, "text", false, state.i, state.line, 1, "", 0, null) catch return false;
        appendChild(state.allocator, parent, lastNode) catch return false;
    } else {
        lastNode = parent.children.?[parent.children.?.len - 1];
        if (!std.mem.eql(u8, lastNode.type, "text")) {
            lastNode = newNode(state.allocator, "text", false, state.i, state.line, 1, "", 0, null) catch return false;
            appendChild(state.allocator, parent, lastNode) catch return false;
        } else if (isNewLine(char)) {
            var end = lastNode.markup.len;
            while (end > 0 and (lastNode.markup[end - 1] == ' ' or lastNode.markup[end - 1] == '\t')) {
                end -= 1;
            }
            if (end < lastNode.markup.len) {
                const trimmed = state.allocator.dupe(u8, lastNode.markup[0..end]) catch unreachable;
                state.allocator.free(lastNode.markup);
                lastNode.*.markup = trimmed;
                lastNode.*.markup_allocated = true;
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
        const old_markup = lastNode.markup;
        const new_markup = state.allocator.alloc(u8, old_markup.len + slice.len) catch return false;
        @memcpy(new_markup[0..old_markup.len], old_markup);
        @memcpy(new_markup[old_markup.len..], slice);
        if (lastNode.markup_allocated) {
            state.allocator.free(old_markup);
        }
        lastNode.*.markup = new_markup;
        lastNode.*.markup_allocated = true;
    } else {
        state.i += 1;
        const old_markup = lastNode.markup;
        const new_markup = state.allocator.alloc(u8, old_markup.len + 1) catch return false;
        @memcpy(new_markup[0..old_markup.len], old_markup);
        new_markup[old_markup.len] = char;
        if (lastNode.markup_allocated) {
            state.allocator.free(old_markup);
        }
        lastNode.*.markup = new_markup;
        lastNode.*.markup_allocated = true;
    }

    return true;
}

pub const textRule = InlineRule{
    .name = "text",
    .@"test" = testText,
};
