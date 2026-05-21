const std = @import("std");

pub const RendererState = struct {
    allocator: std.mem.Allocator,
    renderersMap: std.StringHashMap(*const Renderer),

    output: std.ArrayList(u8),
    footnotes: std.ArrayList(*const MarkdownNode),
    footnoteRefs: std.StringHashMap(*const MarkdownNode),

    listDepth: usize = 0,
    line_width: ?usize = null,
};

const Renderer = @import("Renderer.zig").Renderer;
const MarkdownNode = @import("MarkdownNode.zig").MarkdownNode;
