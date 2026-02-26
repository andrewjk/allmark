const std = @import("std");

pub const RendererSet = struct {
    renderers: std.StringArrayHashMap(*const Renderer),
};

const Renderer = @import("Renderer.zig").Renderer;
