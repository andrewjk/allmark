const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const LinkReference = @import("../types/LinkReference.zig").LinkReference;
const consumeSpaces = @import("consumeSpaces.zig").consumeSpaces;
const decodeEntities = @import("decodeEntities.zig").decodeEntities;
const escapeBackslashes = @import("escapeBackslashes.zig").escapeBackslashes;
const escapeHtml = @import("escapeHtml.zig").escapeHtml;
const encodeUri = @import("encodeUri.zig").encodeUri;
const isEscaped = @import("isEscaped.zig").isEscaped;
const isNewLine = @import("isNewLine.zig").isNewLine;
const isSpaceFn = @import("isSpace.zig").isSpace;
const mvzr = @import("mvzr");

const BLANK_LINE_REGEX = "\\n[ \\t]*\\n";

pub fn parseLinkBlock(state: *BlockParserState, start: usize) !?LinkReference {
    var spaces = try consumeSpaces(state.allocator, state.src, start);
    defer state.allocator.free(spaces);

    const regex = mvzr.compile(BLANK_LINE_REGEX) orelse return null;
    if (regex.match(spaces) != null) {
        return null;
    }
    var i = start + spaces.len;

    var url: []const u8 = "";
    if (state.src[i] == '<') {
        i += 1;
        var j = i;
        while (j < state.src.len) : (j += 1) {
            if (state.src[j] == '>' and !isEscaped(state.src, j)) {
                url = state.src[i..j];
                i = j + 1;
                break;
            }
        }
    } else {
        var j = i;
        while (j <= state.src.len) : (j += 1) {
            if (j == state.src.len or isSpaceFn(state.src[j])) {
                url = state.src[i..j];
                i = j;
                break;
            }
        }

        if (url.len == 0) {
            return null;
        }
    }

    if (url.len > 0) {
        if (std.mem.indexOfAny(u8, url, "\r\n")) |_| {
            return null;
        }

        const decoded = try decodeEntities(state.allocator, url);
        defer state.allocator.free(decoded);
        const escaped = try escapeBackslashes(state.allocator, decoded);
        defer state.allocator.free(escaped);
        url = try encodeUri(state.allocator, escaped);
    }

    var url_allocated = false;
    if (url.len > 0) {
        url_allocated = true;
    }

    const urlEnd = i;

    const spaces2 = try consumeSpaces(state.allocator, state.src, i);
    defer state.allocator.free(spaces2);
    i += spaces2.len;

    var title: []const u8 = "";
    var title_allocated = false;
    const delimiter = if (i < state.src.len) state.src[i] else 0;
    if (delimiter == '\'' or delimiter == '"') {
        i += 1;
        var j = i;
        while (j < state.src.len) : (j += 1) {
            if (state.src[j] == delimiter and !isEscaped(state.src, j)) {
                title = state.src[i..j];
                i = j + 1;
                break;
            }
        }
    } else if (delimiter == '(') {
        i += 1;
        var level: i32 = 1;
        var j = i;
        while (j < state.src.len) : (j += 1) {
            if (!isEscaped(state.src, j)) {
                if (state.src[j] == ')') {
                    level -= 1;
                    if (level == 0) {
                        title = state.src[i..j];
                        i = j + 1;
                        break;
                    }
                } else if (state.src[j] == '(') {
                    level += 1;
                }
            }
        }
    }

    if (title.len > 0) {
        if (spaces2.len == 0) {
            if (url_allocated) state.allocator.free(url);
            if (title_allocated) state.allocator.free(title);
            return null;
        }

        if (std.mem.indexOf(u8, title, "\n\n")) |_| {
            if (url_allocated) state.allocator.free(url);
            if (title_allocated) state.allocator.free(title);
            return null;
        }

        const decoded = try decodeEntities(state.allocator, title);
        defer state.allocator.free(decoded);
        const escaped = try escapeBackslashes(state.allocator, decoded);
        defer state.allocator.free(escaped);
        const escapedHtml = try escapeHtml(state.allocator, escaped);
        if (title_allocated) state.allocator.free(title);
        title = escapedHtml;
        title_allocated = true;
    }

    if (!isNewLine(state.src[i - 1])) {
        var j = i;
        while (j < state.src.len) : (j += 1) {
            if (isNewLine(state.src[j])) {
                i = j + 1;
                break;
            } else if (isSpaceFn(state.src[j])) {
                continue;
            } else {
                if (std.mem.indexOf(u8, spaces2, "\n") != null) {
                    if (title_allocated) state.allocator.free(title);
                    title = "";
                    i = urlEnd;
                    break;
                }
                if (url_allocated) state.allocator.free(url);
                if (title_allocated) state.allocator.free(title);
                return null;
            }
        }
    }

    state.i = i;

    return LinkReference{
        .url = url,
        .title = title,
    };
}
