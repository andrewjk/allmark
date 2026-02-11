import type BlockParserState from "../types/BlockParserState";
import type BlockRule from "../types/BlockRule";
import type MarkdownNode from "../types/MarkdownNode";
import isNewLine from "../utils/isNewLine";
import isSpace from "../utils/isSpace";

const rule: BlockRule = {
	name: "heading_underline",
	testStart,
	testContinue,
};
export default rule;

/**
 * "A setext heading consists of one or more lines of text, each containing at
 * least one non-whitespace character, with no more than 3 spaces indentation,
 * followed by a setext heading underline. The lines of text must be such that,
 * were they not followed by the setext heading underline, they would be
 * interpreted as a paragraph: they cannot be interpretable as a code fence, ATX
 * heading, block quote, thematic break, list item, or HTML block.
 *
 * A setext heading underline is a sequence of = characters or a sequence of -
 * characters, with no more than 3 spaces indentation and any number of trailing
 * spaces. If a line containing a single - can be interpreted as an empty list
 * items, it should be interpreted this way and not as a setext heading
 * underline.
 *
 * The heading is a level 1 heading if = characters are used in the setext
 * heading underline, and a level 2 heading if - characters are used. The
 * contents of the heading are the result of parsing the preceding lines of text
 * as CommonMark inline content.
 *
 * In general, a setext heading need not be preceded or followed by a blank
 * line. However, it cannot interrupt a paragraph, so when a setext heading
 * comes after a paragraph, a blank line is needed between them."
 */

function testStart(state: BlockParserState, parent: MarkdownNode) {
	if (state.maybeContinue) {
		let i = state.openNodes.length;
		while (i-- > 1) {
			let node = state.openNodes[i];
			if (node.maybeContinuing) {
				return false;
			}
		}
	}

	let char = state.src[state.i];
	if (state.indent <= 3 && (char === "=" || char === "-")) {
		let matched = 1;
		let end = state.i + 1;
		for (; end < state.src.length; end++) {
			let nextChar = state.src[end];
			if (nextChar === char) {
				// "The setext heading underline cannot contain internal spaces"
				if (matched > 0 && isSpace(state.src.charCodeAt(end - 1))) {
					return false;
				}
				matched++;
			} else if (isNewLine(nextChar)) {
				// TODO: Handle windows crlf
				end++;
				break;
			} else if (isSpace(state.src.charCodeAt(end))) {
				continue;
			} else if (nextChar !== char) {
				return false;
			}
		}

		// HACK: Special case for an underlined heading in a list
		// Maybe do this with interrupts?
		if (parent.type === "list_item" && !parent.blankAfter && state.indent === parent.indent) {
			state.openNodes.pop();
			state.openNodes.pop();
			parent = state.openNodes.at(-1)!;
		}

		let haveParagraph =
			parent.type === "paragraph" && !parent.blankAfter && /[^\s]/.test(parent.content);
		if (haveParagraph) {
			parent.type = "heading";
			parent.markup = state.src.substring(state.i, end);
			state.i = end;
			return true;
		}
	}

	return false;
}

function testContinue(_state: BlockParserState, _node: MarkdownNode) {
	return false;
}
