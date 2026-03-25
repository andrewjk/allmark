const std = @import("std");

const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const ConsoleRendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const ansiBold = @import("console.zig").ansiBold;
const ansiCyan = @import("console.zig").ansiCyan;
const ansiBlue = @import("console.zig").ansiBlue;
const ansiMagenta = @import("console.zig").ansiMagenta;
const ansiDim = @import("console.zig").ansiDim;
const ansiReset = @import("console.zig").ansiReset;
const renderChildrenConsole = @import("console.zig").renderChildrenConsole;

pub const consoleHeadingRenderer = Renderer{
    .name = "heading",
    .render = render,
};

pub fn render(node: *const MarkdownNode, state: *ConsoleRendererState, decode: ?bool) void {
    _ = decode;

    var level: usize = 0;
    var isSetext = false;

    if (node.markup.len > 0 and node.markup[0] == '#') {
        level = node.markup.len;
    } else if (std.mem.indexOfScalar(u8, node.markup, '=')) |_| {
        level = 1;
        isSetext = true;
    } else if (std.mem.indexOfScalar(u8, node.markup, '-')) |_| {
        level = 2;
        isSetext = true;
    }

    if (state.output.items.len > 0 and state.output.items[state.output.items.len - 1] != '\n') {
        state.output.append(state.allocator, '\n') catch unreachable;
    }

    const style = switch (level) {
        1 => ansiBold ++ ansiMagenta,
        2 => ansiBold ++ ansiMagenta,
        3 => ansiBold ++ ansiMagenta,
        4 => ansiBold,
        5 => ansiDim ++ ansiBold,
        6 => ansiDim ++ ansiBold,
        else => ansiReset,
    };

    if (isSetext) {
        // Render Setext-style heading with underline
        state.output.appendSlice(state.allocator, style) catch unreachable;

        var headingText = std.ArrayList(u8).initCapacity(state.allocator, 0) catch unreachable;
        defer headingText.deinit(state.allocator);

        for (node.children orelse &.{}) |child| {
            if (std.mem.eql(u8, child.type, "text")) {
                headingText.appendSlice(state.allocator, child.content) catch unreachable;
            } else {
                renderChildToString(child, state, &headingText) catch unreachable;
            }
        }

        const plainText = stripAnsiCodes(state.allocator, headingText.items) catch unreachable;
        defer state.allocator.free(plainText);

        const underlineChar: u8 = if (level == 1) '=' else '-';
        var underlineBuf = std.ArrayList(u8).initCapacity(state.allocator, plainText.len) catch unreachable;
        defer underlineBuf.deinit(state.allocator);
        for (0..plainText.len) |_| {
            underlineBuf.append(state.allocator, underlineChar) catch unreachable;
        }
        const underline = underlineBuf.items;

        state.output.appendSlice(state.allocator, headingText.items) catch unreachable;
        state.output.append(state.allocator, '\n') catch unreachable;
        state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
        state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
        state.output.appendSlice(state.allocator, underline) catch unreachable;
        state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
        state.output.append(state.allocator, '\n') catch unreachable;
    } else {
        var hashesBuf = std.ArrayList(u8).initCapacity(state.allocator, level) catch unreachable;
        defer hashesBuf.deinit(state.allocator);
        for (0..level) |_| {
            hashesBuf.append(state.allocator, '#') catch unreachable;
        }
        const hashes = hashesBuf.items;

        state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
        state.output.appendSlice(state.allocator, hashes) catch unreachable;
        state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
        state.output.appendSlice(state.allocator, " ") catch unreachable;
        state.output.appendSlice(state.allocator, style) catch unreachable;
        renderChildrenConsole(node, state, true) catch unreachable;
        state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
        state.output.append(state.allocator, '\n') catch unreachable;
    }
}

fn renderChildToString(node: *const MarkdownNode, state: *ConsoleRendererState, output: *std.ArrayList(u8)) !void {
    const originalOutputLen = state.output.items.len;

    // Capture the output by rendering to state
    if (state.renderers.get(node.type)) |renderer| {
        renderer.render(node, state, true);
    }

    // Copy the new output
    try output.appendSlice(state.allocator, state.output.items[originalOutputLen..]);

    // Restore the original output
    state.output.replaceRange(state.allocator, originalOutputLen, state.output.items.len, "") catch unreachable;
}

fn stripAnsiCodes(allocator: std.mem.Allocator, input: []const u8) ![]u8 {
    var result = std.ArrayList(u8).initCapacity(allocator, 0) catch unreachable;
    defer result.deinit(allocator);

    var i: usize = 0;
    while (i < input.len) {
        if (input[i] == 0x1b and i + 1 < input.len and input[i + 1] == '[') {
            const end = std.mem.indexOfScalar(u8, input[i..], 'm') orelse input.len - i;
            i += end + 1;
        } else {
            try result.append(allocator, input[i]);
            i += 1;
        }
    }

    return result.toOwnedSlice(allocator);
}
