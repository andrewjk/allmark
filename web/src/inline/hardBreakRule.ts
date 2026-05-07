import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type MarkdownNode from "../types/MarkdownNode";
import {
	BACKSLASH_CODE,
	CARRIAGE_RETURN_CODE,
	NEW_LINE_CODE,
	SPACE_CODE,
} from "../utils/charCodes";
import newInline from "../utils/newInline";

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

	if (charCode === BACKSLASH_CODE) {
		let end = state.i + 2;
		let nextCharCode = state.src.charCodeAt(state.i + 1);
		if (nextCharCode === CARRIAGE_RETURN_CODE) {
			nextCharCode = state.src.charCodeAt(state.i + 2);
			end++;
		}
		if (nextCharCode === NEW_LINE_CODE) {
			let hb = newInline("hard_break", state.parentIndex + state.i, state.line, "\\", 0);
			hb.length = 2;
			parent.children!.push(hb);
			state.i = end;
			return true;
		}
	}

	if (charCode === SPACE_CODE) {
		let spaces = 1;
		let end = state.src.length;
		for (let i = state.i + 1; i < state.src.length; i++) {
			let nextCharCode = state.src.charCodeAt(i);
			if (nextCharCode === NEW_LINE_CODE) {
				end = i;
				break;
			} else if (nextCharCode === CARRIAGE_RETURN_CODE) {
				// Keep going...
			} else if (nextCharCode === SPACE_CODE) {
				spaces++;
			} else {
				return false;
			}
		}
		if (spaces >= 2) {
			let hb = newInline("hard_break", state.parentIndex + state.i, state.line, "  ", 0);
			hb.length = spaces;
			parent.children!.push(hb);
			state.i = end + 1;
			return true;
		}
	}

	return false;
}
