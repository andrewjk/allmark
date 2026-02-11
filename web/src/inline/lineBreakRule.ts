import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type MarkdownNode from "../types/MarkdownNode";
import isNewLine from "../utils/isNewLine";
import newNode from "../utils/newNode";

const rule: InlineRule = {
	name: "line_break",
	test: testLineBreak,
};
export default rule;

/**
 * "A line break (not in a code span or HTML tag) that is preceded by two or
 * more spaces and does not occur at the end of a block is parsed as a hard line
 * break (rendered in HTML as a <br /> tag)"
 */
function testLineBreak(state: InlineParserState, parent: MarkdownNode): boolean {
	let char = state.src[state.i];
	if (char === " ") {
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
			let html = newNode("html_span", false, state.i, state.line, 1, "", state.indent);
			html.content += `<br />\n`;
			parent.children!.push(html);
			state.i = end + 1;
			return true;
		}
	}

	return false;
}
