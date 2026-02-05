import type Delimiter from "../types/Delimiter";
import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type MarkdownNode from "../types/MarkdownNode";
import isEscaped from "../utils/isEscaped";
import newNode from "../utils/newNode";

const rule: InlineRule = {
	name: "strikethrough",
	test: testStrikethrough,
};
export default rule;

/**
 * "Strikethrough text is any text wrapped in a matching pair of one or two
 * tildes (~).
 */
function testStrikethrough(state: InlineParserState, parent: MarkdownNode, _end: number): boolean {
	let char = state.src[state.i];
	if (char === "~" && !isEscaped(state.src, state.i)) {
		let start = state.i;
		let end = state.i;

		// TODO: Need a consumeUntil function
		// TODO: Just get the length instead of making a new string
		let markup = char;
		for (let i = state.i + 1; i < state.src.length; i++) {
			if (state.src[i] === char) {
				markup += char;
				end++;
			} else {
				break;
			}
		}

		// "Three or more tildes do not create a strikethrough"
		if (markup.length < 3) {
			// TODO: Precedence
			// Loop backwards through delimiters to find a matching one that does
			// not take precedence
			let lastDelimiter: Delimiter | undefined;
			let i = state.delimiters.length;
			while (i--) {
				let prevDelimiter = state.delimiters[i];
				if (!prevDelimiter.handled) {
					if (prevDelimiter.markup === char && prevDelimiter.length === markup.length) {
						lastDelimiter = prevDelimiter;
						break;
					} else if (prevDelimiter.markup === "*" || prevDelimiter.markup === "_") {
						continue;
					} else {
						break;
					}
				}
			}

			// Check if it's a closing strikethrough
			if (lastDelimiter) {
				// Convert the text node into a strikethrough node with a new
				// text child followed by the other children of the parent (if
				// any)
				let i = parent.children!.length;
				while (i--) {
					let lastNode = parent.children![i];
					if (lastNode.index === lastDelimiter.start) {
						let text = newNode("text", false, lastNode.index, lastNode.line, 1, char, 0);
						text.markup = lastNode.markup.slice(lastDelimiter.length);

						lastNode.type = "strikethrough";
						lastNode.markup = markup;
						lastNode.children = [text, ...parent.children!.splice(i + 1)];

						state.i += markup.length;
						lastDelimiter.handled = true;

						return true;
					}
				}
			}

			// Check if it's an opening strikethrough
			// Add a new text node which may turn into strikethrough
			let text = newNode("text", false, start, state.line, 1, markup, 0);
			parent.children!.push(text);

			state.i += markup.length;
			state.delimiters.push({ markup: char, start, length: markup.length });

			return true;
		}

		// TODO: If we use this in a lot of rules make it a helper
		let lastNode = parent.children!.at(-1);
		let haveText = lastNode && lastNode.type === "text";
		let text = haveText ? lastNode! : newNode("text", false, state.i, state.line, 1, "", 0);
		text.markup += markup;
		if (!haveText) {
			parent.children!.push(text);
		}

		state.i += markup.length;

		return true;
	}

	return false;
}
