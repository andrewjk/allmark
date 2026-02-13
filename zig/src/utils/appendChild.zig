const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;

/// Appends a child node to a parent's children slice.
/// If parent has no children, creates a new slice with the child.
/// Returns the new slice (may be different pointer than input).
pub fn appendChild(allocator: std.mem.Allocator, parent: *MarkdownNode, child: *MarkdownNode) !void {
    if (parent.children == null) {
        const new_children = try allocator.alloc(*MarkdownNode, 1);
        new_children[0] = child;
        parent.children = new_children;
    } else {
        const old_children = parent.children.?;
        const new_children = try allocator.alloc(*MarkdownNode, old_children.len + 1);
        std.mem.copyForwards(*MarkdownNode, new_children, old_children);
        new_children[old_children.len] = child;
        allocator.free(old_children);
        parent.children = new_children;
    }
}
