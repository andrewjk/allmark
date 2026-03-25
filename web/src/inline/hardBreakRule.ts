import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type MarkdownNode from "../types/MarkdownNode";
import isNewLine from "../utils/isNewLine";
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
	if (state.src[state.i] === "\\" && isNewLine(state.src[state.i + 1])) {
		let hb = newInline("hard_break", state.parentIndex + state.i, state.line, "\\", 0);
		hb.length = 2;
		state.i += 2;
		parent.children!.push(hb);
		return true;
	} else if (state.src[state.i] === " ") {
		let end = state.i;
		for (let i = state.i + 1; i < state.src.length; i++) {
			if (isNewLine(state.src[i])) {
				end = i;
				break;
			} else if (state.src[i] === " ") {
				continue;
			} else {
				return false;
			}
		}
		if (end - state.i >= 2) {
			let hb = newInline("hard_break", state.parentIndex + state.i, state.line, "  ", 0);
			hb.length = end - state.i;
			state.i = end + 1;
			parent.children!.push(hb);
			return true;
		}
	}

	return false;
}
