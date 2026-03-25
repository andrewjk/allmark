const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const renderChildrenFn = @import("renderChildren.zig").renderChildren;
const renderUtils = @import("renderUtils.zig");
const escapeHtml = @import("../utils/escapeHtml.zig").escapeHtml;

pub fn render(node: *const MarkdownNode, state: *RendererState, decode: ?bool) void {
    _ = decode;

    // Skip empty code blocks
    if (node.children == null or node.children.?.len == 0) {
        return;
    }

    renderUtils.startNewLine(node, state);

    var lang: []const u8 = "";
    defer if (lang.len > 0) state.allocator.free(lang);

    if (node.info) |info| {
        const trimmed = std.mem.trim(u8, info, &std.ascii.whitespace);
        const space_idx = std.mem.indexOfScalar(u8, trimmed, ' ') orelse trimmed.len;
        if (space_idx > 0) {
            const lang_str = trimmed[0..space_idx];
            lang = escapeHtml(state.allocator, lang_str) catch unreachable;
        }
    }

    const prefix = if (lang.len > 0)
        std.fmt.allocPrint(state.allocator, "<pre><code class=\"language-{s}\">", .{lang}) catch unreachable
    else
        std.fmt.allocPrint(state.allocator, "<pre><code>", .{}) catch unreachable;
    defer state.allocator.free(prefix);
    state.output.appendSlice(state.allocator, prefix) catch unreachable;

    renderChildrenFn(node, state, false);
    state.output.appendSlice(state.allocator, "</code></pre>") catch unreachable;
    renderUtils.endNewLine(node, state);
}

pub const codeBlockRenderer = Renderer{
    .name = "code_block",
    .render = render,
};
