const std = @import("std");
const BlockParserState = @import("../types/BlockParserState.zig").BlockParserState;
const BlockRule = @import("../types/BlockRule.zig").BlockRule;
const MarkdownNode = @import("../types/MarkdownNode.zig").MarkdownNode;
const closeNode = @import("../utils/closeNode.zig").closeNode;
const newBlock = @import("../utils/newBlock.zig").newBlock;
const appendChild = @import("../utils/appendChild.zig").appendChild;
const getEndOfLine = @import("../utils/getEndOfLine.zig").getEndOfLine;
const isSpace = @import("../utils/isSpace.zig").isSpace;
const isNewLine = @import("../utils/isNewLine.zig").isNewLine;
const mvzr = @import("mvzr");
const htmlPatterns = @import("../utils/htmlPatterns.zig");

// Note: mvzr regex patterns don't support non-capturing groups (?:)
// Note: Or case-insensitive matches
const HTML_REGEX_1 = "^<(script|SCRIPT|pre|PRE|style|STYLE|textarea|TEXTAREA)(\\s|\\n|>)";
const HTML_REGEX_2 = "^<!--.+?-->";
const HTML_REGEX_3 = "^<\\?.+?\\?>";
const HTML_REGEX_4 = "^<![A-Z].+>";
const HTML_REGEX_5 = "^<!\\[CDATA\\[.+\\]\\]>";
const HTML_REGEX_6 = "^<\\/?([a-zA-Z][a-zA-Z0-9-]*)(\\s+|>|\\/>)";
// NOTE: removed non-capturing groups `(?:)`
// NOTE: removed `$` anchor as mvzr doesn't support it correctly
const HTML_REGEX_7 = "^(" ++ htmlPatterns.OPEN_TAG ++ "|" ++ htmlPatterns.CLOSE_TAG ++ ")[ \\t]*";

const html_regex_1 = mvzr.compile(HTML_REGEX_1) orelse unreachable;
const html_regex_6 = mvzr.compile(HTML_REGEX_6) orelse unreachable;
const html_regex_7 = mvzr.compile(HTML_REGEX_7) orelse unreachable;

const HtmlTags = struct {
    const script = "script";
    const pre = "pre";
    const style = "style";
    const textarea = "textarea";
};

const BLOCK_LEVEL_TAGS = [_][]const u8{
    "address", "article", "aside",  "base",      "basefont",  "blockquote", "body",      "caption", "center", "col",     "colgroup", "dd",       "details",
    "dialog",   "dir",     "div",    "dl",        "dt",        "fieldset",   "figcaption","figure",  "footer", "form",    "frame",    "frameset", "h1",
    "h2",       "h3",      "h4",     "h5",        "h6",        "head",       "header",    "hr",       "html",    "iframe",  "legend",   "li",       "link",
    "main",     "menu",    "menuitem","nav",      "noframes",  "ol",         "optgroup",  "option",   "p",       "param",   "section",  "source",   "summary",
    "table",    "tbody",   "td",     "tfoot",     "th",        "thead",      "title",     "tr",       "track",   "ul",
};

fn isBlockLevelTag(tag_name: []const u8) bool {
    for (BLOCK_LEVEL_TAGS) |tag| {
        if (std.ascii.eqlIgnoreCase(tag_name, tag)) {
            return true;
        }
    }
    return false;
}

