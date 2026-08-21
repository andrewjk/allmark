const std = @import("std");

const transform = @import("allmark").transform;
const core = @import("allmark").core;
const htmlRenderers = @import("allmark").htmlRenderers;

test "ATX heading level 1" {
    const input =
        \\
        \\# Heading 1
        \\
    ;
    const expected =
        \\<h1>Heading 1</h1>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
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

test "ATX heading level 2" {
    const input =
        \\
        \\## Heading 2
        \\
    ;
    const expected =
        \\<h2>Heading 2</h2>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
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

test "ATX heading level 3" {
    const input =
        \\
        \\### Heading 3
        \\
    ;
    const expected =
        \\<h3>Heading 3</h3>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
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

test "ATX heading level 4" {
    const input =
        \\
        \\#### Heading 4
        \\
    ;
    const expected =
        \\<h4>Heading 4</h4>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
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

test "ATX heading level 5" {
    const input =
        \\
        \\##### Heading 5
        \\
    ;
    const expected =
        \\<h5>Heading 5</h5>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
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

test "ATX heading level 6" {
    const input =
        \\
        \\###### Heading 6
        \\
    ;
    const expected =
        \\<h6>Heading 6</h6>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
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

test "ATX heading with closing sequence" {
    const input =
        \\
        \\# Heading 1 #
        \\
    ;
    const expected =
        \\<h1>Heading 1</h1>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
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

test "ATX heading with multiple closing hashes" {
    const input =
        \\
        \\## Heading 2 ###
        \\
    ;
    const expected =
        \\<h2>Heading 2</h2>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
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

test "ATX heading with closing hashes and spaces" {
    const input =
        \\
        \\# Heading 1 #  
        \\
    ;
    const expected =
        \\<h1>Heading 1</h1>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
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

test "ATX heading with inline emphasis" {
    const input =
        \\
        \\# *Heading* with **emphasis**
        \\
    ;
    const expected =
        \\<h1><em>Heading</em> with <strong>emphasis</strong></h1>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
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

test "ATX heading with inline code" {
    const input =
        \\
        \\# Heading with `code`
        \\
    ;
    const expected =
        \\<h1>Heading with <code>code</code></h1>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
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

test "ATX heading with link" {
    const input =
        \\
        \\# Heading with [link](https://example.com)
        \\
    ;
    const expected =
        \\<h1>Heading with <a href="https://example.com">link</a></h1>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
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

test "Setext heading level 1 with =" {
    const input =
        \\
        \\Heading 1
        \\========
        \\
    ;
    const expected =
        \\<h1>Heading 1</h1>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
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

test "Setext heading level 2 with -" {
    const input =
        \\
        \\Heading 2
        \\--------
        \\
    ;
    const expected =
        \\<h2>Heading 2</h2>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
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

test "Setext heading with multiline content" {
    const input =
        \\
        \\Heading 1
        \\line 2
        \\========
        \\
    ;
    const expected =
        \\<h1>Heading 1
        \\line 2</h1>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
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

test "Setext heading with inline formatting" {
    const input =
        \\
        \\*Heading* 1
        \\========
        \\
    ;
    const expected =
        \\<h1><em>Heading</em> 1</h1>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
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

test "ATX heading with 3 space indent" {
    const input =
        \\
        \\   # Heading 1
        \\
    ;
    const expected =
        \\<h1>Heading 1</h1>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
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

test "ATX heading with 4 space indent should be code" {
    const input =
        \\
        \\    # Heading 1
        \\
    ;
    const expected =
        \\<pre><code># Heading 1
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
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

test "ATX heading without space after # is paragraph" {
    const input =
        \\
        \\#Not a heading
        \\
    ;
    const expected =
        \\<p>#Not a heading</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
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

test "ATX heading with 7 # characters is paragraph" {
    const input =
        \\
        \\####### Not a heading
        \\
    ;
    const expected =
        \\<p>####### Not a heading</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
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

test "ATX heading with empty content" {
    const input =
        \\
        \\# 
        \\
    ;
    const expected =
        \\<h1></h1>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
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

test "ATX heading with only # and closing #" {
    const input =
        \\
        \\## #
        \\
    ;
    const expected =
        \\<h2></h2>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
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

test "Setext heading requires paragraph content" {
    const input =
        \\
        \\- Not a heading
        \\========
        \\
    ;
    const expected =
        \\<ul>
        \\<li>Not a heading
        \\========</li>
        \\</ul>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
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

test "ATX heading escapes closing # with backslash" {
    const input =
        "\n" ++
        "# Heading with \\# escaped\n";
    const expected =
        "<h1>Heading with # escaped</h1>\n";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
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

test "ATX heading at end of document" {
    const input =
        \\
        \\# Last heading
        \\
    ;
    const expected =
        \\<h1>Last heading</h1>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
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

test "Multiple ATX headings" {
    const input =
        \\
        \\# Heading 1
        \\## Heading 2
        \\### Heading 3
        \\
    ;
    const expected =
        \\<h1>Heading 1</h1>
        \\<h2>Heading 2</h2>
        \\<h3>Heading 3</h3>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
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

test "Multiple Setext headings" {
    const input =
        \\
        \\Heading 1
        \\========
        \\
        \\Heading 2
        \\--------
        \\
    ;
    const expected =
        \\<h1>Heading 1</h1>
        \\<h2>Heading 2</h2>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
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

test "ATX heading preceded by paragraph without blank line" {
    const input =
        \\
        \\Paragraph
        \\# Heading
        \\
    ;
    const expected =
        \\<p>Paragraph</p>
        \\<h1>Heading</h1>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
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

test "ATX heading with mixed inline elements" {
    const input =
        \\
        \\# **Bold** text, *italic* text, `code`, and [link](https://example.com)
        \\
    ;
    const expected =
        \\<h1><strong>Bold</strong> text, <em>italic</em> text, <code>code</code>, and <a href="https://example.com">link</a></h1>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
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
