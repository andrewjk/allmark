const std = @import("std");
const RuleSet = @import("../types/RuleSet.zig").RuleSet;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;

const headingRule = @import("../block/headingRule.zig").headingRule;
const headingUnderlineRule = @import("../block/headingUnderlineRule.zig").headingUnderlineRule;
const indentRule = @import("../block/indentRule.zig").indentRule;
const thematicBreakRule = @import("../block/thematicBreakRule.zig").thematicBreakRule;
const blockQuoteRule = @import("../block/blockQuoteRule.zig").blockQuoteRule;
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
const linkRule = @import("../inline/linkRule.zig").linkRule;
const textRule = @import("../inline/textRule.zig").textRule;

pub const core = RuleSet{
    .blocks = &.{},
    .inlines = &.{},
};

pub fn init(allocator: std.mem.Allocator) !RuleSet {
    const blocks = try allocator.alloc(*const BlockRule, 14);
    blocks[0] = &indentRule;
    blocks[1] = &headingRule;
    blocks[2] = &headingUnderlineRule;
    blocks[3] = &thematicBreakRule;
    blocks[4] = &blockQuoteRule;
    blocks[5] = &listOrderedRule;
    blocks[6] = &listBulletedRule;
    blocks[7] = &listItemRule;
    blocks[8] = &codeBlockRule;
    blocks[9] = &codeFenceRule;
    blocks[10] = &htmlBlockRule;
    blocks[11] = &linkReferenceRule;
    blocks[12] = &paragraphRule;
    blocks[13] = &contentRule;

    const inlines = try allocator.alloc(*const InlineRule, 7);
    inlines[0] = &autolinkRule;
    inlines[1] = &htmlSpanRule;
    inlines[2] = &codeSpanRule;
    inlines[3] = &emphasisRule;
    inlines[4] = &linkRule;
    inlines[5] = &hardBreakRule;
    inlines[6] = &textRule;

    return RuleSet{
        .blocks = blocks,
        .inlines = inlines,
    };
}

pub fn deinit(rules: *const RuleSet, allocator: std.mem.Allocator) void {
    allocator.free(rules.blocks);
    allocator.free(rules.inlines);
}
