const std = @import("std");

pub fn encodeUri(allocator: std.mem.Allocator, text: []const u8) ![]const u8 {
    var result = std.ArrayList(u8).empty;
    errdefer result.deinit(allocator);

    var i: usize = 0;
    while (i < text.len) {
        const c = text[i];

        // Check if this is a percent-encoded sequence (e.g., %20)
        if (c == '%' and i + 2 < text.len) {
            const h1 = text[i + 1];
            const h2 = text[i + 2];
            if (isHex(h1) and isHex(h2)) {
                // Already percent-encoded, keep as-is
                try result.append(allocator, '%');
                try result.append(allocator, h1);
                try result.append(allocator, h2);
                i += 3;
                continue;
            }
        }

        if (isUnreserved(c) or isReserved(c)) {
            try result.append(allocator, c);
        } else {
            const encoded = try percentEncode(allocator, c);
            defer allocator.free(encoded);
            try result.appendSlice(allocator, encoded);
        }
        i += 1;
    }

    return result.toOwnedSlice(allocator);
}

fn isHex(c: u8) bool {
    return switch (c) {
        '0'...'9', 'a'...'f', 'A'...'F' => true,
        else => false,
    };
}

fn isUnreserved(c: u8) bool {
    return switch (c) {
        'A'...'Z', 'a'...'z', '0'...'9', '-', '_', '.', '~' => true,
        else => false,
    };
}

fn isReserved(c: u8) bool {
    return switch (c) {
        ';', ',', '/', '?', ':', '@', '&', '=', '+', '$', '!' => true,
        '*', '\'', '(', ')', '#' => true,
        else => false,
    };
}

fn percentEncode(allocator: std.mem.Allocator, c: u8) ![]const u8 {
    var buf: [3]u8 = undefined;
    buf[0] = '%';
    buf[1] = intToHex(c >> 4);
    buf[2] = intToHex(c & 0x0F);
    return allocator.dupe(u8, &buf);
}

fn intToHex(v: u8) u8 {
    return if (v < 10) v + '0' else v - 10 + 'A';
}
