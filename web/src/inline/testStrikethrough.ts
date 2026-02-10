import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type MarkdownNode from "../types/MarkdownNode";
import isEscaped from "../utils/isEscaped";
import testTagMarks from "./testTagMarks";

const rule: InlineRule = {
	name: "strikethrough",
	test: testStrikethrough,
};
export default rule;

/**
 * "Strikethrough text is any text wrapped in a matching pair of one or two
 * tildes (~).
 */
function testStrikethrough(state: InlineParserState, parent: MarkdownNode): boolean {
	let char = state.src[state.i];
	if (char === "~" && !isEscaped(state.src, state.i)) {
		return testTagMarks(rule.name, char, state, parent);
	}
	return false;
}
