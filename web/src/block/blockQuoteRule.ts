import parseBlock from "../parse/parseBlock";
import type BlockParserState from "../types/BlockParserState";
import type BlockRule from "../types/BlockRule";
import type MarkdownNode from "../types/MarkdownNode";
import { ANGLE_RIGHT_CODE } from "../utils/charCodes";
import closeNode from "../utils/closeNode";
import movePastMarker from "../utils/movePastMarker";
import newBlock from "../utils/newBlock";
import { appendChild, getLastChild, hasChildren } from "../utils/nodeUtils";

const rule: BlockRule = {
	name: "block_quote",
	testStart,
	testContinue,
	closeNode: close,
};
export default rule;

/**
 * "A block quote marker consists of 0-3 spaces of initial indent, plus (a) the
 * character > together with a following space, or (b) a single character > not
 * followed by a space.
 *
 * The following rules define block quotes:
 *
 * 1. Basic case. If a string of lines Ls constitute a sequence of blocks Bs,
 *    then the result of prepending a block quote marker to the beginning of
 *    each line in Ls is a block quote containing Bs.
 *
 * 2. Laziness. If a string of lines Ls constitute a block quote with contents
 *    Bs, then the result of deleting the initial block quote marker from one or
 *    more lines in which the next non-whitespace character after the block
 *    quote marker is paragraph continuation text is a block quote with Bs as
 *    its content. Paragraph continuation text is text that will be parsed as
 *    part of the content of a paragraph, but does not occur at the beginning of
 *    the paragraph.
 *
 * 3. Consecutiveness. A document cannot contain two block quotes in a row
 *    unless there is a blank line between them.
 *
 * Nothing else counts as a block quote."
 */

function hasMarkup(state: BlockParserState) {
	return state.indent <= 3 && state.src.charCodeAt(state.i) === ANGLE_RIGHT_CODE;
}

function testStart(state: BlockParserState, parent: MarkdownNode) {
	let closedNode: MarkdownNode | undefined;

	if (parent.acceptsContent) {
		return false;
	}

	if (hasMarkup(state)) {
		if (parent.type === "paragraph") {
			closedNode = state.openNodes.pop();
			parent = state.openNodes.at(-1)!;
		}

		if (closedNode !== undefined) {
			closeNode(state, closedNode);
		}

		let quoteIndent = state.indent + 1;

		let quote = newBlock("block_quote", state.i, state.line, ">", quoteIndent);

		appendChild(parent, quote);
		state.openNodes.push(quote);

		movePastMarker(1, state);

		state.hasBlankLine = false;
		parseBlock(state, quote);

		return true;
	}

	return false;
}

function testContinue(state: BlockParserState, node: MarkdownNode) {
	if (hasMarkup(state)) {
		movePastMarker(1, state);
		return true;
	}

	if (state.hasBlankLine) {
		return false;
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

function close(state: BlockParserState, node: MarkdownNode) {
	// Swallow blank lines
	if (state.hasBlankLine && hasChildren(node)) {
		getLastChild(node)!.blankAfter = true;
		state.hasBlankLine = false;
	}
}
