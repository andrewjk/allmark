import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type MarkdownNode from "../types/MarkdownNode";
import testCriticMarks from "./criticMarksRUle";

const rule: InlineRule = {
	name: "deletion",
	test: testDeletion,
};
export default rule;

function testDeletion(state: InlineParserState, parent: MarkdownNode): boolean {
	return testCriticMarks(rule.name, "-", state, parent);
}
