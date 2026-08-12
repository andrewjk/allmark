import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type MarkdownNode from "../types/MarkdownNode";
import { isAlphaNumeric } from "../utils/isAlphaNumeric";
import isNewLine from "../utils/isNewLine";
import isSpace from "../utils/isSpace";
import newText from "../utils/newText";

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
	let charCode = state.src.charCodeAt(state.i);

	// TODO: Should this be in the testEscaped rule?
	// "Any ASCII punctuation character may be backslash-escaped"
	//if (char === "\\" && isPunctuation(state.src.charCodeAt(state.i + 1))) {
	//	state.i++;
	//	char = state.src[state.i];
	//}

	let lastNode = parent.children!.at(-1);
	if (lastNode === undefined || lastNode.type !== "text") {
		lastNode = newText(state.parentIndex + state.i, state.line, "", 0);
		parent.children!.push(lastNode);
	} else if (isNewLine(charCode)) {
		// "Spaces at the end of the line and beginning of the next line are removed"
		lastNode.content = lastNode.content.trimEnd();
		if (isSpace(state.src.charCodeAt(state.i + 1))) {
			lastNode.content += char;
			lastNode.length = lastNode.content.length;
			state.i += 2;
			while (isSpace(state.src.charCodeAt(state.i))) {
				state.i++;
			}
			lastNode = newText(state.parentIndex + state.i, state.line, "", 0);
			parent.children!.push(lastNode);
			return true;
		}
	}

	if (isAlphaNumeric(charCode)) {
		// If this an alphanumeric character, we can just process whole
		// word, and save checking a bunch of characters that are never going to
		// match anything
		const start = state.i;
		state.i++;
		while (isAlphaNumeric(state.src.charCodeAt(state.i))) {
			state.i++;
		}
		lastNode.content += state.src.substring(start, state.i);
	} else {
		state.i++;
		lastNode.content += char;
	}

	lastNode.length = lastNode.content.length;

	return true;
}
