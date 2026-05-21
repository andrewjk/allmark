const std = @import("std");

pub fn closeNode(
    state: *@import("../types/BlockParserState.zig").BlockParserState,
    node: *@import("../types/MarkdownNode.zig").MarkdownNode,
) void {
    node.length = state.i - node.index;

    const rule = state.rulesMap.get(node.type);
    if (rule) |r| {
        if (r.closeNode) |closeFn| {
            closeFn(state, node);
        }
    }
}
