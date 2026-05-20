import type BlockParserState from "../types/BlockParserState";
import type BlockRule from "../types/BlockRule";
import type MarkdownNode from "../types/MarkdownNode";
import { ANGLE_RIGHT_CODE } from "../utils/charCodes";
import getEndOfLine from "../utils/getEndOfLine";
import movePastMarker from "../utils/movePastMarker";
import newBlock from "../utils/newBlock";

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

function hasMarkup(state: BlockParserState) {
	return state.indent <= 3 && state.src.charCodeAt(state.i) === ANGLE_RIGHT_CODE;
}

function testStart(state: BlockParserState, parent: MarkdownNode) {
	if (parent.acceptsContent) {
		return false;
	}

	if (hasMarkup(state)) {
		const match = state.src.slice(state.i + 1).match(ALERT_REGEX);
		if (match !== null) {
			let quoteIndent = state.indent + 1;

			let quote = newBlock("alert", state.i, state.line, match[1].toLowerCase(), quoteIndent);

			parent.children!.push(quote);
			state.openNodes.push(quote);
			state.i = getEndOfLine(state);

			return true;
		}
	}

	return false;
}

function testContinue(state: BlockParserState, _node: MarkdownNode) {
	if (hasMarkup(state)) {
		movePastMarker(1, state);
		return true;
	}

	if (state.hasBlankLine) {
		return false;
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
