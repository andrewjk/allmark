const std = @import("std");
const RendererSet = @import("../types/RendererSet.zig").RendererSet;
const Renderer = @import("../types/Renderer.zig").Renderer;

const alertRenderer = @import("../render-console/consoleAlertRenderer.zig").consoleAlertRenderer;
const blockQuoteRenderer = @import("../render-console/consoleBlockQuoteRenderer.zig").consoleBlockQuoteRenderer;
const codeBlockRenderer = @import("../render-console/consoleCodeBlockRenderer.zig").consoleCodeBlockRenderer;
const codeFenceRenderer = @import("../render-console/consoleCodeFenceRenderer.zig").consoleCodeFenceRenderer;
const codeSpanRenderer = @import("../render-console/consoleCodeSpanRenderer.zig").consoleCodeSpanRenderer;
const commentRenderer = @import("../render-console/consoleCommentRenderer.zig").consoleCommentRenderer;
const deletionRenderer = @import("../render-console/consoleDeletionRenderer.zig").consoleDeletionRenderer;
const emphasisRenderer = @import("../render-console/consoleEmphasisRenderer.zig").consoleEmphasisRenderer;
const footnoteRenderer = @import("../render-console/consoleFootnoteRenderer.zig").consoleFootnoteRenderer;
const hardBreakRenderer = @import("../render-console/consoleHardBreakRenderer.zig").consoleHardBreakRenderer;
const headingRenderer = @import("../render-console/consoleHeadingRenderer.zig").consoleHeadingRenderer;
const highlightRenderer = @import("../render-console/consoleHighlightRenderer.zig").consoleHighlightRenderer;
const htmlBlockRenderer = @import("../render-console/consoleHtmlRenderer.zig").consoleHtmlRenderer;
const htmlSpanRenderer = @import("../render-console/consoleHtmlRenderer.zig").consoleHtmlRenderer;
const imageRenderer = @import("../render-console/consoleImageRenderer.zig").consoleImageRenderer;
const insertionRenderer = @import("../render-console/consoleInsertionRenderer.zig").consoleInsertionRenderer;
const linkRenderer = @import("../render-console/consoleLinkRenderer.zig").consoleLinkRenderer;
const listBulletedRenderer = @import("../render-console/consoleListRenderer.zig").consoleListRenderer;
const listOrderedRenderer = @import("../render-console/consoleListRenderer.zig").consoleListRenderer;
const listTaskItemRenderer = @import("../render-console/consoleListTaskItemRenderer.zig").consoleListTaskItemRenderer;
const paragraphRenderer = @import("../render-console/consoleParagraphRenderer.zig").consoleParagraphRenderer;
const strikethroughRenderer = @import("../render-console/consoleStrikethroughRenderer.zig").consoleStrikethroughRenderer;
const strongRenderer = @import("../render-console/consoleStrongRenderer.zig").consoleStrongRenderer;
const tableRenderer = @import("../render-console/consoleTableRenderer.zig").consoleTableRenderer;
const textRenderer = @import("../render-console/consoleTextRenderer.zig").consoleTextRenderer;
const thematicBreakRenderer = @import("../render-console/consoleThematicBreakRenderer.zig").consoleThematicBreakRenderer;

pub const consoleRenderers = RendererSet{
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
    try renderers.put(highlightRenderer.name, &highlightRenderer);
    try renderers.put(htmlBlockRenderer.name, &htmlBlockRenderer);
    try renderers.put("html_block", &htmlBlockRenderer);
    try renderers.put(htmlSpanRenderer.name, &htmlSpanRenderer);
    try renderers.put("html_span", &htmlSpanRenderer);
    try renderers.put(imageRenderer.name, &imageRenderer);
    try renderers.put(insertionRenderer.name, &insertionRenderer);
    try renderers.put(linkRenderer.name, &linkRenderer);
    try renderers.put(listBulletedRenderer.name, &listBulletedRenderer);
    try renderers.put("list", &listBulletedRenderer);
    try renderers.put("list_bulleted", &listBulletedRenderer);
    try renderers.put("list_ordered", &listOrderedRenderer);
    try renderers.put(listTaskItemRenderer.name, &listTaskItemRenderer);
    try renderers.put(paragraphRenderer.name, &paragraphRenderer);
    try renderers.put(strikethroughRenderer.name, &strikethroughRenderer);
    try renderers.put(strongRenderer.name, &strongRenderer);
    try renderers.put(tableRenderer.name, &tableRenderer);
    try renderers.put(textRenderer.name, &textRenderer);
    try renderers.put(thematicBreakRenderer.name, &thematicBreakRenderer);

    return RendererSet{
        .renderers = renderers,
    };
}

pub fn deinit(renderers: *RendererSet) void {
    renderers.renderers.deinit();
}
