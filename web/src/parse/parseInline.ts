import type InlineParserState from "../types/InlineParserState";
import type MarkdownNode from "../types/MarkdownNode";

export default function parseInline(
	state: InlineParserState,
	parent: MarkdownNode,
	end = -1,
): void {
	let inlineEnd = end === -1 ? state.src.length : end;
	while (state.i < inlineEnd) {
		let char = state.src[state.i];
		if (end === -1 && (char === "\r" || char === "\n")) {
			// Treat Windows \r\n as \n
			if (char === "\r" && state.src[state.i + 1] === "\n") {
				state.i++;
			}

			state.line += 1;
			state.lineStart = state.i;
		}

		for (let rule of state.rules.values()) {
			let handled = rule.test(state, parent, inlineEnd);
			//console.log("Rule:", rule.name, handled);
			if (handled) {
				// TODO: Make sure that state.i has been incremented to prevent infinite loops
				//console.log(`Found ${rule.name}`);
				break;
			}
		}
	}
}
