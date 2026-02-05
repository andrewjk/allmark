import type BlockParserState from "../types/BlockParserState";
import type BlockRule from "../types/BlockRule";
import type MarkdownNode from "../types/MarkdownNode";
import closeNode from "../utils/closeNode";
import getEndOfLine from "../utils/getEndOfLine";
import { CLOSE_TAG, OPEN_TAG } from "../utils/htmlPatterns";
import isEscaped from "../utils/isEscaped";
import newNode from "../utils/newNode";

// TODO: de-duplicate a lot of this code
// TODO: Should we split it up into seven different node types??

const rule: BlockRule = {
	name: "html_block",
	testStart,
	testContinue,
};
export default rule;

/**
 * "An HTML block is a group of lines that is treated as raw HTML (and will not
 * be escaped in HTML output).
 *
 * There are seven kinds of HTML block, which can be defined by their start and
 * end conditions. The block begins with a line that meets a start condition
 * (after up to three spaces optional indentation). It ends with the first
 * subsequent line that meets a matching end condition, or the last line of the
 * document, or the last line of the container block containing the current HTML
 * block, if no line is encountered that meets the end condition. If the first
 * line meets both the start condition and the end condition, the block will
 * contain just that line.
 *
 * HTML blocks continue until they are closed by their appropriate end
 * condition, or the last line of the document or other container block. This
 * means any HTML within an HTML block that might otherwise be recognised as a
 * start condition will be ignored by the parser and passed through as-is,
 * without changing the parser’s state."
 */
function testStart(state: BlockParserState, parent: MarkdownNode) {
	if (parent.acceptsContent) {
		return false;
	}

	let char = state.src[state.i];
	if (state.indent <= 3 && char === "<" && !isEscaped(state.src, state.i)) {
		let tail = state.src.substring(state.i);

		if (testHtmlCondition1(state, parent, tail)) {
			return true;
		}
		if (testHtmlCondition2(state, parent, tail)) {
			return true;
		}
		if (testHtmlCondition3(state, parent, tail)) {
			return true;
		}
		if (testHtmlCondition4(state, parent, tail)) {
			return true;
		}
		if (testHtmlCondition5(state, parent, tail)) {
			return true;
		}
		if (testHtmlCondition6(state, parent, tail)) {
			return true;
		}
		if (testHtmlCondition7(state, parent, tail)) {
			return true;
		}
	}

	return false;
}

const HTML_REGEX_1 = /^<(script|pre|style|textarea)(\s|$|>)/i;

/**
 * Start condition: line begins with the string <script, <pre, or <style
 * (case-insensitive), followed by whitespace, the string >, or the end of the
 * line.
 *
 * End condition: line contains an end tag </script>, </pre>, or </style>
 * (case-insensitive; it need not match the start tag).
 */
function testHtmlCondition1(state: BlockParserState, parent: MarkdownNode, tail: string) {
	let match1 = tail.match(HTML_REGEX_1);
	if (match1?.index === 0) {
		if (parent.type === "paragraph") {
			closeNode(state, state.openNodes.pop()!);
			parent = state.openNodes.at(-1)!;
		}

		let closingTag = `</${match1[1]}>`.toLocaleLowerCase();
		let start = state.i;
		let end = state.i + 1 + match1[0].length + 1;
		for (; end < state.src.length; end++) {
			if (state.src[end] === "<" && state.src[end + 1] === "/") {
				let nextClosingTag = state.src.substring(end, end + closingTag.length).toLocaleLowerCase();
				if (nextClosingTag === closingTag) {
					//end += closingTag.length;
					state.i = end;
					end = getEndOfLine(state);
					break;
				}
			}
		}

		let html = newNode("html_block", true, start, state.line, 1, "", 1, []);
		html.content = " ".repeat(state.indent) + state.src.substring(start, end);

		if (state.hasBlankLine && parent.children !== undefined && parent.children.length > 0) {
			parent.children.at(-1)!.blankAfter = true;
			state.hasBlankLine = false;
		}

		parent.children!.push(html);
		state.openNodes.push(html);
		state.i = end;

		return true;
	}
}

const HTML_REGEX_2 = /<!--.+?-->/s;

/**
 * Start condition: line begins with the string <!--.
 *
 * End condition: line contains the string -->.
 */
