const std = @import("std");

const parse = @import("allmark").parse;
const render = @import("allmark").render;
const gfm = @import("allmark").gfm;

test "Example 1, line 368: '→foo→baz→→bim'" {
    const input =
        "\tfoo\tbaz\t\tbim\n";
    const expected =
        "<pre><code>foo\tbaz\t\tbim\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 2, line 375: '  →foo→baz→→bim'" {
    const input =
        "  \tfoo\tbaz\t\tbim\n";
    const expected =
        "<pre><code>foo\tbaz\t\tbim\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 3, line 382: '    a→a\\n    ὐ→a'" {
    const input =
        "    a\ta\n" ++
        "    ὐ\ta\n";
    const expected =
        "<pre><code>a\ta\n" ++
        "ὐ\ta\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 4, line 395: '  - foo\\n\\n→bar'" {
    const input =
        "  - foo\n" ++
        "\n" ++
        "\tbar\n";
    const expected =
        "<ul>\n" ++
        "<li>\n" ++
        "<p>foo</p>\n" ++
        "<p>bar</p>\n" ++
        "</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 5, line 408: '- foo\\n\\n→→bar'" {
    const input =
        "- foo\n" ++
        "\n" ++
        "\t\tbar\n";
    const expected =
        "<ul>\n" ++
        "<li>\n" ++
        "<p>foo</p>\n" ++
        "<pre><code>  bar\n" ++
        "</code></pre>\n" ++
        "</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 6, line 431: '>→→foo'" {
    const input =
        ">\t\tfoo\n";
    const expected =
        "<blockquote>\n" ++
        "<pre><code>  foo\n" ++
        "</code></pre>\n" ++
        "</blockquote>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 7, line 440: '-→→foo'" {
    const input =
        "-\t\tfoo\n";
    const expected =
        "<ul>\n" ++
        "<li>\n" ++
        "<pre><code>  foo\n" ++
        "</code></pre>\n" ++
        "</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 8, line 452: '    foo\\n→bar'" {
    const input =
        "    foo\n" ++
        "\tbar\n";
    const expected =
        "<pre><code>foo\n" ++
        "bar\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 9, line 461: ' - foo\\n   - bar\\n→ - baz'" {
    const input =
        " - foo\n" ++
        "   - bar\n" ++
        "\t - baz\n";
    const expected =
        "<ul>\n" ++
        "<li>foo\n" ++
        "<ul>\n" ++
        "<li>bar\n" ++
        "<ul>\n" ++
        "<li>baz</li>\n" ++
        "</ul>\n" ++
        "</li>\n" ++
        "</ul>\n" ++
        "</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 10, line 479: '#→Foo'" {
    const input =
        "#\tFoo\n";
    const expected =
        "<h1>Foo</h1>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 11, line 485: '*→*→*→'" {
    const input =
        "*\t*\t*\t\n";
    const expected =
        "<hr />\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 12, line 512: '- `one\\n- two`'" {
    const input =
        "- `one\n" ++
        "- two`\n";
    const expected =
        "<ul>\n" ++
        "<li>`one</li>\n" ++
        "<li>two`</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 13, line 551: '***\\n---\\n___'" {
    const input =
        "***\n" ++
        "---\n" ++
        "___\n";
    const expected =
        "<hr />\n" ++
        "<hr />\n" ++
        "<hr />\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 14, line 564: '+++'" {
    const input =
        "+++\n";
    const expected =
        "<p>+++</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 15, line 571: '==='" {
    const input =
        "===\n";
    const expected =
        "<p>===</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 16, line 580: '--\\n**\\n__'" {
    const input =
        "--\n" ++
        "**\n" ++
        "__\n";
    const expected =
        "<p>--\n" ++
        "**\n" ++
        "__</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 17, line 593: ' ***\\n  ***\\n   ***'" {
    const input =
        " ***\n" ++
        "  ***\n" ++
        "   ***\n";
    const expected =
        "<hr />\n" ++
        "<hr />\n" ++
        "<hr />\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 18, line 606: '    ***'" {
    const input =
        "    ***\n";
    const expected =
        "<pre><code>***\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 19, line 614: 'Foo\\n    ***'" {
    const input =
        "Foo\n" ++
        "    ***\n";
    const expected =
        "<p>Foo\n" ++
        "***</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 20, line 625: '_____________________________________'" {
    const input =
        "_____________________________________\n";
    const expected =
        "<hr />\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 21, line 634: ' - - -'" {
    const input =
        " - - -\n";
    const expected =
        "<hr />\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 22, line 641: ' **  * ** * ** * **'" {
    const input =
        " **  * ** * ** * **\n";
    const expected =
        "<hr />\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 23, line 648: '-     -      -      -'" {
    const input =
        "-     -      -      -\n";
    const expected =
        "<hr />\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 24, line 657: '- - - -    '" {
    const input =
        "- - - -    \n";
    const expected =
        "<hr />\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 25, line 666: '_ _ _ _ a\\n\\na------\\n\\n---a---'" {
    const input =
        "_ _ _ _ a\n" ++
        "\n" ++
        "a------\n" ++
        "\n" ++
        "---a---\n";
    const expected =
        "<p>_ _ _ _ a</p>\n" ++
        "<p>a------</p>\n" ++
        "<p>---a---</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 26, line 682: ' *-*'" {
    const input =
        " *-*\n";
    const expected =
        "<p><em>-</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 27, line 691: '- foo\\n***\\n- bar'" {
    const input =
        "- foo\n" ++
        "***\n" ++
        "- bar\n";
    const expected =
        "<ul>\n" ++
        "<li>foo</li>\n" ++
        "</ul>\n" ++
        "<hr />\n" ++
        "<ul>\n" ++
        "<li>bar</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 28, line 708: 'Foo\\n***\\nbar'" {
    const input =
        "Foo\n" ++
        "***\n" ++
        "bar\n";
    const expected =
        "<p>Foo</p>\n" ++
        "<hr />\n" ++
        "<p>bar</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 29, line 725: 'Foo\\n---\\nbar'" {
    const input =
        "Foo\n" ++
        "---\n" ++
        "bar\n";
    const expected =
        "<h2>Foo</h2>\n" ++
        "<p>bar</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 30, line 738: '* Foo\\n* * *\\n* Bar'" {
    const input =
        "* Foo\n" ++
        "* * *\n" ++
        "* Bar\n";
    const expected =
        "<ul>\n" ++
        "<li>Foo</li>\n" ++
        "</ul>\n" ++
        "<hr />\n" ++
        "<ul>\n" ++
        "<li>Bar</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 31, line 755: '- Foo\\n- * * *'" {
    const input =
        "- Foo\n" ++
        "- * * *\n";
    const expected =
        "<ul>\n" ++
        "<li>Foo</li>\n" ++
        "<li>\n" ++
        "<hr />\n" ++
        "</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 32, line 784: '# foo\\n## foo\\n### foo\\n#### foo\\n##### foo\\n###### foo'" {
    const input =
        "# foo\n" ++
        "## foo\n" ++
        "### foo\n" ++
        "#### foo\n" ++
        "##### foo\n" ++
        "###### foo\n";
    const expected =
        "<h1>foo</h1>\n" ++
        "<h2>foo</h2>\n" ++
        "<h3>foo</h3>\n" ++
        "<h4>foo</h4>\n" ++
        "<h5>foo</h5>\n" ++
        "<h6>foo</h6>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 33, line 803: '####### foo'" {
    const input =
        "####### foo\n";
    const expected =
        "<p>####### foo</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 34, line 818: '#5 bolt\\n\\n#hashtag'" {
    const input =
        "#5 bolt\n" ++
        "\n" ++
        "#hashtag\n";
    const expected =
        "<p>#5 bolt</p>\n" ++
        "<p>#hashtag</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 35, line 830: '\\## foo'" {
    const input =
        "\\## foo\n";
    const expected =
        "<p>## foo</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 36, line 839: '# foo *bar* \\*baz\\*'" {
    const input =
        "# foo *bar* \\*baz\\*\n";
    const expected =
        "<h1>foo <em>bar</em> *baz*</h1>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 37, line 848: '#                  foo                     '" {
    const input =
        "#                  foo                     \n";
    const expected =
        "<h1>foo</h1>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 38, line 857: ' ### foo\\n  ## foo\\n   # foo'" {
    const input =
        " ### foo\n" ++
        "  ## foo\n" ++
        "   # foo\n";
    const expected =
        "<h3>foo</h3>\n" ++
        "<h2>foo</h2>\n" ++
        "<h1>foo</h1>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 39, line 870: '    # foo'" {
    const input =
        "    # foo\n";
    const expected =
        "<pre><code># foo\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 40, line 878: 'foo\\n    # bar'" {
    const input =
        "foo\n" ++
        "    # bar\n";
    const expected =
        "<p>foo\n" ++
        "# bar</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 41, line 889: '## foo ##\\n  ###   bar    ###'" {
    const input =
        "## foo ##\n" ++
        "  ###   bar    ###\n";
    const expected =
        "<h2>foo</h2>\n" ++
        "<h3>bar</h3>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 42, line 900: '# foo ##################################\\n##### foo ##'" {
    const input =
        "# foo ##################################\n" ++
        "##### foo ##\n";
    const expected =
        "<h1>foo</h1>\n" ++
        "<h5>foo</h5>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 43, line 911: '### foo ###     '" {
    const input =
        "### foo ###     \n";
    const expected =
        "<h3>foo</h3>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 44, line 922: '### foo ### b'" {
    const input =
        "### foo ### b\n";
    const expected =
        "<h3>foo ### b</h3>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 45, line 931: '# foo#'" {
    const input =
        "# foo#\n";
    const expected =
        "<h1>foo#</h1>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 46, line 941: '### foo \\###\\n## foo #\\##\\n# foo \\#'" {
    const input =
        "### foo \\###\n" ++
        "## foo #\\##\n" ++
        "# foo \\#\n";
    const expected =
        "<h3>foo ###</h3>\n" ++
        "<h2>foo ###</h2>\n" ++
        "<h1>foo #</h1>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 47, line 955: '****\\n## foo\\n****'" {
    const input =
        "****\n" ++
        "## foo\n" ++
        "****\n";
    const expected =
        "<hr />\n" ++
        "<h2>foo</h2>\n" ++
        "<hr />\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 48, line 966: 'Foo bar\\n# baz\\nBar foo'" {
    const input =
        "Foo bar\n" ++
        "# baz\n" ++
        "Bar foo\n";
    const expected =
        "<p>Foo bar</p>\n" ++
        "<h1>baz</h1>\n" ++
        "<p>Bar foo</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 49, line 979: '## \\n#\\n### ###'" {
    const input =
        "## \n" ++
        "#\n" ++
        "### ###\n";
    const expected =
        "<h2></h2>\n" ++
        "<h1></h1>\n" ++
        "<h3></h3>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 50, line 1019: 'Foo *bar*\\n=========\\n\\nFoo *bar*\\n---------'" {
    const input =
        "Foo *bar*\n" ++
        "=========\n" ++
        "\n" ++
        "Foo *bar*\n" ++
        "---------\n";
    const expected =
        "<h1>Foo <em>bar</em></h1>\n" ++
        "<h2>Foo <em>bar</em></h2>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 51, line 1033: 'Foo *bar\\nbaz*\\n===='" {
    const input =
        "Foo *bar\n" ++
        "baz*\n" ++
        "====\n";
    const expected =
        "<h1>Foo <em>bar\n" ++
        "baz</em></h1>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 52, line 1047: '  Foo *bar\\nbaz*→\\n===='" {
    const input =
        "  Foo *bar\n" ++
        "baz*\t\n" ++
        "====\n";
    const expected =
        "<h1>Foo <em>bar\n" ++
        "baz</em></h1>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 53, line 1059: 'Foo\\n-------------------------\\n\\nFoo\\n='" {
    const input =
        "Foo\n" ++
        "-------------------------\n" ++
        "\n" ++
        "Foo\n" ++
        "=\n";
    const expected =
        "<h2>Foo</h2>\n" ++
        "<h1>Foo</h1>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 54, line 1074: '   Foo\\n---\\n\\n  Foo\\n-----\\n\\n  Foo\\n  ==='" {
    const input =
        "   Foo\n" ++
        "---\n" ++
        "\n" ++
        "  Foo\n" ++
        "-----\n" ++
        "\n" ++
        "  Foo\n" ++
        "  ===\n";
    const expected =
        "<h2>Foo</h2>\n" ++
        "<h2>Foo</h2>\n" ++
        "<h1>Foo</h1>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 55, line 1092: '    Foo\\n    ---\\n\\n    Foo\\n---'" {
    const input =
        "    Foo\n" ++
        "    ---\n" ++
        "\n" ++
        "    Foo\n" ++
        "---\n";
    const expected =
        "<pre><code>Foo\n" ++
        "---\n" ++
        "\n" ++
        "Foo\n" ++
        "</code></pre>\n" ++
        "<hr />\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 56, line 1111: 'Foo\\n   ----      '" {
    const input =
        "Foo\n" ++
        "   ----      \n";
    const expected =
        "<h2>Foo</h2>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 57, line 1121: 'Foo\\n    ---'" {
    const input =
        "Foo\n" ++
        "    ---\n";
    const expected =
        "<p>Foo\n" ++
        "---</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 58, line 1132: 'Foo\\n= =\\n\\nFoo\\n--- -'" {
    const input =
        "Foo\n" ++
        "= =\n" ++
        "\n" ++
        "Foo\n" ++
        "--- -\n";
    const expected =
        "<p>Foo\n" ++
        "= =</p>\n" ++
        "<p>Foo</p>\n" ++
        "<hr />\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 59, line 1148: 'Foo  \\n-----'" {
    const input =
        "Foo  \n" ++
        "-----\n";
    const expected =
        "<h2>Foo</h2>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 60, line 1158: 'Foo\\\\n----'" {
    const input =
        "Foo\\\n" ++
        "----\n";
    const expected =
        "<h2>Foo\\</h2>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 61, line 1169: '`Foo\\n----\\n`\\n\\n<a title=\"a lot\\n---\\nof dashes\"/>'" {
    const input =
        "`Foo\n" ++
        "----\n" ++
        "`\n" ++
        "\n" ++
        "<a title=\"a lot\n" ++
        "---\n" ++
        "of dashes\"/>\n";
    const expected =
        "<h2>`Foo</h2>\n" ++
        "<p>`</p>\n" ++
        "<h2>&lt;a title=&quot;a lot</h2>\n" ++
        "<p>of dashes&quot;/&gt;</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 62, line 1188: '> Foo\\n---'" {
    const input =
        "> Foo\n" ++
        "---\n";
    const expected =
        "<blockquote>\n" ++
        "<p>Foo</p>\n" ++
        "</blockquote>\n" ++
        "<hr />\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 63, line 1199: '> foo\\nbar\\n==='" {
    const input =
        "> foo\n" ++
        "bar\n" ++
        "===\n";
    const expected =
        "<blockquote>\n" ++
        "<p>foo\n" ++
        "bar\n" ++
        "===</p>\n" ++
        "</blockquote>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 64, line 1212: '- Foo\\n---'" {
    const input =
        "- Foo\n" ++
        "---\n";
    const expected =
        "<ul>\n" ++
        "<li>Foo</li>\n" ++
        "</ul>\n" ++
        "<hr />\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 65, line 1227: 'Foo\\nBar\\n---'" {
    const input =
        "Foo\n" ++
        "Bar\n" ++
        "---\n";
    const expected =
        "<h2>Foo\n" ++
        "Bar</h2>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 66, line 1240: '---\\nFoo\\n---\\nBar\\n---\\nBaz'" {
    const input =
        "---\n" ++
        "Foo\n" ++
        "---\n" ++
        "Bar\n" ++
        "---\n" ++
        "Baz\n";
    const expected =
        "<hr />\n" ++
        "<h2>Foo</h2>\n" ++
        "<h2>Bar</h2>\n" ++
        "<p>Baz</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 67, line 1257: '\\n===='" {
    const input =
        "\n" ++
        "====\n";
    const expected =
        "<p>====</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 68, line 1269: '---\\n---'" {
    const input =
        "---\n" ++
        "---\n";
    const expected =
        "<hr />\n" ++
        "<hr />\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 69, line 1278: '- foo\\n-----'" {
    const input =
        "- foo\n" ++
        "-----\n";
    const expected =
        "<ul>\n" ++
        "<li>foo</li>\n" ++
        "</ul>\n" ++
        "<hr />\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 70, line 1289: '    foo\\n---'" {
    const input =
        "    foo\n" ++
        "---\n";
    const expected =
        "<pre><code>foo\n" ++
        "</code></pre>\n" ++
        "<hr />\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 71, line 1299: '> foo\\n-----'" {
    const input =
        "> foo\n" ++
        "-----\n";
    const expected =
        "<blockquote>\n" ++
        "<p>foo</p>\n" ++
        "</blockquote>\n" ++
        "<hr />\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 72, line 1313: '\\> foo\\n------'" {
    const input =
        "\\> foo\n" ++
        "------\n";
    const expected =
        "<h2>&gt; foo</h2>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 73, line 1344: 'Foo\\n\\nbar\\n---\\nbaz'" {
    const input =
        "Foo\n" ++
        "\n" ++
        "bar\n" ++
        "---\n" ++
        "baz\n";
    const expected =
        "<p>Foo</p>\n" ++
        "<h2>bar</h2>\n" ++
        "<p>baz</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 74, line 1360: 'Foo\\nbar\\n\\n---\\n\\nbaz'" {
    const input =
        "Foo\n" ++
        "bar\n" ++
        "\n" ++
        "---\n" ++
        "\n" ++
        "baz\n";
    const expected =
        "<p>Foo\n" ++
        "bar</p>\n" ++
        "<hr />\n" ++
        "<p>baz</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 75, line 1378: 'Foo\\nbar\\n* * *\\nbaz'" {
    const input =
        "Foo\n" ++
        "bar\n" ++
        "* * *\n" ++
        "baz\n";
    const expected =
        "<p>Foo\n" ++
        "bar</p>\n" ++
        "<hr />\n" ++
        "<p>baz</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 76, line 1393: 'Foo\\nbar\\n\\---\\nbaz'" {
    const input =
        "Foo\n" ++
        "bar\n" ++
        "\\---\n" ++
        "baz\n";
    const expected =
        "<p>Foo\n" ++
        "bar\n" ++
        "---\n" ++
        "baz</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 77, line 1421: '    a simple\\n      indented code block'" {
    const input =
        "    a simple\n" ++
        "      indented code block\n";
    const expected =
        "<pre><code>a simple\n" ++
        "  indented code block\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 78, line 1435: '  - foo\\n\\n    bar'" {
    const input =
        "  - foo\n" ++
        "\n" ++
        "    bar\n";
    const expected =
        "<ul>\n" ++
        "<li>\n" ++
        "<p>foo</p>\n" ++
        "<p>bar</p>\n" ++
        "</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 79, line 1449: '1.  foo\\n\\n    - bar'" {
    const input =
        "1.  foo\n" ++
        "\n" ++
        "    - bar\n";
    const expected =
        "<ol>\n" ++
        "<li>\n" ++
        "<p>foo</p>\n" ++
        "<ul>\n" ++
        "<li>bar</li>\n" ++
        "</ul>\n" ++
        "</li>\n" ++
        "</ol>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 80, line 1469: '    <a/>\\n    *hi*\\n\\n    - one'" {
    const input =
        "    <a/>\n" ++
        "    *hi*\n" ++
        "\n" ++
        "    - one\n";
    const expected =
        "<pre><code>&lt;a/&gt;\n" ++
        "*hi*\n" ++
        "\n" ++
        "- one\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 81, line 1485: '    chunk1\\n\\n    chunk2\\n  \\n \\n \\n    chunk3'" {
    const input =
        "    chunk1\n" ++
        "\n" ++
        "    chunk2\n" ++
        "  \n" ++
        " \n" ++
        " \n" ++
        "    chunk3\n";
    const expected =
        "<pre><code>chunk1\n" ++
        "\n" ++
        "chunk2\n" ++
        "\n" ++
        "\n" ++
        "\n" ++
        "chunk3\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 82, line 1508: '    chunk1\\n      \\n      chunk2'" {
    const input =
        "    chunk1\n" ++
        "      \n" ++
        "      chunk2\n";
    const expected =
        "<pre><code>chunk1\n" ++
        "  \n" ++
        "  chunk2\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 83, line 1523: 'Foo\\n    bar\\n'" {
    const input =
        "Foo\n" ++
        "    bar\n" ++
        "\n";
    const expected =
        "<p>Foo\n" ++
        "bar</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 84, line 1537: '    foo\\nbar'" {
    const input =
        "    foo\n" ++
        "bar\n";
    const expected =
        "<pre><code>foo\n" ++
        "</code></pre>\n" ++
        "<p>bar</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 85, line 1550: '# Heading\\n    foo\\nHeading\\n------\\n    foo\\n----'" {
    const input =
        "# Heading\n" ++
        "    foo\n" ++
        "Heading\n" ++
        "------\n" ++
        "    foo\n" ++
        "----\n";
    const expected =
        "<h1>Heading</h1>\n" ++
        "<pre><code>foo\n" ++
        "</code></pre>\n" ++
        "<h2>Heading</h2>\n" ++
        "<pre><code>foo\n" ++
        "</code></pre>\n" ++
        "<hr />\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 86, line 1570: '        foo\\n    bar'" {
    const input =
        "        foo\n" ++
        "    bar\n";
    const expected =
        "<pre><code>    foo\n" ++
        "bar\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 87, line 1583: '\\n    \\n    foo\\n    \\n'" {
    const input =
        "\n" ++
        "    \n" ++
        "    foo\n" ++
        "    \n" ++
        "\n";
    const expected =
        "<pre><code>foo\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 88, line 1597: '    foo  '" {
    const input =
        "    foo  \n";
    const expected =
        "<pre><code>foo  \n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 89, line 1652: '```\\n<\\n >\\n```'" {
    const input =
        "```\n" ++
        "<\n" ++
        " >\n" ++
        "```\n";
    const expected =
        "<pre><code>&lt;\n" ++
        " &gt;\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 90, line 1666: '~~~\\n<\\n >\\n~~~'" {
    const input =
        "~~~\n" ++
        "<\n" ++
        " >\n" ++
        "~~~\n";
    const expected =
        "<pre><code>&lt;\n" ++
        " &gt;\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 91, line 1679: '``\\nfoo\\n``'" {
    const input =
        "``\n" ++
        "foo\n" ++
        "``\n";
    const expected =
        "<p><code>foo</code></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 92, line 1690: '```\\naaa\\n~~~\\n```'" {
    const input =
        "```\n" ++
        "aaa\n" ++
        "~~~\n" ++
        "```\n";
    const expected =
        "<pre><code>aaa\n" ++
        "~~~\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 93, line 1702: '~~~\\naaa\\n```\\n~~~'" {
    const input =
        "~~~\n" ++
        "aaa\n" ++
        "```\n" ++
        "~~~\n";
    const expected =
        "<pre><code>aaa\n" ++
        "```\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 94, line 1716: '````\\naaa\\n```\\n``````'" {
    const input =
        "````\n" ++
        "aaa\n" ++
        "```\n" ++
        "``````\n";
    const expected =
        "<pre><code>aaa\n" ++
        "```\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 95, line 1728: '~~~~\\naaa\\n~~~\\n~~~~'" {
    const input =
        "~~~~\n" ++
        "aaa\n" ++
        "~~~\n" ++
        "~~~~\n";
    const expected =
        "<pre><code>aaa\n" ++
        "~~~\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 96, line 1743: '```'" {
    const input =
        "```\n";
    const expected =
        "<pre><code></code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 97, line 1750: '`````\\n\\n```\\naaa'" {
    const input =
        "`````\n" ++
        "\n" ++
        "```\n" ++
        "aaa\n";
    const expected =
        "<pre><code>\n" ++
        "```\n" ++
        "aaa\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 98, line 1763: '> ```\\n> aaa\\n\\nbbb'" {
    const input =
        "> ```\n" ++
        "> aaa\n" ++
        "\n" ++
        "bbb\n";
    const expected =
        "<blockquote>\n" ++
        "<pre><code>aaa\n" ++
        "</code></pre>\n" ++
        "</blockquote>\n" ++
        "<p>bbb</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 99, line 1779: '```\\n\\n  \\n```'" {
    const input =
        "```\n" ++
        "\n" ++
        "  \n" ++
        "```\n";
    const expected =
        "<pre><code>\n" ++
        "  \n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 100, line 1793: '```\\n```'" {
    const input =
        "```\n" ++
        "```\n";
    const expected =
        "<pre><code></code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 101, line 1805: ' ```\\n aaa\\naaa\\n```'" {
    const input =
        " ```\n" ++
        " aaa\n" ++
        "aaa\n" ++
        "```\n";
    const expected =
        "<pre><code>aaa\n" ++
        "aaa\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 102, line 1817: '  ```\\naaa\\n  aaa\\naaa\\n  ```'" {
    const input =
        "  ```\n" ++
        "aaa\n" ++
        "  aaa\n" ++
        "aaa\n" ++
        "  ```\n";
    const expected =
        "<pre><code>aaa\n" ++
        "aaa\n" ++
        "aaa\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 103, line 1831: '   ```\\n   aaa\\n    aaa\\n  aaa\\n   ```'" {
    const input =
        "   ```\n" ++
        "   aaa\n" ++
        "    aaa\n" ++
        "  aaa\n" ++
        "   ```\n";
    const expected =
        "<pre><code>aaa\n" ++
        " aaa\n" ++
        "aaa\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 104, line 1847: '    ```\\n    aaa\\n    ```'" {
    const input =
        "    ```\n" ++
        "    aaa\n" ++
        "    ```\n";
    const expected =
        "<pre><code>```\n" ++
        "aaa\n" ++
        "```\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 105, line 1862: '```\\naaa\\n  ```'" {
    const input =
        "```\n" ++
        "aaa\n" ++
        "  ```\n";
    const expected =
        "<pre><code>aaa\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 106, line 1872: '   ```\\naaa\\n  ```'" {
    const input =
        "   ```\n" ++
        "aaa\n" ++
        "  ```\n";
    const expected =
        "<pre><code>aaa\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 107, line 1884: '```\\naaa\\n    ```'" {
    const input =
        "```\n" ++
        "aaa\n" ++
        "    ```\n";
    const expected =
        "<pre><code>aaa\n" ++
        "    ```\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 108, line 1898: '``` ```\\naaa'" {
    const input =
        "``` ```\n" ++
        "aaa\n";
    const expected =
        "<p><code> </code>\n" ++
        "aaa</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 109, line 1907: '~~~~~~\\naaa\\n~~~ ~~'" {
    const input =
        "~~~~~~\n" ++
        "aaa\n" ++
        "~~~ ~~\n";
    const expected =
        "<pre><code>aaa\n" ++
        "~~~ ~~\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 110, line 1921: 'foo\\n```\\nbar\\n```\\nbaz'" {
    const input =
        "foo\n" ++
        "```\n" ++
        "bar\n" ++
        "```\n" ++
        "baz\n";
    const expected =
        "<p>foo</p>\n" ++
        "<pre><code>bar\n" ++
        "</code></pre>\n" ++
        "<p>baz</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 111, line 1938: 'foo\\n---\\n~~~\\nbar\\n~~~\\n# baz'" {
    const input =
        "foo\n" ++
        "---\n" ++
        "~~~\n" ++
        "bar\n" ++
        "~~~\n" ++
        "# baz\n";
    const expected =
        "<h2>foo</h2>\n" ++
        "<pre><code>bar\n" ++
        "</code></pre>\n" ++
        "<h1>baz</h1>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 112, line 1960: '```ruby\\ndef foo(x)\\n  return 3\\nend\\n```'" {
    const input =
        "```ruby\n" ++
        "def foo(x)\n" ++
        "  return 3\n" ++
        "end\n" ++
        "```\n";
    const expected =
        "<pre><code class=\"language-ruby\">def foo(x)\n" ++
        "  return 3\n" ++
        "end\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 113, line 1974: '~~~~    ruby startline=3 $%@#$\\ndef foo(x)\\n  return 3\\nend\\n~~~~~~~'" {
    const input =
        "~~~~    ruby startline=3 $%@#$\n" ++
        "def foo(x)\n" ++
        "  return 3\n" ++
        "end\n" ++
        "~~~~~~~\n";
    const expected =
        "<pre><code class=\"language-ruby\">def foo(x)\n" ++
        "  return 3\n" ++
        "end\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 114, line 1988: '````;\\n````'" {
    const input =
        "````;\n" ++
        "````\n";
    const expected =
        "<pre><code class=\"language-;\"></code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 115, line 1998: '``` aa ```\\nfoo'" {
    const input =
        "``` aa ```\n" ++
        "foo\n";
    const expected =
        "<p><code>aa</code>\n" ++
        "foo</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 116, line 2009: '~~~ aa ``` ~~~\\nfoo\\n~~~'" {
    const input =
        "~~~ aa ``` ~~~\n" ++
        "foo\n" ++
        "~~~\n";
    const expected =
        "<pre><code class=\"language-aa\">foo\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 117, line 2021: '```\\n``` aaa\\n```'" {
    const input =
        "```\n" ++
        "``` aaa\n" ++
        "```\n";
    const expected =
        "<pre><code>``` aaa\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 118, line 2100: '<table><tr><td>\\n<pre>\\n**Hello**,\\n\\n_world_.\\n</pre>\\n</td></tr></table>'" {
    const input =
        "<table><tr><td>\n" ++
        "<pre>\n" ++
        "**Hello**,\n" ++
        "\n" ++
        "_world_.\n" ++
        "</pre>\n" ++
        "</td></tr></table>\n";
    const expected =
        "<table><tr><td>\n" ++
        "<pre>\n" ++
        "**Hello**,\n" ++
        "<p><em>world</em>.\n" ++
        "</pre></p>\n" ++
        "</td></tr></table>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 119, line 2129: '<table>\\n  <tr>\\n    <td>\\n           hi\\n    </td>\\n  </tr>\\n</table>\\n\\nokay.'" {
    const input =
        "<table>\n" ++
        "  <tr>\n" ++
        "    <td>\n" ++
        "           hi\n" ++
        "    </td>\n" ++
        "  </tr>\n" ++
        "</table>\n" ++
        "\n" ++
        "okay.\n";
    const expected =
        "<table>\n" ++
        "  <tr>\n" ++
        "    <td>\n" ++
        "           hi\n" ++
        "    </td>\n" ++
        "  </tr>\n" ++
        "</table>\n" ++
        "<p>okay.</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 120, line 2151: ' <div>\\n  *hello*\\n         <foo><a>'" {
    const input =
        " <div>\n" ++
        "  *hello*\n" ++
        "         <foo><a>\n";
    const expected =
        " <div>\n" ++
        "  *hello*\n" ++
        "         <foo><a>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 121, line 2164: '</div>\\n*foo*'" {
    const input =
        "</div>\n" ++
        "*foo*\n";
    const expected =
        "</div>\n" ++
        "*foo*\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 122, line 2175: '<DIV CLASS=\"foo\">\\n\\n*Markdown*\\n\\n</DIV>'" {
    const input =
        "<DIV CLASS=\"foo\">\n" ++
        "\n" ++
        "*Markdown*\n" ++
        "\n" ++
        "</DIV>\n";
    const expected =
        "<DIV CLASS=\"foo\">\n" ++
        "<p><em>Markdown</em></p>\n" ++
        "</DIV>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 123, line 2191: '<div id=\"foo\"\\n  class=\"bar\">\\n</div>'" {
    const input =
        "<div id=\"foo\"\n" ++
        "  class=\"bar\">\n" ++
        "</div>\n";
    const expected =
        "<div id=\"foo\"\n" ++
        "  class=\"bar\">\n" ++
        "</div>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 124, line 2202: '<div id=\"foo\" class=\"bar\\n  baz\">\\n</div>'" {
    const input =
        "<div id=\"foo\" class=\"bar\n" ++
        "  baz\">\n" ++
        "</div>\n";
    const expected =
        "<div id=\"foo\" class=\"bar\n" ++
        "  baz\">\n" ++
        "</div>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 125, line 2214: '<div>\\n*foo*\\n\\n*bar*'" {
    const input =
        "<div>\n" ++
        "*foo*\n" ++
        "\n" ++
        "*bar*\n";
    const expected =
        "<div>\n" ++
        "*foo*\n" ++
        "<p><em>bar</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 126, line 2230: '<div id=\"foo\"\\n*hi*'" {
    const input =
        "<div id=\"foo\"\n" ++
        "*hi*\n";
    const expected =
        "<div id=\"foo\"\n" ++
        "*hi*\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 127, line 2239: '<div class\\nfoo'" {
    const input =
        "<div class\n" ++
        "foo\n";
    const expected =
        "<div class\n" ++
        "foo\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 128, line 2251: '<div *???-&&&-<---\\n*foo*'" {
    const input =
        "<div *???-&&&-<---\n" ++
        "*foo*\n";
    const expected =
        "<div *???-&&&-<---\n" ++
        "*foo*\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 129, line 2263: '<div><a href=\"bar\">*foo*</a></div>'" {
    const input =
        "<div><a href=\"bar\">*foo*</a></div>\n";
    const expected =
        "<div><a href=\"bar\">*foo*</a></div>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 130, line 2270: '<table><tr><td>\\nfoo\\n</td></tr></table>'" {
    const input =
        "<table><tr><td>\n" ++
        "foo\n" ++
        "</td></tr></table>\n";
    const expected =
        "<table><tr><td>\n" ++
        "foo\n" ++
        "</td></tr></table>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 131, line 2287: '<div></div>\\n``` c\\nint x = 33;\\n```'" {
    const input =
        "<div></div>\n" ++
        "``` c\n" ++
        "int x = 33;\n" ++
        "```\n";
    const expected =
        "<div></div>\n" ++
        "``` c\n" ++
        "int x = 33;\n" ++
        "```\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 132, line 2304: '<a href=\"foo\">\\n*bar*\\n</a>'" {
    const input =
        "<a href=\"foo\">\n" ++
        "*bar*\n" ++
        "</a>\n";
    const expected =
        "<a href=\"foo\">\n" ++
        "*bar*\n" ++
        "</a>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 133, line 2317: '<Warning>\\n*bar*\\n</Warning>'" {
    const input =
        "<Warning>\n" ++
        "*bar*\n" ++
        "</Warning>\n";
    const expected =
        "<Warning>\n" ++
        "*bar*\n" ++
        "</Warning>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 134, line 2328: '<i class=\"foo\">\\n*bar*\\n</i>'" {
    const input =
        "<i class=\"foo\">\n" ++
        "*bar*\n" ++
        "</i>\n";
    const expected =
        "<i class=\"foo\">\n" ++
        "*bar*\n" ++
        "</i>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 135, line 2339: '</ins>\\n*bar*'" {
    const input =
        "</ins>\n" ++
        "*bar*\n";
    const expected =
        "</ins>\n" ++
        "*bar*\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 136, line 2354: '<del>\\n*foo*\\n</del>'" {
    const input =
        "<del>\n" ++
        "*foo*\n" ++
        "</del>\n";
    const expected =
        "<del>\n" ++
        "*foo*\n" ++
        "</del>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 137, line 2369: '<del>\\n\\n*foo*\\n\\n</del>'" {
    const input =
        "<del>\n" ++
        "\n" ++
        "*foo*\n" ++
        "\n" ++
        "</del>\n";
    const expected =
        "<del>\n" ++
        "<p><em>foo</em></p>\n" ++
        "</del>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 138, line 2387: '<del>*foo*</del>'" {
    const input =
        "<del>*foo*</del>\n";
    const expected =
        "<p><del><em>foo</em></del></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 139, line 2403: '<pre language=\"haskell\"><code>\\nimport Text.HTML.TagSoup\\n\\nmain :: IO ()\\nmain = print $ parseTags tags\\n</code></pre>\\nokay'" {
    const input =
        "<pre language=\"haskell\"><code>\n" ++
        "import Text.HTML.TagSoup\n" ++
        "\n" ++
        "main :: IO ()\n" ++
        "main = print $ parseTags tags\n" ++
        "</code></pre>\n" ++
        "okay\n";
    const expected =
        "<pre language=\"haskell\"><code>\n" ++
        "import Text.HTML.TagSoup\n" ++
        "\n" ++
        "main :: IO ()\n" ++
        "main = print $ parseTags tags\n" ++
        "</code></pre>\n" ++
        "<p>okay</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 140, line 2424: '<script type=\"text/javascript\">\\n// JavaScript example\\n\\ndocument.getElementById(\"demo\").innerHTML = \"Hello JavaScript!\";\\n</script>\\nokay'" {
    const input =
        "<script type=\"text/javascript\">\n" ++
        "// JavaScript example\n" ++
        "\n" ++
        "document.getElementById(\"demo\").innerHTML = \"Hello JavaScript!\";\n" ++
        "</script>\n" ++
        "okay\n";
    const expected =
        "<script type=\"text/javascript\">\n" ++
        "// JavaScript example\n" ++
        "\n" ++
        "document.getElementById(\"demo\").innerHTML = \"Hello JavaScript!\";\n" ++
        "</script>\n" ++
        "<p>okay</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 141, line 2443: '<style\\n  type=\"text/css\">\\nh1 {color:red;}\\n\\np {color:blue;}\\n</style>\\nokay'" {
    const input =
        "<style\n" ++
        "  type=\"text/css\">\n" ++
        "h1 {color:red;}\n" ++
        "\n" ++
        "p {color:blue;}\n" ++
        "</style>\n" ++
        "okay\n";
    const expected =
        "<style\n" ++
        "  type=\"text/css\">\n" ++
        "h1 {color:red;}\n" ++
        "\n" ++
        "p {color:blue;}\n" ++
        "</style>\n" ++
        "<p>okay</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 142, line 2466: '<style\\n  type=\"text/css\">\\n\\nfoo'" {
    const input =
        "<style\n" ++
        "  type=\"text/css\">\n" ++
        "\n" ++
        "foo\n";
    const expected =
        "<style\n" ++
        "  type=\"text/css\">\n" ++
        "\n" ++
        "foo\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 143, line 2479: '> <div>\\n> foo\\n\\nbar'" {
    const input =
        "> <div>\n" ++
        "> foo\n" ++
        "\n" ++
        "bar\n";
    const expected =
        "<blockquote>\n" ++
        "<div>\n" ++
        "foo\n" ++
        "</blockquote>\n" ++
        "<p>bar</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 144, line 2493: '- <div>\\n- foo'" {
    const input =
        "- <div>\n" ++
        "- foo\n";
    const expected =
        "<ul>\n" ++
        "<li>\n" ++
        "<div>\n" ++
        "</li>\n" ++
        "<li>foo</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 145, line 2508: '<style>p{color:red;}</style>\\n*foo*'" {
    const input =
        "<style>p{color:red;}</style>\n" ++
        "*foo*\n";
    const expected =
        "<style>p{color:red;}</style>\n" ++
        "<p><em>foo</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 146, line 2517: '<!-- foo -->*bar*\\n*baz*'" {
    const input =
        "<!-- foo -->*bar*\n" ++
        "*baz*\n";
    const expected =
        "<!-- foo -->*bar*\n" ++
        "<p><em>baz</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 147, line 2529: '<script>\\nfoo\\n</script>1. *bar*'" {
    const input =
        "<script>\n" ++
        "foo\n" ++
        "</script>1. *bar*\n";
    const expected =
        "<script>\n" ++
        "foo\n" ++
        "</script>1. *bar*\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 148, line 2542: '<!-- Foo\\n\\nbar\\n   baz -->\\nokay'" {
    const input =
        "<!-- Foo\n" ++
        "\n" ++
        "bar\n" ++
        "   baz -->\n" ++
        "okay\n";
    const expected =
        "<!-- Foo\n" ++
        "\n" ++
        "bar\n" ++
        "   baz -->\n" ++
        "<p>okay</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 149, line 2560: '<?php\\n\\n  echo '>';\\n\\n?>\\nokay'" {
    const input =
        "<?php\n" ++
        "\n" ++
        "  echo '>';\n" ++
        "\n" ++
        "?>\n" ++
        "okay\n";
    const expected =
        "<?php\n" ++
        "\n" ++
        "  echo '>';\n" ++
        "\n" ++
        "?>\n" ++
        "<p>okay</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 150, line 2579: '<!DOCTYPE html>'" {
    const input =
        "<!DOCTYPE html>\n";
    const expected =
        "<!DOCTYPE html>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 151, line 2588: '<![CDATA[\\nfunction matchwo(a,b)\\n{\\n  if (a < b && a < 0) then {\\n    return 1;\\n\\n  } else {\\n\\n    return 0;\\n  }\\n}\\n]]>\\nokay'" {
    const input =
        "<![CDATA[\n" ++
        "function matchwo(a,b)\n" ++
        "{\n" ++
        "  if (a < b && a < 0) then {\n" ++
        "    return 1;\n" ++
        "\n" ++
        "  } else {\n" ++
        "\n" ++
        "    return 0;\n" ++
        "  }\n" ++
        "}\n" ++
        "]]>\n" ++
        "okay\n";
    const expected =
        "<![CDATA[\n" ++
        "function matchwo(a,b)\n" ++
        "{\n" ++
        "  if (a < b && a < 0) then {\n" ++
        "    return 1;\n" ++
        "\n" ++
        "  } else {\n" ++
        "\n" ++
        "    return 0;\n" ++
        "  }\n" ++
        "}\n" ++
        "]]>\n" ++
        "<p>okay</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 152, line 2621: '  <!-- foo -->\\n\\n    <!-- foo -->'" {
    const input =
        "  <!-- foo -->\n" ++
        "\n" ++
        "    <!-- foo -->\n";
    const expected =
        "  <!-- foo -->\n" ++
        "<pre><code>&lt;!-- foo --&gt;\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 153, line 2632: '  <div>\\n\\n    <div>'" {
    const input =
        "  <div>\n" ++
        "\n" ++
        "    <div>\n";
    const expected =
        "  <div>\n" ++
        "<pre><code>&lt;div&gt;\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 154, line 2646: 'Foo\\n<div>\\nbar\\n</div>'" {
    const input =
        "Foo\n" ++
        "<div>\n" ++
        "bar\n" ++
        "</div>\n";
    const expected =
        "<p>Foo</p>\n" ++
        "<div>\n" ++
        "bar\n" ++
        "</div>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 155, line 2663: '<div>\\nbar\\n</div>\\n*foo*'" {
    const input =
        "<div>\n" ++
        "bar\n" ++
        "</div>\n" ++
        "*foo*\n";
    const expected =
        "<div>\n" ++
        "bar\n" ++
        "</div>\n" ++
        "*foo*\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 156, line 2678: 'Foo\\n<a href=\"bar\">\\nbaz'" {
    const input =
        "Foo\n" ++
        "<a href=\"bar\">\n" ++
        "baz\n";
    const expected =
        "<p>Foo\n" ++
        "<a href=\"bar\">\n" ++
        "baz</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 157, line 2719: '<div>\\n\\n*Emphasized* text.\\n\\n</div>'" {
    const input =
        "<div>\n" ++
        "\n" ++
        "*Emphasized* text.\n" ++
        "\n" ++
        "</div>\n";
    const expected =
        "<div>\n" ++
        "<p><em>Emphasized</em> text.</p>\n" ++
        "</div>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 158, line 2732: '<div>\\n*Emphasized* text.\\n</div>'" {
    const input =
        "<div>\n" ++
        "*Emphasized* text.\n" ++
        "</div>\n";
    const expected =
        "<div>\n" ++
        "*Emphasized* text.\n" ++
        "</div>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 159, line 2754: '<table>\\n\\n<tr>\\n\\n<td>\\nHi\\n</td>\\n\\n</tr>\\n\\n</table>'" {
    const input =
        "<table>\n" ++
        "\n" ++
        "<tr>\n" ++
        "\n" ++
        "<td>\n" ++
        "Hi\n" ++
        "</td>\n" ++
        "\n" ++
        "</tr>\n" ++
        "\n" ++
        "</table>\n";
    const expected =
        "<table>\n" ++
        "<tr>\n" ++
        "<td>\n" ++
        "Hi\n" ++
        "</td>\n" ++
        "</tr>\n" ++
        "</table>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 160, line 2781: '<table>\\n\\n  <tr>\\n\\n    <td>\\n      Hi\\n    </td>\\n\\n  </tr>\\n\\n</table>'" {
    const input =
        "<table>\n" ++
        "\n" ++
        "  <tr>\n" ++
        "\n" ++
        "    <td>\n" ++
        "      Hi\n" ++
        "    </td>\n" ++
        "\n" ++
        "  </tr>\n" ++
        "\n" ++
        "</table>\n";
    const expected =
        "<table>\n" ++
        "  <tr>\n" ++
        "<pre><code>&lt;td&gt;\n" ++
        "  Hi\n" ++
        "&lt;/td&gt;\n" ++
        "</code></pre>\n" ++
        "  </tr>\n" ++
        "</table>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 161, line 2829: '[foo]: /url \"title\"\\n\\n[foo]'" {
    const input =
        "[foo]: /url \"title\"\n" ++
        "\n" ++
        "[foo]\n";
    const expected =
        "<p><a href=\"/url\" title=\"title\">foo</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 162, line 2838: '   [foo]: \\n      /url  \\n           'the title'  \\n\\n[foo]'" {
    const input =
        "   [foo]: \n" ++
        "      /url  \n" ++
        "           'the title'  \n" ++
        "\n" ++
        "[foo]\n";
    const expected =
        "<p><a href=\"/url\" title=\"the title\">foo</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 163, line 2849: '[Foo*bar\\]]:my_(url) 'title (with parens)'\\n\\n[Foo*bar\\]]'" {
    const input =
        "[Foo*bar\\]]:my_(url) 'title (with parens)'\n" ++
        "\n" ++
        "[Foo*bar\\]]\n";
    const expected =
        "<p><a href=\"my_(url)\" title=\"title (with parens)\">Foo*bar]</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 164, line 2858: '[Foo bar]:\\n<my url>\\n'title'\\n\\n[Foo bar]'" {
    const input =
        "[Foo bar]:\n" ++
        "<my url>\n" ++
        "'title'\n" ++
        "\n" ++
        "[Foo bar]\n";
    const expected =
        "<p><a href=\"my%20url\" title=\"title\">Foo bar</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 165, line 2871: '[foo]: /url '\\ntitle\\nline1\\nline2\\n'\\n\\n[foo]'" {
    const input =
        "[foo]: /url '\n" ++
        "title\n" ++
        "line1\n" ++
        "line2\n" ++
        "'\n" ++
        "\n" ++
        "[foo]\n";
    const expected =
        "<p><a href=\"/url\" title=\"\n" ++
        "title\n" ++
        "line1\n" ++
        "line2\n" ++
        "\">foo</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 166, line 2890: '[foo]: /url 'title\\n\\nwith blank line'\\n\\n[foo]'" {
    const input =
        "[foo]: /url 'title\n" ++
        "\n" ++
        "with blank line'\n" ++
        "\n" ++
        "[foo]\n";
    const expected =
        "<p>[foo]: /url 'title</p>\n" ++
        "<p>with blank line'</p>\n" ++
        "<p>[foo]</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 167, line 2905: '[foo]:\\n/url\\n\\n[foo]'" {
    const input =
        "[foo]:\n" ++
        "/url\n" ++
        "\n" ++
        "[foo]\n";
    const expected =
        "<p><a href=\"/url\">foo</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 168, line 2917: '[foo]:\\n\\n[foo]'" {
    const input =
        "[foo]:\n" ++
        "\n" ++
        "[foo]\n";
    const expected =
        "<p>[foo]:</p>\n" ++
        "<p>[foo]</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 169, line 2929: '[foo]: <>\\n\\n[foo]'" {
    const input =
        "[foo]: <>\n" ++
        "\n" ++
        "[foo]\n";
    const expected =
        "<p><a href=\"\">foo</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 170, line 2940: '[foo]: <bar>(baz)\\n\\n[foo]'" {
    const input =
        "[foo]: <bar>(baz)\n" ++
        "\n" ++
        "[foo]\n";
    const expected =
        "<p>[foo]: <bar>(baz)</p>\n" ++
        "<p>[foo]</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 171, line 2953: '[foo]: /url\\bar\\*baz \"foo\\\"bar\\baz\"\\n\\n[foo]'" {
    const input =
        "[foo]: /url\\bar\\*baz \"foo\\\"bar\\baz\"\n" ++
        "\n" ++
        "[foo]\n";
    const expected =
        "<p><a href=\"/url%5Cbar*baz\" title=\"foo&quot;bar\\baz\">foo</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 172, line 2964: '[foo]\\n\\n[foo]: url'" {
    const input =
        "[foo]\n" ++
        "\n" ++
        "[foo]: url\n";
    const expected =
        "<p><a href=\"url\">foo</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 173, line 2976: '[foo]\\n\\n[foo]: first\\n[foo]: second'" {
    const input =
        "[foo]\n" ++
        "\n" ++
        "[foo]: first\n" ++
        "[foo]: second\n";
    const expected =
        "<p><a href=\"first\">foo</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 174, line 2989: '[FOO]: /url\\n\\n[Foo]'" {
    const input =
        "[FOO]: /url\n" ++
        "\n" ++
        "[Foo]\n";
    const expected =
        "<p><a href=\"/url\">Foo</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

// TODO:
//test "Example 175, line 2998: '[ΑΓΩ]: /φου\\n\\n[αγω]'" {
//    const input =
//        "[ΑΓΩ]: /φου\n" ++
//        "\n" ++
//        "[αγω]\n";
//    const expected =
//        "<p><a href=\"/%CF%86%CE%BF%CF%85\">αγω</a></p>\n";
//
//    const gpa = std.testing.allocator;
//    var rules = try gfm.init(gpa);
//    defer rules.blocks.deinit();
//    defer rules.inlines.deinit();
////
//    const root = try parse.execute(gpa, input, rules, null);
//    defer root.deinit(gpa);
//
//    const html = try render.renderHtml(gpa, root, null);
//    defer gpa.free(html);
//
//    try std.testing.expectEqualStrings(expected, html);
//}

test "Example 176, line 3010: '[foo]: /url'" {
    const input =
        "[foo]: /url\n";
    const expected =
        "";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 177, line 3018: '[\\nfoo\\n]: /url\\nbar'" {
    const input =
        "[\n" ++
        "foo\n" ++
        "]: /url\n" ++
        "bar\n";
    const expected =
        "<p>bar</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 178, line 3031: '[foo]: /url \"title\" ok'" {
    const input =
        "[foo]: /url \"title\" ok\n";
    const expected =
        "<p>[foo]: /url &quot;title&quot; ok</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 179, line 3040: '[foo]: /url\\n\"title\" ok'" {
    const input =
        "[foo]: /url\n" ++
        "\"title\" ok\n";
    const expected =
        "<p>&quot;title&quot; ok</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 180, line 3051: '    [foo]: /url \"title\"\\n\\n[foo]'" {
    const input =
        "    [foo]: /url \"title\"\n" ++
        "\n" ++
        "[foo]\n";
    const expected =
        "<pre><code>[foo]: /url &quot;title&quot;\n" ++
        "</code></pre>\n" ++
        "<p>[foo]</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 181, line 3065: '```\\n[foo]: /url\\n```\\n\\n[foo]'" {
    const input =
        "```\n" ++
        "[foo]: /url\n" ++
        "```\n" ++
        "\n" ++
        "[foo]\n";
    const expected =
        "<pre><code>[foo]: /url\n" ++
        "</code></pre>\n" ++
        "<p>[foo]</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 182, line 3080: 'Foo\\n[bar]: /baz\\n\\n[bar]'" {
    const input =
        "Foo\n" ++
        "[bar]: /baz\n" ++
        "\n" ++
        "[bar]\n";
    const expected =
        "<p>Foo\n" ++
        "[bar]: /baz</p>\n" ++
        "<p>[bar]</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 183, line 3095: '# [Foo]\\n[foo]: /url\\n> bar'" {
    const input =
        "# [Foo]\n" ++
        "[foo]: /url\n" ++
        "> bar\n";
    const expected =
        "<h1><a href=\"/url\">Foo</a></h1>\n" ++
        "<blockquote>\n" ++
        "<p>bar</p>\n" ++
        "</blockquote>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 184, line 3106: '[foo]: /url\\nbar\\n===\\n[foo]'" {
    const input =
        "[foo]: /url\n" ++
        "bar\n" ++
        "===\n" ++
        "[foo]\n";
    const expected =
        "<h1>bar</h1>\n" ++
        "<p><a href=\"/url\">foo</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 185, line 3116: '[foo]: /url\\n===\\n[foo]'" {
    const input =
        "[foo]: /url\n" ++
        "===\n" ++
        "[foo]\n";
    const expected =
        "<p>===\n" ++
        "<a href=\"/url\">foo</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 186, line 3129: '[foo]: /foo-url \"foo\"\\n[bar]: /bar-url\\n  \"bar\"\\n[baz]: /baz-url\\n\\n[foo],\\n[bar],\\n[baz]'" {
    const input =
        "[foo]: /foo-url \"foo\"\n" ++
        "[bar]: /bar-url\n" ++
        "  \"bar\"\n" ++
        "[baz]: /baz-url\n" ++
        "\n" ++
        "[foo],\n" ++
        "[bar],\n" ++
        "[baz]\n";
    const expected =
        "<p><a href=\"/foo-url\" title=\"foo\">foo</a>,\n" ++
        "<a href=\"/bar-url\" title=\"bar\">bar</a>,\n" ++
        "<a href=\"/baz-url\">baz</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 187, line 3150: '[foo]\\n\\n> [foo]: /url'" {
    const input =
        "[foo]\n" ++
        "\n" ++
        "> [foo]: /url\n";
    const expected =
        "<p><a href=\"/url\">foo</a></p>\n" ++
        "<blockquote>\n" ++
        "</blockquote>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 188, line 3167: '[foo]: /url'" {
    const input =
        "[foo]: /url\n";
    const expected =
        "";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 189, line 3184: 'aaa\\n\\nbbb'" {
    const input =
        "aaa\n" ++
        "\n" ++
        "bbb\n";
    const expected =
        "<p>aaa</p>\n" ++
        "<p>bbb</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 190, line 3196: 'aaa\\nbbb\\n\\nccc\\nddd'" {
    const input =
        "aaa\n" ++
        "bbb\n" ++
        "\n" ++
        "ccc\n" ++
        "ddd\n";
    const expected =
        "<p>aaa\n" ++
        "bbb</p>\n" ++
        "<p>ccc\n" ++
        "ddd</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 191, line 3212: 'aaa\\n\\n\\nbbb'" {
    const input =
        "aaa\n" ++
        "\n" ++
        "\n" ++
        "bbb\n";
    const expected =
        "<p>aaa</p>\n" ++
        "<p>bbb</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 192, line 3225: '  aaa\\n bbb'" {
    const input =
        "  aaa\n" ++
        " bbb\n";
    const expected =
        "<p>aaa\n" ++
        "bbb</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 193, line 3237: 'aaa\\n             bbb\\n                                       ccc'" {
    const input =
        "aaa\n" ++
        "             bbb\n" ++
        "                                       ccc\n";
    const expected =
        "<p>aaa\n" ++
        "bbb\n" ++
        "ccc</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 194, line 3251: '   aaa\\nbbb'" {
    const input =
        "   aaa\n" ++
        "bbb\n";
    const expected =
        "<p>aaa\n" ++
        "bbb</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 195, line 3260: '    aaa\\nbbb'" {
    const input =
        "    aaa\n" ++
        "bbb\n";
    const expected =
        "<pre><code>aaa\n" ++
        "</code></pre>\n" ++
        "<p>bbb</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 196, line 3274: 'aaa     \\nbbb     '" {
    const input =
        "aaa     \n" ++
        "bbb     \n";
    const expected =
        "<p>aaa<br />\n" ++
        "bbb</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 197, line 3291: '  \\n\\naaa\\n  \\n\\n# aaa\\n\\n  '" {
    const input =
        "  \n" ++
        "\n" ++
        "aaa\n" ++
        "  \n" ++
        "\n" ++
        "# aaa\n" ++
        "\n" ++
        "  \n";
    const expected =
        "<p>aaa</p>\n" ++
        "<h1>aaa</h1>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 198, line 3326: '| foo | bar |\\n| --- | --- |\\n| baz | bim |'" {
    const input =
        "| foo | bar |\n" ++
        "| --- | --- |\n" ++
        "| baz | bim |\n";
    const expected =
        "<table>\n" ++
        "<thead>\n" ++
        "<tr>\n" ++
        "<th>foo</th>\n" ++
        "<th>bar</th>\n" ++
        "</tr>\n" ++
        "</thead>\n" ++
        "<tbody>\n" ++
        "<tr>\n" ++
        "<td>baz</td>\n" ++
        "<td>bim</td>\n" ++
        "</tr>\n" ++
        "</tbody>\n" ++
        "</table>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 199, line 3350: '| abc | defghi |\\n:-: | -----------:\\nbar | baz'" {
    const input =
        "| abc | defghi |\n" ++
        ":-: | -----------:\n" ++
        "bar | baz\n";
    const expected =
        "<table>\n" ++
        "<thead>\n" ++
        "<tr>\n" ++
        "<th align=\"center\">abc</th>\n" ++
        "<th align=\"right\">defghi</th>\n" ++
        "</tr>\n" ++
        "</thead>\n" ++
        "<tbody>\n" ++
        "<tr>\n" ++
        "<td align=\"center\">bar</td>\n" ++
        "<td align=\"right\">baz</td>\n" ++
        "</tr>\n" ++
        "</tbody>\n" ++
        "</table>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 200, line 3374: '| f\\|oo  |\\n| ------ |\\n| b `\\|` az |\\n| b **\\|** im |'" {
    const input =
        "| f\\|oo  |\n" ++
        "| ------ |\n" ++
        "| b `\\|` az |\n" ++
        "| b **\\|** im |\n";
    const expected =
        "<table>\n" ++
        "<thead>\n" ++
        "<tr>\n" ++
        "<th>f|oo</th>\n" ++
        "</tr>\n" ++
        "</thead>\n" ++
        "<tbody>\n" ++
        "<tr>\n" ++
        "<td>b <code>|</code> az</td>\n" ++
        "</tr>\n" ++
        "<tr>\n" ++
        "<td>b <strong>|</strong> im</td>\n" ++
        "</tr>\n" ++
        "</tbody>\n" ++
        "</table>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 201, line 3400: '| abc | def |\\n| --- | --- |\\n| bar | baz |\\n> bar'" {
    const input =
        "| abc | def |\n" ++
        "| --- | --- |\n" ++
        "| bar | baz |\n" ++
        "> bar\n";
    const expected =
        "<table>\n" ++
        "<thead>\n" ++
        "<tr>\n" ++
        "<th>abc</th>\n" ++
        "<th>def</th>\n" ++
        "</tr>\n" ++
        "</thead>\n" ++
        "<tbody>\n" ++
        "<tr>\n" ++
        "<td>bar</td>\n" ++
        "<td>baz</td>\n" ++
        "</tr>\n" ++
        "</tbody>\n" ++
        "</table>\n" ++
        "<blockquote>\n" ++
        "<p>bar</p>\n" ++
        "</blockquote>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 202, line 3425: '| abc | def |\\n| --- | --- |\\n| bar | baz |\\nbar\\n\\nbar'" {
    const input =
        "| abc | def |\n" ++
        "| --- | --- |\n" ++
        "| bar | baz |\n" ++
        "bar\n" ++
        "\n" ++
        "bar\n";
    const expected =
        "<table>\n" ++
        "<thead>\n" ++
        "<tr>\n" ++
        "<th>abc</th>\n" ++
        "<th>def</th>\n" ++
        "</tr>\n" ++
        "</thead>\n" ++
        "<tbody>\n" ++
        "<tr>\n" ++
        "<td>bar</td>\n" ++
        "<td>baz</td>\n" ++
        "</tr>\n" ++
        "<tr>\n" ++
        "<td>bar</td>\n" ++
        "<td></td>\n" ++
        "</tr>\n" ++
        "</tbody>\n" ++
        "</table>\n" ++
        "<p>bar</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 203, line 3457: '| abc | def |\\n| --- |\\n| bar |'" {
    const input =
        "| abc | def |\n" ++
        "| --- |\n" ++
        "| bar |\n";
    const expected =
        "<p>| abc | def |\n" ++
        "| --- |\n" ++
        "| bar |</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 204, line 3471: '| abc | def |\\n| --- | --- |\\n| bar |\\n| bar | baz | boo |'" {
    const input =
        "| abc | def |\n" ++
        "| --- | --- |\n" ++
        "| bar |\n" ++
        "| bar | baz | boo |\n";
    const expected =
        "<table>\n" ++
        "<thead>\n" ++
        "<tr>\n" ++
        "<th>abc</th>\n" ++
        "<th>def</th>\n" ++
        "</tr>\n" ++
        "</thead>\n" ++
        "<tbody>\n" ++
        "<tr>\n" ++
        "<td>bar</td>\n" ++
        "<td></td>\n" ++
        "</tr>\n" ++
        "<tr>\n" ++
        "<td>bar</td>\n" ++
        "<td>baz</td>\n" ++
        "</tr>\n" ++
        "</tbody>\n" ++
        "</table>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 205, line 3499: '| abc | def |\\n| --- | --- |'" {
    const input =
        "| abc | def |\n" ++
        "| --- | --- |\n";
    const expected =
        "<table>\n" ++
        "<thead>\n" ++
        "<tr>\n" ++
        "<th>abc</th>\n" ++
        "<th>def</th>\n" ++
        "</tr>\n" ++
        "</thead>\n" ++
        "</table>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 206, line 3565: '> # Foo\\n> bar\\n> baz'" {
    const input =
        "> # Foo\n" ++
        "> bar\n" ++
        "> baz\n";
    const expected =
        "<blockquote>\n" ++
        "<h1>Foo</h1>\n" ++
        "<p>bar\n" ++
        "baz</p>\n" ++
        "</blockquote>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 207, line 3580: '># Foo\\n>bar\\n> baz'" {
    const input =
        "># Foo\n" ++
        ">bar\n" ++
        "> baz\n";
    const expected =
        "<blockquote>\n" ++
        "<h1>Foo</h1>\n" ++
        "<p>bar\n" ++
        "baz</p>\n" ++
        "</blockquote>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 208, line 3595: '   > # Foo\\n   > bar\\n > baz'" {
    const input =
        "   > # Foo\n" ++
        "   > bar\n" ++
        " > baz\n";
    const expected =
        "<blockquote>\n" ++
        "<h1>Foo</h1>\n" ++
        "<p>bar\n" ++
        "baz</p>\n" ++
        "</blockquote>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 209, line 3610: '    > # Foo\\n    > bar\\n    > baz'" {
    const input =
        "    > # Foo\n" ++
        "    > bar\n" ++
        "    > baz\n";
    const expected =
        "<pre><code>&gt; # Foo\n" ++
        "&gt; bar\n" ++
        "&gt; baz\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 210, line 3625: '> # Foo\\n> bar\\nbaz'" {
    const input =
        "> # Foo\n" ++
        "> bar\n" ++
        "baz\n";
    const expected =
        "<blockquote>\n" ++
        "<h1>Foo</h1>\n" ++
        "<p>bar\n" ++
        "baz</p>\n" ++
        "</blockquote>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 211, line 3641: '> bar\\nbaz\\n> foo'" {
    const input =
        "> bar\n" ++
        "baz\n" ++
        "> foo\n";
    const expected =
        "<blockquote>\n" ++
        "<p>bar\n" ++
        "baz\n" ++
        "foo</p>\n" ++
        "</blockquote>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 212, line 3665: '> foo\\n---'" {
    const input =
        "> foo\n" ++
        "---\n";
    const expected =
        "<blockquote>\n" ++
        "<p>foo</p>\n" ++
        "</blockquote>\n" ++
        "<hr />\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 213, line 3685: '> - foo\\n- bar'" {
    const input =
        "> - foo\n" ++
        "- bar\n";
    const expected =
        "<blockquote>\n" ++
        "<ul>\n" ++
        "<li>foo</li>\n" ++
        "</ul>\n" ++
        "</blockquote>\n" ++
        "<ul>\n" ++
        "<li>bar</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 214, line 3703: '>     foo\\n    bar'" {
    const input =
        ">     foo\n" ++
        "    bar\n";
    const expected =
        "<blockquote>\n" ++
        "<pre><code>foo\n" ++
        "</code></pre>\n" ++
        "</blockquote>\n" ++
        "<pre><code>bar\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 215, line 3716: '> ```\\nfoo\\n```'" {
    const input =
        "> ```\n" ++
        "foo\n" ++
        "```\n";
    const expected =
        "<blockquote>\n" ++
        "<pre><code></code></pre>\n" ++
        "</blockquote>\n" ++
        "<p>foo</p>\n" ++
        "<pre><code></code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 216, line 3732: '> foo\\n    - bar'" {
    const input =
        "> foo\n" ++
        "    - bar\n";
    const expected =
        "<blockquote>\n" ++
        "<p>foo\n" ++
        "- bar</p>\n" ++
        "</blockquote>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 217, line 3756: '>'" {
    const input =
        ">\n";
    const expected =
        "<blockquote>\n" ++
        "</blockquote>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 218, line 3764: '>\\n>  \\n> '" {
    const input =
        ">\n" ++
        ">  \n" ++
        "> \n";
    const expected =
        "<blockquote>\n" ++
        "</blockquote>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 219, line 3776: '>\\n> foo\\n>  '" {
    const input =
        ">\n" ++
        "> foo\n" ++
        ">  \n";
    const expected =
        "<blockquote>\n" ++
        "<p>foo</p>\n" ++
        "</blockquote>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 220, line 3789: '> foo\\n\\n> bar'" {
    const input =
        "> foo\n" ++
        "\n" ++
        "> bar\n";
    const expected =
        "<blockquote>\n" ++
        "<p>foo</p>\n" ++
        "</blockquote>\n" ++
        "<blockquote>\n" ++
        "<p>bar</p>\n" ++
        "</blockquote>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 221, line 3811: '> foo\\n> bar'" {
    const input =
        "> foo\n" ++
        "> bar\n";
    const expected =
        "<blockquote>\n" ++
        "<p>foo\n" ++
        "bar</p>\n" ++
        "</blockquote>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 222, line 3824: '> foo\\n>\\n> bar'" {
    const input =
        "> foo\n" ++
        ">\n" ++
        "> bar\n";
    const expected =
        "<blockquote>\n" ++
        "<p>foo</p>\n" ++
        "<p>bar</p>\n" ++
        "</blockquote>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 223, line 3838: 'foo\\n> bar'" {
    const input =
        "foo\n" ++
        "> bar\n";
    const expected =
        "<p>foo</p>\n" ++
        "<blockquote>\n" ++
        "<p>bar</p>\n" ++
        "</blockquote>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 224, line 3852: '> aaa\\n***\\n> bbb'" {
    const input =
        "> aaa\n" ++
        "***\n" ++
        "> bbb\n";
    const expected =
        "<blockquote>\n" ++
        "<p>aaa</p>\n" ++
        "</blockquote>\n" ++
        "<hr />\n" ++
        "<blockquote>\n" ++
        "<p>bbb</p>\n" ++
        "</blockquote>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 225, line 3870: '> bar\\nbaz'" {
    const input =
        "> bar\n" ++
        "baz\n";
    const expected =
        "<blockquote>\n" ++
        "<p>bar\n" ++
        "baz</p>\n" ++
        "</blockquote>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 226, line 3881: '> bar\\n\\nbaz'" {
    const input =
        "> bar\n" ++
        "\n" ++
        "baz\n";
    const expected =
        "<blockquote>\n" ++
        "<p>bar</p>\n" ++
        "</blockquote>\n" ++
        "<p>baz</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 227, line 3893: '> bar\\n>\\nbaz'" {
    const input =
        "> bar\n" ++
        ">\n" ++
        "baz\n";
    const expected =
        "<blockquote>\n" ++
        "<p>bar</p>\n" ++
        "</blockquote>\n" ++
        "<p>baz</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 228, line 3909: '> > > foo\\nbar'" {
    const input =
        "> > > foo\n" ++
        "bar\n";
    const expected =
        "<blockquote>\n" ++
        "<blockquote>\n" ++
        "<blockquote>\n" ++
        "<p>foo\n" ++
        "bar</p>\n" ++
        "</blockquote>\n" ++
        "</blockquote>\n" ++
        "</blockquote>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 229, line 3924: '>>> foo\\n> bar\\n>>baz'" {
    const input =
        ">>> foo\n" ++
        "> bar\n" ++
        ">>baz\n";
    const expected =
        "<blockquote>\n" ++
        "<blockquote>\n" ++
        "<blockquote>\n" ++
        "<p>foo\n" ++
        "bar\n" ++
        "baz</p>\n" ++
        "</blockquote>\n" ++
        "</blockquote>\n" ++
        "</blockquote>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 230, line 3946: '>     code\\n\\n>    not code'" {
    const input =
        ">     code\n" ++
        "\n" ++
        ">    not code\n";
    const expected =
        "<blockquote>\n" ++
        "<pre><code>code\n" ++
        "</code></pre>\n" ++
        "</blockquote>\n" ++
        "<blockquote>\n" ++
        "<p>not code</p>\n" ++
        "</blockquote>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 231, line 4000: 'A paragraph\\nwith two lines.\\n\\n    indented code\\n\\n> A block quote.'" {
    const input =
        "A paragraph\n" ++
        "with two lines.\n" ++
        "\n" ++
        "    indented code\n" ++
        "\n" ++
        "> A block quote.\n";
    const expected =
        "<p>A paragraph\n" ++
        "with two lines.</p>\n" ++
        "<pre><code>indented code\n" ++
        "</code></pre>\n" ++
        "<blockquote>\n" ++
        "<p>A block quote.</p>\n" ++
        "</blockquote>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 232, line 4022: '1.  A paragraph\\n    with two lines.\\n\\n        indented code\\n\\n    > A block quote.'" {
    const input =
        "1.  A paragraph\n" ++
        "    with two lines.\n" ++
        "\n" ++
        "        indented code\n" ++
        "\n" ++
        "    > A block quote.\n";
    const expected =
        "<ol>\n" ++
        "<li>\n" ++
        "<p>A paragraph\n" ++
        "with two lines.</p>\n" ++
        "<pre><code>indented code\n" ++
        "</code></pre>\n" ++
        "<blockquote>\n" ++
        "<p>A block quote.</p>\n" ++
        "</blockquote>\n" ++
        "</li>\n" ++
        "</ol>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 233, line 4055: '- one\\n\\n two'" {
    const input =
        "- one\n" ++
        "\n" ++
        " two\n";
    const expected =
        "<ul>\n" ++
        "<li>one</li>\n" ++
        "</ul>\n" ++
        "<p>two</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 234, line 4067: '- one\\n\\n  two'" {
    const input =
        "- one\n" ++
        "\n" ++
        "  two\n";
    const expected =
        "<ul>\n" ++
        "<li>\n" ++
        "<p>one</p>\n" ++
        "<p>two</p>\n" ++
        "</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 235, line 4081: ' -    one\\n\\n     two'" {
    const input =
        " -    one\n" ++
        "\n" ++
        "     two\n";
    const expected =
        "<ul>\n" ++
        "<li>one</li>\n" ++
        "</ul>\n" ++
        "<pre><code> two\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 236, line 4094: ' -    one\\n\\n      two'" {
    const input =
        " -    one\n" ++
        "\n" ++
        "      two\n";
    const expected =
        "<ul>\n" ++
        "<li>\n" ++
        "<p>one</p>\n" ++
        "<p>two</p>\n" ++
        "</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 237, line 4116: '   > > 1.  one\\n>>\\n>>     two'" {
    const input =
        "   > > 1.  one\n" ++
        ">>\n" ++
        ">>     two\n";
    const expected =
        "<blockquote>\n" ++
        "<blockquote>\n" ++
        "<ol>\n" ++
        "<li>\n" ++
        "<p>one</p>\n" ++
        "<p>two</p>\n" ++
        "</li>\n" ++
        "</ol>\n" ++
        "</blockquote>\n" ++
        "</blockquote>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 238, line 4143: '>>- one\\n>>\\n  >  > two'" {
    const input =
        ">>- one\n" ++
        ">>\n" ++
        "  >  > two\n";
    const expected =
        "<blockquote>\n" ++
        "<blockquote>\n" ++
        "<ul>\n" ++
        "<li>one</li>\n" ++
        "</ul>\n" ++
        "<p>two</p>\n" ++
        "</blockquote>\n" ++
        "</blockquote>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 239, line 4162: '-one\\n\\n2.two'" {
    const input =
        "-one\n" ++
        "\n" ++
        "2.two\n";
    const expected =
        "<p>-one</p>\n" ++
        "<p>2.two</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 240, line 4175: '- foo\\n\\n\\n  bar'" {
    const input =
        "- foo\n" ++
        "\n" ++
        "\n" ++
        "  bar\n";
    const expected =
        "<ul>\n" ++
        "<li>\n" ++
        "<p>foo</p>\n" ++
        "<p>bar</p>\n" ++
        "</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 241, line 4192: '1.  foo\\n\\n    ```\\n    bar\\n    ```\\n\\n    baz\\n\\n    > bam'" {
    const input =
        "1.  foo\n" ++
        "\n" ++
        "    ```\n" ++
        "    bar\n" ++
        "    ```\n" ++
        "\n" ++
        "    baz\n" ++
        "\n" ++
        "    > bam\n";
    const expected =
        "<ol>\n" ++
        "<li>\n" ++
        "<p>foo</p>\n" ++
        "<pre><code>bar\n" ++
        "</code></pre>\n" ++
        "<p>baz</p>\n" ++
        "<blockquote>\n" ++
        "<p>bam</p>\n" ++
        "</blockquote>\n" ++
        "</li>\n" ++
        "</ol>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 242, line 4220: '- Foo\\n\\n      bar\\n\\n\\n      baz'" {
    const input =
        "- Foo\n" ++
        "\n" ++
        "      bar\n" ++
        "\n" ++
        "\n" ++
        "      baz\n";
    const expected =
        "<ul>\n" ++
        "<li>\n" ++
        "<p>Foo</p>\n" ++
        "<pre><code>bar\n" ++
        "\n" ++
        "\n" ++
        "baz\n" ++
        "</code></pre>\n" ++
        "</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 243, line 4242: '123456789. ok'" {
    const input =
        "123456789. ok\n";
    const expected =
        "<ol start=\"123456789\">\n" ++
        "<li>ok</li>\n" ++
        "</ol>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 244, line 4251: '1234567890. not ok'" {
    const input =
        "1234567890. not ok\n";
    const expected =
        "<p>1234567890. not ok</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 245, line 4260: '0. ok'" {
    const input =
        "0. ok\n";
    const expected =
        "<ol start=\"0\">\n" ++
        "<li>ok</li>\n" ++
        "</ol>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 246, line 4269: '003. ok'" {
    const input =
        "003. ok\n";
    const expected =
        "<ol start=\"3\">\n" ++
        "<li>ok</li>\n" ++
        "</ol>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 247, line 4280: '-1. not ok'" {
    const input =
        "-1. not ok\n";
    const expected =
        "<p>-1. not ok</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 248, line 4303: '- foo\\n\\n      bar'" {
    const input =
        "- foo\n" ++
        "\n" ++
        "      bar\n";
    const expected =
        "<ul>\n" ++
        "<li>\n" ++
        "<p>foo</p>\n" ++
        "<pre><code>bar\n" ++
        "</code></pre>\n" ++
        "</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 249, line 4320: '  10.  foo\\n\\n           bar'" {
    const input =
        "  10.  foo\n" ++
        "\n" ++
        "           bar\n";
    const expected =
        "<ol start=\"10\">\n" ++
        "<li>\n" ++
        "<p>foo</p>\n" ++
        "<pre><code>bar\n" ++
        "</code></pre>\n" ++
        "</li>\n" ++
        "</ol>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 250, line 4339: '    indented code\\n\\nparagraph\\n\\n    more code'" {
    const input =
        "    indented code\n" ++
        "\n" ++
        "paragraph\n" ++
        "\n" ++
        "    more code\n";
    const expected =
        "<pre><code>indented code\n" ++
        "</code></pre>\n" ++
        "<p>paragraph</p>\n" ++
        "<pre><code>more code\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 251, line 4354: '1.     indented code\\n\\n   paragraph\\n\\n       more code'" {
    const input =
        "1.     indented code\n" ++
        "\n" ++
        "   paragraph\n" ++
        "\n" ++
        "       more code\n";
    const expected =
        "<ol>\n" ++
        "<li>\n" ++
        "<pre><code>indented code\n" ++
        "</code></pre>\n" ++
        "<p>paragraph</p>\n" ++
        "<pre><code>more code\n" ++
        "</code></pre>\n" ++
        "</li>\n" ++
        "</ol>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 252, line 4376: '1.      indented code\\n\\n   paragraph\\n\\n       more code'" {
    const input =
        "1.      indented code\n" ++
        "\n" ++
        "   paragraph\n" ++
        "\n" ++
        "       more code\n";
    const expected =
        "<ol>\n" ++
        "<li>\n" ++
        "<pre><code> indented code\n" ++
        "</code></pre>\n" ++
        "<p>paragraph</p>\n" ++
        "<pre><code>more code\n" ++
        "</code></pre>\n" ++
        "</li>\n" ++
        "</ol>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 253, line 4403: '   foo\\n\\nbar'" {
    const input =
        "   foo\n" ++
        "\n" ++
        "bar\n";
    const expected =
        "<p>foo</p>\n" ++
        "<p>bar</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 254, line 4413: '-    foo\\n\\n  bar'" {
    const input =
        "-    foo\n" ++
        "\n" ++
        "  bar\n";
    const expected =
        "<ul>\n" ++
        "<li>foo</li>\n" ++
        "</ul>\n" ++
        "<p>bar</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 255, line 4430: '-  foo\\n\\n   bar'" {
    const input =
        "-  foo\n" ++
        "\n" ++
        "   bar\n";
    const expected =
        "<ul>\n" ++
        "<li>\n" ++
        "<p>foo</p>\n" ++
        "<p>bar</p>\n" ++
        "</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 256, line 4458: '-\\n  foo\\n-\\n  ```\\n  bar\\n  ```\\n-\\n      baz'" {
    const input =
        "-\n" ++
        "  foo\n" ++
        "-\n" ++
        "  ```\n" ++
        "  bar\n" ++
        "  ```\n" ++
        "-\n" ++
        "      baz\n";
    const expected =
        "<ul>\n" ++
        "<li>foo</li>\n" ++
        "<li>\n" ++
        "<pre><code>bar\n" ++
        "</code></pre>\n" ++
        "</li>\n" ++
        "<li>\n" ++
        "<pre><code>baz\n" ++
        "</code></pre>\n" ++
        "</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 257, line 4484: '-   \\n  foo'" {
    const input =
        "-   \n" ++
        "  foo\n";
    const expected =
        "<ul>\n" ++
        "<li>foo</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 258, line 4498: '-\\n\\n  foo'" {
    const input =
        "-\n" ++
        "\n" ++
        "  foo\n";
    const expected =
        "<ul>\n" ++
        "<li></li>\n" ++
        "</ul>\n" ++
        "<p>foo</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 259, line 4512: '- foo\\n-\\n- bar'" {
    const input =
        "- foo\n" ++
        "-\n" ++
        "- bar\n";
    const expected =
        "<ul>\n" ++
        "<li>foo</li>\n" ++
        "<li></li>\n" ++
        "<li>bar</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 260, line 4527: '- foo\\n-   \\n- bar'" {
    const input =
        "- foo\n" ++
        "-   \n" ++
        "- bar\n";
    const expected =
        "<ul>\n" ++
        "<li>foo</li>\n" ++
        "<li></li>\n" ++
        "<li>bar</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 261, line 4542: '1. foo\\n2.\\n3. bar'" {
    const input =
        "1. foo\n" ++
        "2.\n" ++
        "3. bar\n";
    const expected =
        "<ol>\n" ++
        "<li>foo</li>\n" ++
        "<li></li>\n" ++
        "<li>bar</li>\n" ++
        "</ol>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 262, line 4557: '*'" {
    const input =
        "*\n";
    const expected =
        "<ul>\n" ++
        "<li></li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 263, line 4567: 'foo\\n*\\n\\nfoo\\n1.'" {
    const input =
        "foo\n" ++
        "*\n" ++
        "\n" ++
        "foo\n" ++
        "1.\n";
    const expected =
        "<p>foo\n" ++
        "*</p>\n" ++
        "<p>foo\n" ++
        "1.</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 264, line 4589: ' 1.  A paragraph\\n     with two lines.\\n\\n         indented code\\n\\n     > A block quote.'" {
    const input =
        " 1.  A paragraph\n" ++
        "     with two lines.\n" ++
        "\n" ++
        "         indented code\n" ++
        "\n" ++
        "     > A block quote.\n";
    const expected =
        "<ol>\n" ++
        "<li>\n" ++
        "<p>A paragraph\n" ++
        "with two lines.</p>\n" ++
        "<pre><code>indented code\n" ++
        "</code></pre>\n" ++
        "<blockquote>\n" ++
        "<p>A block quote.</p>\n" ++
        "</blockquote>\n" ++
        "</li>\n" ++
        "</ol>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 265, line 4613: '  1.  A paragraph\\n      with two lines.\\n\\n          indented code\\n\\n      > A block quote.'" {
    const input =
        "  1.  A paragraph\n" ++
        "      with two lines.\n" ++
        "\n" ++
        "          indented code\n" ++
        "\n" ++
        "      > A block quote.\n";
    const expected =
        "<ol>\n" ++
        "<li>\n" ++
        "<p>A paragraph\n" ++
        "with two lines.</p>\n" ++
        "<pre><code>indented code\n" ++
        "</code></pre>\n" ++
        "<blockquote>\n" ++
        "<p>A block quote.</p>\n" ++
        "</blockquote>\n" ++
        "</li>\n" ++
        "</ol>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 266, line 4637: '   1.  A paragraph\\n       with two lines.\\n\\n           indented code\\n\\n       > A block quote.'" {
    const input =
        "   1.  A paragraph\n" ++
        "       with two lines.\n" ++
        "\n" ++
        "           indented code\n" ++
        "\n" ++
        "       > A block quote.\n";
    const expected =
        "<ol>\n" ++
        "<li>\n" ++
        "<p>A paragraph\n" ++
        "with two lines.</p>\n" ++
        "<pre><code>indented code\n" ++
        "</code></pre>\n" ++
        "<blockquote>\n" ++
        "<p>A block quote.</p>\n" ++
        "</blockquote>\n" ++
        "</li>\n" ++
        "</ol>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 267, line 4661: '    1.  A paragraph\\n        with two lines.\\n\\n            indented code\\n\\n        > A block quote.'" {
    const input =
        "    1.  A paragraph\n" ++
        "        with two lines.\n" ++
        "\n" ++
        "            indented code\n" ++
        "\n" ++
        "        > A block quote.\n";
    const expected =
        "<pre><code>1.  A paragraph\n" ++
        "    with two lines.\n" ++
        "\n" ++
        "        indented code\n" ++
        "\n" ++
        "    &gt; A block quote.\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 268, line 4691: '  1.  A paragraph\\nwith two lines.\\n\\n          indented code\\n\\n      > A block quote.'" {
    const input =
        "  1.  A paragraph\n" ++
        "with two lines.\n" ++
        "\n" ++
        "          indented code\n" ++
        "\n" ++
        "      > A block quote.\n";
    const expected =
        "<ol>\n" ++
        "<li>\n" ++
        "<p>A paragraph\n" ++
        "with two lines.</p>\n" ++
        "<pre><code>indented code\n" ++
        "</code></pre>\n" ++
        "<blockquote>\n" ++
        "<p>A block quote.</p>\n" ++
        "</blockquote>\n" ++
        "</li>\n" ++
        "</ol>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 269, line 4715: '  1.  A paragraph\\n    with two lines.'" {
    const input =
        "  1.  A paragraph\n" ++
        "    with two lines.\n";
    const expected =
        "<ol>\n" ++
        "<li>A paragraph\n" ++
        "with two lines.</li>\n" ++
        "</ol>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 270, line 4728: '> 1. > Blockquote\\ncontinued here.'" {
    const input =
        "> 1. > Blockquote\n" ++
        "continued here.\n";
    const expected =
        "<blockquote>\n" ++
        "<ol>\n" ++
        "<li>\n" ++
        "<blockquote>\n" ++
        "<p>Blockquote\n" ++
        "continued here.</p>\n" ++
        "</blockquote>\n" ++
        "</li>\n" ++
        "</ol>\n" ++
        "</blockquote>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 271, line 4745: '> 1. > Blockquote\\n> continued here.'" {
    const input =
        "> 1. > Blockquote\n" ++
        "> continued here.\n";
    const expected =
        "<blockquote>\n" ++
        "<ol>\n" ++
        "<li>\n" ++
        "<blockquote>\n" ++
        "<p>Blockquote\n" ++
        "continued here.</p>\n" ++
        "</blockquote>\n" ++
        "</li>\n" ++
        "</ol>\n" ++
        "</blockquote>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 272, line 4773: '- foo\\n  - bar\\n    - baz\\n      - boo'" {
    const input =
        "- foo\n" ++
        "  - bar\n" ++
        "    - baz\n" ++
        "      - boo\n";
    const expected =
        "<ul>\n" ++
        "<li>foo\n" ++
        "<ul>\n" ++
        "<li>bar\n" ++
        "<ul>\n" ++
        "<li>baz\n" ++
        "<ul>\n" ++
        "<li>boo</li>\n" ++
        "</ul>\n" ++
        "</li>\n" ++
        "</ul>\n" ++
        "</li>\n" ++
        "</ul>\n" ++
        "</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 273, line 4799: '- foo\\n - bar\\n  - baz\\n   - boo'" {
    const input =
        "- foo\n" ++
        " - bar\n" ++
        "  - baz\n" ++
        "   - boo\n";
    const expected =
        "<ul>\n" ++
        "<li>foo</li>\n" ++
        "<li>bar</li>\n" ++
        "<li>baz</li>\n" ++
        "<li>boo</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 274, line 4816: '10) foo\\n    - bar'" {
    const input =
        "10) foo\n" ++
        "    - bar\n";
    const expected =
        "<ol start=\"10\">\n" ++
        "<li>foo\n" ++
        "<ul>\n" ++
        "<li>bar</li>\n" ++
        "</ul>\n" ++
        "</li>\n" ++
        "</ol>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 275, line 4832: '10) foo\\n   - bar'" {
    const input =
        "10) foo\n" ++
        "   - bar\n";
    const expected =
        "<ol start=\"10\">\n" ++
        "<li>foo</li>\n" ++
        "</ol>\n" ++
        "<ul>\n" ++
        "<li>bar</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 276, line 4847: '- - foo'" {
    const input =
        "- - foo\n";
    const expected =
        "<ul>\n" ++
        "<li>\n" ++
        "<ul>\n" ++
        "<li>foo</li>\n" ++
        "</ul>\n" ++
        "</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 277, line 4860: '1. - 2. foo'" {
    const input =
        "1. - 2. foo\n";
    const expected =
        "<ol>\n" ++
        "<li>\n" ++
        "<ul>\n" ++
        "<li>\n" ++
        "<ol start=\"2\">\n" ++
        "<li>foo</li>\n" ++
        "</ol>\n" ++
        "</li>\n" ++
        "</ul>\n" ++
        "</li>\n" ++
        "</ol>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 278, line 4879: '- # Foo\\n- Bar\\n  ---\\n  baz'" {
    const input =
        "- # Foo\n" ++
        "- Bar\n" ++
        "  ---\n" ++
        "  baz\n";
    const expected =
        "<ul>\n" ++
        "<li>\n" ++
        "<h1>Foo</h1>\n" ++
        "</li>\n" ++
        "<li>\n" ++
        "<h2>Bar</h2>\n" ++
        "baz</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 279, line 5108: '- [ ] foo\\n- [x] bar'" {
    const input =
        "- [ ] foo\n" ++
        "- [x] bar\n";
    const expected =
        "<ul>\n" ++
        "<li><input type=\"checkbox\" disabled=\"\" /> foo</li>\n" ++
        "<li><input type=\"checkbox\" checked=\"\" disabled=\"\" /> bar</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 280, line 5120: '- [x] foo\\n  - [ ] bar\\n  - [x] baz\\n- [ ] bim'" {
    const input =
        "- [x] foo\n" ++
        "  - [ ] bar\n" ++
        "  - [x] baz\n" ++
        "- [ ] bim\n";
    const expected =
        "<ul>\n" ++
        "<li><input type=\"checkbox\" checked=\"\" disabled=\"\" /> foo\n" ++
        "<ul>\n" ++
        "<li><input type=\"checkbox\" disabled=\"\" /> bar</li>\n" ++
        "<li><input type=\"checkbox\" checked=\"\" disabled=\"\" /> baz</li>\n" ++
        "</ul>\n" ++
        "</li>\n" ++
        "<li><input type=\"checkbox\" disabled=\"\" /> bim</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 281, line 5172: '- foo\\n- bar\\n+ baz'" {
    const input =
        "- foo\n" ++
        "- bar\n" ++
        "+ baz\n";
    const expected =
        "<ul>\n" ++
        "<li>foo</li>\n" ++
        "<li>bar</li>\n" ++
        "</ul>\n" ++
        "<ul>\n" ++
        "<li>baz</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 282, line 5187: '1. foo\\n2. bar\\n3) baz'" {
    const input =
        "1. foo\n" ++
        "2. bar\n" ++
        "3) baz\n";
    const expected =
        "<ol>\n" ++
        "<li>foo</li>\n" ++
        "<li>bar</li>\n" ++
        "</ol>\n" ++
        "<ol start=\"3\">\n" ++
        "<li>baz</li>\n" ++
        "</ol>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 283, line 5206: 'Foo\\n- bar\\n- baz'" {
    const input =
        "Foo\n" ++
        "- bar\n" ++
        "- baz\n";
    const expected =
        "<p>Foo</p>\n" ++
        "<ul>\n" ++
        "<li>bar</li>\n" ++
        "<li>baz</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 284, line 5283: 'The number of windows in my house is\\n14.  The number of doors is 6.'" {
    const input =
        "The number of windows in my house is\n" ++
        "14.  The number of doors is 6.\n";
    const expected =
        "<p>The number of windows in my house is\n" ++
        "14.  The number of doors is 6.</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 285, line 5293: 'The number of windows in my house is\\n1.  The number of doors is 6.'" {
    const input =
        "The number of windows in my house is\n" ++
        "1.  The number of doors is 6.\n";
    const expected =
        "<p>The number of windows in my house is</p>\n" ++
        "<ol>\n" ++
        "<li>The number of doors is 6.</li>\n" ++
        "</ol>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 286, line 5307: '- foo\\n\\n- bar\\n\\n\\n- baz'" {
    const input =
        "- foo\n" ++
        "\n" ++
        "- bar\n" ++
        "\n" ++
        "\n" ++
        "- baz\n";
    const expected =
        "<ul>\n" ++
        "<li>\n" ++
        "<p>foo</p>\n" ++
        "</li>\n" ++
        "<li>\n" ++
        "<p>bar</p>\n" ++
        "</li>\n" ++
        "<li>\n" ++
        "<p>baz</p>\n" ++
        "</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 287, line 5328: '- foo\\n  - bar\\n    - baz\\n\\n\\n      bim'" {
    const input =
        "- foo\n" ++
        "  - bar\n" ++
        "    - baz\n" ++
        "\n" ++
        "\n" ++
        "      bim\n";
    const expected =
        "<ul>\n" ++
        "<li>foo\n" ++
        "<ul>\n" ++
        "<li>bar\n" ++
        "<ul>\n" ++
        "<li>\n" ++
        "<p>baz</p>\n" ++
        "<p>bim</p>\n" ++
        "</li>\n" ++
        "</ul>\n" ++
        "</li>\n" ++
        "</ul>\n" ++
        "</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 288, line 5358: '- foo\\n- bar\\n\\n<!-- -->\\n\\n- baz\\n- bim'" {
    const input =
        "- foo\n" ++
        "- bar\n" ++
        "\n" ++
        "<!-- -->\n" ++
        "\n" ++
        "- baz\n" ++
        "- bim\n";
    const expected =
        "<ul>\n" ++
        "<li>foo</li>\n" ++
        "<li>bar</li>\n" ++
        "</ul>\n" ++
        "<!-- -->\n" ++
        "<ul>\n" ++
        "<li>baz</li>\n" ++
        "<li>bim</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 289, line 5379: '-   foo\\n\\n    notcode\\n\\n-   foo\\n\\n<!-- -->\\n\\n    code'" {
    const input =
        "-   foo\n" ++
        "\n" ++
        "    notcode\n" ++
        "\n" ++
        "-   foo\n" ++
        "\n" ++
        "<!-- -->\n" ++
        "\n" ++
        "    code\n";
    const expected =
        "<ul>\n" ++
        "<li>\n" ++
        "<p>foo</p>\n" ++
        "<p>notcode</p>\n" ++
        "</li>\n" ++
        "<li>\n" ++
        "<p>foo</p>\n" ++
        "</li>\n" ++
        "</ul>\n" ++
        "<!-- -->\n" ++
        "<pre><code>code\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 290, line 5410: '- a\\n - b\\n  - c\\n   - d\\n  - e\\n - f\\n- g'" {
    const input =
        "- a\n" ++
        " - b\n" ++
        "  - c\n" ++
        "   - d\n" ++
        "  - e\n" ++
        " - f\n" ++
        "- g\n";
    const expected =
        "<ul>\n" ++
        "<li>a</li>\n" ++
        "<li>b</li>\n" ++
        "<li>c</li>\n" ++
        "<li>d</li>\n" ++
        "<li>e</li>\n" ++
        "<li>f</li>\n" ++
        "<li>g</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 291, line 5431: '1. a\\n\\n  2. b\\n\\n   3. c'" {
    const input =
        "1. a\n" ++
        "\n" ++
        "  2. b\n" ++
        "\n" ++
        "   3. c\n";
    const expected =
        "<ol>\n" ++
        "<li>\n" ++
        "<p>a</p>\n" ++
        "</li>\n" ++
        "<li>\n" ++
        "<p>b</p>\n" ++
        "</li>\n" ++
        "<li>\n" ++
        "<p>c</p>\n" ++
        "</li>\n" ++
        "</ol>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 292, line 5455: '- a\\n - b\\n  - c\\n   - d\\n    - e'" {
    const input =
        "- a\n" ++
        " - b\n" ++
        "  - c\n" ++
        "   - d\n" ++
        "    - e\n";
    const expected =
        "<ul>\n" ++
        "<li>a</li>\n" ++
        "<li>b</li>\n" ++
        "<li>c</li>\n" ++
        "<li>d\n" ++
        "- e</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 293, line 5475: '1. a\\n\\n  2. b\\n\\n    3. c'" {
    const input =
        "1. a\n" ++
        "\n" ++
        "  2. b\n" ++
        "\n" ++
        "    3. c\n";
    const expected =
        "<ol>\n" ++
        "<li>\n" ++
        "<p>a</p>\n" ++
        "</li>\n" ++
        "<li>\n" ++
        "<p>b</p>\n" ++
        "</li>\n" ++
        "</ol>\n" ++
        "<pre><code>3. c\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 294, line 5498: '- a\\n- b\\n\\n- c'" {
    const input =
        "- a\n" ++
        "- b\n" ++
        "\n" ++
        "- c\n";
    const expected =
        "<ul>\n" ++
        "<li>\n" ++
        "<p>a</p>\n" ++
        "</li>\n" ++
        "<li>\n" ++
        "<p>b</p>\n" ++
        "</li>\n" ++
        "<li>\n" ++
        "<p>c</p>\n" ++
        "</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 295, line 5520: '* a\\n*\\n\\n* c'" {
    const input =
        "* a\n" ++
        "*\n" ++
        "\n" ++
        "* c\n";
    const expected =
        "<ul>\n" ++
        "<li>\n" ++
        "<p>a</p>\n" ++
        "</li>\n" ++
        "<li></li>\n" ++
        "<li>\n" ++
        "<p>c</p>\n" ++
        "</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 296, line 5542: '- a\\n- b\\n\\n  c\\n- d'" {
    const input =
        "- a\n" ++
        "- b\n" ++
        "\n" ++
        "  c\n" ++
        "- d\n";
    const expected =
        "<ul>\n" ++
        "<li>\n" ++
        "<p>a</p>\n" ++
        "</li>\n" ++
        "<li>\n" ++
        "<p>b</p>\n" ++
        "<p>c</p>\n" ++
        "</li>\n" ++
        "<li>\n" ++
        "<p>d</p>\n" ++
        "</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 297, line 5564: '- a\\n- b\\n\\n  [ref]: /url\\n- d'" {
    const input =
        "- a\n" ++
        "- b\n" ++
        "\n" ++
        "  [ref]: /url\n" ++
        "- d\n";
    const expected =
        "<ul>\n" ++
        "<li>\n" ++
        "<p>a</p>\n" ++
        "</li>\n" ++
        "<li>\n" ++
        "<p>b</p>\n" ++
        "</li>\n" ++
        "<li>\n" ++
        "<p>d</p>\n" ++
        "</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 298, line 5587: '- a\\n- ```\\n  b\\n\\n\\n  ```\\n- c'" {
    const input =
        "- a\n" ++
        "- ```\n" ++
        "  b\n" ++
        "\n" ++
        "\n" ++
        "  ```\n" ++
        "- c\n";
    const expected =
        "<ul>\n" ++
        "<li>a</li>\n" ++
        "<li>\n" ++
        "<pre><code>b\n" ++
        "\n" ++
        "\n" ++
        "</code></pre>\n" ++
        "</li>\n" ++
        "<li>c</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 299, line 5613: '- a\\n  - b\\n\\n    c\\n- d'" {
    const input =
        "- a\n" ++
        "  - b\n" ++
        "\n" ++
        "    c\n" ++
        "- d\n";
    const expected =
        "<ul>\n" ++
        "<li>a\n" ++
        "<ul>\n" ++
        "<li>\n" ++
        "<p>b</p>\n" ++
        "<p>c</p>\n" ++
        "</li>\n" ++
        "</ul>\n" ++
        "</li>\n" ++
        "<li>d</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 300, line 5637: '* a\\n  > b\\n  >\\n* c'" {
    const input =
        "* a\n" ++
        "  > b\n" ++
        "  >\n" ++
        "* c\n";
    const expected =
        "<ul>\n" ++
        "<li>a\n" ++
        "<blockquote>\n" ++
        "<p>b</p>\n" ++
        "</blockquote>\n" ++
        "</li>\n" ++
        "<li>c</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 301, line 5657: '- a\\n  > b\\n  ```\\n  c\\n  ```\\n- d'" {
    const input =
        "- a\n" ++
        "  > b\n" ++
        "  ```\n" ++
        "  c\n" ++
        "  ```\n" ++
        "- d\n";
    const expected =
        "<ul>\n" ++
        "<li>a\n" ++
        "<blockquote>\n" ++
        "<p>b</p>\n" ++
        "</blockquote>\n" ++
        "<pre><code>c\n" ++
        "</code></pre>\n" ++
        "</li>\n" ++
        "<li>d</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 302, line 5680: '- a'" {
    const input =
        "- a\n";
    const expected =
        "<ul>\n" ++
        "<li>a</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 303, line 5689: '- a\\n  - b'" {
    const input =
        "- a\n" ++
        "  - b\n";
    const expected =
        "<ul>\n" ++
        "<li>a\n" ++
        "<ul>\n" ++
        "<li>b</li>\n" ++
        "</ul>\n" ++
        "</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 304, line 5706: '1. ```\\n   foo\\n   ```\\n\\n   bar'" {
    const input =
        "1. ```\n" ++
        "   foo\n" ++
        "   ```\n" ++
        "\n" ++
        "   bar\n";
    const expected =
        "<ol>\n" ++
        "<li>\n" ++
        "<pre><code>foo\n" ++
        "</code></pre>\n" ++
        "<p>bar</p>\n" ++
        "</li>\n" ++
        "</ol>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 305, line 5725: '* foo\\n  * bar\\n\\n  baz'" {
    const input =
        "* foo\n" ++
        "  * bar\n" ++
        "\n" ++
        "  baz\n";
    const expected =
        "<ul>\n" ++
        "<li>\n" ++
        "<p>foo</p>\n" ++
        "<ul>\n" ++
        "<li>bar</li>\n" ++
        "</ul>\n" ++
        "<p>baz</p>\n" ++
        "</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 306, line 5743: '- a\\n  - b\\n  - c\\n\\n- d\\n  - e\\n  - f'" {
    const input =
        "- a\n" ++
        "  - b\n" ++
        "  - c\n" ++
        "\n" ++
        "- d\n" ++
        "  - e\n" ++
        "  - f\n";
    const expected =
        "<ul>\n" ++
        "<li>\n" ++
        "<p>a</p>\n" ++
        "<ul>\n" ++
        "<li>b</li>\n" ++
        "<li>c</li>\n" ++
        "</ul>\n" ++
        "</li>\n" ++
        "<li>\n" ++
        "<p>d</p>\n" ++
        "<ul>\n" ++
        "<li>e</li>\n" ++
        "<li>f</li>\n" ++
        "</ul>\n" ++
        "</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 307, line 5777: '`hi`lo`'" {
    const input =
        "`hi`lo`\n";
    const expected =
        "<p><code>hi</code>lo`</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 308, line 5791: '\\!\\\"\\#\\$\\%\\&\\'\\(\\)\\*\\+\\,\\-\\.\\/\\:\\;\\<\\=\\>\\?\\@\\[\\\\\\]\\^\\_\\`\\{\\|\\}\\~'" {
    const input =
        "\\!\\\"\\#\\$\\%\\&\\'\\(\\)\\*\\+\\,\\-\\.\\/\\:\\;\\<\\=\\>\\?\\@\\[\\\\\\]\\^\\_\\`\\{\\|\\}\\~\n";
    const expected =
        "<p>!&quot;#$%&amp;'()*+,-./:;&lt;=&gt;?@[\\]^_`{|}~</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 309, line 5801: '\\→\\A\\a\\ \\3\\φ\\«'" {
    const input =
        "\\\t\\A\\a\\ \\3\\φ\\«\n";
    const expected =
        "<p>\\\t\\A\\a\\ \\3\\φ\\«</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 310, line 5811: '\\*not emphasized*\\n\\<br/> not a tag\\n\\[not a link](/foo)\\n\\`not code`\\n1\\. not a list\\n\\* not a list\\n\\# not a heading\\n\\[foo]: /url \"not a reference\"\\n\\&ouml; not a character entity'" {
    const input =
        "\\*not emphasized*\n" ++
        "\\<br/> not a tag\n" ++
        "\\[not a link](/foo)\n" ++
        "\\`not code`\n" ++
        "1\\. not a list\n" ++
        "\\* not a list\n" ++
        "\\# not a heading\n" ++
        "\\[foo]: /url \"not a reference\"\n" ++
        "\\&ouml; not a character entity\n";
    const expected =
        "<p>*not emphasized*\n" ++
        "&lt;br/&gt; not a tag\n" ++
        "[not a link](/foo)\n" ++
        "`not code`\n" ++
        "1. not a list\n" ++
        "* not a list\n" ++
        "# not a heading\n" ++
        "[foo]: /url &quot;not a reference&quot;\n" ++
        "&amp;ouml; not a character entity</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 311, line 5836: '\\\\*emphasis*'" {
    const input =
        "\\\\*emphasis*\n";
    const expected =
        "<p>\\<em>emphasis</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 312, line 5845: 'foo\\\\nbar'" {
    const input =
        "foo\\\n" ++
        "bar\n";
    const expected =
        "<p>foo<br />\n" ++
        "bar</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 313, line 5857: '`` \\[\\` ``'" {
    const input =
        "`` \\[\\` ``\n";
    const expected =
        "<p><code>\\[\\`</code></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 314, line 5864: '    \\[\\]'" {
    const input =
        "    \\[\\]\n";
    const expected =
        "<pre><code>\\[\\]\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 315, line 5872: '~~~\\n\\[\\]\\n~~~'" {
    const input =
        "~~~\n" ++
        "\\[\\]\n" ++
        "~~~\n";
    const expected =
        "<pre><code>\\[\\]\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 316, line 5882: '<http://example.com?find=\\*>'" {
    const input =
        "<http://example.com?find=\\*>\n";
    const expected =
        "<p><a href=\"http://example.com?find=%5C*\">http://example.com?find=\\*</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 317, line 5889: '<a href=\"/bar\\/)\">'" {
    const input =
        "<a href=\"/bar\\/)\">\n";
    const expected =
        "<a href=\"/bar\\/)\">\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 318, line 5899: '[foo](/bar\\* \"ti\\*tle\")'" {
    const input =
        "[foo](/bar\\* \"ti\\*tle\")\n";
    const expected =
        "<p><a href=\"/bar*\" title=\"ti*tle\">foo</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 319, line 5906: '[foo]\\n\\n[foo]: /bar\\* \"ti\\*tle\"'" {
    const input =
        "[foo]\n" ++
        "\n" ++
        "[foo]: /bar\\* \"ti\\*tle\"\n";
    const expected =
        "<p><a href=\"/bar*\" title=\"ti*tle\">foo</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 320, line 5915: '``` foo\\+bar\\nfoo\\n```'" {
    const input =
        "``` foo\\+bar\n" ++
        "foo\n" ++
        "```\n";
    const expected =
        "<pre><code class=\"language-foo+bar\">foo\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 321, line 5952: '&nbsp; &amp; &copy; &AElig; &Dcaron;\\n&frac34; &HilbertSpace; &DifferentialD;\\n&ClockwiseContourIntegral; &ngE;'" {
    const input =
        "&nbsp; &amp; &copy; &AElig; &Dcaron;\n" ++
        "&frac34; &HilbertSpace; &DifferentialD;\n" ++
        "&ClockwiseContourIntegral; &ngE;\n";
    const expected =
        "<p>  &amp; © Æ Ď\n" ++
        "¾ ℋ ⅆ\n" ++
        "∲ ≧̸</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 322, line 5971: '&#35; &#1234; &#992; &#0;'" {
    const input =
        "&#35; &#1234; &#992; &#0;\n";
    const expected =
        "<p># Ӓ Ϡ �</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 323, line 5984: '&#X22; &#XD06; &#xcab;'" {
    const input =
        "&#X22; &#XD06; &#xcab;\n";
    const expected =
        "<p>&quot; ആ ಫ</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 324, line 5993: '&nbsp &x; &#; &#x;\\n&#987654321;\\n&#abcdef0;\\n&ThisIsNotDefined; &hi?;'" {
    const input =
        "&nbsp &x; &#; &#x;\n" ++
        "&#987654321;\n" ++
        "&#abcdef0;\n" ++
        "&ThisIsNotDefined; &hi?;\n";
    const expected =
        "<p>&amp;nbsp &amp;x; &amp;#; &amp;#x;\n" ++
        "&amp;#987654321;\n" ++
        "&amp;#abcdef0;\n" ++
        "&amp;ThisIsNotDefined; &amp;hi?;</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 325, line 6010: '&copy'" {
    const input =
        "&copy\n";
    const expected =
        "<p>&amp;copy</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 326, line 6020: '&MadeUpEntity;'" {
    const input =
        "&MadeUpEntity;\n";
    const expected =
        "<p>&amp;MadeUpEntity;</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 327, line 6031: '<a href=\"&ouml;&ouml;.html\">'" {
    const input =
        "<a href=\"&ouml;&ouml;.html\">\n";
    const expected =
        "<a href=\"&ouml;&ouml;.html\">\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 328, line 6038: '[foo](/f&ouml;&ouml; \"f&ouml;&ouml;\")'" {
    const input =
        "[foo](/f&ouml;&ouml; \"f&ouml;&ouml;\")\n";
    const expected =
        "<p><a href=\"/f%C3%B6%C3%B6\" title=\"föö\">foo</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 329, line 6045: '[foo]\\n\\n[foo]: /f&ouml;&ouml; \"f&ouml;&ouml;\"'" {
    const input =
        "[foo]\n" ++
        "\n" ++
        "[foo]: /f&ouml;&ouml; \"f&ouml;&ouml;\"\n";
    const expected =
        "<p><a href=\"/f%C3%B6%C3%B6\" title=\"föö\">foo</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 330, line 6054: '``` f&ouml;&ouml;\\nfoo\\n```'" {
    const input =
        "``` f&ouml;&ouml;\n" ++
        "foo\n" ++
        "```\n";
    const expected =
        "<pre><code class=\"language-föö\">foo\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 331, line 6067: '`f&ouml;&ouml;`'" {
    const input =
        "`f&ouml;&ouml;`\n";
    const expected =
        "<p><code>f&amp;ouml;&amp;ouml;</code></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 332, line 6074: '    f&ouml;f&ouml;'" {
    const input =
        "    f&ouml;f&ouml;\n";
    const expected =
        "<pre><code>f&amp;ouml;f&amp;ouml;\n" ++
        "</code></pre>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 333, line 6086: '&#42;foo&#42;\\n*foo*'" {
    const input =
        "&#42;foo&#42;\n" ++
        "*foo*\n";
    const expected =
        "<p>*foo*\n" ++
        "<em>foo</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 334, line 6094: '&#42; foo\\n\\n* foo'" {
    const input =
        "&#42; foo\n" ++
        "\n" ++
        "* foo\n";
    const expected =
        "<p>* foo</p>\n" ++
        "<ul>\n" ++
        "<li>foo</li>\n" ++
        "</ul>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 335, line 6105: 'foo&#10;&#10;bar'" {
    const input =
        "foo&#10;&#10;bar\n";
    const expected =
        "<p>foo\n" ++
        "\n" ++
        "bar</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 336, line 6113: '&#9;foo'" {
    const input =
        "&#9;foo\n";
    const expected =
        "<p>\tfoo</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 337, line 6120: '[a](url &quot;tit&quot;)'" {
    const input =
        "[a](url &quot;tit&quot;)\n";
    const expected =
        "<p>[a](url &quot;tit&quot;)</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 338, line 6148: '`foo`'" {
    const input =
        "`foo`\n";
    const expected =
        "<p><code>foo</code></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 339, line 6159: '`` foo ` bar ``'" {
    const input =
        "`` foo ` bar ``\n";
    const expected =
        "<p><code>foo ` bar</code></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 340, line 6169: '` `` `'" {
    const input =
        "` `` `\n";
    const expected =
        "<p><code>``</code></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 341, line 6177: '`  ``  `'" {
    const input =
        "`  ``  `\n";
    const expected =
        "<p><code> `` </code></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 342, line 6186: '` a`'" {
    const input =
        "` a`\n";
    const expected =
        "<p><code> a</code></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 343, line 6195: '` b `'" {
    const input =
        "` b `\n";
    const expected =
        "<p><code> b </code></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 344, line 6203: '` `\\n`  `'" {
    const input =
        "` `\n" ++
        "`  `\n";
    const expected =
        "<p><code> </code>\n" ++
        "<code>  </code></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 345, line 6214: '``\\nfoo\\nbar  \\nbaz\\n``'" {
    const input =
        "``\n" ++
        "foo\n" ++
        "bar  \n" ++
        "baz\n" ++
        "``\n";
    const expected =
        "<p><code>foo bar   baz</code></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 346, line 6224: '``\\nfoo \\n``'" {
    const input =
        "``\n" ++
        "foo \n" ++
        "``\n";
    const expected =
        "<p><code>foo </code></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 347, line 6235: '`foo   bar \\nbaz`'" {
    const input =
        "`foo   bar \n" ++
        "baz`\n";
    const expected =
        "<p><code>foo   bar  baz</code></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 348, line 6252: '`foo\\`bar`'" {
    const input =
        "`foo\\`bar`\n";
    const expected =
        "<p><code>foo\\</code>bar`</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 349, line 6263: '``foo`bar``'" {
    const input =
        "``foo`bar``\n";
    const expected =
        "<p><code>foo`bar</code></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 350, line 6269: '` foo `` bar `'" {
    const input =
        "` foo `` bar `\n";
    const expected =
        "<p><code>foo `` bar</code></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 351, line 6281: '*foo`*`'" {
    const input =
        "*foo`*`\n";
    const expected =
        "<p>*foo<code>*</code></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 352, line 6290: '[not a `link](/foo`)'" {
    const input =
        "[not a `link](/foo`)\n";
    const expected =
        "<p>[not a <code>link](/foo</code>)</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 353, line 6300: '`<a href=\"`\">`'" {
    const input =
        "`<a href=\"`\">`\n";
    const expected =
        "<p><code>&lt;a href=&quot;</code>&quot;&gt;`</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 354, line 6309: '<a href=\"`\">`'" {
    const input =
        "<a href=\"`\">`\n";
    const expected =
        "<p><a href=\"`\">`</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 355, line 6318: '`<http://foo.bar.`baz>`'" {
    const input =
        "`<http://foo.bar.`baz>`\n";
    const expected =
        "<p><code>&lt;http://foo.bar.</code>baz&gt;`</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 356, line 6327: '<http://foo.bar.`baz>`'" {
    const input =
        "<http://foo.bar.`baz>`\n";
    const expected =
        "<p><a href=\"http://foo.bar.%60baz\">http://foo.bar.`baz</a>`</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 357, line 6337: '```foo``'" {
    const input =
        "```foo``\n";
    const expected =
        "<p>```foo``</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 358, line 6344: '`foo'" {
    const input =
        "`foo\n";
    const expected =
        "<p>`foo</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 359, line 6353: '`foo``bar``'" {
    const input =
        "`foo``bar``\n";
    const expected =
        "<p>`foo<code>bar</code></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 360, line 6570: '*foo bar*'" {
    const input =
        "*foo bar*\n";
    const expected =
        "<p><em>foo bar</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 361, line 6580: 'a * foo bar*'" {
    const input =
        "a * foo bar*\n";
    const expected =
        "<p>a * foo bar*</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 362, line 6591: 'a*\"foo\"*'" {
    const input =
        "a*\"foo\"*\n";
    const expected =
        "<p>a*&quot;foo&quot;*</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 363, line 6600: '* a *'" {
    const input =
        "* a *\n";
    const expected =
        "<p>* a *</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 364, line 6609: 'foo*bar*'" {
    const input =
        "foo*bar*\n";
    const expected =
        "<p>foo<em>bar</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 365, line 6616: '5*6*78'" {
    const input =
        "5*6*78\n";
    const expected =
        "<p>5<em>6</em>78</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 366, line 6625: '_foo bar_'" {
    const input =
        "_foo bar_\n";
    const expected =
        "<p><em>foo bar</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 367, line 6635: '_ foo bar_'" {
    const input =
        "_ foo bar_\n";
    const expected =
        "<p>_ foo bar_</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 368, line 6645: 'a_\"foo\"_'" {
    const input =
        "a_\"foo\"_\n";
    const expected =
        "<p>a_&quot;foo&quot;_</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 369, line 6654: 'foo_bar_'" {
    const input =
        "foo_bar_\n";
    const expected =
        "<p>foo_bar_</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 370, line 6661: '5_6_78'" {
    const input =
        "5_6_78\n";
    const expected =
        "<p>5_6_78</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 371, line 6668: 'пристаням_стремятся_'" {
    const input =
        "пристаням_стремятся_\n";
    const expected =
        "<p>пристаням_стремятся_</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 372, line 6678: 'aa_\"bb\"_cc'" {
    const input =
        "aa_\"bb\"_cc\n";
    const expected =
        "<p>aa_&quot;bb&quot;_cc</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 373, line 6689: 'foo-_(bar)_'" {
    const input =
        "foo-_(bar)_\n";
    const expected =
        "<p>foo-<em>(bar)</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 374, line 6701: '_foo*'" {
    const input =
        "_foo*\n";
    const expected =
        "<p>_foo*</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 375, line 6711: '*foo bar *'" {
    const input =
        "*foo bar *\n";
    const expected =
        "<p>*foo bar *</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 376, line 6720: '*foo bar\\n*'" {
    const input =
        "*foo bar\n" ++
        "*\n";
    const expected =
        "<p>*foo bar\n" ++
        "*</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 377, line 6733: '*(*foo)'" {
    const input =
        "*(*foo)\n";
    const expected =
        "<p>*(*foo)</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 378, line 6743: '*(*foo*)*'" {
    const input =
        "*(*foo*)*\n";
    const expected =
        "<p><em>(<em>foo</em>)</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 379, line 6752: '*foo*bar'" {
    const input =
        "*foo*bar\n";
    const expected =
        "<p><em>foo</em>bar</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 380, line 6765: '_foo bar _'" {
    const input =
        "_foo bar _\n";
    const expected =
        "<p>_foo bar _</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 381, line 6775: '_(_foo)'" {
    const input =
        "_(_foo)\n";
    const expected =
        "<p>_(_foo)</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 382, line 6784: '_(_foo_)_'" {
    const input =
        "_(_foo_)_\n";
    const expected =
        "<p><em>(<em>foo</em>)</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 383, line 6793: '_foo_bar'" {
    const input =
        "_foo_bar\n";
    const expected =
        "<p>_foo_bar</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 384, line 6800: '_пристаням_стремятся'" {
    const input =
        "_пристаням_стремятся\n";
    const expected =
        "<p>_пристаням_стремятся</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 385, line 6807: '_foo_bar_baz_'" {
    const input =
        "_foo_bar_baz_\n";
    const expected =
        "<p><em>foo_bar_baz</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 386, line 6818: '_(bar)_.'" {
    const input =
        "_(bar)_.\n";
    const expected =
        "<p><em>(bar)</em>.</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 387, line 6827: '**foo bar**'" {
    const input =
        "**foo bar**\n";
    const expected =
        "<p><strong>foo bar</strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 388, line 6837: '** foo bar**'" {
    const input =
        "** foo bar**\n";
    const expected =
        "<p>** foo bar**</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 389, line 6848: 'a**\"foo\"**'" {
    const input =
        "a**\"foo\"**\n";
    const expected =
        "<p>a**&quot;foo&quot;**</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 390, line 6857: 'foo**bar**'" {
    const input =
        "foo**bar**\n";
    const expected =
        "<p>foo<strong>bar</strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 391, line 6866: '__foo bar__'" {
    const input =
        "__foo bar__\n";
    const expected =
        "<p><strong>foo bar</strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 392, line 6876: '__ foo bar__'" {
    const input =
        "__ foo bar__\n";
    const expected =
        "<p>__ foo bar__</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 393, line 6884: '__\\nfoo bar__'" {
    const input =
        "__\n" ++
        "foo bar__\n";
    const expected =
        "<p>__\n" ++
        "foo bar__</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 394, line 6896: 'a__\"foo\"__'" {
    const input =
        "a__\"foo\"__\n";
    const expected =
        "<p>a__&quot;foo&quot;__</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 395, line 6905: 'foo__bar__'" {
    const input =
        "foo__bar__\n";
    const expected =
        "<p>foo__bar__</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 396, line 6912: '5__6__78'" {
    const input =
        "5__6__78\n";
    const expected =
        "<p>5__6__78</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 397, line 6919: 'пристаням__стремятся__'" {
    const input =
        "пристаням__стремятся__\n";
    const expected =
        "<p>пристаням__стремятся__</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 398, line 6926: '__foo, __bar__, baz__'" {
    const input =
        "__foo, __bar__, baz__\n";
    const expected =
        "<p><strong>foo, <strong>bar</strong>, baz</strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 399, line 6937: 'foo-__(bar)__'" {
    const input =
        "foo-__(bar)__\n";
    const expected =
        "<p>foo-<strong>(bar)</strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 400, line 6950: '**foo bar **'" {
    const input =
        "**foo bar **\n";
    const expected =
        "<p>**foo bar **</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 401, line 6963: '**(**foo)'" {
    const input =
        "**(**foo)\n";
    const expected =
        "<p>**(**foo)</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 402, line 6973: '*(**foo**)*'" {
    const input =
        "*(**foo**)*\n";
    const expected =
        "<p><em>(<strong>foo</strong>)</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 403, line 6980: '**Gomphocarpus (*Gomphocarpus physocarpus*, syn.\\n*Asclepias physocarpa*)**'" {
    const input =
        "**Gomphocarpus (*Gomphocarpus physocarpus*, syn.\n" ++
        "*Asclepias physocarpa*)**\n";
    const expected =
        "<p><strong>Gomphocarpus (<em>Gomphocarpus physocarpus</em>, syn.\n" ++
        "<em>Asclepias physocarpa</em>)</strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 404, line 6989: '**foo \"*bar*\" foo**'" {
    const input =
        "**foo \"*bar*\" foo**\n";
    const expected =
        "<p><strong>foo &quot;<em>bar</em>&quot; foo</strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 405, line 6998: '**foo**bar'" {
    const input =
        "**foo**bar\n";
    const expected =
        "<p><strong>foo</strong>bar</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 406, line 7010: '__foo bar __'" {
    const input =
        "__foo bar __\n";
    const expected =
        "<p>__foo bar __</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 407, line 7020: '__(__foo)'" {
    const input =
        "__(__foo)\n";
    const expected =
        "<p>__(__foo)</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 408, line 7030: '_(__foo__)_'" {
    const input =
        "_(__foo__)_\n";
    const expected =
        "<p><em>(<strong>foo</strong>)</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 409, line 7039: '__foo__bar'" {
    const input =
        "__foo__bar\n";
    const expected =
        "<p>__foo__bar</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 410, line 7046: '__пристаням__стремятся'" {
    const input =
        "__пристаням__стремятся\n";
    const expected =
        "<p>__пристаням__стремятся</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 411, line 7053: '__foo__bar__baz__'" {
    const input =
        "__foo__bar__baz__\n";
    const expected =
        "<p><strong>foo__bar__baz</strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 412, line 7064: '__(bar)__.'" {
    const input =
        "__(bar)__.\n";
    const expected =
        "<p><strong>(bar)</strong>.</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 413, line 7076: '*foo [bar](/url)*'" {
    const input =
        "*foo [bar](/url)*\n";
    const expected =
        "<p><em>foo <a href=\"/url\">bar</a></em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 414, line 7083: '*foo\\nbar*'" {
    const input =
        "*foo\n" ++
        "bar*\n";
    const expected =
        "<p><em>foo\n" ++
        "bar</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 415, line 7095: '_foo __bar__ baz_'" {
    const input =
        "_foo __bar__ baz_\n";
    const expected =
        "<p><em>foo <strong>bar</strong> baz</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 416, line 7102: '_foo _bar_ baz_'" {
    const input =
        "_foo _bar_ baz_\n";
    const expected =
        "<p><em>foo <em>bar</em> baz</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 417, line 7109: '__foo_ bar_'" {
    const input =
        "__foo_ bar_\n";
    const expected =
        "<p><em><em>foo</em> bar</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 418, line 7116: '*foo *bar**'" {
    const input =
        "*foo *bar**\n";
    const expected =
        "<p><em>foo <em>bar</em></em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 419, line 7123: '*foo **bar** baz*'" {
    const input =
        "*foo **bar** baz*\n";
    const expected =
        "<p><em>foo <strong>bar</strong> baz</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 420, line 7129: '*foo**bar**baz*'" {
    const input =
        "*foo**bar**baz*\n";
    const expected =
        "<p><em>foo<strong>bar</strong>baz</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 421, line 7153: '*foo**bar*'" {
    const input =
        "*foo**bar*\n";
    const expected =
        "<p><em>foo**bar</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 422, line 7166: '***foo** bar*'" {
    const input =
        "***foo** bar*\n";
    const expected =
        "<p><em><strong>foo</strong> bar</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 423, line 7173: '*foo **bar***'" {
    const input =
        "*foo **bar***\n";
    const expected =
        "<p><em>foo <strong>bar</strong></em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 424, line 7180: '*foo**bar***'" {
    const input =
        "*foo**bar***\n";
    const expected =
        "<p><em>foo<strong>bar</strong></em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 425, line 7191: 'foo***bar***baz'" {
    const input =
        "foo***bar***baz\n";
    const expected =
        "<p>foo<em><strong>bar</strong></em>baz</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 426, line 7197: 'foo******bar*********baz'" {
    const input =
        "foo******bar*********baz\n";
    const expected =
        "<p>foo<strong><strong><strong>bar</strong></strong></strong>***baz</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 427, line 7206: '*foo **bar *baz* bim** bop*'" {
    const input =
        "*foo **bar *baz* bim** bop*\n";
    const expected =
        "<p><em>foo <strong>bar <em>baz</em> bim</strong> bop</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 428, line 7213: '*foo [*bar*](/url)*'" {
    const input =
        "*foo [*bar*](/url)*\n";
    const expected =
        "<p><em>foo <a href=\"/url\"><em>bar</em></a></em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 429, line 7222: '** is not an empty emphasis'" {
    const input =
        "** is not an empty emphasis\n";
    const expected =
        "<p>** is not an empty emphasis</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 430, line 7229: '**** is not an empty strong emphasis'" {
    const input =
        "**** is not an empty strong emphasis\n";
    const expected =
        "<p>**** is not an empty strong emphasis</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 431, line 7242: '**foo [bar](/url)**'" {
    const input =
        "**foo [bar](/url)**\n";
    const expected =
        "<p><strong>foo <a href=\"/url\">bar</a></strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 432, line 7249: '**foo\\nbar**'" {
    const input =
        "**foo\n" ++
        "bar**\n";
    const expected =
        "<p><strong>foo\n" ++
        "bar</strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 433, line 7261: '__foo _bar_ baz__'" {
    const input =
        "__foo _bar_ baz__\n";
    const expected =
        "<p><strong>foo <em>bar</em> baz</strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 434, line 7268: '__foo __bar__ baz__'" {
    const input =
        "__foo __bar__ baz__\n";
    const expected =
        "<p><strong>foo <strong>bar</strong> baz</strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 435, line 7275: '____foo__ bar__'" {
    const input =
        "____foo__ bar__\n";
    const expected =
        "<p><strong><strong>foo</strong> bar</strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 436, line 7282: '**foo **bar****'" {
    const input =
        "**foo **bar****\n";
    const expected =
        "<p><strong>foo <strong>bar</strong></strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 437, line 7289: '**foo *bar* baz**'" {
    const input =
        "**foo *bar* baz**\n";
    const expected =
        "<p><strong>foo <em>bar</em> baz</strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 438, line 7296: '**foo*bar*baz**'" {
    const input =
        "**foo*bar*baz**\n";
    const expected =
        "<p><strong>foo<em>bar</em>baz</strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 439, line 7303: '***foo* bar**'" {
    const input =
        "***foo* bar**\n";
    const expected =
        "<p><strong><em>foo</em> bar</strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 440, line 7310: '**foo *bar***'" {
    const input =
        "**foo *bar***\n";
    const expected =
        "<p><strong>foo <em>bar</em></strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 441, line 7319: '**foo *bar **baz**\\nbim* bop**'" {
    const input =
        "**foo *bar **baz**\n" ++
        "bim* bop**\n";
    const expected =
        "<p><strong>foo <em>bar <strong>baz</strong>\n" ++
        "bim</em> bop</strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 442, line 7328: '**foo [*bar*](/url)**'" {
    const input =
        "**foo [*bar*](/url)**\n";
    const expected =
        "<p><strong>foo <a href=\"/url\"><em>bar</em></a></strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 443, line 7337: '__ is not an empty emphasis'" {
    const input =
        "__ is not an empty emphasis\n";
    const expected =
        "<p>__ is not an empty emphasis</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 444, line 7344: '____ is not an empty strong emphasis'" {
    const input =
        "____ is not an empty strong emphasis\n";
    const expected =
        "<p>____ is not an empty strong emphasis</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 445, line 7354: 'foo ***'" {
    const input =
        "foo ***\n";
    const expected =
        "<p>foo ***</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 446, line 7361: 'foo *\\**'" {
    const input =
        "foo *\\**\n";
    const expected =
        "<p>foo <em>*</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 447, line 7368: 'foo *_*'" {
    const input =
        "foo *_*\n";
    const expected =
        "<p>foo <em>_</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 448, line 7375: 'foo *****'" {
    const input =
        "foo *****\n";
    const expected =
        "<p>foo *****</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 449, line 7382: 'foo **\\***'" {
    const input =
        "foo **\\***\n";
    const expected =
        "<p>foo <strong>*</strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 450, line 7389: 'foo **_**'" {
    const input =
        "foo **_**\n";
    const expected =
        "<p>foo <strong>_</strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 451, line 7400: '**foo*'" {
    const input =
        "**foo*\n";
    const expected =
        "<p>*<em>foo</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 452, line 7407: '*foo**'" {
    const input =
        "*foo**\n";
    const expected =
        "<p><em>foo</em>*</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 453, line 7414: '***foo**'" {
    const input =
        "***foo**\n";
    const expected =
        "<p>*<strong>foo</strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 454, line 7421: '****foo*'" {
    const input =
        "****foo*\n";
    const expected =
        "<p>***<em>foo</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 455, line 7428: '**foo***'" {
    const input =
        "**foo***\n";
    const expected =
        "<p><strong>foo</strong>*</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 456, line 7435: '*foo****'" {
    const input =
        "*foo****\n";
    const expected =
        "<p><em>foo</em>***</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 457, line 7445: 'foo ___'" {
    const input =
        "foo ___\n";
    const expected =
        "<p>foo ___</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 458, line 7452: 'foo _\\__'" {
    const input =
        "foo _\\__\n";
    const expected =
        "<p>foo <em>_</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 459, line 7459: 'foo _*_'" {
    const input =
        "foo _*_\n";
    const expected =
        "<p>foo <em>*</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 460, line 7466: 'foo _____'" {
    const input =
        "foo _____\n";
    const expected =
        "<p>foo _____</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 461, line 7473: 'foo __\\___'" {
    const input =
        "foo __\\___\n";
    const expected =
        "<p>foo <strong>_</strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 462, line 7480: 'foo __*__'" {
    const input =
        "foo __*__\n";
    const expected =
        "<p>foo <strong>*</strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 463, line 7487: '__foo_'" {
    const input =
        "__foo_\n";
    const expected =
        "<p>_<em>foo</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 464, line 7498: '_foo__'" {
    const input =
        "_foo__\n";
    const expected =
        "<p><em>foo</em>_</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 465, line 7505: '___foo__'" {
    const input =
        "___foo__\n";
    const expected =
        "<p>_<strong>foo</strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 466, line 7512: '____foo_'" {
    const input =
        "____foo_\n";
    const expected =
        "<p>___<em>foo</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 467, line 7519: '__foo___'" {
    const input =
        "__foo___\n";
    const expected =
        "<p><strong>foo</strong>_</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 468, line 7526: '_foo____'" {
    const input =
        "_foo____\n";
    const expected =
        "<p><em>foo</em>___</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 469, line 7536: '**foo**'" {
    const input =
        "**foo**\n";
    const expected =
        "<p><strong>foo</strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 470, line 7543: '*_foo_*'" {
    const input =
        "*_foo_*\n";
    const expected =
        "<p><em><em>foo</em></em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 471, line 7550: '__foo__'" {
    const input =
        "__foo__\n";
    const expected =
        "<p><strong>foo</strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 472, line 7557: '_*foo*_'" {
    const input =
        "_*foo*_\n";
    const expected =
        "<p><em><em>foo</em></em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 473, line 7567: '****foo****'" {
    const input =
        "****foo****\n";
    const expected =
        "<p><strong><strong>foo</strong></strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 474, line 7574: '____foo____'" {
    const input =
        "____foo____\n";
    const expected =
        "<p><strong><strong>foo</strong></strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 475, line 7585: '******foo******'" {
    const input =
        "******foo******\n";
    const expected =
        "<p><strong><strong><strong>foo</strong></strong></strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 476, line 7594: '***foo***'" {
    const input =
        "***foo***\n";
    const expected =
        "<p><em><strong>foo</strong></em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 477, line 7601: '_____foo_____'" {
    const input =
        "_____foo_____\n";
    const expected =
        "<p><em><strong><strong>foo</strong></strong></em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 478, line 7610: '*foo _bar* baz_'" {
    const input =
        "*foo _bar* baz_\n";
    const expected =
        "<p><em>foo _bar</em> baz_</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 479, line 7617: '*foo __bar *baz bim__ bam*'" {
    const input =
        "*foo __bar *baz bim__ bam*\n";
    const expected =
        "<p><em>foo <strong>bar *baz bim</strong> bam</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 480, line 7626: '**foo **bar baz**'" {
    const input =
        "**foo **bar baz**\n";
    const expected =
        "<p>**foo <strong>bar baz</strong></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 481, line 7633: '*foo *bar baz*'" {
    const input =
        "*foo *bar baz*\n";
    const expected =
        "<p>*foo <em>bar baz</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 482, line 7642: '*[bar*](/url)'" {
    const input =
        "*[bar*](/url)\n";
    const expected =
        "<p>*<a href=\"/url\">bar*</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 483, line 7649: '_foo [bar_](/url)'" {
    const input =
        "_foo [bar_](/url)\n";
    const expected =
        "<p>_foo <a href=\"/url\">bar_</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 484, line 7656: '*<img src=\"foo\" title=\"*\"/>'" {
    const input =
        "*<img src=\"foo\" title=\"*\"/>\n";
    const expected =
        "<p>*<img src=\"foo\" title=\"*\"/></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 485, line 7663: '**<a href=\"**\">'" {
    const input =
        "**<a href=\"**\">\n";
    const expected =
        "<p>**<a href=\"**\"></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 486, line 7670: '__<a href=\"__\">'" {
    const input =
        "__<a href=\"__\">\n";
    const expected =
        "<p>__<a href=\"__\"></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 487, line 7677: '*a `*`*'" {
    const input =
        "*a `*`*\n";
    const expected =
        "<p><em>a <code>*</code></em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 488, line 7684: '_a `_`_'" {
    const input =
        "_a `_`_\n";
    const expected =
        "<p><em>a <code>_</code></em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 489, line 7691: '**a<http://foo.bar/?q=**>'" {
    const input =
        "**a<http://foo.bar/?q=**>\n";
    const expected =
        "<p>**a<a href=\"http://foo.bar/?q=**\">http://foo.bar/?q=**</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 490, line 7698: '__a<http://foo.bar/?q=__>'" {
    const input =
        "__a<http://foo.bar/?q=__>\n";
    const expected =
        "<p>__a<a href=\"http://foo.bar/?q=__\">http://foo.bar/?q=__</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 491, line 7714: '~~Hi~~ Hello, world!'" {
    const input =
        "~~Hi~~ Hello, world!\n";
    const expected =
        "<p><del>Hi</del> Hello, world!</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 492, line 7723: 'This ~~has a\\n\\nnew paragraph~~.'" {
    const input =
        "This ~~has a\n" ++
        "\n" ++
        "new paragraph~~.\n";
    const expected =
        "<p>This ~~has a</p>\n" ++
        "<p>new paragraph~~.</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 493, line 7734: 'This will ~~~not~~~ strike.'" {
    const input =
        "This will ~~~not~~~ strike.\n";
    const expected =
        "<p>This will ~~~not~~~ strike.</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 494, line 7817: '[link](/uri \"title\")'" {
    const input =
        "[link](/uri \"title\")\n";
    const expected =
        "<p><a href=\"/uri\" title=\"title\">link</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 495, line 7826: '[link](/uri)'" {
    const input =
        "[link](/uri)\n";
    const expected =
        "<p><a href=\"/uri\">link</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 496, line 7835: '[link]()'" {
    const input =
        "[link]()\n";
    const expected =
        "<p><a href=\"\">link</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 497, line 7842: '[link](<>)'" {
    const input =
        "[link](<>)\n";
    const expected =
        "<p><a href=\"\">link</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 498, line 7851: '[link](/my uri)'" {
    const input =
        "[link](/my uri)\n";
    const expected =
        "<p>[link](/my uri)</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 499, line 7857: '[link](</my uri>)'" {
    const input =
        "[link](</my uri>)\n";
    const expected =
        "<p><a href=\"/my%20uri\">link</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 500, line 7866: '[link](foo\\nbar)'" {
    const input =
        "[link](foo\n" ++
        "bar)\n";
    const expected =
        "<p>[link](foo\n" ++
        "bar)</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 501, line 7874: '[link](<foo\\nbar>)'" {
    const input =
        "[link](<foo\n" ++
        "bar>)\n";
    const expected =
        "<p>[link](<foo\n" ++
        "bar>)</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 502, line 7885: '[a](<b)c>)'" {
    const input =
        "[a](<b)c>)\n";
    const expected =
        "<p><a href=\"b)c\">a</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 503, line 7893: '[link](<foo\\>)'" {
    const input =
        "[link](<foo\\>)\n";
    const expected =
        "<p>[link](&lt;foo&gt;)</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 504, line 7902: '[a](<b)c\\n[a](<b)c>\\n[a](<b>c)'" {
    const input =
        "[a](<b)c\n" ++
        "[a](<b)c>\n" ++
        "[a](<b>c)\n";
    const expected =
        "<p>[a](&lt;b)c\n" ++
        "[a](&lt;b)c&gt;\n" ++
        "[a](<b>c)</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 505, line 7914: '[link](\\(foo\\))'" {
    const input =
        "[link](\\(foo\\))\n";
    const expected =
        "<p><a href=\"(foo)\">link</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 506, line 7923: '[link](foo(and(bar)))'" {
    const input =
        "[link](foo(and(bar)))\n";
    const expected =
        "<p><a href=\"foo(and(bar))\">link</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 507, line 7932: '[link](foo\\(and\\(bar\\))'" {
    const input =
        "[link](foo\\(and\\(bar\\))\n";
    const expected =
        "<p><a href=\"foo(and(bar)\">link</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 508, line 7939: '[link](<foo(and(bar)>)'" {
    const input =
        "[link](<foo(and(bar)>)\n";
    const expected =
        "<p><a href=\"foo(and(bar)\">link</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 509, line 7949: '[link](foo\\)\\:)'" {
    const input =
        "[link](foo\\)\\:)\n";
    const expected =
        "<p><a href=\"foo):\">link</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 510, line 7958: '[link](#fragment)\\n\\n[link](http://example.com#fragment)\\n\\n[link](http://example.com?foo=3#frag)'" {
    const input =
        "[link](#fragment)\n" ++
        "\n" ++
        "[link](http://example.com#fragment)\n" ++
        "\n" ++
        "[link](http://example.com?foo=3#frag)\n";
    const expected =
        "<p><a href=\"#fragment\">link</a></p>\n" ++
        "<p><a href=\"http://example.com#fragment\">link</a></p>\n" ++
        "<p><a href=\"http://example.com?foo=3#frag\">link</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 511, line 7974: '[link](foo\\bar)'" {
    const input =
        "[link](foo\\bar)\n";
    const expected =
        "<p><a href=\"foo%5Cbar\">link</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 512, line 7990: '[link](foo%20b&auml;)'" {
    const input =
        "[link](foo%20b&auml;)\n";
    const expected =
        "<p><a href=\"foo%20b%C3%A4\">link</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 513, line 8001: '[link](\"title\")'" {
    const input =
        "[link](\"title\")\n";
    const expected =
        "<p><a href=\"%22title%22\">link</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 514, line 8010: '[link](/url \"title\")\\n[link](/url 'title')\\n[link](/url (title))'" {
    const input =
        "[link](/url \"title\")\n" ++
        "[link](/url 'title')\n" ++
        "[link](/url (title))\n";
    const expected =
        "<p><a href=\"/url\" title=\"title\">link</a>\n" ++
        "<a href=\"/url\" title=\"title\">link</a>\n" ++
        "<a href=\"/url\" title=\"title\">link</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 515, line 8024: '[link](/url \"title \\\"&quot;\")'" {
    const input =
        "[link](/url \"title \\\"&quot;\")\n";
    const expected =
        "<p><a href=\"/url\" title=\"title &quot;&quot;\">link</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 516, line 8034: '[link](/url \"title\")'" {
    const input =
        "[link](/url \"title\")\n";
    const expected =
        "<p><a href=\"/url%C2%A0%22title%22\">link</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 517, line 8043: '[link](/url \"title \"and\" title\")'" {
    const input =
        "[link](/url \"title \"and\" title\")\n";
    const expected =
        "<p>[link](/url &quot;title &quot;and&quot; title&quot;)</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 518, line 8052: '[link](/url 'title \"and\" title')'" {
    const input =
        "[link](/url 'title \"and\" title')\n";
    const expected =
        "<p><a href=\"/url\" title=\"title &quot;and&quot; title\">link</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 519, line 8076: '[link](   /uri\\n  \"title\"  )'" {
    const input =
        "[link](   /uri\n" ++
        "  \"title\"  )\n";
    const expected =
        "<p><a href=\"/uri\" title=\"title\">link</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 520, line 8087: '[link] (/uri)'" {
    const input =
        "[link] (/uri)\n";
    const expected =
        "<p>[link] (/uri)</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 521, line 8097: '[link [foo [bar]]](/uri)'" {
    const input =
        "[link [foo [bar]]](/uri)\n";
    const expected =
        "<p><a href=\"/uri\">link [foo [bar]]</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 522, line 8104: '[link] bar](/uri)'" {
    const input =
        "[link] bar](/uri)\n";
    const expected =
        "<p>[link] bar](/uri)</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 523, line 8111: '[link [bar](/uri)'" {
    const input =
        "[link [bar](/uri)\n";
    const expected =
        "<p>[link <a href=\"/uri\">bar</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 524, line 8118: '[link \\[bar](/uri)'" {
    const input =
        "[link \\[bar](/uri)\n";
    const expected =
        "<p><a href=\"/uri\">link [bar</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 525, line 8127: '[link *foo **bar** `#`*](/uri)'" {
    const input =
        "[link *foo **bar** `#`*](/uri)\n";
    const expected =
        "<p><a href=\"/uri\">link <em>foo <strong>bar</strong> <code>#</code></em></a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 526, line 8134: '[![moon](moon.jpg)](/uri)'" {
    const input =
        "[![moon](moon.jpg)](/uri)\n";
    const expected =
        "<p><a href=\"/uri\"><img src=\"moon.jpg\" alt=\"moon\" /></a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 527, line 8143: '[foo [bar](/uri)](/uri)'" {
    const input =
        "[foo [bar](/uri)](/uri)\n";
    const expected =
        "<p>[foo <a href=\"/uri\">bar</a>](/uri)</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 528, line 8150: '[foo *[bar [baz](/uri)](/uri)*](/uri)'" {
    const input =
        "[foo *[bar [baz](/uri)](/uri)*](/uri)\n";
    const expected =
        "<p>[foo <em>[bar <a href=\"/uri\">baz</a>](/uri)</em>](/uri)</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 529, line 8157: '![[[foo](uri1)](uri2)](uri3)'" {
    const input =
        "![[[foo](uri1)](uri2)](uri3)\n";
    const expected =
        "<p><img src=\"uri3\" alt=\"[foo](uri2)\" /></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 530, line 8167: '*[foo*](/uri)'" {
    const input =
        "*[foo*](/uri)\n";
    const expected =
        "<p>*<a href=\"/uri\">foo*</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 531, line 8174: '[foo *bar](baz*)'" {
    const input =
        "[foo *bar](baz*)\n";
    const expected =
        "<p><a href=\"baz*\">foo *bar</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

// TODO:
//test "Example 532, line 8184: '*foo [bar* baz]'" {
//    const input =
//        "*foo [bar* baz]\n";
//    const expected =
//        "<p><em>foo [bar</em> baz]</p>\n";
//
//    const gpa = std.testing.allocator;
//    var rules = try gfm.init(gpa);
//    defer rules.blocks.deinit();
//    defer rules.inlines.deinit();
////
//    const root = try parse.execute(gpa, input, rules, null);
//    defer root.deinit(gpa);
//
//    const html = try render.renderHtml(gpa, root, null);
//    defer gpa.free(html);
//
//    try std.testing.expectEqualStrings(expected, html);
//}

test "Example 533, line 8194: '[foo <bar attr=\"](baz)\">'" {
    const input =
        "[foo <bar attr=\"](baz)\">\n";
    const expected =
        "<p>[foo <bar attr=\"](baz)\"></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 534, line 8201: '[foo`](/uri)`'" {
    const input =
        "[foo`](/uri)`\n";
    const expected =
        "<p>[foo<code>](/uri)</code></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 535, line 8208: '[foo<http://example.com/?search=](uri)>'" {
    const input =
        "[foo<http://example.com/?search=](uri)>\n";
    const expected =
        "<p>[foo<a href=\"http://example.com/?search=%5D(uri)\">http://example.com/?search=](uri)</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 536, line 8246: '[foo][bar]\\n\\n[bar]: /url \"title\"'" {
    const input =
        "[foo][bar]\n" ++
        "\n" ++
        "[bar]: /url \"title\"\n";
    const expected =
        "<p><a href=\"/url\" title=\"title\">foo</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 537, line 8261: '[link [foo [bar]]][ref]\\n\\n[ref]: /uri'" {
    const input =
        "[link [foo [bar]]][ref]\n" ++
        "\n" ++
        "[ref]: /uri\n";
    const expected =
        "<p><a href=\"/uri\">link [foo [bar]]</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 538, line 8270: '[link \\[bar][ref]\\n\\n[ref]: /uri'" {
    const input =
        "[link \\[bar][ref]\n" ++
        "\n" ++
        "[ref]: /uri\n";
    const expected =
        "<p><a href=\"/uri\">link [bar</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 539, line 8281: '[link *foo **bar** `#`*][ref]\\n\\n[ref]: /uri'" {
    const input =
        "[link *foo **bar** `#`*][ref]\n" ++
        "\n" ++
        "[ref]: /uri\n";
    const expected =
        "<p><a href=\"/uri\">link <em>foo <strong>bar</strong> <code>#</code></em></a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 540, line 8290: '[![moon](moon.jpg)][ref]\\n\\n[ref]: /uri'" {
    const input =
        "[![moon](moon.jpg)][ref]\n" ++
        "\n" ++
        "[ref]: /uri\n";
    const expected =
        "<p><a href=\"/uri\"><img src=\"moon.jpg\" alt=\"moon\" /></a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 541, line 8301: '[foo [bar](/uri)][ref]\\n\\n[ref]: /uri'" {
    const input =
        "[foo [bar](/uri)][ref]\n" ++
        "\n" ++
        "[ref]: /uri\n";
    const expected =
        "<p>[foo <a href=\"/uri\">bar</a>]<a href=\"/uri\">ref</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 542, line 8310: '[foo *bar [baz][ref]*][ref]\\n\\n[ref]: /uri'" {
    const input =
        "[foo *bar [baz][ref]*][ref]\n" ++
        "\n" ++
        "[ref]: /uri\n";
    const expected =
        "<p>[foo <em>bar <a href=\"/uri\">baz</a></em>]<a href=\"/uri\">ref</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 543, line 8325: '*[foo*][ref]\\n\\n[ref]: /uri'" {
    const input =
        "*[foo*][ref]\n" ++
        "\n" ++
        "[ref]: /uri\n";
    const expected =
        "<p>*<a href=\"/uri\">foo*</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 544, line 8334: '[foo *bar][ref]\\n\\n[ref]: /uri'" {
    const input =
        "[foo *bar][ref]\n" ++
        "\n" ++
        "[ref]: /uri\n";
    const expected =
        "<p><a href=\"/uri\">foo *bar</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 545, line 8346: '[foo <bar attr=\"][ref]\">\\n\\n[ref]: /uri'" {
    const input =
        "[foo <bar attr=\"][ref]\">\n" ++
        "\n" ++
        "[ref]: /uri\n";
    const expected =
        "<p>[foo <bar attr=\"][ref]\"></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 546, line 8355: '[foo`][ref]`\\n\\n[ref]: /uri'" {
    const input =
        "[foo`][ref]`\n" ++
        "\n" ++
        "[ref]: /uri\n";
    const expected =
        "<p>[foo<code>][ref]</code></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 547, line 8364: '[foo<http://example.com/?search=][ref]>\\n\\n[ref]: /uri'" {
    const input =
        "[foo<http://example.com/?search=][ref]>\n" ++
        "\n" ++
        "[ref]: /uri\n";
    const expected =
        "<p>[foo<a href=\"http://example.com/?search=%5D%5Bref%5D\">http://example.com/?search=][ref]</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 548, line 8375: '[foo][BaR]\\n\\n[bar]: /url \"title\"'" {
    const input =
        "[foo][BaR]\n" ++
        "\n" ++
        "[bar]: /url \"title\"\n";
    const expected =
        "<p><a href=\"/url\" title=\"title\">foo</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

// TODO:
//test "Example 549, line 8386: '[Толпой][Толпой] is a Russian word.\\n\\n[ТОЛПОЙ]: /url'" {
//    const input =
//        "[Толпой][Толпой] is a Russian word.\n" ++
//        "\n" ++
//        "[ТОЛПОЙ]: /url\n";
//    const expected =
//        "<p><a href=\"/url\">Толпой</a> is a Russian word.</p>\n";
//
//    const gpa = std.testing.allocator;
//    var rules = try gfm.init(gpa);
//    defer rules.blocks.deinit();
//    defer rules.inlines.deinit();
////
//    const root = try parse.execute(gpa, input, rules, null);
//    defer root.deinit(gpa);
//
//    const html = try render.renderHtml(gpa, root, null);
//    defer gpa.free(html);
//
//    try std.testing.expectEqualStrings(expected, html);
//}

test "Example 550, line 8398: '[Foo\\n  bar]: /url\\n\\n[Baz][Foo bar]'" {
    const input =
        "[Foo\n" ++
        "  bar]: /url\n" ++
        "\n" ++
        "[Baz][Foo bar]\n";
    const expected =
        "<p><a href=\"/url\">Baz</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 551, line 8411: '[foo] [bar]\\n\\n[bar]: /url \"title\"'" {
    const input =
        "[foo] [bar]\n" ++
        "\n" ++
        "[bar]: /url \"title\"\n";
    const expected =
        "<p>[foo] <a href=\"/url\" title=\"title\">bar</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 552, line 8420: '[foo]\\n[bar]\\n\\n[bar]: /url \"title\"'" {
    const input =
        "[foo]\n" ++
        "[bar]\n" ++
        "\n" ++
        "[bar]: /url \"title\"\n";
    const expected =
        "<p>[foo]\n" ++
        "<a href=\"/url\" title=\"title\">bar</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 553, line 8461: '[foo]: /url1\\n\\n[foo]: /url2\\n\\n[bar][foo]'" {
    const input =
        "[foo]: /url1\n" ++
        "\n" ++
        "[foo]: /url2\n" ++
        "\n" ++
        "[bar][foo]\n";
    const expected =
        "<p><a href=\"/url1\">bar</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 554, line 8476: '[bar][foo\\!]\\n\\n[foo!]: /url'" {
    const input =
        "[bar][foo\\!]\n" ++
        "\n" ++
        "[foo!]: /url\n";
    const expected =
        "<p>[bar][foo!]</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 555, line 8488: '[foo][ref[]\\n\\n[ref[]: /uri'" {
    const input =
        "[foo][ref[]\n" ++
        "\n" ++
        "[ref[]: /uri\n";
    const expected =
        "<p>[foo][ref[]</p>\n" ++
        "<p>[ref[]: /uri</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 556, line 8498: '[foo][ref[bar]]\\n\\n[ref[bar]]: /uri'" {
    const input =
        "[foo][ref[bar]]\n" ++
        "\n" ++
        "[ref[bar]]: /uri\n";
    const expected =
        "<p>[foo][ref[bar]]</p>\n" ++
        "<p>[ref[bar]]: /uri</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 557, line 8508: '[[[foo]]]\\n\\n[[[foo]]]: /url'" {
    const input =
        "[[[foo]]]\n" ++
        "\n" ++
        "[[[foo]]]: /url\n";
    const expected =
        "<p>[[[foo]]]</p>\n" ++
        "<p>[[[foo]]]: /url</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 558, line 8518: '[foo][ref\\[]\\n\\n[ref\\[]: /uri'" {
    const input =
        "[foo][ref\\[]\n" ++
        "\n" ++
        "[ref\\[]: /uri\n";
    const expected =
        "<p><a href=\"/uri\">foo</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 559, line 8529: '[bar\\\\]: /uri\\n\\n[bar\\\\]'" {
    const input =
        "[bar\\\\]: /uri\n" ++
        "\n" ++
        "[bar\\\\]\n";
    const expected =
        "<p><a href=\"/uri\">bar\\</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 560, line 8540: '[]\\n\\n[]: /uri'" {
    const input =
        "[]\n" ++
        "\n" ++
        "[]: /uri\n";
    const expected =
        "<p>[]</p>\n" ++
        "<p>[]: /uri</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 561, line 8550: '[\\n ]\\n\\n[\\n ]: /uri'" {
    const input =
        "[\n" ++
        " ]\n" ++
        "\n" ++
        "[\n" ++
        " ]: /uri\n";
    const expected =
        "<p>[\n" ++
        "]</p>\n" ++
        "<p>[\n" ++
        "]: /uri</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 562, line 8573: '[foo][]\\n\\n[foo]: /url \"title\"'" {
    const input =
        "[foo][]\n" ++
        "\n" ++
        "[foo]: /url \"title\"\n";
    const expected =
        "<p><a href=\"/url\" title=\"title\">foo</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 563, line 8582: '[*foo* bar][]\\n\\n[*foo* bar]: /url \"title\"'" {
    const input =
        "[*foo* bar][]\n" ++
        "\n" ++
        "[*foo* bar]: /url \"title\"\n";
    const expected =
        "<p><a href=\"/url\" title=\"title\"><em>foo</em> bar</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 564, line 8593: '[Foo][]\\n\\n[foo]: /url \"title\"'" {
    const input =
        "[Foo][]\n" ++
        "\n" ++
        "[foo]: /url \"title\"\n";
    const expected =
        "<p><a href=\"/url\" title=\"title\">Foo</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 565, line 8606: '[foo] \\n[]\\n\\n[foo]: /url \"title\"'" {
    const input =
        "[foo] \n" ++
        "[]\n" ++
        "\n" ++
        "[foo]: /url \"title\"\n";
    const expected =
        "<p><a href=\"/url\" title=\"title\">foo</a>\n" ++
        "[]</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 566, line 8626: '[foo]\\n\\n[foo]: /url \"title\"'" {
    const input =
        "[foo]\n" ++
        "\n" ++
        "[foo]: /url \"title\"\n";
    const expected =
        "<p><a href=\"/url\" title=\"title\">foo</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 567, line 8635: '[*foo* bar]\\n\\n[*foo* bar]: /url \"title\"'" {
    const input =
        "[*foo* bar]\n" ++
        "\n" ++
        "[*foo* bar]: /url \"title\"\n";
    const expected =
        "<p><a href=\"/url\" title=\"title\"><em>foo</em> bar</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 568, line 8644: '[[*foo* bar]]\\n\\n[*foo* bar]: /url \"title\"'" {
    const input =
        "[[*foo* bar]]\n" ++
        "\n" ++
        "[*foo* bar]: /url \"title\"\n";
    const expected =
        "<p>[<a href=\"/url\" title=\"title\"><em>foo</em> bar</a>]</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 569, line 8653: '[[bar [foo]\\n\\n[foo]: /url'" {
    const input =
        "[[bar [foo]\n" ++
        "\n" ++
        "[foo]: /url\n";
    const expected =
        "<p>[[bar <a href=\"/url\">foo</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 570, line 8664: '[Foo]\\n\\n[foo]: /url \"title\"'" {
    const input =
        "[Foo]\n" ++
        "\n" ++
        "[foo]: /url \"title\"\n";
    const expected =
        "<p><a href=\"/url\" title=\"title\">Foo</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 571, line 8675: '[foo] bar\\n\\n[foo]: /url'" {
    const input =
        "[foo] bar\n" ++
        "\n" ++
        "[foo]: /url\n";
    const expected =
        "<p><a href=\"/url\">foo</a> bar</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 572, line 8687: '\\[foo]\\n\\n[foo]: /url \"title\"'" {
    const input =
        "\\[foo]\n" ++
        "\n" ++
        "[foo]: /url \"title\"\n";
    const expected =
        "<p>[foo]</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 573, line 8699: '[foo*]: /url\\n\\n*[foo*]'" {
    const input =
        "[foo*]: /url\n" ++
        "\n" ++
        "*[foo*]\n";
    const expected =
        "<p>*<a href=\"/url\">foo*</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 574, line 8711: '[foo][bar]\\n\\n[foo]: /url1\\n[bar]: /url2'" {
    const input =
        "[foo][bar]\n" ++
        "\n" ++
        "[foo]: /url1\n" ++
        "[bar]: /url2\n";
    const expected =
        "<p><a href=\"/url2\">foo</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 575, line 8720: '[foo][]\\n\\n[foo]: /url1'" {
    const input =
        "[foo][]\n" ++
        "\n" ++
        "[foo]: /url1\n";
    const expected =
        "<p><a href=\"/url1\">foo</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 576, line 8730: '[foo]()\\n\\n[foo]: /url1'" {
    const input =
        "[foo]()\n" ++
        "\n" ++
        "[foo]: /url1\n";
    const expected =
        "<p><a href=\"\">foo</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 577, line 8738: '[foo](not a link)\\n\\n[foo]: /url1'" {
    const input =
        "[foo](not a link)\n" ++
        "\n" ++
        "[foo]: /url1\n";
    const expected =
        "<p><a href=\"/url1\">foo</a>(not a link)</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 578, line 8749: '[foo][bar][baz]\\n\\n[baz]: /url'" {
    const input =
        "[foo][bar][baz]\n" ++
        "\n" ++
        "[baz]: /url\n";
    const expected =
        "<p>[foo]<a href=\"/url\">bar</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 579, line 8761: '[foo][bar][baz]\\n\\n[baz]: /url1\\n[bar]: /url2'" {
    const input =
        "[foo][bar][baz]\n" ++
        "\n" ++
        "[baz]: /url1\n" ++
        "[bar]: /url2\n";
    const expected =
        "<p><a href=\"/url2\">foo</a><a href=\"/url1\">baz</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 580, line 8774: '[foo][bar][baz]\\n\\n[baz]: /url1\\n[foo]: /url2'" {
    const input =
        "[foo][bar][baz]\n" ++
        "\n" ++
        "[baz]: /url1\n" ++
        "[foo]: /url2\n";
    const expected =
        "<p>[foo]<a href=\"/url1\">bar</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 581, line 8797: '![foo](/url \"title\")'" {
    const input =
        "![foo](/url \"title\")\n";
    const expected =
        "<p><img src=\"/url\" alt=\"foo\" title=\"title\" /></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 582, line 8804: '![foo *bar*]\\n\\n[foo *bar*]: train.jpg \"train & tracks\"'" {
    const input =
        "![foo *bar*]\n" ++
        "\n" ++
        "[foo *bar*]: train.jpg \"train & tracks\"\n";
    const expected =
        "<p><img src=\"train.jpg\" alt=\"foo bar\" title=\"train &amp; tracks\" /></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 583, line 8813: '![foo ![bar](/url)](/url2)'" {
    const input =
        "![foo ![bar](/url)](/url2)\n";
    const expected =
        "<p><img src=\"/url2\" alt=\"foo bar\" /></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 584, line 8820: '![foo [bar](/url)](/url2)'" {
    const input =
        "![foo [bar](/url)](/url2)\n";
    const expected =
        "<p><img src=\"/url2\" alt=\"foo bar\" /></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 585, line 8834: '![foo *bar*][]\\n\\n[foo *bar*]: train.jpg \"train & tracks\"'" {
    const input =
        "![foo *bar*][]\n" ++
        "\n" ++
        "[foo *bar*]: train.jpg \"train & tracks\"\n";
    const expected =
        "<p><img src=\"train.jpg\" alt=\"foo bar\" title=\"train &amp; tracks\" /></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 586, line 8843: '![foo *bar*][foobar]\\n\\n[FOOBAR]: train.jpg \"train & tracks\"'" {
    const input =
        "![foo *bar*][foobar]\n" ++
        "\n" ++
        "[FOOBAR]: train.jpg \"train & tracks\"\n";
    const expected =
        "<p><img src=\"train.jpg\" alt=\"foo bar\" title=\"train &amp; tracks\" /></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 587, line 8852: '![foo](train.jpg)'" {
    const input =
        "![foo](train.jpg)\n";
    const expected =
        "<p><img src=\"train.jpg\" alt=\"foo\" /></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 588, line 8859: 'My ![foo bar](/path/to/train.jpg  \"title\"   )'" {
    const input =
        "My ![foo bar](/path/to/train.jpg  \"title\"   )\n";
    const expected =
        "<p>My <img src=\"/path/to/train.jpg\" alt=\"foo bar\" title=\"title\" /></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 589, line 8866: '![foo](<url>)'" {
    const input =
        "![foo](<url>)\n";
    const expected =
        "<p><img src=\"url\" alt=\"foo\" /></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 590, line 8873: '![](/url)'" {
    const input =
        "![](/url)\n";
    const expected =
        "<p><img src=\"/url\" alt=\"\" /></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 591, line 8882: '![foo][bar]\\n\\n[bar]: /url'" {
    const input =
        "![foo][bar]\n" ++
        "\n" ++
        "[bar]: /url\n";
    const expected =
        "<p><img src=\"/url\" alt=\"foo\" /></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 592, line 8891: '![foo][bar]\\n\\n[BAR]: /url'" {
    const input =
        "![foo][bar]\n" ++
        "\n" ++
        "[BAR]: /url\n";
    const expected =
        "<p><img src=\"/url\" alt=\"foo\" /></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 593, line 8902: '![foo][]\\n\\n[foo]: /url \"title\"'" {
    const input =
        "![foo][]\n" ++
        "\n" ++
        "[foo]: /url \"title\"\n";
    const expected =
        "<p><img src=\"/url\" alt=\"foo\" title=\"title\" /></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 594, line 8911: '![*foo* bar][]\\n\\n[*foo* bar]: /url \"title\"'" {
    const input =
        "![*foo* bar][]\n" ++
        "\n" ++
        "[*foo* bar]: /url \"title\"\n";
    const expected =
        "<p><img src=\"/url\" alt=\"foo bar\" title=\"title\" /></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 595, line 8922: '![Foo][]\\n\\n[foo]: /url \"title\"'" {
    const input =
        "![Foo][]\n" ++
        "\n" ++
        "[foo]: /url \"title\"\n";
    const expected =
        "<p><img src=\"/url\" alt=\"Foo\" title=\"title\" /></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 596, line 8934: '![foo] \\n[]\\n\\n[foo]: /url \"title\"'" {
    const input =
        "![foo] \n" ++
        "[]\n" ++
        "\n" ++
        "[foo]: /url \"title\"\n";
    const expected =
        "<p><img src=\"/url\" alt=\"foo\" title=\"title\" />\n" ++
        "[]</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 597, line 8947: '![foo]\\n\\n[foo]: /url \"title\"'" {
    const input =
        "![foo]\n" ++
        "\n" ++
        "[foo]: /url \"title\"\n";
    const expected =
        "<p><img src=\"/url\" alt=\"foo\" title=\"title\" /></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 598, line 8956: '![*foo* bar]\\n\\n[*foo* bar]: /url \"title\"'" {
    const input =
        "![*foo* bar]\n" ++
        "\n" ++
        "[*foo* bar]: /url \"title\"\n";
    const expected =
        "<p><img src=\"/url\" alt=\"foo bar\" title=\"title\" /></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 599, line 8967: '![[foo]]\\n\\n[[foo]]: /url \"title\"'" {
    const input =
        "![[foo]]\n" ++
        "\n" ++
        "[[foo]]: /url \"title\"\n";
    const expected =
        "<p>![[foo]]</p>\n" ++
        "<p>[[foo]]: /url &quot;title&quot;</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 600, line 8979: '![Foo]\\n\\n[foo]: /url \"title\"'" {
    const input =
        "![Foo]\n" ++
        "\n" ++
        "[foo]: /url \"title\"\n";
    const expected =
        "<p><img src=\"/url\" alt=\"Foo\" title=\"title\" /></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 601, line 8991: '!\\[foo]\\n\\n[foo]: /url \"title\"'" {
    const input =
        "!\\[foo]\n" ++
        "\n" ++
        "[foo]: /url \"title\"\n";
    const expected =
        "<p>![foo]</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 602, line 9003: '\\![foo]\\n\\n[foo]: /url \"title\"'" {
    const input =
        "\\![foo]\n" ++
        "\n" ++
        "[foo]: /url \"title\"\n";
    const expected =
        "<p>!<a href=\"/url\" title=\"title\">foo</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 603, line 9036: '<http://foo.bar.baz>'" {
    const input =
        "<http://foo.bar.baz>\n";
    const expected =
        "<p><a href=\"http://foo.bar.baz\">http://foo.bar.baz</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 604, line 9043: '<http://foo.bar.baz/test?q=hello&id=22&boolean>'" {
    const input =
        "<http://foo.bar.baz/test?q=hello&id=22&boolean>\n";
    const expected =
        "<p><a href=\"http://foo.bar.baz/test?q=hello&amp;id=22&amp;boolean\">http://foo.bar.baz/test?q=hello&amp;id=22&amp;boolean</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 605, line 9050: '<irc://foo.bar:2233/baz>'" {
    const input =
        "<irc://foo.bar:2233/baz>\n";
    const expected =
        "<p><a href=\"irc://foo.bar:2233/baz\">irc://foo.bar:2233/baz</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 606, line 9059: '<MAILTO:FOO@BAR.BAZ>'" {
    const input =
        "<MAILTO:FOO@BAR.BAZ>\n";
    const expected =
        "<p><a href=\"MAILTO:FOO@BAR.BAZ\">MAILTO:FOO@BAR.BAZ</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 607, line 9071: '<a+b+c:d>'" {
    const input =
        "<a+b+c:d>\n";
    const expected =
        "<p><a href=\"a+b+c:d\">a+b+c:d</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 608, line 9078: '<made-up-scheme://foo,bar>'" {
    const input =
        "<made-up-scheme://foo,bar>\n";
    const expected =
        "<p><a href=\"made-up-scheme://foo,bar\">made-up-scheme://foo,bar</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 609, line 9085: '<http://../>'" {
    const input =
        "<http://../>\n";
    const expected =
        "<p><a href=\"http://../\">http://../</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 610, line 9092: '<localhost:5001/foo>'" {
    const input =
        "<localhost:5001/foo>\n";
    const expected =
        "<p><a href=\"localhost:5001/foo\">localhost:5001/foo</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 611, line 9101: '<http://foo.bar/baz bim>'" {
    const input =
        "<http://foo.bar/baz bim>\n";
    const expected =
        "<p>&lt;http://foo.bar/baz bim&gt;</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 612, line 9110: '<http://example.com/\\[\\>'" {
    const input =
        "<http://example.com/\\[\\>\n";
    const expected =
        "<p><a href=\"http://example.com/%5C%5B%5C\">http://example.com/\\[\\</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 613, line 9132: '<foo@bar.example.com>'" {
    const input =
        "<foo@bar.example.com>\n";
    const expected =
        "<p><a href=\"mailto:foo@bar.example.com\">foo@bar.example.com</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 614, line 9139: '<foo+special@Bar.baz-bar0.com>'" {
    const input =
        "<foo+special@Bar.baz-bar0.com>\n";
    const expected =
        "<p><a href=\"mailto:foo+special@Bar.baz-bar0.com\">foo+special@Bar.baz-bar0.com</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 615, line 9148: '<foo\\+@bar.example.com>'" {
    const input =
        "<foo\\+@bar.example.com>\n";
    const expected =
        "<p>&lt;foo+@bar.example.com&gt;</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 616, line 9157: '<>'" {
    const input =
        "<>\n";
    const expected =
        "<p>&lt;&gt;</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 617, line 9164: '< http://foo.bar >'" {
    const input =
        "< http://foo.bar >\n";
    const expected =
        "<p>&lt; http://foo.bar &gt;</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 618, line 9171: '<m:abc>'" {
    const input =
        "<m:abc>\n";
    const expected =
        "<p>&lt;m:abc&gt;</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 619, line 9178: '<foo.bar.baz>'" {
    const input =
        "<foo.bar.baz>\n";
    const expected =
        "<p>&lt;foo.bar.baz&gt;</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 620, line 9185: 'http://example.com'" {
    const input =
        "http://example.com\n";
    const expected =
        "<p><a href=\"http://example.com\">http://example.com</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 621, line 9192: 'foo@bar.example.com'" {
    const input =
        "foo@bar.example.com\n";
    const expected =
        "<p><a href=\"mailto:foo@bar.example.com\">foo@bar.example.com</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 622, line 9221: 'www.commonmark.org'" {
    const input =
        "www.commonmark.org\n";
    const expected =
        "<p><a href=\"http://www.commonmark.org\">www.commonmark.org</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 623, line 9229: 'Visit www.commonmark.org/help for more information.'" {
    const input =
        "Visit www.commonmark.org/help for more information.\n";
    const expected =
        "<p>Visit <a href=\"http://www.commonmark.org/help\">www.commonmark.org/help</a> for more information.</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 624, line 9241: 'Visit www.commonmark.org.\\n\\nVisit www.commonmark.org/a.b.'" {
    const input =
        "Visit www.commonmark.org.\n" ++
        "\n" ++
        "Visit www.commonmark.org/a.b.\n";
    const expected =
        "<p>Visit <a href=\"http://www.commonmark.org\">www.commonmark.org</a>.</p>\n" ++
        "<p>Visit <a href=\"http://www.commonmark.org/a.b\">www.commonmark.org/a.b</a>.</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 625, line 9255: 'www.google.com/search?q=Markup+(business)\\n\\nwww.google.com/search?q=Markup+(business)))\\n\\n(www.google.com/search?q=Markup+(business))\\n\\n(www.google.com/search?q=Markup+(business)'" {
    const input =
        "www.google.com/search?q=Markup+(business)\n" ++
        "\n" ++
        "www.google.com/search?q=Markup+(business)))\n" ++
        "\n" ++
        "(www.google.com/search?q=Markup+(business))\n" ++
        "\n" ++
        "(www.google.com/search?q=Markup+(business)\n";
    const expected =
        "<p><a href=\"http://www.google.com/search?q=Markup+(business)\">www.google.com/search?q=Markup+(business)</a></p>\n" ++
        "<p><a href=\"http://www.google.com/search?q=Markup+(business)\">www.google.com/search?q=Markup+(business)</a>))</p>\n" ++
        "<p>(<a href=\"http://www.google.com/search?q=Markup+(business)\">www.google.com/search?q=Markup+(business)</a>)</p>\n" ++
        "<p>(<a href=\"http://www.google.com/search?q=Markup+(business)\">www.google.com/search?q=Markup+(business)</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 626, line 9274: 'www.google.com/search?q=(business))+ok'" {
    const input =
        "www.google.com/search?q=(business))+ok\n";
    const expected =
        "<p><a href=\"http://www.google.com/search?q=(business))+ok\">www.google.com/search?q=(business))+ok</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 627, line 9285: 'www.google.com/search?q=commonmark&hl=en\\n\\nwww.google.com/search?q=commonmark&hl;'" {
    const input =
        "www.google.com/search?q=commonmark&hl=en\n" ++
        "\n" ++
        "www.google.com/search?q=commonmark&hl;\n";
    const expected =
        "<p><a href=\"http://www.google.com/search?q=commonmark&amp;hl=en\">www.google.com/search?q=commonmark&amp;hl=en</a></p>\n" ++
        "<p><a href=\"http://www.google.com/search?q=commonmark\">www.google.com/search?q=commonmark</a>&amp;hl;</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 628, line 9296: 'www.commonmark.org/he<lp'" {
    const input =
        "www.commonmark.org/he<lp\n";
    const expected =
        "<p><a href=\"http://www.commonmark.org/he\">www.commonmark.org/he</a>&lt;lp</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 629, line 9307: 'http://commonmark.org\\n\\n(Visit https://encrypted.google.com/search?q=Markup+(business))\\n\\nAnonymous FTP is available at ftp://foo.bar.baz.'" {
    const input =
        "http://commonmark.org\n" ++
        "\n" ++
        "(Visit https://encrypted.google.com/search?q=Markup+(business))\n" ++
        "\n" ++
        "Anonymous FTP is available at ftp://foo.bar.baz.\n";
    const expected =
        "<p><a href=\"http://commonmark.org\">http://commonmark.org</a></p>\n" ++
        "<p>(Visit <a href=\"https://encrypted.google.com/search?q=Markup+(business)\">https://encrypted.google.com/search?q=Markup+(business)</a>)</p>\n" ++
        "<p>Anonymous FTP is available at <a href=\"ftp://foo.bar.baz\">ftp://foo.bar.baz</a>.</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 630, line 9333: 'foo@bar.baz'" {
    const input =
        "foo@bar.baz\n";
    const expected =
        "<p><a href=\"mailto:foo@bar.baz\">foo@bar.baz</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 631, line 9341: 'hello@mail+xyz.example isn't valid, but hello+xyz@mail.example is.'" {
    const input =
        "hello@mail+xyz.example isn't valid, but hello+xyz@mail.example is.\n";
    const expected =
        "<p>hello@mail+xyz.example isn't valid, but <a href=\"mailto:hello+xyz@mail.example\">hello+xyz@mail.example</a> is.</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 632, line 9351: 'a.b-c_d@a.b\\n\\na.b-c_d@a.b.\\n\\na.b-c_d@a.b-\\n\\na.b-c_d@a.b_'" {
    const input =
        "a.b-c_d@a.b\n" ++
        "\n" ++
        "a.b-c_d@a.b.\n" ++
        "\n" ++
        "a.b-c_d@a.b-\n" ++
        "\n" ++
        "a.b-c_d@a.b_\n";
    const expected =
        "<p><a href=\"mailto:a.b-c_d@a.b\">a.b-c_d@a.b</a></p>\n" ++
        "<p><a href=\"mailto:a.b-c_d@a.b\">a.b-c_d@a.b</a>.</p>\n" ++
        "<p>a.b-c_d@a.b-</p>\n" ++
        "<p>a.b-c_d@a.b_</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 633, line 9375: 'mailto:foo@bar.baz\\n\\nmailto:a.b-c_d@a.b\\n\\nmailto:a.b-c_d@a.b.\\n\\nmailto:a.b-c_d@a.b/\\n\\nmailto:a.b-c_d@a.b-\\n\\nmailto:a.b-c_d@a.b_\\n\\nxmpp:foo@bar.baz\\n\\nxmpp:foo@bar.baz.'" {
    const input =
        "mailto:foo@bar.baz\n" ++
        "\n" ++
        "mailto:a.b-c_d@a.b\n" ++
        "\n" ++
        "mailto:a.b-c_d@a.b.\n" ++
        "\n" ++
        "mailto:a.b-c_d@a.b/\n" ++
        "\n" ++
        "mailto:a.b-c_d@a.b-\n" ++
        "\n" ++
        "mailto:a.b-c_d@a.b_\n" ++
        "\n" ++
        "xmpp:foo@bar.baz\n" ++
        "\n" ++
        "xmpp:foo@bar.baz.\n";
    const expected =
        "<p><a href=\"mailto:foo@bar.baz\">mailto:foo@bar.baz</a></p>\n" ++
        "<p><a href=\"mailto:a.b-c_d@a.b\">mailto:a.b-c_d@a.b</a></p>\n" ++
        "<p><a href=\"mailto:a.b-c_d@a.b\">mailto:a.b-c_d@a.b</a>.</p>\n" ++
        "<p><a href=\"mailto:a.b-c_d@a.b\">mailto:a.b-c_d@a.b</a>/</p>\n" ++
        "<p>mailto:a.b-c_d@a.b-</p>\n" ++
        "<p>mailto:a.b-c_d@a.b_</p>\n" ++
        "<p><a href=\"xmpp:foo@bar.baz\">xmpp:foo@bar.baz</a></p>\n" ++
        "<p><a href=\"xmpp:foo@bar.baz\">xmpp:foo@bar.baz</a>.</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 634, line 9406: 'xmpp:foo@bar.baz/txt\\n\\nxmpp:foo@bar.baz/txt@bin\\n\\nxmpp:foo@bar.baz/txt@bin.com'" {
    const input =
        "xmpp:foo@bar.baz/txt\n" ++
        "\n" ++
        "xmpp:foo@bar.baz/txt@bin\n" ++
        "\n" ++
        "xmpp:foo@bar.baz/txt@bin.com\n";
    const expected =
        "<p><a href=\"xmpp:foo@bar.baz/txt\">xmpp:foo@bar.baz/txt</a></p>\n" ++
        "<p><a href=\"xmpp:foo@bar.baz/txt@bin\">xmpp:foo@bar.baz/txt@bin</a></p>\n" ++
        "<p><a href=\"xmpp:foo@bar.baz/txt@bin.com\">xmpp:foo@bar.baz/txt@bin.com</a></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 635, line 9420: 'xmpp:foo@bar.baz/txt/bin'" {
    const input =
        "xmpp:foo@bar.baz/txt/bin\n";
    const expected =
        "<p><a href=\"xmpp:foo@bar.baz/txt\">xmpp:foo@bar.baz/txt</a>/bin</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 636, line 9502: '<a><bab><c2c>'" {
    const input =
        "<a><bab><c2c>\n";
    const expected =
        "<p><a><bab><c2c></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 637, line 9511: '<a/><b2/>'" {
    const input =
        "<a/><b2/>\n";
    const expected =
        "<p><a/><b2/></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 638, line 9520: '<a  /><b2\\ndata=\"foo\" >'" {
    const input =
        "<a  /><b2\n" ++
        "data=\"foo\" >\n";
    const expected =
        "<p><a  /><b2\n" ++
        "data=\"foo\" ></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 639, line 9531: '<a foo=\"bar\" bam = 'baz <em>\"</em>'\\n_boolean zoop:33=zoop:33 />'" {
    const input =
        "<a foo=\"bar\" bam = 'baz <em>\"</em>'\n" ++
        "_boolean zoop:33=zoop:33 />\n";
    const expected =
        "<p><a foo=\"bar\" bam = 'baz <em>\"</em>'\n" ++
        "_boolean zoop:33=zoop:33 /></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 640, line 9542: 'Foo <responsive-image src=\"foo.jpg\" />'" {
    const input =
        "Foo <responsive-image src=\"foo.jpg\" />\n";
    const expected =
        "<p>Foo <responsive-image src=\"foo.jpg\" /></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 641, line 9551: '<33> <__>'" {
    const input =
        "<33> <__>\n";
    const expected =
        "<p>&lt;33&gt; &lt;__&gt;</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 642, line 9560: '<a h*#ref=\"hi\">'" {
    const input =
        "<a h*#ref=\"hi\">\n";
    const expected =
        "<p>&lt;a h*#ref=&quot;hi&quot;&gt;</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 643, line 9569: '<a href=\"hi'> <a href=hi'>'" {
    const input =
        "<a href=\"hi'> <a href=hi'>\n";
    const expected =
        "<p>&lt;a href=&quot;hi'&gt; &lt;a href=hi'&gt;</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 644, line 9578: '< a><\\nfoo><bar/ >\\n<foo bar=baz\\nbim!bop />'" {
    const input =
        "< a><\n" ++
        "foo><bar/ >\n" ++
        "<foo bar=baz\n" ++
        "bim!bop />\n";
    const expected =
        "<p>&lt; a&gt;&lt;\n" ++
        "foo&gt;&lt;bar/ &gt;\n" ++
        "&lt;foo bar=baz\n" ++
        "bim!bop /&gt;</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 645, line 9593: '<a href='bar'title=title>'" {
    const input =
        "<a href='bar'title=title>\n";
    const expected =
        "<p>&lt;a href='bar'title=title&gt;</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 646, line 9602: '</a></foo >'" {
    const input =
        "</a></foo >\n";
    const expected =
        "<p></a></foo ></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 647, line 9611: '</a href=\"foo\">'" {
    const input =
        "</a href=\"foo\">\n";
    const expected =
        "<p>&lt;/a href=&quot;foo&quot;&gt;</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 648, line 9620: 'foo <!-- this is a --\\ncomment - with hyphens -->'" {
    const input =
        "foo <!-- this is a --\n" ++
        "comment - with hyphens -->\n";
    const expected =
        "<p>foo <!-- this is a --\n" ++
        "comment - with hyphens --></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 649, line 9628: 'foo <!-- this is a --\\ncomment - with hyphens -->'" {
    const input =
        "foo <!-- this is a --\n" ++
        "comment - with hyphens -->\n";
    const expected =
        "<p>foo <!-- this is a --\n" ++
        "comment - with hyphens --></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 650, line 9636: 'foo <!--> foo -->\\n\\nfoo <!---> foo -->'" {
    const input =
        "foo <!--> foo -->\n" ++
        "\n" ++
        "foo <!---> foo -->\n";
    const expected =
        "<p>foo <!--> foo --&gt;</p>\n" ++
        "<p>foo <!---> foo --&gt;</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 651, line 9648: 'foo <?php echo $a; ?>'" {
    const input =
        "foo <?php echo $a; ?>\n";
    const expected =
        "<p>foo <?php echo $a; ?></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 652, line 9657: 'foo <!ELEMENT br EMPTY>'" {
    const input =
        "foo <!ELEMENT br EMPTY>\n";
    const expected =
        "<p>foo <!ELEMENT br EMPTY></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 653, line 9666: 'foo <![CDATA[>&<]]>'" {
    const input =
        "foo <![CDATA[>&<]]>\n";
    const expected =
        "<p>foo <![CDATA[>&<]]></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 654, line 9676: 'foo <a href=\"&ouml;\">'" {
    const input =
        "foo <a href=\"&ouml;\">\n";
    const expected =
        "<p>foo <a href=\"&ouml;\"></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 655, line 9685: 'foo <a href=\"\\*\">'" {
    const input =
        "foo <a href=\"\\*\">\n";
    const expected =
        "<p>foo <a href=\"\\*\"></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 656, line 9692: '<a href=\"\\\"\">'" {
    const input =
        "<a href=\"\\\"\">\n";
    const expected =
        "<p>&lt;a href=&quot;&quot;&quot;&gt;</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

//test "Example 657, line 9723: '<strong> <title> <style> <em>\\n\\n<blockquote>\\n  <xmp> is disallowed.  <XMP> is also disallowed.\\n</blockquote>'" {
//    const input =
//        "<strong> <title> <style> <em>\n" ++
//        "\n" ++
//        "<blockquote>\n" ++
//        "  <xmp> is disallowed.  <XMP> is also disallowed.\n" ++
//        "</blockquote>\n";
//    const expected =
//        "<p><strong> &lt;title> &lt;style> <em></p>\n" ++
//        "<blockquote>\n" ++
//        "  &lt;xmp> is disallowed.  &lt;XMP> is also disallowed.\n" ++
//        "</blockquote>\n";
//
//    const gpa = std.testing.allocator;
//    var rules = try gfm.init(gpa);
//    defer rules.blocks.deinit();
//    defer rules.inlines.deinit();
////
//    const root = try parse.execute(gpa, input, rules, null);
//    defer root.deinit(gpa);
//
//    const html = try render.renderHtml(gpa, root, null);
//    defer gpa.free(html);
//
//    try std.testing.expectEqualStrings(expected, html);
//}

test "Example 658, line 9745: 'foo  \\nbaz'" {
    const input =
        "foo  \n" ++
        "baz\n";
    const expected =
        "<p>foo<br />\n" ++
        "baz</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 659, line 9757: 'foo\\\\nbaz'" {
    const input =
        "foo\\\n" ++
        "baz\n";
    const expected =
        "<p>foo<br />\n" ++
        "baz</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 660, line 9768: 'foo       \\nbaz'" {
    const input =
        "foo       \n" ++
        "baz\n";
    const expected =
        "<p>foo<br />\n" ++
        "baz</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 661, line 9779: 'foo  \\n     bar'" {
    const input =
        "foo  \n" ++
        "     bar\n";
    const expected =
        "<p>foo<br />\n" ++
        "bar</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 662, line 9788: 'foo\\\\n     bar'" {
    const input =
        "foo\\\n" ++
        "     bar\n";
    const expected =
        "<p>foo<br />\n" ++
        "bar</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 663, line 9800: '*foo  \\nbar*'" {
    const input =
        "*foo  \n" ++
        "bar*\n";
    const expected =
        "<p><em>foo<br />\n" ++
        "bar</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 664, line 9809: '*foo\\\\nbar*'" {
    const input =
        "*foo\\\n" ++
        "bar*\n";
    const expected =
        "<p><em>foo<br />\n" ++
        "bar</em></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 665, line 9820: '`code  \\nspan`'" {
    const input =
        "`code  \n" ++
        "span`\n";
    const expected =
        "<p><code>code   span</code></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 666, line 9828: '`code\\\\nspan`'" {
    const input =
        "`code\\\n" ++
        "span`\n";
    const expected =
        "<p><code>code\\ span</code></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 667, line 9838: '<a href=\"foo  \\nbar\">'" {
    const input =
        "<a href=\"foo  \n" ++
        "bar\">\n";
    const expected =
        "<p><a href=\"foo  \n" ++
        "bar\"></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 668, line 9847: '<a href=\"foo\\\\nbar\">'" {
    const input =
        "<a href=\"foo\\\n" ++
        "bar\">\n";
    const expected =
        "<p><a href=\"foo\\\n" ++
        "bar\"></p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 669, line 9860: 'foo\\'" {
    const input =
        "foo\\\n";
    const expected =
        "<p>foo\\</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 670, line 9867: 'foo  '" {
    const input =
        "foo  \n";
    const expected =
        "<p>foo</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 671, line 9874: '### foo\\'" {
    const input =
        "### foo\\\n";
    const expected =
        "<h3>foo\\</h3>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 672, line 9881: '### foo  '" {
    const input =
        "### foo  \n";
    const expected =
        "<h3>foo</h3>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 673, line 9896: 'foo\\nbaz'" {
    const input =
        "foo\n" ++
        "baz\n";
    const expected =
        "<p>foo\n" ++
        "baz</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 674, line 9908: 'foo \\n baz'" {
    const input =
        "foo \n" ++
        " baz\n";
    const expected =
        "<p>foo\n" ++
        "baz</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 675, line 9928: 'hello $.;'there'" {
    const input =
        "hello $.;'there\n";
    const expected =
        "<p>hello $.;'there</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 676, line 9935: 'Foo χρῆν'" {
    const input =
        "Foo χρῆν\n";
    const expected =
        "<p>Foo χρῆν</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "Example 677, line 9944: 'Multiple     spaces'" {
    const input =
        "Multiple     spaces\n";
    const expected =
        "<p>Multiple     spaces</p>\n";

    const gpa = std.testing.allocator;
    var rules = try gfm.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}
