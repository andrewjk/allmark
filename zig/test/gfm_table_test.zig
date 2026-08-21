const std = @import("std");

const transform = @import("allmark").transform;
const gfm = @import("allmark").gfm;
const htmlRenderers = @import("allmark").htmlRenderers;

test "spec table" {
    const input =
        \\
        \\| foo | bar |
        \\| --- | --- |
        \\| baz | bim |
        \\
        \\
    ;
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
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers, null);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers, null);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);

    const inputCrLf = std.mem.replaceOwned(u8, gpa, input, "\n", "\r\n") catch unreachable;
    defer gpa.free(inputCrLf);
    const htmlCrLf = try transform(gpa, inputCrLf, rules, renderers, null);
    defer gpa.free(htmlCrLf);
    const htmlCrLf2 = std.mem.replaceOwned(u8, gpa, htmlCrLf, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCrLf2);
    try std.testing.expectEqualStrings(expected, htmlCrLf2);

    const inputCr = std.mem.replaceOwned(u8, gpa, input, "\n", "\r") catch unreachable;
    defer gpa.free(inputCr);
    const htmlCr = try transform(gpa, inputCr, rules, renderers, null);
    defer gpa.free(htmlCr);
    const htmlCr2 = std.mem.replaceOwned(u8, gpa, htmlCr, "\r", "\n") catch unreachable;
    defer gpa.free(htmlCr2);
    try std.testing.expectEqualStrings(expected, htmlCr2);
}

test "table with alignment" {
    const input =
        \\
        \\| Left | Center | Right |
        \\| :--- | :----: | ----: |
        \\| foo  |  bar   |   baz |
        \\| a    |   b    |     c |
        \\
        \\
    ;
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
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers, null);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers, null);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);

    const inputCrLf = std.mem.replaceOwned(u8, gpa, input, "\n", "\r\n") catch unreachable;
    defer gpa.free(inputCrLf);
    const htmlCrLf = try transform(gpa, inputCrLf, rules, renderers, null);
    defer gpa.free(htmlCrLf);
    const htmlCrLf2 = std.mem.replaceOwned(u8, gpa, htmlCrLf, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCrLf2);
    try std.testing.expectEqualStrings(expected, htmlCrLf2);

    const inputCr = std.mem.replaceOwned(u8, gpa, input, "\n", "\r") catch unreachable;
    defer gpa.free(inputCr);
    const htmlCr = try transform(gpa, inputCr, rules, renderers, null);
    defer gpa.free(htmlCr);
    const htmlCr2 = std.mem.replaceOwned(u8, gpa, htmlCr, "\r", "\n") catch unreachable;
    defer gpa.free(htmlCr2);
    try std.testing.expectEqualStrings(expected, htmlCr2);
}

test "table with inline formatting" {
    const input =
        \\
        \\| Text | Code |
        \\| ---- | ---- |
        \\| **bold** | `code` |
        \\| *italic* | [link](url) |
        \\| ~~strike~~ | `multi` |
        \\
        \\
    ;
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
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers, null);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers, null);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);

    const inputCrLf = std.mem.replaceOwned(u8, gpa, input, "\n", "\r\n") catch unreachable;
    defer gpa.free(inputCrLf);
    const htmlCrLf = try transform(gpa, inputCrLf, rules, renderers, null);
    defer gpa.free(htmlCrLf);
    const htmlCrLf2 = std.mem.replaceOwned(u8, gpa, htmlCrLf, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCrLf2);
    try std.testing.expectEqualStrings(expected, htmlCrLf2);

    const inputCr = std.mem.replaceOwned(u8, gpa, input, "\n", "\r") catch unreachable;
    defer gpa.free(inputCr);
    const htmlCr = try transform(gpa, inputCr, rules, renderers, null);
    defer gpa.free(htmlCr);
    const htmlCr2 = std.mem.replaceOwned(u8, gpa, htmlCr, "\r", "\n") catch unreachable;
    defer gpa.free(htmlCr2);
    try std.testing.expectEqualStrings(expected, htmlCr2);
}

test "table with missing cells" {
    const input =
        \\
        \\| a | b | c |
        \\| - | - | - |
        \\| 1 | 2 |
        \\| 1 |
        \\
        \\
    ;
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
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers, null);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers, null);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);

    const inputCrLf = std.mem.replaceOwned(u8, gpa, input, "\n", "\r\n") catch unreachable;
    defer gpa.free(inputCrLf);
    const htmlCrLf = try transform(gpa, inputCrLf, rules, renderers, null);
    defer gpa.free(htmlCrLf);
    const htmlCrLf2 = std.mem.replaceOwned(u8, gpa, htmlCrLf, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCrLf2);
    try std.testing.expectEqualStrings(expected, htmlCrLf2);

    const inputCr = std.mem.replaceOwned(u8, gpa, input, "\n", "\r") catch unreachable;
    defer gpa.free(inputCr);
    const htmlCr = try transform(gpa, inputCr, rules, renderers, null);
    defer gpa.free(htmlCr);
    const htmlCr2 = std.mem.replaceOwned(u8, gpa, htmlCr, "\r", "\n") catch unreachable;
    defer gpa.free(htmlCr2);
    try std.testing.expectEqualStrings(expected, htmlCr2);
}

