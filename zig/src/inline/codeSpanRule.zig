const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const addMarkupAsText = @import("../utils/addMarkupAsText.zig").addMarkupAsText;
const isSpace = @import("../utils/isSpace.zig").isSpace;
const newInline = @import("../utils/newInline.zig").newInline;
const newText = @import("../utils/newText.zig").newText;
const skipSpaces = @import("../utils/skipSpaces.zig").skipSpaces;
const appendChild = @import("../utils/appendChild.zig").appendChild;

pub fn testCodeSpan(state: *InlineParserState, parent: *MarkdownNode) bool {
    if (state.i >= state.src.len) return false;

    const char = state.src[state.i];
    if (!state.isEscaped and char == '`') {
        var openMatched: usize = 1;
        var openEnd = state.i + 1;
        while (openEnd < state.src.len and state.src[openEnd] == char) : (openEnd += 1) {
            openMatched += 1;
        }

        var markup = std.ArrayList(u8).initCapacity(state.allocator, openMatched) catch unreachable;
        defer markup.deinit(state.allocator);
        if (openMatched > 0) {
            // First set the length, then fill with backticks
            markup.items.len = openMatched;
            @memset(markup.items[0..openMatched], '`');
        }

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
            defer new_content.deinit(state.allocator);
            for (content) |c| {
                if (c == '\r' or c == '\n') {
                    new_content.append(state.allocator, ' ') catch unreachable;
                } else {
                    new_content.append(state.allocator, c) catch unreachable;
                }
            }
            content = new_content.items;

            const hasNonSpace = for (content) |c| {
                if (!isSpace(c)) break true;
            } else false;

            if (hasNonSpace and content.len >= 2 and isSpace(content[0]) and isSpace(content[content.len - 1])) {
                content = content[1 .. content.len - 1];
            }

            const text = newText(state.allocator, state.parentIndex + state.i, state.line, content, 0) catch unreachable;
            text.*.length = content.len;
            const children_slice = state.allocator.alloc(*MarkdownNode, 1) catch unreachable;
            children_slice[0] = text;
            const code = newInline(state.allocator, "code_span", state.parentIndex + state.i - openMatched, state.line, markup.items, 0) catch unreachable;
            code.*.children = children_slice;
            code.*.length = closeEnd - (state.i - openMatched);

            appendChild(state.allocator, parent, code) catch unreachable;

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
