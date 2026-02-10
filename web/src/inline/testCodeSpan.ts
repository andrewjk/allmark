import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type MarkdownNode from "../types/MarkdownNode";
import addMarkupAsText from "../utils/addMarkupAsText";
import isEscaped from "../utils/isEscaped";
import isSpace from "../utils/isSpace";
import newNode from "../utils/newNode";
import skipSpaces from "../utils/skipSpaces";

const rule: InlineRule = {
	name: "code_span",
	test: testCodeSpan,
};
export default rule;

function testCodeSpan(state: InlineParserState, parent: MarkdownNode): boolean {
	let char = state.src[state.i];
	if (char === "`" && !isEscaped(state.src, state.i)) {
		let openMatched = 1;
		let openEnd = state.i + 1;
		for (; openEnd < state.src.length; openEnd++) {
			let nextChar = state.src[openEnd];
			if (nextChar === char) {
				openMatched++;
			} else {
				break;
			}
		}

		let markup = "`".repeat(openMatched);

		// "The contents of a code block are literal text, and do not get parsed as Markdown"
		let closeEnd = state.i + openMatched;
		closeEnd = skipSpaces(state.src, closeEnd);
		let closeMatched = 0;
		for (; closeEnd < state.src.length; closeEnd++) {
			if (state.src[closeEnd] === char) {
				for (; closeEnd < state.src.length; closeEnd++) {
					let nextChar = state.src[closeEnd];
					if (nextChar === char) {
						closeMatched++;
					} else {
						break;
					}
				}
				if (closeMatched === openMatched) {
					break;
				}
				closeMatched = 0;
			}
		}
		if (closeMatched === openMatched) {
			state.i += openMatched;

			let content = state.src.substring(state.i, closeEnd - closeMatched);

			// "[L]ine endings are converted to spaces"
			content = content.replaceAll(/[\r\n]/g, " ");

			// "If the resulting string both begins and ends with a space
			// character, but does not consist entirely of space characters, a
			// single space character is removed from the front and back. This
			// allows you to include code that begins or ends with backtick
			// characters, which must be separated by whitespace from the
			// opening or closing backtick strings"
			//
			// "Only spaces, and not unicode whitespace in general, are stripped
			// in this way"
			if (
				/[^\s]/.test(content) &&
				isSpace(content.charCodeAt(0)) &&
				isSpace(content.charCodeAt(content.length - 1))
			) {
				content = content.substring(1, content.length - 1);
			}

			let text = newNode("text", false, state.i, state.line, 1, content, 0);
			let code = newNode("code_span", false, state.i, state.line, 1, markup, 0, [text]);
			parent.children!.push(code);

			state.i = closeEnd;

			return true;
		}

		addMarkupAsText(markup, state, parent);

		return true;
	}

	return false;
}
