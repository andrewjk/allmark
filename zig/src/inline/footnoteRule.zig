const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const parseInline = @import("../parse/parseInline.zig").parseInline;
const isEscaped = @import("../utils/isEscaped.zig").isEscaped;
const newNode = @import("../utils/newNode.zig").newNode;
const normalizeLabel = @import("../utils/normalizeLabel.zig").normalizeLabel;

pub fn testFootnote(state: *InlineParserState, parent: *MarkdownNode) bool {
    if (state.i >= state.src.len) return false;

    const char = state.src[state.i];

    if (!isEscaped(state.src, state.i)) {
        if (char == '[') {
            return testFootnoteOpen(state, parent);
        }

        if (char == ']') {
            return testFootnoteClose(state, parent);
        }
    }

    return false;
}

fn testFootnoteOpen(state: *InlineParserState, parent: *MarkdownNode) bool {
    const start = state.i;

    if (state.i + 1 >= state.src.len or state.src[state.i + 1] != '^') {
        return false;
    }

    const markup = "[^";

    const text = newNode(state.allocator, "text", false, start, state.line, 1, markup, 0, null) catch return false;
    if (parent.children == null) {
        parent.children = std.ArrayList(*MarkdownNode).initCapacity(state.allocator, 1) catch return false;
        parent.children.?.appendAssumeCapacity(text);
    } else {
        parent.children.?.append(text) catch return false;
    }

    state.i += 2;
    state.delimiters.append(.{ .markup = markup, .start = start, .length = 2, .handled = null }) catch return false;

    return true;
}

fn testFootnoteClose(state: *InlineParserState, parent: *MarkdownNode) bool {
    var startDelimiter: ?*const std.ArrayList(@import("../types/Delimiter.zig").Delimiter).Item = null;
    var d = state.delimiters.items.len;
    while (d > 0) : (d -= 1) {
        const prevDelimiter = &state.delimiters.items[d - 1];
        if (prevDelimiter.handled == null or !prevDelimiter.handled.?) {
            if (std.mem.eql(u8, prevDelimiter.markup, "[^")) {
                startDelimiter = prevDelimiter;
                break;
            }
        }
    }

    if (startDelimiter != null) {
        const children = parent.children orelse return false;
        var child_i = children.items.len;
        while (child_i > 0) : (child_i -= 1) {
            const lastNode = children.items[child_i - 1];
            if (lastNode.index == startDelimiter.?.start) {
                var label = state.src[startDelimiter.?.start + startDelimiter.?.markup.len .. state.i];

                const hasNonAlpha = for (label) |c| {
                    if (!std.ascii.isAlphanumeric(c)) break true;
                } else false;

                if (hasNonAlpha) {
                    return false;
                }

                var level: i32 = 0;
                var j: usize = 0;
                while (j < label.len) : (j += 1) {
                    if (label[j] == '\\') {
                        j += 1;
                    } else if (label[j] == '[') {
                        level += 1;
                    } else if (label[j] == ']') {
                        level -= 1;
                    }
                }
                if (level != 0) {
                    return false;
                }

                if (state.i + 1 < state.src.len and state.src[state.i + 1] == '[') {
                    const start = state.i + 2;
                    var k = start;
                    while (k < state.src.len) : (k += 1) {
                        if (state.src[k] == ']') {
                            const linkRef = state.src[start..k];
                            const normalized = try normalizeLabel(state.allocator, linkRef);
                            defer state.allocator.free(normalized);

                            if (state.refs.get(normalized)) |_| {
                                if (startDelimiter) |sd| sd.markup = "[";
                                return false;
                            }
                            state.i = k;
                            break;
                        }
                    }
                }

                const normalized = try normalizeLabel(state.allocator, label);
                defer state.allocator.free(normalized);

                const footnote = state.footnotes.get(normalized);

                if (footnote != null) {
                    state.i += 1;
                    if (startDelimiter) |sd| sd.handled = true;

                    lastNode.*.type = "footnote";
                    lastNode.*.info = try state.allocator.dupe(u8, normalized);
                    lastNode.*.markup = try std.fmt.allocPrint(state.allocator, "[^{s}]", .{normalized});
                    lastNode.*.children = try state.allocator.alloc(*MarkdownNode, 0);

                    const trimmedContent = std.mem.trimRight(u8, footnote.content, &std.ascii.whitespace);
                    var tempState = InlineParserState{
                        .rules = state.rules,
                        .src = trimmedContent,
                        .i = 0,
                        .line = lastNode.line,
                        .lineStart = 0,
                        .indent = 0,
                        .delimiters = std.ArrayList(@import("../types/Delimiter.zig").Delimiter).init(state.allocator),
                        .refs = state.refs,
                        .footnotes = state.footnotes,
                        .debug = state.debug,
                    };

                    try parseInline(&tempState, lastNode);

                    return true;
                }

                if (startDelimiter) |sd| sd.handled = true;
                break;
            }
        }
    }

    return false;
}

pub const footnoteRule = InlineRule{
    .name = "footnote",
    .@"test" = testFootnote,
};
