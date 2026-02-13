const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RuleSet = @import("../types/RuleSet.zig").RuleSet;
const RendererState = @import("../types/RendererState.zig").RendererState;
const renderChildren = @import("../render/renderChildren.zig").renderChildren;

const footnoteListRenderer = @import("../render/footnoteListRenderer.zig").footnoteListRenderer;

pub fn renderHtml(allocator: std.mem.Allocator, doc: *const MarkdownNode, rules: RuleSet) ![]const u8 {
    var state = RendererState{
        .renderers = rules.renderers,
        .output = std.ArrayList(u8).init(allocator),
        .footnotes = std.ArrayList(*MarkdownNode).init(allocator),
    };
    defer state.output.deinit();
    defer state.footnotes.deinit();

    renderChildren(doc, &state, true);

    if (state.output.items.len > 0) {
        footnoteListRenderer.render(&state, false, false, false);
    }

    if (state.output.items.len > 0 and state.output.items[state.output.items.len - 1] != '\n') {
        try state.output.append('\n');
    }

    return state.output.toOwnedSlice();
}
