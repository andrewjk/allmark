const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const escapeHtml = @import("../utils/escapeHtml.zig").escapeHtml;
const decodeEntities = @import("../utils/decodeEntities.zig").decodeEntities;
const escapePunctuation = @import("../utils/escapePunctuation.zig").escapePunctuation;

pub fn render(node: *const MarkdownNode, state: *RendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    var markup: []const u8 = node.markup;

    if (first orelse false) {
        markup = std.mem.trimStart(u8, markup, &std.ascii.whitespace);
    }

    if (last orelse false) {
        markup = std.mem.trimEnd(u8, markup, &std.ascii.whitespace);
    }

    if (decode orelse false) {
        const decoded = decodeEntities(state.allocator, markup) catch unreachable;
        defer state.allocator.free(decoded);
        const escaped = escapePunctuation(state.allocator, decoded) catch unreachable;
        defer state.allocator.free(escaped);
        const html = escapeHtml(state.allocator, escaped) catch unreachable;
        defer state.allocator.free(html);
        state.output.appendSlice(state.allocator, html) catch unreachable;
    } else {
        const html = escapeHtml(state.allocator, markup) catch unreachable;
        defer state.allocator.free(html);
        state.output.appendSlice(state.allocator, html) catch unreachable;
    }
}

pub const textRenderer = Renderer{
    .name = "text",
    .render = render,
};
