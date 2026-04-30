import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type MarkdownNode from "../types/MarkdownNode";
import { TILDE_CODE } from "../utils/charCodes";
import testTagMarks from "./tagMarksRule";

const rule: InlineRule = {
	name: "subscript",
	test: testSubscript,
	precedence: 5,
};
export default rule;

function testSubscript(state: InlineParserState, parent: MarkdownNode): boolean {
	if (
		!state.isEscaped &&
		state.src.charCodeAt(state.i) === TILDE_CODE &&
		// Subscripts can only be one character long, otherwise they are a GFM strikethrough
		state.src.charCodeAt(state.i + 1) !== TILDE_CODE
	) {
		return testTagMarks(rule.name, "~", state, parent, rule.precedence!);
	}
	return false;
}
