const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererSet = @import("../types/RendererSet.zig").RendererSet;
const RendererState = @import("../types/RendererState.zig").RendererState;
const htmlRenderers = @import("../rulesets/htmlRenderers.zig");
const renderChildren = @import("../render/renderChildren.zig").renderChildren;

const renderFootnoteList = @import("../render/footnoteListRenderer.zig").renderFootnoteList;

pub fn renderHtml(allocator: std.mem.Allocator, doc: *const MarkdownNode, renderers: ?RendererSet) ![]const u8 {
    var createdRenderers = false;
    var localRenderers: RendererSet = undefined;
    var renderersToUse: *const RendererSet = undefined;

    if (renderers) |r| {
        renderersToUse = &r;
    } else {
        localRenderers = try htmlRenderers.init(allocator);
        renderersToUse = &localRenderers;
        createdRenderers = true;
    }
    defer if (createdRenderers) {
        htmlRenderers.deinit(@constCast(renderersToUse));
    };

    var state = RendererState{
        .allocator = allocator,
        .renderers = renderersToUse.renderers,
        .output = std.ArrayList(u8).initCapacity(allocator, 1024) catch unreachable,
        .footnotes = std.ArrayList(*const MarkdownNode).initCapacity(allocator, 8) catch unreachable,
    };
    defer state.output.deinit(allocator);
    defer state.footnotes.deinit(allocator);

    renderChildren(doc, &state, true);

    if (state.output.items.len > 0 and state.footnotes.items.len > 0) {
        renderFootnoteList(&state);
    }

    if (state.output.items.len > 0 and state.output.items[state.output.items.len - 1] != '\n') {
        try state.output.append(allocator, '\n');
    }

    return state.output.toOwnedSlice(allocator);
}
