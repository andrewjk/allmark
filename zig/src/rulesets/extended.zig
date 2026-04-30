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
const linkRule = @import("../inline/linkRule.zig").linkRule;
const strikethroughRule = @import("../inline/strikethroughRule.zig").strikethroughRule;
const subscriptRule = @import("../inline/subscriptRule.zig").subscriptRule;
const superscriptRule = @import("../inline/superscriptRule.zig").superscriptRule;
const textRule = @import("../inline/textRule.zig").textRule;

pub const extended = RuleSet{
    .blocks = &.{},
    .inlines = &.{},
};

pub fn init(allocator: std.mem.Allocator) !RuleSet {
    const blocks = try allocator.alloc(*const BlockRule, 18);
    blocks[0] = &indentRule;
    blocks[1] = &headingRule;
    blocks[2] = &headingUnderlineRule;
    blocks[3] = &thematicBreakRule;
    blocks[4] = &alertRule;
    blocks[5] = &blockQuoteRule;
    blocks[6] = &listOrderedRule;
    blocks[7] = &listBulletedRule;
    blocks[8] = &listItemRule;
    blocks[9] = &listTaskItemRule;
    blocks[10] = &footnoteReferenceRule;
    blocks[11] = &codeBlockRule;
    blocks[12] = &codeFenceRule;
    blocks[13] = &htmlBlockRule;
    blocks[14] = &linkReferenceRule;
    blocks[15] = &tableRule;
    blocks[16] = &paragraphRule;
    blocks[17] = &contentRule;

    const inlines = try allocator.alloc(*const InlineRule, 16);
    inlines[0] = &autolinkRule;
    inlines[1] = &extendedAutolinkRule;
    inlines[2] = &htmlSpanRule;
    inlines[3] = &codeSpanRule;
    inlines[4] = &emphasisRule;
    inlines[5] = &subscriptRule;
    inlines[6] = &superscriptRule;
    inlines[7] = &strikethroughRule;
    inlines[8] = &highlightRule;
    inlines[9] = &footnoteRule;
    inlines[10] = &linkRule;
    inlines[11] = &hardBreakRule;
    inlines[12] = &insertionRule;
    inlines[13] = &deletionRule;
    inlines[14] = &commentRule;
    inlines[15] = &textRule;

    return RuleSet{
        .blocks = blocks,
        .inlines = inlines,
    };
}

pub fn deinit(rules: *const RuleSet, allocator: std.mem.Allocator) void {
    allocator.free(rules.blocks);
    allocator.free(rules.inlines);
}
