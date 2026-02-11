import type BlockParserState from "../types/BlockParserState";
import type BlockRule from "../types/BlockRule";
import type MarkdownNode from "../types/MarkdownNode";
import getEndOfLine from "../utils/getEndOfLine";

const rule: BlockRule = {
	name: "content",
	testStart,
	testContinue,
};
export default rule;

function testStart(state: BlockParserState, parent: MarkdownNode) {
	let endOfLine = getEndOfLine(state);
	let content = state.src.substring(state.i, endOfLine);
	if (parent.acceptsContent) {
		if (state.hasBlankLine) {
			//parent.content += "\n";
		} else {
			parent.content += " ".repeat(state.indent);
		}
		parent.content += content;
		state.hasBlankLine = false;
	} else {
		parent.content += content;
	}
	state.i = endOfLine;
	return true;
}

function testContinue(_state: BlockParserState, _node: MarkdownNode) {
	return false;
}
