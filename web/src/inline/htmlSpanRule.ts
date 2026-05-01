import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type MarkdownNode from "../types/MarkdownNode";
import { ANGLE_LEFT_CODE } from "../utils/charCodes";
import {
	CDATA,
	CLOSE_TAG,
	COMMENT,
	DECLARATION,
	INSTRUCTION,
	OPEN_TAG,
} from "../utils/htmlPatterns";
import newInline from "../utils/newInline";
import { appendChild } from "../utils/nodeUtils";

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

function testHtmlSpan(state: InlineParserState, parent: MarkdownNode): boolean {
	// Don't try to extract HTML for HTML blocks
	if (parent.type === "html_block") {
		return false;
	}

	if (!state.isEscaped && state.src.charCodeAt(state.i) === ANGLE_LEFT_CODE) {
		let tail = state.src.substring(state.i);
		let match = tail.match(HTML_TAG_REGEX);
		if (match !== null) {
			let content = match[0];
			let html = newInline("html_span", state.parentIndex + state.i, state.line, "", state.indent);
			html.content = content;
			html.length = match[0].length;
			appendChild(parent, html);
			state.i += match[0].length;
			return true;
		}
	}

	return false;
}
