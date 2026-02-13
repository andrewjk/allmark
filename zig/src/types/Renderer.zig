const std = @import("std");

pub const Renderer = struct {
    const Self = @This();

    const MarkdownNode = @import("MarkdownNode.zig").MarkdownNode;
    const RendererState = @import("RendererState.zig").RendererState;

    name: []const u8,

    render: *const fn (
        node: *const MarkdownNode,
        state: *RendererState,
        first: ?bool,
        last: ?bool,
        decode: ?bool,
    ) callconv(.Inline) void,
};
