import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type MarkdownNode from "../types/MarkdownNode";
import { BACKSLASH_CODE, SPACE_CODE } from "../utils/charCodes";
import isNewLine from "../utils/isNewLine";
import newInline from "../utils/newInline";
import { appendChild } from "../utils/nodeUtils";

const rule: InlineRule = {
	name: "hard_break",
	test: testHardBreak,
};
export default rule;

/**
 * "A line break (not in a code span or HTML tag) that is preceded by two or
 * more spaces and does not occur at the end of a block is parsed as a hard line
 * break (rendered in HTML as a <br /> tag)"
 */

function testHardBreak(state: InlineParserState, parent: MarkdownNode): boolean {
	let charCode = state.src.charCodeAt(state.i);

	if (charCode === BACKSLASH_CODE && isNewLine(state.src.charCodeAt(state.i + 1))) {
		let hb = newInline("hard_break", state.parentIndex + state.i, state.line, "\\", 0);
		hb.length = 2;
		state.i += 2;
		appendChild(parent, hb);
		return true;
	}

	if (charCode === SPACE_CODE) {
		let end = state.i;
		for (let i = state.i + 1; i < state.src.length; i++) {
			let nextCharCode = state.src.charCodeAt(i);
			if (isNewLine(nextCharCode)) {
				end = i;
				break;
			} else if (nextCharCode === SPACE_CODE) {
				continue;
			} else {
				return false;
			}
		}
		if (end - state.i >= 2) {
			let hb = newInline("hard_break", state.parentIndex + state.i, state.line, "  ", 0);
			hb.length = end - state.i;
			state.i = end + 1;
			appendChild(parent, hb);
			return true;
		}
	}

	return false;
}
