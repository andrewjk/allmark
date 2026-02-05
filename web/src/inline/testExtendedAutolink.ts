import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type MarkdownNode from "../types/MarkdownNode";
import escapeHtml from "../utils/escapeHtml";
import { isAlphaNumeric } from "../utils/isAlphaNumeric";
import isEscaped from "../utils/isEscaped";
import newNode from "../utils/newNode";

const rule: InlineRule = {
	name: "extended_autolink",
	test: testAutolink,
};
export default rule;

// An HTML tag consists of an open tag, a closing tag, an HTML comment, a
// processing instruction, a declaration, or a CDATA section.
const SPACE_REGEX = /\s/;
// TODO: This needs improvement:
const URL_REGEX = /^(www\.([a-z0-9_-]\.*)+([a-z0-9-]\.*){0,2}[^\s<]*)/i;
const EXT_URL_REGEX = /^((https*|ftp):\/\/([a-z0-9_-]\.*)+([a-z0-9-]\.*){0,2}[^\s<]*)/i;
const EXT_EMAIL_REGEX = /^([a-z0-9._\-+]+@([a-z0-9._\-+]+\.*)+)/i;
const EXT_XMPP_REGEX = /^((mailto|xmpp):[a-z0-9._\-+]+@([a-z0-9._\-+]+\.*)+(\/[a-z0-9@.]+){0,1})/i;

function testAutolink(state: InlineParserState, parent: MarkdownNode, _end: number): boolean {
	// Don't try to extract HTML for HTML blocks
	if (parent.type === "html_block") {
		return false;
	}

	let char = state.src[state.i];
	if (!isEscaped(state.src, state.i)) {
		if (char === "w") {
			let tail = state.src.substring(state.i);

			let urlMatch = tail.match(URL_REGEX);
			if (urlMatch !== null) {
				let url = urlMatch[1];

				if (SPACE_REGEX.test(url)) {
					let text = newNode("text", false, state.i, state.line, 1, "", state.indent);
					text.markup = escapeHtml(urlMatch[0]);
					parent.children!.push(text);
					state.i += urlMatch[0].length;

					return true;
				}

				url = extendedValidation(url);
				url = escapeHtml(url);

				let html = newNode("html_span", false, state.i, state.line, 1, "", state.indent);
				html.content = `<a href="http://${encodeURI(url)}">${url}</a>`;
				parent.children!.push(html);
				state.i += url.length;

				return true;
			}
		}

		if (char === "h" || char === "f") {
			let tail = state.src.substring(state.i);

			let urlMatch = tail.match(EXT_URL_REGEX);
			if (urlMatch !== null) {
				let url = urlMatch[1];

				if (SPACE_REGEX.test(url)) {
					let text = newNode("text", false, state.i, state.line, 1, "", state.indent);
					text.markup = escapeHtml(urlMatch[0]);
					parent.children!.push(text);
					state.i += urlMatch[0].length;

					return true;
				}

				url = extendedValidation(url);
				url = escapeHtml(url);

				let html = newNode("html_span", false, state.i, state.line, 1, "", state.indent);
				html.content = `<a href="${encodeURI(url)}">${url}</a>`;
				parent.children!.push(html);
				state.i += url.length;

				return true;
			}
		}

		if (isAlphaNumeric(state.src.charCodeAt(state.i))) {
			// TODO: I think we should actually check this when we come across an @,
			// rather than any alphanumeric
			let tail = state.src.substring(state.i);

			let emailMatch = tail.match(EXT_EMAIL_REGEX);
			if (emailMatch !== null) {
				let url = emailMatch[1];

				// "+ can occur before the @, but not after" "., -, and _ can
				// occur on both sides of the @, but only . may occur at the end
				// of the email address, in which case it will not be considered
				// part of the address"
				if (/[-_]$/.test(url) || url.indexOf("+", url.indexOf("@")) !== -1) {
					let text = newNode("text", false, state.i, state.line, 1, "", state.indent);
					text.markup = escapeHtml(emailMatch[0]);
					parent.children!.push(text);
					state.i += emailMatch[0].length;

					return true;
				}

				url = url.replaceAll(/\.$/g, "");

				let html = newNode("html_span", false, state.i, state.line, 1, "", state.indent);
				html.content = `<a href="mailto:${encodeURI(url)}">${url}</a>`;
				parent.children!.push(html);
				state.i += url.length;

				return true;
			}
		}

		if (char === "m" || char === "x") {
			let tail = state.src.substring(state.i);

			let emailMatch = tail.match(EXT_XMPP_REGEX);
			if (emailMatch !== null) {
				let url = emailMatch[1];

				// "+ can occur before the @, but not after" "., -, and _ can
				// occur on both sides of the @, but only . may occur at the end
				// of the email address, in which case it will not be considered
				// part of the address"
				if (/[-_]$/.test(url) || url.indexOf("+", url.indexOf("@")) !== -1) {
					let text = newNode("text", false, state.i, state.line, 1, "", state.indent);
					text.markup = escapeHtml(emailMatch[0]);
					parent.children!.push(text);
					state.i += emailMatch[0].length;

					return true;
				}

				url = url.replaceAll(/\.$/g, "");

				let html = newNode("html_span", false, state.i, state.line, 1, "", state.indent);
				html.content = `<a href="${encodeURI(url)}">${url}</a>`;
				parent.children!.push(html);
				state.i += url.length;

				return true;
			}
		}
	}

	return false;
}

const TRAILING_PUNCTUATION = /[?!.,:*_~]$/g;
const TRAILING_ENTITY = /&[a-z0-9]+;$/gi;

function extendedValidation(url: string) {
	// "Trailing punctuation (specifically, ?, !, ., ,, :, *, _,
	// and ~) will not be considered part of the autolink,
	// though they may be included in the interior of the link"
	url = url.replaceAll(TRAILING_PUNCTUATION, "");

	// "When an autolink ends in ), we scan the entire autolink for the total
	// number of parentheses. If there is a greater number of closing
	// parentheses than opening ones, we don’t consider the unmatched trailing
	// parentheses part of the autolink, in order to facilitate including an
	// autolink inside a parenthesis"
	if (url.endsWith(")")) {
		let trimCount = 0;
		let i = url.length;
		let countingUp = true;
		while (i--) {
			if (countingUp) {
				if (url[i] === ")") {
					trimCount++;
				} else {
					countingUp = false;
				}
			} else {
				if (url[i] === "(") {
					trimCount--;
				}
				if (trimCount === 0) {
					break;
				}
			}
		}
		url = url.substring(0, url.length - trimCount);
	}

	// "If an autolink ends in a semicolon (;), we check to see if it appears to
	// resemble an entity reference; if the preceding text is & followed by one
	// or more alphanumeric characters. If so, it is excluded from the autolink"
	if (url.endsWith(";")) {
		url = url.replaceAll(TRAILING_ENTITY, "");
	}

	return url;
}
