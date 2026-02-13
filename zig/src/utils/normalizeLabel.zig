const std = @import("std");

pub fn normalizeLabel(allocator: std.mem.Allocator, text: []const u8) ![]const u8 {
    // First convert to lowercase, then to uppercase (like JavaScript's toLowerCase().toUpperCase())
    // This handles special Unicode cases like the German sharp s (ẞ/SS)
    var lowercase = try std.ArrayList(u8).initCapacity(allocator, text.len * 3);
    defer lowercase.deinit(allocator);

    for (text) |c| {
        const lower = std.ascii.toLower(c);
        try lowercase.append(allocator, lower);
    }

    // Convert to uppercase
    var uppercase = try std.ArrayList(u8).initCapacity(allocator, lowercase.items.len * 3);
    defer uppercase.deinit(allocator);

    for (lowercase.items) |c| {
        const upper = std.ascii.toUpper(c);
        try uppercase.append(allocator, upper);
    }

    const trimmed = std.mem.trim(u8, uppercase.items, &std.ascii.whitespace);
    var normalized = std.ArrayList(u8).empty;
    defer normalized.deinit(allocator);

    var i: usize = 0;
    while (i < trimmed.len) {
        if (isSpace(trimmed[i])) {
            try normalized.append(allocator, ' ');
            while (i < trimmed.len and isSpace(trimmed[i])) {
                i += 1;
            }
        } else {
            try normalized.append(allocator, trimmed[i]);
            i += 1;
        }
    }

    return normalized.toOwnedSlice(allocator);
}

fn isSpace(c: u8) bool {
    return switch (c) {
        ' ', '\t', '\n', '\r' => true,
        else => false,
    };
}
