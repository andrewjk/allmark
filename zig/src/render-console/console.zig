const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;

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
