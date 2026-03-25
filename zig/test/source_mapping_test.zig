const std = @import("std");

const parse = @import("allmark").parse;
const extended = @import("allmark").extended;

test "source mapping - heading ATX" {
    const input = "# Heading 1";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 1);
    const heading = doc.children.?[0];
    try std.testing.expectEqualStrings("heading", heading.type);
    try std.testing.expectEqual(@as(usize, 0), heading.index);
    try std.testing.expectEqual(@as(usize, 11), heading.length);
}

test "source mapping - heading ATX with multiple hashes" {
    const input = "### Heading 3";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 1);
    const heading = doc.children.?[0];
    try std.testing.expectEqualStrings("heading", heading.type);
    try std.testing.expectEqual(@as(usize, 0), heading.index);
    try std.testing.expectEqual(@as(usize, 13), heading.length);
}

test "source mapping - heading underline" {
    const input = "Heading\n=====";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 1);
    const heading = doc.children.?[0];
    try std.testing.expectEqualStrings("heading", heading.type);
    try std.testing.expectEqual(@as(usize, 0), heading.index);
    try std.testing.expectEqual(@as(usize, 13), heading.length);
}

test "source mapping - thematic break" {
    const input = "---";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 1);
    const thematicBreak = doc.children.?[0];
    try std.testing.expectEqualStrings("thematic_break", thematicBreak.type);
    try std.testing.expectEqual(@as(usize, 0), thematicBreak.index);
    try std.testing.expectEqual(@as(usize, 3), thematicBreak.length);
}

test "source mapping - alert" {
    const input = "> [!NOTE]\n> Alert content";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 1);
    const alert = doc.children.?[0];
    try std.testing.expectEqual(@as(usize, 0), alert.index);
    try std.testing.expectEqual(@as(usize, 25), alert.length);
}

test "source mapping - block quote" {
    const input = "> Quote content";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 1);
    const blockQuote = doc.children.?[0];
    try std.testing.expectEqualStrings("block_quote", blockQuote.type);
    try std.testing.expectEqual(@as(usize, 0), blockQuote.index);
    try std.testing.expectEqual(@as(usize, 15), blockQuote.length);
}

test "source mapping - code block indented" {
    const input = "\n    code\n    here";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 1);
    const codeBlock = doc.children.?[0];
    try std.testing.expectEqualStrings("code_block", codeBlock.type);
    try std.testing.expectEqual(@as(usize, 1), codeBlock.index);
    try std.testing.expectEqual(@as(usize, 17), codeBlock.length);
}

test "source mapping - code fence backticks" {
    const input = "```\ncode\n```";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 1);
    const codeFence = doc.children.?[0];
    try std.testing.expectEqualStrings("code_fence", codeFence.type);
    try std.testing.expectEqual(@as(usize, 0), codeFence.index);
    try std.testing.expectEqual(@as(usize, 12), codeFence.length);
}

test "source mapping - code fence tildes" {
    const input = "~~~\ncode\n~~~";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 1);
    const codeFence = doc.children.?[0];
    try std.testing.expectEqualStrings("code_fence", codeFence.type);
    try std.testing.expectEqual(@as(usize, 0), codeFence.index);
    try std.testing.expectEqual(@as(usize, 12), codeFence.length);
}

test "source mapping - code fence with language" {
    const input = "```javascript\ncode\n```";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 1);
    const codeFence = doc.children.?[0];
    try std.testing.expectEqualStrings("code_fence", codeFence.type);
    try std.testing.expectEqual(@as(usize, 0), codeFence.index);
    try std.testing.expectEqual(@as(usize, 22), codeFence.length);
}

test "source mapping - html block" {
    const input = "<div>content</div>";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 1);
    const htmlBlock = doc.children.?[0];
    try std.testing.expectEqualStrings("html_block", htmlBlock.type);
    try std.testing.expectEqual(@as(usize, 0), htmlBlock.index);
    try std.testing.expectEqual(@as(usize, 18), htmlBlock.length);
}

test "source mapping - html block multiline" {
    const input = "<div>\ncontent\n</div>";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 1);
    const htmlBlock = doc.children.?[0];
    try std.testing.expectEqualStrings("html_block", htmlBlock.type);
    try std.testing.expectEqual(@as(usize, 0), htmlBlock.index);
    try std.testing.expectEqual(@as(usize, 20), htmlBlock.length);
}

