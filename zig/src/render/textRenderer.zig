const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const renderTag = @import("renderTag.zig").renderTag;
const escapeHtml = @import("../utils/escapeHtml.zig").escapeHtml;
const decodeEntities = @import("../utils/decodeEntities.zig").decodeEntities;
const escapePunctuation = @import("../utils/escapePunctuation.zig").escapePunctuation;

pub fn render(node: *const MarkdownNode, state: *RendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    var markup = node.markup;
    if (first) |f| if (f) {
        markup = std.mem.trimLeft(u8, markup, " \t\n\r");
    };
    if (last) |l| if (l) {
        markup = std.mem.trimRight(u8, markup, " \t\n\r");
    };
    if (decode orelse true) {
        var buffer = std.ArrayList(u8).init(std.heap.page_allocator);
        defer buffer.deinit();
        try decodeEntities(&buffer, markup);
        const decoded = buffer.toOwnedSlice();
        defer std.heap.page_allocator.free(decoded);
        var punct_buffer = std.ArrayList(u8).init(std.heap.page_allocator);
        defer punct_buffer.deinit();
        try escapePunctuation(&punct_buffer, decoded);
        const punct_escaped = punct_buffer.toOwnedSlice();
        defer std.heap.page_allocator.free(punct_escaped);
        try escapeHtml(&state.output, punct_escaped);
    } else {
        try escapeHtml(&state.output, markup);
    }
}

pub const textRenderer = Renderer{
    .name = "text",
    .render = render,
};
