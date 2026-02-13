const std = @import("std");

pub const InlineRule = struct {
    const Self = @This();

    const InlineParserState = @import("InlineParserState.zig").InlineParserState;
    const MarkdownNode = @import("MarkdownNode.zig").MarkdownNode;

    name: []const u8,

    @"test": *const fn (state: *InlineParserState, parent: *MarkdownNode) bool,
};
