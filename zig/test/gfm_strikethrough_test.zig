const std = @import("std");

const parse = @import("allmark").parse;
const render = @import("allmark").render;
const gfm = @import("allmark").gfm;

test "spec strikethrough" {
    const input =
        \\~~Hi~~ Hello, world!
    ;

    const expected =
        \\<p><del>Hi</del> Hello, world!</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "strikethrough single word" {
    const input = "~~deleted~~";

    const expected =
        \\<p><del>deleted</del></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "strikethrough multiple words" {
    const input = "~~this is deleted~~";

    const expected =
        \\<p><del>this is deleted</del></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "strikethrough with spaces inside" {
    const input = "~~  spaces  ~~";

    const expected =
        \\<p>~~  spaces  ~~</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "strikethrough with emphasis" {
    const input = "~~*bold and deleted*~~";

    const expected =
        \\<p><del><em>bold and deleted</em></del></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "strikethrough inside emphasis" {
    const input = "*~~deleted in italic~~*";

    const expected =
        \\<p><em><del>deleted in italic</del></em></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "strikethrough with code" {
    const input = "~~code: `var x` here~~";

    const expected =
        \\<p><del>code: <code>var x</code> here</del></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "strikethrough with link" {
    const input = "~~[link text](http://example.com)~~";

    const expected =
        \\<p><del><a href="http://example.com">link text</a></del></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "multiple strikethroughs in one line" {
    const input = "~~first~~ and ~~second~~ and ~~third~~";

    const expected =
        \\<p><del>first</del> and <del>second</del> and <del>third</del></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "strikethrough at start of paragraph" {
    const input = "~~deleted~~ followed by normal text.";

    const expected =
        \\<p><del>deleted</del> followed by normal text.</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "strikethrough at end of paragraph" {
    const input = "Normal text followed by ~~deleted~~";

    const expected =
        \\<p>Normal text followed by <del>deleted</del></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "strikethrough in list item" {
    const input = "- ~~deleted item~~\n- normal item";

    const expected =
        \\<ul>
        \\<li><del>deleted item</del></li>
        \\<li>normal item</li>
        \\</ul>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "strikethrough with tildes inside" {
    const input = "~~text with ~ tilde~~";

    const expected =
        \\<p><del>text with ~ tilde</del></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "strikethrough with multiple tildes" {
    const input = "~~~~double~~~~";

    const expected =
        \\<pre><code class="language-double~~~~"></code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "strikethrough across lines" {
    const input = "~~line one\nline two~~";

    const expected =
        \\<p><del>line one
        \\line two</del></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "strikethrough with punctuation" {
    const input = "~~Hello, world!~~";

    const expected =
        \\<p><del>Hello, world!</del></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "strikethrough with numbers" {
    const input = "~~12345~~";

    const expected =
        \\<p><del>12345</del></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "strikethrough in table cell" {
    const input = "| col1 | col2 |\n| ---- | ---- |\n| ~~deleted~~ | normal |";

    const expected =
        \\<table>
        \\<thead>
        \\<tr>
        \\<th>col1</th>
        \\<th>col2</th>
        \\</tr>
        \\</thead>
        \\<tbody>
        \\<tr>
        \\<td><del>deleted</del></td>
        \\<td>normal</td>
        \\</tr>
        \\</tbody>
        \\</table>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "strikethrough adjacent to regular text" {
    const input = "normal~~deleted~~normal";

    const expected =
        \\<p>normal<del>deleted</del>normal</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "strikethrough with escaped characters" {
    const input = "~~text with \\*asterisk\\*~~";

    const expected =
        \\<p><del>text with *asterisk*</del></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}
