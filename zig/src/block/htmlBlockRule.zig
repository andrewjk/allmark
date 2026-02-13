const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const closeNode = @import("../utils/closeNode.zig").closeNode;
const newNode = @import("../utils/newNode.zig").newNode;
const appendChild = @import("../utils/appendChild.zig").appendChild;
const getEndOfLine = @import("../utils/getEndOfLine.zig").getEndOfLine;
const isSpace = @import("../utils/isSpace.zig").isSpace;
const isNewLine = @import("../utils/isNewLine.zig").isNewLine;
const isEscaped = @import("../utils/isEscaped.zig").isEscaped;
const mvzr = @import("mvzr");

// HTML block condition regexes (case-insensitive where noted)
// Note: mvzr regex patterns don't support non-capturing groups (?:)
// Or `$`??
// Or case-insensitive matches
const HTML_REGEX_1 = "^<(script|SCRIPT|pre|PRE|style|STYLE|textarea|TEXTAREA)(\\s|\\n|>)";
const HTML_REGEX_2 = "^<!--.+?-->";
const HTML_REGEX_3 = "^<\\?.+?\\?>";
const HTML_REGEX_4 = "^<![A-Z].+>";
const HTML_REGEX_5 = "^<!\\[CDATA\\[.+\\]\\]>";

const HtmlTags = struct {
    const script = "script";
    const pre = "pre";
    const style = "style";
    const textarea = "textarea";
};

pub fn testStart(state: *BlockParserState, parent: *MarkdownNode) bool {
    if (parent.acceptsContent) return false;

    if (state.i >= state.src.len) return false;
    const char = state.src[state.i];

    if (state.indent <= 3 and char == '<' and !isEscaped(state.src, state.i)) {
        const tail = state.src[state.i..];

        if (testHtmlCondition1(state, parent, tail)) {
            //std.debug.print("MATCHED 1\n", .{});
            return true;
        }
        if (testHtmlCondition2(state, parent, tail)) {
            //std.debug.print("MATCHED 2\n", .{});
            return true;
        }
        if (testHtmlCondition3(state, parent, tail)) {
            //std.debug.print("MATCHED 3\n", .{});
            return true;
        }
        if (testHtmlCondition4(state, parent, tail)) {
            //std.debug.print("MATCHED 4\n", .{});
            return true;
        }
        if (testHtmlCondition5(state, parent, tail)) {
            //std.debug.print("MATCHED 5\n", .{});
            return true;
        }
        if (testHtmlCondition6(state, parent, tail)) {
            //std.debug.print("MATCHED 6\n", .{});
            return true;
        }
        if (testHtmlCondition7(state, parent, tail)) {
            //std.debug.print("MATCHED 7\n", .{});
            return true;
        }
    }

    return false;
}

fn testHtmlCondition1(state: *BlockParserState, parent: *MarkdownNode, tail: []const u8) bool {
    const regex = mvzr.compile(HTML_REGEX_1) orelse return false;
    const match = regex.match(tail);

    if (match != null and match.?.start == 0) {
        const start = state.i;

        // Extract the tag name
        const tag_match = match.?.slice;
        var tag_name_start: usize = 1;
        if (tag_match.len > 0 and tag_match[0] == '<') {
            if (tag_match.len > 1 and tag_match[1] == '/') {
                tag_name_start = 2;
            }
        }
        var tag_name_end = tag_name_start;
        while (tag_name_end < tag_match.len) : (tag_name_end += 1) {
            const c = tag_match[tag_name_end];
            if (c == ' ' or c == '\n' or c == '>' or c == '/') {
                break;
            }
        }
        const tag_name = tag_match[tag_name_start..tag_name_end];

        var effective_parent = parent;
        if (std.mem.eql(u8, parent.type, "paragraph")) {
            const idx = state.openNodes.items.len;
            if (idx > 0) {
                effective_parent = state.openNodes.items[idx - 2];
                const closed_node = state.openNodes.pop();
                if (closed_node) |cn| closeNode(state, cn);
            }
        }

        const closing_tag = std.fmt.allocPrint(state.allocator, "</{s}>", .{tag_name}) catch unreachable;
        defer state.allocator.free(closing_tag);

        var end = start;
        while (end < state.src.len) : (end += 1) {
            if (state.src[end] == '<' and end + 1 < state.src.len and state.src[end + 1] == '/') {
                if (end + closing_tag.len <= state.src.len) {
                    const potential_closing = state.src[end .. end + closing_tag.len];
                    if (std.ascii.eqlIgnoreCase(potential_closing, closing_tag)) {
                        state.i = end;
                        end = getEndOfLine(state);
                        break;
                    }
                }
            }
        }

        createHtmlBlock(state, effective_parent, start, end, 1);

        return true;
    }

    return false;
}

