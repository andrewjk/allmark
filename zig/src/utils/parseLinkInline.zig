const std = @import("std");
const LinkReference = @import("../types/LinkReference.zig").LinkReference;
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const consumeSpaces = @import("consumeSpaces.zig").consumeSpaces;
const decodeEntities = @import("decodeEntities.zig").decodeEntities;
const escapeBackslashes = @import("escapeBackslashes.zig").escapeBackslashes;
const escapeHtml = @import("escapeHtml.zig").escapeHtml;
const encodeUri = @import("encodeUri.zig").encodeUri;
const isEscaped = @import("isEscaped.zig").isEscaped;
const isSpace = @import("isSpace.zig").isSpace;
const mvzr = @import("mvzr");

const BLANK_LINE_REGEX = "\\n[ \\t]*\\n";

pub fn parseLinkInline(allocator: std.mem.Allocator, state: *InlineParserState, start: usize, end_str: []const u8) !?LinkReference {
    _ = end_str;

    var i = start;

    const spaces = try consumeSpaces(allocator, state.src, i);
    defer allocator.free(spaces);

    const regex = mvzr.compile(BLANK_LINE_REGEX) orelse return null;
    if (regex.match(spaces) != null) {
        return null;
    }
    i += spaces.len;

    var url: []const u8 = "";
    var url_arena: ?std.heap.ArenaAllocator = null;

    if (i < state.src.len and state.src[i] == '<') {
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
        var level: i32 = 1;
        var j = i;
        while (j <= state.src.len) : (j += 1) {
            if (j < state.src.len and state.src[j] == ')' and !isEscaped(state.src, j)) {
                level -= 1;
                if (level == 0) {
                    url = state.src[i..j];
                    i = j;
                    break;
                }
            } else if (j < state.src.len and state.src[j] == '(' and !isEscaped(state.src, j)) {
                level += 1;
            }

            if (j == state.src.len or isSpace(state.src[j])) {
                url = state.src[i..j];
                i = j;
                break;
            }
        }
    }

    if (url.len > 0) {
        if (std.mem.indexOfAny(u8, url, "\r\n")) |_| {
            return null;
        }

        url_arena = std.heap.ArenaAllocator.init(allocator);
        url = try decodeEntities(url_arena.?.allocator(), url);
        url = try escapeBackslashes(url_arena.?.allocator(), url);
        url = try encodeUri(url_arena.?.allocator(), url);
    }

    const spaces2 = try consumeSpaces(allocator, state.src, i);
    defer allocator.free(spaces2);
    i += spaces2.len;

    var title: []const u8 = "";
    var title_arena: ?std.heap.ArenaAllocator = null;

    const delimiter = if (i < state.src.len) state.src[i] else 0;
    if (delimiter == ')') {
        // No title
    } else if (delimiter == '\'' or delimiter == '"') {
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
    } else {
        if (url_arena) |arena| arena.deinit();
        return null;
    }

    if (title.len > 0) {
        if (spaces2.len == 0) {
            if (url_arena) |arena| arena.deinit();
            return null;
        }

        if (std.mem.indexOf(u8, title, "\n\n")) |_| {
            if (url_arena) |arena| arena.deinit();
            return null;
        }

        title_arena = std.heap.ArenaAllocator.init(allocator);
        var decoded = try decodeEntities(title_arena.?.allocator(), title);
        decoded = try escapeBackslashes(title_arena.?.allocator(), decoded);
        title = try escapeHtml(title_arena.?.allocator(), decoded);
    }

    const spaces3 = try consumeSpaces(allocator, state.src, i);
    defer allocator.free(spaces3);
    i += spaces3.len;

    if (i >= state.src.len or state.src[i] != ')') {
        if (url_arena) |arena| arena.deinit();
        if (title_arena) |arena| arena.deinit();
        return null;
    }

    state.i = i + 1;

    const result = LinkReference{
        .url = try allocator.dupe(u8, url),
        .title = if (title.len > 0) blk: {
            const buf = try allocator.dupe(u8, title);
            break :blk buf;
        } else "",
    };

    if (url_arena) |arena| arena.deinit();
    if (title_arena) |arena| arena.deinit();

    return result;
}