test "source mapping - link reference definition" {
    const input = "[link]: url";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const linkReference = doc.children.?[0];
    try std.testing.expectEqualStrings("link_ref", linkReference.type);
    try std.testing.expectEqual(@as(usize, 0), linkReference.index);
    try std.testing.expectEqual(@as(usize, 11), linkReference.length);
}

test "source mapping - list ordered" {
    const input = "1. Item one";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 1);
    const list = doc.children.?[0];
    try std.testing.expectEqualStrings("list_ordered", list.type);
    try std.testing.expectEqual(@as(usize, 0), list.index);
    try std.testing.expectEqual(@as(usize, 11), list.length);
}

test "source mapping - list bulleted" {
    const input = "- Item one";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 1);
    const list = doc.children.?[0];
    try std.testing.expectEqualStrings("list_bulleted", list.type);
    try std.testing.expectEqual(@as(usize, 0), list.index);
    try std.testing.expectEqual(@as(usize, 10), list.length);
}

test "source mapping - list item" {
    const input = "1. Item one";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 1);
    const list = doc.children.?[0];
    try std.testing.expect(list.children != null);
    try std.testing.expectEqual(@as(usize, 1), list.children.?.len);
    const listItem = list.children.?[0];
    try std.testing.expectEqualStrings("list_item", listItem.type);
    try std.testing.expectEqual(@as(usize, 0), listItem.index);
    try std.testing.expectEqual(@as(usize, 11), listItem.length);
}

test "source mapping - list task item checked" {
    const input = "- [x] Done task";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 1);
    const list = doc.children.?[0];
    try std.testing.expect(list.children != null);
    try std.testing.expectEqual(@as(usize, 1), list.children.?.len);
    const taskItem = list.children.?[0];
    try std.testing.expectEqualStrings("list_item", taskItem.type);
    try std.testing.expectEqual(@as(usize, 0), taskItem.index);
    try std.testing.expectEqual(@as(usize, 15), taskItem.length);
}

test "source mapping - list task item unchecked" {
    const input = "- [ ] Todo task";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 1);
    const list = doc.children.?[0];
    try std.testing.expect(list.children != null);
    try std.testing.expectEqual(@as(usize, 1), list.children.?.len);
    const taskItem = list.children.?[0];
    try std.testing.expectEqualStrings("list_item", taskItem.type);
    try std.testing.expectEqual(@as(usize, 0), taskItem.index);
    try std.testing.expectEqual(@as(usize, 15), taskItem.length);
}

test "source mapping - footnote reference" {
    const input = "[^1]: Footnote content";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 1);
    const footnoteReference = doc.children.?[0];
    try std.testing.expectEqualStrings("footnote_ref", footnoteReference.type);
    try std.testing.expectEqual(@as(usize, 0), footnoteReference.index);
    try std.testing.expectEqual(@as(usize, 22), footnoteReference.length);
}

test "source mapping - table" {
    const input = "| A | B |\n|---|---|\n| 1 | 2 |";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 1);
    const table = doc.children.?[0];
    try std.testing.expectEqualStrings("table", table.type);
    try std.testing.expectEqual(@as(usize, 0), table.index);
    try std.testing.expectEqual(@as(usize, 29), table.length);
}

test "source mapping - paragraph" {
    const input = "A paragraph.";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 1);
    const paragraph = doc.children.?[0];
    try std.testing.expectEqualStrings("paragraph", paragraph.type);
    try std.testing.expectEqual(@as(usize, 0), paragraph.index);
    try std.testing.expectEqual(@as(usize, 12), paragraph.length);
}

test "source mapping - indent" {
    const input = "  indented paragraph";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 1);
    const indent = doc.children.?[0];
    try std.testing.expectEqualStrings("paragraph", indent.type);
    try std.testing.expectEqual(@as(usize, 2), indent.index);
    try std.testing.expectEqual(@as(usize, 18), indent.length);
}

test "source mapping - escaped block" {
    const input = "\\# Not a heading";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 1);
    const escaped = doc.children.?[0];
    try std.testing.expectEqualStrings("paragraph", escaped.type);
    try std.testing.expectEqual(@as(usize, 0), escaped.index);
    try std.testing.expectEqual(@as(usize, 16), escaped.length);
}

