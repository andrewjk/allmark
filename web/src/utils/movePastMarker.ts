import type BlockParserState from "../types/BlockParserState";

export default function movePastMarker(markerLength: number, state: BlockParserState): void {
	// If the marker (e.g. '>' or '-') is followed by a tab, the markup is
	// considered to be '> ' followed by 2 spaces. Otherwise we reset the indent
	// for children
	state.i += markerLength;
	if (state.src[state.i] === "\t" && state.src[state.i + 1] === "\t") {
		state.indent = 6;
		state.i += 2;
	} else if (state.src[state.i] === " ") {
		state.indent = 0;
		state.i += 1;
	}
}
