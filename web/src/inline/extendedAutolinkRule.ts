import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type MarkdownNode from "../types/MarkdownNode";
import { AT_SIGN_CODE, PAREN_CLOSE_CODE, PAREN_OPEN_CODE } from "../utils/charCodes";
import decodeEntities from "../utils/decodeEntities";
import escapeHtml from "../utils/escapeHtml";
import isSpace from "../utils/isSpace";
import newInline from "../utils/newInline";
import newText from "../utils/newText";

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

const F_CHAR_CODE = 102;
const H_CHAR_CODE = 104;
const M_CHAR_CODE = 109;
const W_CHAR_CODE = 119;
const X_CHAR_CODE = 120;

function testAutolink(state: InlineParserState, parent: MarkdownNode): boolean {
	if (!state.isEscaped) {
		let charCode = state.src.charCodeAt(state.i);
		if (charCode === W_CHAR_CODE) {
			let tail = state.src.substring(state.i);

			let urlMatch = tail.match(URL_REGEX);
			if (urlMatch !== null) {
				let url = urlMatch[1];

				if (SPACE_REGEX.test(url)) {
					let content = escapeHtml(urlMatch[0]);
					let text = newText(state.parentIndex + state.i, state.line, content, state.indent);
					text.length = urlMatch[0].length;
					parent.children!.push(text);
					state.i += urlMatch[0].length;

					return true;
				}

				url = extendedValidation(url);
				url = escapeHtml(url);

				let link = newLink(url, state);
				link.info = `http://${link.info}`;
				parent.children!.push(link);

				state.i += url.length;

				return true;
			}
		}

		if (charCode === H_CHAR_CODE || charCode === F_CHAR_CODE) {
			let tail = state.src.substring(state.i);

			let urlMatch = tail.match(EXT_URL_REGEX);
			if (urlMatch !== null) {
				let url = urlMatch[1];

				if (SPACE_REGEX.test(url)) {
					let content = escapeHtml(urlMatch[0]);
					let text = newText(state.parentIndex + state.i, state.line, content, state.indent);
					text.length = urlMatch[0].length;
					parent.children!.push(text);
					state.i += urlMatch[0].length;

					return true;
				}

				url = extendedValidation(url);
				url = escapeHtml(url);

				let link = newLink(url, state);
				parent.children!.push(link);

				state.i += url.length;

				return true;
			}
		}

		if (charCode === AT_SIGN_CODE) {
			let start = 0;
			for (let i = state.i - 1; i >= 0; i--) {
				let previousChar = state.src.charCodeAt(i);
				if (isSpace(previousChar)) {
					start = i + 1;
					break;
				}
			}

			let tail = state.src.substring(start);

			let emailMatch = tail.match(EXT_EMAIL_REGEX);
			if (emailMatch !== null) {
				let url = emailMatch[1];

				// "+ can occur before the @, but not after" "., -, and _ can
				// occur on both sides of the @, but only . may occur at the end
				// of the email address, in which case it will not be considered
				// part of the address"
				if (/[-_]$/.test(url) || url.indexOf("+", url.indexOf("@")) !== -1) {
					return false;
				}

				url = url.replaceAll(/\.$/g, "");

				let lastNode = parent.children!.at(-1)!;
				lastNode.content = lastNode.content.substring(
					0,
					lastNode.content.length - (state.i - start),
				);

				let link = newLink(url, state);
				link.info = `mailto:${link.info}`;
				parent.children!.push(link);

				state.i = start + url.length;

				return true;
			}
		}

		if (charCode === M_CHAR_CODE || charCode === X_CHAR_CODE) {
			let tail = state.src.substring(state.i);

			let emailMatch = tail.match(EXT_XMPP_REGEX);
			if (emailMatch !== null) {
				let url = emailMatch[1];

				// "+ can occur before the @, but not after" "., -, and _ can
				// occur on both sides of the @, but only . may occur at the end
				// of the email address, in which case it will not be considered
				// part of the address"
				if (/[-_]$/.test(url) || url.indexOf("+", url.indexOf("@")) !== -1) {
					let content = escapeHtml(emailMatch[0]);
					let text = newText(state.parentIndex + state.i, state.line, content, state.indent);
					text.length = emailMatch[0].length;
					parent.children!.push(text);
					state.i += emailMatch[0].length;

					return true;
				}

				url = url.replaceAll(/\.$/g, "");

				let link = newLink(url, state);
				parent.children!.push(link);

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
				if (url.charCodeAt(i) === PAREN_CLOSE_CODE) {
					trimCount++;
				} else {
					countingUp = false;
				}
			} else {
				if (url.charCodeAt(i) === PAREN_OPEN_CODE) {
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

function newLink(url: string, state: InlineParserState) {
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
	link.length = url.length;

	return link;
}
