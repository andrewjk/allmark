const Renderer = @import("../types/Renderer.zig").Renderer;
const render = @import("listRenderer.zig").render;

pub const listBulletedRenderer = Renderer{
    .name = "list_bulleted",
    .render = render,
};
