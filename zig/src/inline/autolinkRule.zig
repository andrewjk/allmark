const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const decodeEntities = @import("../utils/decodeEntities.zig").decodeEntities;
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

            // Process URL: decode entities, then encode for URI
            const escapedUrl = escapeHtml(state.allocator, rawUrl) catch return false;
            defer state.allocator.free(escapedUrl);

            const decodedUrl = decodeEntities(state.allocator, escapedUrl) catch return false;
            defer state.allocator.free(decodedUrl);

            // uriEncodedUrl will be owned by the link node (not freed here)
            const uriEncodedUrl = encodeUri(state.allocator, decodedUrl) catch return false;

            // Create text node with backslash-escaped URL for display
            const backslashEscaped = std.mem.replaceOwned(u8, state.allocator, rawUrl, "\\", "\\\\") catch {
                state.allocator.free(uriEncodedUrl);
                return false;
            };
            defer state.allocator.free(backslashEscaped);

            const linkText = newNode(state.allocator, "text", false, state.parentIndex + state.i, state.line, 1, backslashEscaped, state.indent, null) catch {
                state.allocator.free(uriEncodedUrl);
                return false;
            };

            const children = state.allocator.alloc(*MarkdownNode, 1) catch {
                state.allocator.free(uriEncodedUrl);
                return false;
            };
            children[0] = linkText;

            const link = newNode(state.allocator, "link", false, state.parentIndex + state.i, state.line, 1, "", state.indent, children) catch {
                state.allocator.free(uriEncodedUrl);
                state.allocator.free(children);
                return false;
            };

            // Transfer ownership of uriEncodedUrl to the link node
            link.*.info = uriEncodedUrl;
            link.*.length = linkMatch.?.end;

            appendChild(state.allocator, parent, link) catch {
                state.allocator.free(uriEncodedUrl);
                state.allocator.free(children);
                return false;
            };

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

            // Process email URL
            const escapedUrl = escapeHtml(state.allocator, rawUrl) catch return false;
            defer state.allocator.free(escapedUrl);

            const decodedUrl = decodeEntities(state.allocator, escapedUrl) catch return false;
            defer state.allocator.free(decodedUrl);

            // uriEncodedUrl will be owned by the link node
            const uriEncodedUrl = encodeUri(state.allocator, decodedUrl) catch return false;

            // Create mailto URL (owned by link node)
            const mailtoUrl = std.fmt.allocPrint(state.allocator, "mailto:{s}", .{uriEncodedUrl}) catch {
                state.allocator.free(uriEncodedUrl);
                return false;
            };
            state.allocator.free(uriEncodedUrl);

            // Create text node with raw email URL for display
            const linkText = newNode(state.allocator, "text", false, state.parentIndex + state.i, state.line, 1, rawUrl, state.indent, null) catch {
                state.allocator.free(mailtoUrl);
                return false;
            };

            const children = state.allocator.alloc(*MarkdownNode, 1) catch {
                state.allocator.free(mailtoUrl);
                return false;
            };
            children[0] = linkText;

            const link = newNode(state.allocator, "link", false, state.parentIndex + state.i, state.line, 1, "", state.indent, children) catch {
                state.allocator.free(mailtoUrl);
                state.allocator.free(children);
                return false;
            };

            // Transfer ownership of mailtoUrl to the link node
            link.*.info = mailtoUrl;
            link.*.length = emailMatch.?.end;

            appendChild(state.allocator, parent, link) catch {
                state.allocator.free(mailtoUrl);
                state.allocator.free(children);
                return false;
            };

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
