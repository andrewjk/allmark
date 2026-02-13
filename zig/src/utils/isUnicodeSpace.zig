pub fn isUnicodeSpace(code: u8) bool {
    return switch (code) {
        0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x20, 0xa0 => true,
        else => false,
    };
}
