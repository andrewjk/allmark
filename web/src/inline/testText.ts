import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type MarkdownNode from "../types/MarkdownNode";
import isNewLine from "../utils/isNewLine";
import newNode from "../utils/newNode";

const rule: InlineRule = {
	name: "text",
	test: testText,
};
export default rule;

/**
 * The text inline rule handles any character that hasn't been handled by a
 * previous rule
 */
function testText(state: InlineParserState, parent: MarkdownNode): boolean {
	let char = state.src[state.i];

	// TODO: Should this be in the testEscaped rule?
	// "Any ASCII punctuation character may be backslash-escaped"
	//if (char === "\\" && isPunctuation(state.src.charCodeAt(state.i + 1))) {
	//	state.i++;
	//	char = state.src[state.i];
	//}

	let lastNode = parent.children!.at(-1);
	let haveText = lastNode && lastNode.type === "text";
	let text = haveText ? lastNode! : newNode("text", false, state.i, state.line, 1, "", 0);

	// "Spaces at the end of the line and beginning of the next line are removed"
	if (haveText && isNewLine(char)) {
		text.markup = text.markup.trimEnd();
	}
	text.markup += char;

	if (!haveText) {
		parent.children!.push(text);
	}

	state.i++;

	return true;
}
