import type Delimiter from "../types/Delimiter";
import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type MarkdownNode from "../types/MarkdownNode";
import isEscaped from "../utils/isEscaped";
import newNode from "../utils/newNode";

const rule: InlineRule = {
	name: "deletion",
	test: testDeletion,
};
export default rule;

function testDeletion(state: InlineParserState, parent: MarkdownNode, _end: number): boolean {
	let char = state.src[state.i];
	if (char === "{" && !isEscaped(state.src, state.i)) {
		let start = state.i;
		let end = state.i;

		// TODO: Need a consumeUntil function
		// TODO: Just get the length instead of making a new string
		let markup = char;
		for (let i = state.i + 1; i < state.src.length; i++) {
			if (state.src[i] === "-") {
				markup += "-";
				end++;
			} else if (state.src[i] === "}") {
				return false;
			} else {
				break;
			}
		}

		if (markup === "{-" || markup === "{--") {
			// Add a new text node which may turn into deletion
			let text = newNode("text", false, start, state.line, 1, markup, 0);
			parent.children!.push(text);

			// Add the start delimiter
			state.i += markup.length;
			state.delimiters.push({ markup, start, length: markup.length });

			return true;
		}
	} else if (char === "-" && !isEscaped(state.src, state.i)) {
		// TODO: Need a consumeUntil function
		// TODO: Just get the length instead of making a new string
		let markup = "{-";
		for (let i = state.i + 1; i < state.src.length; i++) {
			if (state.src[i] === "-") {
				markup += "-";
			} else if (state.src[i] === "}") {
				break;
			} else {
				return false;
			}
		}

		if (markup === "{-" || markup === "{--") {
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

			// Check if it's a closing deletion
			if (startDelimiter !== undefined) {
				// Convert the text node into a deletion node with a new text
				// child followed by the other children of the parent (if any)
				let i = parent.children!.length;
				while (i--) {
					let lastNode = parent.children![i];
					if (lastNode.index === startDelimiter.start) {
						const newText = lastNode.markup.slice(startDelimiter.length);
						let text = newNode("text", false, lastNode.index, lastNode.line, 1, newText, 0);

						lastNode.type = "deletion";
						lastNode.markup = markup;
						lastNode.children = [text, ...parent.children!.splice(i + 1)];

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
