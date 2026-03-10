const std = @import("std");

const parse = @import("allmark").parse;
const render = @import("allmark").render;
const core = @import("allmark").core;

test "Simple ordered list with period delimiter" {
    const input =
        \\1. Item
    ;
    const expected =
        \\<ol>
        \\<li>Item</li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Simple ordered list with paren delimiter" {
    const input =
        \\1) Item
    ;
    const expected =
        \\<ol>
        \\<li>Item</li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Ordered list starting at 1" {
    const input =
        \\1. Item
    ;
    const expected =
        \\<ol>
        \\<li>Item</li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Ordered list starting at 2" {
    const input =
        \\2. Item
    ;
    const expected =
        \\<ol start="2">
        \\<li>Item</li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Ordered list starting at 10" {
    const input =
        \\10. Item
    ;
    const expected =
        \\<ol start="10">
        \\<li>Item</li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Ordered list starting at 0" {
    const input =
        \\0. Item
    ;
    const expected =
        \\<ol start="0">
        \\<li>Item</li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Ordered list with large start number" {
    const input =
        \\123456789. Item
    ;
    const expected =
        \\<ol start="123456789">
        \\<li>Item</li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Ordered list with too large number (10+ digits)" {
    const input =
        \\1234567890. Item
    ;
    const expected =
        \\<p>1234567890. Item</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Ordered list with leading zeros" {
    const input =
        \\003. Item
    ;
    const expected =
        \\<ol start="3">
        \\<li>Item</li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Ordered list with multiple items" {
    const input =
        \\1. Item 1
        \\2. Item 2
        \\3. Item 3
    ;
    const expected =
        \\<ol>
        \\<li>Item 1</li>
        \\<li>Item 2</li>
        \\<li>Item 3</li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Ordered list with sequential numbers disregarded" {
    const input =
        \\1. Item 1
        \\1. Item 2
        \\1. Item 3
    ;
    const expected =
        \\<ol>
        \\<li>Item 1</li>
        \\<li>Item 2</li>
        \\<li>Item 3</li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Ordered list with mixed numbers disregarded" {
    const input =
        \\1. Item 1
        \\5. Item 2
        \\3. Item 3
    ;
    const expected =
        \\<ol>
        \\<li>Item 1</li>
        \\<li>Item 2</li>
        \\<li>Item 3</li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Tight ordered list" {
    const input =
        \\1. Item 1
        \\2. Item 2
    ;
    const expected =
        \\<ol>
        \\<li>Item 1</li>
        \\<li>Item 2</li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Loose ordered list with blank lines" {
    const input =
        \\1. Item 1
        \\
        \\2. Item 2
    ;
    const expected =
        \\<ol>
        \\<li>
        \\<p>Item 1</p>
        \\</li>
        \\<li>
        \\<p>Item 2</p>
        \\</li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Nested ordered lists" {
    const input =
        \\1. Item 1
        \\   1. Nested item
        \\2. Item 2
    ;
    const expected =
        \\<ol>
        \\<li>Item 1
        \\<ol>
        \\<li>Nested item</li>
        \\</ol>
        \\</li>
        \\<li>Item 2</li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Deep nested ordered lists" {
    const input =
        \\1. Level 1
        \\   1. Level 2
        \\      1. Level 3
    ;
    const expected =
        \\<ol>
        \\<li>Level 1
        \\<ol>
        \\<li>Level 2
        \\<ol>
        \\<li>Level 3</li>
        \\</ol>
        \\</li>
        \\</ol>
        \\</li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Ordered list in blockquote" {
    const input =
        \\> 1. Item 1
        \\> 2. Item 2
    ;
    const expected =
        \\<blockquote>
        \\<ol>
        \\<li>Item 1</li>
        \\<li>Item 2</li>
        \\</ol>
        \\</blockquote>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Empty ordered list item" {
    const input =
        \\1.
    ;
    const expected =
        \\<ol>
        \\<li></li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Ordered list with paragraphs" {
    const input =
        \\1. Item 1
        \\
        \\   Paragraph in item 1
        \\
        \\2. Item 2
    ;
    const expected =
        \\<ol>
        \\<li>
        \\<p>Item 1</p>
        \\<p>Paragraph in item 1</p>
        \\</li>
        \\<li>
        \\<p>Item 2</p>
        \\</li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Ordered list preceded by paragraph" {
    const input =
        \\Paragraph
        \\
        \\1. Item
    ;
    const expected =
        \\<p>Paragraph</p>
        \\<ol>
        \\<li>Item</li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Ordered list followed by paragraph" {
    const input =
        \\1. Item
        \\
        \\Paragraph
    ;
    const expected =
        \\<ol>
        \\<li>Item</li>
        \\</ol>
        \\<p>Paragraph</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Mixed delimiters should not be same list" {
    const input =
        \\1. Item 1
        \\1) Item 2
    ;
    const expected =
        \\<ol>
        \\<li>Item 1</li>
        \\</ol>
        \\<ol>
        \\<li>Item 2</li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Ordered list with code block" {
    const input =
        \\1. Item
        \\
        \\   ```
        \\   code
        \\   ```
    ;
    const expected =
        \\<ol>
        \\<li>
        \\<p>Item</p>
        \\<pre><code>code
        \\</code></pre>
        \\</li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Ordered list with inline formatting" {
    const input =
        \\1. Item with *emphasis*
    ;
    const expected =
        \\<ol>
        \\<li>Item with <em>emphasis</em></li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Ordered list with bold" {
    const input =
        \\1. Item with **bold**
    ;
    const expected =
        \\<ol>
        \\<li>Item with <strong>bold</strong></li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Ordered list item with multiple paragraphs (loose)" {
    const input =
        \\1. Item 1
        \\
        \\   Second paragraph
        \\
        \\2. Item 2
    ;
    const expected =
        \\<ol>
        \\<li>
        \\<p>Item 1</p>
        \\<p>Second paragraph</p>
        \\</li>
        \\<li>
        \\<p>Item 2</p>
        \\</li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Ordered list with links" {
    const input =
        \\1. [Link](https://example.com)
    ;
    const expected =
        \\<ol>
        \\<li><a href="https://example.com">Link</a></li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Ordered list with code span" {
    const input =
        \\1. `inline code`
    ;
    const expected =
        \\<ol>
        \\<li><code>inline code</code></li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Ordered list at end of document" {
    const input =
        \\1. Item 1
        \\2. Item 2
    ;
    const expected =
        \\<ol>
        \\<li>Item 1</li>
        \\<li>Item 2</li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Multiple separate ordered lists" {
    const input =
        \\1. List 1 item 1
        \\2. List 1 item 2
        \\
        \\1. List 2 item 1
        \\2. List 2 item 2
    ;
    const expected =
        \\<ol>
        \\<li>
        \\<p>List 1 item 1</p>
        \\</li>
        \\<li>
        \\<p>List 1 item 2</p>
        \\</li>
        \\<li>
        \\<p>List 2 item 1</p>
        \\</li>
        \\<li>
        \\<p>List 2 item 2</p>
        \\</li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Ordered list item with leading spaces (still a list)" {
    const input =
        \\   1. Item
    ;
    const expected =
        \\<ol>
        \\<li>Item</li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Ordered list item with 4 spaces indent should be code" {
    const input =
        \\    1. Item
    ;
    const expected =
        \\<pre><code>1. Item
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Ordered list with only spaces after marker" {
    const input =
        \\1.    Item
    ;
    const expected =
        \\<ol>
        \\<li>Item</li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Nested ordered and bulleted lists" {
    const input =
        \\1. Ordered
        \\   - Bulleted
        \\      1. Nested ordered
    ;
    const expected =
        \\<ol>
        \\<li>Ordered
        \\<ul>
        \\<li>Bulleted
        \\<ol>
        \\<li>Nested ordered</li>
        \\</ol>
        \\</li>
        \\</ul>
        \\</li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Ordered list followed immediately by bulleted list" {
    const input =
        \\1. Item 1
        \\2. Item 2
        \\- Bullet 1
        \\- Bullet 2
    ;
    const expected =
        \\<ol>
        \\<li>Item 1</li>
        \\<li>Item 2</li>
        \\</ol>
        \\<ul>
        \\<li>Bullet 1</li>
        \\<li>Bullet 2</li>
        \\</ul>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Ordered list with paren delimiter multiple items" {
    const input =
        \\1) Item 1
        \\2) Item 2
        \\3) Item 3
    ;
    const expected =
        \\<ol>
        \\<li>Item 1</li>
        \\<li>Item 2</li>
        \\<li>Item 3</li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Ordered list with paren delimiter starting at 5" {
    const input =
        \\5) Item
    ;
    const expected =
        \\<ol start="5">
        \\<li>Item</li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Ordered list item with nested bulleted list" {
    const input =
        \\1. Item
        \\   - Nested bullet
        \\   - Another bullet
    ;
    const expected =
        \\<ol>
        \\<li>Item
        \\<ul>
        \\<li>Nested bullet</li>
        \\<li>Another bullet</li>
        \\</ul>
        \\</li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Not an ordered list - text after number" {
    const input =
        \\1.5 is a number
    ;
    const expected =
        \\<p>1.5 is a number</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Not an ordered list - no space after delimiter" {
    const input =
        \\1.Item
    ;
    const expected =
        \\<p>1.Item</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Ordered list at end of line without space" {
    const input =
        \\1.
    ;
    const expected =
        \\<ol>
        \\<li></li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Ordered list with thematic break in item" {
    const input =
        \\1. Item 1
        \\
        \\   ---
        \\
        \\2. Item 2
    ;
    const expected =
        \\<ol>
        \\<li>
        \\<p>Item 1</p>
        \\<hr />
        \\</li>
        \\<li>
        \\<p>Item 2</p>
        \\</li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}