function testHtmlCondition2(state: BlockParserState, parent: MarkdownNode, tail: string) {
	let match = tail.match(HTML_REGEX_2);
	if (match !== null) {
		if (parent.type === "paragraph") {
			closeNode(state, state.openNodes.pop()!);
			parent = state.openNodes.at(-1)!;
		}

		let start = state.i;
		state.i += match[0].length;
		let endOfLine = getEndOfLine(state);
		let html = newNode("html_block", true, start, state.line, 1, "", 2, []);
		html.content = " ".repeat(state.indent) + state.src.substring(start, endOfLine);

		if (state.hasBlankLine && parent.children !== undefined && parent.children.length > 0) {
			parent.children.at(-1)!.blankAfter = true;
			state.hasBlankLine = false;
		}

		parent.children!.push(html);
		state.openNodes.push(html);
		state.i = endOfLine;

		return true;
	}
}

const HTML_REGEX_3 = /<\?.+?\?>/s;

/**
 * Start condition: line begins with the string <?.
 *
 * End condition: line contains the string ?>.
 */
function testHtmlCondition3(state: BlockParserState, parent: MarkdownNode, tail: string) {
	let match = tail.match(HTML_REGEX_3);
	if (match !== null) {
		if (parent.type === "paragraph") {
			closeNode(state, state.openNodes.pop()!);
			parent = state.openNodes.at(-1)!;
		}

		let start = state.i;
		state.i += match[0].length;
		let endOfLine = getEndOfLine(state);
		let html = newNode("html_block", true, start, state.line, 1, "", 3, []);
		html.content = " ".repeat(state.indent) + state.src.substring(start, endOfLine);

		if (state.hasBlankLine && parent.children !== undefined && parent.children.length > 0) {
			parent.children.at(-1)!.blankAfter = true;
			state.hasBlankLine = false;
		}

		parent.children!.push(html);
		state.openNodes.push(html);
		state.i = endOfLine;

		return true;
	}
}

const HTML_REGEX_4 = /<![A-Z].+>/s;

/**
 * Start condition: line begins with the string <! followed by an uppercase
 * ASCII letter.
 *
 * End condition: line contains the character >.
 */
function testHtmlCondition4(state: BlockParserState, parent: MarkdownNode, tail: string) {
	let match = tail.match(HTML_REGEX_4);
	if (match !== null) {
		if (parent.type === "paragraph") {
			closeNode(state, state.openNodes.pop()!);
			parent = state.openNodes.at(-1)!;
		}

		let start = state.i;
		state.i += match[0].length;
		let endOfLine = getEndOfLine(state);
		let html = newNode("html_block", true, start, state.line, 1, "", 4, []);
		html.content = " ".repeat(state.indent) + state.src.substring(start, endOfLine);

		if (state.hasBlankLine && parent.children !== undefined && parent.children.length > 0) {
			parent.children.at(-1)!.blankAfter = true;
			state.hasBlankLine = false;
		}

		parent.children!.push(html);
		state.openNodes.push(html);
		state.i = endOfLine;

		return true;
	}
}

const HTML_REGEX_5 = /<!\[CDATA\[.+\]\]>/s;

/**
 * Start condition: line begins with the string <![CDATA[.
 *
 * End condition: line contains the string ]]>.
 */
function testHtmlCondition5(state: BlockParserState, parent: MarkdownNode, tail: string) {
	let match = tail.match(HTML_REGEX_5);
	if (match !== null) {
		if (parent.type === "paragraph") {
			closeNode(state, state.openNodes.pop()!);
			parent = state.openNodes.at(-1)!;
		}

		let start = state.i;
		state.i += match[0].length;
		let endOfLine = getEndOfLine(state);
		let html = newNode("html_block", true, start, state.line, 1, "", 5, []);
		html.content = " ".repeat(state.indent) + state.src.substring(start, endOfLine);

		if (state.hasBlankLine && parent.children !== undefined && parent.children.length > 0) {
			parent.children.at(-1)!.blankAfter = true;
			state.hasBlankLine = false;
		}

		parent.children!.push(html);
		state.openNodes.push(html);
		state.i = endOfLine;

		return true;
	}
}

