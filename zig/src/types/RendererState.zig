const std = @import("std");

pub const RendererState = struct {
    allocator: std.mem.Allocator,
    renderers: std.StringArrayHashMap(*const Renderer),

    output: std.ArrayList(u8),
    footnotes: std.ArrayList(*const MarkdownNode),

    listDepth: usize = 0,
};

const Renderer = @import("Renderer.zig").Renderer;
const MarkdownNode = @import("MarkdownNode.zig").MarkdownNode;
