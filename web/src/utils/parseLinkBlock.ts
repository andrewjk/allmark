import type BlockParserState from "../types/BlockParserState";
import type LinkReference from "../types/LinkReference";
import consumeSpaces from "./consumeSpaces";
import decodeEntities from "./decodeEntities";
import escapeBackslashes from "./escapeBackslashes";
import escapeHtml from "./escapeHtml";
import isEscaped from "./isEscaped";
import isNewLine from "./isNewLine";
import isSpace from "./isSpace";

// TODO: Get this from utils??
const BLANK_LINE_REGEX = /\n[ \t]*\n/;

export default function parseLinkBlock(
	state: BlockParserState,
	start: number,
	_end: string,
): LinkReference | undefined {
	// Consume spaces
	let spaces = consumeSpaces(state.src, start);
	if (BLANK_LINE_REGEX.test(spaces)) {
		return;
	}
	start += spaces.length;

	// Get the url
	let url = "";
	if (state.src[start] === "<") {
		start++;
		for (let i = start; i < state.src.length; i++) {
			if (state.src[i] === ">" && !isEscaped(state.src, i)) {
				url = state.src.substring(start, i);
				start = i + 1;
				break;
			}
		}
	} else {
		for (let i = start; i <= state.src.length; i++) {
			if (i === state.src.length || isSpace(state.src.charCodeAt(i))) {
				url = state.src.substring(start, i);
				start = i;
				break;
			}
		}

		// "The link destination may not be omitted"
		// "However, an empty link destination may be specified using angle brackets" (see above)
		if (url === undefined) {
			return;
		}
	}

	if (url !== undefined) {
		// "The destination cannot contain line breaks, even if enclosed in pointy brackets"
		if (/[\r\n]/.test(url)) {
			return;
		}

		// "Both title and destination can contain backslash escapes and literal backslashes"
		url = decodeEntities(url);
		url = escapeBackslashes(url);
		url = encodeURI(decodeURI(url));
	}

	// We may need to backtrack to here if there is an invalid title
	let urlEnd = start;

	// Consume spaces
	spaces = consumeSpaces(state.src, start);
	start += spaces.length;

	// Get the title
	let title = "";
	let delimiter = state.src[start];
	if (delimiter === "'" || delimiter === '"') {
		start++;
		for (let i = start; i < state.src.length; i++) {
			if (state.src[i] === delimiter && !isEscaped(state.src, i)) {
				title = state.src.substring(start, i);
				start = i + 1;
				break;
			}
		}
	} else if (delimiter === "(") {
		start++;
		let level = 1;
		for (let i = start; i < state.src.length; i++) {
			if (!isEscaped(state.src, i)) {
				if (state.src[i] === ")") {
					level--;
					if (level === 0) {
						title = state.src.substring(start, i);
						start = i + 1;
						break;
					}
				} else if (state.src[i] === "(") {
					level++;
				}
			}
		}
	}

	// "The title may be omitted"
	// "The title may extend over multiple lines"
	if (title) {
		// "The title must be separated from the link destination by whitespace"
		if (!spaces.length) {
			return;
		}

		// "[The title] may not contain a blank line"
		if (BLANK_LINE_REGEX.test(title)) {
			return;
		}

		// "Both title and destination can contain backslash escapes and literal backslashes"
		title = decodeEntities(title);
		title = escapeBackslashes(title);
		title = escapeHtml(title);
	}

	// "[There may not be] non-whitespace characters after the title"
	if (!isNewLine(state.src[start - 1])) {
		for (; start < state.src.length; start++) {
			if (isNewLine(state.src[start])) {
				start++;
				break;
			} else if (isSpace(state.src.charCodeAt(start))) {
				continue;
			} else {
				// If the title is on a new line, only it is ignored,
				// otherwise the whole link is ignored
				if (spaces.includes("\n")) {
					title = "";
					start = urlEnd;
					break;
				} else {
					return;
				}
			}
		}
	}

	state.i = start;

	return { url, title };
}
