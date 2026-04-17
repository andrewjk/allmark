const std = @import("std");

const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const ConsoleRendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const ansiBold = @import("console.zig").ansiBold;
const ansiCyan = @import("console.zig").ansiCyan;
const ansiBlue = @import("console.zig").ansiBlue;
const ansiMagenta = @import("console.zig").ansiMagenta;
const ansiDim = @import("console.zig").ansiDim;
const ansiReset = @import("console.zig").ansiReset;
const renderChildrenConsole = @import("console.zig").renderChildrenConsole;

pub const consoleHeadingRenderer = Renderer{
    .name = "heading",
    .render = render,
};

pub fn render(node: *const MarkdownNode, state: *ConsoleRendererState, decode: ?bool) void {
    _ = decode;

    const level = node.markup.len;

    if (state.output.items.len > 0 and state.output.items[state.output.items.len - 1] != '\n') {
        state.output.append(state.allocator, '\n') catch unreachable;
    }

    const style = switch (level) {
        1 => ansiBold ++ ansiMagenta,
        2 => ansiBold ++ ansiMagenta,
        3 => ansiBold ++ ansiMagenta,
        4 => ansiBold,
        5 => ansiDim ++ ansiBold,
        6 => ansiDim ++ ansiBold,
        else => ansiReset,
    };

    var hashesBuf = std.ArrayList(u8).initCapacity(state.allocator, level) catch unreachable;
    defer hashesBuf.deinit(state.allocator);
    for (0..level) |_| {
        hashesBuf.append(state.allocator, '#') catch unreachable;
    }
    const hashes = hashesBuf.items;

    state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
    state.output.appendSlice(state.allocator, hashes) catch unreachable;
    state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
    state.output.appendSlice(state.allocator, " ") catch unreachable;
    state.output.appendSlice(state.allocator, style) catch unreachable;

    // Render the dummy paragraph's children directly (not the paragraph itself)
    if (node.children) |children| {
        if (children.len > 0) {
            renderChildrenConsole(children[0], state, true) catch unreachable;
        }
    }

    state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
    state.output.appendSlice(state.allocator, "\n\n") catch unreachable;
}