test "table with extra cells" {
    const input =
        \\
        \\| a | b |
        \\| - | - |
        \\| 1 | 2 | 3 | 4 |
        \\
        \\
    ;
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
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers, null);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers, null);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);

    const inputCrLf = std.mem.replaceOwned(u8, gpa, input, "\n", "\r\n") catch unreachable;
    defer gpa.free(inputCrLf);
    const htmlCrLf = try transform(gpa, inputCrLf, rules, renderers, null);
    defer gpa.free(htmlCrLf);
    const htmlCrLf2 = std.mem.replaceOwned(u8, gpa, htmlCrLf, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCrLf2);
    try std.testing.expectEqualStrings(expected, htmlCrLf2);

    const inputCr = std.mem.replaceOwned(u8, gpa, input, "\n", "\r") catch unreachable;
    defer gpa.free(inputCr);
    const htmlCr = try transform(gpa, inputCr, rules, renderers, null);
    defer gpa.free(htmlCr);
    const htmlCr2 = std.mem.replaceOwned(u8, gpa, htmlCr, "\r", "\n") catch unreachable;
    defer gpa.free(htmlCr2);
    try std.testing.expectEqualStrings(expected, htmlCr2);
}

test "table with only header" {
    const input =
        \\
        \\| foo | bar |
        \\| --- | --- |
        \\
        \\
    ;
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
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers, null);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers, null);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);

    const inputCrLf = std.mem.replaceOwned(u8, gpa, input, "\n", "\r\n") catch unreachable;
    defer gpa.free(inputCrLf);
    const htmlCrLf = try transform(gpa, inputCrLf, rules, renderers, null);
    defer gpa.free(htmlCrLf);
    const htmlCrLf2 = std.mem.replaceOwned(u8, gpa, htmlCrLf, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCrLf2);
    try std.testing.expectEqualStrings(expected, htmlCrLf2);

    const inputCr = std.mem.replaceOwned(u8, gpa, input, "\n", "\r") catch unreachable;
    defer gpa.free(inputCr);
    const htmlCr = try transform(gpa, inputCr, rules, renderers, null);
    defer gpa.free(htmlCr);
    const htmlCr2 = std.mem.replaceOwned(u8, gpa, htmlCr, "\r", "\n") catch unreachable;
    defer gpa.free(htmlCr2);
    try std.testing.expectEqualStrings(expected, htmlCr2);
}

test "table with empty cells" {
    const input =
        \\
        \\| a | b | c |
        \\| - | - | - |
        \\|   | 2 |   |
        \\| 1 |   | 3 |
        \\
        \\
    ;
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
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers, null);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers, null);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);

    const inputCrLf = std.mem.replaceOwned(u8, gpa, input, "\n", "\r\n") catch unreachable;
    defer gpa.free(inputCrLf);
    const htmlCrLf = try transform(gpa, inputCrLf, rules, renderers, null);
    defer gpa.free(htmlCrLf);
    const htmlCrLf2 = std.mem.replaceOwned(u8, gpa, htmlCrLf, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCrLf2);
    try std.testing.expectEqualStrings(expected, htmlCrLf2);

    const inputCr = std.mem.replaceOwned(u8, gpa, input, "\n", "\r") catch unreachable;
    defer gpa.free(inputCr);
    const htmlCr = try transform(gpa, inputCr, rules, renderers, null);
    defer gpa.free(htmlCr);
    const htmlCr2 = std.mem.replaceOwned(u8, gpa, htmlCr, "\r", "\n") catch unreachable;
    defer gpa.free(htmlCr2);
    try std.testing.expectEqualStrings(expected, htmlCr2);
}

test "table without outer pipes" {
    const input =
        \\
        \\a | b | c
        \\- | - | -
        \\1 | 2 | 3
        \\
        \\
    ;
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
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers, null);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers, null);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);

    const inputCrLf = std.mem.replaceOwned(u8, gpa, input, "\n", "\r\n") catch unreachable;
    defer gpa.free(inputCrLf);
    const htmlCrLf = try transform(gpa, inputCrLf, rules, renderers, null);
    defer gpa.free(htmlCrLf);
    const htmlCrLf2 = std.mem.replaceOwned(u8, gpa, htmlCrLf, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCrLf2);
    try std.testing.expectEqualStrings(expected, htmlCrLf2);

    const inputCr = std.mem.replaceOwned(u8, gpa, input, "\n", "\r") catch unreachable;
    defer gpa.free(inputCr);
    const htmlCr = try transform(gpa, inputCr, rules, renderers, null);
    defer gpa.free(htmlCr);
    const htmlCr2 = std.mem.replaceOwned(u8, gpa, htmlCr, "\r", "\n") catch unreachable;
    defer gpa.free(htmlCr2);
    try std.testing.expectEqualStrings(expected, htmlCr2);
}

