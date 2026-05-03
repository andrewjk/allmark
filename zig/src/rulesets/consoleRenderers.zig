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
const footnoteRefRenderer = @import("../render-console/consoleFootnoteRefRenderer.zig").consoleFootnoteRefRenderer;
const footnoteListRenderer = @import("../render-console/consoleFootnoteListRenderer.zig").consoleFootnoteListRenderer;
const hardBreakRenderer = @import("../render-console/consoleHardBreakRenderer.zig").consoleHardBreakRenderer;
const headingRenderer = @import("../render-console/consoleHeadingRenderer.zig").consoleHeadingRenderer;
const headingUnderlineRenderer = @import("../render-console/consoleHeadingUnderlineRenderer.zig").consoleHeadingUnderlineRenderer;
const highlightRenderer = @import("../render-console/consoleHighlightRenderer.zig").consoleHighlightRenderer;
const htmlBlockRenderer = @import("../render-console/consoleHtmlBlockRenderer.zig").consoleHtmlBlockRenderer;
const htmlSpanRenderer = @import("../render-console/consoleHtmlSpanRenderer.zig").consoleHtmlSpanRenderer;
const imageRenderer = @import("../render-console/consoleImageRenderer.zig").consoleImageRenderer;
const insertionRenderer = @import("../render-console/consoleInsertionRenderer.zig").consoleInsertionRenderer;
const linkRenderer = @import("../render-console/consoleLinkRenderer.zig").consoleLinkRenderer;
const listBulletedRenderer = @import("../render-console/consoleListBulletedRenderer.zig").consoleListBulletedRenderer;
const listOrderedRenderer = @import("../render-console/consoleListOrderedRenderer.zig").consoleListOrderedRenderer;
const listTaskItemRenderer = @import("../render-console/consoleListTaskItemRenderer.zig").consoleListTaskItemRenderer;
const paragraphRenderer = @import("../render-console/consoleParagraphRenderer.zig").consoleParagraphRenderer;
const strikethroughRenderer = @import("../render-console/consoleStrikethroughRenderer.zig").consoleStrikethroughRenderer;
const strongRenderer = @import("../render-console/consoleStrongRenderer.zig").consoleStrongRenderer;
const tableRenderer = @import("../render-console/consoleTableRenderer.zig").consoleTableRenderer;
const textRenderer = @import("../render-console/consoleTextRenderer.zig").consoleTextRenderer;
const thematicBreakRenderer = @import("../render-console/consoleThematicBreakRenderer.zig").consoleThematicBreakRenderer;

pub const consoleRenderers = RendererSet{
    .renderers = &.{},
};

pub fn init(allocator: std.mem.Allocator) !RendererSet {
    const renderers = try allocator.alloc(*const Renderer, 30);
    renderers[0] = &alertRenderer;
    renderers[1] = &blockQuoteRenderer;
    renderers[2] = &codeBlockRenderer;
    renderers[3] = &codeFenceRenderer;
    renderers[4] = &codeSpanRenderer;
    renderers[5] = &commentRenderer;
    renderers[6] = &deletionRenderer;
    renderers[7] = &emphasisRenderer;
    renderers[8] = &footnoteRenderer;
    renderers[9] = &footnoteRefRenderer;
    renderers[10] = &footnoteListRenderer;
    renderers[11] = &hardBreakRenderer;
    renderers[12] = &headingRenderer;
    renderers[13] = &headingUnderlineRenderer;
    renderers[14] = &highlightRenderer;
    renderers[15] = &htmlBlockRenderer;
    renderers[16] = &htmlSpanRenderer;
    renderers[17] = &imageRenderer;
    renderers[18] = &insertionRenderer;
    renderers[19] = &linkRenderer;
    renderers[20] = &listBulletedRenderer;
    renderers[21] = &listOrderedRenderer;
    renderers[22] = &listTaskItemRenderer;
    renderers[23] = &paragraphRenderer;
    renderers[24] = &strikethroughRenderer;
    renderers[25] = &strongRenderer;
    renderers[26] = &tableRenderer;
    renderers[27] = &textRenderer;
    renderers[28] = &thematicBreakRenderer;
    renderers[29] = &htmlBlockRenderer;

    return RendererSet{
        .renderers = renderers,
    };
}

pub fn deinit(renderers: *const RendererSet, allocator: std.mem.Allocator) void {
    allocator.free(renderers.renderers);
}
