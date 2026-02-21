const std = @import("std");

const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RuleSet = @import("../types/RuleSet.zig").RuleSet;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;

const ansiReset = "\x1b[0m";
const ansiBold = "\x1b[1m";
const ansiDim = "\x1b[2m";
const ansiGray = "\x1b[90m";
const ansiRed = "\x1b[31m";
const ansiGreen = "\x1b[32m";
const ansiYellow = "\x1b[33m";
const ansiBlue = "\x1b[34m";
const ansiMagenta = "\x1b[35m";
const ansiCyan = "\x1b[36m";
const ansiOrange = "\x1b[38;5;208m";
const ansiUnderline = "\x1b[4m";

const consoleBullets = [4][]const u8{
    "•",
    "◦",
    "▪",
    "‣",
};

const ConsoleRendererState = struct {
    allocator: std.mem.Allocator,
    renderers: std.StringHashMap(*const Renderer),
    output: std.ArrayList(u8),
    footnotes: std.ArrayList(*const MarkdownNode),
    depth: usize = 0,
    quoteDepth: usize = 0,
};

pub fn renderToConsole(allocator: std.mem.Allocator, doc: *const MarkdownNode, rules: RuleSet) ![]const u8 {
    var state = ConsoleRendererState{
        .allocator = allocator,
        .renderers = getConsoleRenderers(allocator, rules.renderers),
        .output = std.ArrayList(u8).initCapacity(allocator, 4096) catch unreachable,
        .footnotes = std.ArrayList(*const MarkdownNode).initCapacity(allocator, 8) catch unreachable,
        .depth = 0,
        .quoteDepth = 0,
    };
    defer state.output.deinit(allocator);
    defer state.footnotes.deinit(allocator);
    defer {
        var iter = state.renderers.iterator();
        while (iter.next()) |entry| {
            allocator.free(entry.key_ptr.*);
        }
    }

    try renderChildrenConsole(doc, &state, true);

    while (state.output.items.len > 0 and state.output.items[state.output.items.len - 1] == '\n') {
        _ = state.output.pop();
    }

    return state.output.toOwnedSlice(allocator);
}

