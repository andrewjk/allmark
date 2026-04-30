const std = @import("std");

const parse = @import("allmark").parse;
const render = @import("allmark").render;
const gfm = @import("allmark").gfm;

test "spec table" {
    const input = "| foo | bar |\n| --- | --- |\n| baz | bim |";

    const expected =
        \\<table>
        \\<thead>
        \\<tr>
        \\<th>foo</th>
        \\<th>bar</th>
        \\</tr>
        \\</thead>
        \\<tbody>
        \\<tr>
        \\<td>baz</td>
        \\<td>bim</td>
        \\</tr>
        \\</tbody>
        \\</table>
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

test "table with alignment" {
    const input = "| Left | Center | Right |\n| :--- | :----: | ----: |\n| foo  |  bar   |   baz |\n| a    |   b    |     c |";

    const expected =
        \\<table>
        \\<thead>
        \\<tr>
        \\<th align="left">Left</th>
        \\<th align="center">Center</th>
        \\<th align="right">Right</th>
        \\</tr>
        \\</thead>
        \\<tbody>
        \\<tr>
        \\<td align="left">foo</td>
        \\<td align="center">bar</td>
        \\<td align="right">baz</td>
        \\</tr>
        \\<tr>
        \\<td align="left">a</td>
        \\<td align="center">b</td>
        \\<td align="right">c</td>
        \\</tr>
        \\</tbody>
        \\</table>
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

test "table with inline formatting" {
    const input = "| Text | Code |\n| ---- | ---- |\n| **bold** | `code` |\n| *italic* | [link](url) |\n| ~~strike~~ | `multi` |";

    const expected =
        \\<table>
        \\<thead>
        \\<tr>
        \\<th>Text</th>
        \\<th>Code</th>
        \\</tr>
        \\</thead>
        \\<tbody>
        \\<tr>
        \\<td><strong>bold</strong></td>
        \\<td><code>code</code></td>
        \\</tr>
        \\<tr>
        \\<td><em>italic</em></td>
        \\<td><a href="url">link</a></td>
        \\</tr>
        \\<tr>
        \\<td><del>strike</del></td>
        \\<td><code>multi</code></td>
        \\</tr>
        \\</tbody>
        \\</table>
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

test "table with missing cells" {
    const input = "| a | b | c |\n| - | - | - |\n| 1 | 2 |\n| 1 |";

    const expected =
        \\<table>
        \\<thead>
        \\<tr>
        \\<th>a</th>
        \\<th>b</th>
        \\<th>c</th>
        \\</tr>
        \\</thead>
        \\<tbody>
        \\<tr>
        \\<td>1</td>
        \\<td>2</td>
        \\<td></td>
        \\</tr>
        \\<tr>
        \\<td>1</td>
        \\<td></td>
        \\<td></td>
        \\</tr>
        \\</tbody>
        \\</table>
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

test "table with extra cells" {
    const input = "| a | b |\n| - | - |\n| 1 | 2 | 3 | 4 |";

    const expected =
        \\<table>
        \\<thead>
        \\<tr>
        \\<th>a</th>
        \\<th>b</th>
        \\</tr>
        \\</thead>
        \\<tbody>
        \\<tr>
        \\<td>1</td>
        \\<td>2</td>
        \\</tr>
        \\</tbody>
        \\</table>
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

test "table with only header" {
    const input = "| foo | bar |\n| --- | --- |";

    const expected =
        \\<table>
        \\<thead>
        \\<tr>
        \\<th>foo</th>
        \\<th>bar</th>
        \\</tr>
        \\</thead>
        \\</table>
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

test "table with empty cells" {
    const input = "| a | b | c |\n| - | - | - |\n|   | 2 |   |\n| 1 |   | 3 |";

    const expected =
        \\<table>
        \\<thead>
        \\<tr>
        \\<th>a</th>
        \\<th>b</th>
        \\<th>c</th>
        \\</tr>
        \\</thead>
        \\<tbody>
        \\<tr>
        \\<td></td>
        \\<td>2</td>
        \\<td></td>
        \\</tr>
        \\<tr>
        \\<td>1</td>
        \\<td></td>
        \\<td>3</td>
        \\</tr>
        \\</tbody>
        \\</table>
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

test "table without outer pipes" {
    const input = "a | b | c\n- | - | -\n1 | 2 | 3";

    const expected =
        \\<p>a | b | c</p>
        \\<ul>
        \\<li>| - | -
        \\1 | 2 | 3</li>
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

test "table with whitespace variations" {
    const input = "|  a  |  b  |  c  |\n| --- | --- | --- |\n| 1   |   2 |3    |";

    const expected =
        \\<table>
        \\<thead>
        \\<tr>
        \\<th>a</th>
        \\<th>b</th>
        \\<th>c</th>
        \\</tr>
        \\</thead>
        \\<tbody>
        \\<tr>
        \\<td>1</td>
        \\<td>2</td>
        \\<td>3</td>
        \\</tr>
        \\</tbody>
        \\</table>
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

test "table with mixed content types" {
    const input = "| Type | Example |\n| ---- | ------- |\n| Text | plain text |\n| Code | `inline` |\n| Bold | **strong** |\n| Link | [text](http://example.com) |";

    const expected =
        \\<table>
        \\<thead>
        \\<tr>
        \\<th>Type</th>
        \\<th>Example</th>
        \\</tr>
        \\</thead>
        \\<tbody>
        \\<tr>
        \\<td>Text</td>
        \\<td>plain text</td>
        \\</tr>
        \\<tr>
        \\<td>Code</td>
        \\<td><code>inline</code></td>
        \\</tr>
        \\<tr>
        \\<td>Bold</td>
        \\<td><strong>strong</strong></td>
        \\</tr>
        \\<tr>
        \\<td>Link</td>
        \\<td><a href="http://example.com">text</a></td>
        \\</tr>
        \\</tbody>
        \\</table>
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

test "table with single column" {
    const input = "| Column |\n| ------ |\n| data   |\n| more   |";

    const expected =
        \\<table>
        \\<thead>
        \\<tr>
        \\<th>Column</th>
        \\</tr>
        \\</thead>
        \\<tbody>
        \\<tr>
        \\<td>data</td>
        \\</tr>
        \\<tr>
        \\<td>more</td>
        \\</tr>
        \\</tbody>
        \\</table>
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

test "table with many columns" {
    const input = "| A | B | C | D | E | F |\n| - | - | - | - | - | - |\n| 1 | 2 | 3 | 4 | 5 | 6 |\n| a | b | c | d | e | f |";

    const expected =
        \\<table>
        \\<thead>
        \\<tr>
        \\<th>A</th>
        \\<th>B</th>
        \\<th>C</th>
        \\<th>D</th>
        \\<th>E</th>
        \\<th>F</th>
        \\</tr>
        \\</thead>
        \\<tbody>
        \\<tr>
        \\<td>1</td>
        \\<td>2</td>
        \\<td>3</td>
        \\<td>4</td>
        \\<td>5</td>
        \\<td>6</td>
        \\</tr>
        \\<tr>
        \\<td>a</td>
        \\<td>b</td>
        \\<td>c</td>
        \\<td>d</td>
        \\<td>e</td>
        \\<td>f</td>
        \\</tr>
        \\</tbody>
        \\</table>
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
