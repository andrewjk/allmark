const std = @import("std");

pub const Delimiter = struct {
    markup: []const u8,
    start: usize,
    length: usize,
    handled: ?bool = null,
};