fn addHtmlBlock(state: *BlockParserState, parent: *MarkdownNode, start: usize, end: usize, html_type: i32) void {
    const html = newBlock(state.allocator, "html_block", start, state.line, "", html_type) catch unreachable;

    const base_content = state.src[start..end];
    if (state.indent > 0) {
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
    html.length = end - start;

    if (html_type == 6 or html_type == 7) {
        html.acceptsContent = true;

        if (state.hasBlankLine and parent.children != null and parent.children.?.len > 0) {
            const last_child = parent.children.?[parent.children.?.len - 1];
            last_child.blankAfter = true;
            state.hasBlankLine = false;
        }
    }

    appendChild(state.allocator, parent, html) catch unreachable;
    state.openNodes.append(state.allocator, html) catch unreachable;
    state.i = end;
}

fn testHtmlCondition2to5(state: *BlockParserState, parent: *MarkdownNode, tail: []const u8, pattern: []const u8, html_type: i32) bool {
    const regex = mvzr.compile(pattern) orelse return false;
    const match = regex.match(tail);

    if (match != null and match.?.start == 0) {
        const start = state.i;
        state.i += match.?.end;
        const end_of_line = getEndOfLine(state);
        addHtmlBlock(state, parent, start, end_of_line, html_type);
        return true;
    }

    return false;
}

fn testHtmlCondition1(state: *BlockParserState, parent: *MarkdownNode, tail: []const u8) bool {
    const match = html_regex_1.match(tail);

    if (match != null and match.?.start == 0) {
        const start = state.i;

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
            if (c == ' ' or c == '\n' or c == '\r' or c == '>' or c == '/') {
                break;
            }
        }
        const tag_name = tag_match[tag_name_start..tag_name_end];

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

        addHtmlBlock(state, parent, start, end, 1);

        return true;
    }

    return false;
}

fn testHtmlCondition6(state: *BlockParserState, parent: *MarkdownNode, tail: []const u8) bool {
    const match = html_regex_6.match(tail);

    if (match != null and match.?.start == 0) {
        const start = state.i;

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
            if (c == ' ' or c == '\n' or c == '\r' or c == '>' or c == '/') {
                break;
            }
        }
        const tag_name = tag_match[tag_name_start..tag_name_end];

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
        addHtmlBlock(state, effective_parent, start, end_of_line, 6);

        return true;
    }

    return false;
}

fn testHtmlCondition7(state: *BlockParserState, parent: *MarkdownNode, tail: []const u8) bool {
    const match = html_regex_7.match(tail);

    if (match != null and match.?.start == 0) {
        const start = state.i;

        const match_end = match.?.end;
        var end = state.i + match_end;
        if (end < state.src.len and !isNewLine(state.src[end])) {
            return false;
        }
        if (end >= 2 and state.src[end - 2] == '\r' and state.src[end - 1] == '\n') {
            end -= 2;
        } else {
            end -= 1;
        }
        var i: usize = state.i;
        while (i < end) : (i += 1) {
            if (isNewLine(state.src[i])) {
                return false;
            }
        }

        if (std.mem.eql(u8, parent.type, "paragraph") and !parent.blankAfter) {
            var p_end = state.i + match_end;
            if (p_end < state.src.len and state.src[p_end] == '\n') {
                p_end += 1;
            } else if (p_end + 1 < state.src.len and state.src[p_end] == '\r' and state.src[p_end + 1] == '\n') {
                p_end += 2;
            } else if (p_end < state.src.len and state.src[p_end] == '\r') {
                p_end += 1;
            }
            const old_content = parent.content;
            const new_content = state.allocator.alloc(u8, old_content.len + (p_end - state.i)) catch unreachable;
            @memcpy(new_content[0..old_content.len], old_content);
            @memcpy(new_content[old_content.len..], state.src[state.i..p_end]);
            if (parent.content_allocated) {
                state.allocator.free(old_content);
            }
            parent.content = new_content;
            parent.content_allocated = true;
            state.i = p_end;
            return true;
        }

        const end_of_line = getEndOfLine(state);
        addHtmlBlock(state, parent, start, end_of_line, 7);

        return true;
    }

    return false;
}

pub fn testStart(state: *BlockParserState, parent: *MarkdownNode, end_of_line: usize) bool {
    _ = end_of_line;
    if (parent.acceptsContent) return false;

    if (state.i >= state.src.len) return false;
    const char = state.src[state.i];

    if (!state.isEscaped and state.indent <= 3 and char == '<') {
        const tail = state.src[state.i..];

        if (testHtmlCondition1(state, parent, tail)) return true;
        if (testHtmlCondition2to5(state, parent, tail, HTML_REGEX_2, 2) or
            testHtmlCondition2to5(state, parent, tail, HTML_REGEX_3, 3) or
            testHtmlCondition2to5(state, parent, tail, HTML_REGEX_4, 4) or
            testHtmlCondition2to5(state, parent, tail, HTML_REGEX_5, 5))
        {
            return true;
        }
        if (testHtmlCondition6(state, parent, tail)) return true;
        if (testHtmlCondition7(state, parent, tail)) return true;
    }

    return false;
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
