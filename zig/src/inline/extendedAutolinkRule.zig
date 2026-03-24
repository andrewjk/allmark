const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const escapeHtml = @import("../utils/escapeHtml.zig").escapeHtml;
const isAlphaNumeric = @import("../utils/isAlphaNumeric.zig").isAlphaNumeric;
const isEscaped = @import("../utils/isEscaped.zig").isEscaped;
const isSpace = @import("../utils/isSpace.zig").isSpace;
const newNode = @import("../utils/newNode.zig").newNode;

const mvzr = @import("mvzr");

// NOTE: removed non-capturing groups `(?:)`
// HACK: `\s` doesn't work??
const URL_REGEX = "^(www\\.([a-zA-Z0-9_-]\\.*)+([a-zA-Z0-9-]\\.*){0,2}[^ <]*)";
const EXT_URL_REGEX = "^((https*|ftp):\\/\\/([a-zA-Z0-9_-]\\.*)+([a-zA-Z0-9-]\\.*){0,2}[^ <]*)";
const EXT_EMAIL_REGEX = "^([a-zA-Z0-9._\\-+]+@([a-zA-Z0-9._\\-+]+\\.*)+)";
const EXT_XMPP_REGEX = "^((mailto|xmpp):[a-zA-Z0-9._\\-+]+@([a-zA-Z0-9._\\-+]+\\.*)+)(\\/[a-zA-Z0-9@.]+){0,1}";

const TRAILING_PUNCTUATION = "[?!.,:*_~]$";
const TRAILING_ENTITY = "&[a-zA-Z0-9]+;$";

