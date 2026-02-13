const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const escapeHtml = @import("../utils/escapeHtml.zig").escapeHtml;
const isAlphaNumeric = @import("../utils/isAlphaNumeric.zig").isAlphaNumeric;
const isEscaped = @import("../utils/isEscaped.zig").isEscaped;
const newNode = @import("../utils/newNode.zig").newNode;

const mvzr = @import("mvzr");

const URL_REGEX = "www\\.([a-z0-9_-](?:\\.)+[a-z0-9-](?:\\.){0,2}[^\\s<]*)";
const EXT_URL_REGEX = "((https*|ftp)://([a-z0-9_-](?:\\.)+[a-z0-9-](?:\\.){0,2}[^\\s<]*))";
const EXT_EMAIL_REGEX = "([a-z0-9._\\-+]+@([a-z0-9._\\-+]+\\.)+)";
const EXT_XMPP_REGEX = "((mailto|xmpp):[a-z0-9._\\-+]+@([a-z0-9._\\-+]+\\.)+(/[a-z0-9@.]+){0,1})";

const TRAILING_PUNCTUATION = "[?!.,:*_~]$";
const TRAILING_ENTITY = "&[a-z0-9]+;$";

pub fn testAutolink(state: *InlineParserState, parent: *MarkdownNode) bool {
    if (std.mem.eql(u8, parent.type, "html_block")) {
        return false;
    }

    if (state.i >= state.src.len) return false;

    const char = state.src[state.i];
    if (!isEscaped(state.src, state.i)) {
        if (char == 'w') {
            const tail = state.src[state.i..];

            const urlRegex = mvzr.compile(URL_REGEX) catch return false;
            const urlMatch = urlRegex.match(tail) catch null;

            if (urlMatch != null and urlMatch.?.start == 0) {
                var url = tail[0..urlMatch.?.end];

                const hasSpace = for (url) |c| {
                    if (std.ascii.isSpace(c)) break true;
                } else false;

                if (hasSpace) {
                    const text = newNode(state.allocator, "text", false, state.i, state.line, 1, "", state.indent, null) catch return false;
                    text.*.markup = try escapeHtml(state.allocator, tail[0..urlMatch.?.end]);
                    defer state.allocator.free(text.markup);

                    if (parent.children == null) {
                        parent.children = std.ArrayList(*MarkdownNode).initCapacity(state.allocator, 1) catch return false;
                        parent.children.?.appendAssumeCapacity(text);
                    } else {
                        parent.children.?.append(text) catch return false;
                    }

                    state.i += urlMatch.?.end;

                    return true;
                }

                url = try extendedValidation(state.allocator, url);
                defer state.allocator.free(url);
                const escapedUrl = try escapeHtml(state.allocator, url);
                defer state.allocator.free(escapedUrl);

                const html = newNode(state.allocator, "html_span", false, state.i, state.line, 1, "", state.indent, null) catch return false;

                var href = std.ArrayList(u8).initCapacity(state.allocator, url.len + 14) catch return false;
                defer href.deinit();
                try href.appendSlice("<a href=\"http://");
                try href.appendSlice(try escapeHtml(state.allocator, try std.UriComponent.encode(state.allocator, url)));
                try href.appendSlice("\">");
                try href.appendSlice(escapedUrl);
                try href.appendSlice("</a>");

                html.*.content = href.toOwnedSlice() catch return false;

                if (parent.children == null) {
                    parent.children = std.ArrayList(*MarkdownNode).initCapacity(state.allocator, 1) catch return false;
                    parent.children.?.appendAssumeCapacity(html);
                } else {
                    parent.children.?.append(html) catch return false;
                }

                state.i += url.len;

                return true;
            }
        }

        if (char == 'h' or char == 'f') {
            const tail = state.src[state.i..];

            const urlRegex = mvzr.compile(EXT_URL_REGEX) catch return false;
            const urlMatch = urlRegex.match(tail) catch null;

            if (urlMatch != null and urlMatch.?.start == 0) {
                var url = tail[0..urlMatch.?.end];

                const hasSpace = for (url) |c| {
                    if (std.ascii.isSpace(c)) break true;
                } else false;

                if (hasSpace) {
                    const text = newNode(state.allocator, "text", false, state.i, state.line, 1, "", state.indent, null) catch return false;
                    text.*.markup = try escapeHtml(state.allocator, tail[0..urlMatch.?.end]);
                    defer state.allocator.free(text.markup);

                    if (parent.children == null) {
                        parent.children = std.ArrayList(*MarkdownNode).initCapacity(state.allocator, 1) catch return false;
                        parent.children.?.appendAssumeCapacity(text);
                    } else {
                        parent.children.?.append(text) catch return false;
                    }

                    state.i += urlMatch.?.end;

                    return true;
                }

                url = try extendedValidation(state.allocator, url);
                defer state.allocator.free(url);
                const escapedUrl = try escapeHtml(state.allocator, url);
                defer state.allocator.free(escapedUrl);

                const html = newNode(state.allocator, "html_span", false, state.i, state.line, 1, "", state.indent, null) catch return false;

                var href = std.ArrayList(u8).initCapacity(state.allocator, url.len + 10) catch return false;
                defer href.deinit();
                try href.appendSlice("<a href=\"");
                try href.appendSlice(try escapeHtml(state.allocator, try std.UriComponent.encode(state.allocator, url)));
                try href.appendSlice("\">");
                try href.appendSlice(escapedUrl);
                try href.appendSlice("</a>");

                html.*.content = href.toOwnedSlice() catch return false;

                if (parent.children == null) {
                    parent.children = std.ArrayList(*MarkdownNode).initCapacity(state.allocator, 1) catch return false;
                    parent.children.?.appendAssumeCapacity(html);
                } else {
                    parent.children.?.append(html) catch return false;
                }

                state.i += url.len;

                return true;
            }
        }

        if (isAlphaNumeric(state.src[state.i])) {
            const tail = state.src[state.i..];

            const emailRegex = mvzr.compile(EXT_EMAIL_REGEX) catch return false;
            const emailMatch = emailRegex.match(tail) catch null;

            if (emailMatch != null and emailMatch.?.start == 0) {
                var url = tail[0..emailMatch.?.end];

                if (url.len > 0 and (url[url.len - 1] == '-' or url[url.len - 1] == '_' or
                    std.mem.indexOf(u8, url, "+") != null and
                        std.mem.indexOf(u8, url, "@") != null and
                        std.mem.indexOf(u8, url, "+") orelse 0 > std.mem.indexOf(u8, url, "@") orelse 0))
                {
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

                while (url.len > 0 and url[url.len - 1] == '.') {
                    url = url[0 .. url.len - 1];
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

                state.i += url.len;

                return true;
            }
        }

        if (char == 'm' or char == 'x') {
            const tail = state.src[state.i..];

            const emailRegex = mvzr.compile(EXT_XMPP_REGEX) catch return false;
            const emailMatch = emailRegex.match(tail) catch null;

            if (emailMatch != null and emailMatch.?.start == 0) {
                var url = tail[0..emailMatch.?.end];

                if (url.len > 0 and (url[url.len - 1] == '-' or url[url.len - 1] == '_' or
                    std.mem.indexOf(u8, url, "+") != null and
                        std.mem.indexOf(u8, url, "@") != null and
                        std.mem.indexOf(u8, url, "+") orelse 0 > std.mem.indexOf(u8, url, "@") orelse 0))
                {
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

                while (url.len > 0 and url[url.len - 1] == '.') {
                    url = url[0 .. url.len - 1];
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

                state.i += url.len;

                return true;
            }
        }
    }

    return false;
}

fn extendedValidation(allocator: std.mem.Allocator, url: []const u8) ![]const u8 {
    var result = std.ArrayList(u8).initCapacity(allocator, url.len) catch unreachable;
    defer result.deinit();

    for (url) |c| {
        if (!std.mem.indexOf(u8, "?!.,:*_~", &.{c})) |_| {
            try result.append(c);
        }
    }

    const trimmed = try result.toOwnedSlice();
    defer allocator.free(trimmed);

    if (trimmed.len > 0 and trimmed[trimmed.len - 1] == ')') {
        var trimCount: usize = 0;
        var i = trimmed.len;
        var countingUp = true;
        while (i > 0) : (i -= 1) {
            if (countingUp) {
                if (trimmed[i - 1] == ')') {
                    trimCount += 1;
                } else {
                    countingUp = false;
                }
            } else {
                if (trimmed[i - 1] == '(') {
                    if (trimCount > 0) trimCount -= 1;
                }
                if (trimCount == 0) break;
            }
        }

        if (trimCount > 0 and trimCount < trimmed.len) {
            return allocator.dupe(u8, trimmed[0 .. trimmed.len - trimCount]);
        }
    }

    return allocator.dupe(u8, trimmed);
}

pub const extendedAutolinkRule = InlineRule{
    .name = "extended_autolink",
    .@"test" = testAutolink,
};
