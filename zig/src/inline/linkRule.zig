const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const LinkReference = @import("../types/LinkReference.zig").LinkReference;
const isEscaped = @import("../utils/isEscaped.zig").isEscaped;
const newNode = @import("../utils/newNode.zig").newNode;
const normalizeLabel = @import("../utils/normalizeLabel.zig").normalizeLabel;
const parseLinkInline = @import("../utils/parseLinkInline.zig").parseLinkInline;

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
    if (parent.children == null) {
        parent.children = std.ArrayList(*MarkdownNode).initCapacity(state.allocator, 1) catch return false;
        parent.children.?.appendAssumeCapacity(text);
    } else {
        parent.children.?.append(text) catch return false;
    }

    state.i += 1;
    state.delimiters.append(.{ .markup = markup, .start = start, .length = 1, .handled = null }) catch return false;

    return true;
}

fn testImageOpen(state: *InlineParserState, parent: *MarkdownNode) bool {
    const start = state.i;
    const markup = "![";

    const text = newNode(state.allocator, "text", false, start, state.line, 1, markup, 0, null) catch return false;
    if (parent.children == null) {
        parent.children = std.ArrayList(*MarkdownNode).initCapacity(state.allocator, 1) catch return false;
        parent.children.?.appendAssumeCapacity(text);
    } else {
        parent.children.?.append(text) catch return false;
    }

    state.i += markup.len;
    state.delimiters.append(.{ .markup = markup, .start = start, .length = 1, .handled = null }) catch return false;

    return true;
}

fn testLinkClose(state: *InlineParserState, parent: *MarkdownNode) bool {
    const markup = "]";

    var startDelimiter: ?*const std.ArrayList(@import("../types/Delimiter.zig").Delimiter).Item = null;
    var d = state.delimiters.items.len;
    while (d > 0) : (d -= 1) {
        const prevDelimiter = &state.delimiters.items[d - 1];
        if (prevDelimiter.handled == null or !prevDelimiter.handled.?) {
            if (std.mem.eql(u8, prevDelimiter.markup, "[") or std.mem.eql(u8, prevDelimiter.markup, "![")) {
                startDelimiter = prevDelimiter;
                break;
            } else if (std.mem.eql(u8, prevDelimiter.markup, "*") or std.mem.eql(u8, prevDelimiter.markup, "_")) {
                continue;
            } else {
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
                const start = state.i + 1;
                var label = state.src[startDelimiter.?.start + startDelimiter.?.markup.len .. state.i];

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

                const isLink = std.mem.eql(u8, startDelimiter.?.markup, "[");

                const hasInfo = state.i + 1 < state.src.len and state.src[state.i + 1] == '(';
                const hasRef = state.i + 1 < state.src.len and state.src[state.i + 1] == '[';

                var link: ?LinkReference = null;

                if (hasInfo) {
                    const newStart = start + 1;
                    link = try parseLinkInline(state.allocator, state, newStart, ")");
                } else if (hasRef) {
                    const newStart = start + 1;
                    var k = newStart;
                    while (k < state.src.len) : (k += 1) {
                        if (state.src[k] == ']') {
                            const refLabel = if (k > newStart) state.src[newStart..k] else label;
                            const normalized = try normalizeLabel(state.allocator, refLabel);
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
                    const normalized = try normalizeLabel(state.allocator, label);
                    defer state.allocator.free(normalized);
                    link = state.refs.get(normalized);
                    if (link != null) {
                        state.i += 1;
                    }
                }

                if (link != null) {
                    const text = newNode(state.allocator, "text", false, lastNode.index, lastNode.line, 1, markup, 0, null) catch unreachable;
                    text.*.markup = startDelimiter.?.markup[startDelimiter.?.length..];

                    lastNode.*.type = if (isLink) "link" else "image";
                    lastNode.*.info = try state.allocator.dupe(u8, link.?.url);
                    lastNode.*.title = try state.allocator.dupe(u8, link.?.title);

                    const moved_len = children.items.len - child_i;
                    lastNode.*.children = try state.allocator.alloc(*MarkdownNode, moved_len + 1);
                    lastNode.*.children.?[0] = text;

                    var k: usize = 1;
                    while (child_i < children.items.len) {
                        lastNode.*.children.?[k] = children.items[child_i];
                        k += 1;
                        child_i += 1;
                    }
                    children.shrinkRetainingCapacity(child_i - moved_len);

                    if (isLink) {
                        var d2 = state.delimiters.items.len;
                        while (d2 > 0) : (d2 -= 1) {
                            const pd = &state.delimiters.items[d2 - 1];
                            if (std.mem.eql(u8, pd.markup, "[") or std.mem.eql(u8, pd.markup, "]")) {
                                pd.handled = true;
                            }
                        }
                    }

                    if (startDelimiter) |sd| sd.handled = true;
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
