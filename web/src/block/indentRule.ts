import type BlockParserState from "../types/BlockParserState";
import type BlockRule from "../types/BlockRule";
import { SPACE_CODE, TAB_CODE } from "../utils/charCodes";
import isSpace from "../utils/isSpace";

const rule: BlockRule = {
	name: "indent",
	testStart,
	testContinue: () => false,
};
export default rule;

// TODO: Should this be built in and not a rule??
function testStart(state: BlockParserState) {
	if (isSpace(state.src.charCodeAt(state.i))) {
		for (; state.i < state.src.length; state.i++) {
			let charCode = state.src.charCodeAt(state.i);
			if (charCode === SPACE_CODE) {
				// TODO: All the other spaces
				state.indent += 1;
			} else if (charCode === TAB_CODE) {
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
