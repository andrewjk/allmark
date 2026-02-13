const std = @import("std");

const parse = @import("allmark").parse;
const render = @import("allmark").render;
const extended = @import("allmark").extended;

test "subscript single" {
    const input =
        \\This should be ~down~ below everything else.
    ;

    const expected =
        \\<p>This should be <sub>down</sub> below everything else.</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "subscript double" {
    const input =
        \\This should be ~~down~~ below everything else.
    ;

    const expected =
        \\<p>This should be <del>down</del> below everything else.</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "subscript triple" {
    const input =
        \\This should be ~~~down~~~ below everything else.
    ;

    const expected =
        \\<p>This should be ~~~down~~~ below everything else.</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "subscript single character" {
    const input = "H~2~O";

    const expected =
        \\<p>H<sub>2</sub>O</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "subscript with numbers" {
    const input = "x~1~ + x~2~";

    const expected =
        \\<p>x<sub>1</sub> + x<sub>2</sub></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "multiple subscripts in one line" {
    const input = "a~i~ + b~j~ = c~k~";

    const expected =
        \\<p>a<sub>i</sub> + b<sub>j</sub> = c<sub>k</sub></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "subscript at start of paragraph" {
    const input = "~note~ This is important.";

    const expected =
        \\<p><sub>note</sub> This is important.</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "subscript at end of paragraph" {
    const input = "See index~1~";

    const expected =
        \\<p>See index<sub>1</sub></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "subscript with punctuation" {
    const input = "Hello~world!~";

    const expected =
        \\<p>Hello<sub>world!</sub></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "subscript with spaces" {
    const input = "text ~with spaces~ more";

    const expected =
        \\<p>text <sub>with spaces</sub> more</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "subscript with special characters" {
    const input = "math~i+j~";

    const expected =
        \\<p>math<sub>i+j</sub></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "subscript adjacent to text" {
    const input = "test~ing~test";

    const expected =
        \\<p>test<sub>ing</sub>test</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "empty subscript" {
    const input = "text~~text";

    const expected =
        \\<p>text~~text</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "subscript with markdown inside" {
    const input = "text ~**bold**~";

    const expected =
        \\<p>text <sub><strong>bold</strong></sub></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "subscript with code inside" {
    const input = "text ~`code`~";

    const expected =
        \\<p>text <sub><code>code</code></sub></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "escaped tilde should not be subscript" {
    const input = "text \\~not subscript\\~";

    const expected =
        \\<p>text ~not subscript~</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "unmatched opening tilde" {
    const input = "text ~not closed";

    const expected =
        \\<p>text ~not closed</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "unmatched closing tilde" {
    const input = "text not opened~";

    const expected =
        \\<p>text not opened~</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "subscript in list item" {
    const input = "- Item with ~subscript~";

    const expected =
        \\<ul>
        \\<li>Item with <sub>subscript</sub></li>
        \\</ul>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "subscript in blockquote" {
    const input = "> Quote with ~subscript~";

    const expected =
        \\<blockquote>
        \\<p>Quote with <sub>subscript</sub></p>
        \\</blockquote>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "strikethrough vs subscript precedence" {
    const input = "This is ~~deleted~~ text.";

    const expected =
        \\<p>This is <del>deleted</del> text.</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "subscript with tilde inside" {
    const input = "text ~tilde ~ inside~";

    const expected =
        \\<p>text <sub>tilde ~ inside</sub></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "strikethrough still works" {
    const input = "text ~~struck~~, not subscripted";

    const expected =
        \\<p>text <del>struck</del>, not subscripted</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}
