const std = @import("std");
const MarkdownNode = @import("types/MarkdownNode.zig").MarkdownNode;
const RendererSet = @import("types/RendererSet.zig").RendererSet;
const RendererState = @import("types/RendererState.zig").RendererState;
const RenderOptions = @import("types/RenderOptions.zig").RenderOptions;
const Renderer = @import("types/Renderer.zig").Renderer;
const htmlRenderers = @import("rulesets/htmlRenderers.zig");
const consoleRenderers = @import("rulesets/consoleRenderers.zig");
const renderChildren = @import("render/renderChildren.zig").renderChildren;
const renderChildrenConsole = @import("render-console/console.zig").renderChildrenConsole;

pub fn render(allocator: std.mem.Allocator, doc: *const MarkdownNode, renderers: ?*const RendererSet, useConsole: bool, options: ?RenderOptions) ![]const u8 {
    var createdRenderers = false;
    var localRenderers: RendererSet = undefined;
    var renderersToUse: *const RendererSet = undefined;

    if (renderers) |r| {
        renderersToUse = r;
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
            consoleRenderers.deinit(renderersToUse, allocator)
        else
            htmlRenderers.deinit(renderersToUse, allocator);
    };

    var renderersMap = std.StringHashMap(*const Renderer).init(allocator);
    for (renderersToUse.renderers) |renderer| {
        try renderersMap.put(renderer.name, renderer);
    }
    defer {
        var iter = renderersMap.iterator();
        while (iter.next()) |_| {
            // No need to free the key - it's a string literal
        }
        renderersMap.deinit();
    }

    var state = RendererState{
        .allocator = allocator,
        .renderersMap = renderersMap,
        .output = std.ArrayList(u8).initCapacity(allocator, if (useConsole) 4096 else 1024) catch unreachable,
        .footnotes = std.ArrayList(*const MarkdownNode).initCapacity(allocator, 8) catch unreachable,
        .footnoteRefs = std.StringHashMap(*const MarkdownNode).init(allocator),
        .listDepth = 0,
        .line_width = if (options) |opts| opts.line_width else null,
    };
    defer state.output.deinit(allocator);
    defer state.footnotes.deinit(allocator);
    defer state.footnoteRefs.deinit();

    if (useConsole) {
        try renderChildrenConsole(doc, &state, true);
    } else {
        renderChildren(doc, &state, true);
    }

    if (state.footnotes.items.len > 0 and renderersMap.get("footnote_list") != null) {
        const footnoteListRenderer = renderersMap.get("footnote_list").?;
        footnoteListRenderer.render(doc, &state, null);
    }

    // Ensure exactly one trailing newline (matching web implementation behavior)
    while (state.output.items.len > 0 and (state.output.items[state.output.items.len - 1] == '\n' or state.output.items[state.output.items.len - 1] == '\r')) {
        _ = state.output.pop();
    }
    if (state.output.items.len > 0) {
        try state.output.append(allocator, '\n');
    }

    return state.output.toOwnedSlice(allocator);
}
