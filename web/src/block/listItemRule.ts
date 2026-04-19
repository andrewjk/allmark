import type BlockParserState from "../types/BlockParserState";
import type BlockRule from "../types/BlockRule";
import type MarkdownNode from "../types/MarkdownNode";
import { isNumeric } from "../utils/isAlphaNumeric";

const rule: BlockRule = {
	name: "list_item",
	testStart,
	testContinue,
};
export default rule;

function testStart(_state: BlockParserState, _parent: MarkdownNode) {
	return false;
}

function testContinue(state: BlockParserState, node: MarkdownNode) {
	let char = state.src[state.i];

	if (state.indent >= node.subindent) {
		state.indent -= node.subindent;
		return true;
	}

	// This only applies to the lowest list_item
	let i = state.openNodes.length;
	let itemNode: MarkdownNode | undefined;
	while (i-- > 1) {
		let openNode = state.openNodes[i];
		if (openNode.type === "list_item") {
			itemNode = openNode;
		} else if (openNode.type === "list_ordered") {
			let numbers = "";
			let end = state.i;
			while (isNumeric(state.src.charCodeAt(end))) {
				numbers += state.src[end];
				end++;
			}
			let delimiter = state.src[end];
			if (
				state.indent <= 3 &&
				state.indent < itemNode!.subindent &&
				numbers.length &&
				delimiter === node.delimiter
			) {
				return false;
			}
			// Break only when content is inside this nesting level, otherwise
			// continue walking up to check ancestor lists
			if (state.indent >= itemNode!.subindent) {
				break;
			}
		} else if (openNode.type === "list_bulleted") {
			if (state.indent <= 3 && state.indent < itemNode!.subindent && char === node.delimiter) {
				return false;
			}
			// Break only when content is inside this nesting level, otherwise
			// continue walking up to check ancestor lists
			if (state.indent >= itemNode!.subindent) {
				break;
			}
		}
	}

	if (state.hasBlankLine) {
		return true;
	}

	let openNode = state.openNodes.at(-1)!;
	if (openNode.type === "paragraph") {
		// We won't know until we try more things
		state.maybeContinue = true;
		node.maybeContinuing = true;
		return true;
	}

	return false;
}
