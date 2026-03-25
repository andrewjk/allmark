const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const closeNode = @import("../utils/closeNode.zig").closeNode;
const movePastMarker = @import("../utils/movePastMarker.zig").movePastMarker;
const newBlock = @import("../utils/newBlock.zig").newBlock;
const appendChild = @import("../utils/appendChild.zig").appendChild;
const getEndOfLine = @import("../utils/getEndOfLine.zig").getEndOfLine;

pub fn testStart(state: *BlockParserState, parent: *MarkdownNode) bool {
    if (parent.acceptsContent) return false;

    if (state.i >= state.src.len) return false;
    const char = state.src[state.i];

    if (state.indent > 3 or char != '>') return false;

    var pos = state.i + 1;
    if (pos >= state.src.len) return false;

    while (pos < state.src.len and isSpace(state.src[pos])) {
        pos += 1;
    }

    if (pos >= state.src.len) return false;
    if (state.src[pos] != '[') return false;

    const exclamation_pos = pos + 1;
    if (exclamation_pos >= state.src.len) return false;
    if (state.src[exclamation_pos] != '!') return false;

    const alert_start = exclamation_pos + 1;
    if (alert_start >= state.src.len) return false;

    const end_pos = std.mem.indexOfScalarPos(u8, state.src[alert_start..], 0, ']') orelse state.src.len;
    if (end_pos == state.src.len) return false;

    const alert_types = &[_][]const u8{ "note", "tip", "important", "warning", "caution" };
    var found_alert: ?[]const u8 = null;
    for (alert_types) |alert_type| {
        const alert_len = alert_type.len;
        if (end_pos >= alert_len) {
            if (std.ascii.eqlIgnoreCase(state.src[alert_start .. alert_start + alert_len], alert_type)) {
                found_alert = alert_type;
                break;
            }
        }
    }

    if (found_alert == null) return false;

    var effective_parent = parent;
    var closed_node: ?*MarkdownNode = null;

    if (std.mem.eql(u8, parent.type, "paragraph")) {
        const idx = state.openNodes.items.len;
        if (idx > 0) {
            effective_parent = state.openNodes.items[idx - 1];
            closed_node = state.openNodes.pop();
        }
    }

    if (closed_node) |cn| {
        closeNode(state, cn);
    }

    const quote = newBlock(state.allocator, "alert", state.i, state.line, found_alert.?, state.indent + 1) catch unreachable;
    appendChild(state.allocator, effective_parent, quote) catch unreachable;
    state.openNodes.append(state.allocator, quote) catch unreachable;

    state.i = getEndOfLine(state);

    return true;
}

fn isSpace(char: u8) bool {
    return char == ' ' or char == '\t';
}

pub fn testContinue(state: *BlockParserState, node: *MarkdownNode) bool {
    if (state.i >= state.src.len) return false;
    const char = state.src[state.i];

    if (state.indent <= 3 and char == '>') {
        movePastMarker(1, state);
        return true;
    }

    if (state.hasBlankLine) return false;

    const idx = state.openNodes.items.len;
    if (idx > 0) {
        const open_node = state.openNodes.items[idx - 1];
        if (std.mem.eql(u8, open_node.type, "paragraph")) {
            state.maybeContinue = true;
            node.maybeContinuing = true;
            return true;
        }
    }

    return false;
}

pub fn closeNodeFn(state: *BlockParserState, node: *MarkdownNode) void {
    if (state.hasBlankLine) {
        const last_child = if (node.children) |children| blk: {
            if (children.len > 0) {
                break :blk children[children.len - 1];
            }
            break :blk null;
        } else null;

        if (last_child) |lc| {
            lc.blankAfter = true;
        }
        state.hasBlankLine = false;
    }
}

pub const alertRule = BlockRule{
    .name = "alert",
    .testStart = testStart,
    .testContinue = testContinue,
    .closeNode = closeNodeFn,
};
