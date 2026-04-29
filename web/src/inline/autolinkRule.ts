import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type MarkdownNode from "../types/MarkdownNode";
import { ANGLE_LEFT_CODE } from "../utils/charCodes";
import decodeEntities from "../utils/decodeEntities";
import escapeHtml from "../utils/escapeHtml";
import isEscaped from "../utils/isEscaped";
import newInline from "../utils/newInline";
import newText from "../utils/newText";

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

	if (state.src.charCodeAt(state.i) === ANGLE_LEFT_CODE && !isEscaped(state.src, state.i)) {
		let tail = state.src.substring(state.i);

		let linkMatch = tail.match(LINK_REGEX);
		if (linkMatch !== null) {
			let url = escapeHtml(linkMatch[1]);

			if (SPACE_REGEX.test(url)) {
				let content = escapeHtml(linkMatch[0]);
				let text = newText(state.parentIndex + state.i, state.line, content, state.indent);
				text.length = linkMatch[0].length;
				parent.children!.push(text);
				state.i += linkMatch[0].length;

				return true;
			}

			let text = newText(
				state.parentIndex + state.i,
				state.line,
				url.replaceAll("\\", "\\\\"),
				state.indent,
			);
			let link = newInline("link", state.parentIndex + state.i, state.line, "", state.indent);
			link.children = [text];

			url = decodeEntities(url);
			url = encodeURI(decodeURI(url));

			link.info = url;
			link.length = linkMatch[0].length;
			parent.children!.push(link);

			state.i += linkMatch[0].length;

			return true;
		}

		let emailMatch = tail.match(EMAIL_REGEX);
		if (emailMatch !== null) {
			let url = escapeHtml(emailMatch[1]);

			if (SPACE_REGEX.test(url)) {
				let content = escapeHtml(emailMatch[0]);
				let text = newText(state.parentIndex + state.i, state.line, content, state.indent);
				text.length = emailMatch[0].length;
				parent.children!.push(text);
				state.i += emailMatch[0].length;

				return true;
			}

			let text = newText(
				state.parentIndex + state.i,
				state.line,
				url.replaceAll("\\", "\\\\"),
				state.indent,
			);
			let link = newInline("link", state.parentIndex + state.i, state.line, "", state.indent);
			link.children = [text];

			url = `mailto:${encodeURI(url)}`;

			link.info = url;
			link.length = emailMatch[0].length;
			parent.children!.push(link);

			state.i += emailMatch[0].length;

			return true;
		}
	}

	return false;
}
