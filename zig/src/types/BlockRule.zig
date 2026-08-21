const std = @import("std");

pub const BlockRule = struct {
    const Self = @This();

    const BlockParserState = @import("BlockParserState.zig").BlockParserState;
    const MarkdownNode = @import("MarkdownNode.zig").MarkdownNode;

    name: []const u8,

    testStart: *const fn (state: *BlockParserState, parent: *MarkdownNode, end_of_line: usize) bool,

    testContinue: *const fn (state: *BlockParserState, parent: *MarkdownNode) bool,

    closeNode: ?*const fn (state: *BlockParserState, parent: *MarkdownNode) void = null,
};