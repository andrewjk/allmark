const std = @import("std");
const RendererSet = @import("../types/RendererSet.zig").RendererSet;
const Renderer = @import("../types/Renderer.zig").Renderer;

const alertRenderer = @import("../render/alertRenderer.zig").alertRenderer;
const blockQuoteRenderer = @import("../render/blockQuoteRenderer.zig").blockQuoteRenderer;
const codeBlockRenderer = @import("../render/codeBlockRenderer.zig").codeBlockRenderer;
const codeFenceRenderer = @import("../render/codeFenceRenderer.zig").codeFenceRenderer;
const codeSpanRenderer = @import("../render/codeSpanRenderer.zig").codeSpanRenderer;
const commentRenderer = @import("../render/commentRenderer.zig").commentRenderer;
const deletionRenderer = @import("../render/deletionRenderer.zig").deletionRenderer;
const emphasisRenderer = @import("../render/emphasisRenderer.zig").emphasisRenderer;
const footnoteRenderer = @import("../render/footnoteRenderer.zig").footnoteRenderer;
const hardBreakRenderer = @import("../render/hardBreakRenderer.zig").hardBreakRenderer;
const headingRenderer = @import("../render/headingRenderer.zig").headingRenderer;
const headingUnderlineRenderer = @import("../render/headingUnderlineRenderer.zig").headingUnderlineRenderer;
const highlightRenderer = @import("../render/highlightRenderer.zig").highlightRenderer;
const htmlBlockRenderer = @import("../render/htmlBlockRenderer.zig").htmlBlockRenderer;
const htmlSpanRenderer = @import("../render/htmlSpanRenderer.zig").htmlSpanRenderer;
const imageRenderer = @import("../render/imageRenderer.zig").imageRenderer;
const insertionRenderer = @import("../render/insertionRenderer.zig").insertionRenderer;
const linkRenderer = @import("../render/linkRenderer.zig").linkRenderer;
const listBulletedRenderer = @import("../render/listRenderer.zig").listBulletedRenderer;
const listOrderedRenderer = @import("../render/listRenderer.zig").listOrderedRenderer;
const listTaskItemRenderer = @import("../render/listTaskItemRenderer.zig").listTaskItemRenderer;
const paragraphRenderer = @import("../render/paragraphRenderer.zig").paragraphRenderer;
const strikethroughRenderer = @import("../render/strikethroughRenderer.zig").strikethroughRenderer;
const strongRenderer = @import("../render/strongRenderer.zig").strongRenderer;
const subscriptRenderer = @import("../render/subscriptRenderer.zig").subscriptRenderer;
const superscriptRenderer = @import("../render/superscriptRenderer.zig").superscriptRenderer;
const tableRenderer = @import("../render/tableRenderer.zig").tableRenderer;
const tableCellRenderer = @import("../render/tableCellRenderer.zig").tableCellRenderer;
const tableHeaderRenderer = @import("../render/tableHeaderRenderer.zig").tableHeaderRenderer;
const tableRowRenderer = @import("../render/tableRowRenderer.zig").tableRowRenderer;
const textRenderer = @import("../render/textRenderer.zig").textRenderer;
const thematicBreakRenderer = @import("../render/thematicBreakRenderer.zig").thematicBreakRenderer;

pub const htmlRenderers = RendererSet{
    .renderers = std.StringArrayHashMap(*const Renderer).init(std.heap.page_allocator),
};

pub fn init(allocator: std.mem.Allocator) !RendererSet {
    var renderers = std.StringArrayHashMap(*const Renderer).init(allocator);

    try renderers.put(alertRenderer.name, &alertRenderer);
    try renderers.put(blockQuoteRenderer.name, &blockQuoteRenderer);
    try renderers.put(codeBlockRenderer.name, &codeBlockRenderer);
    try renderers.put(codeFenceRenderer.name, &codeFenceRenderer);
    try renderers.put(codeSpanRenderer.name, &codeSpanRenderer);
    try renderers.put(commentRenderer.name, &commentRenderer);
    try renderers.put(deletionRenderer.name, &deletionRenderer);
    try renderers.put(emphasisRenderer.name, &emphasisRenderer);
    try renderers.put(footnoteRenderer.name, &footnoteRenderer);
    try renderers.put(hardBreakRenderer.name, &hardBreakRenderer);
    try renderers.put(headingRenderer.name, &headingRenderer);
    try renderers.put(headingUnderlineRenderer.name, &headingUnderlineRenderer);
    try renderers.put(highlightRenderer.name, &highlightRenderer);
    try renderers.put(htmlBlockRenderer.name, &htmlBlockRenderer);
    try renderers.put(htmlSpanRenderer.name, &htmlSpanRenderer);
    try renderers.put(imageRenderer.name, &imageRenderer);
    try renderers.put(insertionRenderer.name, &insertionRenderer);
    try renderers.put(linkRenderer.name, &linkRenderer);
    try renderers.put(listBulletedRenderer.name, &listBulletedRenderer);
    try renderers.put(listOrderedRenderer.name, &listOrderedRenderer);
    try renderers.put(listTaskItemRenderer.name, &listTaskItemRenderer);
    try renderers.put(paragraphRenderer.name, &paragraphRenderer);
    try renderers.put(strikethroughRenderer.name, &strikethroughRenderer);
    try renderers.put(strongRenderer.name, &strongRenderer);
    try renderers.put(subscriptRenderer.name, &subscriptRenderer);
    try renderers.put(superscriptRenderer.name, &superscriptRenderer);
    try renderers.put(tableRenderer.name, &tableRenderer);
    try renderers.put(tableCellRenderer.name, &tableCellRenderer);
    try renderers.put(tableHeaderRenderer.name, &tableHeaderRenderer);
    try renderers.put(tableRowRenderer.name, &tableRowRenderer);
    try renderers.put(textRenderer.name, &textRenderer);
    try renderers.put(thematicBreakRenderer.name, &thematicBreakRenderer);

    return RendererSet{
        .renderers = renderers,
    };
}

pub fn deinit(renderers: *RendererSet) void {
    renderers.renderers.deinit();
}
