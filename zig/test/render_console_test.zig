const std = @import("std");

const parse = @import("allmark").parse;
const render = @import("allmark").render;
const core = @import("allmark").core;
const gfm = @import("allmark").gfm;
const extended = @import("allmark").extended;

test "renders paragraph to console" {
    const input = "Hello, world!";
    const expected = "Hello, world!\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders paragraph then paragraph to console" {
    const input = "Hello, world!\n\nHello again";
    const expected = "Hello, world!\n\nHello again\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders heading to console with color" {
    const input = "# Heading 1\n## Heading 2";
    const expected = "\x1b[2m#\x1b[0m \x1b[1m\x1b[35mHeading 1\x1b[0m\n\n\x1b[2m##\x1b[0m \x1b[1m\x1b[35mHeading 2\x1b[0m\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders heading then heading to console" {
    const input = "# Heading 1\n## Heading 2";
    const expected = "# Heading 1\n\n## Heading 2\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    const stripped = try stripAnsiCodes(gpa, output);
    defer gpa.free(stripped);

    try std.testing.expectEqualStrings(expected, stripped);
}

test "renders paragraph x 3 to console" {
    const input = "First\n\nSecond\n\nThird";
    const expected = "First\n\nSecond\n\nThird\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    const stripped = try stripAnsiCodes(gpa, output);
    defer gpa.free(stripped);

    try std.testing.expectEqualStrings(expected, stripped);
}

test "renders heading then paragraph" {
    const input = "# Heading\n\nParagraph text";
    const expected = "# Heading\n\nParagraph text\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    const stripped = try stripAnsiCodes(gpa, output);
    defer gpa.free(stripped);

    try std.testing.expectEqualStrings(expected, stripped);
}

test "renders paragraph then heading" {
    const input = "Paragraph text\n\n# Heading";
    const expected = "Paragraph text\n\n# Heading\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    const stripped = try stripAnsiCodes(gpa, output);
    defer gpa.free(stripped);

    try std.testing.expectEqualStrings(expected, stripped);
}

test "renders heading then list" {
    const input = "# Heading\n\n- Item 1\n- Item 2";
    const expected = "# Heading\n\n• Item 1\n• Item 2\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    const stripped = try stripAnsiCodes(gpa, output);
    defer gpa.free(stripped);

    try std.testing.expectEqualStrings(expected, stripped);
}

test "renders list then heading" {
    const input = "- Item 1\n- Item 2\n\n# Heading";
    const expected = "• Item 1\n• Item 2\n\n# Heading\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    const stripped = try stripAnsiCodes(gpa, output);
    defer gpa.free(stripped);

    try std.testing.expectEqualStrings(expected, stripped);
}

test "renders paragraph then list" {
    const input = "Paragraph\n\n- Item 1\n- Item 2";
    const expected = "Paragraph\n\n• Item 1\n• Item 2\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    const stripped = try stripAnsiCodes(gpa, output);
    defer gpa.free(stripped);

    try std.testing.expectEqualStrings(expected, stripped);
}

test "renders list then paragraph" {
    const input = "- Item 1\n- Item 2\n\nParagraph";
    const expected = "• Item 1\n• Item 2\n\nParagraph\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    const stripped = try stripAnsiCodes(gpa, output);
    defer gpa.free(stripped);

    try std.testing.expectEqualStrings(expected, stripped);
}

test "renders heading then code block" {
    const input = "# Heading\n\n```\ncode\n```";
    const expected = "# Heading\n\n┌─\n│ code\n└─\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    const stripped = try stripAnsiCodes(gpa, output);
    defer gpa.free(stripped);

    try std.testing.expectEqualStrings(expected, stripped);
}

test "renders code block then heading" {
    const input = "```\ncode\n```\n\n# Heading";
    const expected = "┌─\n│ code\n└─\n\n# Heading\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    const stripped = try stripAnsiCodes(gpa, output);
    defer gpa.free(stripped);

    try std.testing.expectEqualStrings(expected, stripped);
}

test "renders heading then block quote" {
    const input = "# Heading\n\n> Quote text";
    const expected = "# Heading\n\n┃ Quote text\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    const stripped = try stripAnsiCodes(gpa, output);
    defer gpa.free(stripped);

    try std.testing.expectEqualStrings(expected, stripped);
}

test "renders block quote then heading" {
    const input = "> Quote text\n\n# Heading";
    const expected = "┃ Quote text\n\n# Heading\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    const stripped = try stripAnsiCodes(gpa, output);
    defer gpa.free(stripped);

    try std.testing.expectEqualStrings(expected, stripped);
}

test "renders heading then thematic break" {
    const input = "# Heading\n\n---";
    const expected = "# Heading\n\n───\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    const stripped = try stripAnsiCodes(gpa, output);
    defer gpa.free(stripped);

    try std.testing.expectEqualStrings(expected, stripped);
}

test "renders thematic break then heading" {
    const input = "---\n\n# Heading";
    const expected = "───\n\n# Heading\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    const stripped = try stripAnsiCodes(gpa, output);
    defer gpa.free(stripped);

    try std.testing.expectEqualStrings(expected, stripped);
}

test "renders multiple block types" {
    const input = "# Heading 1\n\nParagraph 1\n\n---\n\n## Heading 2\n\nParagraph 2";
    const expected = "# Heading 1\n\nParagraph 1\n\n───\n\n## Heading 2\n\nParagraph 2\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    const stripped = try stripAnsiCodes(gpa, output);
    defer gpa.free(stripped);

    try std.testing.expectEqualStrings(expected, stripped);
}

test "renders alert then paragraph" {
    const input = "> [!NOTE]\n> Note\n\nParagraph";
    const expected = "📝 Note:\n\nNote\n\nParagraph\n";

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    const stripped = try stripAnsiCodes(gpa, output);
    defer gpa.free(stripped);

    try std.testing.expectEqualStrings(expected, stripped);
}

test "renders paragraph then alert" {
    const input = "Paragraph\n\n> [!NOTE]\n> Note";
    const expected = "Paragraph\n\n📝 Note:\n\nNote\n";

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    const stripped = try stripAnsiCodes(gpa, output);
    defer gpa.free(stripped);

    try std.testing.expectEqualStrings(expected, stripped);
}

test "renders bulleted list with Unicode bullets" {
    const input = "- Item 1\n- Item 2";
    const expected = "\x1b[2m•\x1b[0m Item 1\n\x1b[2m•\x1b[0m Item 2\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders ordered list" {
    const input = "1. First\n2. Second";
    const expected = "\x1b[2m1.\x1b[0m First\n\x1b[2m2.\x1b[0m Second\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders tight bulleted list" {
    const input = "- Item 1\n- Item 2\n- Item 3";
    const expected = "• Item 1\n• Item 2\n• Item 3\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    const stripped = try stripAnsiCodes(gpa, output);
    defer gpa.free(stripped);

    try std.testing.expectEqualStrings(expected, stripped);
}

test "renders loose bulleted list" {
    const input = "- Item 1\n\n- Item 2\n\n- Item 3";
    const expected = "• Item 1\n\n• Item 2\n\n• Item 3\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    const stripped = try stripAnsiCodes(gpa, output);
    defer gpa.free(stripped);

    try std.testing.expectEqualStrings(expected, stripped);
}

test "renders tight ordered list" {
    const input = "1. First\n2. Second\n3. Third";
    const expected = "1. First\n2. Second\n3. Third\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    const stripped = try stripAnsiCodes(gpa, output);
    defer gpa.free(stripped);

    try std.testing.expectEqualStrings(expected, stripped);
}

test "renders loose ordered list" {
    const input = "1. First\n\n2. Second\n\n3. Third";
    const expected = "1. First\n\n2. Second\n\n3. Third\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    const stripped = try stripAnsiCodes(gpa, output);
    defer gpa.free(stripped);

    try std.testing.expectEqualStrings(expected, stripped);
}

test "renders ordered list with nested bulleted list" {
    const input = "1. First\n   - Nested A\n   - Nested B\n2. Second";
    const expected = "1. First\n  ◦ Nested A\n  ◦ Nested B\n2. Second\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    const stripped = try stripAnsiCodes(gpa, output);
    defer gpa.free(stripped);

    try std.testing.expectEqualStrings(expected, stripped);
}

test "renders bulleted list with nested ordered list" {
    const input = "- First\n  1. Nested A\n  2. Nested B\n- Second";
    const expected = "• First\n  1. Nested A\n  2. Nested B\n• Second\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    const stripped = try stripAnsiCodes(gpa, output);
    defer gpa.free(stripped);

    try std.testing.expectEqualStrings(expected, stripped);
}

test "renders code fence with box drawing" {
    const input = "```\ncode\n```";
    const expected = "\x1b[2m┌─\x1b[0m\n\x1b[2m│\x1b[0m code\n\x1b[2m└─\x1b[0m\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders inline code" {
    const input = "`code`";
    const expected = "\x1b[32mcode\x1b[0m\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders block quote with vertical line" {
    const input = "> Quote text";
    const expected_stripped = "┃ Quote text\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

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
    const expected = "\x1b[2m───\x1b[0m\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders task list" {
    const input = "- [x] Done\n- [ ] Todo";
    const expected = "\x1b[2m•\x1b[0m \x1b[2m[\x1b[0m✓\x1b[2m]\x1b[0m Done\n\x1b[2m•\x1b[0m \x1b[2m[\x1b[0m \x1b[2m]\x1b[0m Todo\n";

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders table with Unicode borders" {
    const input = "| A | B |\n|---|---|\n| 1 | 2 |";
    const expected = "\x1b[2m┌───┬───┐\x1b[0m\n\x1b[2m│\x1b[0m A \x1b[2m│\x1b[0m B \x1b[2m│\x1b[0m\n\x1b[2m├───┼───┤\x1b[0m\n\x1b[2m│\x1b[0m 1 \x1b[2m│\x1b[0m 2 \x1b[2m│\x1b[0m\n\x1b[2m└───┴───┘\x1b[0m\n";

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders table then paragraph" {
    const input = "| A |\n|---|\n| 1 |\n\nParagraph";
    const expected = "┌───┐\n│ A │\n├───┤\n│ 1 │\n└───┘\nParagraph\n";

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    const stripped = try stripAnsiCodes(gpa, output);
    defer gpa.free(stripped);

    try std.testing.expectEqualStrings(expected, stripped);
}

test "renders paragraph then table" {
    const input = "Paragraph\n\n| A |\n|---|\n| 1 |";
    const expected = "Paragraph\n\n┌───┐\n│ A │\n├───┤\n│ 1 │\n└───┘\n";

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    const stripped = try stripAnsiCodes(gpa, output);
    defer gpa.free(stripped);

    try std.testing.expectEqualStrings(expected, stripped);
}

test "renders table with padding" {
    const input = "| A | B |\n| - | - |\n| 1 | hello |";
    const expected =
        \\┌───┬───────┐
        \\│ A │ B     │
        \\├───┼───────┤
        \\│ 1 │ hello │
        \\└───┴───────┘
    ;
    const expected_with_newline = expected ++ "\n";

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    const stripped = try stripAnsiCodes(gpa, output);
    defer gpa.free(stripped);

    try std.testing.expectEqualStrings(expected_with_newline, stripped);
}

test "renders table with correctly aligned padding" {
    const input = "| A | B |\n| - | -: |\n| x | 1 |\n| y | 200 |";
    const expected =
        \\┌───┬─────┐
        \\│ A │   B │
        \\├───┼─────┤
        \\│ x │   1 │
        \\│ y │ 200 │
        \\└───┴─────┘
    ;
    const expected_with_newline = expected ++ "\n";

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    const stripped = try stripAnsiCodes(gpa, output);
    defer gpa.free(stripped);

    try std.testing.expectEqualStrings(expected_with_newline, stripped);
}

test "renders strong text" {
    const input = "**bold**";
    const expected = "\x1b[1m\x1b[33mbold\x1b[0m\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders emphasis text" {
    const input = "*italic*";
    const expected = "\x1b[3m\x1b[33mitalic\x1b[0m\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders link" {
    const input = "[text](url)";
    const expected = "\x1b[4m\x1b[34mtext\x1b[0m \x1b[2m(url)\x1b[0m\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders image" {
    const input = "![alt](url)";
    const expected = "\x1b[90m[Image: alt]\x1b[0m\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders strikethrough" {
    const input = "~~deleted~~";
    const expected = "\x1b[2m\x1b[9mdeleted\x1b[29m\x1b[0m\n";

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders alert with emoji" {
    const input = "> [!NOTE]\n> Note content";
    const expected = "\x1b[34m📝 Note:\x1b[0m\n\nNote content\n";

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders nested list with different bullets" {
    const input = "- Level 1\n  - Level 2\n    - Level 3";
    const expected = "\x1b[2m•\x1b[0m Level 1\n  \x1b[2m◦\x1b[0m Level 2\n    \x1b[2m▪\x1b[0m Level 3\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders hard break" {
    const input = "Line 1\n\nLine 2";
    const expected = "Line 1\n\nLine 2\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders heading with underline Setext style" {
    const input = "Heading\n=======\n\nSubheading\n-------";
    const expected = "\x1b[1m\x1b[35mHeading\n\x1b[0m\x1b[2m=======\x1b[0m\n\n\x1b[1m\x1b[35mSubheading\n\x1b[0m\x1b[2m----------\x1b[0m\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders HTML block" {
    const input = "<div>html</div>";
    const expected = "<div>html</div>\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders HTML span inline" {
    const input = "test <span>html</span> test";
    const expected = "test <span>html</span> test\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders comment" {
    const input = "<!-- comment -->";
    const expected = "<!-- comment -->\n";

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders deletion (strikethrough alternative)" {
    const input = "~~deleted~~";
    const expected = "\x1b[2m\x1b[9mdeleted\x1b[29m\x1b[0m\n";

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders footnote" {
    const input = "Text [^1]\n\n[^1]: http://example.com";
    const expected = "Text \x1b[2m[1]\x1b[0m\n\n\n\x1b[2m---\x1b[0m\n\x1b[2m[1]\x1b[0m \x1b[4m\x1b[34mhttp://example.com\x1b[0m \x1b[2m(http://example.com)\x1b[0m\n";

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders highlight" {
    const input = "==highlighted==";
    const expected = "\x1b[43m\x1b[30mhighlighted\x1b[0m\n";

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "renders insertion" {
    const input = "++inserted++";
    const expected = "++inserted++\n";

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    try std.testing.expectEqualStrings(expected, output);
}

test "basic parse and render" {
    const input =
        \\# Test
        \\
        \\Here is some text
        \\
        \\* Tight item 1
        \\  * Nested item 1
        \\* Tight item 2
        \\
        \\- Loose item 1
        \\
        \\- Loose item 2
        \\
        \\## Subtest
        \\
        \\Here is some more text
    ;
    const expected =
        \\# Test
        \\
        \\Here is some text
        \\
        \\• Tight item 1
        \\  ◦ Nested item 1
        \\• Tight item 2
        \\
        \\• Loose item 1
        \\
        \\• Loose item 2
        \\
        \\## Subtest
        \\
        \\Here is some more text
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    const stripped = try stripAnsiCodes(gpa, output);
    defer gpa.free(stripped);

    try std.testing.expectEqualStrings(expected, stripped);
}

test "renders nested and spaced lists" {
    const input =
        \\1. Item one
        \\2. Item two
        \\   - child one
        \\   - child two
        \\
        \\3. Item three
        \\4. Item four
    ;
    const expected =
        \\1. Item one
        \\
        \\2. Item two
        \\
        \\  ◦ child one
        \\  ◦ child two
        \\
        \\3. Item three
        \\
        \\4. Item four
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const output = try render(gpa, doc, null, true);
    defer gpa.free(output);

    const stripped = try stripAnsiCodes(gpa, output);
    defer gpa.free(stripped);

    try std.testing.expectEqualStrings(expected, stripped);
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
