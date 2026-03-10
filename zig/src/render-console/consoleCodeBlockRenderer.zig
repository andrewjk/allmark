const std = @import("std");

const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const ConsoleRendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const ansiDim = @import("console.zig").ansiDim;
const ansiReset = @import("console.zig").ansiReset;

pub const consoleCodeBlockRenderer = Renderer{
    .name = "code_block",
    .render = render,
};

pub fn render(node: *const MarkdownNode, state: *ConsoleRendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    if (state.output.items.len > 0 and state.output.items[state.output.items.len - 1] != '\n') {
        state.output.append(state.allocator, '\n') catch unreachable;
    }

    state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
    state.output.appendSlice(state.allocator, "┌─") catch unreachable;
    state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
    state.output.append(state.allocator, '\n') catch unreachable;

    if (node.content.len > 0) {
        var iter = std.mem.splitScalar(u8, node.content, '\n');
        while (iter.next()) |line| {
            state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
            state.output.appendSlice(state.allocator, "│") catch unreachable;
            state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
            state.output.append(state.allocator, ' ') catch unreachable;
            state.output.appendSlice(state.allocator, line) catch unreachable;
            state.output.append(state.allocator, '\n') catch unreachable;
        }
    } else {
        state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
        state.output.appendSlice(state.allocator, "│") catch unreachable;
        state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
        state.output.append(state.allocator, '\n') catch unreachable;
    }

    state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
    state.output.appendSlice(state.allocator, "└─") catch unreachable;
    state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
    state.output.append(state.allocator, '\n') catch unreachable;
}
