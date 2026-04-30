const Renderer = @import("../types/Renderer.zig").Renderer;
const render = @import("consoleListRenderer.zig").render;

pub const consoleListBulletedRenderer = Renderer{
    .name = "list_bulleted",
    .render = render,
};
