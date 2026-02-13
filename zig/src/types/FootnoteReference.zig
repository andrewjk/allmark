const std = @import("std");

pub const FootnoteReference = struct {
    const MarkdownNode = @import("MarkdownNode.zig").MarkdownNode;

    label: []const u8,
    content: MarkdownNode,
};