fn renderChildrenConsole(node: *const MarkdownNode, state: *ConsoleRendererState, decode: bool) !void {
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

fn renderNodeConsole(node: *const MarkdownNode, state: *ConsoleRendererState, first: bool, last: bool, decode: bool) !void {
    if (state.renderers.get(node.type)) |renderer| {
        renderer.render(node, state, first, last, decode);
    }
}

fn getConsoleRenderers(allocator: std.mem.Allocator, htmlRenderers: std.StringHashMap(*const Renderer)) !std.StringHashMap(*const Renderer) {
    _ = htmlRenderers;
    var consoleRenderers = std.StringHashMap(*const Renderer).init(allocator);
    defer {
        var iter = consoleRenderers.iterator();
        while (iter.next()) |entry| {
            allocator.free(entry.key_ptr.*);
        }
    }

    try consoleRenderers.put("paragraph", consoleParagraphRenderer);
    try consoleRenderers.put("heading", consoleHeadingRenderer);
    try consoleRenderers.put("heading_underline", consoleHeadingRenderer);
    try consoleRenderers.put("thematic_break", consoleThematicBreakRenderer);
    try consoleRenderers.put("block_quote", consoleBlockQuoteRenderer);
    try consoleRenderers.put("list_bulleted", consoleListRenderer);
    try consoleRenderers.put("list_ordered", consoleListRenderer);
    try consoleRenderers.put("list_task_item", consoleListTaskItemRenderer);
    try consoleRenderers.put("code_block", consoleCodeBlockRenderer);
    try consoleRenderers.put("code_fence", consoleCodeBlockRenderer);
    try consoleRenderers.put("code_span", consoleCodeSpanRenderer);
    try consoleRenderers.put("strong", consoleInlineRenderer);
    try consoleRenderers.put("emphasis", consoleInlineRenderer);
    try consoleRenderers.put("strikethrough", consoleStrikethroughRenderer);
    try consoleRenderers.put("link", consoleLinkRenderer);
    try consoleRenderers.put("image", consoleImageRenderer);
    try consoleRenderers.put("text", consoleTextRenderer);
    try consoleRenderers.put("hard_break", consoleHardBreakRenderer);
    try consoleRenderers.put("alert", consoleAlertRenderer);
    try consoleRenderers.put("footnote", consoleFootnoteRenderer);
    try consoleRenderers.put("table", consoleTableRenderer);
    try consoleRenderers.put("html_block", consoleHtmlRenderer);
    try consoleRenderers.put("html_span", consoleHtmlRenderer);

    return consoleRenderers;
}

fn consoleParagraphRenderer(node: *const MarkdownNode, state: *ConsoleRendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    if (state.output.items.len > 0 and state.output.items[state.output.items.len - 1] != '\n') {
        state.output.append(state.allocator, '\n') catch unreachable;
    }
    if (state.output.items.len > 0 and state.output.items.len >= 2 and !std.mem.eql(u8, state.output.items[state.output.items.len - 2 ..], "\n\n")) {
        state.output.append(state.allocator, '\n') catch unreachable;
    }
    renderChildrenConsole(node, state, true) catch unreachable;
    state.output.appendSlice(state.allocator, "\n\n") catch unreachable;
}

fn consoleHeadingRenderer(node: *const MarkdownNode, state: *ConsoleRendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    var level: usize = 0;
    if (node.markup.len > 0 and node.markup[0] == '#') {
        level = node.markup.len;
    } else if (std.mem.indexOfScalar(u8, node.markup, '=')) |_| {
        level = 1;
    } else if (std.mem.indexOfScalar(u8, node.markup, '-')) |_| {
        level = 2;
    }

    if (state.output.items.len > 0 and state.output.items[state.output.items.len - 1] != '\n') {
        state.output.append(state.allocator, '\n') catch unreachable;
    }

    const style = switch (level) {
        1 => ansiBold ++ ansiCyan,
        2 => ansiBold ++ ansiBlue,
        3 => ansiBold ++ ansiMagenta,
        4 => ansiBold,
        5 => ansiDim ++ ansiBold,
        6 => ansiDim ++ ansiBold,
        else => ansiReset,
    };

    const hashes = state.allocator.dupe(u8, &[_]u8{'#'} ** level) catch unreachable;
    defer state.allocator.free(hashes);

    state.output.appendSlice(state.allocator, style) catch unreachable;
    state.output.appendSlice(state.allocator, hashes) catch unreachable;
    state.output.appendSlice(state.allocator, " ") catch unreachable;
    renderChildrenConsole(node, state, true) catch unreachable;
    state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
    state.output.append(state.allocator, '\n') catch unreachable;
}

fn consoleThematicBreakRenderer(node: *const MarkdownNode, state: *ConsoleRendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = node;
    _ = first;
    _ = last;
    _ = decode;

    if (state.output.items.len > 0 and state.output.items[state.output.items.len - 1] != '\n') {
        state.output.append(state.allocator, '\n') catch unreachable;
    }
    state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
    state.output.appendSlice(state.allocator, "─") catch unreachable;
    state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
    state.output.append(state.allocator, '\n') catch unreachable;
}

fn consoleBlockQuoteRenderer(node: *const MarkdownNode, state: *ConsoleRendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    state.quoteDepth += 1;

    if (state.output.items.len > 0 and state.output.items[state.output.items.len - 1] != '\n') {
        state.output.append(state.allocator, '\n') catch unreachable;
    }

    var content_iter = std.mem.splitScalar(u8, node.content, '\n');
    while (content_iter.next()) |line| {
        if (line.len > 0) {
            state.output.appendSlice(state.allocator, ansiGray) catch unreachable;
            state.output.appendSlice(state.allocator, "┃") catch unreachable;
            state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
            state.output.append(state.allocator, ' ') catch unreachable;
            state.output.appendSlice(state.allocator, line) catch unreachable;
            state.output.append(state.allocator, '\n') catch unreachable;
        }
    }

    if (node.children) |children| {
        for (children) |child| {
            const lines = renderNodeToString(child, state);
            defer state.allocator.free(lines);
            var line_iter = std.mem.splitScalar(u8, lines, '\n');
            while (line_iter.next()) |line| {
                if (line.len > 0) {
                    state.output.appendSlice(state.allocator, ansiGray) catch unreachable;
                    state.output.appendSlice(state.allocator, "┃") catch unreachable;
                    state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
                    state.output.append(state.allocator, ' ') catch unreachable;
                    state.output.appendSlice(state.allocator, line) catch unreachable;
                    state.output.append(state.allocator, '\n') catch unreachable;
                }
            }
        }
    }

    state.quoteDepth -= 1;
}

fn renderNodeToString(node: *const MarkdownNode, state: *ConsoleRendererState, allocator: std.mem.Allocator) []const u8 {
    _ = allocator;
    const old_output = state.output;
    state.output = std.ArrayList(u8).initCapacity(state.allocator, 1024) catch unreachable;
    defer state.output = old_output;

    if (state.renderers.get(node.type)) |renderer| {
        renderer.render(node, state, false, false, true);
    }

    return state.output.toOwnedSlice(state.allocator) catch unreachable;
}

fn consoleListRenderer(node: *const MarkdownNode, state: *ConsoleRendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    state.depth += 1;

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
            const prefix = if (ordered)
                std.fmt.allocPrint(state.allocator, "{d}.", .{counter}) catch "1."
            else
                consoleBullets[@min(state.depth - 1, consoleBullets.len - 1)];

            if (ordered) {
                counter += 1;
                state.allocator.free(prefix);
            }

            if (item.children) |itemChildren| {
                for (itemChildren, 0..) |child, i| {
                    if (!loose and std.mem.eql(u8, child.type, "paragraph")) {
                        if (i == 0) {
                            var indentBuf: [200]u8 = undefined;
                            const spaces = (state.depth - 1) * 2;
                            @memset(indentBuf[0..spaces], ' ');
                            const indent = indentBuf[0..spaces];

                            state.output.appendSlice(state.allocator, indent) catch unreachable;
                            state.output.appendSlice(state.allocator, prefix) catch unreachable;
                            state.output.append(state.allocator, ' ') catch unreachable;
                        }
                        renderChildrenConsole(child, state, true) catch unreachable;
                        state.output.append(state.allocator, '\n') catch unreachable;
                    } else {
                        if (i == 0) {
                            var indentBuf: [200]u8 = undefined;
                            const spaces = (state.depth - 1) * 2;
                            @memset(indentBuf[0..spaces], ' ');
                            const indent = indentBuf[0..spaces];

                            state.output.appendSlice(state.allocator, indent) catch unreachable;
                            state.output.appendSlice(state.allocator, prefix) catch unreachable;
                            state.output.append(state.allocator, ' ') catch unreachable;
                        }
                        if (state.renderers.get(child.type)) |renderer| {
                            renderer.render(child, state, false, false, true);
                        }
                    }
                }
            }
        }
    }

    state.depth -= 1;
}

