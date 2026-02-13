pub fn isEscaped(text: []const u8, i: usize) bool {
    if (i == 0) return false;
    if (text[i - 1] != '\\') return false;
    if (i == 1) return true;
    return text[i - 2] != '\\';
}
