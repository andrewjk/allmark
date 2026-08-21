const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const renderChildrenFn = @import("console.zig").renderChildrenConsole;
const ansiDim = @import("console.zig").ansiDim;
const ansiReset = @import("console.zig").ansiReset;

pub fn render(node: *const MarkdownNode, state: *RendererState, decode: ?bool) void {
    _ = node;
    _ = decode;
    if (state.footnotes.items.len == 0) {
        return;
    }

    state.output.appendSlice(state.allocator, "\n") catch unreachable;
    state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
    state.output.appendSlice(state.allocator, "---") catch unreachable;
    state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
    state.output.appendSlice(state.allocator, "\n") catch unreachable;

    var number: usize = 1;
    for (state.footnotes.items) |fn_node| {
        const label = number;
        number += 1;

        const label_str = std.fmt.allocPrint(state.allocator, "[{d}]", .{label}) catch unreachable;
        defer state.allocator.free(label_str);

        state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
        state.output.appendSlice(state.allocator, label_str) catch unreachable;
        state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
        state.output.appendSlice(state.allocator, " ") catch unreachable;

        try renderChildrenFn(fn_node, state, true);

        if (state.output.items.len > 0 and state.output.items[state.output.items.len - 1] == '\n') {
            state.output.shrinkRetainingCapacity(state.output.items.len - 1);
        }
        if (state.output.items.len > 0 and state.output.items[state.output.items.len - 1] == '\r') {
            state.output.shrinkRetainingCapacity(state.output.items.len - 1);
        }

        state.output.appendSlice(state.allocator, "\n") catch unreachable;
    }
}

pub const consoleFootnoteListRenderer = Renderer{
    .name = "footnote_list",
    .render = render,
};
