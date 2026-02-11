import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type MarkdownNode from "../types/MarkdownNode";
import isEscaped from "../utils/isEscaped";
import testTagMarks from "./tagMarksRule";

const rule: InlineRule = {
	name: "subscript",
	test: testSubscript,
};
export default rule;

function testSubscript(state: InlineParserState, parent: MarkdownNode): boolean {
	let char = state.src[state.i];
	if (char === "~" && !isEscaped(state.src, state.i)) {
		// Subscripts can only be one character long, otherwise they are a GFM strikethrough
		if (state.src[state.i + 1] === "~") {
			return false;
		}
		return testTagMarks(rule.name, char, state, parent);
	}
	return false;
}
