import type BlockParserState from "../types/BlockParserState";
import { SPACE_CODE, TAB_CODE } from "./charCodes";

export default function movePastMarker(markerLength: number, state: BlockParserState): void {
	// If the marker (e.g. '>' or '-') is followed by a tab, the markup is
	// considered to be '> ' followed by 2 spaces. Otherwise we reset the indent
	// for children
	state.i += markerLength;
	let charCode = state.src.charCodeAt(state.i);
	if (charCode === TAB_CODE && state.src.charCodeAt(state.i + 1) === TAB_CODE) {
		state.indent = 6;
		state.i += 2;
	} else if (charCode === SPACE_CODE) {
		state.indent = 0;
		state.i += 1;
	}
}