fn createHtmlContent(state: *BlockParserState, start: usize, end: usize) []const u8 {
    const base_content = state.src[start..end];
    if (state.indent > 0) {
        // Add indentation to the content
        const indent_len: usize = @intCast(state.indent);
        var content = state.allocator.alloc(u8, indent_len + base_content.len) catch unreachable;
        var i: usize = 0;
        while (i < indent_len) : (i += 1) {
            content[i] = ' ';
        }
        @memcpy(content[indent_len..], base_content);
        return content;
    } else {
        return state.allocator.dupe(u8, base_content) catch unreachable;
    }
}

fn testHtmlCondition2(state: *BlockParserState, parent: *MarkdownNode, tail: []const u8) bool {
    const regex = mvzr.compile(HTML_REGEX_2) orelse return false;
    const match = regex.match(tail);

    if (match != null and match.?.start == 0) {
        const start = state.i;

        var effective_parent = parent;
        if (std.mem.eql(u8, parent.type, "paragraph")) {
            const idx = state.openNodes.items.len;
            if (idx > 0) {
                effective_parent = state.openNodes.items[idx - 2];
                const closed_node = state.openNodes.pop();
                if (closed_node) |cn| closeNode(state, cn);
            }
        }

        state.i += match.?.end;
        const end_of_line = getEndOfLine(state);
        const html = newNode(state.allocator, "html_block", true, start, state.line, 1, "", 2, null) catch unreachable;
        html.content = createHtmlContent(state, start, end_of_line);
        html.content_allocated = true;

        if (state.hasBlankLine and effective_parent.children != null and effective_parent.children.?.len > 0) {
            const last_child = effective_parent.children.?[effective_parent.children.?.len - 1];
            last_child.blankAfter = true;
            state.hasBlankLine = false;
        }

        appendChild(state.allocator, effective_parent, html) catch unreachable;
        state.openNodes.append(state.allocator, html) catch unreachable;
        state.i = end_of_line;
        return true;
    }

    return false;
}

fn testHtmlCondition3(state: *BlockParserState, parent: *MarkdownNode, tail: []const u8) bool {
    const regex = mvzr.compile(HTML_REGEX_3) orelse return false;
    const match = regex.match(tail);

    if (match != null and match.?.start == 0) {
        const start = state.i;

        var effective_parent = parent;
        if (std.mem.eql(u8, parent.type, "paragraph")) {
            const idx = state.openNodes.items.len;
            if (idx > 0) {
                effective_parent = state.openNodes.items[idx - 2];
                const closed_node = state.openNodes.pop();
                if (closed_node) |cn| closeNode(state, cn);
            }
        }

        state.i += match.?.end;
        const end_of_line = getEndOfLine(state);
        const html = newNode(state.allocator, "html_block", true, start, state.line, 1, "", 3, null) catch unreachable;
        html.content = createHtmlContent(state, start, end_of_line);
        html.content_allocated = true;

        if (state.hasBlankLine and effective_parent.children != null and effective_parent.children.?.len > 0) {
            const last_child = effective_parent.children.?[effective_parent.children.?.len - 1];
            last_child.blankAfter = true;
            state.hasBlankLine = false;
        }

        appendChild(state.allocator, effective_parent, html) catch unreachable;
        state.openNodes.append(state.allocator, html) catch unreachable;
        state.i = end_of_line;
        return true;
    }

    return false;
}

fn testHtmlCondition4(state: *BlockParserState, parent: *MarkdownNode, tail: []const u8) bool {
    const regex = mvzr.compile(HTML_REGEX_4) orelse return false;
    const match = regex.match(tail);

    if (match != null and match.?.start == 0) {
        const start = state.i;

        var effective_parent = parent;
        if (std.mem.eql(u8, parent.type, "paragraph")) {
            const idx = state.openNodes.items.len;
            if (idx > 0) {
                effective_parent = state.openNodes.items[idx - 2];
                const closed_node = state.openNodes.pop();
                if (closed_node) |cn| closeNode(state, cn);
            }
        }

        state.i += match.?.end;
        const end_of_line = getEndOfLine(state);
        const html = newNode(state.allocator, "html_block", true, start, state.line, 1, "", 4, null) catch unreachable;
        html.content = createHtmlContent(state, start, end_of_line);
        html.content_allocated = true;

        if (state.hasBlankLine and effective_parent.children != null and effective_parent.children.?.len > 0) {
            const last_child = effective_parent.children.?[effective_parent.children.?.len - 1];
            last_child.blankAfter = true;
            state.hasBlankLine = false;
        }

        appendChild(state.allocator, effective_parent, html) catch unreachable;
        state.openNodes.append(state.allocator, html) catch unreachable;
        state.i = end_of_line;
        return true;
    }

    return false;
}

