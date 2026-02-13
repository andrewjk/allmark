const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const isNewLine = @import("../utils/isNewLine.zig").isNewLine;
const newNode = @import("../utils/newNode.zig").newNode;

pub fn testLineBreak(state: *InlineParserState, parent: *MarkdownNode) bool {
    const char = if (state.i < state.src.len) state.src[state.i] else 0;
    if (char == ' ') {
        var end = state.i;
        var i = state.i + 1;
        while (i < state.src.len) {
            if (isNewLine(&.{state.src[i]})) {
                end = i;
                break;
            } else if (state.src[i] == ' ') {
                i += 1;
                continue;
            } else {
                return false;
            }
            i += 1;
        }

        if (end - state.i >= 2) {
            const html = newNode(state.allocator, "html_span", false, state.i, state.line, 1, "", state.indent, null) catch unreachable;
            html.*.content = "<br />\n";
            const children = parent.children orelse {
                parent.children = std.ArrayList(*MarkdownNode).initCapacity(state.allocator, 1) catch unreachable;
                parent.children.?.appendAssumeCapacity(html);
                state.i = end + 1;
                return true;
            };
            children.append(html) catch unreachable;
            state.i = end + 1;
            return true;
        }
    }

    return false;
}

pub const lineBreakRule = InlineRule{
    .name = "line_break",
    .@"test" = testLineBreak,
};
