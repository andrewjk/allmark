const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;

pub fn newText(
    allocator: std.mem.Allocator,
    index: usize,
    line: i32,
    content: []const u8,
    indent: i32,
) !*MarkdownNode {
    const node = try allocator.create(MarkdownNode);
    node.* = MarkdownNode{
        .type = try allocator.dupe(u8, "text"),
        .block = false,
        .index = index,
        .length = 0,
        .line = line,
        .markup = "",
        .markup_allocated = false,
        .delimiter = "",
        .content = try allocator.dupe(u8, content),
        .content_allocated = true,
        .indent = indent,
        .subindent = 0,
        .loose = false,
        .acceptsContent = false,
        .maybeContinuing = false,
        .blankAfter = false,
        .info = null,
        .title = null,
        .children = null,
    };
    return node;
}
