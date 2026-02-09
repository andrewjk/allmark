import type BlockParserState from "../types/BlockParserState";
import type BlockRule from "../types/BlockRule";
import type MarkdownNode from "../types/MarkdownNode";
import closeNode from "../utils/closeNode";
import getEndOfLine from "../utils/getEndOfLine";
import newNode from "../utils/newNode";

const rule: BlockRule = {
	name: "alert",
	testStart,
	testContinue,
	closeNode: close,
};
export default rule;

/**
 * Alerts, also sometimes known as callouts or admonitions, are a Markdown
 * extension based on the blockquote syntax that you can use to emphasize
 * critical information. On GitHub, they are displayed with distinctive colors
 * and icons to indicate the significance of the content.
 *
 * Use alerts only when they are crucial for user success and limit them to one
 * or two per article to prevent overloading the reader. Additionally, you
 * should avoid placing alerts consecutively. Alerts cannot be nested within
 * other elements.
 *
 * To add an alert, use a special blockquote line specifying the alert type,
 * followed by the alert information in a standard blockquote. Five types of
 * alerts are available:
 */

const ALERT_REGEX = /^\s*\[!(note|tip|important|warning|caution)]/i;

function hasMarkup(char: string, state: BlockParserState) {
	return state.indent <= 3 && char === ">";
}

function testStart(state: BlockParserState, parent: MarkdownNode) {
	let closedNode: MarkdownNode | undefined;

	if (parent.acceptsContent) {
		return false;
	}

	let char = state.src[state.i];
	if (hasMarkup(char, state)) {
		const match = state.src.slice(state.i + 1).match(ALERT_REGEX);
		if (match !== null) {
			if (parent.type === "paragraph") {
				closedNode = state.openNodes.pop();
				parent = state.openNodes.at(-1)!;
			}

			if (closedNode !== undefined) {
				closeNode(state, closedNode);
			}

			let quoteIndent = state.indent + 1;

			let quote = newNode(
				"alert",
				true,
				state.i,
				state.line,
				1,
				match[1].toLowerCase(),
				quoteIndent,
				[],
			);

			parent.children!.push(quote);
			state.openNodes.push(quote);

			state.i = getEndOfLine(state);

			//parseBlock(state, quote);

			return true;
		}
	}

	return false;
}

function movePastMarker(state: BlockParserState) {
	// HACK: I think this can be done better
	// If the '>' is followed by a tab, the markup is considered to be '> '
	// followed by 2 spaces. Otherwise we reset the indent for children
	state.i += 1;
	if (state.src[state.i] === "\t" && state.src[state.i + 1] === "\t") {
		state.indent = 6;
		state.i += 2;
	} else if (state.src[state.i] === " ") {
		state.indent = 0;
		state.i += 1;
	}
}

function testContinue(state: BlockParserState, node: MarkdownNode) {
	let char = state.src[state.i];
	if (hasMarkup(char, state)) {
		movePastMarker(state);
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
	if (state.hasBlankLine && node.children !== undefined && node.children.length > 0) {
		node.children.at(-1)!.blankAfter = true;
		state.hasBlankLine = false;
	}
}
