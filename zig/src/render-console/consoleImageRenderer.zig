const std = @import("std");

const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const ConsoleRendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const ansiGray = @import("./renderToConsole.zig").ansiGray;
const ansiReset = @import("./renderToConsole.zig").ansiReset;

pub const consoleImageRenderer = Renderer{
    .name = "image",
    .render = render,
};

pub fn render(node: *const MarkdownNode, state: *ConsoleRendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    var altBuffer: std.ArrayList(u8) = std.ArrayList(u8).initCapacity(state.allocator, 64) catch unreachable;
    defer altBuffer.deinit(state.allocator);
    if (node.children) |children| {
        for (children) |child| {
            if (std.mem.eql(u8, child.type, "text")) {
                altBuffer.appendSlice(state.allocator, child.markup) catch unreachable;
            }
        }
    }

    const alt_text = if (altBuffer.items.len > 0) altBuffer.items else if (node.info) |info| info else "";

    state.output.appendSlice(state.allocator, ansiGray) catch unreachable;
    state.output.appendSlice(state.allocator, "[Image: ") catch unreachable;
    state.output.appendSlice(state.allocator, alt_text) catch unreachable;
    state.output.appendSlice(state.allocator, "]") catch unreachable;
    state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
}
