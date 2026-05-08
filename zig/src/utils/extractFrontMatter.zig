const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const isNewLine = @import("./isNewLine.zig").isNewLine;
const isSpace = @import("./isSpace.zig").isSpace;

// Check if char is a newline (\n) or carriage return (\r for CrLf)
inline fn isLineEnding(char: u8) bool {
    return char == '\n' or char == '\r';
}

pub fn extractFrontMatter(allocator: std.mem.Allocator, document: *MarkdownNode, src: []const u8, index: usize) !?[]const u8 {
    const DASH: u8 = '-';

    if (src[index] != DASH) {
        return null;
    }

    // Check for opening pattern: "---" followed by optional whitespace and newline
    if (index + 2 >= src.len or src[index + 1] != DASH or src[index + 2] != DASH) {
        return null;
    }

    var i: usize = index + 3;
    // Only consume horizontal whitespace (space and tab), not newlines
    while (i < src.len and (src[i] == ' ' or src[i] == '\t')) : (i += 1) {}

    // Check for newline (\n) or CrLf (\r\n)
    if (i >= src.len or !isLineEnding(src[i])) {
        return null;
    }

    // Skip the newline (and \r if present)
    i += 1;
    if (i < src.len and src[i - 1] == '\r' and src[i] == '\n') {
        i += 1;
    }

    // Look for closing pattern
    var contentEnd: ?usize = null;
    while (i < src.len) {
        if (src[i] == DASH and i + 2 < src.len and src[i + 1] == DASH and src[i + 2] == DASH) {
            var j: usize = i + 3;
            // Only consume horizontal whitespace (space and tab), not newlines
            while (j < src.len and (src[j] == ' ' or src[j] == '\t')) : (j += 1) {}

            // Check for newline (\n) or CrLf (\r\n) or end of string
            if (j >= src.len or isLineEnding(src[j])) {
                // Find the end of the line after the closing delimiter
                if (j < src.len and src[j] == '\r') {
                    j += 1;
                    if (j < src.len and src[j] == '\n') {
                        j += 1;
                    }
                } else if (j < src.len and src[j] == '\n') {
                    j += 1;
                }
                i = j;
                contentEnd = i;
                break;
            }
        }
        i += 1;
    }

    if (contentEnd) |end| {
        const frontmatter = try allocator.dupe(u8, src[index..end]);
        var line_count: usize = 1;
        for (src[0..i]) |c| {
            if (c == '\n') {
                line_count += 1;
            }
        }
        document.line = @intCast(line_count);
        return frontmatter;
    }

    return null;
}
