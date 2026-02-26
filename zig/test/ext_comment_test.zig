const std = @import("std");

const parse = @import("allmark").parse;
const render = @import("allmark").render;
const extended = @import("allmark").extended;

test "comment basic" {
    const input =
        \\This text was {>>commented<<} recently.
        \\  
    ;

    const expected =
        \\<p>This text was <span class="markdown-comment">commented</span> recently.</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "comment single character" {
    const input = "text {>>a<<} more";

    const expected =
        \\<p>text <span class="markdown-comment">a</span> more</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "comment with spaces" {
    const input = "text {>>with spaces<<} more";

    const expected =
        \\<p>text <span class="markdown-comment">with spaces</span> more</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "comment at start of paragraph" {
    const input = "{>>commented<<} This is new.";

    const expected =
        \\<p><span class="markdown-comment">commented</span> This is new.</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "comment at end of paragraph" {
    const input = "This is {>>commented<<}";

    const expected =
        \\<p>This is <span class="markdown-comment">commented</span></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "comment with punctuation" {
    const input = "text {>>word!<<} more";

    const expected =
        \\<p>text <span class="markdown-comment">word!</span> more</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "comment with special characters" {
    const input = "text {>>a-b<<} more";

    const expected =
        \\<p>text <span class="markdown-comment">a-b</span> more</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "comment adjacent to text" {
    const input = "test{>>ing<<}test";

    const expected =
        \\<p>test<span class="markdown-comment">ing</span>test</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "empty comment" {
    const input = "text{>><<}text";

    const expected =
        \\<p>text{&gt;&gt;&lt;&lt;}text</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "comment with markdown inside" {
    const input = "text {>>**bold**<<}";

    const expected =
        \\<p>text <span class="markdown-comment"><strong>bold</strong></span></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "comment with code inside" {
    const input = "text {>>`code`<<}";

    const expected =
        \\<p>text <span class="markdown-comment"><code>code</code></span></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "escaped braces should not be comment" {
    const input = "text \\{>>not comment<<\\}";

    const expected =
        \\<p>text {&gt;&gt;not comment&lt;&lt;}</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "unmatched opening comment" {
    const input = "text {>>not closed";

    const expected =
        \\<p>text {&gt;&gt;not closed</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "unmatched closing comment" {
    const input = "text not opened<<}";

    const expected =
        \\<p>text not opened&lt;&lt;}</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "comment in list item" {
    const input = "- Item with {>>comment<<}";

    const expected =
        \\<ul>
        \\<li>Item with <span class="markdown-comment">comment</span></li>
        \\</ul>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "comment in blockquote" {
    const input = "> Quote with {>>comment<<}";

    const expected =
        \\<blockquote>
        \\<p>Quote with <span class="markdown-comment">comment</span></p>
        \\</blockquote>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "comment with angle brackets inside" {
    const input = "text {>>some <text> inside<<}";

    const expected =
        \\<p>text <span class="markdown-comment">some <text> inside</span></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "comment at beginning of document" {
    const input = "{>>Start<<} of document.";

    const expected =
        \\<p><span class="markdown-comment">Start</span> of document.</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "comment at end of document" {
    const input = "End of {>>document<<}";

    const expected =
        \\<p>End of <span class="markdown-comment">document</span></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "multiple comments in one line" {
    const input = "{>>first<<} and {>>second<<} and {>>third<<}";

    const expected =
        \\<p><span class="markdown-comment">first</span> and <span class="markdown-comment">second</span> and <span class="markdown-comment">third</span></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "comment with starting emphasis" {
    const input = "{>>comment *text<<} that shouldn't be bold*";

    const expected =
        \\<p><span class="markdown-comment">comment *text</span> that shouldn't be bold*</p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "comment with ending emphasis" {
    const input = "*this text should be {>>commented but not bold*<<}";

    const expected =
        \\<p>*this text should be <span class="markdown-comment">commented but not bold*</span></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "comment with plus signs inside" {
    const input = "text {>>plus + sign<<}";

    const expected =
        \\<p>text <span class="markdown-comment">plus + sign</span></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "comment with minus signs inside" {
    const input = "text {>>minus - sign<<}";

    const expected =
        \\<p>text <span class="markdown-comment">minus - sign</span></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "comment nested with other critic marks" {
    const input = "text {+insertion {>>comment<<} end+}";

    const expected =
        \\<p>text <ins class="markdown-insertion">insertion <span class="markdown-comment">comment</span> end</ins></p>
        \\
    ;

    const gpa = std.testing.allocator;
    var rules = try extended.init(gpa);
    defer rules.blocks.deinit();
    defer rules.inlines.deinit();

    const root = try parse.execute(gpa, input, rules, null);
    defer root.deinit(gpa);

    const html = try render.renderHtml(gpa, root, null);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}
