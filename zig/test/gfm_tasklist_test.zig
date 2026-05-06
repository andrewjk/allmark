const std = @import("std");

const transform = @import("allmark").transform;
const gfm = @import("allmark").gfm;
const htmlRenderers = @import("allmark").htmlRenderers;

test "spec tasklist" {
    const input =
        \\
        \\- [ ] foo
        \\- [x] bar
        \\
    ;
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
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);

    const inputCrLf = std.mem.replaceOwned(u8, gpa, input, "\n", "\r\n") catch unreachable;
    defer gpa.free(inputCrLf);
    const htmlCrLf = try transform(gpa, inputCrLf, rules, renderers);
    defer gpa.free(htmlCrLf);
    const htmlCrLf2 = std.mem.replaceOwned(u8, gpa, htmlCrLf, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCrLf2);
    try std.testing.expectEqualStrings(expected, htmlCrLf2);

    const inputCr = std.mem.replaceOwned(u8, gpa, input, "\n", "\r") catch unreachable;
    defer gpa.free(inputCr);
    const htmlCr = try transform(gpa, inputCr, rules, renderers);
    defer gpa.free(htmlCr);
    const htmlCr2 = std.mem.replaceOwned(u8, gpa, htmlCr, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCr2);
    const htmlCr3 = std.mem.replaceOwned(u8, gpa, htmlCr2, "\r", "\n") catch unreachable;
    defer gpa.free(htmlCr3);
    try std.testing.expectEqualStrings(expected, htmlCr3);
}

test "tasklist with asterisk marker" {
    const input =
        \\
        \\* [ ] unchecked
        \\* [x] checked
        \\
    ;
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
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);

    const inputCrLf = std.mem.replaceOwned(u8, gpa, input, "\n", "\r\n") catch unreachable;
    defer gpa.free(inputCrLf);
    const htmlCrLf = try transform(gpa, inputCrLf, rules, renderers);
    defer gpa.free(htmlCrLf);
    const htmlCrLf2 = std.mem.replaceOwned(u8, gpa, htmlCrLf, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCrLf2);
    try std.testing.expectEqualStrings(expected, htmlCrLf2);

    const inputCr = std.mem.replaceOwned(u8, gpa, input, "\n", "\r") catch unreachable;
    defer gpa.free(inputCr);
    const htmlCr = try transform(gpa, inputCr, rules, renderers);
    defer gpa.free(htmlCr);
    const htmlCr2 = std.mem.replaceOwned(u8, gpa, htmlCr, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCr2);
    const htmlCr3 = std.mem.replaceOwned(u8, gpa, htmlCr2, "\r", "\n") catch unreachable;
    defer gpa.free(htmlCr3);
    try std.testing.expectEqualStrings(expected, htmlCr3);
}

test "tasklist with plus marker" {
    const input =
        \\
        \\+ [ ] unchecked
        \\+ [x] checked
        \\
    ;
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
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);

    const inputCrLf = std.mem.replaceOwned(u8, gpa, input, "\n", "\r\n") catch unreachable;
    defer gpa.free(inputCrLf);
    const htmlCrLf = try transform(gpa, inputCrLf, rules, renderers);
    defer gpa.free(htmlCrLf);
    const htmlCrLf2 = std.mem.replaceOwned(u8, gpa, htmlCrLf, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCrLf2);
    try std.testing.expectEqualStrings(expected, htmlCrLf2);

    const inputCr = std.mem.replaceOwned(u8, gpa, input, "\n", "\r") catch unreachable;
    defer gpa.free(inputCr);
    const htmlCr = try transform(gpa, inputCr, rules, renderers);
    defer gpa.free(htmlCr);
    const htmlCr2 = std.mem.replaceOwned(u8, gpa, htmlCr, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCr2);
    const htmlCr3 = std.mem.replaceOwned(u8, gpa, htmlCr2, "\r", "\n") catch unreachable;
    defer gpa.free(htmlCr3);
    try std.testing.expectEqualStrings(expected, htmlCr3);
}

