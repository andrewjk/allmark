const std = @import("std");

const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const ConsoleRendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const ansiDim = @import("console.zig").ansiDim;
const ansiReset = @import("console.zig").ansiReset;

pub const consoleThematicBreakRenderer = Renderer{
    .name = "thematic_break",
    .render = render,
};

fn render(node: *const MarkdownNode, state: *ConsoleRendererState, decode: ?bool) void {
    _ = node;
    _ = decode;

    if (state.output.items.len > 0 and state.output.items[state.output.items.len - 1] != '\n') {
        state.output.append(state.allocator, '\n') catch unreachable;
    }
    const count = 3;
    var dashes = std.ArrayList(u8).initCapacity(state.allocator, count) catch unreachable;
    defer dashes.deinit(state.allocator);
    for (0..count) |_| {
        dashes.appendSlice(state.allocator, "─") catch unreachable;
    }
    const dashesSlice = dashes.items;

    state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
    state.output.appendSlice(state.allocator, dashesSlice) catch unreachable;
    state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
    state.output.appendSlice(state.allocator, "\n\n") catch unreachable;
}
