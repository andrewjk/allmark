const std = @import("std");

const parse = @import("allmark").parse;
const render = @import("allmark").render;
const gfm = @import("allmark").gfm;

test "spec alert" {
    const input = "> [!NOTE]\n> Useful information that users should know, even when skimming content.";

    const expected =
        \\<div class="markdown-alert markdown-alert-note">
        \\<p class="markdown-alert-title">Note</p>
        \\<p>Useful information that users should know, even when skimming content.</p>
        \\</div>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "alert tip" {
    const input = "> [!TIP]\n> Helpful advice for doing things better or more easily.";

    const expected =
        \\<div class="markdown-alert markdown-alert-tip">
        \\<p class="markdown-alert-title">Tip</p>
        \\<p>Helpful advice for doing things better or more easily.</p>
        \\</div>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "alert important" {
    const input = "> [!IMPORTANT]\n> Key information users need to know to achieve their goal.";

    const expected =
        \\<div class="markdown-alert markdown-alert-important">
        \\<p class="markdown-alert-title">Important</p>
        \\<p>Key information users need to know to achieve their goal.</p>
        \\</div>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "alert warning" {
    const input = "> [!WARNING]\n> Urgent info that needs immediate user attention to avoid problems.";

    const expected =
        \\<div class="markdown-alert markdown-alert-warning">
        \\<p class="markdown-alert-title">Warning</p>
        \\<p>Urgent info that needs immediate user attention to avoid problems.</p>
        \\</div>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "alert caution" {
    const input = "> [!CAUTION]\n> Advises about risks or negative outcomes of certain actions.";

    const expected =
        \\<div class="markdown-alert markdown-alert-caution">
        \\<p class="markdown-alert-title">Caution</p>
        \\<p>Advises about risks or negative outcomes of certain actions.</p>
        \\</div>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "alert with multiple paragraphs" {
    const input = "> [!NOTE]\n> First paragraph of the note.\n>\n> Second paragraph of the note.";

    const expected =
        \\<div class="markdown-alert markdown-alert-note">
        \\<p class="markdown-alert-title">Note</p>
        \\<p>First paragraph of the note.</p>
        \\<p>Second paragraph of the note.</p>
        \\</div>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "alert with inline formatting" {
    const input = "> [!NOTE]\n> This is **bold** and this is *italic* and this is `code`.";

    const expected =
        \\<div class="markdown-alert markdown-alert-note">
        \\<p class="markdown-alert-title">Note</p>
        \\<p>This is <strong>bold</strong> and this is <em>italic</em> and this is <code>code</code>.</p>
        \\</div>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "alert with list" {
    const input = "> [!NOTE]\n> Some important points:\n> - First point\n> - Second point\n> - Third point";

    const expected =
        \\<div class="markdown-alert markdown-alert-note">
        \\<p class="markdown-alert-title">Note</p>
        \\<p>Some important points:</p>
        \\<ul>
        \\<li>First point</li>
        \\<li>Second point</li>
        \\<li>Third point</li>
        \\</ul>
        \\</div>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "alert with code block" {
    const input = "> [!NOTE]\n> Example code:\n>\n> code block here";

    const expected =
        \\<div class="markdown-alert markdown-alert-note">
        \\<p class="markdown-alert-title">Note</p>
        \\<p>Example code:</p>
        \\<p>code block here</p>
        \\</div>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "alert with link" {
    const input = "> [!NOTE]\n> Check out the [documentation](https://example.com) for more info.";

    const expected =
        \\<div class="markdown-alert markdown-alert-note">
        \\<p class="markdown-alert-title">Note</p>
        \\<p>Check out the <a href="https://example.com">documentation</a> for more info.</p>
        \\</div>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "alert case insensitive" {
    const input = "> [!note]\n> This should work with lowercase.";

    const expected =
        \\<div class="markdown-alert markdown-alert-note">
        \\<p class="markdown-alert-title">Note</p>
        \\<p>This should work with lowercase.</p>
        \\</div>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "non alert blockquote" {
    const input = "> This is just a regular blockquote.\n> It should not be treated as an alert.";

    const expected =
        \\<blockquote>
        \\<p>This is just a regular blockquote.
        \\It should not be treated as an alert.</p>
        \\</blockquote>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "blockquote with brackets but not alert" {
    const input = "> [NOTE] This is not an alert syntax.\n> It should be a regular blockquote.";

    const expected =
        \\<blockquote>
        \\<p>[NOTE] This is not an alert syntax.
        \\It should be a regular blockquote.</p>
        \\</blockquote>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "alert with nested blockquote" {
    const input = "> [!NOTE]\n> Outer alert content.\n>\n> > Nested blockquote inside alert.";

    const expected =
        \\<div class="markdown-alert markdown-alert-note">
        \\<p class="markdown-alert-title">Note</p>
        \\<p>Outer alert content.</p>
        \\<blockquote>
        \\<p>Nested blockquote inside alert.</p>
        \\</blockquote>
        \\</div>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "consecutive alerts" {
    const input = "> [!NOTE]\n> First alert.\n\n> [!WARNING]\n> Second alert.";

    const expected =
        \\<div class="markdown-alert markdown-alert-note">
        \\<p class="markdown-alert-title">Note</p>
        \\<p>First alert.</p>
        \\</div>
        \\<div class="markdown-alert markdown-alert-warning">
        \\<p class="markdown-alert-title">Warning</p>
        \\<p>Second alert.</p>
        \\</div>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}

test "alert with empty content" {
    const input = "> [!NOTE]\n>\n> Content after empty line.";

    const expected =
        \\<div class="markdown-alert markdown-alert-note">
        \\<p class="markdown-alert-title">Note</p>
        \\<p>Content after empty line.</p>
        \\</div>
        \\
    ;

    const gpa = std.testing.allocator;
    const rules = try gfm.init(gpa);
    defer gfm.deinit(&rules, gpa);

    const doc = try parse.execute(gpa, input, rules);
    defer doc.deinit(gpa);

    const html = try render(gpa, doc, null, false);
    defer gpa.free(html);

    try std.testing.expectEqualStrings(expected, html);
}
