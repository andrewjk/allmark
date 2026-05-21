const std = @import("std");
const zbench = @import("zbench");
const allmark = @import("allmark");

fn benchMarkdownToHtmlWithGfm(allocator: std.mem.Allocator) void {
    // Load the markdown file
    const markdown = @embedFile("full-markdown.md");

    // Initialize GFM rules
    const rules = allmark.gfm.init(allocator) catch @panic("Failed to init GFM rules");
    defer allmark.gfm.deinit(&rules, allocator);

    // Parse the markdown
    const doc = allmark.parse.execute(allocator, markdown, rules) catch @panic("Failed to parse markdown");
    defer doc.deinit(allocator);

    // Render to HTML
    const html = allmark.render(allocator, doc, null, false, null) catch @panic("Failed to render HTML");
    defer allocator.free(html);

    // Consume the result to prevent optimization
    std.mem.doNotOptimizeAway(html.ptr);
    std.mem.doNotOptimizeAway(html.len);
}

pub fn main(init: std.process.Init) !void {
    const allocator = init.arena.allocator();
    const io = init.io;

    var bench = zbench.Benchmark.init(allocator, .{
        .iterations = 100,
    });
    defer bench.deinit();

    try bench.add("Markdown to HTML with GFM", benchMarkdownToHtmlWithGfm, .{});
    try bench.run(io, std.Io.File.stdout());
}
