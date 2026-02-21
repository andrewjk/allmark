const std = @import("std");

const parse = @import("root.zig").parse.execute;
const core = @import("rulesets/core.zig").core;
const gfm = @import("rulesets/gfm.zig").gfm;
const renderToConsole = @import("root.zig").render.renderToConsole;

test "renderConsole paragraph" {
    const input = "Hello, world!";
    const root = try parse(std.testing.allocator, input, core.ruleSet, false);
    const output = try renderToConsole(std.testing.allocator, root, core.ruleSet);
    try std.testing.expect(std.mem.indexOf(u8, output, "Hello, world!") != null);
}

test "renderConsole heading" {
    const input = "# Heading 1\n## Heading 2";
    const root = try parse(std.testing.allocator, input, core.ruleSet, false);
    const output = try renderToConsole(std.testing.allocator, root, core.ruleSet);
    try std.testing.expect(std.mem.indexOf(u8, output, "# Heading 1") != null);
    try std.testing.expect(std.mem.indexOf(u8, output, "## Heading 2") != null);
}

test "renderConsole bulleted list" {
    const input = "- Item 1\n- Item 2";
    const root = try parse(std.testing.allocator, input, core.ruleSet, false);
    const output = try renderToConsole(std.testing.allocator, root, core.ruleSet);
    try std.testing.expect(std.mem.indexOf(u8, output, "• Item 1") != null);
    try std.testing.expect(std.mem.indexOf(u8, output, "• Item 2") != null);
}

test "renderConsole ordered list" {
    const input = "1. First\n2. Second";
    const root = try parse(std.testing.allocator, input, core.ruleSet, false);
    const output = try renderToConsole(std.testing.allocator, root, core.ruleSet);
    try std.testing.expect(std.mem.indexOf(u8, output, "1. First") != null);
    try std.testing.expect(std.mem.indexOf(u8, output, "2. Second") != null);
}

test "renderConsole code block" {
    const input = "```\ncode\n```";
    const root = try parse(std.testing.allocator, input, core.ruleSet, false);
    const output = try renderToConsole(std.testing.allocator, root, core.ruleSet);
    try std.testing.expect(std.mem.indexOf(u8, output, "┌─") != null);
    try std.testing.expect(std.mem.indexOf(u8, output, "│") != null);
    try std.testing.expect(std.mem.indexOf(u8, output, "└─") != null);
}

test "renderConsole inline code" {
    const input = "`code`";
    const root = try parse(std.testing.allocator, input, core.ruleSet, false);
    const output = try renderToConsole(std.testing.allocator, root, core.ruleSet);
    try std.testing.expect(std.mem.indexOf(u8, output, "`code`") != null);
}

test "renderConsole block quote" {
    const input = "> Quote text";
    const root = try parse(std.testing.allocator, input, core.ruleSet, false);
    const output = try renderToConsole(std.testing.allocator, root, core.ruleSet);
    const stripped = stripAnsi(std.testing.allocator, output);
    defer std.testing.allocator.free(stripped);
    try std.testing.expect(std.mem.indexOf(u8, stripped, "┃ Quote text") != null);
}

fn stripAnsi(allocator: std.mem.Allocator, input: []const u8) []const u8 {
    var result = std.ArrayList(u8).initCapacity(allocator, input.len) catch unreachable;
    defer result.deinit(allocator);
    var i: usize = 0;
    while (i < input.len) {
        if (input[i] == 0x1B and i + 1 < input.len and input[i + 1] == '[') {
            var j = i + 2;
            while (j < input.len) {
                if (input[j] == 'm') {
                    i = j + 1;
                    break;
                }
                if (!std.ascii.isDigit(input[j]) and input[j] != ';') {
                    break;
                }
                j += 1;
            }
            continue;
        }
        result.append(allocator, input[i]) catch unreachable;
        i += 1;
    }
    return result.toOwnedSlice(allocator) catch unreachable;
}

test "renderConsole thematic break" {
    const input = "---";
    const root = try parse(std.testing.allocator, input, core.ruleSet, false);
    const output = try renderToConsole(std.testing.allocator, root, core.ruleSet);
    try std.testing.expect(std.mem.indexOf(u8, output, "─") != null);
}

