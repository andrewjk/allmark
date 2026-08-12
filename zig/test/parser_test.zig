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
    try std.testing.expectEqual(@as(usize, 14), doc.children.?[0].length);

    const start = doc.children.?[0].index;
    const end = start + doc.children.?[0].length;
    try std.testing.expectEqualStrings("# Test ☺️\n", input[start..end]);
}
