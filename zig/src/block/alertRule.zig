const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const closeNode = @import("../utils/closeNode.zig").closeNode;
const movePastMarker = @import("../utils/movePastMarker.zig").movePastMarker;
const newNode = @import("../utils/newNode.zig").newNode;

pub fn testStart(state: *BlockParserState, parent: *MarkdownNode) bool {
    if (state.i >= state.src.len) return false;
    const char = state.src[state.i];

    if (state.indent > 3 or char != '>') return false;

    const match_pos = state.i + 1;
    if (match_pos >= state.src.len) return false;

    if (std.ascii.toLower(state.src[match_pos]) != '[') return false;

    const end_pos = std.mem.indexOfScalarPos(u8, state.src[match_pos..], ']') orelse state.src.len;
    if (end_pos == state.src.len) return false;

    if (parent.children == null) return false;

    const alert_types = &[_][]const u8{ "note", "tip", "important", "warning", "caution" };
    var found_alert: ?[]const u8 = null;
    for (alert_types) |alert_type| {
        const alert_len = alert_type.len;
        if (end_pos - match_pos >= alert_len + 2) {
            if (std.ascii.eqlIgnoreCase(state.src[match_pos + 1 .. match_pos + 1 + alert_len], alert_type.*)) {
                found_alert = alert_type.*;
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

    const alert_type_lower = std.ascii.allocLowerPrint(state.allocator) catch unreachable;
    defer state.allocator.free(alert_type_lower);

    for (found_alert.?) |fa| {
        for (fa, 0..) |c, i| {
            alert_type_lower[i] = std.ascii.toLower(c);
        }
    }

    const quote = newNode(state.allocator, "alert", true, state.i, state.line, 1, alert_type_lower, state.indent + 1, null) catch unreachable;
    effective_parent.children.?.append(quote) catch unreachable;
    state.openNodes.append(quote) catch unreachable;

    state.i = end_pos + 1;

    return true;
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
            if (children.len > 0) children[children.len - 1] else null;
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
