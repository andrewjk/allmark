const std = @import("std");

const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const ConsoleRendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const ansiBlue = @import("console.zig").ansiBlue;
const ansiGreen = @import("console.zig").ansiGreen;
const ansiMagenta = @import("console.zig").ansiMagenta;
const ansiYellow = @import("console.zig").ansiYellow;
const ansiRed = @import("console.zig").ansiRed;
const ansiDim = @import("console.zig").ansiDim;
const ansiReset = @import("console.zig").ansiReset;
const renderChildrenConsole = @import("console.zig").renderChildrenConsole;

pub const consoleAlertRenderer = Renderer{
    .name = "alert",
    .render = render,
};

pub fn render(node: *const MarkdownNode, state: *ConsoleRendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    const alert_type = node.markup;

    const style = if (std.ascii.eqlIgnoreCase(alert_type, "note"))
        ansiBlue
    else if (std.ascii.eqlIgnoreCase(alert_type, "tip"))
        ansiGreen
    else if (std.ascii.eqlIgnoreCase(alert_type, "important"))
        ansiMagenta
    else if (std.ascii.eqlIgnoreCase(alert_type, "warning"))
        ansiYellow
    else if (std.ascii.eqlIgnoreCase(alert_type, "caution"))
        ansiRed
    else
        ansiBlue;

    const icon = if (std.ascii.eqlIgnoreCase(alert_type, "note"))
        "📝"
    else if (std.ascii.eqlIgnoreCase(alert_type, "tip"))
        "💡"
    else if (std.ascii.eqlIgnoreCase(alert_type, "important"))
        "❗"
    else if (std.ascii.eqlIgnoreCase(alert_type, "warning"))
        "⚠️"
    else if (std.ascii.eqlIgnoreCase(alert_type, "caution"))
        "🚨"
    else
        "📝";

    if (state.output.items.len > 0 and state.output.items[state.output.items.len - 1] != '\n') {
        state.output.append(state.allocator, '\n') catch unreachable;
    }

    const title_type = std.fmt.allocPrint(state.allocator, "{c}{s}:", .{ std.ascii.toUpper(alert_type[0]), alert_type[1..] }) catch "Note:";
    defer state.allocator.free(title_type);

    state.output.appendSlice(state.allocator, style) catch unreachable;
    state.output.appendSlice(state.allocator, icon) catch unreachable;
    state.output.append(state.allocator, ' ') catch unreachable;
    state.output.appendSlice(state.allocator, title_type) catch unreachable;
    state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
    state.output.append(state.allocator, '\n') catch unreachable;

    renderChildrenConsole(node, state, true) catch unreachable;
}
