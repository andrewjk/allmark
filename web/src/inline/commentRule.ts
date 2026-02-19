import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type MarkdownNode from "../types/MarkdownNode";
import testCriticMarks from "./criticMarksRule";

const rule: InlineRule = {
	name: "comment",
	test: testComment,
};
export default rule;

function testComment(state: InlineParserState, parent: MarkdownNode): boolean {
	return testCriticMarks(rule.name, ">", state, parent, "<");
}
