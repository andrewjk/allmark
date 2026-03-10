const std = @import("std");

const parse = @import("allmark").parse;
const render = @import("allmark").render;
const core = @import("allmark").core;
const gfm = @import("allmark").gfm;
const extended = @import("allmark").extended;

test "renders paragraph to console" {
    const input = "Hello, world!";
    const expected = "Hello, world!";

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders heading to console with color" {
    const input = "# Heading 1\n## Heading 2";
    const expected = "\x1b[2m#\x1b[0m \x1b[1m\x1b[35mHeading 1\x1b[0m\n\x1b[2m##\x1b[0m \x1b[1m\x1b[35mHeading 2\x1b[0m";

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders bulleted list with Unicode bullets" {
    const input = "- Item 1\n- Item 2";
    const expected = "\x1b[2m•\x1b[0m Item 1\n\x1b[2m•\x1b[0m Item 2";

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders ordered list" {
    const input = "1. First\n2. Second";
    const expected = "\x1b[2m1.\x1b[0m First\n\x1b[2m2.\x1b[0m Second";

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders code fence with box drawing" {
    const input = "```\ncode\n```";
    const expected = "\x1b[2m┌─\x1b[0m\n\x1b[2m│\x1b[0m code\n\x1b[2m└─\x1b[0m";

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders inline code" {
    const input = "`code`";
    const expected = "\x1b[32mcode\x1b[0m";

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders block quote with vertical line" {
    const input = "> Quote text";
    const expected_stripped = "┃ Quote text";

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    const stripped = try stripAnsiCodes(gpa, output);
    defer gpa.free(stripped);

    try std.testing.expectEqualStrings(expected_stripped, stripped);
}

test "renders thematic break" {
    const input = "---";
    const expected = "\x1b[2m───\x1b[0m";

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders task list" {
    const input = "- [x] Done\n- [ ] Todo";
    const expected = "\x1b[2m•\x1b[0m \x1b[2m[\x1b[0m✓\x1b[2m]\x1b[0m Done\n\x1b[2m•\x1b[0m \x1b[2m[\x1b[0m \x1b[2m]\x1b[0m Todo";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders table with Unicode borders" {
    const input = "| A | B |\n|---|---|\n| 1 | 2 |";
    const expected = "\x1b[2m┌───┬───┐\x1b[0m\n\x1b[2m│\x1b[0m A \x1b[2m│\x1b[0m B \x1b[2m│\x1b[0m\n\x1b[2m├───┼───┤\x1b[0m\n\x1b[2m│\x1b[0m 1 \x1b[2m│\x1b[0m 2 \x1b[2m│\x1b[0m\n\x1b[2m└───┴───┘\x1b[0m";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders strong text" {
    const input = "**bold**";
    const expected = "\x1b[1m\x1b[33mbold\x1b[0m";

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders emphasis text" {
    const input = "*italic*";
    const expected = "\x1b[3m\x1b[33mitalic\x1b[0m";

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders link" {
    const input = "[text](url)";
    const expected = "\x1b[4m\x1b[34mtext\x1b[0m \x1b[2m(url)\x1b[0m";

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders image" {
    const input = "![alt](url)";
    const expected = "\x1b[90m[Image: alt]\x1b[0m";

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders strikethrough" {
    const input = "~~deleted~~";
    const expected = "\x1b[2m\x1b[9mdeleted\x1b[29m\x1b[0m";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders alert with emoji" {
    const input = "> [!NOTE]\n> Note content";
    const expected = "\x1b[34m📝 Note:\x1b[0m\n\nNote content";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders nested list with different bullets" {
    const input = "- Level 1\n  - Level 2\n    - Level 3";
    const expected = "\x1b[2m•\x1b[0m Level 1\n  \x1b[2m◦\x1b[0m Level 2\n    \x1b[2m▪\x1b[0m Level 3";

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders hard break" {
    const input = "Line 1\n\nLine 2";
    const expected = "Line 1\n\nLine 2";

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders heading with underline Setext style" {
    const input = "Heading\n=======\n\nSubheading\n-------";
    const expected = "\x1b[1m\x1b[35mHeading\n\x1b[0m\x1b[2m=======\x1b[0m\n\x1b[1m\x1b[35mSubheading\n\x1b[0m\x1b[2m----------\x1b[0m";

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders HTML block" {
    const input = "<div>html</div>";
    const expected = "<div>html</div>";

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders HTML span inline" {
    const input = "test <span>html</span> test";
    const expected = "test <span>html</span> test";

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders comment" {
    const input = "<!-- comment -->";
    const expected = "<!-- comment -->";

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders deletion (strikethrough alternative)" {
    const input = "~~deleted~~";
    const expected = "\x1b[2m\x1b[9mdeleted\x1b[29m\x1b[0m";

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders footnote" {
    const input = "Text [^1]\n\n[^1]: http://example.com";
    const expected = "Text \x1b[2m[1]\x1b[0m";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders highlight" {
    const input = "==highlighted==";
    const expected = "\x1b[43m\x1b[30mhighlighted\x1b[0m";

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders insertion" {
    const input = "++inserted++";
    const expected = "++inserted++";

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
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
