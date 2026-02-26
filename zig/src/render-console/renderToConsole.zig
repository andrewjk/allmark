const std = @import("std");

const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererSet = @import("../types/RendererSet.zig").RendererSet;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const consoleRenderers = @import("../rulesets/consoleRenderers.zig");

pub const ansiReset = "\x1b[0m";
pub const ansiBold = "\x1b[1m";
pub const ansiItalic = "\x1b[3m";
pub const ansiDim = "\x1b[2m";
pub const ansiGray = "\x1b[90m";
pub const ansiRed = "\x1b[31m";
pub const ansiGreen = "\x1b[32m";
pub const ansiYellow = "\x1b[33m";
pub const ansiBlue = "\x1b[34m";
pub const ansiMagenta = "\x1b[35m";
pub const ansiCyan = "\x1b[36m";
pub const ansiOrange = "\x1b[38;5;208m";
pub const ansiUnderline = "\x1b[4m";
pub const ansiBlack = "\x1b[30m";
pub const ansiYellowBg = "\x1b[43m";
pub const ansiStrikethrough = "\x1b[9m";
pub const ansiStrikethroughReset = "\x1b[29m";

pub const consoleBullets = [4][]const u8{
    "•",
    "◦",
    "▪",
    "‣",
};

pub fn renderToConsole(allocator: std.mem.Allocator, doc: *const MarkdownNode, renderers: ?RendererSet) ![]const u8 {
    var createdRenderers = false;
    var localRenderers: RendererSet = undefined;
    var renderersToUse: *const RendererSet = undefined;

    if (renderers) |r| {
        renderersToUse = &r;
    } else {
        localRenderers = try consoleRenderers.init(allocator);
        renderersToUse = &localRenderers;
        createdRenderers = true;
    }
    defer if (createdRenderers) {
        consoleRenderers.deinit(@constCast(renderersToUse));
    };

    var state = RendererState{
        .allocator = allocator,
        .renderers = renderersToUse.renderers,
        .output = std.ArrayList(u8).initCapacity(allocator, 4096) catch unreachable,
        .footnotes = std.ArrayList(*const MarkdownNode).initCapacity(allocator, 8) catch unreachable,
        .depth = 0,
        .quoteDepth = 0,
    };
    defer state.output.deinit(allocator);
    defer state.footnotes.deinit(allocator);

    try renderChildrenConsole(doc, &state, true);

    while (state.output.items.len > 0 and state.output.items[state.output.items.len - 1] == '\n') {
        _ = state.output.pop();
    }

    return state.output.toOwnedSlice(allocator);
}

pub fn renderChildrenConsole(node: *const MarkdownNode, state: *RendererState, decode: bool) !void {
    if (node.children) |children| {
        if (children.len > 0) {
            const trim = !std.mem.eql(u8, node.type, "code_block") and
                !std.mem.eql(u8, node.type, "code_fence") and
                !std.mem.eql(u8, node.type, "code_span");

            for (children, 0..) |child, i| {
                const first = i == 0;
                const last = i == children.len - 1;
                try renderNodeConsole(child, state, if (trim) first else false, if (trim) last else false, decode);
            }
        }
    }
}

fn renderNodeConsole(node: *const MarkdownNode, state: *RendererState, first: bool, last: bool, decode: bool) !void {
    if (state.renderers.get(node.type)) |renderer| {
        renderer.render(node, state, first, last, decode);
    }
}
