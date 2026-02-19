const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const isEscaped = @import("../utils/isEscaped.zig").isEscaped;
const newNode = @import("../utils/newNode.zig").newNode;

pub fn testCriticMarks(name: []const u8, delimiter: u8, state: *InlineParserState, parent: *MarkdownNode, closingDelimiter: ?u8) bool {
    const closeDel = closingDelimiter orelse delimiter;
    if (state.i < state.src.len) {
        const char = state.src[state.i];
        if (char == '{' and !isEscaped(state.src, state.i)) {
            const start = state.i;
            var end = state.i;

            var markup_buf: [4]u8 = undefined;
            var markup_len: usize = 0;
            markup_buf[0] = '{';
            markup_len = 1;

            var i = start + 1;
            while (i < state.src.len) : (i += 1) {
                if (state.src[i] == delimiter) {
                    markup_buf[markup_len] = delimiter;
                    markup_len += 1;
                    end += 1;
                } else if (state.src[i] == '}' or (closeDel != delimiter and state.src[i] == closeDel)) {
                    return false;
                } else {
                    break;
                }
            }

            const markup = markup_buf[0..markup_len];

            if (markup_len == 2 or markup_len == 3) {
                const text = newNode(state.allocator, "text", false, start, state.line, 1, markup, 0, null) catch return false;
                const old_children = parent.children orelse &[_]*MarkdownNode{};
                const new_children = state.allocator.alloc(*MarkdownNode, old_children.len + 1) catch return false;
                if (parent.children) |children| {
                    std.mem.copyForwards(*MarkdownNode, new_children, children);
                    state.allocator.free(children);
                }
                new_children[old_children.len] = text;
                parent.children = new_children;

                state.i += markup_len;
                const Delimiter = @import("../types/Delimiter.zig").Delimiter;
                var delim: Delimiter = .{
                    .markup = undefined,
                    .markup_len = @intCast(markup_len),
                    .start = start,
                    .length = markup_len,
                    .handled = false,
                };
                @memcpy(delim.markup[0..markup_len], markup);
                state.delimiters.append(state.allocator, delim) catch return false;

                return true;
            }
        } else if (char == closeDel and !isEscaped(state.src, state.i)) {
            var markup_buf: [4]u8 = undefined;
            var markup_len: usize = 0;
            markup_buf[0] = '{';
            markup_buf[1] = delimiter;
            markup_len = 2;

            var i = state.i + 1;
            while (i < state.src.len) : (i += 1) {
                if (state.src[i] == closeDel) {
                    markup_buf[markup_len] = delimiter;
                    markup_len += 1;
                } else if (state.src[i] == '}') {
                    break;
                } else {
                    return false;
                }
            }

            const markup = markup_buf[0..markup_len];

            if (markup_len == 2 or markup_len == 3) {
                var startDelimiter: ?*@import("../types/Delimiter.zig").Delimiter = null;
                var d = state.delimiters.items.len;
                while (d > 0) : (d -= 1) {
                    var prevDelimiter = &state.delimiters.items[d - 1];
                    if (!prevDelimiter.handled) {
                        if (std.mem.eql(u8, prevDelimiter.getMarkup(), markup)) {
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
                            if (startDelimiter) |sd| {
                                sd.handled = true;
                            }

                            return true;
                        }
                    }
                }
            }
        }
    }

    return false;
}
