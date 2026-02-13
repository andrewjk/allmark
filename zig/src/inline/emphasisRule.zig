const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const addMarkupAsText = @import("../utils/addMarkupAsText.zig").addMarkupAsText;
const isEscaped = @import("../utils/isEscaped.zig").isEscaped;
const isUnicodePunctuation = @import("../utils/isUnicodePunctuation.zig").isUnicodePunctuation;
const isUnicodeSpace = @import("../utils/isUnicodeSpace.zig").isUnicodeSpace;
const newNode = @import("../utils/newNode.zig").newNode;

pub fn testEmphasis(state: *InlineParserState, parent: *MarkdownNode) bool {
    if (state.i >= state.src.len) return false;

    const char = state.src[state.i];
    if ((char == '*' or char == '_') and !isEscaped(state.src, state.i)) {
        const start = state.i;
        var end = state.i;

        var markup_buf: [3]u8 = undefined;
        var markup_len: usize = 0;
        markup_buf[0] = char;
        markup_len = 1;

        var i = start + 1;
        while (i < state.src.len and state.src[i] == char) : (i += 1) {
            markup_buf[markup_len] = char;
            markup_len += 1;
            end += 1;
        }

        const markup = markup_buf[0..markup_len];

        const codeBefore = if (start > 0) state.src[start - 1] else 0;
        const spaceBefore = start == 0 or isUnicodeSpace(codeBefore);
        const punctuationBefore = !spaceBefore and isUnicodePunctuation(codeBefore);

        const codeAfter = if (end + 1 < state.src.len) state.src[end + 1] else 0;
        const spaceAfter = end == state.src.len - 1 or isUnicodeSpace(codeAfter);
        const punctuationAfter = !spaceAfter and isUnicodePunctuation(codeAfter);

        const leftFlanking = !spaceAfter and (!punctuationAfter or (punctuationAfter and (spaceBefore or punctuationBefore)));

        const rightFlanking = !spaceBefore and (!punctuationBefore or (punctuationBefore and (spaceAfter or punctuationAfter)));

        var startDelimiter: ?*const std.ArrayList(@import("../types/Delimiter.zig").Delimiter).Item = null;
        var startIndex: usize = 0;
        var d = state.delimiters.items.len;
        while (d > 0) : (d -= 1) {
            const prevDelimiter = &state.delimiters.items[d - 1];
            if (prevDelimiter.handled == null or !prevDelimiter.handled.?) {
                if (std.mem.eql(u8, prevDelimiter.markup, &.{char})) {
                    if (prevDelimiter.length == markup_len) {
                        startDelimiter = prevDelimiter;
                        startIndex = d - 1;
                        break;
                    } else if (startDelimiter == null) {
                        startDelimiter = prevDelimiter;
                        startIndex = d - 1;
                    }
                } else if (std.mem.eql(u8, prevDelimiter.markup, "*") or std.mem.eql(u8, prevDelimiter.markup, "_")) {
                    continue;
                } else {
                    break;
                }
            }
        }

        if (startDelimiter != null) {
            const canClose = (rightFlanking or (state.i > 0 and state.src[state.i - 1] == char)) and
                std.mem.eql(u8, startDelimiter.?.markup, &.{char}) and
                (char != '_' or spaceAfter or punctuationAfter) and
                (!leftFlanking or
                    ((markup_len + startDelimiter.?.length) % 3 != 0 or
                        (markup_len % 3 == 0 and startDelimiter.?.length % 3 == 0)));

            if (canClose) {
                const children = parent.children orelse return false;
                var child_i = children.items.len;
                while (child_i > 0) : (child_i -= 1) {
                    const lastNode = children.items[child_i - 1];
                    if (lastNode.index == startDelimiter.?.start) {
                        const use_len = @min(startDelimiter.?.length, 2);
                        const actual_markup = markup[0..use_len];

                        const text = newNode(state.allocator, "text", false, lastNode.index, lastNode.line, 1, &.{char}, 0, null) catch return false;
                        text.*.markup = startDelimiter.?.markup[startDelimiter.?.length..];

                        const moved_len = children.items.len - child_i;

                        const type_str = if (use_len == 2) "strong" else "emphasis";

                        if (use_len < startDelimiter.?.length) {
                            const new_len = startDelimiter.?.length - use_len;
                            lastNode.*.markup = startDelimiter.?.markup[0..new_len];

                            const emphasis = newNode(state.allocator, type_str, false, lastNode.index + use_len, lastNode.line, 1, actual_markup, 0, null) catch return false;
                            emphasis.*.children = state.allocator.alloc(*MarkdownNode, moved_len + 1) catch return false;
                            emphasis.*.children.?[0] = text;

                            var j: usize = 1;
                            while (child_i < children.items.len) {
                                emphasis.*.children.?[j] = children.items[child_i];
                                j += 1;
                                child_i += 1;
                            }
                            children.shrinkRetainingCapacity(child_i - moved_len);
                            children.append(emphasis) catch return false;
                        } else {
                            lastNode.*.type = type_str;
                            lastNode.*.markup = actual_markup;
                            lastNode.*.children = state.allocator.alloc(*MarkdownNode, moved_len + 1) catch return false;
                            lastNode.*.children.?[0] = text;

                            var j: usize = 1;
                            while (child_i < children.items.len) {
                                lastNode.*.children.?[j] = children.items[child_i];
                                j += 1;
                                child_i += 1;
                            }
                            children.shrinkRetainingCapacity(child_i - moved_len);
                        }

                        state.i += use_len;

                        var del_i = state.delimiters.items.len;
                        while (del_i > 0) : (del_i -= 1) {
                            if (del_i == startIndex) break;
                            state.delimiters.items[del_i - 1].handled = true;
                        }

                        if (startDelimiter) |sd| {
                            sd.length -= use_len;
                            if (sd.length == 0) {
                                sd.handled = true;
                            }
                        }

                        return true;
                    }
                }
            }
        }

        const canOpen = leftFlanking and (char != '_' or spaceBefore or punctuationBefore);
        if (canOpen) {
            const text = newNode(state.allocator, "text", false, start, state.line, 1, markup, 0, null) catch return false;
            if (parent.children == null) {
                parent.children = std.ArrayList(*MarkdownNode).initCapacity(state.allocator, 1) catch return false;
                parent.children.?.appendAssumeCapacity(text);
            } else {
                parent.children.?.append(text) catch return false;
            }

            state.i += markup_len;
            state.delimiters.append(.{ .markup = &.{char}, .start = start, .length = markup_len, .handled = null }) catch return false;

            return true;
        }

        addMarkupAsText(state.allocator, markup, state, parent) catch return false;

        return true;
    }

    return false;
}

pub const emphasisRule = InlineRule{
    .name = "emphasis",
    .@"test" = testEmphasis,
};
