import type BlockParserState from "../types/BlockParserState";
import type BlockRule from "../types/BlockRule";
import type MarkdownNode from "../types/MarkdownNode";
import { isNumeric } from "../utils/isAlphaNumeric";
import isNewLine from "../utils/isNewLine";
import isSpace from "../utils/isSpace";
import { testListContinue, testListStart } from "./listRule";

const rule: BlockRule = {
	name: "list_ordered",
	testStart,
	testContinue,
};
export default rule;

/**
 * "A list is a sequence of one or more list items of the same type. The list
 * items may be separated by any number of blank lines.
 *
 * Two list items are of the same type if they begin with a list marker of the
 * same type. Two list markers are of the same type if (a) they are bullet list
 * markers using the same character (-, +, or *) or (b) they are ordered list
 * numbers with the same delimiter (either . or )).
 *
 * A list is an ordered list if its constituent list items begin with ordered
 * list markers, and a bullet list if its constituent list items begin with
 * bullet list markers.
 *
 * The start number of an ordered list is determined by the list number of its
 * initial list item. The numbers of subsequent list items are disregarded.
 *
 * A list is loose if any of its constituent list items are separated by blank
 * lines, or if any of its constituent list items directly contain two
 * block-level elements with a blank line between them. Otherwise a list is
 * tight. (The difference in HTML output is that paragraphs in a loose list are
 * wrapped in <p> tags, while paragraphs in a tight list are not.)"
 */

function getMarkup(state: BlockParserState) {
	let numbers = "";
	let end = state.i;
	while (isNumeric(state.src.charCodeAt(end))) {
		numbers += state.src[end];
		end++;
	}
	let orderedList =
		numbers.length > 0 &&
		numbers.length < 10 &&
		(state.src[end] === "." || state.src[end] === ")") &&
		(end === state.src.length - 1 || isSpace(state.src.charCodeAt(end + 1)));
	if (orderedList) {
		let delimiter = state.src[end];
		return {
			delimiter,
			markup: numbers + delimiter,
			isBlank: end === state.src.length - 1 || isNewLine(state.src[end + 1]),
			type: "list_ordered",
		};
	}
}

function testStart(state: BlockParserState, parent: MarkdownNode) {
	if (parent.acceptsContent) {
		return false;
	}

	let info = getMarkup(state);
	return testListStart(state, parent, info);
}

function testContinue(state: BlockParserState, node: MarkdownNode) {
	let info = getMarkup(state);
	return testListContinue(state, node, info);
}