test "tasklist in ordered list" {
    const input =
        \\
        \\1. [ ] unchecked item
        \\2. [x] checked item
        \\
    ;
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
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);

    const inputCrLf = std.mem.replaceOwned(u8, gpa, input, "\n", "\r\n") catch unreachable;
    defer gpa.free(inputCrLf);
    const htmlCrLf = try transform(gpa, inputCrLf, rules, renderers);
    defer gpa.free(htmlCrLf);
    const htmlCrLf2 = std.mem.replaceOwned(u8, gpa, htmlCrLf, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCrLf2);
    try std.testing.expectEqualStrings(expected, htmlCrLf2);

    const inputCr = std.mem.replaceOwned(u8, gpa, input, "\n", "\r") catch unreachable;
    defer gpa.free(inputCr);
    const htmlCr = try transform(gpa, inputCr, rules, renderers);
    defer gpa.free(htmlCr);
    const htmlCr2 = std.mem.replaceOwned(u8, gpa, htmlCr, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCr2);
    const htmlCr3 = std.mem.replaceOwned(u8, gpa, htmlCr2, "\r", "\n") catch unreachable;
    defer gpa.free(htmlCr3);
    try std.testing.expectEqualStrings(expected, htmlCr3);
}

test "tasklist with inline formatting" {
    const input =
        \\
        \\- [ ] **bold** task
        \\- [x] *italic* task
        \\- [ ] ~~strikethrough~~ task
        \\
    ;
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
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);

    const inputCrLf = std.mem.replaceOwned(u8, gpa, input, "\n", "\r\n") catch unreachable;
    defer gpa.free(inputCrLf);
    const htmlCrLf = try transform(gpa, inputCrLf, rules, renderers);
    defer gpa.free(htmlCrLf);
    const htmlCrLf2 = std.mem.replaceOwned(u8, gpa, htmlCrLf, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCrLf2);
    try std.testing.expectEqualStrings(expected, htmlCrLf2);

    const inputCr = std.mem.replaceOwned(u8, gpa, input, "\n", "\r") catch unreachable;
    defer gpa.free(inputCr);
    const htmlCr = try transform(gpa, inputCr, rules, renderers);
    defer gpa.free(htmlCr);
    const htmlCr2 = std.mem.replaceOwned(u8, gpa, htmlCr, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCr2);
    const htmlCr3 = std.mem.replaceOwned(u8, gpa, htmlCr2, "\r", "\n") catch unreachable;
    defer gpa.free(htmlCr3);
    try std.testing.expectEqualStrings(expected, htmlCr3);
}

test "tasklist with code" {
    const input =
        \\
        \\- [ ] task with `code`
        \\- [x] another `code` task
        \\
    ;
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
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);

    const inputCrLf = std.mem.replaceOwned(u8, gpa, input, "\n", "\r\n") catch unreachable;
    defer gpa.free(inputCrLf);
    const htmlCrLf = try transform(gpa, inputCrLf, rules, renderers);
    defer gpa.free(htmlCrLf);
    const htmlCrLf2 = std.mem.replaceOwned(u8, gpa, htmlCrLf, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCrLf2);
    try std.testing.expectEqualStrings(expected, htmlCrLf2);

    const inputCr = std.mem.replaceOwned(u8, gpa, input, "\n", "\r") catch unreachable;
    defer gpa.free(inputCr);
    const htmlCr = try transform(gpa, inputCr, rules, renderers);
    defer gpa.free(htmlCr);
    const htmlCr2 = std.mem.replaceOwned(u8, gpa, htmlCr, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCr2);
    const htmlCr3 = std.mem.replaceOwned(u8, gpa, htmlCr2, "\r", "\n") catch unreachable;
    defer gpa.free(htmlCr3);
    try std.testing.expectEqualStrings(expected, htmlCr3);
}

