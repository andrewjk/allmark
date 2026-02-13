const std = @import("std");

pub fn escapeBackslashes(allocator: std.mem.Allocator, text: []const u8) ![]const u8 {
    var result = std.ArrayList(u8).init(allocator);
    defer result.deinit();

    var i: usize = 0;
    while (i < text.len) {
        if (text[i] == '\\' and i + 1 < text.len and @import("isPunctuation.zig").isPunctuation(text[i + 1])) {
            i += 1;
        }
        try result.append(text[i]);
        i += 1;
    }

    return result.toOwnedSlice();
}
