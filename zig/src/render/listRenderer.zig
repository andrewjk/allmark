const std = @import("std");
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const RendererState = @import("../types/RendererState.zig").RendererState;
const Renderer = @import("../types/Renderer.zig").Renderer;
const renderChildren = @import("renderChildren.zig").renderChildren;
const renderUtils = @import("renderUtils.zig");
const renderNode = @import("renderNode.zig").renderNode;

pub fn render(node: *const MarkdownNode, state: *RendererState, first: ?bool, last: ?bool, decode: ?bool) void {
    _ = first;
    _ = last;
    _ = decode;

    const ordered = std.mem.eql(u8, node.type, "list_ordered");
    const markup_len = node.markup.len;
    var start: []const u8 = "";

    if (ordered and markup_len > 0) {
        const start_str = node.markup[0 .. markup_len - 1];
        const start_num = std.fmt.parseInt(usize, start_str, 10) orelse 1;
        if (start_num != 1) {
            const buf = try std.fmt.allocPrint(state.output.allocator, " start=\"{d}\"", .{start_num});
            defer state.output.allocator.free(buf);
            start = buf;
        }
    }

    renderUtils.startNewLine(node, state);

    const tag = if (ordered) "ol" else "ul";
    state.output.writer().print("<{s}{s}>", .{ tag, start }) catch unreachable;
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
        }

        for (children) |item| {
            state.output.appendSlice("<li>") catch unreachable;

            if (item.children) |ic| {
                for (ic, 0..) |child, ii| {
                    if (!loose and std.mem.eql(u8, child.type, "paragraph")) {
                        if (child.children) |cc| {
                            for (cc) |grandchild| {
                                renderNode.renderNode(grandchild, state, ii == ic.len - 1, ii == ic.len - 1, true);
                            }
                        }
                    } else {
                        if (ii == 0) {
                            renderUtils.innerNewLine(item, state);
                        }
                        renderNode.renderNode(child, state, ii == ic.len - 1, false, true);
                    }
                }
            }

            state.output.appendSlice("</li>") catch unreachable;
            renderUtils.endNewLine(node, state);
        }
    }

    state.output.writer().print("</{s}>", .{tag}) catch unreachable;
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
