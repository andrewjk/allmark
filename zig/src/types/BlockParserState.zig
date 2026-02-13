const std = @import("std");

pub const BlockParserState = struct {
    rules: std.StringHashMap(*const BlockRule),

    src: []const u8,
    i: usize,
    line: i32,
    lineStart: usize,
    indent: i32,
    openNodes: std.ArrayList(*MarkdownNode),
    maybeContinue: bool,
    hasBlankLine: bool,
    refs: std.StringHashMap(LinkReference),
    footnotes: std.StringHashMap(FootnoteReference),

    // HACK:
    debug: ?bool = null,
};

const BlockRule = @import("BlockRule.zig").BlockRule;
const MarkdownNode = @import("MarkdownNode.zig").MarkdownNode;
const LinkReference = @import("LinkReference.zig").LinkReference;
const FootnoteReference = @import("FootnoteReference.zig").FootnoteReference;
