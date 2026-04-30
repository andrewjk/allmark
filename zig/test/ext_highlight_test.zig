const std = @import("std");

const parse = @import("allmark").parse;
const render = @import("allmark").render;
const extended = @import("allmark").extended;

test "highlight single" {
    const input =
        \\This should be =highlighted= as it is important.
    ;

    const expected =
        \\<p>This should be <mark>highlighted</mark> as it is important.</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "highlight double" {
    const input =
        \\This should be ==highlighted== as it is important.
    ;

    const expected =
        \\<p>This should be <mark>highlighted</mark> as it is important.</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "highlight triple" {
    const input =
        \\This should be ===highlighted=== as it is important.
    ;

    const expected =
        \\<p>This should be ===highlighted=== as it is important.</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "highlight single character" {
    const input = "text =a= more";

    const expected =
        \\<p>text <mark>a</mark> more</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "multiple highlights in one line" {
    const input = "=first= and =second= and =third=";

    const expected =
        \\<p><mark>first</mark> and <mark>second</mark> and <mark>third</mark></p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "highlight at start of paragraph" {
    const input = "=highlighted= This is important.";

    const expected =
        \\<p><mark>highlighted</mark> This is important.</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "highlight at end of paragraph" {
    const input = "This is =highlighted=";

    const expected =
        \\<p>This is <mark>highlighted</mark></p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "highlight with punctuation" {
    const input = "text =word!= more";

    const expected =
        \\<p>text <mark>word!</mark> more</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "highlight with spaces" {
    const input = "text =with spaces= more";

    const expected =
        \\<p>text <mark>with spaces</mark> more</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "highlight with special characters" {
    const input = "text =a+b= more";

    const expected =
        \\<p>text <mark>a+b</mark> more</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "highlight adjacent to text" {
    const input = "test=ing=test";

    const expected =
        \\<p>test<mark>ing</mark>test</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "empty highlight" {
    const input = "text==text";

    const expected =
        \\<p>text==text</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "highlight with markdown inside" {
    const input = "text =**bold**=";

    const expected =
        \\<p>text <mark><strong>bold</strong></mark></p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "highlight with code inside" {
    const input = "text =`code`=";

    const expected =
        \\<p>text <mark><code>code</code></mark></p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "escaped equals should not be highlight" {
    const input = "text \\=not highlight\\=";

    const expected =
        \\<p>text =not highlight=</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "unmatched opening equals" {
    const input = "text =not closed";

    const expected =
        \\<p>text =not closed</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "unmatched closing equals" {
    const input = "text not opened=";

    const expected =
        \\<p>text not opened=</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "highlight in list item" {
    const input = "- Item with =highlight=";

    const expected =
        \\<ul>
        \\<li>Item with <mark>highlight</mark></li>
        \\</ul>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "highlight in blockquote" {
    const input = "> Quote with =highlight=";

    const expected =
        \\<blockquote>
        \\<p>Quote with <mark>highlight</mark></p>
        \\</blockquote>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "highlight with equals inside" {
    const input = "text =equals = inside=";

    const expected =
        \\<p>text <mark>equals = inside</mark></p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "highlight at beginning of document" {
    const input = "=Start= of document.";

    const expected =
        \\<p><mark>Start</mark> of document.</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "highlight at end of document" {
    const input = "End of =document=";

    const expected =
        \\<p>End of <mark>document</mark></p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}
