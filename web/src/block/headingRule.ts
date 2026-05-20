import type BlockParserState from "../types/BlockParserState";
import type BlockRule from "../types/BlockRule";
import type MarkdownNode from "../types/MarkdownNode";
import { BACKSLASH_CODE, HASH_CODE } from "../utils/charCodes";
import closeNode from "../utils/closeNode";
import getEndOfLine from "../utils/getEndOfLine";
import isSpace from "../utils/isSpace";
import movePastMarker from "../utils/movePastMarker";
import newBlock from "../utils/newBlock";

const rule: BlockRule = {
	name: "heading",
	testStart,
	testContinue: () => false,
};
export default rule;

/**
 * "An ATX heading consists of a string of characters, parsed as inline content,
 * between an opening sequence of 1–6 unescaped # characters and an optional
 * closing sequence of any number of unescaped # characters. The opening
 * sequence of # characters must be followed by a space or by the end of line.
 * The optional closing sequence of #s must be preceded by a space and may be
 * followed by spaces only. The opening # character may be indented 0-3 spaces.
 * The raw contents of the heading are stripped of leading and trailing spaces
 * before being parsed as inline content. The heading level is equal to the
 * number of # characters in the opening sequence."
 */

function testStart(state: BlockParserState, parent: MarkdownNode) {
	if (parent.acceptsContent) {
		return false;
	}

	if (!state.isEscaped && state.indent <= 3 && state.src.charCodeAt(state.i) === HASH_CODE) {
		let level = 1;
		// TODO: peekUntil
		for (let j = state.i + 1; j < state.src.length; j++) {
			if (state.src.charCodeAt(j) === HASH_CODE) {
				level++;
			} else {
				break;
			}
		}
		if (level < 7 && isSpace(state.src.charCodeAt(state.i + level))) {
			let closedNode: MarkdownNode | undefined;
			// TODO: consumeSpace(state, state.i + level)

			// If there's an open paragraph, close it
			// TODO: Is there a better way to do this??
			if (parent.type === "paragraph") {
				closedNode = state.openNodes.pop();
				parent = state.openNodes.at(-1)!;
			}

			if (closedNode !== undefined) {
				closeNode(state, closedNode);
			}

			let heading = newBlock("heading", state.i, state.line, "#".repeat(level), 0);

			if (state.hasBlankLine && parent.children !== undefined && parent.children.length > 0) {
				parent.children.at(-1)!.blankAfter = true;
				state.hasBlankLine = false;
			}

			parent.children!.push(heading);

			movePastMarker(level, state);

			// Ignore optional end heading marks and spaces for content, but put
			// them in info so that they aren't lost
			let endOfLine = getEndOfLine(state);
			let end = endOfLine - 1;
			for (; end >= state.i; end--) {
				if (!isSpace(state.src.charCodeAt(end))) break;
			}
			for (; end >= state.i; end--) {
				let nextCharCode = state.src.charCodeAt(end);
				if (nextCharCode !== HASH_CODE) {
					if (nextCharCode === BACKSLASH_CODE || !isSpace(nextCharCode)) {
						end = endOfLine - 1;
					}
					break;
				}
			}
			end++;

			// Add a dummy block which will be removed in the renderer. It is
			// needed to get the correct mapping of elements (e.g. emphasis)
			// inside the header
			let content = newBlock("heading_content", state.i, state.line, "", 0);
			content.content = state.src.substring(state.i, end);
			heading.children = [content];

			if (end < endOfLine) {
				heading.info = state.src.substring(end, endOfLine);
			}

			state.i = endOfLine;
			heading.length = state.i - heading.index;
			content.length = state.i - content.index;

			return true;
		}
	}

	return false;
}
