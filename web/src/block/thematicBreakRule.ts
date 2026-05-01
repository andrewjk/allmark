import type BlockParserState from "../types/BlockParserState";
import type BlockRule from "../types/BlockRule";
import type MarkdownNode from "../types/MarkdownNode";
import { ASTERISK_CODE, DASH_CODE, UNDERSCORE_CODE } from "../utils/charCodes";
import closeNode from "../utils/closeNode";
import isNewLine from "../utils/isNewLine";
import isSpace from "../utils/isSpace";
import newBlock from "../utils/newBlock";
import { appendChild } from "../utils/nodeUtils";

const rule: BlockRule = {
	name: "thematic_break",
	testStart,
	testContinue,
};
export default rule;

/**
 * "A line consisting of 0-3 spaces of indentation, followed by a sequence of
 * three or more matching -, _, or * characters, each followed optionally by any
 * number of spaces or tabs, forms a thematic break."
 */
function testStart(state: BlockParserState, parent: MarkdownNode) {
	if (parent.acceptsContent) {
		return false;
	}

	let charCode = state.src.charCodeAt(state.i);
	if (
		state.indent <= 3 &&
		(charCode === DASH_CODE || charCode === UNDERSCORE_CODE || charCode === ASTERISK_CODE)
	) {
		let matched = 1;
		let end = state.i + 1;
		for (; end < state.src.length; end++) {
			let nextCharCode = state.src.charCodeAt(end);
			if (nextCharCode === charCode) {
				matched++;
			} else if (isNewLine(nextCharCode)) {
				// TODO: Handle windows crlf
				end++;
				break;
			} else if (isSpace(nextCharCode)) {
				continue;
			} else {
				return false;
			}
		}
		if (matched >= 3) {
			let closedNode: MarkdownNode | undefined;

			if (state.maybeContinue) {
				state.maybeContinue = false;
				let i = state.openNodes.length;
				while (i-- > 1) {
					let node = state.openNodes[i];
					if (node.maybeContinuing) {
						node.maybeContinuing = false;
						closedNode = node;
						state.openNodes.length = i;
						break;
					}
				}
				parent = state.openNodes.at(-1)!;
			}

			if (parent.type === "paragraph") {
				closedNode = state.openNodes.pop();
				parent = state.openNodes.at(-1)!;
			}

			// HACK: Special case for a thematic break in a list
			// Maybe do this with interrupts?
			if (
				parent.type === "list_item" &&
				!state.hasBlankLine &&
				charCode === parent.delimiter.charCodeAt(0)
			) {
				closedNode = state.openNodes.pop();
				closedNode = state.openNodes.pop();
				parent = state.openNodes.at(-1)!;
			}
			if (parent.type === "list_bulleted" || parent.type === "list_ordered") {
				closedNode = state.openNodes.pop();
				parent = state.openNodes.at(-1)!;
			}

			if (closedNode !== undefined) {
				closeNode(state, closedNode);
			}

			let markup = state.src.substring(state.i, end);
			let tbr = newBlock("thematic_break", state.i, state.line, markup, 0);
			tbr.length = end - state.i;
			appendChild(parent, tbr);
			state.i = end;
			return true;
		}
	}

	return false;
}

function testContinue(_state: BlockParserState, _node: MarkdownNode) {
	return false;
}
