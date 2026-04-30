const std = @import("std");

const parse = @import("allmark").parse;
const render = @import("allmark").render;
const gfm = @import("allmark").gfm;

test "spec tasklist" {
    const input = "- [ ] foo\n- [x] bar";

    const expected =
        \\<ul>
        \\<li><input type="checkbox" disabled="" /> foo</li>
        \\<li><input type="checkbox" checked="" disabled="" /> bar</li>
        \\</ul>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "tasklist with asterisk marker" {
    const input = "* [ ] unchecked\n* [x] checked";

    const expected =
        \\<ul>
        \\<li><input type="checkbox" disabled="" /> unchecked</li>
        \\<li><input type="checkbox" checked="" disabled="" /> checked</li>
        \\</ul>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "tasklist with plus marker" {
    const input = "+ [ ] unchecked\n+ [x] checked";

    const expected =
        \\<ul>
        \\<li><input type="checkbox" disabled="" /> unchecked</li>
        \\<li><input type="checkbox" checked="" disabled="" /> checked</li>
        \\</ul>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "tasklist in ordered list" {
    const input = "1. [ ] unchecked item\n2. [x] checked item";

    const expected =
        \\<ol>
        \\<li><input type="checkbox" disabled="" /> unchecked item</li>
        \\<li><input type="checkbox" checked="" disabled="" /> checked item</li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "tasklist with inline formatting" {
    const input = "- [ ] **bold** task\n- [x] *italic* task\n- [ ] ~~strikethrough~~ task";

    const expected =
        \\<ul>
        \\<li><input type="checkbox" disabled="" /> <strong>bold</strong> task</li>
        \\<li><input type="checkbox" checked="" disabled="" /> <em>italic</em> task</li>
        \\<li><input type="checkbox" disabled="" /> <del>strikethrough</del> task</li>
        \\</ul>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "tasklist with code" {
    const input = "- [ ] task with `code`\n- [x] another `code` task";

    const expected =
        \\<ul>
        \\<li><input type="checkbox" disabled="" /> task with <code>code</code></li>
        \\<li><input type="checkbox" checked="" disabled="" /> another <code>code</code> task</li>
        \\</ul>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "tasklist with links" {
    const input = "- [ ] task with [link](http://example.com)\n- [x] checked [link](http://example.com) task";

    const expected =
        \\<ul>
        \\<li><input type="checkbox" disabled="" /> task with <a href="http://example.com">link</a></li>
        \\<li><input type="checkbox" checked="" disabled="" /> checked <a href="http://example.com">link</a> task</li>
        \\</ul>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "nested tasklist" {
    const input = "- [ ] parent task\n  - [ ] child task 1\n  - [x] child task 2\n- [x] another parent";

    const expected =
        \\<ul>
        \\<li><input type="checkbox" disabled="" /> parent task
        \\<ul>
        \\<li><input type="checkbox" disabled="" /> child task 1</li>
        \\<li><input type="checkbox" checked="" disabled="" /> child task 2</li>
        \\</ul>
        \\</li>
        \\<li><input type="checkbox" checked="" disabled="" /> another parent</li>
        \\</ul>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "mixed tasks and regular items" {
    const input = "- [ ] task item\n- regular item\n- [x] checked task\n- another regular item";

    const expected =
        \\<ul>
        \\<li><input type="checkbox" disabled="" /> task item</li>
        \\<li>regular item</li>
        \\<li><input type="checkbox" checked="" disabled="" /> checked task</li>
        \\<li>another regular item</li>
        \\</ul>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "tasklist with single character" {
    const input = "- [ ] a\n- [x] b";

    const expected =
        \\<ul>
        \\<li><input type="checkbox" disabled="" /> a</li>
        \\<li><input type="checkbox" checked="" disabled="" /> b</li>
        \\</ul>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "tasklist with empty brackets" {
    const input = "- [ ] \n- [x] ";

    const expected =
        \\<ul>
        \\<li><input type="checkbox" disabled="" /> </li>
        \\<li><input type="checkbox" checked="" disabled="" /> </li>
        \\</ul>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "tasklist with uppercase X" {
    const input = "- [ ] unchecked\n- [X] checked with uppercase";

    const expected =
        \\<ul>
        \\<li><input type="checkbox" disabled="" /> unchecked</li>
        \\<li><input type="checkbox" checked="" disabled="" /> checked with uppercase</li>
        \\</ul>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "tasklist in blockquote" {
    const input = "> - [ ] quoted task\n> - [x] checked quoted task";

    const expected =
        \\<blockquote>
        \\<ul>
        \\<li>[ ] quoted task</li>
        \\<li>[x] checked quoted task</li>
        \\</ul>
        \\</blockquote>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "tasklist with multiple paragraphs" {
    const input = "- [ ] task with paragraph\n\n  continuation paragraph\n- [x] another task";

    const expected =
        \\<ul>
        \\<li><input type="checkbox" disabled="" /> 
        \\<p>task with paragraph</p>
        \\<p>continuation paragraph</p>
        \\</li>
        \\<li><input type="checkbox" checked="" disabled="" /> 
        \\<p>another task</p>
        \\</li>
        \\</ul>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "tasklist with sublist" {
    const input = "- [ ] task with sublist\n  - subitem 1\n  - subitem 2\n- [x] checked task";

    const expected =
        \\<ul>
        \\<li><input type="checkbox" disabled="" /> task with sublist
        \\<ul>
        \\<li>subitem 1</li>
        \\<li>subitem 2</li>
        \\</ul>
        \\</li>
        \\<li><input type="checkbox" checked="" disabled="" /> checked task</li>
        \\</ul>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "tasklist with html entities" {
    const input = "- [ ] task with &amp; entity\n- [x] task with &lt;HTML&gt;";

    const expected =
        \\<ul>
        \\<li><input type="checkbox" disabled="" /> task with &amp; entity</li>
        \\<li><input type="checkbox" checked="" disabled="" /> task with &lt;HTML&gt;</li>
        \\</ul>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "tasklist with various whitespace" {
    const input = "- [ ]one\n- [  ] two\n- [ x] three";

    const expected =
        \\<ul>
        \\<li>[ ]one</li>
        \\<li>[  ] two</li>
        \\<li>[ x] three</li>
        \\</ul>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}
