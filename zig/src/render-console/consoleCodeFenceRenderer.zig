const std = @import("std");

const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const ConsoleRendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const ansiDim = @import("./renderToConsole.zig").ansiDim;
const ansiReset = @import("./renderToConsole.zig").ansiReset;
const renderChildrenConsole = @import("./renderToConsole.zig").renderChildrenConsole;

pub const consoleCodeFenceRenderer = Renderer{
    .name = "code_fence",
    .render = render,
};

pub fn render(node: *const MarkdownNode, state: *ConsoleRendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    const content = std.mem.trimEnd(u8, node.content, "\n");
    const isEmpty = content.len == 0;

    if (isEmpty) {
        state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
        state.output.appendSlice(state.allocator, "┌─") catch unreachable;
        state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
        state.output.append(state.allocator, '\n') catch unreachable;
        state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
        state.output.appendSlice(state.allocator, "└─") catch unreachable;
        state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
    } else {
        state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
        state.output.appendSlice(state.allocator, "┌─") catch unreachable;
        state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
        state.output.append(state.allocator, '\n') catch unreachable;

        var lineIter = std.mem.splitScalar(u8, content, '\n');
        while (lineIter.next()) |line| {
            // Skip last empty line
            if (line.len == 0) {
                const isLast = lineIter.peek() == null;
                if (isLast) {
                    continue;
                }
            }
            state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
            state.output.appendSlice(state.allocator, "│") catch unreachable;
            state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
            state.output.append(state.allocator, ' ') catch unreachable;

            // Trim carriage returns
            const trimmedLine = std.mem.trim(u8, line, "\r");
            state.output.appendSlice(state.allocator, trimmedLine) catch unreachable;
            state.output.append(state.allocator, '\n') catch unreachable;
        }

        state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
        state.output.appendSlice(state.allocator, "└─") catch unreachable;
        state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
    }
}
