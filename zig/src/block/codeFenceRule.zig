const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const closeNode = @import("../utils/closeNode.zig").closeNode;
const decodeEntities = @import("../utils/decodeEntities.zig").decodeEntities;
const escapeBackslashes = @import("../utils/escapeBackslashes.zig").escapeBackslashes;
const getEndOfLine = @import("../utils/getEndOfLine.zig").getEndOfLine;
const isNewLine = @import("../utils/isNewLine.zig").isNewLine;
const isSpaceFn = @import("../utils/isSpace.zig").isSpace;
const newNode = @import("../utils/newNode.zig").newNode;

pub fn testStart(state: *BlockParserState, parent: *MarkdownNode) bool {
    if (parent.acceptsContent) {
        return false;
    }

    if (state.i >= state.src.len) return false;

    const char = state.src[state.i];
    if (state.indent <= 3 and (char == '`' or char == '~')) {
        var matched: usize = 1;
        var end = state.i + 1;
        var haveSpace = false;
        while (end < state.src.len) : (end += 1) {
            const nextChar = state.src[end];
            if (nextChar == char) {
                if (haveSpace) {
                    return false;
                }
                matched += 1;
            } else if (isNewLine(&.{nextChar})) {
                break;
            } else if (isSpaceFn(state.src[end])) {
                haveSpace = true;
            } else {
                break;
            }
        }

        if (matched >= 3) {
            var closedNode: ?*MarkdownNode = null;

            const markup = try state.allocator.alloc(u8, matched);
            defer state.allocator.free(markup);
            @memset(markup, char);

            var info: []const u8 = "";
            if (!isNewLine(&.{state.src[state.i + matched]})) {
                const eol = getEndOfLine(state);
                info = state.src[state.i + matched .. eol];

                if (char == '`' and std.mem.indexOfScalar(u8, info, '`') != null) {
                    return false;
                }

                info = try decodeEntities(state.allocator, info);
                defer state.allocator.free(info);
                const escaped = try escapeBackslashes(state.allocator, info);
                state.allocator.free(info);
                info = escaped;
            } else {
                end += 1;
            }

            if (state.maybeContinue) {
                state.maybeContinue = false;
                var i = state.openNodes.items.len;
                while (i > 0) : (i -= 1) {
                    const node = state.openNodes.items[i - 1];
                    if (node.maybeContinuing) {
                        node.maybeContinuing = false;
                        closedNode = node;
                        state.openNodes.shrinkRetainingCapacity(i);
                        break;
                    }
                }
                parent = state.openNodes.items[state.openNodes.items.len - 1];
            }

            if (std.mem.eql(u8, parent.type, "paragraph")) {
                closedNode = state.openNodes.pop();
                parent = state.openNodes.items[state.openNodes.items.len - 1];
            }

            if (closedNode != null) {
                closeNode(state, closedNode.?);
            }

            const code = try newNode(state.allocator, "code_fence", true, state.i, state.line, 1, markup, state.indent, null);
            code.*.acceptsContent = true;
            code.*.info = try state.allocator.dupe(u8, info);

            state.i = end;

            if (state.hasBlankLine and parent.children != null and parent.children.?.len > 0) {
                const last_child = parent.children.?[parent.children.?.len - 1];
                last_child.?.blankAfter = true;
                state.hasBlankLine = false;
            }

            parent.children.?.append(code) catch unreachable;
            state.openNodes.append(code) catch unreachable;

            return true;
        }
    }

    return false;
}

pub fn testContinue(state: *BlockParserState, node: *MarkdownNode) bool {
    if (state.hasBlankLine) {
        const indent = " " ** node.indent;
        node.*.content = try std.fmt.allocPrint(state.allocator, "{s}{s}", .{ node.content, indent });
        return true;
    }

    const char = state.src[state.i];
    if (state.indent <= 3 and (char == '`' or char == '~')) {
        if (node.markup.len > 0 and node.markup[0] == char) {
            var endMatched: usize = 0;
            var end = state.i;
            while (end < state.src.len) : (end += 1) {
                const nextChar = state.src[end];
                if (nextChar == char) {
                    endMatched += 1;
                } else {
                    break;
                }
            }

            if (endMatched >= node.markup.len) {
                while (end < state.src.len) : (end += 1) {
                    const nextChar = state.src[end];
                    if (isNewLine(&.{nextChar})) {
                        break;
                    } else if (isSpaceFn(state.src[end])) {
                        continue;
                    } else {
                        return true;
                    }
                }

                state.i = end;
                return false;
            }
        }
    }

    return true;
}

pub const codeFenceRule = BlockRule{
    .name = "code_fence",
    .testStart = testStart,
    .testContinue = testContinue,
};
