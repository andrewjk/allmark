const std = @import("std");

const transform = @import("allmark").transform;
const gfm = @import("allmark").gfm;
const htmlRenderers = @import("allmark").htmlRenderers;

test "spec footnote" {
    const input =
        \\
        \\Here is a simple footnote[^1].
        \\
        \\A footnote can also have multiple lines[^2].
        \\
        \\[^1]: My reference.
        \\[^2]: To add line breaks within a footnote, add 2 spaces to the end of a line.  
        \\This is a second line.
        \\
    ;
    const expected =
        \\<p>Here is a simple footnote<sup class="footnote-ref"><a href="#fn1" id="fnref1">1</a></sup>.</p>
        \\<p>A footnote can also have multiple lines<sup class="footnote-ref"><a href="#fn2" id="fnref2">2</a></sup>.</p>
        \\<section class="footnotes">
        \\<ol>
        \\<li id="fn1">
        \\<p>My reference. <a href="#fnref1" class="footnote-backref">↩</a></p>
        \\</li>
        \\<li id="fn2">
        \\<p>To add line breaks within a footnote, add 2 spaces to the end of a line.<br />
        \\This is a second line. <a href="#fnref2" class="footnote-backref">↩</a></p>
        \\</li>
        \\</ol>
        \\</section>
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

test "simple footnote reference" {
    const input =
        \\
        \\Text with a footnote[^1].
        \\
        \\[^1]: This is the footnote content.
        \\
    ;
    const expected =
        \\<p>Text with a footnote<sup class="footnote-ref"><a href="#fn1" id="fnref1">1</a></sup>.</p>
        \\<section class="footnotes">
        \\<ol>
        \\<li id="fn1">
        \\<p>This is the footnote content. <a href="#fnref1" class="footnote-backref">↩</a></p>
        \\</li>
        \\</ol>
        \\</section>
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

test "multiple footnote references" {
    const input =
        \\
        \\First reference[^1] and second[^2].
        \\
        \\[^1]: First footnote.
        \\[^2]: Second footnote.
        \\
    ;
    const expected =
        \\<p>First reference<sup class="footnote-ref"><a href="#fn1" id="fnref1">1</a></sup> and second<sup class="footnote-ref"><a href="#fn2" id="fnref2">2</a></sup>.</p>
        \\<section class="footnotes">
        \\<ol>
        \\<li id="fn1">
        \\<p>First footnote. <a href="#fnref1" class="footnote-backref">↩</a></p>
        \\</li>
        \\<li id="fn2">
        \\<p>Second footnote. <a href="#fnref2" class="footnote-backref">↩</a></p>
        \\</li>
        \\</ol>
        \\</section>
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

test "footnote with inline formatting" {
    const input =
        \\
        \\Text[^1].
        \\
        \\[^1]: Footnote with **bold** and *italic* text.
        \\
    ;
    const expected =
        \\<p>Text<sup class="footnote-ref"><a href="#fn1" id="fnref1">1</a></sup>.</p>
        \\<section class="footnotes">
        \\<ol>
        \\<li id="fn1">
        \\<p>Footnote with <strong>bold</strong> and <em>italic</em> text. <a href="#fnref1" class="footnote-backref">↩</a></p>
        \\</li>
        \\</ol>
        \\</section>
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

test "footnote with code" {
    const input =
        \\
        \\Code reference[^1].
        \\
        \\[^1]: Footnote with `inline code`.
        \\
    ;
    const expected =
        \\<p>Code reference<sup class="footnote-ref"><a href="#fn1" id="fnref1">1</a></sup>.</p>
        \\<section class="footnotes">
        \\<ol>
        \\<li id="fn1">
        \\<p>Footnote with <code>inline code</code>. <a href="#fnref1" class="footnote-backref">↩</a></p>
        \\</li>
        \\</ol>
        \\</section>
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

test "footnote with link" {
    const input =
        \\
        \\Link reference[^1].
        \\
        \\[^1]: See [example](http://example.com).
        \\
    ;
    const expected =
        \\<p>Link reference<sup class="footnote-ref"><a href="#fn1" id="fnref1">1</a></sup>.</p>
        \\<section class="footnotes">
        \\<ol>
        \\<li id="fn1">
        \\<p>See <a href="http://example.com">example</a>. <a href="#fnref1" class="footnote-backref">↩</a></p>
        \\</li>
        \\</ol>
        \\</section>
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

test "footnote reference not at definition" {
    const input =
        \\
        \\Unknown footnote[^99].
        \\
    ;
    const expected =
        \\<p>Unknown footnote[^99].</p>
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

test "footnote with multiline content" {
    const input =
        \\
        \\Multiline[^1].
        \\
        \\[^1]: First line
        \\    Second line
        \\    Third line
        \\
    ;
    const expected =
        \\<p>Multiline<sup class="footnote-ref"><a href="#fn1" id="fnref1">1</a></sup>.</p>
        \\<section class="footnotes">
        \\<ol>
        \\<li id="fn1">
        \\<p>First line
        \\Second line
        \\Third line <a href="#fnref1" class="footnote-backref">↩</a></p>
        \\</li>
        \\</ol>
        \\</section>
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

test "repeated footnote reference" {
    const input =
        \\
        \\First[^1] and second[^1] use same footnote.
        \\
        \\[^1]: Shared footnote content.
        \\
    ;
    const expected =
        \\<p>First<sup class="footnote-ref"><a href="#fn1" id="fnref1">1</a></sup> and second<sup class="footnote-ref"><a href="#fn1" id="fnref1">1</a></sup> use same footnote.</p>
        \\<section class="footnotes">
        \\<ol>
        \\<li id="fn1">
        \\<p>Shared footnote content. <a href="#fnref1" class="footnote-backref">↩</a></p>
        \\</li>
        \\</ol>
        \\</section>
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

test "footnote in list" {
    const input =
        \\
        \\- Item with footnote[^1]
        \\- Another item[^2]
        \\
        \\[^1]: First footnote.
        \\[^2]: Second footnote.
        \\
    ;
    const expected =
        \\<ul>
        \\<li>Item with footnote<sup class="footnote-ref"><a href="#fn1" id="fnref1">1</a></sup></li>
        \\<li>Another item<sup class="footnote-ref"><a href="#fn2" id="fnref2">2</a></sup></li>
        \\</ul>
        \\<section class="footnotes">
        \\<ol>
        \\<li id="fn1">
        \\<p>First footnote. <a href="#fnref1" class="footnote-backref">↩</a></p>
        \\</li>
        \\<li id="fn2">
        \\<p>Second footnote. <a href="#fnref2" class="footnote-backref">↩</a></p>
        \\</li>
        \\</ol>
        \\</section>
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

test "footnote in blockquote" {
    const input =
        \\
        \\> Quoted text with footnote[^1]
        \\
        \\[^1]: Footnote for quote.
        \\
    ;
    const expected =
        \\<blockquote>
        \\<p>Quoted text with footnote<sup class="footnote-ref"><a href="#fn1" id="fnref1">1</a></sup></p>
        \\</blockquote>
        \\<section class="footnotes">
        \\<ol>
        \\<li id="fn1">
        \\<p>Footnote for quote. <a href="#fnref1" class="footnote-backref">↩</a></p>
        \\</li>
        \\</ol>
        \\</section>
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

test "footnote with special characters in label" {
    const input =
        \\
        \\Special label[^a-b_c].
        \\
        \\[^a-b_c]: Footnote with special label.
        \\
    ;
    const expected =
        \\<p>Special label[^a-b_c].</p>
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

test "case insensitive footnote labels" {
    const input =
        \\
        \\Mixed case[^ABC].
        \\
        \\[^abc]: Should match.
        \\
    ;
    const expected =
        \\<p>Mixed case<sup class="footnote-ref"><a href="#fn1" id="fnref1">1</a></sup>.</p>
        \\<section class="footnotes">
        \\<ol>
        \\<li id="fn1">
        \\<p>Should match. <a href="#fnref1" class="footnote-backref">↩</a></p>
        \\</li>
        \\</ol>
        \\</section>
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

test "footnote then list" {
    const input =
        \\
        \\Text[^1]
        \\
        \\[^1]: Here is the content  
        \\- and here is a list
        \\
    ;
    const expected =
        \\<p>Text<sup class="footnote-ref"><a href="#fn1" id="fnref1">1</a></sup></p>
        \\<ul>
        \\<li>and here is a list</li>
        \\</ul>
        \\<section class="footnotes">
        \\<ol>
        \\<li id="fn1">
        \\<p>Here is the content <a href="#fnref1" class="footnote-backref">↩</a></p>
        \\</li>
        \\</ol>
        \\</section>
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

test "title after footnote label" {
    const input =
        \\
        \\Text[^1]
        \\
        \\[^1]: https://example.com test
        \\
    ;
    const expected =
        \\<p>Text<sup class="footnote-ref"><a href="#fn1" id="fnref1">1</a></sup></p>
        \\<section class="footnotes">
        \\<ol>
        \\<li id="fn1">
        \\<p><a href="https://example.com">https://example.com</a> test <a href="#fnref1" class="footnote-backref">↩</a></p>
        \\</li>
        \\</ol>
        \\</section>
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

test "link then footnote" {
    const input =
        \\
        \\Text[^1] [foo]
        \\
        \\[foo]: https://example.com/foo
        \\[^1]: https://example.com/1 test
        \\
    ;
    const expected =
        \\<p>Text<sup class="footnote-ref"><a href="#fn1" id="fnref1">1</a></sup> <a href="https://example.com/foo">foo</a></p>
        \\<section class="footnotes">
        \\<ol>
        \\<li id="fn1">
        \\<p><a href="https://example.com/1">https://example.com/1</a> test <a href="#fnref1" class="footnote-backref">↩</a></p>
        \\</li>
        \\</ol>
        \\</section>
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

test "footnote then link" {
    const input =
        \\
        \\Text[^1] [foo]
        \\
        \\[^1]: https://example.com/1 test
        \\[foo]: https://example.com/foo
        \\
    ;
    const expected =
        \\<p>Text<sup class="footnote-ref"><a href="#fn1" id="fnref1">1</a></sup> [foo]</p>
        \\<section class="footnotes">
        \\<ol>
        \\<li id="fn1">
        \\<p><a href="https://example.com/1">https://example.com/1</a> test
        \\[foo]: <a href="https://example.com/foo">https://example.com/foo</a> <a href="#fnref1" class="footnote-backref">↩</a></p>
        \\</li>
        \\</ol>
        \\</section>
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

test "swallow following brackets" {
    const input =
        \\
        \\[^1][asd]f]
        \\
        \\[^1]: /footnote
        \\
    ;
    const expected =
        \\<p><sup class="footnote-ref"><a href="#fn1" id="fnref1">1</a></sup>f]</p>
        \\<section class="footnotes">
        \\<ol>
        \\<li id="fn1">
        \\<p>/footnote <a href="#fnref1" class="footnote-backref">↩</a></p>
        \\</li>
        \\</ol>
        \\</section>
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

test "link reference takes precedence" {
    const input =
        \\
        \\[^1][foo]
        \\
        \\[^1]: /footnote
        \\
        \\[foo]: /url
        \\
        \\
    ;
    const expected =
        \\<p><a href="/url">^1</a></p>
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

test "multiple paragraphs" {
    const input =
        \\
        \\Footnote 1 link[^first].
        \\
        \\[^first]: Footnote **can have markup**
        \\
        \\    and multiple paragraphs.
        \\
    ;
    const expected =
        \\<p>Footnote 1 link<sup class="footnote-ref"><a href="#fn1" id="fnref1">1</a></sup>.</p>
        \\<section class="footnotes">
        \\<ol>
        \\<li id="fn1">
        \\<p>Footnote <strong>can have markup</strong></p>
        \\<p>and multiple paragraphs. <a href="#fnref1" class="footnote-backref">↩</a></p>
        \\</li>
        \\</ol>
        \\</section>
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
