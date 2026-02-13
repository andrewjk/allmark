const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RuleSet = @import("../types/RuleSet.zig").RuleSet;
const RendererState = @import("../types/RendererState.zig").RendererState;
const renderChildren = @import("../render/renderChildren.zig").renderChildren;

const renderFootnoteList = @import("../render/footnoteListRenderer.zig").renderFootnoteList;

pub fn renderHtml(allocator: std.mem.Allocator, doc: *const MarkdownNode, rules: RuleSet) ![]const u8 {
    var state = RendererState{
        .allocator = allocator,
        .renderers = rules.renderers,
        .output = std.ArrayList(u8).initCapacity(allocator, 1024) catch unreachable,
        .footnotes = std.ArrayList(*const MarkdownNode).initCapacity(allocator, 8) catch unreachable,
    };
    defer state.output.deinit(allocator);
    defer state.footnotes.deinit(allocator);

    renderChildren(doc, &state, true);

    if (state.output.items.len > 0 and state.footnotes.items.len > 0) {
        renderFootnoteList(&state);
    }

    if (state.output.items.len == 0 or state.output.items[state.output.items.len - 1] != '\n') {
        try state.output.append(allocator, '\n');
    }

    return state.output.toOwnedSlice(allocator);
}
