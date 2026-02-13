const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const isNumeric = @import("../utils/isAlphaNumeric.zig").isNumeric;
const isNewLine = @import("../utils/isNewLine.zig").isNewLine;
const isSpace = @import("../utils/isSpace.zig").isSpace;
const movePastMarker = @import("../utils/movePastMarker.zig").movePastMarker;
const newNode = @import("../utils/newNode.zig").newNode;

pub fn getMarkup(state: *BlockParserState) ?ListInfo {
    if (state.i >= state.src.len) return null;
    const char = state.src[state.i];

    const is_bullet = (char == '-' or char == '+' or char == '*') and
        (state.i == state.src.len - 1 or isNewLine(state.src[state.i + 1]));

    if (!is_bullet) return null;

    return ListInfo{
        .delimeter = char,
        .markup = &[_]u8{char},
        .is_blank = is_bullet and (state.i == state.src.len - 1 or isNewLine(state.src[state.i + 1])),
        .type = "list_bulleted",
    };
}

pub fn testStart(state: * *BlockParserState, parent: *MarkdownNode) bool {
    if (parent.acceptsContent) return false;

    const info = getMarkup(state);
    if (info == null) return false;

    var effective_parent = parent;
    var closed_node: ?*MarkdownNode = null;

    const is_list_parent = std.mem.eql(u8, parent.type, "list_bulleted") or std.mem.eql(u8, parent.type, "list_ordered");

    if (parent.type == "paragraph" and state.openNodes.items.len == 2) {
        const info_blank = info.?.is_blank orelse false;
        if (info_blank) return false;

        const is_ordered_first = info.?.type == "list_ordered" and std.mem.eql(u8, info.markup, "1") orelse false;
        if (!is_ordered_first) return false;
    }

    var open_indent: i32 = state.indent;
    var i: usize = state.openNodes.items.len;
    while (i > 1) : (i -= 1) {
        const open_node = state.openNodes.items[i];
        if (is_list_parent) {
            open_indent -= open_node.indent;
            break;
        }
    }

    if (open_indent >= 4) return false;

    if (state.maybeContinue) {
        state.maybeContinue = false;
        var i: usize = state.openNodes.items.len;
        while (i > 1) : (i -= 1) {
            const node = state.openNodes.items[i];
            if (node.maybeContinuing) {
                node.maybeContinuing = false;
                closed_node = node;
                state.openNodes.shrinkRetainingCapacity(i);
                break;
            }
        }
        effective_parent = state.openNodes.items[state.openNodes.items.len - 1];
    }

    if (parent.type == "paragraph") {
        const idx = state.openNodes.items.len;
        if (idx > 0) {
            closed_node = state.openNodes.pop();
        }
    }

    const is_diff_type = is_list_parent and (parent.delimiter != info.?.delimiter orelse "");

    if (is_diff_type) {
        const last_item = if (parent.children) |children| {
            if (children.len > 0) children[children.len - 1] else null;
        } else null;

        if (last_item) |li| {
            if (li.type == "list_item" and state.indent < li.subindent) {
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
        if (isNewLine(state.src[j])) break;
        if (isSpace(state.src[j])) {
            spaces += 1;
        } else {
            blank = false;
            break;
        }
    }

    if (blank) {
        spaces = @min(spaces, 1);
    }

    const list = newNode(state.allocator, info.?.type orelse "list_bulleted", true, state.i, state.line, 1, info.markup, state.indent, null) catch unreachable;
    const item = newNode(state.allocator, "list_item", true, state.i, state.line, 1, info.markup, state.indent + info.markup.len + spaces, null) catch unreachable;

    const has_list = std.mem.eql(u8, parent.type, info.?.type orelse "list_bulleted");
    if (has_list) {
        if (state.hasBlankLine and parent.children != null and parent.children.?.len > 0) {
            const last_child = parent.children.?[parent.children.?.len - 1];
            if (last_child) |lc| {
                lc.blankAfter = true;
            }
            state.hasBlankLine = false;
        }
        parent.children.?.append(list) catch unreachable;
        state.openNodes.append(list) catch unreachable;
    }

    if (state.hasBlankLine and parent.children != null and parent.children.?.len > 0) {
        const last_child = parent.children.?[parent.children.?.len - 1];
        if (last_child) |lc| {
            lc.blankAfter = true;
        }
        state.hasBlankLine = false;
    }

    item.children = try state.allocator.alloc(*MarkdownNode, 0);
    item.children.?.* = try state.allocator.dupe(*MarkdownNode, &.{});
    list.children = try state.allocator.alloc(*MarkdownNode, 0);
    list.children.?.* = try state.allocator.dupe(*MarkdownNode, &.{});
    list.children.?.append(item) catch unreachable;
    state.openNodes.append(item) catch unreachable;

    movePastMarker(info.markup.len, state);
    state.hasBlankLine = false;

    return true;
}

pub fn testContinue(state: *BlockParserState, node: *MarkdownNode) bool {
    if (state.i >= state.src.len) return false;
    const char = state.src[state.i];

    if (getMarkup(state)) |info| {
        if (state.hasBlankLine and state.indent >= 4) {
            return false;
        }
        if (info.delimiter == node.delimiter) {
            return true;
        }
    }

    const idx = state.openNodes.items.len;
    if (idx > 0) {
        const open_node = state.openNodes.items[idx - 1];
        if (std.mem.eql(u8, open_node.type, "paragraph")) {
            return true;
        }
    }

    if (state.hasBlankLine) {
        if (node.children != null and node.children.?.len == 1) {
            const first_child = if (node.children) |children| {
                if (children.len > 0) children[0] else null;
            } else null;

            if (first_child == null or (first_child.?.children.?.len == 0) {
                return false;
            }
        }
    }

    if (isNewLine(char)) {
        return true;
    }

    const idx = state.openNodes.items.len;
    if (idx > 0) {
        const open_node = state.openNodes.items[idx - 1];
        if (open_node.type == "paragraph") {
            return true;
        }
    }

    const last_item = if (node.children) |children| {
        if (children.len > 0) children[children.len - 1] else null;
    } else null;

    if (last_item) |li| {
        if (li.type == "list_item" and state.indent >= li.subindent) {
            return true;
        }
    }

    return false;
}

const ListInfo = struct {
    delimiter: u8,
    markup: []const u8,
    is_blank: bool,
    type: []const u8,
};

pub fn testListStart(state: *BlockParserState, parent: *MarkdownNode, info: ?ListInfo) bool {
    if (info == null) return false;
    _ = testStart(state, parent);
}

pub fn testListContinue(state: *BlockParserState, node: *MarkdownNode, info: ?ListInfo) bool {
    _ = node;
    return testContinue(state, node, info);
}

pub const listBulletedRule = BlockRule{
    .name = "list_bulleted",
    .testStart = testListStart,
    .testContinue = testListContinue,
};

pub const listOrderedRule = BlockRule{
    .name = "list_ordered",
    .testStart = testListStart,
    .testContinue = testListContinue,
};
