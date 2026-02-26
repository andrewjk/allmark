const std = @import("std");

const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const ConsoleRendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;

pub const consoleTextRenderer = Renderer{
    .name = "text",
    .render = render,
};

pub fn render(node: *const MarkdownNode, state: *ConsoleRendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = decode;

    var text = node.markup;
    const is_first = first orelse false;
    const is_last = last orelse false;
    if (is_first or is_last) {
        if (is_first and is_last) {
            text = std.mem.trim(u8, text, &std.ascii.whitespace);
        } else if (is_first) {
            var start: usize = 0;
            while (start < text.len and std.ascii.isWhitespace(text[start])) : (start += 1) {}
            text = text[start..];
        } else if (is_last) {
            var end = text.len;
            while (end > 0 and std.ascii.isWhitespace(text[end - 1])) : (end -= 1) {}
            text = text[0..end];
        }
    }

    state.output.appendSlice(state.allocator, text) catch unreachable;
}
