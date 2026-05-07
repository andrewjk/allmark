import type BlockParserState from "../types/BlockParserState";
import { CARRIAGE_RETURN_CODE, NEW_LINE_CODE, SPACE_CODE, TAB_CODE } from "../utils/charCodes";
import isSpace from "../utils/isSpace";

export default function parseIndent(state: BlockParserState): void {
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
			} else if (charCode === NEW_LINE_CODE) {
				state.hasBlankLine = true;
				break;
			} else if (charCode === CARRIAGE_RETURN_CODE) {
				// Keep going...
			} else {
				break;
			}
		}
	}
}
