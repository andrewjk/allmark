const std = @import("std");

pub fn consumeSpaces(allocator: std.mem.Allocator, text: []const u8, i: usize) ![]const u8 {
    var result = std.ArrayList(u8).initCapacity(allocator, text.len - i) catch unreachable;
    defer result.deinit(allocator);

    var j: usize = i;
    while (j < text.len) : (j += 1) {
        if (isSpace(text[j])) {
            result.append(allocator, text[j]) catch unreachable;
        } else {
            break;
        }
    }
    return result.toOwnedSlice(allocator) catch unreachable;
}

fn isSpace(code: u8) bool {
    return switch (code) {
        0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x20 => true,
        else => false,
    };
}
