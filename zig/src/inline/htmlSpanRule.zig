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
const isEscaped = @import("../utils/isEscaped.zig").isEscaped;
const newNode = @import("../utils/newNode.zig").newNode;

const mvzr = @import("mvzr");

pub fn testHtmlSpan(state: *InlineParserState, parent: *MarkdownNode) bool {
    if (std.mem.eql(u8, parent.type, "html_block")) {
        return false;
    }

    if (state.i >= state.src.len) return false;

    const char = state.src[state.i];
    if (char == '<' and !isEscaped(state.src, state.i)) {
        const tail = state.src[state.i..];
        const pattern = "(?:" ++ OPEN_TAG ++ "|" ++ CLOSE_TAG ++ "|" ++ COMMENT ++ "|" ++ INSTRUCTION ++ "|" ++ DECLARATION ++ "|" ++ CDATA ++ ")";

        const regex = mvzr.compile(pattern) catch return false;
        const match = regex.match(tail) catch return false;

        if (match.start != 0) {
            return false;
        }

        const content = tail[0..match.end];
        const html = newNode(state.allocator, "html_span", false, state.i, state.line, 1, "", state.indent, null) catch return false;
        html.*.content = content;

        if (parent.children == null) {
            parent.children = std.ArrayList(*MarkdownNode).initCapacity(state.allocator, 1) catch return false;
            parent.children.?.appendAssumeCapacity(html);
        } else {
            parent.children.?.append(html) catch return false;
        }

        state.i += content.len;

        return true;
    }

    return false;
}

pub const htmlSpanRule = InlineRule{
    .name = "html_span",
    .@"test" = testHtmlSpan,
};
