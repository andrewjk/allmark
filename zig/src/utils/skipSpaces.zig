pub fn skipSpaces(text: []const u8, start: usize) usize {
    var i: usize = start;
    while (i < text.len) {
        if (!isSpace(text[i])) {
            break;
        }
        i += 1;
    }
    return i;
}

fn isSpace(code: u8) bool {
    return switch (code) {
        0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x20 => true,
        else => false,
    };
}
