const std = @import("std");

pub const RuleSet = struct {
    blocks: std.StringHashMap(*const BlockRule),
    inlines: std.StringHashMap(*const InlineRule),
    renderers: std.StringHashMap(*const Renderer),
};

const BlockRule = @import("BlockRule.zig").BlockRule;
const InlineRule = @import("InlineRule.zig").InlineRule;
const Renderer = @import("Renderer.zig").Renderer;
