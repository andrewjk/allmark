const std = @import("std");
const InlineParserState = @import("../types/InlineParserState.zig").InlineParserState;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const isEscaped = @import("../utils/isEscaped.zig").isEscaped;
const newNode = @import("../utils/newNode.zig").newNode;

pub fn testCriticMarks(name: []const u8, delimiter: u8, state: *InlineParserState, parent: *MarkdownNode) bool {
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
                } else if (state.src[i] == '}') {
                    return false;
                } else {
                    break;
                }
            }

            const markup = markup_buf[0..markup_len];

            if (markup_len == 2 or markup_len == 3) {
                const text = newNode(state.allocator, "text", false, start, state.line, 1, markup, 0, null) catch return false;
                if (parent.children == null) {
                    parent.children = std.ArrayList(*MarkdownNode).initCapacity(state.allocator, 1) catch return false;
                    parent.children.?.appendAssumeCapacity(text);
                } else {
                    parent.children.?.append(text) catch return false;
                }

                state.i += markup_len;
                state.delimiters.append(.{ .markup = markup, .start = start, .length = markup_len, .handled = null }) catch return false;

                return true;
            }
        } else if (char == delimiter and !isEscaped(state.src, state.i)) {
            var markup_buf: [4]u8 = undefined;
            var markup_len: usize = 0;
            markup_buf[0] = '{';
            markup_buf[1] = delimiter;
            markup_len = 2;

            var i = state.i + 1;
            while (i < state.src.len) : (i += 1) {
                if (state.src[i] == delimiter) {
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
                var startDelimiter: ?*const std.ArrayList(@import("../types/Delimiter.zig").Delimiter).Item = null;
                var d = state.delimiters.items.len;
                while (d > 0) : (d -= 1) {
                    const prevDelimiter = &state.delimiters.items[d - 1];
                    if (prevDelimiter.handled == null or !prevDelimiter.handled.?) {
                        if (std.mem.eql(u8, prevDelimiter.markup, markup)) {
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
                            const newText = lastNode.markup[startDelimiter.?.length..];
                            const text = newNode(state.allocator, "text", false, lastNode.index, lastNode.line, 1, newText, 0, null) catch return false;

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
        }
    }

    return false;
}
