const std = @import("std");

pub const RuleSet = struct {
    blocks: std.StringArrayHashMap(*const BlockRule),
    inlines: std.StringArrayHashMap(*const InlineRule),
    renderers: std.StringArrayHashMap(*const Renderer),
};

const BlockRule = @import("BlockRule.zig").BlockRule;
const InlineRule = @import("InlineRule.zig").InlineRule;
const Renderer = @import("Renderer.zig").Renderer;