const HTML_REGEX_6 =
	/^<\/*(address|article|aside|base|basefont|blockquote|body|caption|center|col|colgroup|dd|details|dialog|dir|div|dl|dt|fieldset|figcaption|figure|footer|form|frame|frameset|h1|h2|h3|h4|h5|h6|head|header|hr|html|iframe|legend|li|link|main|menu|menuitem|nav|noframes|ol|optgroup|option|p|param|section|source|summary|table|tbody|td|tfoot|th|thead|title|tr|track|ul)(\s|\n|$|>|\/>)/i;

/**
 * Start condition: line begins the string < or </ followed by one of the
 * strings (case-insensitive) address, article, aside, base, basefont,
 * blockquote, body, caption, center, col, colgroup, dd, details, dialog, dir,
 * div, dl, dt, fieldset, figcaption, figure, footer, form, frame, frameset, h1,
 * h2, h3, h4, h5, h6, head, header, hr, html, iframe, legend, li, link, main,
 * menu, menuitem, nav, noframes, ol, optgroup, option, p, param, section,
 * source, summary, table, tbody, td, tfoot, th, thead, title, tr, track, ul,
 * followed by whitespace, the end of the line, the string >, or the string />.
 *
 * End condition: line is followed by a blank line.
 */
function testHtmlCondition6(state: BlockParserState, parent: MarkdownNode, tail: string) {
	let match = tail.match(HTML_REGEX_6);
	if (match !== null) {
		if (parent.type === "paragraph") {
			closeNode(state, state.openNodes.pop()!);
			parent = state.openNodes.at(-1)!;
		}

		let endOfLine = getEndOfLine(state);
		let html = newNode("html_block", true, state.i, state.line, 1, "", 6, []);
		html.content = " ".repeat(state.indent) + state.src.substring(state.i, endOfLine);
		html.acceptsContent = true;

		if (state.hasBlankLine && parent.children !== undefined && parent.children.length > 0) {
			parent.children.at(-1)!.blankAfter = true;
			state.hasBlankLine = false;
		}

		parent.children!.push(html);
		state.openNodes.push(html);
		state.i = endOfLine;

		return true;
	}
}

const HTML_REGEX_7 = new RegExp(`^(?:${OPEN_TAG}|${CLOSE_TAG})(?:\\s|$)`);

/**
 * Start condition: line begins with a complete open tag (with any tag name
 * other than script, style, or pre) or a complete closing tag, followed only by
 * whitespace or the end of the line.
 *
 * End condition: line is followed by a blank line.
 */
function testHtmlCondition7(state: BlockParserState, parent: MarkdownNode, tail: string) {
	let match = tail.match(HTML_REGEX_7);
	if (match !== null) {
		// "To start an HTML block with a tag that is not in the list of
		// block-level tags in (6), you must put the tag by itself on the first
		// line (and it must be complete)"
		// HACK: Maybe we could improve the regex?
		let newLineIndex = match[0].indexOf("\n");
		if (newLineIndex !== -1 && newLineIndex < match[0].length - 1) {
			return false;
		}

		// "All types of HTML blocks except type 7 may interrupt a paragraph.
		// Blocks of type 7 may not interrupt a paragraph"
		let lastNode = parent; //parent.children!.at(-1);
		if (lastNode && lastNode.type === "paragraph" && !lastNode.blankAfter) {
			let end = state.i + match[0].length;
			let content = state.src.substring(state.i, end);
			lastNode.content += content;
			state.i = end;
			return true;
		}

		let endOfLine = getEndOfLine(state);
		let html = newNode("html_block", true, state.i, state.line, 1, "", 7, []);
		html.content = " ".repeat(state.indent) + state.src.substring(state.i, endOfLine);
		html.acceptsContent = true;

		if (state.hasBlankLine && parent.children !== undefined && parent.children.length > 0) {
			parent.children.at(-1)!.blankAfter = true;
			state.hasBlankLine = false;
		}

		parent.children!.push(html);
		state.openNodes.push(html);
		state.i = endOfLine;
		return true;
	}
}

function testContinue(state: BlockParserState, node: MarkdownNode) {
	if (node.indent === 6 || node.indent === 7) {
		let result = !state.hasBlankLine;
		state.hasBlankLine = false;
		return result;
	}

	return false;
}
