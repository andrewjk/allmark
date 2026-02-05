import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type MarkdownNode from "../types/MarkdownNode";
import isNewLine from "../utils/isNewLine";
import newNode from "../utils/newNode";

const rule: InlineRule = {
	name: "hard_break",
	test: testHardBreak,
};
export default rule;

function testHardBreak(state: InlineParserState, parent: MarkdownNode, _end: number): boolean {
	if (state.src[state.i] === "\\" && isNewLine(state.src[state.i + 1])) {
		let hb = newNode("hard_break", false, state.i, state.line, 1, "\\", 0);
		state.i += 2;
		parent.children!.push(hb);
		return true;
	}

	return false;
}
