import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type MarkdownNode from "../types/MarkdownNode";
import isEscaped from "../utils/isEscaped";
import testTagMarks from "./tagMarksRule";

const rule: InlineRule = {
	name: "superscript",
	test: testSuperscript,
	precedence: 5,
};
export default rule;

function testSuperscript(state: InlineParserState, parent: MarkdownNode): boolean {
	let char = state.src[state.i];
	if (char === "^" && !isEscaped(state.src, state.i)) {
		return testTagMarks(rule.name, char, state, parent, rule.precedence!);
	}
	return false;
}
