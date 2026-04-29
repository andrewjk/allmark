import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type MarkdownNode from "../types/MarkdownNode";
import { EQUALS_CODE } from "../utils/charCodes";
import isEscaped from "../utils/isEscaped";
import testTagMarks from "./tagMarksRule";

const rule: InlineRule = {
	name: "highlight",
	test: testHighlight,
	precedence: 5,
};
export default rule;

function testHighlight(state: InlineParserState, parent: MarkdownNode): boolean {
	if (state.src.charCodeAt(state.i) === EQUALS_CODE && !isEscaped(state.src, state.i)) {
		return testTagMarks(rule.name, "=", state, parent, rule.precedence!);
	}
	return false;
}