pub fn testAutolink(state: *InlineParserState, parent: *MarkdownNode) bool {
    if (std.mem.eql(u8, parent.type, "html_block")) {
        return false;
    }

    if (state.i >= state.src.len) return false;

    const char = state.src[state.i];
    if (!isEscaped(state.src, state.i)) {
        if (char == 'w') {
            const tail = state.src[state.i..];

            const urlRegex = mvzr.compile(URL_REGEX) orelse return false;
            const urlMatch = urlRegex.match(tail) orelse null;

            if (urlMatch != null and urlMatch.?.start == 0) {
                var url = tail[0..urlMatch.?.end];

                const hasSpace = for (url) |c| {
                    if (isSpace(c)) break true;
                } else false;

                if (hasSpace) {
                    const text = newNode(state.allocator, "text", false, state.parentIndex + state.i, state.line, 1, "", state.indent, null) catch return false;
                    text.*.markup = escapeHtml(state.allocator, tail[0..urlMatch.?.end]) catch return false;
                    text.*.markup_allocated = true;
                    text.*.length = urlMatch.?.end;

                    if (parent.children == null) {
                        const children = state.allocator.alloc(*MarkdownNode, 1) catch return false;
                        children[0] = text;
                        parent.children = children;
                    } else {
                        const old_children = parent.children.?;
                        const new_children = state.allocator.alloc(*MarkdownNode, old_children.len + 1) catch return false;
                        std.mem.copyForwards(*MarkdownNode, new_children, old_children);
                        new_children[old_children.len] = text;
                        state.allocator.free(old_children);
                        parent.children = new_children;
                    }

                    state.i += urlMatch.?.end;

                    return true;
                }

                url = extendedValidation(state.allocator, url) catch return false;
                defer state.allocator.free(url);
                const escapedUrl = escapeHtml(state.allocator, url) catch return false;
                defer state.allocator.free(escapedUrl);

                const html = newNode(state.allocator, "html_span", false, state.parentIndex + state.i, state.line, 1, "", state.indent, null) catch return false;

                var href = std.ArrayList(u8).initCapacity(state.allocator, url.len + 14) catch return false;
                defer href.deinit(state.allocator);
                href.appendSlice(state.allocator, "<a href=\"http://") catch return false;
                href.appendSlice(state.allocator, escapedUrl) catch return false;
                href.appendSlice(state.allocator, "\">") catch return false;
                href.appendSlice(state.allocator, escapedUrl) catch return false;
                href.appendSlice(state.allocator, "</a>") catch return false;

                html.*.content = href.toOwnedSlice(state.allocator) catch return false;
                html.*.content_allocated = true;
                html.*.length = url.len;

                if (parent.children == null) {
                    const children = state.allocator.alloc(*MarkdownNode, 1) catch return false;
                    children[0] = html;
                    parent.children = children;
                } else {
                    const old_children = parent.children.?;
                    const new_children = state.allocator.alloc(*MarkdownNode, old_children.len + 1) catch return false;
                    std.mem.copyForwards(*MarkdownNode, new_children, old_children);
                    new_children[old_children.len] = html;
                    state.allocator.free(old_children);
                    parent.children = new_children;
                }

                state.i += url.len;

                return true;
            }
        }

        if (char == 'h' or char == 'f') {
            const tail = state.src[state.i..];

            const urlRegex = mvzr.compile(EXT_URL_REGEX) orelse return false;
            const urlMatch = urlRegex.match(tail) orelse null;

            if (urlMatch != null and urlMatch.?.start == 0) {
                var url = tail[0..urlMatch.?.end];

                const hasSpace = for (url) |c| {
                    if (isSpace(c)) break true;
                } else false;

                if (hasSpace) {
                    const text = newNode(state.allocator, "text", false, state.parentIndex + state.i, state.line, 1, "", state.indent, null) catch return false;
                    text.*.markup = escapeHtml(state.allocator, tail[0..urlMatch.?.end]) catch return false;
                    text.*.markup_allocated = true;
                    text.*.length = urlMatch.?.end;

                    if (parent.children == null) {
                        const children = state.allocator.alloc(*MarkdownNode, 1) catch return false;
                        children[0] = text;
                        parent.children = children;
                    } else {
                        const old_children = parent.children.?;
                        const new_children = state.allocator.alloc(*MarkdownNode, old_children.len + 1) catch return false;
                        std.mem.copyForwards(*MarkdownNode, new_children, old_children);
                        new_children[old_children.len] = text;
                        state.allocator.free(old_children);
                        parent.children = new_children;
                    }

                    state.i += urlMatch.?.end;

                    return true;
                }

                url = extendedValidation(state.allocator, url) catch return false;
                defer state.allocator.free(url);
                const escapedUrl = escapeHtml(state.allocator, url) catch return false;
                defer state.allocator.free(escapedUrl);

                const html = newNode(state.allocator, "html_span", false, state.parentIndex + state.i, state.line, 1, "", state.indent, null) catch return false;

                var href = std.ArrayList(u8).initCapacity(state.allocator, url.len + 10) catch return false;
                defer href.deinit(state.allocator);
                href.appendSlice(state.allocator, "<a href=\"") catch return false;
                href.appendSlice(state.allocator, escapedUrl) catch return false;
                href.appendSlice(state.allocator, "\">") catch return false;
                href.appendSlice(state.allocator, escapedUrl) catch return false;
                href.appendSlice(state.allocator, "</a>") catch return false;

                html.*.content = href.toOwnedSlice(state.allocator) catch return false;
                html.*.content_allocated = true;
                html.*.length = url.len;

                if (parent.children == null) {
                    const children = state.allocator.alloc(*MarkdownNode, 1) catch return false;
                    children[0] = html;
                    parent.children = children;
                } else {
                    const old_children = parent.children.?;
                    const new_children = state.allocator.alloc(*MarkdownNode, old_children.len + 1) catch return false;
                    std.mem.copyForwards(*MarkdownNode, new_children, old_children);
                    new_children[old_children.len] = html;
                    state.allocator.free(old_children);
                    parent.children = new_children;
                }

                state.i += url.len;

                return true;
            }
        }

        if (isAlphaNumeric(state.src[state.i])) {
            const tail = state.src[state.i..];

            const emailRegex = mvzr.compile(EXT_EMAIL_REGEX) orelse return false;
            const emailMatch = emailRegex.match(tail) orelse null;

            if (emailMatch != null and emailMatch.?.start == 0) {
                var url = tail[0..emailMatch.?.end];

                const at_idx = std.mem.indexOf(u8, url, "@");
                const plus_idx = std.mem.indexOf(u8, url, "+");
                const hasPlusAfterAt = at_idx != null and plus_idx != null and plus_idx.? > at_idx.?;
                if (url.len > 0 and (url[url.len - 1] == '-' or url[url.len - 1] == '_' or hasPlusAfterAt)) {
                    const text = newNode(state.allocator, "text", false, state.parentIndex + state.i, state.line, 1, "", state.indent, null) catch return false;
                    text.*.markup = escapeHtml(state.allocator, tail[0..emailMatch.?.end]) catch return false;
                    text.*.markup_allocated = true;
                    text.*.length = emailMatch.?.end;

                    if (parent.children == null) {
                        const children = state.allocator.alloc(*MarkdownNode, 1) catch return false;
                        children[0] = text;
                        parent.children = children;
                    } else {
                        const old_children = parent.children.?;
                        const new_children = state.allocator.alloc(*MarkdownNode, old_children.len + 1) catch return false;
                        std.mem.copyForwards(*MarkdownNode, new_children, old_children);
                        new_children[old_children.len] = text;
                        state.allocator.free(old_children);
                        parent.children = new_children;
                    }

                    state.i += emailMatch.?.end;

                    return true;
                }

                while (url.len > 0 and url[url.len - 1] == '.') {
                    url = url[0 .. url.len - 1];
                }

                const html = newNode(state.allocator, "html_span", false, state.parentIndex + state.i, state.line, 1, "", state.indent, null) catch return false;

                var href = std.ArrayList(u8).initCapacity(state.allocator, url.len + 15) catch return false;
                defer href.deinit(state.allocator);
                href.appendSlice(state.allocator, "<a href=\"mailto:") catch return false;
                const escapedMailUrl = escapeHtml(state.allocator, url) catch return false;
                defer state.allocator.free(escapedMailUrl);
                href.appendSlice(state.allocator, escapedMailUrl) catch return false;
                href.appendSlice(state.allocator, "\">") catch return false;
                href.appendSlice(state.allocator, url) catch return false;
                href.appendSlice(state.allocator, "</a>") catch return false;

                html.*.content = href.toOwnedSlice(state.allocator) catch return false;
                html.*.content_allocated = true;
                html.*.length = url.len;

                if (parent.children == null) {
                    const children = state.allocator.alloc(*MarkdownNode, 1) catch return false;
                    children[0] = html;
                    parent.children = children;
                } else {
                    const old_children = parent.children.?;
                    const new_children = state.allocator.alloc(*MarkdownNode, old_children.len + 1) catch return false;
                    std.mem.copyForwards(*MarkdownNode, new_children, old_children);
                    new_children[old_children.len] = html;
                    state.allocator.free(old_children);
                    parent.children = new_children;
                }

                state.i += url.len;

                return true;
            }
        }

        if (char == 'm' or char == 'x') {
            const tail = state.src[state.i..];

            const emailRegex = mvzr.compile(EXT_XMPP_REGEX) orelse return false;
            const emailMatch = emailRegex.match(tail) orelse null;

            if (emailMatch != null and emailMatch.?.start == 0) {
                var url = tail[0..emailMatch.?.end];

                const at_idx = std.mem.indexOf(u8, url, "@");
                const plus_idx = std.mem.indexOf(u8, url, "+");
                const hasPlusAfterAt = at_idx != null and plus_idx != null and plus_idx.? > at_idx.?;
                if (url.len > 0 and (url[url.len - 1] == '-' or url[url.len - 1] == '_' or hasPlusAfterAt)) {
                    const text = newNode(state.allocator, "text", false, state.parentIndex + state.i, state.line, 1, "", state.indent, null) catch return false;
                    text.*.markup = escapeHtml(state.allocator, tail[0..emailMatch.?.end]) catch return false;
                    text.*.markup_allocated = true;
                    text.*.length = emailMatch.?.end;

                    if (parent.children == null) {
                        const children = state.allocator.alloc(*MarkdownNode, 1) catch return false;
                        children[0] = text;
                        parent.children = children;
                    } else {
                        const old_children = parent.children.?;
                        const new_children = state.allocator.alloc(*MarkdownNode, old_children.len + 1) catch return false;
                        std.mem.copyForwards(*MarkdownNode, new_children, old_children);
                        new_children[old_children.len] = text;
                        state.allocator.free(old_children);
                        parent.children = new_children;
                    }

                    state.i += emailMatch.?.end;

                    return true;
                }

                while (url.len > 0 and url[url.len - 1] == '.') {
                    url = url[0 .. url.len - 1];
                }

                const html = newNode(state.allocator, "html_span", false, state.parentIndex + state.i, state.line, 1, "", state.indent, null) catch return false;

                var href = std.ArrayList(u8).initCapacity(state.allocator, url.len + 10) catch return false;
                defer href.deinit(state.allocator);
                href.appendSlice(state.allocator, "<a href=\"") catch return false;
                const escapedXmppUrl = escapeHtml(state.allocator, url) catch return false;
                defer state.allocator.free(escapedXmppUrl);
                href.appendSlice(state.allocator, escapedXmppUrl) catch return false;
                href.appendSlice(state.allocator, "\">") catch return false;
                href.appendSlice(state.allocator, url) catch return false;
                href.appendSlice(state.allocator, "</a>") catch return false;

                html.*.content = href.toOwnedSlice(state.allocator) catch return false;
                html.*.content_allocated = true;
                html.*.length = url.len;

                if (parent.children == null) {
                    const children = state.allocator.alloc(*MarkdownNode, 1) catch return false;
                    children[0] = html;
                    parent.children = children;
                } else {
                    const old_children = parent.children.?;
                    const new_children = state.allocator.alloc(*MarkdownNode, old_children.len + 1) catch return false;
                    std.mem.copyForwards(*MarkdownNode, new_children, old_children);
                    new_children[old_children.len] = html;
                    state.allocator.free(old_children);
                    parent.children = new_children;
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
    defer result.deinit(allocator);

    var end_idx = url.len;
    while (end_idx > 0) {
        const c = url[end_idx - 1];
        if (std.mem.indexOf(u8, "?!.,:*_~", &.{c}) != null) {
            end_idx -= 1;
        } else {
            break;
        }
    }

    try result.appendSlice(allocator, url[0..end_idx]);
    const trimmed = try result.toOwnedSlice(allocator);
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

    if (trimmed.len > 0 and trimmed[trimmed.len - 1] == ';') {
        var i: usize = trimmed.len - 1;
        while (i > 0 and isAlphaNumeric(trimmed[i - 1])) {
            i -= 1;
        }
        if (i > 0 and trimmed[i - 1] == '&') {
            return allocator.dupe(u8, trimmed[0 .. i - 1]);
        }
    }

    return allocator.dupe(u8, trimmed);
}

pub const extendedAutolinkRule = InlineRule{
    .name = "extended_autolink",
    .@"test" = testAutolink,
};
