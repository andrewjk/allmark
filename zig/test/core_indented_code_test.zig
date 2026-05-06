const std = @import("std");

const transform = @import("allmark").transform;
const core = @import("allmark").core;
const htmlRenderers = @import("allmark").htmlRenderers;

test "Simple 4-space indented code" {
    const input =
        \\
        \\    code here
        \\
    ;
    const expected =
        \\<pre><code>code here
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Tab-indented code" {
    const input =
        "\n\n\tcode here\n\n";
    const expected =
        \\<pre><code>code here
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Multi-line indented code" {
    const input =
        \\
        \\    line 1
        \\    line 2
        \\    line 3
        \\
    ;
    const expected =
        \\<pre><code>line 1
        \\line 2
        \\line 3
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Less than 4 spaces should be paragraph" {
    const input =
        \\
        \\   code here
        \\
    ;
    const expected =
        \\<p>code here</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "5-space indented code" {
    const input =
        \\
        \\     code here
        \\
    ;
    const expected =
        \\<pre><code> code here
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "8-space indented code" {
    const input =
        \\
        \\        code here
        \\
    ;
    const expected =
        \\<pre><code>    code here
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code block with blank line in middle" {
    const input =
        \\
        \\    line 1
        \\
        \\    line 2
        \\
    ;
    const expected =
        \\<pre><code>line 1
        \\
        \\line 2
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code block interrupts paragraph with blank line" {
    const input =
        \\
        \\Paragraph
        \\
        \\    code here
        \\
    ;
    const expected =
        \\<p>Paragraph</p>
        \\<pre><code>code here
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code block does not interrupt paragraph without blank line" {
    const input =
        \\
        \\Paragraph
        \\    code here
        \\
    ;
    const expected =
        \\<p>Paragraph
        \\code here</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code block with trailing spaces" {
    const input =
        \\
        \\    code here  
        \\
    ;
    const expected =
        \\<pre><code>code here  
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Mixed 4-space and 8-space indentation" {
    const input =
        \\
        \\    line 1
        \\        line 2
        \\
    ;
    const expected =
        \\<pre><code>line 1
        \\    line 2
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code with backticks" {
    const input =
        \\
        \\    `code`
        \\
    ;
    const expected =
        \\<pre><code>`code`
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code with tildes" {
    const input =
        \\
        \\    ~code~
        \\
    ;
    const expected =
        \\<pre><code>~code~
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code with asterisks" {
    const input =
        \\
        \\    **bold**
        \\
    ;
    const expected =
        \\<pre><code>**bold**
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code in blockquote" {
    const input =
        \\
        \\>     code here
        \\
    ;
    const expected =
        \\<blockquote>
        \\<pre><code>code here
        \\</code></pre>
        \\</blockquote>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code in list item" {
    const input =
        \\
        \\-     code here
        \\
    ;
    const expected =
        \\<ul>
        \\<li>
        \\<pre><code>code here
        \\</code></pre>
        \\</li>
        \\</ul>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code in ordered list" {
    const input =
        \\
        \\1.     code here
        \\
    ;
    const expected =
        \\<ol>
        \\<li>
        \\<pre><code>code here
        \\</code></pre>
        \\</li>
        \\</ol>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code followed by paragraph" {
    const input =
        \\
        \\    code here
        \\
        \\Paragraph
        \\
    ;
    const expected =
        \\<pre><code>code here
        \\</code></pre>
        \\<p>Paragraph</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Paragraph followed by indented code" {
    const input =
        \\
        \\Paragraph
        \\
        \\    code here
        \\
    ;
    const expected =
        \\<p>Paragraph</p>
        \\<pre><code>code here
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Multiple indented code blocks" {
    const input =
        \\
        \\    code 1
        \\
        \\    code 2
        \\
    ;
    const expected =
        \\<pre><code>code 1
        \\
        \\code 2
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code with special characters" {
    const input =
        \\
        \\    <>& "'\
        \\
    ;
    const expected =
        \\<pre><code>&lt;&gt;&amp; &quot;'\
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code with mixed indentation" {
    const input =
        \\
        \\    line 1
        \\      line 2
        \\  line 3
        \\
    ;
    const expected =
        \\<pre><code>line 1
        \\  line 2
        \\</code></pre>
        \\<p>line 3</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code block after heading" {
    const input =
        \\
        \\# Heading
        \\
        \\    code here
        \\
    ;
    const expected =
        \\<h1>Heading</h1>
        \\<pre><code>code here
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code block before heading" {
    const input =
        \\
        \\    code here
        \\
        \\# Heading
        \\
    ;
    const expected =
        \\<pre><code>code here
        \\</code></pre>
        \\<h1>Heading</h1>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code block after thematic break" {
    const input =
        \\
        \\---
        \\
        \\    code here
        \\
    ;
    const expected =
        \\<hr />
        \\<pre><code>code here
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code block before thematic break" {
    const input =
        \\
        \\    code here
        \\
        \\---
        \\
    ;
    const expected =
        \\<pre><code>code here
        \\</code></pre>
        \\<hr />
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code with fenced code block above" {
    const input =
        \\
        \\```
        \\ fenced code
        \\```
        \\    indented code
        \\
    ;
    const expected =
        \\<pre><code> fenced code
        \\</code></pre>
        \\<pre><code>indented code
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code with fenced code block below" {
    const input =
        \\
        \\    indented code
        \\```
        \\ fenced code
        \\```
        \\
    ;
    const expected =
        \\<pre><code>indented code
        \\</code></pre>
        \\<pre><code> fenced code
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code with ATX heading above" {
    const input =
        \\
        \\# Heading
        \\
        \\    code here
        \\
    ;
    const expected =
        \\<h1>Heading</h1>
        \\<pre><code>code here
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code with ATX heading below" {
    const input =
        \\
        \\    code here
        \\
        \\# Heading
        \\
    ;
    const expected =
        \\<pre><code>code here
        \\</code></pre>
        \\<h1>Heading</h1>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code with setext heading above" {
    const input =
        \\
        \\Heading
        \\=======
        \\
        \\    code here
        \\
    ;
    const expected =
        \\<h1>Heading</h1>
        \\<pre><code>code here
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code with setext heading below" {
    const input =
        \\
        \\    code here
        \\
        \\Heading
        \\=======
        \\
    ;
    const expected =
        \\<pre><code>code here
        \\</code></pre>
        \\<h1>Heading</h1>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code preceded by paragraph without blank line" {
    const input =
        \\
        \\Paragraph
        \\    code here
        \\
    ;
    const expected =
        \\<p>Paragraph
        \\code here</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Paragraph preceded by indented code without blank line" {
    const input =
        \\
        \\    code here
        \\Paragraph
        \\
    ;
    const expected =
        \\<pre><code>code here
        \\</code></pre>
        \\<p>Paragraph</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code with HTML entities" {
    const input =
        \\
        \\    &lt;code&gt;
        \\
    ;
    const expected =
        \\<pre><code>&amp;lt;code&amp;gt;
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code block in nested list" {
    const input =
        \\
        \\-     code 1
        \\-     code 2
        \\
    ;
    const expected =
        \\<ul>
        \\<li>
        \\<pre><code>code 1
        \\</code></pre>
        \\</li>
        \\<li>
        \\<pre><code>code 2
        \\</code></pre>
        \\</li>
        \\</ul>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code block at end of document" {
    const input =
        \\
        \\    code here
        \\
    ;
    const expected =
        \\<pre><code>code here
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code block with varying indentation" {
    const input =
        \\
        \\    level 1
        \\      level 2
        \\  level 3
        \\
    ;
    const expected =
        \\<pre><code>level 1
        \\  level 2
        \\</code></pre>
        \\<p>level 3</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Single tab indented" {
    const input =
        "\n\n\tcode here\n\n";
    const expected =
        \\<pre><code>code here
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Mixed tab and space indentation" {
    const input =
        "\n\n\t    code here\n\n";
    const expected =
        \\<pre><code>    code here
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "3 spaces - should be paragraph" {
    const input =
        \\
        \\   code here
        \\
    ;
    const expected =
        \\<p>code here</p>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "6 spaces indented code" {
    const input =
        \\
        \\      code here
        \\
    ;
    const expected =
        \\<pre><code>  code here
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "12 spaces indented code" {
    const input =
        \\
        \\            code here
        \\
    ;
    const expected =
        \\<pre><code>        code here
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Code block with unicode characters" {
    const input =
        \\
        \\    hello 世界
        \\
    ;
    const expected =
        \\<pre><code>hello 世界
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code with inline link" {
    const input =
        \\
        \\    [link](https://example.com)
        \\
    ;
    const expected =
        \\<pre><code>[link](https://example.com)
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code with inline image" {
    const input =
        \\
        \\    ![alt](image.png)
        \\
    ;
    const expected =
        \\<pre><code>![alt](image.png)
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code with emphasis" {
    const input =
        \\
        \\    *italic*
        \\
    ;
    const expected =
        \\<pre><code>*italic*
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code with strong" {
    const input =
        \\
        \\    **bold**
        \\
    ;
    const expected =
        \\<pre><code>**bold**
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code with inline code" {
    const input =
        \\
        \\    `inline code`
        \\
    ;
    const expected =
        \\<pre><code>`inline code`
        \\</code></pre>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Empty indented code block" {
    // Input: newline + 4 spaces + newline + 4 spaces (substring(1) removes first newline)
    const input =
        \\
        \\    
        \\    
        \\
    ;
    const expected = "";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}

test "Indented code block with only whitespace" {
    // Input: newline + 4 spaces + newline + 4 spaces + newline + 4 spaces (substring(1) removes first newline)
    const input =
        \\
        \\    
        \\    
        \\    
        \\
    ;
    const expected = "";

    const gpa = std.testing.allocator;
    const rules = try core.init(gpa);
    defer core.deinit(&rules, gpa);
    const renderers = try htmlRenderers.init(gpa);
    defer htmlRenderers.deinit(&renderers, gpa);

    const htmlSpaced = try transform(gpa, input, rules, renderers);
    defer gpa.free(htmlSpaced);
    try std.testing.expectEqualStrings(expected, htmlSpaced);

    const htmlTrimmed = try transform(gpa, input[1 .. input.len - 1], rules, renderers);
    defer gpa.free(htmlTrimmed);
    try std.testing.expectEqualStrings(expected, htmlTrimmed);
}