test "source mapping - autolink URL" {
    const input = "# Test\n\n<https://example.com>";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 2);
    const paragraph = doc.children.?[1];
    try std.testing.expect(paragraph.children != null);
    try std.testing.expectEqual(@as(usize, 1), paragraph.children.?.len);
    const autolink = paragraph.children.?[0];
    try std.testing.expectEqualStrings("link", autolink.type);
    try std.testing.expectEqual(@as(usize, 8), autolink.index);
    try std.testing.expectEqual(@as(usize, 21), autolink.length);
}

test "source mapping - autolink email" {
    const input = "# Test\n\n<user@example.com>";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 2);
    const paragraph = doc.children.?[1];
    try std.testing.expect(paragraph.children != null);
    try std.testing.expectEqual(@as(usize, 1), paragraph.children.?.len);
    const autolink = paragraph.children.?[0];
    try std.testing.expectEqualStrings("link", autolink.type);
    try std.testing.expectEqual(@as(usize, 8), autolink.index);
    try std.testing.expectEqual(@as(usize, 18), autolink.length);
}

test "source mapping - extended autolink www" {
    const input = "# Test\n\nwww.example.com";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 2);
    const paragraph = doc.children.?[1];
    try std.testing.expect(paragraph.children != null);
    try std.testing.expectEqual(@as(usize, 1), paragraph.children.?.len);
    const extendedAutolink = paragraph.children.?[0];
    try std.testing.expectEqualStrings("link", extendedAutolink.type);
    try std.testing.expectEqual(@as(usize, 8), extendedAutolink.index);
    try std.testing.expectEqual(@as(usize, 15), extendedAutolink.length);
}

test "source mapping - code span" {
    const input = "# Test\n\n`code`";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 2);
    const paragraph = doc.children.?[1];
    try std.testing.expect(paragraph.children != null);
    try std.testing.expectEqual(@as(usize, 1), paragraph.children.?.len);
    const codeSpan = paragraph.children.?[0];
    try std.testing.expectEqualStrings("code_span", codeSpan.type);
    try std.testing.expectEqual(@as(usize, 8), codeSpan.index);
    try std.testing.expectEqual(@as(usize, 6), codeSpan.length);
}

test "source mapping - emphasis asterisk" {
    const input = "# Test\n\n*emphasis*";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 2);
    const paragraph = doc.children.?[1];
    try std.testing.expect(paragraph.children != null);
    try std.testing.expectEqual(@as(usize, 1), paragraph.children.?.len);
    const emphasis = paragraph.children.?[0];
    try std.testing.expectEqualStrings("emphasis", emphasis.type);
    try std.testing.expectEqual(@as(usize, 8), emphasis.index);
    try std.testing.expectEqual(@as(usize, 10), emphasis.length);
}

test "source mapping - emphasis underscore" {
    const input = "# Test\n\nhere: _emphasis_";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 2);
    const paragraph = doc.children.?[1];
    try std.testing.expect(paragraph.children != null);
    try std.testing.expectEqual(@as(usize, 2), paragraph.children.?.len);
    const emphasis = paragraph.children.?[1];
    try std.testing.expectEqualStrings("emphasis", emphasis.type);
    try std.testing.expectEqual(@as(usize, 14), emphasis.index);
    try std.testing.expectEqual(@as(usize, 10), emphasis.length);
}

test "source mapping - strong" {
    const input = "# Test\n\n**strong**";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 2);
    const paragraph = doc.children.?[1];
    try std.testing.expect(paragraph.children != null);
    try std.testing.expectEqual(@as(usize, 1), paragraph.children.?.len);
    const strong = paragraph.children.?[0];
    try std.testing.expectEqualStrings("strong", strong.type);
    try std.testing.expectEqual(@as(usize, 8), strong.index);
    try std.testing.expectEqual(@as(usize, 10), strong.length);
}

test "source mapping - link" {
    const input = "# Test\n\n[link](url)";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 2);
    const paragraph = doc.children.?[1];
    try std.testing.expect(paragraph.children != null);
    try std.testing.expectEqual(@as(usize, 1), paragraph.children.?.len);
    const link = paragraph.children.?[0];
    try std.testing.expectEqualStrings("link", link.type);
    try std.testing.expectEqual(@as(usize, 8), link.index);
    try std.testing.expectEqual(@as(usize, 11), link.length);
}