test "renderConsole task list" {
    const input = "- [x] Done\n- [ ] Todo";
    const root = try parse(std.testing.allocator, input, gfm.ruleSet, false);
    const output = try renderToConsole(std.testing.allocator, root, gfm.ruleSet);
    try std.testing.expect(std.mem.indexOf(u8, output, "[✓]") != null);
    try std.testing.expect(std.mem.indexOf(u8, output, "[ ]") != null);
}

test "renderConsole table" {
    const input = "| A | B |\n|---|---|\n| 1 | 2 |";
    const root = try parse(std.testing.allocator, input, gfm.ruleSet, false);
    const output = try renderToConsole(std.testing.allocator, root, gfm.ruleSet);
    try std.testing.expect(std.mem.indexOf(u8, output, "┌") != null);
    try std.testing.expect(std.mem.indexOf(u8, output, "┬") != null);
    try std.testing.expect(std.mem.indexOf(u8, output, "┐") != null);
    try std.testing.expect(std.mem.indexOf(u8, output, "┼") != null);
    try std.testing.expect(std.mem.indexOf(u8, output, "│") != null);
    try std.testing.expect(std.mem.indexOf(u8, output, "├") != null);
    try std.testing.expect(std.mem.indexOf(u8, output, "┤") != null);
    try std.testing.expect(std.mem.indexOf(u8, output, "└") != null);
    try std.testing.expect(std.mem.indexOf(u8, output, "┴") != null);
    try std.testing.expect(std.mem.indexOf(u8, output, "┘") != null);
}

test "renderConsole strong text" {
    const input = "**bold**";
    const root = try parse(std.testing.allocator, input, core.ruleSet, false);
    const output = try renderToConsole(std.testing.allocator, root, core.ruleSet);
    try std.testing.expect(std.mem.indexOf(u8, output, "bold") != null);
}

test "renderConsole emphasis text" {
    const input = "*italic*";
    const root = try parse(std.testing.allocator, input, core.ruleSet, false);
    const output = try renderToConsole(std.testing.allocator, root, core.ruleSet);
    try std.testing.expect(std.mem.indexOf(u8, output, "italic") != null);
}

test "renderConsole link" {
    const input = "[text](url)";
    const root = try parse(std.testing.allocator, input, core.ruleSet, false);
    const output = try renderToConsole(std.testing.allocator, root, core.ruleSet);
    try std.testing.expect(std.mem.indexOf(u8, output, "text") != null);
    try std.testing.expect(std.mem.indexOf(u8, output, "url") != null);
}

test "renderConsole image" {
    const input = "![alt](url)";
    const root = try parse(std.testing.allocator, input, core.ruleSet, false);
    const output = try renderToConsole(std.testing.allocator, root, core.ruleSet);
    try std.testing.expect(std.mem.indexOf(u8, output, "Image") != null);
}

test "renderConsole strikethrough" {
    const input = "~~deleted~~";
    const root = try parse(std.testing.allocator, input, gfm.ruleSet, false);
    const output = try renderToConsole(std.testing.allocator, root, gfm.ruleSet);
    try std.testing.expect(std.mem.indexOf(u8, output, "~~deleted~~") != null);
}

test "renderConsole alert" {
    const input = "> [!NOTE]\n> Note content";
    const root = try parse(std.testing.allocator, input, gfm.ruleSet, false);
    const output = try renderToConsole(std.testing.allocator, root, gfm.ruleSet);
    try std.testing.expect(std.mem.indexOf(u8, output, "📝") != null);
    try std.testing.expect(std.mem.indexOf(u8, output, "Note:") != null);
}

test "renderConsole nested list" {
    const input = "- Level 1\n  - Level 2\n    - Level 3";
    const root = try parse(std.testing.allocator, input, core.ruleSet, false);
    const output = try renderToConsole(std.testing.allocator, root, core.ruleSet);
    try std.testing.expect(std.mem.indexOf(u8, output, "• Level 1") != null);
    try std.testing.expect(std.mem.indexOf(u8, output, "◦ Level 2") != null);
}
