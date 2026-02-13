const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const isAlphaNumeric = @import("../utils/isAlphaNumeric.zig").isAlphaNumeric;
const isNewLine = @import("../utils/isNewLine.zig").isNewLine;
const newNode = @import("../utils/newNode.zig").newNode;

pub fn testText(state: *InlineParserState, parent: *MarkdownNode) bool {
    if (state.i >= state.src.len) return false;

    const char = state.src[state.i];

    var lastNode: *MarkdownNode = undefined;
    if (parent.children == null or parent.children.?.items.len == 0) {
        lastNode = newNode(state.allocator, "text", false, state.i, state.line, 1, "", 0, null) catch return false;
        if (parent.children == null) {
            parent.children = std.ArrayList(*MarkdownNode).initCapacity(state.allocator, 1) catch return false;
            parent.children.?.appendAssumeCapacity(lastNode);
        } else {
            parent.children.?.append(lastNode) catch return false;
        }
    } else {
        lastNode = parent.children.?.items[parent.children.?.items.len - 1];
        if (!std.mem.eql(u8, lastNode.type, "text")) {
            lastNode = newNode(state.allocator, "text", false, state.i, state.line, 1, "", 0, null) catch return false;
            parent.children.?.append(lastNode) catch return false;
        } else if (isNewLine(&.{char})) {
            var trimmed = lastNode.markup;
            while (trimmed.len > 0 and (trimmed[trimmed.len - 1] == ' ' or trimmed[trimmed.len - 1] == '\t')) {
                trimmed = trimmed[0 .. trimmed.len - 1];
            }
            lastNode.*.markup = trimmed;
        }
    }

    if (isAlphaNumeric(state.src[state.i])) {
        const start = state.i;
        state.i += 1;
        while (state.i < state.src.len and isAlphaNumeric(state.src[state.i])) {
            state.i += 1;
        }
        const slice = state.src[start..state.i];
        var new_markup = std.ArrayList(u8).initCapacity(state.allocator, lastNode.markup.len + slice.len) catch return false;
        defer new_markup.deinit();
        new_markup.appendSlice(lastNode.markup) catch return false;
        new_markup.appendSlice(slice) catch return false;
        state.allocator.free(lastNode.markup);
        lastNode.*.markup = new_markup.toOwnedSlice() catch return false;
    } else {
        state.i += 1;
        var new_markup = std.ArrayList(u8).initCapacity(state.allocator, lastNode.markup.len + 1) catch return false;
        defer new_markup.deinit();
        new_markup.appendSlice(lastNode.markup) catch return false;
        new_markup.append(char) catch return false;
        state.allocator.free(lastNode.markup);
        lastNode.*.markup = new_markup.toOwnedSlice() catch return false;
    }

    return true;
}

pub const textRule = InlineRule{
    .name = "text",
    .@"test" = testText,
};