test "source mapping - link with title" {
    const input = "# Test\n\n[link](url \"title\")";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 2);
    const paragraph = doc.children.?[1];
    try std.testing.expect(paragraph.children != null);
    try std.testing.expectEqual(@as(usize, 1), paragraph.children.?.len);
    const link = paragraph.children.?[0];
    try std.testing.expectEqualStrings("link", link.type);
    try std.testing.expectEqual(@as(usize, 8), link.index);
    try std.testing.expectEqual(@as(usize, 19), link.length);
}

test "source mapping - footnote" {
    const input = "# Test\n\n[^1]";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 2);
    const paragraph = doc.children.?[1];
    try std.testing.expect(paragraph.children != null);
    try std.testing.expectEqual(@as(usize, 1), paragraph.children.?.len);
    const footnote = paragraph.children.?[0];
    try std.testing.expectEqual(@as(usize, 8), footnote.index);
    try std.testing.expectEqual(@as(usize, 4), footnote.length);
}

test "source mapping - hard break" {
    const input = "# Test\n\nline  \nbreak";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    //try std.testing.expect(doc.children.?.len == 2);
    const paragraph = doc.children.?[1];
    try std.testing.expect(paragraph.children != null);
    try std.testing.expectEqual(@as(usize, 3), paragraph.children.?.len);
    const hardBreak = paragraph.children.?[1];
    try std.testing.expectEqual(@as(usize, 12), hardBreak.index);
    try std.testing.expectEqual(@as(usize, 2), hardBreak.length);
}

test "source mapping - strikethrough" {
    const input = "# Test\n\n~~strikethrough~~";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 2);
    const paragraph = doc.children.?[1];
    try std.testing.expect(paragraph.children != null);
    try std.testing.expectEqual(@as(usize, 1), paragraph.children.?.len);
    const strikethrough = paragraph.children.?[0];
    try std.testing.expectEqualStrings("strikethrough", strikethrough.type);
    try std.testing.expectEqual(@as(usize, 8), strikethrough.index);
    try std.testing.expectEqual(@as(usize, 17), strikethrough.length);
}

test "source mapping - highlight" {
    const input = "# Test\n\n==highlight==";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 2);
    const paragraph = doc.children.?[1];
    try std.testing.expect(paragraph.children != null);
    try std.testing.expectEqual(@as(usize, 1), paragraph.children.?.len);
    const highlight = paragraph.children.?[0];
    try std.testing.expectEqualStrings("highlight", highlight.type);
    try std.testing.expectEqual(@as(usize, 8), highlight.index);
    try std.testing.expectEqual(@as(usize, 13), highlight.length);
}

test "source mapping - subscript" {
    const input = "# Test\n\n~subscript~";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 2);
    const paragraph = doc.children.?[1];
    try std.testing.expect(paragraph.children != null);
    try std.testing.expectEqual(@as(usize, 1), paragraph.children.?.len);
    const subscript = paragraph.children.?[0];
    try std.testing.expectEqualStrings("subscript", subscript.type);
    try std.testing.expectEqual(@as(usize, 8), subscript.index);
    try std.testing.expectEqual(@as(usize, 11), subscript.length);
}

test "source mapping - superscript" {
    const input = "# Test\n\n^superscript^";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 2);
    const paragraph = doc.children.?[1];
    try std.testing.expect(paragraph.children != null);
    try std.testing.expectEqual(@as(usize, 1), paragraph.children.?.len);
    const superscript = paragraph.children.?[0];
    try std.testing.expectEqualStrings("superscript", superscript.type);
    try std.testing.expectEqual(@as(usize, 8), superscript.index);
    try std.testing.expectEqual(@as(usize, 13), superscript.length);
}

test "source mapping - insertion" {
    const input = "# Test\n\n{++inserted++}";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 2);
    const paragraph = doc.children.?[1];
    try std.testing.expect(paragraph.children != null);
    try std.testing.expectEqual(@as(usize, 1), paragraph.children.?.len);
    const insertion = paragraph.children.?[0];
    try std.testing.expectEqualStrings("insertion", insertion.type);
    try std.testing.expectEqual(@as(usize, 8), insertion.index);
    try std.testing.expectEqual(@as(usize, 14), insertion.length);
}

