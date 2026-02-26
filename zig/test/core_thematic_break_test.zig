const std = @import("std");

const parse = @import("allmark").parse;
const render = @import("allmark").render;
const core = @import("allmark").core;

test "Simple thematic break with dashes" {
    const input =
        \\---
    ;
    const expected =
        \\<hr />
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Simple thematic break with asterisks" {
    const input =
        \\***
    ;
    const expected =
        \\<hr />
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Simple thematic break with underscores" {
    const input =
        \\___
    ;
    const expected =
        \\<hr />
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break with 4 dashes" {
    const input =
        \\----
    ;
    const expected =
        \\<hr />
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break with 5 asterisks" {
    const input =
        \\*****
    ;
    const expected =
        \\<hr />
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break with spaces between characters" {
    const input =
        \\- - -
    ;
    const expected =
        \\<hr />
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break with tabs between characters" {
    const input =
        "*\t*\t*";
    const expected =
        \\<hr />
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break with 1 space indent" {
    const input =
        \\ ---
    ;
    const expected =
        \\<hr />
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break with 3 space indent" {
    const input =
        \\   ---
    ;
    const expected =
        \\<hr />
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break with 4 space indent should be code" {
    const input =
        \\    ---
    ;
    const expected =
        \\<pre><code>---
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break followed by paragraph without blank line" {
    const input =
        \\---
        \\Paragraph
    ;
    const expected =
        \\<hr />
        \\<p>Paragraph</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Multiple thematic breaks" {
    const input =
        \\---
        \\
        \\***
    ;
    const expected =
        \\<hr />
        \\<hr />
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break not valid - only 2 dashes" {
    const input =
        \\--
    ;
    const expected =
        \\<p>--</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break not valid - only 2 asterisks" {
    const input =
        \\**
    ;
    const expected =
        \\<p>**</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break not valid - only 2 underscores" {
    const input =
        \\__
    ;
    const expected =
        \\<p>__</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break not valid - mixed characters" {
    const input =
        \\-*-
    ;
    const expected =
        \\<p>-*-</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break not valid - mixed dashes and asterisks" {
    const input =
        \\---***
    ;
    const expected =
        \\<p>---***</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break in blockquote" {
    const input =
        \\> ---
    ;
    const expected =
        \\<blockquote>
        \\<hr />
        \\</blockquote>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break in list item" {
    const input =
        \\- Item
        \\---
    ;
    const expected =
        \\<ul>
        \\<li>Item</li>
        \\</ul>
        \\<hr />
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break with trailing spaces" {
    const input =
        \\---   
    ;
    const expected =
        \\<hr />
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break with trailing tabs" {
    const input =
        "***\t\t";
    const expected =
        \\<hr />
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break after list without blank line" {
    const input =
        \\- Item 1
        \\- Item 2
        \\---
    ;
    const expected =
        \\<ul>
        \\<li>Item 1</li>
        \\<li>Item 2</li>
        \\</ul>
        \\<hr />
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break before list without blank line" {
    const input =
        \\---
        \\- Item
    ;
    const expected =
        \\<hr />
        \\<ul>
        \\<li>Item</li>
        \\</ul>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break at end of document" {
    const input =
        \\> Quote
        \\---
    ;
    const expected =
        \\<blockquote>
        \\<p>Quote</p>
        \\</blockquote>
        \\<hr />
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break between paragraphs" {
    const input =
        \\Paragraph 1
        \\
        \\---
        \\
        \\Paragraph 2
    ;
    const expected =
        \\<p>Paragraph 1</p>
        \\<hr />
        \\<p>Paragraph 2</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break between paragraphs without blank lines" {
    const input =
        \\Paragraph 1
        \\
        \\---
        \\Paragraph 2
    ;
    const expected =
        \\<p>Paragraph 1</p>
        \\<hr />
        \\<p>Paragraph 2</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break after heading" {
    const input =
        \\# Heading
        \\---
    ;
    const expected =
        \\<h1>Heading</h1>
        \\<hr />
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break before heading" {
    const input =
        \\---
        \\# Heading
    ;
    const expected =
        \\<hr />
        \\<h1>Heading</h1>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break with code block above" {
    const input =
        \\```
        \\code
        \\```
        \\---
    ;
    const expected =
        \\<pre><code>code
        \\</code></pre>
        \\<hr />
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break with code block below" {
    const input =
        \\---
        \\```
        \\code
        \\```
    ;
    const expected =
        \\<hr />
        \\<pre><code>code
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break in nested blockquote" {
    const input =
        \\> Quote
        \\>
        \\> ---
        \\> More quote
    ;
    const expected =
        \\<blockquote>
        \\<p>Quote</p>
        \\<hr />
        \\<p>More quote</p>
        \\</blockquote>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break with very long sequence" {
    const input =
        \\--------------------------------------------------
    ;
    const expected =
        \\<hr />
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break not valid - starts with dash but has spaces" {
    const input =
        \\-   -   -
    ;
    const expected =
        \\<hr />
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break with inline elements above" {
    const input =
        \\Text with *emphasis*
        \\
        \\---
    ;
    const expected =
        \\<p>Text with <em>emphasis</em></p>
        \\<hr />
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break with inline elements below" {
    const input =
        \\---
        \\Text with **bold**
    ;
    const expected =
        \\<hr />
        \\<p>Text with <strong>bold</strong></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break between blockquotes" {
    const input =
        \\> Quote 1
        \\
        \\---
        \\
        \\> Quote 2
    ;
    const expected =
        \\<blockquote>
        \\<p>Quote 1</p>
        \\</blockquote>
        \\<hr />
        \\<blockquote>
        \\<p>Quote 2</p>
        \\</blockquote>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break with setext heading" {
    const input =
        \\Heading
        \\=======
        \\---
    ;
    const expected =
        \\<h1>Heading</h1>
        \\<hr />
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break after ordered list" {
    const input =
        \\1. First
        \\2. Second
        \\---
    ;
    const expected =
        \\<ol>
        \\<li>First</li>
        \\<li>Second</li>
        \\</ol>
        \\<hr />
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Empty thematic break (should not match)" {
    const input = "";
    const expected = "";

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Text that looks like thematic break but has other content" {
    const input =
        \\--- text
    ;
    const expected =
        \\<p>--- text</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break preceded by code fence" {
    const input =
        \\```
        \\code
        \\```
        \\---
    ;
    const expected =
        \\<pre><code>code
        \\</code></pre>
        \\<hr />
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break not valid - less than 3 chars with spaces" {
    const input =
        \\- -
    ;
    const expected =
        \\<ul>
        \\<li>
        \\<ul>
        \\<li></li>
        \\</ul>
        \\</li>
        \\</ul>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break after loose list" {
    const input =
        \\- Item 1
        \\
        \\- Item 2
        \\---
    ;
    const expected =
        \\<ul>
        \\<li>
        \\<p>Item 1</p>
        \\</li>
        \\<li>
        \\<p>Item 2</p>
        \\</li>
        \\</ul>
        \\<hr />
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break in fenced code block (should not be interpreted)" {
    const input =
        \\```
        \\---
        \\```
    ;
    const expected =
        \\<pre><code>---
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break with mixed spacing" {
    const input =
        \\  *  *  *  
    ;
    const expected =
        \\<hr />
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Thematic break not valid - text after spaces" {
    const input =
        \\---   text
    ;
    const expected =
        \\<p>---   text</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}