fn isLooseList(node: *const MarkdownNode) bool {
    if (node.children) |children| {
        if (children.len > 1) {
            for (0..children.len - 1) |i| {
                const child = children[i];
                if (child.children) |grandchildren| {
                    if (grandchildren.len > 0 and grandchildren[grandchildren.len - 1].blankAfter) {
                        return true;
                    }
                }
            }
        }
    }
    return false;
}

fn consoleListTaskItemRenderer(node: *const MarkdownNode, state: *ConsoleRendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    const is_checked = node.markup.len > 1 and node.markup[1] != ' ';
    const emoji = if (is_checked) "[✓]" else "[ ]";

    state.output.appendSlice(state.allocator, emoji) catch unreachable;
    state.output.append(state.allocator, ' ') catch unreachable;
}

fn consoleCodeBlockRenderer(node: *const MarkdownNode, state: *ConsoleRendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    if (state.output.items.len > 0 and state.output.items[state.output.items.len - 1] != '\n') {
        state.output.append(state.allocator, '\n') catch unreachable;
    }

    state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
    state.output.appendSlice(state.allocator, "┌─") catch unreachable;
    state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
    state.output.append(state.allocator, '\n') catch unreachable;

    if (node.content.len > 0) {
        var iter = std.mem.splitScalar(u8, node.content, '\n');
        while (iter.next()) |line| {
            state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
            state.output.append(state.allocator, '│') catch unreachable;
            state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
            state.output.append(state.allocator, ' ') catch unreachable;
            state.output.appendSlice(state.allocator, line) catch unreachable;
            state.output.append(state.allocator, '\n') catch unreachable;
        }
    } else {
        state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
        state.output.append(state.allocator, '│') catch unreachable;
        state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
        state.output.append(state.allocator, '\n') catch unreachable;
    }

    state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
    state.output.appendSlice(state.allocator, "└─") catch unreachable;
    state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
    state.output.append(state.allocator, '\n') catch unreachable;
}

