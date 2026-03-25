const std = @import("std");
const MarkdownNode = @import("types/MarkdownNode.zig").MarkdownNode;
const RendererSet = @import("types/RendererSet.zig").RendererSet;
const RendererState = @import("types/RendererState.zig").RendererState;
const htmlRenderers = @import("rulesets/htmlRenderers.zig");
const consoleRenderers = @import("rulesets/consoleRenderers.zig");
const renderChildren = @import("render/renderChildren.zig").renderChildren;
const renderChildrenConsole = @import("render-console/console.zig").renderChildrenConsole;

pub fn render(allocator: std.mem.Allocator, doc: *const MarkdownNode, renderers: ?RendererSet, useConsole: bool) ![]const u8 {
    var createdRenderers = false;
    var localRenderers: RendererSet = undefined;
    var renderersToUse: *const RendererSet = undefined;

    if (renderers) |r| {
        renderersToUse = &r;
    } else {
        localRenderers = if (useConsole)
            try consoleRenderers.init(allocator)
        else
            try htmlRenderers.init(allocator);
        renderersToUse = &localRenderers;
        createdRenderers = true;
    }
    defer if (createdRenderers) {
        if (useConsole)
            consoleRenderers.deinit(@constCast(renderersToUse))
        else
            htmlRenderers.deinit(@constCast(renderersToUse));
    };

    var state = RendererState{
        .allocator = allocator,
        .renderers = renderersToUse.renderers,
        .output = std.ArrayList(u8).initCapacity(allocator, if (useConsole) 4096 else 1024) catch unreachable,
        .footnotes = std.ArrayList(*const MarkdownNode).initCapacity(allocator, 8) catch unreachable,
        .listDepth = 0,
    };
    defer state.output.deinit(allocator);
    defer state.footnotes.deinit(allocator);

    if (useConsole) {
        try renderChildrenConsole(doc, &state, true);
    } else {
        renderChildren(doc, &state, true);
    }

    if (state.footnotes.items.len > 0 and renderersToUse.renderers.get("footnote_list") != null) {
        const footnoteListRenderer = renderersToUse.renderers.get("footnote_list").?;
        footnoteListRenderer.render(doc, &state, null);
    }

    if (useConsole) {
        while (state.output.items.len > 0 and state.output.items[state.output.items.len - 1] == '\n') {
            _ = state.output.pop();
        }
    } else {
        if (state.output.items.len > 0 and state.output.items[state.output.items.len - 1] != '\n') {
            try state.output.append(allocator, '\n');
        }
    }

    return state.output.toOwnedSlice(allocator);
}
