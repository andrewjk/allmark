const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const parseBlockInlines = @import("../parse/parseBlockInlines.zig").parseBlockInlines;
const parseInline = @import("../parse/parseInline.zig").parseInline;
const isAlphanumeric = @import("../utils/isAlphaNumeric.zig").isAlphaNumeric;
const isEscaped = @import("../utils/isEscaped.zig").isEscaped;
const newNode = @import("../utils/newNode.zig").newNode;
const normalizeLabel = @import("../utils/normalizeLabel.zig").normalizeLabel;
const appendChild = @import("../utils/appendChild.zig").appendChild;

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

    const text = newNode(state.allocator, "text", false, state.parentIndex + start, state.line, 1, markup, 0, null) catch return false;
    appendChild(state.allocator, parent, text) catch return false;

    state.i += 2;
    const Delimiter = @import("../types/Delimiter.zig").Delimiter;
    var delim: Delimiter = .{
        .markup = undefined,
        .markup_len = 2,
        .start = start,
        .length = 2,
        .handled = false,
    };
    delim.markup[0] = '[';
    delim.markup[1] = '^';
    state.delimiters.append(state.allocator, delim) catch return false;

    return true;
}

fn testFootnoteClose(state: *InlineParserState, parent: *MarkdownNode) bool {
    const Delimiter = @import("../types/Delimiter.zig").Delimiter;
    var startDelimiter: ?*Delimiter = null;
    var d = state.delimiters.items.len;
    while (d > 0) : (d -= 1) {
        const prevDelimiter = &state.delimiters.items[d - 1];
        if (!prevDelimiter.handled) {
            if (std.mem.eql(u8, prevDelimiter.getMarkup(), "[^")) {
                startDelimiter = prevDelimiter;
                break;
            }
        }
    }

    if (startDelimiter != null) {
        const children = parent.children orelse return false;
        var child_i: usize = children.len;
        while (child_i > 0) : (child_i -= 1) {
            const lastNode = children[child_i - 1];
            if (lastNode.index == state.parentIndex + startDelimiter.?.start) {
                const start_markup = startDelimiter.?.getMarkup();
                var label = state.src[startDelimiter.?.start + start_markup.len .. state.i];

                const hasNonAlpha = for (label) |c| {
                    if (!isAlphanumeric(c)) break true;
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
                            const normalized = normalizeLabel(state.allocator, linkRef) catch return false;
                            defer state.allocator.free(normalized);

                            if (state.refs.get(normalized) != null) {
                                if (startDelimiter) |sd| {
                                    sd.markup[0] = '[';
                                    sd.markup_len = 1;
                                }
                                return false;
                            }
                            state.i = k;
                            break;
                        }
                    }
                }

                const normalized = normalizeLabel(state.allocator, label) catch return false;
                defer state.allocator.free(normalized);

                const footnote = state.footnotes.get(normalized);

                if (footnote != null) {
                    state.i += 1;
                    if (startDelimiter) |sd| sd.handled = true;

                    const oldType = lastNode.*.type;
                    lastNode.*.type = state.allocator.dupe(u8, "footnote") catch return false;
                    state.allocator.free(oldType);
                    lastNode.*.info = state.allocator.dupe(u8, normalized) catch return false;
                    if (lastNode.*.markup_allocated) {
                        state.allocator.free(lastNode.*.markup);
                    }
                    lastNode.*.markup = std.fmt.allocPrint(state.allocator, "[^{s}]", .{normalized}) catch return false;
                    lastNode.*.markup_allocated = true;

                    // Transfer children from the footnote definition node to prevent double-free
                    if (footnote.?.content.children) |contentChildren| {
                        // Since they are transferred (rather than referenced, as in the web version)
                        // they will never be parsed and so we need to do it here
                        parseBlockInlines(state.allocator, footnote.?.content, state.rules, state.refs, state.footnotes) catch |err| {
                            std.debug.print("Error parsing footnote inlines: {s}\n", .{@errorName(err)});
                        };

                        lastNode.*.children = contentChildren;

                        // Null out the source to prevent double-free during cleanup
                        footnote.?.content.children = null;
                    } else {
                        lastNode.*.children = state.allocator.alloc(*MarkdownNode, 0) catch return false;
                    }

                    var trimmed_content = lastNode.content;
                    while (trimmed_content.len > 0 and std.ascii.isWhitespace(trimmed_content[trimmed_content.len - 1])) {
                        trimmed_content = trimmed_content[0 .. trimmed_content.len - 1];
                    }
                    const delimiters_list = std.ArrayList(Delimiter).initCapacity(state.allocator, 0) catch unreachable;
                    var temp_state = InlineParserState{
                        .allocator = state.allocator,
                        .rules = state.rules,
                        .src = trimmed_content,
                        .i = 0,
                        .line = lastNode.line,
                        .lineStart = 0,
                        .indent = 0,
                        .delimiters = delimiters_list,
                        .refs = state.refs,
                        .footnotes = state.footnotes,
                        .parentIndex = lastNode.index,
                    };
                    defer temp_state.delimiters.deinit(state.allocator);

                    parseInline(state.allocator, &temp_state, lastNode) catch |err| {
                        std.debug.print("Error parsing inlines for {s}: {s}\n", .{ lastNode.type, @errorName(err) });
                    };

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