test "tasklist with links" {
    const input =
        \\
        \\- [ ] task with [link](http://example.com)
        \\- [x] checked [link](http://example.com) task
        \\
    ;
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
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);

    const inputCrLf = std.mem.replaceOwned(u8, gpa, input, "\n", "\r\n") catch unreachable;
    defer gpa.free(inputCrLf);
    const htmlCrLf = try transform(gpa, inputCrLf, rules, renderers);
    defer gpa.free(htmlCrLf);
    const htmlCrLf2 = std.mem.replaceOwned(u8, gpa, htmlCrLf, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCrLf2);
    try std.testing.expectEqualStrings(expected, htmlCrLf2);

    const inputCr = std.mem.replaceOwned(u8, gpa, input, "\n", "\r") catch unreachable;
    defer gpa.free(inputCr);
    const htmlCr = try transform(gpa, inputCr, rules, renderers);
    defer gpa.free(htmlCr);
    const htmlCr2 = std.mem.replaceOwned(u8, gpa, htmlCr, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCr2);
    const htmlCr3 = std.mem.replaceOwned(u8, gpa, htmlCr2, "\r", "\n") catch unreachable;
    defer gpa.free(htmlCr3);
    try std.testing.expectEqualStrings(expected, htmlCr3);
}

test "nested tasklist" {
    const input =
        \\
        \\- [ ] parent task
        \\  - [ ] child task 1
        \\  - [x] child task 2
        \\- [x] another parent
        \\
    ;
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
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);

    const inputCrLf = std.mem.replaceOwned(u8, gpa, input, "\n", "\r\n") catch unreachable;
    defer gpa.free(inputCrLf);
    const htmlCrLf = try transform(gpa, inputCrLf, rules, renderers);
    defer gpa.free(htmlCrLf);
    const htmlCrLf2 = std.mem.replaceOwned(u8, gpa, htmlCrLf, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCrLf2);
    try std.testing.expectEqualStrings(expected, htmlCrLf2);

    const inputCr = std.mem.replaceOwned(u8, gpa, input, "\n", "\r") catch unreachable;
    defer gpa.free(inputCr);
    const htmlCr = try transform(gpa, inputCr, rules, renderers);
    defer gpa.free(htmlCr);
    const htmlCr2 = std.mem.replaceOwned(u8, gpa, htmlCr, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCr2);
    const htmlCr3 = std.mem.replaceOwned(u8, gpa, htmlCr2, "\r", "\n") catch unreachable;
    defer gpa.free(htmlCr3);
    try std.testing.expectEqualStrings(expected, htmlCr3);
}

test "mixed tasks and regular items" {
    const input =
        \\
        \\- [ ] task item
        \\- regular item
        \\- [x] checked task
        \\- another regular item
        \\
    ;
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
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);

    const inputCrLf = std.mem.replaceOwned(u8, gpa, input, "\n", "\r\n") catch unreachable;
    defer gpa.free(inputCrLf);
    const htmlCrLf = try transform(gpa, inputCrLf, rules, renderers);
    defer gpa.free(htmlCrLf);
    const htmlCrLf2 = std.mem.replaceOwned(u8, gpa, htmlCrLf, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCrLf2);
    try std.testing.expectEqualStrings(expected, htmlCrLf2);

    const inputCr = std.mem.replaceOwned(u8, gpa, input, "\n", "\r") catch unreachable;
    defer gpa.free(inputCr);
    const htmlCr = try transform(gpa, inputCr, rules, renderers);
    defer gpa.free(htmlCr);
    const htmlCr2 = std.mem.replaceOwned(u8, gpa, htmlCr, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCr2);
    const htmlCr3 = std.mem.replaceOwned(u8, gpa, htmlCr2, "\r", "\n") catch unreachable;
    defer gpa.free(htmlCr3);
    try std.testing.expectEqualStrings(expected, htmlCr3);
}

test "tasklist with single character" {
    const input =
        \\
        \\- [ ] a
        \\- [x] b
        \\
    ;
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
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);

    const inputCrLf = std.mem.replaceOwned(u8, gpa, input, "\n", "\r\n") catch unreachable;
    defer gpa.free(inputCrLf);
    const htmlCrLf = try transform(gpa, inputCrLf, rules, renderers);
    defer gpa.free(htmlCrLf);
    const htmlCrLf2 = std.mem.replaceOwned(u8, gpa, htmlCrLf, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCrLf2);
    try std.testing.expectEqualStrings(expected, htmlCrLf2);

    const inputCr = std.mem.replaceOwned(u8, gpa, input, "\n", "\r") catch unreachable;
    defer gpa.free(inputCr);
    const htmlCr = try transform(gpa, inputCr, rules, renderers);
    defer gpa.free(htmlCr);
    const htmlCr2 = std.mem.replaceOwned(u8, gpa, htmlCr, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCr2);
    const htmlCr3 = std.mem.replaceOwned(u8, gpa, htmlCr2, "\r", "\n") catch unreachable;
    defer gpa.free(htmlCr3);
    try std.testing.expectEqualStrings(expected, htmlCr3);
}

