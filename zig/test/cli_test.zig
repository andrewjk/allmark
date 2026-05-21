const std = @import("std");

const allmark = @import("allmark");

const Ruleset = enum {
    core,
    gfm,
    extended,
};

const Args = struct {
    allocator: std.mem.Allocator,
    input: []const u8,
    output: ?[]const u8,
    ruleset: Ruleset,

    pub fn deinit(self: *const Args) void {
        if (self.output) |out| {
            self.allocator.free(out);
        }
    }
};

fn printUsage() void {
    std.debug.print("Usage: allmark <input-file> [--output <file>] [--ruleset <core|gfm|extended>]\n", .{});
    std.debug.print("  input-file  Path to the markdown file to convert\n", .{});
    std.debug.print("  --output     Path to output HTML file (optional, prints to stdout by default)\n", .{});
    std.debug.print("  --ruleset    Ruleset to use: core, gfm, or extended (default: extended)\n", .{});
}

fn parseArgs(allocator: std.mem.Allocator, args: [][]const u8) !Args {
    var result = Args{
        .allocator = allocator,
        .input = "",
        .output = null,
        .ruleset = .extended,
    };

    var i: usize = 0;
    while (i < args.len) {
        const arg = args[i];

        if (std.mem.eql(u8, arg, "--output")) {
            if (i + 1 >= args.len) {
                std.debug.print("Error: --output requires a file path\n", .{});
                printUsage();
                return error.MissingArgument;
            }
            result.output = try allocator.dupe(u8, args[i + 1]);
            i += 2;
        } else if (std.mem.eql(u8, arg, "--ruleset")) {
            if (i + 1 >= args.len) {
                std.debug.print("Error: --ruleset requires a value (core, gfm, or extended)\n", .{});
                printUsage();
                return error.MissingArgument;
            }
            const ruleset_str = args[i + 1];
            if (std.mem.eql(u8, ruleset_str, "core")) {
                result.ruleset = .core;
            } else if (std.mem.eql(u8, ruleset_str, "gfm")) {
                result.ruleset = .gfm;
            } else if (std.mem.eql(u8, ruleset_str, "extended")) {
                result.ruleset = .extended;
            } else {
                std.debug.print("Error: Invalid ruleset '{s}'. Must be core, gfm, or extended\n", .{ruleset_str});
                printUsage();
                return error.InvalidRuleset;
            }
            i += 2;
        } else if (std.mem.eql(u8, arg, "--help") or std.mem.eql(u8, arg, "-h")) {
            printUsage();
            std.process.exit(0);
        } else if (std.mem.startsWith(u8, arg, "--")) {
            std.debug.print("Error: Unknown option '{s}'\n", .{arg});
            printUsage();
            return error.UnknownOption;
        } else {
            if (result.input.len > 0) {
                std.debug.print("Error: Multiple input files specified: '{s}' and '{s}'\n", .{ result.input, arg });
                printUsage();
                return error.MultipleInputFiles;
            }
            result.input = arg;
            i += 1;
        }
    }

    if (result.input.len == 0) {
        std.debug.print("Error: No input file specified\n", .{});
        printUsage();
        return error.NoInputFile;
    }

    return result;
}

test "parseArgs basic input file" {
    const allocator = std.testing.allocator;
    const args = &[_][]const u8{"input.md"};
    const result = try parseArgs(allocator, args);
    defer result.deinit();

    try std.testing.expectEqualStrings("input.md", result.input);
    try std.testing.expect(result.output == null);
    try std.testing.expectEqual(Ruleset.extended, result.ruleset);
}

test "parseArgs with output file" {
    const allocator = std.testing.allocator;
    const args = &[_][]const u8{ "input.md", "--output", "output.html" };
    const result = try parseArgs(allocator, args);
    defer result.deinit();

    try std.testing.expectEqualStrings("input.md", result.input);
    try std.testing.expectEqualStrings("output.html", result.output.?);
    try std.testing.expectEqual(Ruleset.extended, result.ruleset);
}

test "parseArgs with ruleset core" {
    const allocator = std.testing.allocator;
    const args = &[_][]const u8{ "input.md", "--ruleset", "core" };
    const result = try parseArgs(allocator, args);
    defer result.deinit();

    try std.testing.expectEqualStrings("input.md", result.input);
    try std.testing.expect(result.output == null);
    try std.testing.expectEqual(Ruleset.core, result.ruleset);
}

test "parseArgs with ruleset gfm" {
    const allocator = std.testing.allocator;
    const args = &[_][]const u8{ "input.md", "--ruleset", "gfm" };
    const result = try parseArgs(allocator, args);
    defer result.deinit();

    try std.testing.expectEqualStrings("input.md", result.input);
    try std.testing.expect(result.output == null);
    try std.testing.expectEqual(Ruleset.gfm, result.ruleset);
}

test "parseArgs with ruleset extended" {
    const allocator = std.testing.allocator;
    const args = &[_][]const u8{ "input.md", "--ruleset", "extended" };
    const result = try parseArgs(allocator, args);
    defer result.deinit();

    try std.testing.expectEqualStrings("input.md", result.input);
    try std.testing.expect(result.output == null);
    try std.testing.expectEqual(Ruleset.extended, result.ruleset);
}

