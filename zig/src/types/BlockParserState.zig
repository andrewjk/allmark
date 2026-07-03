const std = @import("std");

pub const BlockParserState = struct {
    allocator: std.mem.Allocator,
    rules: []const *const BlockRule,
    rulesMap: std.StringHashMap(*const BlockRule),
    src: []const u8,
    i: usize,
    line: i32,
    lineStart: usize,
    indent: i32,
    spaces: []const u8,
    isEscaped: bool,
    openNodes: std.ArrayList(*MarkdownNode),
    maybeContinue: bool,
    hasBlankLine: bool,
    refs: std.StringHashMap(LinkReference),
    footnotes: std.StringHashMap(FootnoteReference),
};

const BlockRule = @import("BlockRule.zig").BlockRule;
const MarkdownNode = @import("MarkdownNode.zig").MarkdownNode;
const LinkReference = @import("LinkReference.zig").LinkReference;
const FootnoteReference = @import("FootnoteReference.zig").FootnoteReference;
