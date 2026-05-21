const std = @import("std");

const allmark = @import("allmark");

const printUsage = struct {
    pub fn print(writer: anytype) !void {
        try writer.print("Usage: allmark <input-file> [-o <file>] [-r <core|gfm|extended>] [-f <html|console>]\n", .{});
        try writer.print("  input-file   Path to the markdown file to convert\n", .{});
        try writer.print("  -o, --output Path to output HTML file (optional, prints to stdout by default)\n", .{});
        try writer.print("  -r, --ruleset Ruleset to use: core, gfm, or extended (default: extended)\n", .{});
        try writer.print("  -f, --format Output format: html or console (default: html)\n", .{});
        try writer.print("  -h, --help   Show this help message\n", .{});
    }
};

const Args = struct {
    input: []const u8,
    output: ?[]const u8,
    ruleset: []const u8,
    format: []const u8,
};

pub fn parseArgs(allocator: std.mem.Allocator, args: []const []const u8, io: std.Io) !Args {
    _ = allocator;
    var buffer: [4096]u8 = undefined;
    var result = Args{
        .input = "",
        .output = null,
        .ruleset = "extended",
        .format = "html",
    };

    var i: usize = 0;
    while (i < args.len) {
        const arg = args[i];

        if (std.mem.eql(u8, arg, "--output") or std.mem.eql(u8, arg, "-o")) {
            if (i + 1 >= args.len) {
                var stderr = std.Io.File.stderr().writer(io, &buffer);
                try stderr.interface.print("Error: {s} requires a file path\n", .{arg});
                try printUsage.print(&stderr.interface);
                try stderr.interface.flush();
                std.process.exit(1);
            }
            result.output = args[i + 1];
            i += 2;
            continue;
        } else if (std.mem.eql(u8, arg, "--ruleset") or std.mem.eql(u8, arg, "-r")) {
            if (i + 1 >= args.len) {
                var stderr = std.Io.File.stderr().writer(io, &buffer);
                try stderr.interface.print("Error: {s} requires a value (core, gfm, or extended)\n", .{arg});
                try printUsage.print(&stderr.interface);
                try stderr.interface.flush();
                std.process.exit(1);
            }
            const ruleset = args[i + 1];
            if (!std.mem.eql(u8, ruleset, "core") and !std.mem.eql(u8, ruleset, "gfm") and !std.mem.eql(u8, ruleset, "extended")) {
                var stderr = std.Io.File.stderr().writer(io, &buffer);
                try stderr.interface.print("Error: Invalid ruleset '{s}'. Must be core, gfm, or extended\n", .{ruleset});
                try printUsage.print(&stderr.interface);
                try stderr.interface.flush();
                std.process.exit(1);
            }
            result.ruleset = ruleset;
            i += 2;
            continue;
        } else if (std.mem.eql(u8, arg, "--format") or std.mem.eql(u8, arg, "-f")) {
            if (i + 1 >= args.len) {
                var stderr = std.Io.File.stderr().writer(io, &buffer);
                try stderr.interface.print("Error: {s} requires a value (html or console)\n", .{arg});
                try printUsage.print(&stderr.interface);
                try stderr.interface.flush();
                std.process.exit(1);
            }
            const format = args[i + 1];
            if (!std.mem.eql(u8, format, "html") and !std.mem.eql(u8, format, "console")) {
                var stderr = std.Io.File.stderr().writer(io, &buffer);
                try stderr.interface.print("Error: Invalid format '{s}'. Must be html or console\n", .{format});
                try printUsage.print(&stderr.interface);
                try stderr.interface.flush();
                std.process.exit(1);
            }
            result.format = format;
            i += 2;
            continue;
        } else if (std.mem.eql(u8, arg, "--help") or std.mem.eql(u8, arg, "-h")) {
            var stdout = std.Io.File.stdout().writer(io, &buffer);
            try printUsage.print(&stdout.interface);
            try stdout.interface.flush();
            std.process.exit(0);
        } else if (std.mem.startsWith(u8, arg, "--")) {
            var stderr = std.Io.File.stderr().writer(io, &buffer);
            try stderr.interface.print("Error: Unknown option '{s}'\n", .{arg});
            try printUsage.print(&stderr.interface);
            try stderr.interface.flush();
            std.process.exit(1);
        } else if (std.mem.startsWith(u8, arg, "-") and arg.len > 1) {
            var stderr = std.Io.File.stderr().writer(io, &buffer);
            try stderr.interface.print("Error: Unknown option '{s}'\n", .{arg});
            try printUsage.print(&stderr.interface);
            try stderr.interface.flush();
            std.process.exit(1);
        } else {
            if (result.input.len > 0) {
                var stderr = std.Io.File.stderr().writer(io, &buffer);
                try stderr.interface.print("Error: Multiple input files specified: '{s}' and '{s}'\n", .{ result.input, arg });
                try printUsage.print(&stderr.interface);
                try stderr.interface.flush();
                std.process.exit(1);
            }
            result.input = arg;
            i += 1;
        }
    }

    if (result.input.len == 0) {
        var stderr = std.Io.File.stderr().writer(io, &buffer);
        try stderr.interface.print("Error: No input file specified\n", .{});
        try printUsage.print(&stderr.interface);
        try stderr.interface.flush();
        std.process.exit(1);
    }

    return result;
}

pub fn main(init: std.process.Init) !void {
    const allocator = init.gpa;
    const arena = init.arena.allocator();
    const io = init.io;

    const args = try std.process.Args.toSlice(init.minimal.args, arena);
    const parsed_args = try parseArgs(allocator, args[1..], io);

    const cwd = std.Io.Dir.cwd();
    const markdown = cwd.readFileAlloc(io, parsed_args.input, allocator, .unlimited) catch |err| {
        var buffer: [4096]u8 = undefined;
        var stderr = std.Io.File.stderr().writer(io, &buffer);
        if (err == error.FileNotFound) {
            try stderr.interface.print("Error: File not found: '{s}'\n", .{parsed_args.input});
        } else {
            try stderr.interface.print("Error: {}\n", .{err});
        }
        try stderr.interface.flush();
        std.process.exit(1);
    };
    defer allocator.free(markdown);

    var ruleset: allmark.RuleSet = if (std.mem.eql(u8, parsed_args.ruleset, "core"))
        try allmark.core.init(allocator)
    else if (std.mem.eql(u8, parsed_args.ruleset, "gfm"))
        try allmark.gfm.init(allocator)
    else
        try allmark.extended.init(allocator);
    defer allmark.core.deinit(&ruleset, allocator);

    const document = try allmark.parse.execute(allocator, markdown, ruleset);
    defer document.deinit(allocator);

    const useConsole = std.mem.eql(u8, parsed_args.format, "console");
    const output = try allmark.render(allocator, document, null, useConsole, null);
    defer allocator.free(output);

    if (parsed_args.output) |output_path| {
        try cwd.writeFile(io, .{ .sub_path = output_path, .data = output });
    } else {
        var buffer: [4096]u8 = undefined;
        var stdout = std.Io.File.stdout().writer(io, &buffer);
        try stdout.interface.print("{s}\n", .{output});
        try stdout.interface.flush();
    }
}
