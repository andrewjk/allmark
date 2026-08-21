const std = @import("std");

const parse = @import("allmark").parse;
const render = @import("allmark").render;
const core = @import("allmark").core;

test "basic parse" {
    const input =
        \\# Test ☺️
        \\
        \\Here is some text
        \\ *with* bold stuff
        \\
        \\* Tight item 1
        \\* Tight item 2
        \\
        \\- Loose item 1
        \\
        \\- Loose item 2
        \\
        \\## Subtest
        \\
        \\Here is some more text
        \\
    ;

    const expected =
        \\<h1>Test ☺️</h1>
        \\<p>Here is some text
        \\<em>with</em> bold stuff</p>
        \\<ul>
        \\<li>Tight item 1</li>
        \\<li>Tight item 2</li>
        \\</ul>
        \\<ul>
        \\<li>
        \\<p>Loose item 1</p>
        \\</li>
        \\<li>
        \\<p>Loose item 2</p>
        \\</li>
        \\</ul>
        \\<h2>Subtest</h2>
        \\<p>Here is some more text</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);

    try std.testing.expect(doc.children != null);
    try std.testing.expectEqualStrings("heading", doc.children.?[0].type);
    try std.testing.expectEqual(@as(usize, 0), doc.children.?[0].index);
    try std.testing.expectEqual(@as(usize, 13), doc.children.?[0].length);

    const start = doc.children.?[0].index;
    const end = start + doc.children.?[0].length;
    try std.testing.expectEqualStrings("# Test ☺️", input[start..end]);

    const input2 = try std.mem.replaceOwned(u8, gpa, input, "\r\n", "\r");
    defer gpa.free(input2);
    const input2b = try std.mem.replaceOwned(u8, gpa, input2, "\n", "\r");
    defer gpa.free(input2b);

    const doc2 = try parse.execute(gpa, input2b, rules);
    defer doc2.deinit(gpa);

    const html2 = try render(gpa, doc2, null, false, null);
    defer gpa.free(html2);

    const normalized_html2a = try std.mem.replaceOwned(u8, gpa, html2, "\r\n", "\n");
    defer gpa.free(normalized_html2a);
    const normalized_html2b = try std.mem.replaceOwned(u8, gpa, normalized_html2a, "\r", "\n");
    defer gpa.free(normalized_html2b);

    try std.testing.expectEqualStrings(expected, normalized_html2b);

    try std.testing.expect(doc2.children != null);
    try std.testing.expectEqualStrings("heading", doc2.children.?[0].type);
    try std.testing.expectEqual(@as(usize, 0), doc2.children.?[0].index);
    try std.testing.expectEqual(@as(usize, 13), doc2.children.?[0].length);

    const start2 = doc2.children.?[0].index;
    const end2 = start2 + doc2.children.?[0].length;
    try std.testing.expectEqualStrings("# Test ☺️", input2b[start2..end2]);
}
