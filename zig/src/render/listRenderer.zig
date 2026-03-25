const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const renderChildrenFn = @import("renderChildren.zig").renderChildren;
const renderUtils = @import("renderUtils.zig");
const renderNodeFn = @import("renderNode.zig").renderNode;

pub fn render(node: *const MarkdownNode, state: *RendererState, decode: ?bool) void {
    _ = decode;

    const ordered = std.mem.eql(u8, node.type, "list_ordered");
    const markup_len = node.markup.len;
    var start: []const u8 = "";

    if (ordered and markup_len > 0) {
        const start_str = node.markup[0 .. markup_len - 1];
        const start_num = std.fmt.parseInt(usize, start_str, 10) catch 1;
        if (start_num != 1) {
            const buf = std.fmt.allocPrint(state.allocator, " start=\"{d}\"", .{start_num}) catch unreachable;
            start = buf;
        }
    }
    defer if (start.len > 0) state.allocator.free(start);

    renderUtils.startNewLine(node, state);

    const tag = if (ordered) "ol" else "ul";
    const open_tag = std.fmt.allocPrint(state.allocator, "<{s}{s}>", .{ tag, start }) catch unreachable;
    defer state.allocator.free(open_tag);
    state.output.appendSlice(state.allocator, open_tag) catch unreachable;
    renderUtils.innerNewLine(node, state);

    var loose = false;

    if (node.children) |children| {
        if (children.len > 1) {
            var i: usize = 0;
            while (i < children.len - 1) : (i += 1) {
                const child = children[i];
                var grandchild: ?*MarkdownNode = null;
                if (child.children) |c| {
                    if (c.len > 0) {
                        grandchild = c[c.len - 1];
                    }
                }

                if (grandchild) |gc| {
                    if (gc.blankAfter) {
                        child.blankAfter = true;
                    }
                }

                if (child.blankAfter) {
                    loose = true;
                    break;
                }
            }

            i = 0;
            while (i < children.len) : (i += 1) {
                const child = children[i];
                if (child.children) |cc| {
                    if (cc.len > 1) {
                        var j: usize = 0;
                        while (j < cc.len - 1) : (j += 1) {
                            const first_child = cc[j];
                            const second_child = cc[j + 1];

                            if (first_child.block and first_child.blankAfter and second_child.block) {
                                loose = true;
                                break;
                            }
                        }
                    }
                }
                if (loose) break;
            }
        } else if (children.len == 1) {
            // Check single item list for loose condition
            const child = children[0];
            if (child.children) |cc| {
                if (cc.len > 1) {
                    var j: usize = 0;
                    while (j < cc.len - 1) : (j += 1) {
                        const first_child = cc[j];
                        const second_child = cc[j + 1];

                        if (first_child.block and first_child.blankAfter and second_child.block) {
                            loose = true;
                            break;
                        }
                    }
                }
            }
        }

        for (children) |item| {
            state.output.appendSlice(state.allocator, "<li>") catch unreachable;

            if (item.children) |ic| {
                for (ic, 0..) |child, ii| {
                    if (!loose and std.mem.eql(u8, child.type, "paragraph")) {
                        renderChildrenFn(child, state, true);
                    } else {
                        if (ii == 0) {
                            renderUtils.innerNewLine(item, state);
                        }
                        renderNodeFn(child, state, true);
                        if (ii == ic.len - 1 and child.block) {
                            const output_slice = state.output.items;
                            if (output_slice.len > 0 and output_slice[output_slice.len - 1] != '\n') {
                                state.output.append(state.allocator, '\n') catch unreachable;
                            }
                        }
                    }
                }
            }

            state.output.appendSlice(state.allocator, "</li>") catch unreachable;
            renderUtils.endNewLine(node, state);
        }
    }

    const close_tag = std.fmt.allocPrint(state.allocator, "</{s}>", .{tag}) catch unreachable;
    defer state.allocator.free(close_tag);
    state.output.appendSlice(state.allocator, close_tag) catch unreachable;
    renderUtils.endNewLine(node, state);
}

pub const listBulletedRenderer = Renderer{
    .name = "list_bulleted",
    .render = render,
};

pub const listOrderedRenderer = Renderer{
    .name = "list_ordered",
    .render = render,
};
