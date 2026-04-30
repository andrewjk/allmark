const std = @import("std");
const RuleSet = @import("../types/RuleSet.zig").RuleSet;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;

const indentRule = @import("../block/indentRule.zig").indentRule;
const headingRule = @import("../block/headingRule.zig").headingRule;
const headingUnderlineRule = @import("../block/headingUnderlineRule.zig").headingUnderlineRule;
const thematicBreakRule = @import("../block/thematicBreakRule.zig").thematicBreakRule;
const blockQuoteRule = @import("../block/blockQuoteRule.zig").blockQuoteRule;
const alertRule = @import("../block/alertRule.zig").alertRule;
const listOrderedRule = @import("../block/listOrderedRule.zig").listOrderedRule;
const listBulletedRule = @import("../block/listBulletedRule.zig").listBulletedRule;
const listItemRule = @import("../block/listItemRule.zig").listItemRule;
const listTaskItemRule = @import("../block/listTaskItemRule.zig").listTaskItemRule;
const codeBlockRule = @import("../block/codeBlockRule.zig").codeBlockRule;
const codeFenceRule = @import("../block/codeFenceRule.zig").codeFenceRule;
const htmlBlockRule = @import("../block/htmlBlockRule.zig").htmlBlockRule;
const footnoteReferenceRule = @import("../block/footnoteReferenceRule.zig").footnoteReferenceRule;
const linkReferenceRule = @import("../block/linkReferenceRule.zig").linkReferenceRule;
const tableRule = @import("../block/tableRule.zig").tableRule;
const paragraphRule = @import("../block/paragraphRule.zig").paragraphRule;
const contentRule = @import("../block/contentRule.zig").contentRule;
const autolinkRule = @import("../inline/autolinkRule.zig").autolinkRule;
const extendedAutolinkRule = @import("../inline/extendedAutolinkRule.zig").extendedAutolinkRule;
const codeSpanRule = @import("../inline/codeSpanRule.zig").codeSpanRule;
const emphasisRule = @import("../inline/emphasisRule.zig").emphasisRule;
const strikethroughRule = @import("../inline/strikethroughRule.zig").strikethroughRule;
const footnoteRule = @import("../inline/footnoteRule.zig").footnoteRule;
const hardBreakRule = @import("../inline/hardBreakRule.zig").hardBreakRule;
const htmlSpanRule = @import("../inline/htmlSpanRule.zig").htmlSpanRule;
const linkRule = @import("../inline/linkRule.zig").linkRule;
const textRule = @import("../inline/textRule.zig").textRule;

pub const gfm = RuleSet{
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

    const inlines = try allocator.alloc(*const InlineRule, 10);
    inlines[0] = &autolinkRule;
    inlines[1] = &extendedAutolinkRule;
    inlines[2] = &htmlSpanRule;
    inlines[3] = &codeSpanRule;
    inlines[4] = &emphasisRule;
    inlines[5] = &strikethroughRule;
    inlines[6] = &footnoteRule;
    inlines[7] = &linkRule;
    inlines[8] = &hardBreakRule;
    inlines[9] = &textRule;

    return RuleSet{
        .blocks = blocks,
        .inlines = inlines,
    };
}

pub fn deinit(rules: *const RuleSet, allocator: std.mem.Allocator) void {
    allocator.free(rules.blocks);
    allocator.free(rules.inlines);
}
