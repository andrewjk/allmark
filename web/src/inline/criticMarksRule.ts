import type Delimiter from "../types/Delimiter";
import type InlineParserState from "../types/InlineParserState";
import type MarkdownNode from "../types/MarkdownNode";
import { BRACE_RIGHT_CODE } from "../utils/charCodes";
import newText from "../utils/newText";
import { appendChild, getLastDescendant, spliceTextNode } from "../utils/nodeUtils";

export default function testCriticMarks(
	name: string,
	delimiter: string,
	state: InlineParserState,
	parent: MarkdownNode,
	precedence: number,
	closingDelimiter?: string,
): boolean {
	closingDelimiter ??= delimiter;

	let char = state.src[state.i];
	if (!state.isEscaped) {
		if (char === "{") {
			let start = state.i;
			let end = state.i;

			// Get the markup
			let markup = char;
			for (let i = start + 1; i < state.src.length; i++) {
				let nextCharCode = state.src.charCodeAt(i);
				if (nextCharCode === delimiter.charCodeAt(0)) {
					markup += delimiter;
					end++;
				} else if (
					nextCharCode === BRACE_RIGHT_CODE ||
					(closingDelimiter !== delimiter && nextCharCode === closingDelimiter.charCodeAt(0))
				) {
					return false;
				} else {
					break;
				}
			}

			if (markup.length === 2 || markup.length === 3) {
				// Add a new text node which may turn into a critic mark
				let text = newText(state.parentIndex + start, state.line, markup, 0);
				appendChild(parent, text);

				// Add start delimiter
				state.i += markup.length;
				state.delimiters.push({ markup, start, length: markup.length, precedence });

				return true;
			}
		} else if (char === closingDelimiter) {
			// Get the markup
			let markup = "{" + delimiter;
			for (let i = state.i + 1; i < state.src.length; i++) {
				let nextCharCode = state.src.charCodeAt(i);
				if (nextCharCode === closingDelimiter.charCodeAt(0)) {
					markup += delimiter;
				} else if (nextCharCode === BRACE_RIGHT_CODE) {
					break;
				} else {
					return false;
				}
			}

			if (markup.length === 2 || markup.length === 3) {
				// Loop backwards through delimiters to find a matching one that
				// does not take precedence
				let startDelimiter: Delimiter | undefined;
				let i = state.delimiters.length;
				while (i--) {
					let prevDelimiter = state.delimiters[i];
					if (!prevDelimiter.handled && prevDelimiter.markup === markup) {
						startDelimiter = prevDelimiter;
						break;
					}
				}

				// Check if it's a closing critic mark
				if (startDelimiter !== undefined) {
					// Convert the text node into a critic mark node with a new text
					// child followed by the other children of the parent (if any)
					let lastDescendant = getLastDescendant(parent)!;
					let lastNode = lastDescendant;
					while (lastNode !== parent) {
						if (
							lastNode.depth === parent.depth + 1 &&
							lastNode.index === state.parentIndex + startDelimiter.start
						) {
							const content = lastNode.content.slice(startDelimiter.length);
							let text = newText(lastNode.index, lastNode.line, content, 0);

							// Convert the previous node to the tag type
							lastNode.type = name;
							lastNode.markup = markup;
							lastNode.length = state.parentIndex + state.i - lastNode.index + markup.length;

							// Move the parent's subsequent children under the new link node
							spliceTextNode(parent, text, lastNode, lastDescendant);

							state.i += markup.length;
							startDelimiter.handled = true;

							return true;
						}

						lastNode = lastNode.previousNode!;
					}
				}
			}
		}
	}

	return false;
}
