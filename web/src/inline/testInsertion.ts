import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type MarkdownNode from "../types/MarkdownNode";
import testCriticMarks from "./testCriticMarks";

const rule: InlineRule = {
	name: "insertion",
	test: testInsertion,
};
export default rule;

function testInsertion(state: InlineParserState, parent: MarkdownNode): boolean {
	return testCriticMarks(rule.name, "+", state, parent);
}
