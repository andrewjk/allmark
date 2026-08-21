const std = @import("std");

const transform = @import("allmark").transform;
const core = @import("allmark").core;
const htmlRenderers = @import("allmark").htmlRenderers;

test "basic inline link" {
    const input =
        \\
        \\[Google](https://google.com)
        \\
    ;
    const expected =
        \\<p><a href="https://google.com">Google</a></p>
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

test "link with title" {
    const input =
        \\
        \\[Google](https://google.com "Search Engine")
        \\
    ;
    const expected =
        \\<p><a href="https://google.com" title="Search Engine">Google</a></p>
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

test "link with single quoted title" {
    const input =
        \\
        \\[Google](https://google.com 'Search Engine')
        \\
    ;
    const expected =
        \\<p><a href="https://google.com" title="Search Engine">Google</a></p>
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

test "link in paragraph" {
    const input =
        \\
        \\Visit [Google](https://google.com) for search.
        \\
    ;
    const expected =
        \\<p>Visit <a href="https://google.com">Google</a> for search.</p>
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

test "multiple links in one line" {
    const input =
        \\
        \\[Google](https://google.com) and [GitHub](https://github.com)
        \\
    ;
    const expected =
        \\<p><a href="https://google.com">Google</a> and <a href="https://github.com">GitHub</a></p>
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

test "link with emphasis" {
    const input =
        \\
        \\[*Google*](https://google.com)
        \\
    ;
    const expected =
        \\<p><a href="https://google.com"><em>Google</em></a></p>
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

test "emphasis around link" {
    const input =
        \\
        \\*[Google](https://google.com)*
        \\
    ;
    const expected =
        \\<p><em><a href="https://google.com">Google</a></em></p>
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

test "link with code in text" {
    const input =
        \\
        \\[`const`](https://example.com)
        \\
    ;
    const expected =
        \\<p><a href="https://example.com"><code>const</code></a></p>
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

test "link in list item" {
    const input =
        \\
        \\- [Link](https://example.com)
        \\
    ;
    const expected =
        \\<ul>
        \\<li><a href="https://example.com">Link</a></li>
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

test "link in heading" {
    const input =
        \\
        \\# See [Google](https://google.com)
        \\
    ;
    const expected =
        \\<h1>See <a href="https://google.com">Google</a></h1>
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

test "reference link definition and usage" {
    const input =
        \\
        \\[Google][google]
        \\
        \\[google]: https://google.com
        \\
    ;
    const expected =
        \\<p><a href="https://google.com">Google</a></p>
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

test "reference link with implicit label" {
    const input =
        \\
        \\[Google][]
        \\
        \\[Google]: https://google.com
        \\
    ;
    const expected =
        \\<p><a href="https://google.com">Google</a></p>
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

test "reference link with title" {
    const input =
        \\
        \\[Google][google]
        \\
        \\[google]: https://google.com "Search Engine"
        \\
    ;
    const expected =
        \\<p><a href="https://google.com" title="Search Engine">Google</a></p>
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

test "multiple reference links" {
    const input =
        \\
        \\[Google][google] and [GitHub][github]
        \\
        \\[google]: https://google.com
        \\[github]: https://github.com
        \\
    ;
    const expected =
        \\<p><a href="https://google.com">Google</a> and <a href="https://github.com">GitHub</a></p>
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

test "autolink with http" {
    const input =
        \\
        \\<http://example.com>
        \\
    ;
    const expected =
        \\<p><a href="http://example.com">http://example.com</a></p>
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

test "autolink with https" {
    const input =
        \\
        \\<https://example.com>
        \\
    ;
    const expected =
        \\<p><a href="https://example.com">https://example.com</a></p>
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

test "autolink with ftp" {
    const input =
        \\
        \\<ftp://example.com>
        \\
    ;
    const expected =
        \\<p><a href="ftp://example.com">ftp://example.com</a></p>
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

test "email autolink" {
    const input =
        \\
        \\<user@example.com>
        \\
    ;
    const expected =
        \\<p><a href="mailto:user@example.com">user@example.com</a></p>
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

test "link with special characters in URL" {
    const input =
        \\
        \\[Link](https://example.com/path?query=value&other=123#anchor)
        \\
    ;
    const expected =
        \\<p><a href="https://example.com/path?query=value&amp;other=123#anchor">Link</a></p>
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

test "link with parentheses in URL" {
    const input =
        \\
        \\[Link](https://example.com/path(with)parentheses)
        \\
    ;
    const expected =
        \\<p><a href="https://example.com/path(with)parentheses">Link</a></p>
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

test "link with spaces in title" {
    const input =
        \\
        \\[Link](https://example.com "This is a title")
        \\
    ;
    const expected =
        \\<p><a href="https://example.com" title="This is a title">Link</a></p>
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

test "link with escaped brackets in text" {
    const input =
        \\
        \\[[link]](https://example.com)
        \\
    ;
    const expected =
        \\<p><a href="https://example.com">[link]</a></p>
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

test "empty link text" {
    const input =
        \\
        \\[](https://example.com)
        \\
    ;
    const expected =
        \\<p><a href="https://example.com"></a></p>
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

test "link with underscore in URL" {
    const input =
        \\
        \\[Link](https://example.com/path_with_underscore)
        \\
    ;
    const expected =
        \\<p><a href="https://example.com/path_with_underscore">Link</a></p>
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

test "relative URL" {
    const input =
        \\
        \\[Link](/path/to/page)
        \\
    ;
    const expected =
        \\<p><a href="/path/to/page">Link</a></p>
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

test "link with percent encoding" {
    const input =
        \\
        \\[Link](https://example.com/path%20with%20spaces)
        \\
    ;
    const expected =
        \\<p><a href="https://example.com/path%20with%20spaces">Link</a></p>
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
