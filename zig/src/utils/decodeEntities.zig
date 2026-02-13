const std = @import("std");
const mvzr = @import("mvzr");

const ENTITY_REGEX = "(?<!\\\\)&([a-z0-9]+|#[0-9]{1,4}|#x[a-f0-9]{1,6});";

pub fn decodeEntities(allocator: std.mem.Allocator, text: []const u8) ![]const u8 {
    const regex = try mvzr.compile(ENTITY_REGEX);
    const match = regex.match(text) catch return text;

    if (match.start == 0 and match.end == text.len) {
        return try decodeEntity(allocator, text[1 .. text.len - 1]);
    }

    var result = std.ArrayList(u8).init(allocator);
    defer result.deinit();

    var pos: usize = 0;
    while (pos < text.len) {
        if (pos + 1 < text.len and text[pos] == '&') {
            var end = pos + 1;
            while (end < text.len and text[end] != ';') : (end += 1) {
                if (end - pos > 10) break;
            }
            if (end < text.len and text[end] == ';') {
                const entity = text[pos + 1 .. end];
                const decoded = try decodeEntity(allocator, entity);
                try result.appendSlice(decoded);
                allocator.free(decoded);
                pos = end + 1;
                continue;
            }
        }
        try result.append(text[pos]);
        pos += 1;
    }

    return result.toOwnedSlice();
}

fn decodeEntity(allocator: std.mem.Allocator, entity: []const u8) ![]const u8 {
    if (entity.len == 0) return error.InvalidEntity;

    if (entity[0] == '#') {
        const num = if (entity.len >= 2 and (entity[1] == 'x' or entity[1] == 'X'))
            std.fmt.parseInt(u21, entity[2..], 16)
        else
            std.fmt.parseInt(u21, entity[1..], 10);

        if (num) |n| {
            if (n == 0) {
                return allocator.dupe(u8, &[_]u8{ 0xEF, 0xBF, 0xBD });
            }
            const code = @as(u21, @intCast(n));
            var buf: [4]u8 = undefined;
            const len = std.unicode.utf8Encode(code, &buf);
            return allocator.dupe(u8, buf[0..len]);
        }
        return error.InvalidEntity;
    }

    if (getEntity(entity)) |decoded| {
        return allocator.dupe(u8, decoded);
    }

    const with_amp = try std.ArrayList(u8).initCapacity(allocator, entity.len + 2);
    defer with_amp.deinit();
    try with_amp.append('&');
    try with_amp.appendSlice(entity);
    try with_amp.append(';');
    return with_amp.toOwnedSlice();
}

fn getEntity(entity: []const u8) ?[]const u8 {
    _ = entity;
    return null;
}