fn consoleCodeSpanRenderer(node: *const MarkdownNode, state: *ConsoleRendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    state.output.appendSlice(state.allocator, ansiGreen) catch unreachable;
    state.output.appendSlice(state.allocator, '`') catch unreachable;
    renderChildrenConsole(node, state, true) catch unreachable;
    state.output.appendSlice(state.allocator, '`') catch unreachable;
    state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
}

fn consoleInlineRenderer(node: *const MarkdownNode, state: *ConsoleRendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    const style = if (std.mem.eql(u8, node.type, "strong"))
        ansiBold ++ ansiOrange
    else if (std.mem.eql(u8, node.type, "emphasis"))
        ansiYellow
    else
        "";

    state.output.appendSlice(state.allocator, style) catch unreachable;
    renderChildrenConsole(node, state, true) catch unreachable;
    state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
}

fn consoleStrikethroughRenderer(node: *const MarkdownNode, state: *ConsoleRendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
    state.output.appendSlice(state.allocator, "~~") catch unreachable;
    renderChildrenConsole(node, state, true) catch unreachable;
    state.output.appendSlice(state.allocator, "~~") catch unreachable;
    state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
}

fn consoleLinkRenderer(node: *const MarkdownNode, state: *ConsoleRendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    state.output.appendSlice(state.allocator, ansiBlue ++ ansiUnderline) catch unreachable;
    renderChildrenConsole(node, state, true) catch unreachable;
    state.output.appendSlice(state.allocator, ansiReset) catch unreachable;

    if (node.info) |info| {
        state.output.appendSlice(state.allocator, " (") catch unreachable;
        state.output.appendSlice(state.allocator, info) catch unreachable;
        state.output.append(state.allocator, ')') catch unreachable;
    }
}

fn consoleImageRenderer(node: *const MarkdownNode, state: *ConsoleRendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    var alt: []const u8 = "";
    if (node.children) |children| {
        for (children) |child| {
            if (std.mem.eql(u8, child.type, "text")) {
                var buffer: std.ArrayList(u8) = std.ArrayList(u8).init(state.allocator);
                buffer.appendSlice(alt) catch unreachable;
                buffer.appendSlice(child.markup) catch unreachable;
                alt = buffer.toOwnedSlice(state.allocator) catch unreachable;
            }
        }
    }

    const alt_text = if (alt.len > 0) alt else if (node.info) |info| info else "";

    state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
    state.output.appendSlice(state.allocator, "[Image: ") catch unreachable;
    state.output.appendSlice(state.allocator, alt_text) catch unreachable;
    state.output.appendSlice(state.allocator, "]") catch unreachable;
    state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
}

