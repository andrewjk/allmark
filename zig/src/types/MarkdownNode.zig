const std = @import("std");

pub const MarkdownNode = struct {
    type: []const u8,
    block: bool,
    index: usize,

    line: i32,
    column: i32,

    markup: []const u8,
    delimiter: []const u8,
    content: []const u8,

    indent: i32,
    subindent: i32,

    blankAfter: bool,
    acceptsContent: bool,
    maybeContinuing: bool,

    info: ?[]const u8 = null,
    title: ?[]const u8 = null,
    children: ?[]*MarkdownNode = null,
};
