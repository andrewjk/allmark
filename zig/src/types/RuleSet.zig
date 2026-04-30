const std = @import("std");

pub const RuleSet = struct {
    blocks: []const *const BlockRule,
    inlines: []const *const InlineRule,
};

const BlockRule = @import("BlockRule.zig").BlockRule;
const InlineRule = @import("InlineRule.zig").InlineRule;
