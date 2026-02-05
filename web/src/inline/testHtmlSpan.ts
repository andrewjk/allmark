import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type MarkdownNode from "../types/MarkdownNode";
import {
	CDATA,
	CLOSE_TAG,
	COMMENT,
	DECLARATION,
	INSTRUCTION,
	OPEN_TAG,
} from "../utils/htmlPatterns";
import isEscaped from "../utils/isEscaped";
import newNode from "../utils/newNode";

const rule: InlineRule = {
	name: "html_span",
	test: testHtmlSpan,
};
export default rule;

// An HTML tag consists of an open tag, a closing tag, an HTML comment, a
// processing instruction, a declaration, or a CDATA section.
const HTML_TAG_REGEX = new RegExp(
	`^(?:${OPEN_TAG}|${CLOSE_TAG}|${COMMENT}|${INSTRUCTION}|${DECLARATION}|${CDATA})`,
);

function testHtmlSpan(state: InlineParserState, parent: MarkdownNode, _end: number): boolean {
	// Don't try to extract HTML for HTML blocks
	if (parent.type === "html_block") {
		return false;
	}

	let char = state.src[state.i];
	if (char === "<" && !isEscaped(state.src, state.i)) {
		let tail = state.src.substring(state.i);
		let match = tail.match(HTML_TAG_REGEX);
		if (match !== null) {
			let content = match[0];
			let html = newNode("html_span", false, state.i, state.line, 1, "", state.indent);
			html.content = content;
			parent.children!.push(html);
			state.i += match[0].length;
			return true;
		}
	}

	return false;
}