test "source mapping - deletion" {
    const input = "# Test\n\ndel: {--deleted--}";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 2);
    const paragraph = doc.children.?[1];
    try std.testing.expect(paragraph.children != null);
    try std.testing.expectEqual(@as(usize, 2), paragraph.children.?.len);
    const deletion = paragraph.children.?[1];
    try std.testing.expectEqualStrings("deletion", deletion.type);
    try std.testing.expectEqual(@as(usize, 13), deletion.index);
    try std.testing.expectEqual(@as(usize, 13), deletion.length);
}

test "source mapping - html span" {
    const input = "# Test\n\n<span>content</span>";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 2);
    const paragraph = doc.children.?[1];
    try std.testing.expect(paragraph.children != null);
    try std.testing.expectEqual(@as(usize, 3), paragraph.children.?.len);
    const htmlStart = paragraph.children.?[0];
    const htmlEnd = paragraph.children.?[2];
    try std.testing.expectEqualStrings("html_span", htmlStart.type);
    try std.testing.expectEqual(@as(usize, 8), htmlStart.index);
    try std.testing.expectEqual(@as(usize, 6), htmlStart.length);
    try std.testing.expectEqualStrings("html_span", htmlEnd.type);
    try std.testing.expectEqual(@as(usize, 21), htmlEnd.index);
    try std.testing.expectEqual(@as(usize, 7), htmlEnd.length);
}

test "source mapping - comment" {
    const input = "# Test\n\n<!-- comment -->";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 2);
    const comment = doc.children.?[1];
    try std.testing.expectEqual(@as(usize, 8), comment.index);
    try std.testing.expectEqual(@as(usize, 16), comment.length);
}

test "source mapping - text" {
    const input = "# Test\n\nplain text";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 2);
    const paragraph = doc.children.?[1];
    try std.testing.expect(paragraph.children != null);
    try std.testing.expectEqual(@as(usize, 1), paragraph.children.?.len);
    const text = paragraph.children.?[0];
    try std.testing.expectEqualStrings("text", text.type);
    try std.testing.expectEqual(@as(usize, 8), text.index);
    try std.testing.expectEqual(@as(usize, 10), text.length);
}

test "source mapping - text with special chars" {
    const input = "# Test\n\ntext with & chars";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    try std.testing.expect(doc.children.?.len == 2);
    const paragraph = doc.children.?[1];
    try std.testing.expect(paragraph.children != null);
    try std.testing.expectEqual(@as(usize, 1), paragraph.children.?.len);
    const text = paragraph.children.?[0];
    try std.testing.expectEqualStrings("text", text.type);
    try std.testing.expectEqual(@as(usize, 8), text.index);
    try std.testing.expectEqual(@as(usize, 17), text.length);
}

test "source mapping - various formattings" {
    const input = "# Heading 1\n\nSome **bold** text, I'm ~~deleted~~, really {+gone+}";
    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const heading = doc.children.?[0];
    try std.testing.expectEqualStrings("heading", heading.type);
    try std.testing.expectEqual(@as(usize, 0), heading.index);
    try std.testing.expectEqual(@as(usize, 12), heading.length);

    const paragraph = doc.children.?[1];
    try std.testing.expectEqualStrings("paragraph", paragraph.type);
    try std.testing.expectEqual(@as(usize, 13), paragraph.index);
    try std.testing.expectEqual(@as(usize, 52), paragraph.length);

    const strong = paragraph.children.?[1];
    try std.testing.expectEqualStrings("strong", strong.type);
    try std.testing.expectEqual(@as(usize, 18), strong.index);
    try std.testing.expectEqual(@as(usize, 8), strong.length);

    const strikethrough = paragraph.children.?[3];
    try std.testing.expectEqualStrings("strikethrough", strikethrough.type);
    try std.testing.expectEqual(@as(usize, 37), strikethrough.index);
    try std.testing.expectEqual(@as(usize, 11), strikethrough.length);

    const insertion = paragraph.children.?[5];
    try std.testing.expectEqualStrings("insertion", insertion.type);
    try std.testing.expectEqual(@as(usize, 57), insertion.index);
    try std.testing.expectEqual(@as(usize, 8), insertion.length);
}
