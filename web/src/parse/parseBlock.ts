import type BlockParserState from "../types/BlockParserState";
import type MarkdownNode from "../types/MarkdownNode";
import isEscaped from "../utils/isEscaped";

export default function parseBlock(
	state: BlockParserState,
	parent: MarkdownNode,
	endOfLine: number,
): void {
	state.isEscaped = isEscaped(state.src, state.i);

	for (let rule of state.rules) {
		//let start = state.i;
		let handled = rule.testStart(state, parent, endOfLine);

		if (handled) {
			//if (state.debug) {
			//	console.log(`Found ${rule.name}, at ${start}`);
			//}

			// DEBUG: Make sure we are AFTER the line end?

			return;
		}
	}
}
