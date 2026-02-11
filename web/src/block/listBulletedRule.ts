import type BlockParserState from "../types/BlockParserState";
import type BlockRule from "../types/BlockRule";
import type MarkdownNode from "../types/MarkdownNode";
import isNewLine from "../utils/isNewLine";
import isSpace from "../utils/isSpace";
import { testListContinue, testListStart } from "./listRule";

const rule: BlockRule = {
	name: "list_bulleted",
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
	let char = state.src[state.i];
	if (
		(char === "-" || char === "+" || char === "*") &&
		// TODO: Should this be part of the isSpace/isNewLine check? i.e. eof counts as a space?
		(state.i === state.src.length - 1 || isSpace(state.src.charCodeAt(state.i + 1)))
	) {
		return {
			delimiter: char,
			markup: char,
			isBlank: state.i === state.src.length - 1 || isNewLine(state.src[state.i + 1]),
			type: "list_bulleted",
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
