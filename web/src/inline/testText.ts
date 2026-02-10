import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type MarkdownNode from "../types/MarkdownNode";
import { isAlphaNumeric } from "../utils/isAlphaNumeric";
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
	if (lastNode === undefined || lastNode.type !== "text") {
		lastNode = newNode("text", false, state.i, state.line, 1, "", 0);
		parent.children!.push(lastNode);
	} else if (isNewLine(char)) {
		// "Spaces at the end of the line and beginning of the next line are removed"
		lastNode.markup = lastNode.markup.trimEnd();
	}

	if (isAlphaNumeric(state.src.charCodeAt(state.i))) {
		// If this an alphanumeric character, we can just process the whole
		// word, and save checking a bunch of characters that are never going to
		// match anything
		const start = state.i;
		state.i++;
		while (isAlphaNumeric(state.src.charCodeAt(state.i))) {
			state.i++;
		}
		lastNode.markup += state.src.substring(start, state.i);
	} else {
		state.i++;
		lastNode.markup += char;
	}

	return true;
}
