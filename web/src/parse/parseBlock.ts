import type BlockParserState from "../types/BlockParserState";
import type MarkdownNode from "../types/MarkdownNode";

export default function parseBlock(state: BlockParserState, parent: MarkdownNode): void {
	for (let rule of state.rules.values()) {
		//let start = state.i;
		let handled = rule.testStart(state, parent);

		if (handled) {
			//if (state.debug) {
			//	console.log(`Found ${rule.name}, at ${start}`);
			//}

			// DEBUG: Make sure we are AFTER the line end?

			return;
		}
	}
}
