import type BlockParserState from "../types/BlockParserState";
import type BlockRule from "../types/BlockRule";
import type MarkdownNode from "../types/MarkdownNode";
import closeNode from "../utils/closeNode";
import getEndOfLine from "../utils/getEndOfLine";
import isEscaped from "../utils/isEscaped";
import isSpace from "../utils/isSpace";
import newNode from "../utils/newNode";

const rule: BlockRule = {
	name: "heading",
	testStart,
	testContinue,
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

	let char = state.src[state.i];
	if (state.indent <= 3 && char === "#" && !isEscaped(state.src, state.i)) {
		let level = 1;
		// TODO: peekUntil
		for (let j = state.i + 1; j < state.src.length; j++) {
			if (state.src[j] === "#") {
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

			let heading = newNode("heading", true, state.i, state.line, 1, "#".repeat(level), 0, []);

			if (state.hasBlankLine && parent.children !== undefined && parent.children.length > 0) {
				parent.children.at(-1)!.blankAfter = true;
				state.hasBlankLine = false;
			}

			parent.children!.push(heading);

			// HACK: ignore optional end heading marks and spaces, destructively
			state.i += level;
			let endOfLine = getEndOfLine(state);
			let end = endOfLine - 1;
			for (; end >= state.i; end--) {
				if (!isSpace(state.src.charCodeAt(end))) break;
			}
			for (; end >= state.i; end--) {
				if (state.src[end] !== "#") {
					if (state.src[end] === "\\" || !isSpace(state.src.charCodeAt(end))) {
						end = endOfLine - 1;
					}
					break;
				}
			}
			heading.content = state.src.substring(state.i, end + 1);
			state.i = endOfLine;

			return true;
		}
	}

	return false;
}

function testContinue(_state: BlockParserState, _node: MarkdownNode) {
	return false;
}
