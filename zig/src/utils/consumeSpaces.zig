const std = @import("std");

pub fn consumeSpaces(allocator: std.mem.Allocator, text: []const u8, i: usize) ![]const u8 {
    var result = std.ArrayList(u8).init(allocator);
    defer result.deinit();

    var j: usize = i;
    while (j < text.len) : (j += 1) {
        if (isSpace(text[j])) {
            try result.append(text[j]);
        } else {
            break;
        }
    }
    return result.toOwnedSlice();
}

fn isSpace(code: u8) bool {
    return switch (code) {
        0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x20 => true,
        else => false,
    };
}
