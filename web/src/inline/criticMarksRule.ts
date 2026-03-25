import type Delimiter from "../types/Delimiter";
import type InlineParserState from "../types/InlineParserState";
import type MarkdownNode from "../types/MarkdownNode";
import isEscaped from "../utils/isEscaped";
import newInline from "../utils/newInline";

export default function testCriticMarks(
	name: string,
	delimiter: string,
	state: InlineParserState,
	parent: MarkdownNode,
	closingDelimiter?: string,
): boolean {
	closingDelimiter ??= delimiter;

	let char = state.src[state.i];
	if (char === "{" && !isEscaped(state.src, state.i)) {
		let start = state.i;
		let end = state.i;

		// Get the markup
		let markup = char;
		for (let i = start + 1; i < state.src.length; i++) {
			if (state.src[i] === delimiter) {
				markup += delimiter;
				end++;
			} else if (
				state.src[i] === "}" ||
				(closingDelimiter !== delimiter && state.src[i] === closingDelimiter)
			) {
				return false;
			} else {
				break;
			}
		}

		if (markup.length === 2 || markup.length === 3) {
			// Add a new text node which may turn into a critic mark
			let text = newInline("text", state.parentIndex + start, state.line, markup, 0);
			parent.children!.push(text);

			// Add start delimiter
			state.i += markup.length;
			state.delimiters.push({ markup, start, length: markup.length });

			return true;
		}
	} else if (char === closingDelimiter && !isEscaped(state.src, state.i)) {
		// Get the markup
		let markup = "{" + delimiter;
		for (let i = state.i + 1; i < state.src.length; i++) {
			if (state.src[i] === closingDelimiter) {
				markup += delimiter;
			} else if (state.src[i] === "}") {
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
				let i = parent.children!.length;
				while (i--) {
					let lastNode = parent.children![i];
					if (lastNode.index === state.parentIndex + startDelimiter.start) {
						const newText = lastNode.markup.slice(startDelimiter.length);
						let text = newInline("text", lastNode.index, lastNode.line, newText, 0);

						lastNode.type = name;
						lastNode.markup = markup;
						lastNode.children = [text, ...parent.children!.splice(i + 1)];
						lastNode.length = state.parentIndex + state.i - lastNode.index + markup.length;

						state.i += markup.length;
						startDelimiter.handled = true;

						return true;
					}
				}

				// TODO: Precedence!
				// TODO: Should mark all delimiters between the tags as handled...
			}
		}
	}

	return false;
}
