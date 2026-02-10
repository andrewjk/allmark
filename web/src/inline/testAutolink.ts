import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type MarkdownNode from "../types/MarkdownNode";
import escapeHtml from "../utils/escapeHtml";
import isEscaped from "../utils/isEscaped";
import newNode from "../utils/newNode";

const rule: InlineRule = {
	name: "autolink",
	test: testAutolink,
};
export default rule;

// An HTML tag consists of an open tag, a closing tag, an HTML comment, a
// processing instruction, a declaration, or a CDATA section.
const SPACE_REGEX = /\s/;
const LINK_REGEX = /^<(\s*[a-z][a-z0-9+.-]{1,31}:[^<>]*)>/i;
const EMAIL_REGEX =
	/^<(\s*[a-z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-z0-9](?:[a-z0-9-]{0,61}[a-z0-9])?(?:\.[a-z0-9](?:[a-z0-9-]{0,61}[a-z0-9])?)*\s*)>/i;

function testAutolink(state: InlineParserState, parent: MarkdownNode): boolean {
	// Don't try to extract HTML for HTML blocks
	if (parent.type === "html_block") {
		return false;
	}

	let char = state.src[state.i];
	if (char === "<" && !isEscaped(state.src, state.i)) {
		let tail = state.src.substring(state.i);

		let linkMatch = tail.match(LINK_REGEX);
		if (linkMatch !== null) {
			let url = escapeHtml(linkMatch[1]);

			if (SPACE_REGEX.test(url)) {
				let text = newNode("text", false, state.i, state.line, 1, "", state.indent);
				text.markup = escapeHtml(linkMatch[0]);
				parent.children!.push(text);
				state.i += linkMatch[0].length;

				return true;
			}

			let html = newNode("html_span", false, state.i, state.line, 1, "", state.indent);
			html.content = `<a href="${encodeURI(url)}">${url}</a>`;
			parent.children!.push(html);
			state.i += linkMatch[0].length;

			return true;
		}

		let emailMatch = tail.match(EMAIL_REGEX);
		if (emailMatch !== null) {
			let url = escapeHtml(emailMatch[1]);

			if (SPACE_REGEX.test(url)) {
				let text = newNode("text", false, state.i, state.line, 1, "", state.indent);
				text.markup = escapeHtml(emailMatch[0]);
				parent.children!.push(text);
				state.i += emailMatch[0].length;

				return true;
			}

			let html = newNode("html_span", false, state.i, state.line, 1, "", state.indent);
			html.content = `<a href="mailto:${encodeURI(url)}">${url}</a>`;
			parent.children!.push(html);
			state.i += emailMatch[0].length;

			return true;
		}
	}

	return false;
}