test "table with whitespace variations" {
    const input =
        \\
        \\|  a  |  b  |  c  |
        \\| --- | --- | --- |
        \\| 1   |   2 |3    |
        \\
        \\
    ;
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
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers, null);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers, null);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);

    const inputCrLf = std.mem.replaceOwned(u8, gpa, input, "\n", "\r\n") catch unreachable;
    defer gpa.free(inputCrLf);
    const htmlCrLf = try transform(gpa, inputCrLf, rules, renderers, null);
    defer gpa.free(htmlCrLf);
    const htmlCrLf2 = std.mem.replaceOwned(u8, gpa, htmlCrLf, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCrLf2);
    try std.testing.expectEqualStrings(expected, htmlCrLf2);

    const inputCr = std.mem.replaceOwned(u8, gpa, input, "\n", "\r") catch unreachable;
    defer gpa.free(inputCr);
    const htmlCr = try transform(gpa, inputCr, rules, renderers, null);
    defer gpa.free(htmlCr);
    const htmlCr2 = std.mem.replaceOwned(u8, gpa, htmlCr, "\r", "\n") catch unreachable;
    defer gpa.free(htmlCr2);
    try std.testing.expectEqualStrings(expected, htmlCr2);
}

test "table with mixed content types" {
    const input =
        \\
        \\| Type | Example |
        \\| ---- | ------- |
        \\| Text | plain text |
        \\| Code | `inline` |
        \\| Bold | **strong** |
        \\| Link | [text](http://example.com) |
        \\
        \\
    ;
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
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers, null);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers, null);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);

    const inputCrLf = std.mem.replaceOwned(u8, gpa, input, "\n", "\r\n") catch unreachable;
    defer gpa.free(inputCrLf);
    const htmlCrLf = try transform(gpa, inputCrLf, rules, renderers, null);
    defer gpa.free(htmlCrLf);
    const htmlCrLf2 = std.mem.replaceOwned(u8, gpa, htmlCrLf, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCrLf2);
    try std.testing.expectEqualStrings(expected, htmlCrLf2);

    const inputCr = std.mem.replaceOwned(u8, gpa, input, "\n", "\r") catch unreachable;
    defer gpa.free(inputCr);
    const htmlCr = try transform(gpa, inputCr, rules, renderers, null);
    defer gpa.free(htmlCr);
    const htmlCr2 = std.mem.replaceOwned(u8, gpa, htmlCr, "\r", "\n") catch unreachable;
    defer gpa.free(htmlCr2);
    try std.testing.expectEqualStrings(expected, htmlCr2);
}

test "table with single column" {
    const input =
        \\
        \\| Column |
        \\| ------ |
        \\| data   |
        \\| more   |
        \\
        \\
    ;
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
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers, null);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers, null);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);

    const inputCrLf = std.mem.replaceOwned(u8, gpa, input, "\n", "\r\n") catch unreachable;
    defer gpa.free(inputCrLf);
    const htmlCrLf = try transform(gpa, inputCrLf, rules, renderers, null);
    defer gpa.free(htmlCrLf);
    const htmlCrLf2 = std.mem.replaceOwned(u8, gpa, htmlCrLf, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCrLf2);
    try std.testing.expectEqualStrings(expected, htmlCrLf2);

    const inputCr = std.mem.replaceOwned(u8, gpa, input, "\n", "\r") catch unreachable;
    defer gpa.free(inputCr);
    const htmlCr = try transform(gpa, inputCr, rules, renderers, null);
    defer gpa.free(htmlCr);
    const htmlCr2 = std.mem.replaceOwned(u8, gpa, htmlCr, "\r", "\n") catch unreachable;
    defer gpa.free(htmlCr2);
    try std.testing.expectEqualStrings(expected, htmlCr2);
}

test "table with many columns" {
    const input =
        \\
        \\| A | B | C | D | E | F |
        \\| - | - | - | - | - | - |
        \\| 1 | 2 | 3 | 4 | 5 | 6 |
        \\| a | b | c | d | e | f |
        \\
        \\
    ;
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
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers, null);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers, null);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);

    const inputCrLf = std.mem.replaceOwned(u8, gpa, input, "\n", "\r\n") catch unreachable;
    defer gpa.free(inputCrLf);
    const htmlCrLf = try transform(gpa, inputCrLf, rules, renderers, null);
    defer gpa.free(htmlCrLf);
    const htmlCrLf2 = std.mem.replaceOwned(u8, gpa, htmlCrLf, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCrLf2);
    try std.testing.expectEqualStrings(expected, htmlCrLf2);

    const inputCr = std.mem.replaceOwned(u8, gpa, input, "\n", "\r") catch unreachable;
    defer gpa.free(inputCr);
    const htmlCr = try transform(gpa, inputCr, rules, renderers, null);
    defer gpa.free(htmlCr);
    const htmlCr2 = std.mem.replaceOwned(u8, gpa, htmlCr, "\r", "\n") catch unreachable;
    defer gpa.free(htmlCr2);
    try std.testing.expectEqualStrings(expected, htmlCr2);
}