fn consoleTextRenderer(node: *const MarkdownNode, state: *ConsoleRendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = decode;

    var text = node.markup;
    if (first orelse false) {
        text = std.mem.trimLeft(u8, text, &std.ascii.whitespace);
    }
    if (last orelse false) {
        text = std.mem.trimRight(u8, text, &std.ascii.whitespace);
    }

    state.output.appendSlice(state.allocator, text) catch unreachable;
}

fn consoleHardBreakRenderer(node: *const MarkdownNode, state: *ConsoleRendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = node;
    _ = first;
    _ = last;
    _ = decode;

    state.output.append(state.allocator, '\n') catch unreachable;
}

fn consoleAlertRenderer(node: *const MarkdownNode, state: *ConsoleRendererState, first: ?bool, last: ?bool, decode: ?bool) void {
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

fn consoleFootnoteRenderer(node: *const MarkdownNode, state: *ConsoleRendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    var exists = false;
    for (state.footnotes.items) |footnote| {
        if (footnote.info) |info| {
            if (node.info) |node_info| {
                if (std.mem.eql(u8, info, node_info)) {
                    exists = true;
                    break;
                }
            }
        }
    }

    if (!exists) {
        state.footnotes.append(node) catch unreachable;
    }

    const label = state.footnotes.items.len;

    state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
    state.output.append(state.allocator, '[') catch unreachable;
    const label_str = std.fmt.allocPrint(state.allocator, "{d}", .{label}) catch "1";
    state.output.appendSlice(state.allocator, label_str) catch unreachable;
    state.allocator.free(label_str);
    state.output.append(state.allocator, ']') catch unreachable;
    state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
}

fn consoleTableRenderer(node: *const MarkdownNode, state: *ConsoleRendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    if (state.output.items.len > 0 and state.output.items[state.output.items.len - 1] != '\n') {
        state.output.append(state.allocator, '\n') catch unreachable;
    }

    const children = node.children orelse return;

    if (children.len == 0) return;

    const headerRow = children[0];
    const dataRows = children[1..];

    const headerCells = headerRow.children orelse &[_]MarkdownNode{};

    var maxColumns: usize = headerCells.len;
    for (dataRows) |row| {
        if (row.children) |rowCells| {
            if (rowCells.len > maxColumns) {
                maxColumns = rowCells.len;
            }
        }
    }

    var columnWidths = state.allocator.alloc(usize, maxColumns) catch unreachable;
    defer state.allocator.free(columnWidths);
    @memset(columnWidths, 0);

    var cellTexts = std.ArrayList([]const []const u8).initCapacity(state.allocator, dataRows.len + 1) catch unreachable;
    defer {
        for (cellTexts.items) |row| {
            state.allocator.free(row);
        }
        cellTexts.deinit(state.allocator);
    }

    var headerTexts = state.allocator.alloc([]const u8, maxColumns) catch unreachable;
    @memset(headerTexts, "");
    try cellTexts.append(headerTexts);

    {
        var i: usize = 0;
        while (i < headerCells.len) : (i += 1) {
            const text = getTextFromNode(headerCells[i], state.allocator);
            defer state.allocator.free(text);
            headerTexts[i] = text;
            columnWidths[i] = @max(columnWidths[i], text.len + 2);
        }
    }

    {
        var r: usize = 0;
        while (r < dataRows.len) : (r += 1) {
            const row = dataRows[r];
            const rowCells = row.children orelse &[_]MarkdownNode{};
            const rowTexts = state.allocator.alloc([]const u8, maxColumns) catch unreachable;
            @memset(rowTexts, "");
            try cellTexts.append(rowTexts);

            var c: usize = 0;
            while (c < rowCells.len) : (c += 1) {
                const text = getTextFromNode(rowCells[c], state.allocator);
                defer state.allocator.free(text);
                rowTexts[c] = text;
                columnWidths[c] = @max(columnWidths[c], text.len + 2);
            }
        }
    }

    const makeLine = struct {
        fn func(left: []const u8, mid: []const u8, right: []const u8, sep: []const u8, s: *ConsoleRendererState, widths: []const usize) !void {
            s.output.appendSlice(s.allocator, ansiDim) catch unreachable;
            s.output.appendSlice(s.allocator, left) catch unreachable;
            for (0..widths.len) |i| {
                const dashes = s.allocator.dupe(u8, &[_]u8{'─'} ** widths[i]) catch unreachable;
                defer s.allocator.free(dashes);
                s.output.appendSlice(s.allocator, dashes) catch unreachable;
                if (i < widths.len - 1) {
                    s.output.appendSlice(s.allocator, if (i == 0) mid else sep) catch unreachable;
                }
            }
            s.output.appendSlice(s.allocator, right) catch unreachable;
            s.output.appendSlice(s.allocator, ansiReset) catch unreachable;
            s.output.append(s.allocator, '\n') catch unreachable;
        }
    }.func;

    try makeLine("┌", "┬", "┐", "┼", state, columnWidths);

    if (headerCells.len > 0) {
        state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
        state.output.append(state.allocator, '│') catch unreachable;
        state.output.appendSlice(state.allocator, ansiReset) catch unreachable;

        for (0..headerCells.len) |i| {
            const text = cellTexts.items[0][i];
            state.output.append(state.allocator, ' ') catch unreachable;
            state.output.appendSlice(state.allocator, text) catch unreachable;
            const padding = columnWidths[i] - text.len - 1;
            for (0..padding) |_| {
                state.output.append(state.allocator, ' ') catch unreachable;
            }
            state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
            state.output.append(state.allocator, '│') catch unreachable;
            state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
        }
        state.output.append(state.allocator, '\n') catch unreachable;
    }

    try makeLine("├", "┼", "┤", "┼", state, columnWidths);

    {
        var r: usize = 0;
        while (r < dataRows.len) : (r += 1) {
            state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
            state.output.append(state.allocator, '│') catch unreachable;
            state.output.appendSlice(state.allocator, ansiReset) catch unreachable;

            const rowTexts = cellTexts.items[r + 1];
            for (0..columnWidths.len) |c| {
                const text = if (c < rowTexts.len) rowTexts[c] else "";
                state.output.append(state.allocator, ' ') catch unreachable;
                state.output.appendSlice(state.allocator, text) catch unreachable;
                const padding = columnWidths[c] - text.len - 1;
                for (0..padding) |_| {
                    state.output.append(state.allocator, ' ') catch unreachable;
                }
                state.output.appendSlice(state.allocator, ansiDim) catch unreachable;
                state.output.append(state.allocator, '│') catch unreachable;
                state.output.appendSlice(state.allocator, ansiReset) catch unreachable;
            }
            state.output.append(state.allocator, '\n') catch unreachable;
        }
    }

    try makeLine("└", "┴", "┘", "┴", state, columnWidths);
}

fn getTextFromNode(node: *const MarkdownNode, allocator: std.mem.Allocator) []const u8 {
    if (std.mem.eql(u8, node.type, "text")) {
        return node.markup;
    }
    if (node.children) |children| {
        var buffer = std.ArrayList(u8).initCapacity(allocator, 64) catch unreachable;
        for (children) |child| {
            const text = getTextFromNode(child, allocator);
            buffer.appendSlice(text) catch unreachable;
        }
        return buffer.toOwnedSlice(allocator) catch unreachable;
    }
    return "";
}

fn consoleHtmlRenderer(node: *const MarkdownNode, state: *ConsoleRendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    state.output.appendSlice(state.allocator, node.content) catch unreachable;
}
