const std = @import("std");

const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const ConsoleRendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const ansiDim = @import("./renderToConsole.zig").ansiDim;
const ansiReset = @import("./renderToConsole.zig").ansiReset;

pub const consoleFootnoteRenderer = Renderer{
    .name = "footnote",
    .render = render,
};

pub fn render(node: *const MarkdownNode, state: *ConsoleRendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    var exists = false;
    for (state.footnotes.items) |footnote| {
        if (footnote.info) |info| {
            if (node.info) |node_info| {
                if (std.mem.eql(u8, info, node_info)) {
                    exists = true;
                    break;
                }
            }
        }
    }

    if (!exists) {
        state.footnotes.append(state.allocator, node) catch unreachable;
    }

    const label = state.footnotes.items.len;

    state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
    state.output.append(state.allocator, '[') catch unreachable;
    const label_str = std.fmt.allocPrint(state.allocator, "{d}", .{label}) catch "1";
    state.output.appendSlice(state.allocator, label_str) catch unreachable;
    state.allocator.free(label_str);
    state.output.append(state.allocator, ']') catch unreachable;
    state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
}
