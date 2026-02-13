const std = @import("std");

pub fn normalizeLabel(allocator: std.mem.Allocator, text: []const u8) ![]const u8 {
    var result = try std.ArrayList(u8).initCapacity(allocator, text.len);
    defer result.deinit();

    for (text) |c| {
        const upper = std.ascii.toUpper(c);
        try result.append(upper);
    }

    const trimmed = std.mem.trim(u8, result.items, &std.ascii.whitespace);
    var normalized = std.ArrayList(u8).init(allocator);
    defer normalized.deinit();

    var i: usize = 0;
    while (i < trimmed.len) {
        if (isSpace(trimmed[i])) {
            try normalized.append(' ');
            while (i < trimmed.len and isSpace(trimmed[i])) {
                i += 1;
            }
        } else {
            try normalized.append(trimmed[i]);
            i += 1;
        }
    }

    return normalized.toOwnedSlice();
}

fn isSpace(c: u8) bool {
    return switch (c) {
        ' ', '\t', '\n', '\r' => true,
        else => false,
    };
}
