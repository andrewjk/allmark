const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const InlineRule = @import("../types/InlineRule.zig").InlineRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const Delimiter = @import("../types/Delimiter.zig").Delimiter;
const addMarkupAsText = @import("../utils/addMarkupAsText.zig").addMarkupAsText;
const isEscaped = @import("../utils/isEscaped.zig").isEscaped;
const isUnicodePunctuation = @import("../utils/isUnicodePunctuation.zig").isUnicodePunctuation;
const isUnicodeSpace = @import("../utils/isUnicodeSpace.zig").isUnicodeSpace;
const newText = @import("../utils/newText.zig").newText;
const newInline = @import("../utils/newInline.zig").newInline;

fn testEmphasis(state: *InlineParserState, parent: *MarkdownNode) bool {
    const char = state.src[state.i];
    if ((char != '*' and char != '_') or isEscaped(state.src, state.i)) {
        return false;
    }

    const start = state.i;
    var end = state.i;

    var markup_buf: [10]u8 = undefined;
    var markup_len: usize = 0;
    markup_buf[0] = char;
    markup_len = 1;

    var i = start + 1;
    while (i < state.src.len and state.src[i] == char) : (i += 1) {
        markup_buf[markup_len] = char;
        markup_len += 1;
        end += 1;
    }

    const codeBefore = if (start > 0) @as(u21, state.src[start - 1]) else 0;
    const spaceBefore = start == 0 or isUnicodeSpace(@as(u8, @intCast(codeBefore)));
    const punctuationBefore = !spaceBefore and isUnicodePunctuation(codeBefore);

    const codeAfter = if (end + 1 < state.src.len) @as(u21, state.src[end + 1]) else 0;
    const spaceAfter = end == state.src.len - 1 or isUnicodeSpace(@as(u8, @intCast(codeAfter)));
    const punctuationAfter = !spaceAfter and isUnicodePunctuation(codeAfter);

    const leftFlanking = !spaceAfter and
        (!punctuationAfter or (punctuationAfter and (spaceBefore or punctuationBefore)));

    const rightFlanking = !spaceBefore and
        (!punctuationBefore or (punctuationBefore and (spaceAfter or punctuationAfter)));

    var startDelimiter: ?*Delimiter = null;
    var startIndex: usize = 0;
    var d = state.delimiters.items.len;
    while (d > 0) {
        d -= 1;
        const prevDelimiter = &state.delimiters.items[d];
        if (!prevDelimiter.handled) {
            if (prevDelimiter.markup[0] == char) {
                if (prevDelimiter.length == markup_len) {
                    startDelimiter = prevDelimiter;
                    startIndex = d;
                    break;
                } else if (startDelimiter == null) {
                    startDelimiter = prevDelimiter;
                    startIndex = d;
                }
            } else if ((prevDelimiter.precedence orelse 0) <= emphasisRule.precedence.?) {
                // Same or lower precedence delimiters can be skipped over
                continue;
            } else {
                // Higher precedence delimiters block
                break;
            }
        }
    }

    if (startDelimiter != null) {
        const canClose = (rightFlanking or (state.i > 0 and state.src[state.i - 1] == char)) and
            startDelimiter.?.markup[0] == char and
            (char != '_' or spaceAfter or punctuationAfter) and
            (!leftFlanking or
                (markup_len + startDelimiter.?.length) % 3 != 0 or
                (markup_len % 3 == 0 and startDelimiter.?.length % 3 == 0));

        if (canClose) {
            const children = parent.children orelse return false;
            var child_i: usize = children.len;
            while (child_i > 0) {
                child_i -= 1;
                const lastNode = children[child_i];
                if (lastNode.index == state.parentIndex + startDelimiter.?.start) {
                    const closeLen = @min(markup_len, @min(startDelimiter.?.length, @as(usize, 2)));
                    const closeMarkup = markup_buf[0..closeLen];

                    const textChildContent = if (startDelimiter.?.length < lastNode.content.len) lastNode.content[startDelimiter.?.length..] else "";
                    const text = newText(state.allocator, lastNode.index, lastNode.line, textChildContent, 0) catch return false;

                    const moved_len = children.len - child_i - 1;
                    const emphasisChildren = state.allocator.alloc(*MarkdownNode, moved_len + 1) catch return false;
                    emphasisChildren[0] = text;

                    var j: usize = 1;
                    var k = child_i + 1;
                    while (k < children.len) {
                        emphasisChildren[j] = children[k];
                        j += 1;
                        k += 1;
                    }

                    if (closeLen < startDelimiter.?.length) {
                        const remainingLen = startDelimiter.?.length - closeLen;
                        const newContent = state.allocator.dupe(u8, lastNode.content[0..remainingLen]) catch return false;
                        if (lastNode.content_allocated) {
                            state.allocator.free(lastNode.content);
                        }
                        lastNode.content = newContent;
                        lastNode.content_allocated = true;

                        const emph = newInline(
                            state.allocator,
                            if (closeLen == 2) "strong" else "emphasis",
                            lastNode.index + remainingLen,
                            lastNode.line,
                            closeMarkup,
                            0,
                        ) catch return false;
                        emph.*.children = emphasisChildren;
                        emph.*.length = state.parentIndex + state.i - (lastNode.index + remainingLen) + closeLen;

                        const new_parent_children_len = child_i + 2;
                        const new_parent_children = state.allocator.alloc(*MarkdownNode, new_parent_children_len) catch return false;
                        for (0..child_i + 1) |idx| {
                            new_parent_children[idx] = children[idx];
                        }
                        new_parent_children[child_i + 1] = emph;
                        state.allocator.free(children);
                        parent.children = new_parent_children;
                    } else {
                        const oldType = lastNode.type;
                        lastNode.type = state.allocator.dupe(u8, if (closeLen == 2) "strong" else "emphasis") catch return false;
                        state.allocator.free(oldType);

                        const oldContent = lastNode.content;
                        lastNode.content = state.allocator.dupe(u8, closeMarkup) catch return false;
                        if (lastNode.content_allocated) {
                            state.allocator.free(oldContent);
                        }
                        lastNode.content_allocated = true;
                        lastNode.children = emphasisChildren;
                        lastNode.*.length = state.parentIndex + state.i - lastNode.index + closeLen;

                        const new_parent_children_len = child_i + 1;
                        const new_parent_children = state.allocator.alloc(*MarkdownNode, new_parent_children_len) catch return false;
                        for (0..new_parent_children_len) |idx| {
                            new_parent_children[idx] = children[idx];
                        }
                        state.allocator.free(children);
                        parent.children = new_parent_children;
                    }

                    state.i += closeLen;

                    var dd = state.delimiters.items.len;
                    while (dd > 0) {
                        dd -= 1;
                        if (dd == startIndex) break;
                        state.delimiters.items[dd].handled = true;
                    }

                    startDelimiter.?.length -= closeLen;
                    if (startDelimiter.?.length == 0) {
                        startDelimiter.?.handled = true;
                    }

                    return true;
                }
            }
        }
    }

    const canOpen = leftFlanking and
        (char != '_' or spaceBefore or punctuationBefore);

    if (canOpen) {
        const markup = markup_buf[0..markup_len];
        const text = newText(state.allocator, state.parentIndex + start, state.line, markup, 0) catch return false;

        if (parent.children == null) {
            const newChildren = state.allocator.alloc(*MarkdownNode, 1) catch return false;
            newChildren[0] = text;
            parent.children = newChildren;
        } else {
            const oldChildren = parent.children.?;
            const newChildren = state.allocator.alloc(*MarkdownNode, oldChildren.len + 1) catch return false;
            std.mem.copyForwards(*MarkdownNode, newChildren, oldChildren);
            newChildren[oldChildren.len] = text;
            state.allocator.free(oldChildren);
            parent.children = newChildren;
        }

        state.i += markup_len;

        var delim: Delimiter = .{
            .markup = undefined,
            .markup_len = 1,
            .start = start,
            .length = markup_len,
            .handled = false,
            .precedence = emphasisRule.precedence,
        };
        delim.markup[0] = char;
        state.delimiters.append(state.allocator, delim) catch return false;

        return true;
    }

    const markup = markup_buf[0..markup_len];
    addMarkupAsText(state.allocator, markup, state, parent) catch return false;

    return true;
}

pub const emphasisRule = InlineRule{
    .name = "emphasis",
    .@"test" = testEmphasis,
    .precedence = 10,
};
