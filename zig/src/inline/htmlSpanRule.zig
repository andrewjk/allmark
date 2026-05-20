const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const OPEN_TAG = @import("../utils/htmlPatterns.zig").OPEN_TAG;
const CLOSE_TAG = @import("../utils/htmlPatterns.zig").CLOSE_TAG;
const COMMENT = @import("../utils/htmlPatterns.zig").COMMENT;
const INSTRUCTION = @import("../utils/htmlPatterns.zig").INSTRUCTION;
const DECLARATION = @import("../utils/htmlPatterns.zig").DECLARATION;
const CDATA = @import("../utils/htmlPatterns.zig").CDATA;
const newInline = @import("../utils/newInline.zig").newInline;
const appendChild = @import("../utils/appendChild.zig").appendChild;

const mvzr = @import("mvzr");

// NOTE: removed non-capturing groups `(?:)`
const pattern = "^(" ++ OPEN_TAG ++ "|" ++ CLOSE_TAG ++ "|" ++ COMMENT ++ "|" ++ INSTRUCTION ++ "|" ++ DECLARATION ++ "|" ++ CDATA ++ ")";
const ProperlySizedRegex = mvzr.SizedRegex(119, 11);

pub fn testHtmlSpan(state: *InlineParserState, parent: *MarkdownNode) bool {
    if (state.i >= state.src.len) return false;

    const char = state.src[state.i];
    if (!state.isEscaped and char == '<') {
        const tail = state.src[state.i..];

        const regex = ProperlySizedRegex.compile(pattern) orelse return false;
        const match = regex.match(tail);

        if (match == null or match.?.start != 0) {
            return false;
        }

        const content = match.?.slice;
        const html = newInline(state.allocator, "html_span", state.parentIndex + state.i, state.line, "", state.indent) catch return false;
        html.*.content = state.allocator.dupe(u8, content) catch return false;
        html.*.content_allocated = true;
        html.*.length = content.len;

        appendChild(state.allocator, parent, html) catch return false;

        state.i += match.?.end;

        return true;
    }

    return false;
}

pub const htmlSpanRule = InlineRule{
    .name = "html_span",
    .@"test" = testHtmlSpan,
};
