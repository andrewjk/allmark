const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const addMarkupAsText = @import("../utils/addMarkupAsText.zig").addMarkupAsText;
const isEscaped = @import("../utils/isEscaped.zig").isEscaped;
const isSpace = @import("../utils/isSpace.zig").isSpace;
const newNode = @import("../utils/newNode.zig").newNode;
const skipSpaces = @import("../utils/skipSpaces.zig").skipSpaces;

pub fn testCodeSpan(state: *InlineParserState, parent: *MarkdownNode) bool {
    if (state.i >= state.src.len) return false;

    const char = state.src[state.i];
    if (char == '`' and !isEscaped(state.src, state.i)) {
        var openMatched: usize = 1;
        var openEnd = state.i + 1;
        while (openEnd < state.src.len and state.src[openEnd] == char) : (openEnd += 1) {
            openMatched += 1;
        }

        var markup = std.ArrayList(u8).initCapacity(state.allocator, openMatched) catch unreachable;
        defer markup.deinit();
        @memset(markup.items[0..openMatched], '`');
        markup.items.len = openMatched;

        var closeEnd = state.i + openMatched;
        closeEnd = skipSpaces(state.src, closeEnd);
        var closeMatched: usize = 0;
        while (closeEnd < state.src.len) {
            if (state.src[closeEnd] == char) {
                while (closeEnd < state.src.len and state.src[closeEnd] == char) : (closeEnd += 1) {
                    closeMatched += 1;
                }
                if (closeMatched == openMatched) {
                    break;
                }
                closeMatched = 0;
            } else {
                closeEnd += 1;
            }
        }

        if (closeMatched == openMatched) {
            state.i += openMatched;

            var content = state.src[state.i .. closeEnd - closeMatched];

            var new_content = std.ArrayList(u8).initCapacity(state.allocator, content.len) catch unreachable;
            defer new_content.deinit();
            for (content) |c| {
                if (c == '\r' or c == '\n') {
                    try new_content.append(' ');
                } else {
                    try new_content.append(c);
                }
            }
            content = new_content.items;

            const hasNonSpace = for (content) |c| {
                if (isSpace(c) == false) break true;
            } else false;

            if (hasNonSpace and isSpace(content[0]) and isSpace(content[content.len - 1])) {
                content = content[1 .. content.len - 1];
            }

            const text = newNode(state.allocator, "text", false, state.i, state.line, 1, content, 0, null) catch unreachable;
            const code = newNode(state.allocator, "code_span", false, state.i, state.line, 1, markup.items, 0, &[_]*MarkdownNode{text}) catch unreachable;

            if (parent.children == null) {
                parent.children = std.ArrayList(*MarkdownNode).initCapacity(state.allocator, 1) catch unreachable;
                parent.children.?.appendAssumeCapacity(code);
            } else {
                parent.children.?.append(code) catch unreachable;
            }

            state.i = closeEnd;

            return true;
        }

        addMarkupAsText(state.allocator, markup.items, state, parent) catch unreachable;

        return true;
    }

    return false;
}

pub const codeSpanRule = InlineRule{
    .name = "code_span",
    .@"test" = testCodeSpan,
};
