const std = @import("std");

pub const InlineParserState = struct {
    rules: std.StringHashMap(*const InlineRule),

    src: []const u8,
    i: usize,
    line: i32,
    lineStart: usize,
    indent: i32,
    delimiters: std.ArrayList(Delimiter),
    refs: std.StringHashMap(LinkReference),
    footnotes: std.StringHashMap(FootnoteReference),

    // HACK:
    debug: ?bool = null,
};

const InlineRule = @import("InlineRule.zig").InlineRule;
const Delimiter = @import("Delimiter.zig").Delimiter;
const LinkReference = @import("LinkReference.zig").LinkReference;
const FootnoteReference = @import("FootnoteReference.zig").FootnoteReference;
