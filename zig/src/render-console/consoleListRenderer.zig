const std = @import("std");

const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const ConsoleRendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const ansiDim = @import("console.zig").ansiDim;
const ansiReset = @import("console.zig").ansiReset;
const consoleBullets = @import("console.zig").consoleBullets;
const renderChildrenConsole = @import("console.zig").renderChildrenConsole;

pub const consoleListRenderer = Renderer{
    .name = "list",
    .render = render,
};

pub fn render(node: *const MarkdownNode, state: *ConsoleRendererState, decode: ?bool) void {
    _ = decode;

    state.listDepth += 1;

    const ordered = std.mem.eql(u8, node.type, "list_ordered");
    const loose = isLooseList(node);

    var counter: usize = 0;
    if (ordered and node.markup.len > 0) {
        var i: usize = 0;
        while (i < node.markup.len and i < 10) : (i += 1) {
            const c = node.markup[i];
            if (c >= '0' and c <= '9') {
                counter = counter * 10 + (c - '0');
            } else {
                break;
            }
        }
        if (counter == 0) counter = 1;
    }

    if (node.children) |children| {
        for (children) |item| {
            var prefixBuf: [20]u8 = undefined;
            const prefix: []const u8 = if (ordered)
                std.fmt.bufPrint(&prefixBuf, "{d}.", .{counter}) catch "1."
            else
                consoleBullets[@min(state.listDepth - 1, consoleBullets.len - 1)];

            if (ordered) {
                counter += 1;
            }

            if (item.children) |itemChildren| {
                for (itemChildren, 0..) |child, i| {
                    if (!loose and std.mem.eql(u8, child.type, "paragraph")) {
                        if (i == 0) {
                            var indentBuf: [200]u8 = undefined;
                            const spaces = (state.listDepth - 1) * 2;
                            @memset(indentBuf[0..spaces], ' ');
                            const indent = indentBuf[0..spaces];

                            state.output.appendSlice(state.allocator, indent) catch unreachable;
                            state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
                            state.output.appendSlice(state.allocator, prefix) catch unreachable;
                            state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
                            state.output.append(state.allocator, ' ') catch unreachable;
                        }
                        renderChildrenConsole(child, state, true) catch unreachable;
                        state.output.append(state.allocator, '\n') catch unreachable;
                    } else {
                        if (i == 0) {
                            var indentBuf: [200]u8 = undefined;
                            const spaces = (state.listDepth - 1) * 2;
                            @memset(indentBuf[0..spaces], ' ');
                            const indent = indentBuf[0..spaces];

                            state.output.appendSlice(state.allocator, indent) catch unreachable;
                            state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
                            state.output.appendSlice(state.allocator, prefix) catch unreachable;
                            state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
                            state.output.append(state.allocator, ' ') catch unreachable;
                        }
                        if (state.renderersMap.get(child.type)) |renderer| {
                            renderer.render(child, state, true);
                        }
                        // Trim extra newline for tight lists (nested lists add their own)
                        if (!loose and state.output.items.len >= 2 and
                            std.mem.eql(u8, state.output.items[state.output.items.len - 2 ..], "\n\n"))
                        {
                            _ = state.output.pop();
                        }
                    }
                }
            }
        }
    }

    state.listDepth -= 1;

    // Loose lists will already have a double newline from the paragraphs
    if (!loose) {
        state.output.append(state.allocator, '\n') catch unreachable;
    }
}

fn isLooseList(node: *const MarkdownNode) bool {
    if (node.children) |children| {
        if (children.len > 1) {
            for (0..children.len - 1) |i| {
                const child = children[i];
                // Check if the list item itself has blankAfter set
                if (child.blankAfter) {
                    return true;
                }
            }
        }
    }
    return false;
}

fn consoleListTaskItemRenderer(node: *const MarkdownNode, state: *ConsoleRendererState, decode: ?bool) void {
    _ = decode;

    const is_checked = node.markup.len > 1 and node.markup[1] != ' ';
    const emoji = if (is_checked) "[✓]" else "[ ]";

    state.output.appendSlice(state.allocator, emoji) catch unreachable;
    state.output.append(state.allocator, ' ') catch unreachable;
}
