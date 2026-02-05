import type BlockParserState from "../types/BlockParserState";
import type BlockRule from "../types/BlockRule";
import type MarkdownNode from "../types/MarkdownNode";
import isSpace from "../utils/isSpace";

const rule: BlockRule = {
	name: "indent",
	testStart,
	testContinue,
};
export default rule;

// TODO: Should this be built in and not a rule??
function testStart(state: BlockParserState) {
	//let char = state.src[state.i];
	if (isSpace(state.src.charCodeAt(state.i))) {
		for (; state.i < state.src.length; state.i++) {
			let char = state.src[state.i];
			if (char === " ") {
				// TODO: All the other spaces
				state.indent += 1;
			} else if (char === "\t") {
				// Set spaces to the next tabstop of 4 characters (e.g. for '  \t', set
				// the spaces to 4)
				state.indent += 4 - (state.indent % 4);
			} else {
				break;
			}
		}
	}

	return false;
}

function testContinue(_state: BlockParserState, _node: MarkdownNode) {
	return false;
}
