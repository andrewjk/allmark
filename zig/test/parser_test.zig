const std = @import("std");

const parse = @import("allmark").parse;
const render = @import("allmark").render;
const core = @import("allmark").core;

test "basic parse" {
    const input =
        \\# Test
        \\
        \\Here is some *text*
        \\
        \\* Tight item 1
        \\* Tight item 2
        \\
        \\- Loose item 1
        \\
        \\- Loose item 2
        \\
    ;

    const expected =
        \\<h1>Test</h1>
        \\<p>Here is some <em>text</em></p>
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
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}
