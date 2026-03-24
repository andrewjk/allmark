const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const escapeHtml = @import("../utils/escapeHtml.zig").escapeHtml;
const isEscaped = @import("../utils/isEscaped.zig").isEscaped;
const newNode = @import("../utils/newNode.zig").newNode;
const appendChild = @import("../utils/appendChild.zig").appendChild;

const mvzr = @import("mvzr");

// NOTE: removed non-capturing groups `(?:)`
const LINK_REGEX = "^<(\\s*[a-zA-Z][a-zA-Z0-9+.-]{1,31}:[^<>]*)>";
const EMAIL_REGEX = "^<(\\s*[a-zA-Z0-9.!#$%&'*+\\/=?^_`{|}~-]+@[a-zA-Z0-9]([a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(\\.[a-zA-Z0-9]([a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*\\s*)>";

pub fn testAutolink(state: *InlineParserState, parent: *MarkdownNode) bool {
    if (std.mem.eql(u8, parent.type, "html_block")) {
        return false;
    }

    if (state.i >= state.src.len) return false;

    const char = state.src[state.i];
    if (char == '<' and !isEscaped(state.src, state.i)) {
        const tail = state.src[state.i..];

        const linkRegex = mvzr.compile(LINK_REGEX) orelse return false;
        const linkMatch = linkRegex.match(tail);

        if (linkMatch != null and linkMatch.?.start == 0) {
            const rawUrl = tail[linkMatch.?.start + 1 .. linkMatch.?.end - 1];

            const hasSpace = for (rawUrl) |c| {
                if (c == ' ' or c == '\t' or c == '\n' or c == '\r') break true;
            } else false;

            if (hasSpace) {
                const text = newNode(state.allocator, "text", false, state.parentIndex + state.i, state.line, 1, "", state.indent, null) catch return false;
                text.*.markup = escapeHtml(state.allocator, tail[0..linkMatch.?.end]) catch return false;
                text.*.markup_allocated = true;
                text.*.length = linkMatch.?.end;

                appendChild(state.allocator, parent, text) catch return false;

                state.i += linkMatch.?.end;

                return true;
            }

            const encodeUri = @import("../utils/encodeUri.zig").encodeUri;
            const html = newNode(state.allocator, "html_span", false, state.parentIndex + state.i, state.line, 1, "", state.indent, null) catch return false;

            const escapedUrl = escapeHtml(state.allocator, rawUrl) catch return false;
            const encoded_url = encodeUri(state.allocator, escapedUrl) catch return false;
            const content = std.fmt.allocPrint(state.allocator, "<a href=\"{s}\">{s}</a>", .{ encoded_url, escapedUrl }) catch return false;
            state.allocator.free(escapedUrl);
            state.allocator.free(encoded_url);
            html.*.content = content;
            html.*.content_allocated = true;
            html.*.length = linkMatch.?.end;

            appendChild(state.allocator, parent, html) catch return false;

            state.i += linkMatch.?.end;

            return true;
        }

        const emailRegex = mvzr.compile(EMAIL_REGEX) orelse return false;
        const emailMatch = emailRegex.match(tail);

        if (emailMatch != null and emailMatch.?.start == 0) {
            const rawUrl = tail[emailMatch.?.start + 1 .. emailMatch.?.end - 1];

            const hasSpace = for (rawUrl) |c| {
                if (c == ' ' or c == '\t' or c == '\n' or c == '\r') break true;
            } else false;

            if (hasSpace) {
                const text = newNode(state.allocator, "text", false, state.parentIndex + state.i, state.line, 1, "", state.indent, null) catch return false;
                text.*.markup = escapeHtml(state.allocator, tail[0..emailMatch.?.end]) catch return false;
                text.*.markup_allocated = true;
                text.*.length = emailMatch.?.end;

                appendChild(state.allocator, parent, text) catch return false;

                state.i += emailMatch.?.end;

                return true;
            }

            const encodeUri = @import("../utils/encodeUri.zig").encodeUri;
            const html = newNode(state.allocator, "html_span", false, state.parentIndex + state.i, state.line, 1, "", state.indent, null) catch return false;

            const escapedUrl = escapeHtml(state.allocator, rawUrl) catch return false;
            const encoded_url = encodeUri(state.allocator, escapedUrl) catch return false;
            const content = std.fmt.allocPrint(state.allocator, "<a href=\"mailto:{s}\">{s}</a>", .{ encoded_url, escapedUrl }) catch return false;
            state.allocator.free(escapedUrl);
            state.allocator.free(encoded_url);
            html.*.content = content;
            html.*.content_allocated = true;
            html.*.length = emailMatch.?.end;

            appendChild(state.allocator, parent, html) catch return false;

            state.i += emailMatch.?.end;

            return true;
        }
    }

    return false;
}

pub const autolinkRule = InlineRule{
    .name = "autolink",
    .@"test" = testAutolink,
};
