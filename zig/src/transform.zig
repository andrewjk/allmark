const std = @import("std");
const parse = @import("parse/parse.zig").parse;
const render = @import("render.zig").render;
const RuleSet = @import("types/RuleSet.zig").RuleSet;
const RendererSet = @import("types/RendererSet.zig").RendererSet;
const RenderOptions = @import("types/RenderOptions.zig").RenderOptions;
const MarkdownNode = @import("types/MarkdownNode.zig").MarkdownNode;

pub fn transform(allocator: std.mem.Allocator, src: []const u8, rules: RuleSet, renderers: RendererSet, options: ?RenderOptions) ![]const u8 {
    const doc = try parse(allocator, src, rules);
    defer doc.deinit(allocator);
    return render(allocator, doc, &renderers, false, options);
}
