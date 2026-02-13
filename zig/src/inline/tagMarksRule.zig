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
            var startDelimiter: ?*const std.ArrayList(@import("../types/Delimiter.zig").Delimiter).Item = null;
            var d = state.delimiters.items.len;
            while (d > 0) : (d -= 1) {
                const prevDelimiter = &state.delimiters.items[d - 1];
                if (prevDelimiter.handled == null or !prevDelimiter.handled.?) {
                    if (std.mem.eql(u8, prevDelimiter.markup, &.{char}) and prevDelimiter.length == markup_len) {
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
                        const text = newNode(state.allocator, "text", false, lastNode.index, lastNode.line, 1, &.{char}, 0, null) catch return false;
                        text.*.markup = startDelimiter.?.markup[startDelimiter.?.length..];

                        lastNode.*.type = name;
                        lastNode.*.markup = markup;

                        const moved_len = children.items.len - child_i;
                        lastNode.*.children = state.allocator.alloc(*MarkdownNode, moved_len + 1) catch return false;
                        lastNode.*.children.?[0] = text;

                        var j: usize = 1;
                        while (child_i < children.items.len) {
                            lastNode.*.children.?[j] = children.items[child_i];
                            j += 1;
                            child_i += 1;
                        }
                        children.shrinkRetainingCapacity(child_i - moved_len);

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
                parent.children = std.ArrayList(*MarkdownNode).initCapacity(state.allocator, 1) catch return false;
                parent.children.?.appendAssumeCapacity(text);
            } else {
                parent.children.?.append(text) catch return false;
            }

            state.i += markup_len;
            state.delimiters.append(.{ .markup = &.{char}, .start = start, .length = markup_len, .handled = null }) catch return false;

            return true;
        }
    }

    addMarkupAsText(state.allocator, markup, state, parent) catch return false;

    return true;
}
