const Renderer = @import("../types/Renderer.zig").Renderer;
const render = @import("consoleListRenderer.zig").render;

pub const consoleListOrderedRenderer = Renderer{
    .name = "list_ordered",
    .render = render,
};
