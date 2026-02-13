const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const isNewLine = @import("../utils/isNewLine.zig").isNewLine;
const isSpace = @import("../utils/isSpace.zig").isSpace;
const closeNode = @import("../utils/closeNode.zig").closeNode;
const getEndOfLine = @import("../utils/getEndOfLine.zig").getEndOfLine;
const newNode = @import("../utils/newNode.zig").newNode;
const appendChild = @import("../utils/appendChild.zig").appendChild;

pub fn testStart(state: *BlockParserState, parent: *MarkdownNode) bool {
    if (parent.acceptsContent) return false;

    if (state.maybeContinue) {
        var i: usize = state.openNodes.items.len;
        while (i > 1) : (i -= 1) {
            const node = state.openNodes.items[i - 1];
            if (node.maybeContinuing) {
                return false;
            }
        }
    }

    if (state.i >= state.src.len) return false;
    const char = state.src[state.i];

    if (state.indent <= 3 and (char == '=' or char == '-')) {
        var end = state.i + 1;
        var prevChar: u8 = char;
        while (end < state.src.len) : (end += 1) {
            const nextChar = state.src[end];
            if (nextChar == char) {
                if (isSpace(prevChar)) {
                    return false;
                }
            } else if (isNewLine(nextChar)) {
                end += 1;
                break;
            } else if (!isSpace(nextChar)) {
                return false;
            }
            prevChar = nextChar;
        }

        const haveParagraph = std.mem.eql(u8, parent.type, "paragraph") and !parent.blankAfter;
        if (haveParagraph) {
            const hasNonSpace = for (parent.content) |c| {
                if (!isSpace(c)) break true;
            } else false;

            if (hasNonSpace) {
                // Remove lazy continuation indentation from the heading content
                var cleaned_content = std.ArrayList(u8).initCapacity(state.allocator, parent.content.len) catch unreachable;
                defer cleaned_content.deinit(state.allocator);

                var i: usize = 0;
                while (i < parent.content.len) {
                    if (parent.content[i] == '\n' and i + 1 < parent.content.len) {
                        // Check for continuation indentation (up to 3 spaces)
                        var j: usize = i + 1;
                        while (j < parent.content.len and j < i + 4 and isSpace(parent.content[j])) : (j += 1) {}
                        // If the next non-space character is not a newline, this is lazy continuation
                        // Skip the indentation spaces
                        if (j < i + 4 and j < parent.content.len and !isNewLine(parent.content[j])) {
                            cleaned_content.append(state.allocator, '\n') catch unreachable;
                            i = j;
                            continue;
                        }
                    }
                    cleaned_content.append(state.allocator, parent.content[i]) catch unreachable;
                    i += 1;
                }

                const new_content = cleaned_content.toOwnedSlice(state.allocator) catch unreachable;
                state.allocator.free(parent.content);
                parent.content = new_content;
                parent.content_allocated = true;

                const new_type = state.allocator.dupe(u8, "heading") catch unreachable;
                state.allocator.free(parent.type);
                parent.type = new_type;
                parent.markup = state.allocator.dupe(u8, state.src[state.i..end]) catch unreachable;
                state.i = end;
                return true;
            }
        }
    }

    return false;
}

pub fn testContinue(state: *BlockParserState, node: *MarkdownNode) bool {
    _ = state;
    _ = node;
    return false;
}

pub const headingUnderlineRule = BlockRule{
    .name = "heading_underline",
    .testStart = testStart,
    .testContinue = testContinue,
};
