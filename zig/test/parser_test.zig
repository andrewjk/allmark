const std = @import("std");

const parse = @import("../src/parse/parse.zig").parse;
const renderHtml = @import("../src/render/renderHtml.zig").renderHtml;
const core = @import("../src/rulesets/core.zig");

test "basic parse" {
    const input =
        \\# Test
        \\
        \\Here is some text
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
        \\<p>Here is some text</p>
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
    ;

    const gpa = std.testing.allocator;
    var rules = try core.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();
    defer rules.renderers.deinit();

    const root = try parse(gpa, input, rules, null);
    defer {
        var walker = root;
        while (walker) |node| : (walker = node.next) {
            if (node.children) |children| {
                for (children.items) |child| {
                    child.deinit(gpa);
                }
                children.deinit(gpa);
            }
        }
        root.deinit(gpa);
    }

    const html = try renderHtml(gpa, root, rules);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}