test "tasklist with empty brackets" {
    const input =
        \\
        \\- [ ] 
        \\- [x] 
        \\
    ;
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
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);

    const inputCrLf = std.mem.replaceOwned(u8, gpa, input, "\n", "\r\n") catch unreachable;
    defer gpa.free(inputCrLf);
    const htmlCrLf = try transform(gpa, inputCrLf, rules, renderers);
    defer gpa.free(htmlCrLf);
    const htmlCrLf2 = std.mem.replaceOwned(u8, gpa, htmlCrLf, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCrLf2);
    try std.testing.expectEqualStrings(expected, htmlCrLf2);

    const inputCr = std.mem.replaceOwned(u8, gpa, input, "\n", "\r") catch unreachable;
    defer gpa.free(inputCr);
    const htmlCr = try transform(gpa, inputCr, rules, renderers);
    defer gpa.free(htmlCr);
    const htmlCr2 = std.mem.replaceOwned(u8, gpa, htmlCr, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCr2);
    const htmlCr3 = std.mem.replaceOwned(u8, gpa, htmlCr2, "\r", "\n") catch unreachable;
    defer gpa.free(htmlCr3);
    try std.testing.expectEqualStrings(expected, htmlCr3);
}

test "tasklist with uppercase X" {
    const input =
        \\
        \\- [ ] unchecked
        \\- [X] checked with uppercase
        \\
    ;
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
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);

    const inputCrLf = std.mem.replaceOwned(u8, gpa, input, "\n", "\r\n") catch unreachable;
    defer gpa.free(inputCrLf);
    const htmlCrLf = try transform(gpa, inputCrLf, rules, renderers);
    defer gpa.free(htmlCrLf);
    const htmlCrLf2 = std.mem.replaceOwned(u8, gpa, htmlCrLf, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCrLf2);
    try std.testing.expectEqualStrings(expected, htmlCrLf2);

    const inputCr = std.mem.replaceOwned(u8, gpa, input, "\n", "\r") catch unreachable;
    defer gpa.free(inputCr);
    const htmlCr = try transform(gpa, inputCr, rules, renderers);
    defer gpa.free(htmlCr);
    const htmlCr2 = std.mem.replaceOwned(u8, gpa, htmlCr, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCr2);
    const htmlCr3 = std.mem.replaceOwned(u8, gpa, htmlCr2, "\r", "\n") catch unreachable;
    defer gpa.free(htmlCr3);
    try std.testing.expectEqualStrings(expected, htmlCr3);
}

test "tasklist in blockquote" {
    const input =
        \\
        \\> - [ ] quoted task
        \\> - [x] checked quoted task
        \\
    ;
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
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);

    const inputCrLf = std.mem.replaceOwned(u8, gpa, input, "\n", "\r\n") catch unreachable;
    defer gpa.free(inputCrLf);
    const htmlCrLf = try transform(gpa, inputCrLf, rules, renderers);
    defer gpa.free(htmlCrLf);
    const htmlCrLf2 = std.mem.replaceOwned(u8, gpa, htmlCrLf, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCrLf2);
    try std.testing.expectEqualStrings(expected, htmlCrLf2);

    const inputCr = std.mem.replaceOwned(u8, gpa, input, "\n", "\r") catch unreachable;
    defer gpa.free(inputCr);
    const htmlCr = try transform(gpa, inputCr, rules, renderers);
    defer gpa.free(htmlCr);
    const htmlCr2 = std.mem.replaceOwned(u8, gpa, htmlCr, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCr2);
    const htmlCr3 = std.mem.replaceOwned(u8, gpa, htmlCr2, "\r", "\n") catch unreachable;
    defer gpa.free(htmlCr3);
    try std.testing.expectEqualStrings(expected, htmlCr3);
}

test "tasklist with multiple paragraphs" {
    const input =
        \\
        \\- [ ] task with paragraph
        \\
        \\  continuation paragraph
        \\- [x] another task
        \\
    ;
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
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);

    const inputCrLf = std.mem.replaceOwned(u8, gpa, input, "\n", "\r\n") catch unreachable;
    defer gpa.free(inputCrLf);
    const htmlCrLf = try transform(gpa, inputCrLf, rules, renderers);
    defer gpa.free(htmlCrLf);
    const htmlCrLf2 = std.mem.replaceOwned(u8, gpa, htmlCrLf, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCrLf2);
    try std.testing.expectEqualStrings(expected, htmlCrLf2);

    const inputCr = std.mem.replaceOwned(u8, gpa, input, "\n", "\r") catch unreachable;
    defer gpa.free(inputCr);
    const htmlCr = try transform(gpa, inputCr, rules, renderers);
    defer gpa.free(htmlCr);
    const htmlCr2 = std.mem.replaceOwned(u8, gpa, htmlCr, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCr2);
    const htmlCr3 = std.mem.replaceOwned(u8, gpa, htmlCr2, "\r", "\n") catch unreachable;
    defer gpa.free(htmlCr3);
    try std.testing.expectEqualStrings(expected, htmlCr3);
}

