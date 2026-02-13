const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const escapeHtml = @import("../utils/escapeHtml.zig").escapeHtml;
const isEscaped = @import("../utils/isEscaped.zig").isEscaped;
const newNode = @import("../utils/newNode.zig").newNode;

const mvzr = @import("mvzr");

const LINK_REGEX = "<(\\s*[a-z][a-z0-9+.-]{1,31}:[^<>]*)>";
const EMAIL_REGEX = "<(\\s*[a-z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-z0-9](?:[a-z0-9-]{0,61}[a-z0-9])?(?:\\.[a-z0-9](?:[a-z0-9-]{0,61}[a-z0-9])?)*\\s*)>";

pub fn testAutolink(state: *InlineParserState, parent: *MarkdownNode) bool {
    if (std.mem.eql(u8, parent.type, "html_block")) {
        return false;
    }

    if (state.i >= state.src.len) return false;

    const char = state.src[state.i];
    if (char == '<' and !isEscaped(state.src, state.i)) {
        const tail = state.src[state.i..];

        const linkRegex = mvzr.compile(LINK_REGEX) catch return false;
        const linkMatch = linkRegex.match(tail) catch null;

        if (linkMatch != null and linkMatch.?.start == 0) {
            const url = try escapeHtml(state.allocator, tail[linkMatch.?.end - linkMatch.?.start - 1 .. linkMatch.?.end - 1]);
            defer state.allocator.free(url);

            const hasSpace = for (url) |c| {
                if (std.ascii.isSpace(c)) break true;
            } else false;

            if (hasSpace) {
                const text = newNode(state.allocator, "text", false, state.i, state.line, 1, "", state.indent, null) catch return false;
                text.*.markup = try escapeHtml(state.allocator, tail[0..linkMatch.?.end]);
                defer state.allocator.free(text.markup);

                if (parent.children == null) {
                    parent.children = std.ArrayList(*MarkdownNode).initCapacity(state.allocator, 1) catch return false;
                    parent.children.?.appendAssumeCapacity(text);
                } else {
                    parent.children.?.append(text) catch return false;
                }

                state.i += linkMatch.?.end;

                return true;
            }

            const html = newNode(state.allocator, "html_span", false, state.i, state.line, 1, "", state.indent, null) catch return false;

            var href = std.ArrayList(u8).initCapacity(state.allocator, url.len + 10) catch return false;
            defer href.deinit();
            try href.appendSlice("<a href=\"");
            try href.appendSlice(try escapeHtml(state.allocator, try std.UriComponent.encode(state.allocator, url)));
            try href.appendSlice("\">");
            try href.appendSlice(url);
            try href.appendSlice("</a>");

            html.*.content = href.toOwnedSlice() catch return false;

            if (parent.children == null) {
                parent.children = std.ArrayList(*MarkdownNode).initCapacity(state.allocator, 1) catch return false;
                parent.children.?.appendAssumeCapacity(html);
            } else {
                parent.children.?.append(html) catch return false;
            }

            state.i += linkMatch.?.end;

            return true;
        }

        const emailRegex = mvzr.compile(EMAIL_REGEX) catch return false;
        const emailMatch = emailRegex.match(tail) catch null;

        if (emailMatch != null and emailMatch.?.start == 0) {
            const url = try escapeHtml(state.allocator, tail[emailMatch.?.end - emailMatch.?.start - 1 .. emailMatch.?.end - 1]);
            defer state.allocator.free(url);

            const hasSpace = for (url) |c| {
                if (std.ascii.isSpace(c)) break true;
            } else false;

            if (hasSpace) {
                const text = newNode(state.allocator, "text", false, state.i, state.line, 1, "", state.indent, null) catch return false;
                text.*.markup = try escapeHtml(state.allocator, tail[0..emailMatch.?.end]);
                defer state.allocator.free(text.markup);

                if (parent.children == null) {
                    parent.children = std.ArrayList(*MarkdownNode).initCapacity(state.allocator, 1) catch return false;
                    parent.children.?.appendAssumeCapacity(text);
                } else {
                    parent.children.?.append(text) catch return false;
                }

                state.i += emailMatch.?.end;

                return true;
            }

            const html = newNode(state.allocator, "html_span", false, state.i, state.line, 1, "", state.indent, null) catch return false;

            var href = std.ArrayList(u8).initCapacity(state.allocator, url.len + 15) catch return false;
            defer href.deinit();
            try href.appendSlice("<a href=\"mailto:");
            try href.appendSlice(try escapeHtml(state.allocator, try std.UriComponent.encode(state.allocator, url)));
            try href.appendSlice("\">");
            try href.appendSlice(url);
            try href.appendSlice("</a>");

            html.*.content = href.toOwnedSlice() catch return false;

            if (parent.children == null) {
                parent.children = std.ArrayList(*MarkdownNode).initCapacity(state.allocator, 1) catch return false;
                parent.children.?.appendAssumeCapacity(html);
            } else {
                parent.children.?.append(html) catch return false;
            }

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
