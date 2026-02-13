const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const LinkReference = @import("../types/LinkReference.zig").LinkReference;
const consumeSpaces = @import("consumeSpaces.zig").consumeSpaces;
const decodeEntities = @import("decodeEntities.zig").decodeEntities;
const escapeBackslashes = @import("escapeBackslashes.zig").escapeBackslashes;
const escapeHtml = @import("escapeHtml.zig").escapeHtml;
const isEscaped = @import("isEscaped.zig").isEscaped;
const isNewLine = @import("isNewLine.zig").isNewLine;
const isSpaceFn = @import("isSpace.zig").isSpace;

const BLANK_LINE_REGEX = "\n[ \t]*\n";

pub fn parseLinkBlock(state: *BlockParserState, start: usize) !?LinkReference {
    var spaces = try consumeSpaces(state.allocator, state.src, start);
    defer state.allocator.free(spaces);

    if (std.mem.indexOf(u8, spaces, "\n")) |_| {
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
        state.allocator.free(escaped);
        var uri_encoded = std.ArrayList(u8).init(state.allocator);
        defer uri_encoded.deinit();

        for (url) |c| {
            if (c > 127 or std.ascii.isASCII(c) and !std.ascii.isAlphanumeric(c) and !std.mem.containsAtLeast(u8, "-_.!~*'();:@&=+$,/?#%[]", &.{c})) {
                try std.fmt.format(uri_encoded.writer(), "%{X:0>2}", .{c});
            } else {
                try uri_encoded.append(c);
            }
        }

        url = try uri_encoded.toOwnedSlice();
    }

    const urlEnd = i;

    spaces = try consumeSpaces(state.allocator, state.src, i);
    defer state.allocator.free(spaces);
    i += spaces.len;

    var title: []const u8 = "";
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
        if (spaces.len == 0) {
            return null;
        }

        if (std.mem.indexOf(u8, title, "\n\n")) |_| {
            return null;
        }

        const decoded = try decodeEntities(state.allocator, title);
        defer state.allocator.free(decoded);
        const escaped = try escapeBackslashes(state.allocator, decoded);
        state.allocator.free(escaped);
        title = try escapeHtml(state.allocator, escaped);
    }

    if (!isNewLine(&.{state.src[i - 1]})) {
        var j = i;
        while (j < state.src.len) : (j += 1) {
            if (isNewLine(&.{state.src[j]})) {
                i = j + 1;
                break;
            } else if (isSpaceFn(state.src[j])) {
                continue;
            } else {
                if (std.mem.indexOf(u8, spaces, "\n") != null) {
                    _ = "";
                    _ = urlEnd;
                }
                return null;
            }
        }
    }

    state.i = i;

    return LinkReference{
        .url = try state.allocator.dupe(u8, url),
        .title = try state.allocator.dupe(u8, title),
    };
}
