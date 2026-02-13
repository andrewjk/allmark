const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const addMarkupAsText = @import("../utils/addMarkupAsText.zig").addMarkupAsText;
const isUnicodePunctuation = @import("../utils/isUnicodePunctuation.zig").isUnicodePunctuation;
const isUnicodeSpace = @import("../utils/isUnicodeSpace.zig").isUnicodeSpace;
const newNode = @import("../utils/newNode.zig").newNode;

pub fn testTagMarks(name: []const u8, char: u8, state: *InlineParserState, parent: *MarkdownNode) bool {
    const start = state.i;
    var end = state.i;

    var markup_buf: [3]u8 = undefined;
    var markup_len: usize = 0;
    markup_buf[0] = char;
    markup_len = 1;

    var i = state.i + 1;
    while (i < state.src.len and state.src[i] == char) : (i += 1) {
        markup_buf[markup_len] = char;
        markup_len += 1;
        end += 1;
    }

    const markup = markup_buf[0..markup_len];

    if (markup_len < 3) {
        const codeBefore = if (start > 0) state.src[start - 1] else 0;
        const spaceBefore = start == 0 or isUnicodeSpace(codeBefore);
        const punctuationBefore = !spaceBefore and isUnicodePunctuation(codeBefore);

        const codeAfter = if (end + 1 < state.src.len) state.src[end + 1] else 0;
        const spaceAfter = end == state.src.len - 1 or isUnicodeSpace(codeAfter);
        const punctuationAfter = !spaceAfter and isUnicodePunctuation(codeAfter);

        const leftFlanking = !spaceAfter and (!punctuationAfter or (punctuationAfter and (spaceBefore or punctuationBefore)));

        const rightFlanking = !spaceBefore and (!punctuationBefore or (punctuationBefore and (spaceAfter or punctuationAfter)));

        if (rightFlanking) {
            var startDelimiter: ?*@import("../types/Delimiter.zig").Delimiter = null;
            var d = state.delimiters.items.len;
            while (d > 0) : (d -= 1) {
                const prevDelimiter = &state.delimiters.items[d - 1];
                if (!prevDelimiter.handled) {
                    if (std.mem.eql(u8, prevDelimiter.getMarkup(), &.{char}) and prevDelimiter.length == markup_len) {
                        startDelimiter = prevDelimiter;
                        break;
                    }
                }
            }

            if (startDelimiter != null) {
                const children = parent.children orelse return false;
                var child_i = children.len;
                while (child_i > 0) : (child_i -= 1) {
                    const lastNode = children[child_i - 1];
                    if (lastNode.index == startDelimiter.?.start) {
                        //const text = newNode(state.allocator, "text", false, lastNode.index, lastNode.line, 1, "", 0, null) catch return false;
                        //const start_markup = startDelimiter.?.getMarkup();
                        //if (startDelimiter.?.length < start_markup.len) {
                        //    const remaining = start_markup[startDelimiter.?.length..];
                        //    text.*.markup = state.allocator.dupe(u8, remaining) catch return false;
                        //    text.*.markup_allocated = true;
                        //}
                        const newText = lastNode.markup[startDelimiter.?.length..];
                        const text = newNode(state.allocator, "text", false, lastNode.index, lastNode.line, 1, newText, 0, null) catch return false;

                        const oldType = lastNode.*.type;
                        lastNode.*.type = state.allocator.dupe(u8, name) catch return false;
                        state.allocator.free(oldType);

                        const oldMarkup = lastNode.*.markup;
                        lastNode.*.markup = state.allocator.dupe(u8, markup) catch return false;
                        if (lastNode.markup_allocated) {
                            state.allocator.free(oldMarkup);
                        }
                        lastNode.*.markup_allocated = true;

                        const moved_len = children.len - child_i;
                        lastNode.*.children = state.allocator.alloc(*MarkdownNode, moved_len + 1) catch return false;
                        lastNode.*.children.?[0] = text;

                        var j: usize = 1;
                        while (child_i < children.len) {
                            lastNode.*.children.?[j] = children[child_i];
                            j += 1;
                            child_i += 1;
                        }

                        // Create new parent children array (shrink it by moved_len)
                        const new_parent_children_len = children.len - moved_len;
                        const new_parent_children = state.allocator.alloc(*MarkdownNode, new_parent_children_len) catch return false;
                        for (0..new_parent_children_len) |idx| {
                            // Copy children to new array, the emphasis node has already been modified
                            new_parent_children[idx] = children[idx];
                        }

                        // Now safe to free old children array
                        state.allocator.free(children);
                        parent.children = new_parent_children;

                        state.i += markup_len;
                        if (startDelimiter) |sd| sd.handled = true;

                        return true;
                    }
                }
            }
        }

        if (leftFlanking) {
            const text = newNode(state.allocator, "text", false, start, state.line, 1, markup, 0, null) catch return false;
            if (parent.children == null) {
                const children = state.allocator.alloc(*MarkdownNode, 1) catch return false;
                children[0] = text;
                parent.children = children;
            } else {
                const old_children = parent.children.?;
                const new_children = state.allocator.alloc(*MarkdownNode, old_children.len + 1) catch return false;
                std.mem.copyForwards(*MarkdownNode, new_children, old_children);
                new_children[old_children.len] = text;
                state.allocator.free(old_children);
                parent.children = new_children;
            }

            state.i += markup_len;
            const Delimiter = @import("../types/Delimiter.zig").Delimiter;
            var delim: Delimiter = .{
                .markup = undefined,
                .markup_len = 1,
                .start = start,
                .length = markup_len,
                .handled = false,
            };
            delim.markup[0] = char;
            state.delimiters.append(state.allocator, delim) catch return false;

            return true;
        }
    }

    addMarkupAsText(state.allocator, markup, state, parent) catch return false;

    return true;
}
