const std = @import("std");

pub fn closeNode(
    state: *@import("../types/BlockParserState.zig").BlockParserState,
    node: *@import("../types/MarkdownNode.zig").MarkdownNode,
) void {
    var i = state.openNodes.items.len;
    while (i > 1) : (i -= 1) {
        const openNode = state.openNodes.items[i - 1];
        const rule = state.rules.get(openNode.type).?;
        if (rule.closeNode) |closeFn| {
            closeFn(state, openNode);
        }
        if (openNode == node) {
            break;
        }
    }
}
