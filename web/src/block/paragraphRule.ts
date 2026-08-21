import type BlockParserState from "../types/BlockParserState";
import type BlockRule from "../types/BlockRule";
import type MarkdownNode from "../types/MarkdownNode";
import getLineEnding from "../utils/getLineEnding";
import newBlock from "../utils/newBlock";

const rule: BlockRule = {
	name: "paragraph",
	testStart,
	testContinue,
};
export default rule;

/**
 * "A sequence of non-blank lines that cannot be interpreted as other kinds of
 * blocks forms a paragraph. The contents of the paragraph are the result of
 * parsing the paragraph’s raw content as inlines. The paragraph’s raw content
 * is formed by concatenating the lines and removing initial and final
 * whitespace."
 */

function testStart(state: BlockParserState, parent: MarkdownNode, endOfLine: number) {
	if (parent.acceptsContent) {
		return false;
	}

	if (parent.type === "paragraph" && !parent.blankAfter) {
		return false;
	}

	let content = state.src.substring(state.i, endOfLine) + getLineEnding(state, endOfLine);

	if (!/[^\s]/.test(content)) {
		return true;
	}

	let paragraph = newBlock("paragraph", state.i, state.line, "", 0);
	paragraph.content = content;

	if (state.hasBlankLine && parent.children !== undefined && parent.children.length > 0) {
		parent.children.at(-1)!.blankAfter = true;
		state.hasBlankLine = false;
	}

	parent.children!.push(paragraph);
	state.openNodes.push(paragraph);

	return true;
}

function testContinue(state: BlockParserState, _node: MarkdownNode) {
	if (state.hasBlankLine) {
		return false;
	}

	return true;
}