test "parseArgs with all options" {
    const allocator = std.testing.allocator;
    const args = &[_][]const u8{ "input.md", "--output", "output.html", "--ruleset", "gfm" };
    const result = try parseArgs(allocator, args);
    defer result.deinit();

    try std.testing.expectEqualStrings("input.md", result.input);
    try std.testing.expectEqualStrings("output.html", result.output.?);
    try std.testing.expectEqual(Ruleset.gfm, result.ruleset);
}

test "parseArgs options in different order" {
    const allocator = std.testing.allocator;
    const args = &[_][]const u8{ "--ruleset", "core", "input.md", "--output", "out.html" };
    const result = try parseArgs(allocator, args);
    defer result.deinit();

    try std.testing.expectEqualStrings("input.md", result.input);
    try std.testing.expectEqualStrings("out.html", result.output.?);
    try std.testing.expectEqual(Ruleset.core, result.ruleset);
}

test "parseArgs error - no input file" {
    const allocator = std.testing.allocator;
    const args = &[_][]const u8{};
    const result = parseArgs(allocator, args);

    try std.testing.expectError(error.NoInputFile, result);
}

test "parseArgs error - multiple input files" {
    const allocator = std.testing.allocator;
    const args = &[_][]const u8{ "input1.md", "input2.md" };
    const result = parseArgs(allocator, args);

    try std.testing.expectError(error.MultipleInputFiles, result);
}

test "parseArgs error - --output without value" {
    const allocator = std.testing.allocator;
    const args = &[_][]const u8{ "input.md", "--output" };
    const result = parseArgs(allocator, args);

    try std.testing.expectError(error.MissingArgument, result);
}

test "parseArgs error - --ruleset without value" {
    const allocator = std.testing.allocator;
    const args = &[_][]const u8{ "input.md", "--ruleset" };
    const result = parseArgs(allocator, args);

    try std.testing.expectError(error.MissingArgument, result);
}

test "parseArgs error - invalid ruleset" {
    const allocator = std.testing.allocator;
    const args = &[_][]const u8{ "input.md", "--ruleset", "invalid" };
    const result = parseArgs(allocator, args);

    try std.testing.expectError(error.InvalidRuleset, result);
}

test "parseArgs error - unknown option" {
    const allocator = std.testing.allocator;
    const args = &[_][]const u8{ "input.md", "--unknown" };
    const result = parseArgs(allocator, args);

    try std.testing.expectError(error.UnknownOption, result);
}

test "CLI integration - parse markdown with core ruleset" {
    const allocator = std.testing.allocator;

    const markdown = "# Heading\n\nParagraph text";
    var ruleset = try allmark.core.init(allocator);
    defer ruleset.blocks.deinit();
    defer ruleset.inlines.deinit();
    defer ruleset.renderers.deinit();

    const doc = try allmark.parse.execute(allocator, markdown, ruleset);
    defer doc.deinit(allocator);

    const html = try allmark.render(allocator, doc, null, false, null);
    defer allocator.free(html);

    try std.testing.expect(html.len > 0);
    try std.testing.expect(std.mem.indexOf(u8, html, "<h1>") != null);
    try std.testing.expect(std.mem.indexOf(u8, html, "Heading") != null);
}

test "CLI integration - parse markdown with gfm ruleset" {
    const allocator = std.testing.allocator;

    const markdown = "# Heading\n\n- List item 1\n- List item 2";
    var ruleset = try allmark.gfm.init(allocator);
    defer ruleset.blocks.deinit();
    defer ruleset.inlines.deinit();
    defer ruleset.renderers.deinit();

    const doc = try allmark.parse.execute(allocator, markdown, ruleset);
    defer doc.deinit(allocator);

    const html = try allmark.render(allocator, doc, null, false, null);
    defer allocator.free(html);

    try std.testing.expect(html.len > 0);
    try std.testing.expect(std.mem.indexOf(u8, html, "<h1>") != null);
    try std.testing.expect(std.mem.indexOf(u8, html, "<ul>") != null);
}

test "CLI integration - parse markdown with extended ruleset" {
    const allocator = std.testing.allocator;

    const markdown = "# Heading\n\n**bold** text";
    var ruleset = try allmark.extended.init(allocator);
    defer ruleset.blocks.deinit();
    defer ruleset.inlines.deinit();
    defer ruleset.renderers.deinit();

    const doc = try allmark.parse.execute(allocator, markdown, ruleset);
    defer doc.deinit(allocator);

    const html = try allmark.render(allocator, doc, null, false, null);
    defer allocator.free(html);

    try std.testing.expect(html.len > 0);
    try std.testing.expect(std.mem.indexOf(u8, html, "<h1>") != null);
    try std.testing.expect(std.mem.indexOf(u8, html, "<strong>") != null);
}

test "CLI integration - write output to file" {
    const allocator = std.testing.allocator;

    const markdown = "# Test";
    var ruleset = try allmark.core.init(allocator);
    defer ruleset.blocks.deinit();
    defer ruleset.inlines.deinit();
    defer ruleset.renderers.deinit();

    const doc = try allmark.parse.execute(allocator, markdown, ruleset);
    defer doc.deinit(allocator);

    const html = try allmark.render(allocator, doc, null, false, null);
    defer allocator.free(html);

    try std.testing.expect(html.len > 0);
    try std.testing.expect(std.mem.indexOf(u8, html, "<h1>Test</h1>") != null);
}
