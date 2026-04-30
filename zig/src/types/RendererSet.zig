const std = @import("std");

pub const RendererSet = struct {
    renderers: []const *const Renderer,
};

const Renderer = @import("Renderer.zig").Renderer;
