const std = @import("std");

pub fn isAlpha(code: u8) bool {
    return (code > 64 and code < 91) or (code > 96 and code < 123);
}

pub fn isNumeric(code: u8) bool {
    return code > 47 and code < 58;
}

pub fn isAlphaNumeric(code: u8) bool {
    return isAlpha(code) or isNumeric(code);
}
