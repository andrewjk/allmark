const std = @import("std");

const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const ConsoleRendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const ansiGray = @import("console.zig").ansiGray;
const ansiReset = @import("console.zig").ansiReset;

pub const consoleBlockQuoteRenderer = Renderer{
    .name = "block_quote",
    .render = render,
};

pub fn render(node: *const MarkdownNode, state: *ConsoleRendererState, decode: ?bool) void {
    _ = decode;

    if (state.output.items.len > 0 and state.output.items[state.output.items.len - 1] != '\n') {
        state.output.append(state.allocator, '\n') catch unreachable;
    }

    var content_iter = std.mem.splitScalar(u8, node.content, '\n');
    while (content_iter.next()) |line| {
        if (line.len > 0) {
            state.output.appendSlice(state.allocator, ansiGray) catch unreachable;
            state.output.appendSlice(state.allocator, "┃") catch unreachable;
            state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
            state.output.append(state.allocator, ' ') catch unreachable;
            state.output.appendSlice(state.allocator, line) catch unreachable;
            state.output.append(state.allocator, '\n') catch unreachable;
        }
    }

    if (node.children) |children| {
        for (children) |child| {
            const lines = renderNodeToString(child, state);
            defer state.allocator.free(lines);
            var line_iter = std.mem.splitScalar(u8, lines, '\n');
            while (line_iter.next()) |line| {
                if (line.len > 0) {
                    state.output.appendSlice(state.allocator, ansiGray) catch unreachable;
                    state.output.appendSlice(state.allocator, "┃") catch unreachable;
                    state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
                    state.output.append(state.allocator, ' ') catch unreachable;
                    state.output.appendSlice(state.allocator, line) catch unreachable;
                    state.output.append(state.allocator, '\n') catch unreachable;
                }
            }
        }
    }
    state.output.appendSlice(state.allocator, "\n") catch unreachable;
}

fn renderNodeToString(node: *const MarkdownNode, state: *ConsoleRendererState) []const u8 {
    const old_output = state.output;
    state.output = std.ArrayList(u8).initCapacity(state.allocator, 1024) catch unreachable;
    defer state.output = old_output;

    if (state.renderers.get(node.type)) |renderer| {
        renderer.render(node, state, true);
    }

    return state.output.toOwnedSlice(state.allocator) catch unreachable;
}
