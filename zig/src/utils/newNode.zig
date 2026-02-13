const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;

pub fn newNode(
    allocator: std.mem.Allocator,
    type_str: []const u8,
    block: bool,
    index: usize,
    line: i32,
    column: i32,
    markup: []const u8,
    indent: i32,
    children: ?[]*MarkdownNode,
) !*MarkdownNode {
    const node = try allocator.create(MarkdownNode);
    node.* = MarkdownNode{
        .type = try allocator.dupe(u8, type_str),
        .block = block,
        .index = index,
        .line = line,
        .column = column,
        .markup = try allocator.dupe(u8, markup),
        .delimiter = "",
        .content = "",
        .indent = indent,
        .subindent = 0,
        .acceptsContent = false,
        .maybeContinuing = false,
        .blankAfter = false,
        .info = null,
        .title = null,
        .children = children,
    };
    return node;
}
