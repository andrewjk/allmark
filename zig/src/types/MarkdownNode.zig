const std = @import("std");

pub const MarkdownNode = struct {
    type: []const u8,
    block: bool,
    index: usize,
    length: usize,

    line: i32,
    column: i32,

    markup: []const u8,
    markup_allocated: bool = false,
    delimiter: []const u8,
    delimiter_allocated: bool = false,
    content: []const u8,
    content_allocated: bool = false,

    indent: i32,
    subindent: i32,

    blankAfter: bool,
    acceptsContent: bool,
    maybeContinuing: bool,

    info: ?[]const u8 = null,
    title: ?[]const u8 = null,
    children: ?[]*MarkdownNode = null,

    pub fn deinit(self: *MarkdownNode, allocator: std.mem.Allocator) void {
        if (self.children) |children| {
            for (children) |child| {
                child.deinit(allocator);
            }
            allocator.free(children);
        }
        if (self.info) |info| {
            allocator.free(info);
        }
        if (self.title) |title| {
            allocator.free(title);
        }
        if (self.content_allocated and self.content.len > 0) {
            allocator.free(self.content);
        }
        if (self.markup_allocated) {
            allocator.free(self.markup);
        }
        if (self.delimiter_allocated) {
            allocator.free(self.delimiter);
        }
        allocator.free(self.type);
        allocator.destroy(self);
    }
};