fn testHtmlCondition5(state: *BlockParserState, parent: *MarkdownNode, tail: []const u8) bool {
    const regex = mvzr.compile(HTML_REGEX_5) orelse return false;
    const match = regex.match(tail);

    if (match != null and match.?.start == 0) {
        const start = state.i;

        var effective_parent = parent;
        if (std.mem.eql(u8, parent.type, "paragraph")) {
            const idx = state.openNodes.items.len;
            if (idx > 0) {
                effective_parent = state.openNodes.items[idx - 2];
                const closed_node = state.openNodes.pop();
                if (closed_node) |cn| closeNode(state, cn);
            }
        }

        state.i += match.?.end;
        const end_of_line = getEndOfLine(state);
        const html = newNode(state.allocator, "html_block", true, start, state.line, 1, "", 5, null) catch unreachable;
        html.content = createHtmlContent(state, start, end_of_line);
        html.content_allocated = true;

        if (state.hasBlankLine and effective_parent.children != null and effective_parent.children.?.len > 0) {
            const last_child = effective_parent.children.?[effective_parent.children.?.len - 1];
            last_child.blankAfter = true;
            state.hasBlankLine = false;
        }

        appendChild(state.allocator, effective_parent, html) catch unreachable;
        state.openNodes.append(state.allocator, html) catch unreachable;
        state.i = end_of_line;
        return true;
    }

    return false;
}

// Simplified regex that matches any tag
const HTML_REGEX_6 = "^<\\/?([a-zA-Z][a-zA-Z0-9-]*)(\\s|\\n|>|\\/>)";

// List of block-level tags (from CommonMark spec)
const BLOCK_LEVEL_TAGS = [_][]const u8{
    "address", "article", "aside", "base", "basefont", "blockquote", "body", "caption", "center", "col", "colgroup", "dd", "details", "dialog", "dir", "div", "dl", "dt", "fieldset", "figcaption", "figure", "footer", "form", "frame", "frameset", "h1", "h2", "h3", "h4", "h5", "h6", "head", "header", "hr", "html", "iframe", "legend", "li", "link", "main", "menu", "menuitem", "nav", "noframes", "ol", "optgroup", "option", "p", "param", "section", "source", "summary", "table", "tbody", "td", "tfoot", "th", "thead", "title", "tr", "track", "ul",
};

fn isBlockLevelTag(tag_name: []const u8) bool {
    for (BLOCK_LEVEL_TAGS) |tag| {
        if (std.ascii.eqlIgnoreCase(tag_name, tag)) {
            return true;
        }
    }
    return false;
}

fn testHtmlCondition6(state: *BlockParserState, parent: *MarkdownNode, tail: []const u8) bool {
    const regex = mvzr.compile(HTML_REGEX_6) orelse return false;
    const match = regex.match(tail);

    if (match != null and match.?.start == 0) {
        const start = state.i;

        // Extract the tag name
        const tag_match = match.?.slice;
        var tag_name_start: usize = 1;
        if (tag_match.len > 0 and tag_match[0] == '<') {
            if (tag_match.len > 1 and tag_match[1] == '/') {
                tag_name_start = 2;
            }
        }
        var tag_name_end = tag_name_start;
        while (tag_name_end < tag_match.len) : (tag_name_end += 1) {
            const c = tag_match[tag_name_end];
            if (c == ' ' or c == '\n' or c == '>' or c == '/') {
                break;
            }
        }
        const tag_name = tag_match[tag_name_start..tag_name_end];

        // Check if it's a block-level tag
        if (!isBlockLevelTag(tag_name)) {
            return false;
        }

        var effective_parent = parent;
        if (std.mem.eql(u8, parent.type, "paragraph")) {
            const idx = state.openNodes.items.len;
            if (idx > 0) {
                effective_parent = state.openNodes.items[idx - 2];
                const closed_node = state.openNodes.pop();
                if (closed_node) |cn| closeNode(state, cn);
            }
        }

        const end_of_line = getEndOfLine(state);
        const html = newNode(state.allocator, "html_block", true, start, state.line, 1, "", 6, null) catch unreachable;
        html.content = createHtmlContent(state, start, end_of_line);
        html.content_allocated = true;
        html.acceptsContent = true;

        if (state.hasBlankLine and effective_parent.children != null and effective_parent.children.?.len > 0) {
            const last_child = effective_parent.children.?[effective_parent.children.?.len - 1];
            last_child.blankAfter = true;
            state.hasBlankLine = false;
        }

        appendChild(state.allocator, effective_parent, html) catch unreachable;
        state.openNodes.append(state.allocator, html) catch unreachable;
        state.i = end_of_line;
        return true;
    }

    return false;
}

