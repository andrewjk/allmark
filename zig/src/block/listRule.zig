const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const parseBlock = @import("../parse/parseBlock.zig").parseBlock;
const isNumeric = @import("../utils/isAlphaNumeric.zig").isNumeric;
const isNewLine = @import("../utils/isNewLine.zig").isNewLine;
const isSpace = @import("../utils/isSpace.zig").isSpace;
const movePastMarker = @import("../utils/movePastMarker.zig").movePastMarker;
const newBlock = @import("../utils/newBlock.zig").newBlock;
const closeNode = @import("../utils/closeNode.zig").closeNode;
const appendChild = @import("../utils/appendChild.zig").appendChild;

pub fn getMarkup(state: *BlockParserState) ?ListInfo {
    if (state.i >= state.src.len) return null;
    const char = state.src[state.i];

    if (char != '-' and char != '+' and char != '*') return null;

    const is_bullet = (state.i + 1 >= state.src.len) or isSpace(state.src[state.i + 1]);

    if (!is_bullet) return null;

    var next_char: u8 = 0;
    if (state.i + 1 < state.src.len) {
        next_char = state.src[state.i + 1];
    }

    return ListInfo{
        .delimiter = char,
        .markup = state.src[state.i .. state.i + 1],
        .is_blank = (state.i + 1 >= state.src.len) or next_char == '\n' or next_char == '\r',
        .type = "list_bulleted",
    };
}

pub fn testListStart(state: *BlockParserState, parent: *MarkdownNode, end_of_line: usize, info: ?ListInfo) bool {
    if (info == null) {
        return false;
    }

    return testStartWithInfo(state, parent, end_of_line, info.?);
}

fn testStartWithInfo(state: *BlockParserState, parent: *MarkdownNode, end_of_line: usize, info: ListInfo) bool {
    var effective_parent = parent;
    var closed_node: ?*MarkdownNode = null;

    if (std.mem.eql(u8, parent.type, "paragraph") and state.openNodes.items.len == 2) {
        const info_blank = info.is_blank;
        if (info_blank) return false;

        const is_ordered_not_first = std.mem.eql(u8, info.type, "list_ordered") and !(info.markup.len == 2 and info.markup[0] == '1');
        if (is_ordered_not_first) return false;
    }

    var open_indent: i32 = state.indent;
    var i: usize = state.openNodes.items.len;
    while (i > 1) {
        i -= 1;
        const open_node = state.openNodes.items[i];
        const is_list_parent = std.mem.eql(u8, open_node.type, "list_bulleted") or std.mem.eql(u8, open_node.type, "list_ordered");
        if (is_list_parent) {
            open_indent -= open_node.indent;
            break;
        }
    }

    if (open_indent >= 4) {
        return false;
    }

    if (state.maybeContinue) {
        state.maybeContinue = false;
        var j: usize = state.openNodes.items.len;
        while (j > 1) {
            j -= 1;
            const node = state.openNodes.items[j];
            if (node.maybeContinuing) {
                node.maybeContinuing = false;
                closed_node = node;
                state.openNodes.shrinkRetainingCapacity(j);
            }
        }
        effective_parent = state.openNodes.items[state.openNodes.items.len - 1];
    }

    // If there's an open paragraph, close it
    if (std.mem.eql(u8, effective_parent.type, "paragraph")) {
        closed_node = state.openNodes.pop();
        effective_parent = state.openNodes.items[state.openNodes.items.len - 1];
    }

    const is_list_parent = std.mem.eql(u8, effective_parent.type, "list_bulleted") or std.mem.eql(u8, effective_parent.type, "list_ordered");
    const is_diff_type = is_list_parent and (effective_parent.delimiter.len == 0 or effective_parent.delimiter[0] != info.delimiter);

    if (is_diff_type) {
        const last_item: ?*MarkdownNode = if (effective_parent.children) |children| blk: {
            if (children.len > 0) break :blk children[children.len - 1] else break :blk null;
        } else null;

        if (last_item) |li| {
            if (std.mem.eql(u8, li.type, "list_item") and state.indent < li.subindent) {
                closed_node = state.openNodes.pop();
                effective_parent = state.openNodes.items[state.openNodes.items.len - 1];
            }
        }
    }

    if (closed_node) |cn| {
        closeNode(state, cn);
    }

    var spaces: usize = 0;
    var blank: bool = true;
    var j: usize = state.i + info.markup.len;
    while (j < state.src.len) : (j += 1) {
        if (state.src[j] == '\n' or state.src[j] == '\r') break;
        if (isSpace(state.src[j])) {
            spaces += 1;
        } else {
            blank = false;
            break;
        }
    }

    if (blank) {
        spaces = 1;
    }

    // "If the first block in the list item is an indented code block, then
    // by rule #2, the contents must be indented one space after the list
    // marker:"
    if (spaces > 4) {
        spaces = 1;
    }

    const has_list = std.mem.eql(u8, effective_parent.type, info.type);
    const list = if (has_list) effective_parent else newBlock(state.allocator, info.type, state.i, state.line, info.markup, state.indent) catch unreachable;
    const item = newBlock(state.allocator, "list_item", state.i, state.line, info.markup, state.indent) catch unreachable;

    if (!has_list) {
        const delimiter_char = &[_]u8{info.delimiter};
        list.delimiter = state.allocator.dupe(u8, delimiter_char) catch unreachable;
        list.delimiter_allocated = true;
    }

    const item_delimiter_char = &[_]u8{info.delimiter};
    item.delimiter = state.allocator.dupe(u8, item_delimiter_char) catch unreachable;
    item.delimiter_allocated = true;
    item.subindent = state.indent + @as(i32, @intCast(info.markup.len)) + @as(i32, @intCast(spaces));

    if (!has_list) {
        if (state.hasBlankLine and effective_parent.children != null and effective_parent.children.?.len > 0) {
            const last_child = effective_parent.children.?[effective_parent.children.?.len - 1];
            last_child.blankAfter = true;
            state.hasBlankLine = false;
        }
        appendChild(state.allocator, effective_parent, list) catch unreachable;
        state.openNodes.append(state.allocator, list) catch unreachable;
    }

    if (state.hasBlankLine and effective_parent.children != null and effective_parent.children.?.len > 0) {
        const last_child = effective_parent.children.?[effective_parent.children.?.len - 1];
        last_child.blankAfter = true;
        state.hasBlankLine = false;
    }

    if (list.children == null) {
        list.children = state.allocator.alloc(*MarkdownNode, 0) catch unreachable;
    }
    item.children = state.allocator.alloc(*MarkdownNode, 0) catch unreachable;
    appendChild(state.allocator, list, item) catch unreachable;
    state.openNodes.append(state.allocator, item) catch unreachable;

    movePastMarker(info.markup.len, state);
    state.hasBlankLine = false;
    parseBlock(state, item, end_of_line);

    return true;
}