test "tasklist with sublist" {
    const input =
        \\
        \\- [ ] task with sublist
        \\  - subitem 1
        \\  - subitem 2
        \\- [x] checked task
        \\
    ;
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
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);

    const inputCrLf = std.mem.replaceOwned(u8, gpa, input, "\n", "\r\n") catch unreachable;
    defer gpa.free(inputCrLf);
    const htmlCrLf = try transform(gpa, inputCrLf, rules, renderers);
    defer gpa.free(htmlCrLf);
    const htmlCrLf2 = std.mem.replaceOwned(u8, gpa, htmlCrLf, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCrLf2);
    try std.testing.expectEqualStrings(expected, htmlCrLf2);

    const inputCr = std.mem.replaceOwned(u8, gpa, input, "\n", "\r") catch unreachable;
    defer gpa.free(inputCr);
    const htmlCr = try transform(gpa, inputCr, rules, renderers);
    defer gpa.free(htmlCr);
    const htmlCr2 = std.mem.replaceOwned(u8, gpa, htmlCr, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCr2);
    const htmlCr3 = std.mem.replaceOwned(u8, gpa, htmlCr2, "\r", "\n") catch unreachable;
    defer gpa.free(htmlCr3);
    try std.testing.expectEqualStrings(expected, htmlCr3);
}

test "tasklist with html entities" {
    const input =
        \\
        \\- [ ] task with &amp; entity
        \\- [x] task with &lt;HTML&gt;
        \\
    ;
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
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);

    const inputCrLf = std.mem.replaceOwned(u8, gpa, input, "\n", "\r\n") catch unreachable;
    defer gpa.free(inputCrLf);
    const htmlCrLf = try transform(gpa, inputCrLf, rules, renderers);
    defer gpa.free(htmlCrLf);
    const htmlCrLf2 = std.mem.replaceOwned(u8, gpa, htmlCrLf, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCrLf2);
    try std.testing.expectEqualStrings(expected, htmlCrLf2);

    const inputCr = std.mem.replaceOwned(u8, gpa, input, "\n", "\r") catch unreachable;
    defer gpa.free(inputCr);
    const htmlCr = try transform(gpa, inputCr, rules, renderers);
    defer gpa.free(htmlCr);
    const htmlCr2 = std.mem.replaceOwned(u8, gpa, htmlCr, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCr2);
    const htmlCr3 = std.mem.replaceOwned(u8, gpa, htmlCr2, "\r", "\n") catch unreachable;
    defer gpa.free(htmlCr3);
    try std.testing.expectEqualStrings(expected, htmlCr3);
}

test "tasklist with various whitespace" {
    const input =
        \\
        \\- [ ]one
        \\- [  ] two
        \\- [ x] three
        \\
    ;
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
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);

    const inputCrLf = std.mem.replaceOwned(u8, gpa, input, "\n", "\r\n") catch unreachable;
    defer gpa.free(inputCrLf);
    const htmlCrLf = try transform(gpa, inputCrLf, rules, renderers);
    defer gpa.free(htmlCrLf);
    const htmlCrLf2 = std.mem.replaceOwned(u8, gpa, htmlCrLf, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCrLf2);
    try std.testing.expectEqualStrings(expected, htmlCrLf2);

    const inputCr = std.mem.replaceOwned(u8, gpa, input, "\n", "\r") catch unreachable;
    defer gpa.free(inputCr);
    const htmlCr = try transform(gpa, inputCr, rules, renderers);
    defer gpa.free(htmlCr);
    const htmlCr2 = std.mem.replaceOwned(u8, gpa, htmlCr, "\r\n", "\n") catch unreachable;
    defer gpa.free(htmlCr2);
    const htmlCr3 = std.mem.replaceOwned(u8, gpa, htmlCr2, "\r", "\n") catch unreachable;
    defer gpa.free(htmlCr3);
    try std.testing.expectEqualStrings(expected, htmlCr3);
}
