const std = @import("std");
const RuleSet = @import("../types/RuleSet.zig").RuleSet;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;

const indentRule = @import("../block/indentRule.zig").indentRule;
const headingRule = @import("../block/headingRule.zig").headingRule;
const headingUnderlineRule = @import("../block/headingUnderlineRule.zig").headingUnderlineRule;
const thematicBreakRule = @import("../block/thematicBreakRule.zig").thematicBreakRule;
const alertRule = @import("../block/alertRule.zig").alertRule;
const blockQuoteRule = @import("../block/blockQuoteRule.zig").blockQuoteRule;
const listOrderedRule = @import("../block/listOrderedRule.zig").listOrderedRule;
const listBulletedRule = @import("../block/listBulletedRule.zig").listBulletedRule;
const listItemRule = @import("../block/listItemRule.zig").listItemRule;
const listTaskItemRule = @import("../block/listTaskItemRule.zig").listTaskItemRule;
const footnoteReferenceRule = @import("../block/footnoteReferenceRule.zig").footnoteReferenceRule;
const codeBlockRule = @import("../block/codeBlockRule.zig").codeBlockRule;
const codeFenceRule = @import("../block/codeFenceRule.zig").codeFenceRule;
const htmlBlockRule = @import("../block/htmlBlockRule.zig").htmlBlockRule;
const linkReferenceRule = @import("../block/linkReferenceRule.zig").linkReferenceRule;
const tableRule = @import("../block/tableRule.zig").tableRule;
const paragraphRule = @import("../block/paragraphRule.zig").paragraphRule;
const contentRule = @import("../block/contentRule.zig").contentRule;
const autolinkRule = @import("../inline/autolinkRule.zig").autolinkRule;
const extendedAutolinkRule = @import("../inline/extendedAutolinkRule.zig").extendedAutolinkRule;
const codeSpanRule = @import("../inline/codeSpanRule.zig").codeSpanRule;
const commentRule = @import("../inline/commentRule.zig").commentRule;
const deletionRule = @import("../inline/deletionRule.zig").deletionRule;
const emphasisRule = @import("../inline/emphasisRule.zig").emphasisRule;
const footnoteRule = @import("../inline/footnoteRule.zig").footnoteRule;
const highlightRule = @import("../inline/highlightRule.zig").highlightRule;
const hardBreakRule = @import("../inline/hardBreakRule.zig").hardBreakRule;
const htmlSpanRule = @import("../inline/htmlSpanRule.zig").htmlSpanRule;
const insertionRule = @import("../inline/insertionRule.zig").insertionRule;
const lineBreakRule = @import("../inline/lineBreakRule.zig").lineBreakRule;
const linkRule = @import("../inline/linkRule.zig").linkRule;
const strikethroughRule = @import("../inline/strikethroughRule.zig").strikethroughRule;
const subscriptRule = @import("../inline/subscriptRule.zig").subscriptRule;
const superscriptRule = @import("../inline/superscriptRule.zig").superscriptRule;
const textRule = @import("../inline/textRule.zig").textRule;

pub const extended = RuleSet{
    .blocks = std.StringArrayHashMap(*const BlockRule).init(std.heap.page_allocator) catch unreachable,
    .inlines = std.StringArrayHashMap(*const InlineRule).init(std.heap.page_allocator) catch unreachable,
};

pub fn init(allocator: std.mem.Allocator) !RuleSet {
    var blocks = std.StringArrayHashMap(*const BlockRule).init(allocator);
    var inlines = std.StringArrayHashMap(*const InlineRule).init(allocator);

    try blocks.put(indentRule.name, &indentRule);
    try blocks.put(headingRule.name, &headingRule);
    try blocks.put(headingUnderlineRule.name, &headingUnderlineRule);
    try blocks.put(thematicBreakRule.name, &thematicBreakRule);
    try blocks.put(alertRule.name, &alertRule);
    try blocks.put(blockQuoteRule.name, &blockQuoteRule);
    try blocks.put(listOrderedRule.name, &listOrderedRule);
    try blocks.put(listBulletedRule.name, &listBulletedRule);
    try blocks.put(listItemRule.name, &listItemRule);
    try blocks.put(listTaskItemRule.name, &listTaskItemRule);
    try blocks.put(footnoteReferenceRule.name, &footnoteReferenceRule);
    try blocks.put(codeBlockRule.name, &codeBlockRule);
    try blocks.put(codeFenceRule.name, &codeFenceRule);
    try blocks.put(htmlBlockRule.name, &htmlBlockRule);
    try blocks.put(linkReferenceRule.name, &linkReferenceRule);
    try blocks.put(tableRule.name, &tableRule);
    try blocks.put(paragraphRule.name, &paragraphRule);
    try blocks.put(contentRule.name, &contentRule);

    try inlines.put(autolinkRule.name, &autolinkRule);
    try inlines.put(extendedAutolinkRule.name, &extendedAutolinkRule);
    try inlines.put(htmlSpanRule.name, &htmlSpanRule);
    try inlines.put(codeSpanRule.name, &codeSpanRule);
    try inlines.put(emphasisRule.name, &emphasisRule);
    try inlines.put(subscriptRule.name, &subscriptRule);
    try inlines.put(superscriptRule.name, &superscriptRule);
    try inlines.put(strikethroughRule.name, &strikethroughRule);
    try inlines.put(highlightRule.name, &highlightRule);
    try inlines.put(footnoteRule.name, &footnoteRule);
    try inlines.put(linkRule.name, &linkRule);
    try inlines.put(hardBreakRule.name, &hardBreakRule);
    try inlines.put(lineBreakRule.name, &lineBreakRule);
    try inlines.put(insertionRule.name, &insertionRule);
    try inlines.put(deletionRule.name, &deletionRule);
    try inlines.put(commentRule.name, &commentRule);
    try inlines.put(textRule.name, &textRule);

    return RuleSet{
        .blocks = blocks,
        .inlines = inlines,
    };
}

pub fn deinit(rules: *RuleSet) void {
    rules.blocks.deinit();
    rules.inlines.deinit();
}
