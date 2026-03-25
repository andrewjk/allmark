const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;

pub fn newBlock(
    allocator: std.mem.Allocator,
    type_str: []const u8,
    index: usize,
    line: i32,
    markup: []const u8,
    indent: i32,
) !*MarkdownNode {
    const node = try allocator.create(MarkdownNode);
    node.* = MarkdownNode{
        .type = try allocator.dupe(u8, type_str),
        .block = true,
        .index = index,
        .length = 0,
        .line = line,
        .markup = try allocator.dupe(u8, markup),
        .markup_allocated = true,
        .delimiter = "",
        .content = "",
        .indent = indent,
        .subindent = 0,
        .acceptsContent = false,
        .maybeContinuing = false,
        .blankAfter = false,
        .info = null,
        .title = null,
        .children = null,
    };
    return node;
}
