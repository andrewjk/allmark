const std = @import("std");

pub const Delimiter = struct {
    markup: [3]u8,
    markup_len: u8,
    start: usize,
    length: usize,
    handled: bool = false,
    /// Precedence for delimiter matching (higher = takes precedence).
    precedence: ?u8 = null,

    pub fn getMarkup(self: *const Delimiter) []const u8 {
        return self.markup[0..self.markup_len];
    }
};
