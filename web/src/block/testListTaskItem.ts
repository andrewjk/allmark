import type BlockParserState from "../types/BlockParserState";
import type BlockRule from "../types/BlockRule";
import type MarkdownNode from "../types/MarkdownNode";
import isSpace from "../utils/isSpace";
import newNode from "../utils/newNode";

const rule: BlockRule = {
	name: "list_task_item",
	testStart,
	testContinue,
};
export default rule;

/**
 * "GFM enables the tasklist extension, where an additional processing step is
 * performed on list items.
 *
 * A task list item is a list item where the first block in it is a paragraph
 * which begins with a task list item marker and at least one whitespace
 * character before any other content.
 *
 * A task list item marker consists of an optional number of spaces, a left
 * bracket ([), either a whitespace character or the letter x in either
 * lowercase or uppercase, and then a right bracket (]).
 *
 * When rendered, the task list item marker is replaced with a semantic checkbox
 * element; in an HTML output, this would be an <input type="checkbox"> element.
 *
 * If the character between the brackets is a whitespace character, the checkbox
 * is unchecked. Otherwise, the checkbox is checked.
 *
 * This spec does not define how the checkbox elements are interacted with: in
 * practice, implementors are free to render the checkboxes as disabled or
 * inmutable elements, or they may dynamically handle dynamic interactions (i.e.
 * checking, unchecking) in the final rendered document."
 */

function testStart(state: BlockParserState, parent: MarkdownNode) {
	if (parent.type === "list_item") {
		let start = state.i;
		if (
			state.src[start] === "[" &&
			state.src[start + 2] === "]" &&
			isSpace(state.src.charCodeAt(start + 3)) &&
			// GitHub doesn't support task lists in block quotes
			state.openNodes.find((n) => n.type === "block_quote") === undefined
		) {
			let markup = `[${state.src[start + 1]}]`;
			// HACK: It should be a block, but it's not for output reasons
			let task = newNode("list_task_item", false, state.i, state.line, 1, markup, 0, []);
			parent.children!.push(task);
			state.i = start + 3;
		}
	}

	return false;
}

function testContinue(_state: BlockParserState, _node: MarkdownNode) {
	return false;
}
