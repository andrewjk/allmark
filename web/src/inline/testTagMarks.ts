import type Delimiter from "../types/Delimiter";
import type InlineParserState from "../types/InlineParserState";
import type MarkdownNode from "../types/MarkdownNode";
import addMarkupAsText from "../utils/addMarkupAsText";
import isUnicodePunctuation from "../utils/isUnicodePunctuation";
import isUnicodeSpace from "../utils/isUnicodeSpace";
import newNode from "../utils/newNode";

export default function testTagMarks(
	name: string,
	char: string,
	state: InlineParserState,
	parent: MarkdownNode,
): boolean {
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
		// TODO: Better space checks including start/end of line
		let codeBefore = state.src.charCodeAt(start - 1);
		let spaceBefore = start === 0 || isUnicodeSpace(codeBefore);
		let punctuationBefore = !spaceBefore && isUnicodePunctuation(codeBefore);

		let codeAfter = state.src.charCodeAt(end + 1);
		let spaceAfter = end === state.src.length - 1 || isUnicodeSpace(codeAfter);
		let punctuationAfter = !spaceAfter && isUnicodePunctuation(codeAfter);

		// "A left-flanking delimiter run is a delimiter run that is (1) not
		// followed by Unicode whitespace, and either (2a) not followed by a
		// punctuation character, or (2b) followed by a punctuation character
		// and preceded by Unicode whitespace or a punctuation character. For
		// purposes of this definition, the beginning and the end of the line
		// count as Unicode whitespace."
		let leftFlanking =
			!spaceAfter &&
			(!punctuationAfter || (punctuationAfter && (spaceBefore || punctuationBefore)));

		// "A right-flanking delimiter run is a delimiter run that is (1) not
		// preceded by Unicode whitespace, and either (2a) not preceded by a
		// punctuation character, or (2b) preceded by a punctuation character
		// and followed by Unicode whitespace or a punctuation character. For
		// purposes of this definition, the beginning and the end of the line
		// count as Unicode whitespace"
		let rightFlanking =
			!spaceBefore &&
			(!punctuationBefore || (punctuationBefore && (spaceAfter || punctuationAfter)));

		if (rightFlanking) {
			// TODO: Precedence
			// Loop backwards through delimiters to find a matching one that does
			// not take precedence
			let startDelimiter: Delimiter | undefined;
			let i = state.delimiters.length;
			while (i--) {
				let prevDelimiter = state.delimiters[i];
				if (!prevDelimiter.handled) {
					if (prevDelimiter.markup === char && prevDelimiter.length === markup.length) {
						startDelimiter = prevDelimiter;
						break;
					} else if (prevDelimiter.markup === "*" || prevDelimiter.markup === "_") {
						continue;
					} else {
						break;
					}
				}
			}

			// Check if it's a closing delimiter
			if (startDelimiter !== undefined) {
				// Convert the text node into a delimited node with a new text
				// child followed by the other children of the parent (if any)
				let i = parent.children!.length;
				while (i--) {
					let lastNode = parent.children![i];
					if (lastNode.index === startDelimiter.start) {
						let text = newNode("text", false, lastNode.index, lastNode.line, 1, char, 0);
						text.markup = lastNode.markup.slice(startDelimiter.length);

						lastNode.type = name;
						lastNode.markup = markup;
						lastNode.children = [text, ...parent.children!.splice(i + 1)];

						state.i += markup.length;
						startDelimiter.handled = true;

						return true;
					}
				}
			}
		}

		if (leftFlanking) {
			// Add a new text node which may turn into a delimiter
			let text = newNode("text", false, start, state.line, 1, markup, 0);
			parent.children!.push(text);

			state.i += markup.length;
			state.delimiters.push({ markup: char, start, length: markup.length });

			return true;
		}
	}

	addMarkupAsText(markup, state, parent);

	return true;
}
