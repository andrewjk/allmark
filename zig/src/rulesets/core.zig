const std = @import("std");
const RuleSet = @import("../types/RuleSet.zig").RuleSet;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;
const Renderer = @import("../types/Renderer.zig").Renderer;

const indentRule = @import("../block/indentRule.zig").indentRule;
const headingRule = @import("../block/headingRule.zig").headingRule;
const headingUnderlineRule = @import("../block/headingUnderlineRule.zig").headingUnderlineRule;
const thematicBreakRule = @import("../block/thematicBreakRule.zig").thematicBreakRule;
const blockQuoteRule2 = @import("../block/blockQuoteRule2.zig").blockQuoteRule2;
const listOrderedRule = @import("../block/listOrderedRule.zig").listOrderedRule;
const listBulletedRule = @import("../block/listBulletedRule.zig").listBulletedRule;
const listItemRule = @import("../block/listItemRule.zig").listItemRule;
const codeBlockRule = @import("../block/codeBlockRule.zig").codeBlockRule;
const codeFenceRule = @import("../block/codeFenceRule.zig").codeFenceRule;
const htmlBlockRule = @import("../block/htmlBlockRule.zig").htmlBlockRule;
const linkReferenceRule = @import("../block/linkReferenceRule.zig").linkReferenceRule;
const paragraphRule = @import("../block/paragraphRule.zig").paragraphRule;
const contentRule = @import("../block/contentRule.zig").contentRule;
const autolinkRule = @import("../inline/autolinkRule.zig").autolinkRule;
const codeSpanRule = @import("../inline/codeSpanRule.zig").codeSpanRule;
const emphasisRule = @import("../inline/emphasisRule.zig").emphasisRule;
const hardBreakRule = @import("../inline/hardBreakRule.zig").hardBreakRule;
const htmlSpanRule = @import("../inline/htmlSpanRule.zig").htmlSpanRule;
const lineBreakRule = @import("../inline/lineBreakRule.zig").lineBreakRule;
const linkRule = @import("../inline/linkRule.zig").linkRule;
const textRule = @import("../inline/textRule.zig").textRule;
const blockQuoteRenderer = @import("../render/blockQuoteRenderer.zig").blockQuoteRenderer;
const codeBlockRenderer = @import("../render/codeBlockRenderer.zig").codeBlockRenderer;
const codeFenceRenderer = @import("../render/codeFenceRenderer.zig").codeFenceRenderer;
const codeSpanRenderer = @import("../render/codeSpanRenderer.zig").codeSpanRenderer;
const emphasisRenderer = @import("../render/emphasisRenderer.zig").emphasisRenderer;
const hardBreakRenderer = @import("../render/hardBreakRenderer.zig").hardBreakRenderer;
const headingRenderer = @import("../render/headingRenderer.zig").headingRenderer;
const headingUnderlineRenderer = @import("../render/headingUnderlineRenderer.zig").headingUnderlineRenderer;
const htmlRenderer = @import("../render/htmlRenderer.zig").htmlRenderer;
const imageRenderer = @import("../render/imageRenderer.zig").imageRenderer;
const linkRenderer = @import("../render/linkRenderer.zig").linkRenderer;
const listRenderer = @import("../render/listRenderer.zig").listRenderer;
const paragraphRenderer = @import("../render/paragraphRenderer.zig").paragraphRenderer;
const strongRenderer = @import("../render/strongRenderer.zig").strongRenderer;
const textRenderer = @import("../render/textRenderer.zig").textRenderer;
const thematicBreakRenderer = @import("../render/thematicBreakRenderer.zig").thematicBreakRenderer;

pub const core = RuleSet{
    .blocks = std.StringHashMap(*const BlockRule).init(std.heap.page_allocator) catch unreachable,
    .inlines = std.StringHashMap(*const InlineRule).init(std.heap.page_allocator) catch unreachable,
    .renderers = std.StringHashMap(*const Renderer).init(std.heap.page_allocator) catch unreachable,
};

pub fn init(allocator: std.mem.Allocator) !RuleSet {
    var blocks = std.StringHashMap(*const BlockRule).init(allocator);
    var inlines = std.StringHashMap(*const InlineRule).init(allocator);
    var renderers = std.StringHashMap(*const Renderer).init(allocator);

    try blocks.put(indentRule.name, &indentRule);
    try blocks.put(headingRule.name, &headingRule);
    try blocks.put(headingUnderlineRule.name, &headingUnderlineRule);
    try blocks.put(thematicBreakRule.name, &thematicBreakRule);
    try blocks.put(blockQuoteRule2.name, &blockQuoteRule2);
    try blocks.put(listOrderedRule.name, &listOrderedRule);
    try blocks.put(listBulletedRule.name, &listBulletedRule);
    try blocks.put(listItemRule.name, &listItemRule);
    try blocks.put(codeBlockRule.name, &codeBlockRule);
    try blocks.put(codeFenceRule.name, &codeFenceRule);
    try blocks.put(htmlBlockRule.name, &htmlBlockRule);
    try blocks.put(linkReferenceRule.name, &linkReferenceRule);
    try blocks.put(paragraphRule.name, &paragraphRule);
    try blocks.put(contentRule.name, &contentRule);

    try inlines.put(autolinkRule.name, &autolinkRule);
    try inlines.put(htmlSpanRule.name, &htmlSpanRule);
    try inlines.put(codeSpanRule.name, &codeSpanRule);
    try inlines.put(emphasisRule.name, &emphasisRule);
    try inlines.put(linkRule.name, &linkRule);
    try inlines.put(hardBreakRule.name, &hardBreakRule);
    try inlines.put(lineBreakRule.name, &lineBreakRule);
    try inlines.put(textRule.name, &textRule);

    try renderers.put(blockQuoteRenderer.name, &blockQuoteRenderer);
    try renderers.put(codeBlockRenderer.name, &codeBlockRenderer);
    try renderers.put(codeFenceRenderer.name, &codeFenceRenderer);
    try renderers.put(codeSpanRenderer.name, &codeSpanRenderer);
    try renderers.put(emphasisRenderer.name, &emphasisRenderer);
    try renderers.put(hardBreakRenderer.name, &hardBreakRenderer);
    try renderers.put(headingRenderer.name, &headingRenderer);
    try renderers.put(headingUnderlineRenderer.name, &headingUnderlineRenderer);
    try renderers.put(htmlRenderer.name, &htmlRenderer);
    try renderers.put(imageRenderer.name, &imageRenderer);
    try renderers.put(linkRenderer.name, &linkRenderer);
    try renderers.put(listRenderer.name, &listRenderer);
    try renderers.put(paragraphRenderer.name, &paragraphRenderer);
    try renderers.put(strongRenderer.name, &strongRenderer);
    try renderers.put(textRenderer.name, &textRenderer);
    try renderers.put(thematicBreakRenderer.name, &thematicBreakRenderer);

    return RuleSet{
        .blocks = blocks,
        .inlines = inlines,
        .renderers = renderers,
    };
}
