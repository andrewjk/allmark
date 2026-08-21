import type BlockParserState from "../types/BlockParserState";
import type BlockRule from "../types/BlockRule";
import type MarkdownNode from "../types/MarkdownNode";
import { ANGLE_LEFT_CODE, SLASH_CODE } from "../utils/charCodes";
import closeNode from "../utils/closeNode";
import getEndOfLine from "../utils/getEndOfLine";
import { CLOSE_TAG, OPEN_TAG } from "../utils/htmlPatterns";
import isNewLine from "../utils/isNewLine";
import newBlock from "../utils/newBlock";

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

	if (!state.isEscaped && state.indent <= 3 && state.src.charCodeAt(state.i) === ANGLE_LEFT_CODE) {
		let tail = state.src.substring(state.i);

		if (
			testHtmlCondition1(state, parent, tail) ||
			testHtmlCondition2to5(state, parent, tail, HTML_REGEX_2) ||
			testHtmlCondition2to5(state, parent, tail, HTML_REGEX_3) ||
			testHtmlCondition2to5(state, parent, tail, HTML_REGEX_4) ||
			testHtmlCondition2to5(state, parent, tail, HTML_REGEX_5) ||
			testHtmlCondition6(state, parent, tail) ||
			testHtmlCondition7(state, parent, tail)
		) {
			return true;
		}
	}

	return false;
}

/**
 * Start condition: line begins with the string <script, <pre, or <style
 * (case-insensitive), followed by whitespace, the string >, or the end of the
 * line.
 *
 * End condition: line contains an end tag </script>, </pre>, or </style>
 * (case-insensitive; it need not match the start tag).
 */
const HTML_REGEX_1 = /^<(script|pre|style|textarea)(\s|$|>)/i;

/**
 * Start condition: line begins with the string <!--.
 *
 * End condition: line contains the string -->.
 */
const HTML_REGEX_2 = /<!--.+?-->/s;

/**
 * Start condition: line begins with the string <?.
 *
 * End condition: line contains the string ?>.
 */
const HTML_REGEX_3 = /<\?.+?\?>/s;

/**
 * Start condition: line begins with the string <! followed by an uppercase
 * ASCII letter.
 *
 * End condition: line contains the character >.
 */
const HTML_REGEX_4 = /<![A-Z].+>/s;

/**
 * Start condition: line begins with the string <![CDATA[.
 *
 * End condition: line contains the string ]]>.
 */
const HTML_REGEX_5 = /<!\[CDATA\[.+\]\]>/s;

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
const HTML_REGEX_6 =
	/^<\/*(address|article|aside|base|basefont|blockquote|body|caption|center|col|colgroup|dd|details|dialog|dir|div|dl|dt|fieldset|figcaption|figure|footer|form|frame|frameset|h1|h2|h3|h4|h5|h6|head|header|hr|html|iframe|legend|li|link|main|menu|menuitem|nav|noframes|ol|optgroup|option|p|param|section|source|summary|table|tbody|td|tfoot|th|thead|title|tr|track|ul)(\s+|$|>|\/>)/i;

/**
 * Start condition: line begins with a complete open tag (with any tag name
 * other than script, style, or pre) or a complete closing tag, followed only by
 * whitespace or the end of the line.
 *
 * End condition: line is followed by a blank line.
 */
const HTML_REGEX_7 = new RegExp(`^(?:${OPEN_TAG}|${CLOSE_TAG})(?:\\r?\\n|\\r|\\s|$)`);

function testHtmlCondition1(state: BlockParserState, parent: MarkdownNode, tail: string) {
	let match1 = tail.match(HTML_REGEX_1);
	if (match1?.index === 0) {
		let closingTag = `</${match1[1]}>`.toLocaleLowerCase();
		let start = state.i;
		let end = state.i + 1 + match1[0].length + 1;
		for (; end < state.src.length; end++) {
			if (
				state.src.charCodeAt(end) === ANGLE_LEFT_CODE &&
				state.src.charCodeAt(end + 1) === SLASH_CODE
			) {
				let nextClosingTag = state.src.substring(end, end + closingTag.length).toLocaleLowerCase();
				if (nextClosingTag === closingTag) {
					state.i = end;
					end = getEndOfLine(state);
					break;
				}
			}
		}
		addHtmlBlock(state, parent, start, end, 1);
		return true;
	}
}

function testHtmlCondition2to5(
	state: BlockParserState,
	parent: MarkdownNode,
	tail: string,
	regex: RegExp,
) {
	let match = tail.match(regex);
	if (match !== null) {
		let start = state.i;
		state.i += match[0].length;
		let endOfLine = getEndOfLine(state);
		addHtmlBlock(state, parent, start, endOfLine, 2);
		return true;
	}
}

function testHtmlCondition6(state: BlockParserState, parent: MarkdownNode, tail: string) {
	let match = tail.match(HTML_REGEX_6);
	if (match !== null) {
		if (parent.type === "paragraph") {
			closeNode(state, state.openNodes.pop()!);
			parent = state.openNodes.at(-1)!;
		}
		let endOfLine = getEndOfLine(state);
		addHtmlBlock(state, parent, state.i, endOfLine, 6);
		return true;
	}
}

function testHtmlCondition7(state: BlockParserState, parent: MarkdownNode, tail: string) {
	let match = tail.match(HTML_REGEX_7);
	if (match !== null) {
		// "To start an HTML block with a tag that is not in the list of
		// block-level tags in (6), you must put the tag by itself on the first
		// line (and it must be complete)"
		// HACK: Maybe we could improve the regex?
		let end = state.i + match[0].length - (match[0].endsWith("\r\n") ? 2 : 1);
		for (let i = state.i; i < end; i++) {
			if (isNewLine(state.src.charCodeAt(i))) {
				return false;
			}
		}

		// "All types of HTML blocks except type 7 may interrupt a paragraph.
		// Blocks of type 7 may not interrupt a paragraph"
		let lastNode = parent;
		if (lastNode && lastNode.type === "paragraph" && !lastNode.blankAfter) {
			let end = state.i + match[0].length;
			let content = state.src.substring(state.i, end);
			lastNode.content += content;
			state.i = end;
			return true;
		}

		let endOfLine = getEndOfLine(state);
		addHtmlBlock(state, parent, state.i, endOfLine, 7);
		return true;
	}
}

function addHtmlBlock(
	state: BlockParserState,
	parent: MarkdownNode,
	start: number,
	end: number,
	type: number,
) {
	let html = newBlock("html_block", start, state.line, "", type);
	html.content = " ".repeat(state.indent) + state.src.substring(start, end);
	html.acceptsContent = type === 6 || type === 7;
	if (
		html.acceptsContent &&
		state.hasBlankLine &&
		parent.children !== undefined &&
		parent.children.length > 0
	) {
		parent.children.at(-1)!.blankAfter = true;
		state.hasBlankLine = false;
	}
	parent.children!.push(html);
	state.openNodes.push(html);
	state.i = end;
}

function testContinue(state: BlockParserState, node: MarkdownNode) {
	if (node.indent === 6 || node.indent === 7) {
		let result = !state.hasBlankLine;
		state.hasBlankLine = false;
		return result;
	}

	return false;
}
