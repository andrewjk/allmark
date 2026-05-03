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
const footnoteRefRenderer = @import("../render/footnoteRefRenderer.zig").footnoteRefRenderer;
const footnoteListRenderer = @import("../render/footnoteListRenderer.zig").footnoteListRenderer;
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
const textRenderer = @import("../render/textRenderer.zig").textRenderer;
const thematicBreakRenderer = @import("../render/thematicBreakRenderer.zig").thematicBreakRenderer;

pub const htmlRenderers = RendererSet{
    .renderers = &.{},
};

pub fn init(allocator: std.mem.Allocator) !RendererSet {
    const renderers = try allocator.alloc(*const Renderer, 31);
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
    renderers[26] = &subscriptRenderer;
    renderers[27] = &superscriptRenderer;
    renderers[28] = &tableRenderer;
    renderers[29] = &textRenderer;
    renderers[30] = &thematicBreakRenderer;

    return RendererSet{
        .renderers = renderers,
    };
}

pub fn deinit(renderers: *const RendererSet, allocator: std.mem.Allocator) void {
    allocator.free(renderers.renderers);
}