// NOTE: removed non-capturing groups `(?:)`
// NOTE: removed `$` anchor as mvzr doesn't support it correctly
const htmlPatterns = @import("../utils/htmlPatterns.zig");
const HTML_REGEX_7 = "^(" ++ htmlPatterns.OPEN_TAG ++ "|" ++ htmlPatterns.CLOSE_TAG ++ ")\\s*";

fn testHtmlCondition7(state: *BlockParserState, parent: *MarkdownNode, tail: []const u8) bool {
    const regex = mvzr.compile(HTML_REGEX_7) orelse return false;
    const match = regex.match(tail);

    if (match != null and match.?.start == 0) {
        const start = state.i;

        // "To start an HTML block with a tag that is not in the list of
        // block-level tags in (6), you must put the tag by itself on the first
        // line (and it must be complete)"
        const match_end = match.?.end;

        // Can't have any newlines before the last one
        var x: usize = 0;
        while (x < match_end - 2) : (x += 1) {
            if (tail[x] == '\n') {
                return false;
            }
        }

        // Must finish at the end of the source, or at a newline
        if (match_end != tail.len and tail[match_end - 1] != '\n') {
            return false;
        }

        // "All types of HTML blocks except type 7 may interrupt a paragraph.
        // Blocks of type 7 may not interrupt a paragraph"
        if (std.mem.eql(u8, parent.type, "paragraph") and !parent.blankAfter) {
            // Append the HTML tag to the paragraph content
            const old_content = parent.content;
            const new_content = state.allocator.alloc(u8, old_content.len + match_end) catch unreachable;
            @memcpy(new_content[0..old_content.len], old_content);
            @memcpy(new_content[old_content.len..], tail[0..match_end]);
            if (parent.content_allocated) {
                state.allocator.free(old_content);
            }
            parent.content = new_content;
            parent.content_allocated = true;
            state.i += match_end;
            return true;
        }

        const end_of_line = getEndOfLine(state);
        const html = newNode(state.allocator, "html_block", true, start, state.line, 1, "", 7, null) catch unreachable;
        html.content = createHtmlContent(state, start, end_of_line);
        html.content_allocated = true;
        html.acceptsContent = true;

        if (state.hasBlankLine and parent.children != null and parent.children.?.len > 0) {
            const last_child = parent.children.?[parent.children.?.len - 1];
            last_child.blankAfter = true;
            state.hasBlankLine = false;
        }

        appendChild(state.allocator, parent, html) catch unreachable;
        state.openNodes.append(state.allocator, html) catch unreachable;
        state.i = end_of_line;
        return true;
    }

    return false;
}

fn createHtmlBlock(state: *BlockParserState, parent: *MarkdownNode, start: usize, end: usize, html_type: i32) void {
    const html = newNode(state.allocator, "html_block", true, start, state.line, 1, "", html_type, null) catch unreachable;

    const base_content = state.src[start..end];
    if (state.indent > 0) {
        // Add indentation to the content
        const indent_len: usize = @intCast(state.indent);
        var content = state.allocator.alloc(u8, indent_len + base_content.len) catch unreachable;
        var i: usize = 0;
        while (i < indent_len) : (i += 1) {
            content[i] = ' ';
        }
        @memcpy(content[indent_len..], base_content);
        html.content = content;
    } else {
        html.content = state.allocator.dupe(u8, base_content) catch unreachable;
    }
    html.content_allocated = true;

    if (state.hasBlankLine and parent.children != null and parent.children.?.len > 0) {
        const last_child = parent.children.?[parent.children.?.len - 1];
        last_child.blankAfter = true;
        state.hasBlankLine = false;
    }

    appendChild(state.allocator, parent, html) catch unreachable;
    state.openNodes.append(state.allocator, html) catch unreachable;
    state.i = end;
}

pub fn testContinue(state: *BlockParserState, node: *MarkdownNode) bool {
    if (node.indent == 6 or node.indent == 7) {
        const result = !state.hasBlankLine;
        state.hasBlankLine = false;
        return result;
    }

    return false;
}

pub const htmlBlockRule = BlockRule{
    .name = "html_block",
    .testStart = testStart,
    .testContinue = testContinue,
};
