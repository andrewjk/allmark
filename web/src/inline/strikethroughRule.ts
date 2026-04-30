import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type MarkdownNode from "../types/MarkdownNode";
import { TILDE_CODE } from "../utils/charCodes";
import testTagMarks from "./tagMarksRule";

const rule: InlineRule = {
	name: "strikethrough",
	test: testStrikethrough,
	precedence: 5,
};
export default rule;

/**
 * "Strikethrough text is any text wrapped in a matching pair of one or two
 * tildes (~).
 */
function testStrikethrough(state: InlineParserState, parent: MarkdownNode): boolean {
	if (!state.isEscaped && state.src.charCodeAt(state.i) === TILDE_CODE) {
		return testTagMarks(rule.name, "~", state, parent, rule.precedence!);
	}
	return false;
}
