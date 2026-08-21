import type BlockParserState from "../types/BlockParserState";
import type BlockRule from "../types/BlockRule";
import type MarkdownNode from "../types/MarkdownNode";
import getLineEnding from "../utils/getLineEnding";

const rule: BlockRule = {
	name: "content",
	testStart,
	testContinue: () => false,
};
export default rule;

function testStart(state: BlockParserState, parent: MarkdownNode, endOfLine: number) {
	let content = state.src.substring(state.i, endOfLine) + getLineEnding(state, endOfLine);
	if (parent.acceptsContent) {
		if (state.hasBlankLine) {
			state.hasBlankLine = false;
		} else {
			parent.content += " ".repeat(state.indent);
		}
	} else {
		parent.content += state.spaces;
		state.spaces = "";
	}
	parent.content += content;
	return true;
}