pub fn testListContinue(state: *BlockParserState, node: *MarkdownNode, info: ?ListInfo) bool {
    return testContinue(info, state, node);
}

pub fn testContinue(info: ?ListInfo, state: *BlockParserState, node: *MarkdownNode) bool {
    if (state.i >= state.src.len) return false;
    const char = state.src[state.i];

    if (info != null) {
        if (state.hasBlankLine and state.indent >= 4) {
            return false;
        }
        if (info.?.delimiter == node.delimiter[0]) {
            return true;
        }
    }

    if (state.hasBlankLine) {
        if (node.children != null and node.children.?.len == 1) {
            const first_child = if (node.children) |children| blk: {
                if (children.len > 0) break :blk children[0] else break :blk null;
            } else null;

            if (first_child == null or first_child.?.children.?.len == 0) {
                return false;
            }
        }
    }

    if (isNewLine(char)) {
        return true;
    }

    const idx2 = state.openNodes.items.len;
    if (idx2 > 0) {
        const open_node = state.openNodes.items[idx2 - 1];
        if (std.mem.eql(u8, open_node.type, "paragraph")) {
            return true;
        }
    }

    const last_item = if (node.children) |children| blk: {
        if (children.len > 0) break :blk children[children.len - 1] else break :blk null;
    } else null;

    if (last_item) |li| {
        if (std.mem.eql(u8, li.type, "list_item") and state.indent >= li.subindent) {
            return true;
        }
    }

    return false;
}

pub fn isLooseList(node: *MarkdownNode) bool {
    var loose = false;

    if (node.children) |children| {
        if (children.len > 1) {
            var i: usize = 0;
            while (i < children.len - 1) : (i += 1) {
                const child = children[i];

                if (child.children) |cc| {
                    if (cc.len > 0) {
                        const grandchild = cc[cc.len - 1];
                        if (grandchild.blankAfter) {
                            child.blankAfter = true;
                        }
                    }
                }

                if (child.blankAfter) {
                    loose = true;
                    break;
                }
            }
        }

        for (children) |child| {
            if (child.children) |cc| {
                if (cc.len > 1) {
                    var j: usize = 0;
                    while (j < cc.len - 1) : (j += 1) {
                        const first = cc[j];
                        const second = cc[j + 1];
                        if (first.block and first.blankAfter and second.block) {
                            loose = true;
                            break;
                        }
                    }
                }
            }
            if (loose) break;
        }
    }

    return loose;
}

pub const ListInfo = struct {
    delimiter: u8,
    markup: []const u8,
    is_blank: bool,
    type: []const u8,
};
