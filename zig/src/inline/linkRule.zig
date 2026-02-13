const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const LinkReference = @import("../types/LinkReference.zig").LinkReference;
const isEscaped = @import("../utils/isEscaped.zig").isEscaped;
const newNode = @import("../utils/newNode.zig").newNode;
const normalizeLabel = @import("../utils/normalizeLabel.zig").normalizeLabel;
const parseLinkInline = @import("../utils/parseLinkInline.zig").parseLinkInline;
const appendChild = @import("../utils/appendChild.zig").appendChild;

pub fn testLink(state: *InlineParserState, parent: *MarkdownNode) bool {
    if (state.i >= state.src.len) return false;

    const char = state.src[state.i];

    if (!isEscaped(state.src, state.i)) {
        if (char == '[') {
            return testLinkOpen(state, parent);
        }

        if (char == '!' and state.i + 1 < state.src.len and state.src[state.i + 1] == '[') {
            return testImageOpen(state, parent);
        }

        if (char == ']') {
            return testLinkClose(state, parent);
        }
    }

    return false;
}

fn testLinkOpen(state: *InlineParserState, parent: *MarkdownNode) bool {
    const start = state.i;
    const markup = "[";

    const text = newNode(state.allocator, "text", false, start, state.line, 1, markup, 0, null) catch return false;
    appendChild(state.allocator, parent, text) catch return false;

    state.i += 1;
    const Delimiter = @import("../types/Delimiter.zig").Delimiter;
    var delim: Delimiter = .{
        .markup = undefined,
        .markup_len = 1,
        .start = start,
        .length = 1,
        .handled = false,
    };
    delim.markup[0] = '[';
    state.delimiters.append(state.allocator, delim) catch return false;

    return true;
}

fn testImageOpen(state: *InlineParserState, parent: *MarkdownNode) bool {
    const start = state.i;
    const markup = "![";

    const text = newNode(state.allocator, "text", false, start, state.line, 1, markup, 0, null) catch return false;
    appendChild(state.allocator, parent, text) catch return false;

    state.i += markup.len;
    const Delimiter = @import("../types/Delimiter.zig").Delimiter;
    var delim: Delimiter = .{
        .markup = undefined,
        .markup_len = 2,
        .start = start,
        .length = 1,
        .handled = false,
    };
    delim.markup[0] = '!';
    delim.markup[1] = '[';
    state.delimiters.append(state.allocator, delim) catch return false;

    return true;
}

fn testLinkClose(state: *InlineParserState, parent: *MarkdownNode) bool {
    const Delimiter = @import("../types/Delimiter.zig").Delimiter;
    var startDelimiter: ?*Delimiter = null;
    var d = state.delimiters.items.len;
    while (d > 0) : (d -= 1) {
        const prevDelimiter = &state.delimiters.items[d - 1];
        if (!prevDelimiter.handled) {
            const prev_markup = prevDelimiter.getMarkup();
            if (std.mem.eql(u8, prev_markup, "[") or std.mem.eql(u8, prev_markup, "![")) {
                startDelimiter = prevDelimiter;
                break;
            } else if (std.mem.eql(u8, prev_markup, "*") or std.mem.eql(u8, prev_markup, "_")) {
                continue;
            } else {
                break;
            }
        }
    }

    if (startDelimiter != null) {
        const children = parent.children orelse return false;
        var child_i: usize = children.len;
        while (child_i > 0) : (child_i -= 1) {
            const lastNode = children[child_i - 1];
            if (lastNode.index == startDelimiter.?.start) {
                const start = state.i + 1;
                const start_markup = startDelimiter.?.getMarkup();
                var label = state.src[startDelimiter.?.start + start_markup.len .. state.i];

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

                const isLink = std.mem.eql(u8, start_markup, "[");

                const hasInfo = state.i + 1 < state.src.len and state.src[state.i + 1] == '(';
                const hasRef = state.i + 1 < state.src.len and state.src[state.i + 1] == '[';

                var link: ?LinkReference = null;
                var inlineLink: ?LinkReference = null;

                if (hasInfo) {
                    const newStart = start + 1;
                    inlineLink = parseLinkInline(state.allocator, state, newStart, ")") catch null;
                    link = inlineLink;
                } else if (hasRef) {
                    const newStart = start + 1;
                    var k = newStart;
                    while (k < state.src.len) : (k += 1) {
                        if (state.src[k] == ']') {
                            label = if (k > newStart) state.src[newStart..k] else label;
                            const normalized = normalizeLabel(state.allocator, label) catch return false;
                            defer state.allocator.free(normalized);
                            link = state.refs.get(normalized);
                            if (link != null) {
                                state.i = k + 1;
                            }
                            break;
                        }
                    }
                }

                if (link == null) {
                    const normalized = normalizeLabel(state.allocator, label) catch return false;
                    defer state.allocator.free(normalized);
                    link = state.refs.get(normalized);
                    if (link != null) {
                        state.i += 1;
                    }
                }

                if (link != null) {
                    const linkText = lastNode.*.markup[start_markup.len..];
                    const text = newNode(state.allocator, "text", false, lastNode.index, lastNode.line, 1, linkText, 0, null) catch unreachable;

                    const oldType = lastNode.*.type;
                    const newType = if (isLink) "link" else "image";
                    lastNode.*.type = state.allocator.dupe(u8, newType) catch unreachable;
                    state.allocator.free(oldType);
                    lastNode.*.info = state.allocator.dupe(u8, link.?.url) catch unreachable;
                    if (link.?.title.len > 0) {
                        lastNode.*.title = state.allocator.dupe(u8, link.?.title) catch unreachable;
                    }

                    // Free link reference URL and title if they were allocated by parseLinkInline
                    // (not from refs hashmap which is freed in parse.zig)
                    if (inlineLink != null) {
                        state.allocator.free(inlineLink.?.url);
                        if (inlineLink.?.title.len > 0) {
                            state.allocator.free(inlineLink.?.title);
                        }
                    }

                    const moved_len = children.len - child_i;
                    lastNode.*.children = state.allocator.alloc(*MarkdownNode, moved_len + 1) catch return false;
                    lastNode.*.children.?[0] = text;

                    var k: usize = 1;
                    while (child_i < children.len) {
                        lastNode.*.children.?[k] = children[child_i];
                        k += 1;
                        child_i += 1;
                    }

                    // Create new parent children array (shrink it by moved_len)
                    const new_parent_children_len = children.len - moved_len;
                    const new_parent_children = state.allocator.alloc(*MarkdownNode, new_parent_children_len) catch return false;
                    for (0..new_parent_children_len) |idx| {
                        // Copy children to new array, emphasis node has already been modified
                        new_parent_children[idx] = children[idx];
                    }

                    // Now safe to free old children array
                    state.allocator.free(children);
                    parent.children = new_parent_children;

                    if (isLink) {
                        var d2 = state.delimiters.items.len;
                        while (d2 > 0) : (d2 -= 1) {
                            const pd = &state.delimiters.items[d2 - 1];
                            const pd_markup = pd.getMarkup();
                            if (std.mem.eql(u8, pd_markup, "[") or std.mem.eql(u8, pd_markup, "]")) {
                                pd.handled = true;
                            }
                        }
                    }

                    if (startDelimiter) |sd| sd.handled = true;

                    // Note: Don't free link.?.url and link.?.title here
                    // They are owned by refs hashmap and will be freed in parse.zig

                    return true;
                }

                if (startDelimiter) |sd| sd.handled = true;
                break;
            }
        }
    }

    return false;
}

pub const linkRule = InlineRule{
    .name = "link",
    .@"test" = testLink,
};
