import type BlockParserState from "../types/BlockParserState";
import type BlockRule from "../types/BlockRule";
import type MarkdownNode from "../types/MarkdownNode";
import closeNode from "../utils/closeNode";
import isNewLine from "../utils/isNewLine";
import isSpace from "../utils/isSpace";
import newNode from "../utils/newNode";

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

	let char = state.src[state.i];
	if (state.indent <= 3 && (char === "-" || char === "_" || char === "*")) {
		let matched = 1;
		let end = state.i + 1;
		for (; end < state.src.length; end++) {
			let nextChar = state.src[end];
			if (nextChar === char) {
				matched++;
			} else if (isNewLine(nextChar)) {
				// TODO: Handle windows crlf
				end++;
				break;
			} else if (isSpace(state.src.charCodeAt(end))) {
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

			// HACK: Special case for an underlined heading in a list
			// Maybe do this with interrupts?
			if (parent.type === "list_item" && !parent.blankAfter && char === parent.delimiter) {
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
			let tbr = newNode("thematic_break", true, state.i, state.line, 1, markup, 0, []);
			parent.children!.push(tbr);
			state.i = end;
			return true;
		}
	}

	return false;
}

function testContinue(_state: BlockParserState, _node: MarkdownNode) {
	return false;
}
