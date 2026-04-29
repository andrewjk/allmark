import type InlineParserState from "../types/InlineParserState";
import type MarkdownNode from "../types/MarkdownNode";
import { CARRIAGE_RETURN_CODE, NEW_LINE_CODE } from "../utils/charCodes";

export default function parseInline(state: InlineParserState, parent: MarkdownNode): void {
	while (state.i < state.src.length) {
		let charCode = state.src.charCodeAt(state.i);
		if (charCode === NEW_LINE_CODE || charCode === CARRIAGE_RETURN_CODE) {
			// Treat Windows \r\n as \n
			if (
				charCode === CARRIAGE_RETURN_CODE &&
				state.src.charCodeAt(state.i + 1) === NEW_LINE_CODE
			) {
				state.i++;
			}

			state.line += 1;
			state.lineStart = state.i;
		}

		for (let rule of state.rules.values()) {
			let handled = rule.test(state, parent);
			//console.log("Rule:", rule.name, handled);
			if (handled) {
				// TODO: Make sure that state.i has been incremented to prevent infinite loops
				//console.log(`Found ${rule.name}`);
				break;
			}
		}
	}
}
