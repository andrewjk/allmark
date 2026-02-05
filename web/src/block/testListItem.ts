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

	// This only applies to the lowest list_item
	// TODO: Is there a way to check this only once instead for each open list_item??
	// TODO: Split this out into different list_items
	let i = state.openNodes.length;
	let itemNode: MarkdownNode | undefined;
	while (i--) {
		let openNode = state.openNodes[i];
		if (openNode.type === "list_item") {
			itemNode = openNode;
		} else if (state.openNodes[i].type === "list_ordered") {
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
			break;
		} else if (state.openNodes[i].type === "list_bulleted") {
			if (state.indent <= 3 && state.indent < itemNode!.subindent && char === node.delimiter) {
				return false;
			}
			break;
		}
	}

	if (state.indent >= node.subindent) {
		// Unindent to prevent code blocks
		state.indent -= node.subindent;
		return true;
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
