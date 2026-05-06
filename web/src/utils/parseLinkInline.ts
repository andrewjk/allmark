import type InlineParserState from "../types/InlineParserState";
import LinkReference from "../types/LinkReference";
import {
	ANGLE_RIGHT_CODE,
	ANGLE_LEFT_CODE,
	PAREN_CLOSE_CODE,
	PAREN_OPEN_CODE,
	QUOTE_DOUBLE_CODE,
	QUOTE_SINGLE_CODE,
} from "./charCodes";
import consumeSpaces from "./consumeSpaces";
import decodeEntities from "./decodeEntities";
import escapeBackslashes from "./escapeBackslashes";
import escapeHtml from "./escapeHtml";
import isEscaped from "./isEscaped";
import isSpace from "./isSpace";

// TODO: Get this from utils??
const BLANK_LINE_REGEX = /\r?\n[ \t]*\r?\n|\r[ \t]*\r/;

export default function parseLinkInline(
	state: InlineParserState,
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
	if (state.src.charCodeAt(start) === ANGLE_LEFT_CODE) {
		start++;
		for (let i = start; i < state.src.length; i++) {
			if (state.src.charCodeAt(i) === ANGLE_RIGHT_CODE && !isEscaped(state.src, i)) {
				url = state.src.substring(start, i);
				start = i + 1;
				break;
			}
		}
	} else {
		let level = 1;
		for (let i = start; i <= state.src.length; i++) {
			let charCode = state.src.charCodeAt(i);

			// "Any number of parentheses are allowed without escaping, as long
			// as they are balanced"
			if (charCode === PAREN_CLOSE_CODE && !isEscaped(state.src, i)) {
				level--;
				if (level === 0) {
					url = state.src.substring(start, i);
					start = i;
					break;
				}
			} else if (charCode === PAREN_OPEN_CODE && !isEscaped(state.src, i)) {
				level++;
			}

			if (i === state.src.length || isSpace(charCode)) {
				url = state.src.substring(start, i);
				start = i;
				break;
			}
		}

		// "The link destination may not be omitted"
		// "However, an empty link destination may be specified using angle brackets" (see above)
		//if (!url) {
		//	return;
		//}
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
	//let urlEnd = start;

	// Consume spaces
	spaces = consumeSpaces(state.src, start);
	start += spaces.length;

	// Get the title
	let title = "";
	let delimiterCode = state.src.charCodeAt(start);
	if (delimiterCode === PAREN_CLOSE_CODE) {
		// No title
	} else if (delimiterCode === QUOTE_SINGLE_CODE || delimiterCode === QUOTE_DOUBLE_CODE) {
		start++;
		for (let i = start; i < state.src.length; i++) {
			if (state.src.charCodeAt(i) === delimiterCode && !isEscaped(state.src, i)) {
				title = state.src.substring(start, i);
				start = i + 1;
				break;
			}
		}
	} else if (delimiterCode === PAREN_OPEN_CODE) {
		start++;
		let level = 1;
		for (let i = start; i < state.src.length; i++) {
			if (!isEscaped(state.src, i)) {
				let charCode = state.src.charCodeAt(i);
				if (charCode === PAREN_CLOSE_CODE) {
					level--;
					if (level === 0) {
						title = state.src.substring(start, i);
						start = i + 1;
						break;
					}
				} else if (charCode === PAREN_OPEN_CODE) {
					level++;
				}
			}
		}
	} else {
		// Bad character
		return;
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

	spaces = consumeSpaces(state.src, start);
	start += spaces.length;

	// "[There may not be] non-whitespace characters after the title"
	if (state.src.charCodeAt(start) !== PAREN_CLOSE_CODE) {
		return;
	}

	state.i = start + 1;

	return { url, title };
}
