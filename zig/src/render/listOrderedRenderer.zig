const Renderer = @import("../types/Renderer.zig").Renderer;
const render = @import("listRenderer.zig").render;

pub const listOrderedRenderer = Renderer{
    .name = "list_ordered",
    .render = render,
};
