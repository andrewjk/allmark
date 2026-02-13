const std = @import("std");

pub const RendererState = struct {
    renderers: std.StringHashMap(*const Renderer),

    output: std.ArrayList(u8),
    footnotes: []*MarkdownNode,
};

const Renderer = @import("Renderer.zig").Renderer;
const MarkdownNode = @import("MarkdownNode.zig").MarkdownNode;
