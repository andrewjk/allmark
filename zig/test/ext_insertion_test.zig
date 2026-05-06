const std = @import("std");

const transform = @import("allmark").transform;
const extended = @import("allmark").extended;
const htmlRenderers = @import("allmark").htmlRenderers;

test "insertion single" {
    const input =
        \\
        \\This text was {+inserted+} recently.
        \\
    ;
    const expected =
        \\<p>This text was <ins class="markdown-insertion">inserted</ins> recently.</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);
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

test "insertion double" {
    const input =
        \\
        \\This text was {++inserted++} recently.
        \\
    ;
    const expected =
        \\<p>This text was <ins class="markdown-insertion">inserted</ins> recently.</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);
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

test "insertion triple" {
    const input =
        \\
        \\This text was {+++inserted+++} recently.
        \\
    ;
    const expected =
        \\<p>This text was {+++inserted+++} recently.</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);
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

test "insertion single character" {
    const input =
        \\
        \\text {+a+} more
        \\
    ;
    const expected =
        \\<p>text <ins class="markdown-insertion">a</ins> more</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);
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

test "insertion with spaces" {
    const input =
        \\
        \\text {+with spaces+} more
        \\
    ;
    const expected =
        \\<p>text <ins class="markdown-insertion">with spaces</ins> more</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);
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

test "insertion at start of paragraph" {
    const input =
        \\
        \\{+inserted+} This is new.
        \\
    ;
    const expected =
        \\<p><ins class="markdown-insertion">inserted</ins> This is new.</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);
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

test "insertion at end of paragraph" {
    const input =
        \\
        \\This is {+inserted+}
        \\
    ;
    const expected =
        \\<p>This is <ins class="markdown-insertion">inserted</ins></p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);
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

test "insertion with punctuation" {
    const input =
        \\
        \\text {+word!+} more
        \\
    ;
    const expected =
        \\<p>text <ins class="markdown-insertion">word!</ins> more</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);
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

test "insertion with special characters" {
    const input =
        \\
        \\text {+a+b+} more
        \\
    ;
    const expected =
        \\<p>text <ins class="markdown-insertion">a+b</ins> more</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);
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

test "insertion adjacent to text" {
    const input =
        \\
        \\test{+ing+}test
        \\
    ;
    const expected =
        \\<p>test<ins class="markdown-insertion">ing</ins>test</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);
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

test "empty insertion" {
    const input =
        \\
        \\text{++}text
        \\
    ;
    const expected =
        \\<p>text{++}text</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);
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

test "insertion with markdown inside" {
    const input =
        \\
        \\text {+**bold**+}
        \\
    ;
    const expected =
        \\<p>text <ins class="markdown-insertion"><strong>bold</strong></ins></p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);
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

test "insertion with code inside" {
    const input =
        \\
        \\text {+`code`+}
        \\
    ;
    const expected =
        \\<p>text <ins class="markdown-insertion"><code>code</code></ins></p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);
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

test "escaped braces should not be insertion" {
    const input =
        \\
        \\text \{+not insertion\+}
        \\
    ;
    const expected =
        \\<p>text {+not insertion+}</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);
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

test "unmatched opening insertion" {
    const input =
        \\
        \\text {+not closed
        \\
    ;
    const expected =
        \\<p>text {+not closed</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);
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

test "unmatched closing insertion" {
    const input =
        \\
        \\text not opened+}
        \\
    ;
    const expected =
        \\<p>text not opened+}</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);
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

test "insertion in list item" {
    const input =
        \\
        \\- Item with {+insertion+}
        \\
    ;
    const expected =
        \\<ul>
        \\<li>Item with <ins class="markdown-insertion">insertion</ins></li>
        \\</ul>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);
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

test "insertion in blockquote" {
    const input =
        \\
        \\> Quote with {+insertion+}
        \\
    ;
    const expected =
        \\<blockquote>
        \\<p>Quote with <ins class="markdown-insertion">insertion</ins></p>
        \\</blockquote>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);
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

test "insertion with plus inside" {
    const input =
        \\
        \\text {+plus + inside+}
        \\
    ;
    const expected =
        \\<p>text <ins class="markdown-insertion">plus + inside</ins></p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);
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

test "insertion at beginning of document" {
    const input =
        \\
        \\{+Start+} of document.
        \\
    ;
    const expected =
        \\<p><ins class="markdown-insertion">Start</ins> of document.</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);
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

test "insertion at end of document" {
    const input =
        \\
        \\End of {+document+}
        \\
    ;
    const expected =
        \\<p>End of <ins class="markdown-insertion">document</ins></p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);
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

test "multiple insertions in one line" {
    const input =
        \\
        \\{+first+} and {+second+} and {+third+}
        \\
    ;
    const expected =
        \\<p><ins class="markdown-insertion">first</ins> and <ins class="markdown-insertion">second</ins> and <ins class="markdown-insertion">third</ins></p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);
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

test "insertion with starting emphasis" {
    const input =
        \\
        \\{+inserted *text+} that shouldn't be bold*
        \\
    ;
    const expected =
        \\<p><ins class="markdown-insertion">inserted *text</ins> that shouldn't be bold*</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);
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

test "insertion with ending emphasis" {
    const input =
        \\
        \\*this text should be {+inserted but not bold*+}
        \\
    ;
    const expected =
        \\<p>*this text should be <ins class="markdown-insertion">inserted but not bold*</ins></p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try extended.init(gpa);
    defer extended.deinit(&rules, gpa);
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
