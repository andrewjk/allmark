import type Delimiter from "../types/Delimiter";
import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type MarkdownNode from "../types/MarkdownNode";
import addMarkupAsText from "../utils/addMarkupAsText";
import isEscaped from "../utils/isEscaped";
import isUnicodePunctuation from "../utils/isUnicodePunctuation";
import isUnicodeSpace from "../utils/isUnicodeSpace";
import newNode from "../utils/newNode";

const rule: InlineRule = {
	name: "emphasis",
	test: testEmphasis,
};
export default rule;

function testEmphasis(state: InlineParserState, parent: MarkdownNode): boolean {
	let char = state.src[state.i];
	if ((char === "*" || char === "_") && !isEscaped(state.src, state.i)) {
		let start = state.i;
		let end = state.i;

		// TODO: Need a consumeUntil function
		let markup = char;
		for (let i = state.i + 1; i < state.src.length; i++) {
			if (state.src[i] === char) {
				markup += char;
				end++;
			} else {
				break;
			}
		}

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

		// TODO: Precedence
		// Loop backwards through delimiters to find a matching one that does
		// not take precedence, and ideally has the same length
		let startDelimiter: Delimiter | undefined;
		let startIndex = -1;
		let i = state.delimiters.length;
		while (i--) {
			let prevDelimiter = state.delimiters[i];
			if (!prevDelimiter.handled) {
				if (prevDelimiter.markup === char) {
					if (prevDelimiter.length === markup.length) {
						startDelimiter = prevDelimiter;
						startIndex = i;
						break;
					} else if (!startDelimiter) {
						startDelimiter = prevDelimiter;
						startIndex = i;
					}
				} else if (prevDelimiter.markup === "*" || prevDelimiter.markup === "_") {
					continue;
				} else {
					break;
				}
			}
		}

		// Check if it's a closing emphasis
		if (startDelimiter !== undefined) {
			let canClose =
				(rightFlanking ||
					// Check if it's a continuing part of a three-run delimiter
					state.src[state.i - 1] === char) &&
				startDelimiter.markup === char &&
				// "Emphasis with _ is not allowed inside words"
				(char !== "_" || spaceAfter || punctuationAfter) &&
				// "[A] delimiter that can both open and close ... cannot form
				// emphasis if the sum of the lengths of the delimiter runs
				// containing the opening and closing delimiters is a multiple
				// of 3 unless both lengths are multiples of 3."
				(!leftFlanking ||
					(markup.length + startDelimiter.length) % 3 !== 0 ||
					(markup.length % 3 === 0 && startDelimiter.length % 3 === 0));
			if (canClose) {
				// Convert the text node into an emphasis node with a new text child
				// followed by the other children of the parent (if any)
				let i = parent.children!.length;
				while (i--) {
					let lastNode = parent.children![i];
					if (lastNode.index === startDelimiter.start) {
						// If it's longer than the last delimiter, or longer
						// than two, save some for the next go-round
						markup = markup.substring(0, Math.min(startDelimiter.length, 2));

						let text = newNode("text", false, lastNode.index, lastNode.line, 1, char, 0);
						text.markup = lastNode.markup.slice(startDelimiter.length);

						let movedNodes = parent.children!.splice(i + 1);

						if (markup.length < startDelimiter.length) {
							lastNode.markup = lastNode.markup.substring(0, startDelimiter.length - markup.length);
							let emphasis = newNode(
								markup.length === 2 ? "strong" : "emphasis",
								false,
								lastNode.index + markup.length,
								lastNode.line,
								1,
								markup,
								0,
								[text, ...movedNodes],
							);
							parent.children!.push(emphasis);
						} else {
							lastNode.type = markup.length === 2 ? "strong" : "emphasis";
							lastNode.markup = markup;
							lastNode.children = [text, ...movedNodes];
						}

						state.i += markup.length;

						// Mark delimiters between the start and end as handled,
						// as they can't start anything anymore
						let d = state.delimiters.length;
						while (d--) {
							if (d === startIndex) {
								break;
							}
							let prevDelimiter = state.delimiters[d];
							prevDelimiter.handled = true;
						}

						// Mark the start delimiter handled if all its chars are used up
						startDelimiter.length -= markup.length;
						if (!startDelimiter.length) {
							startDelimiter.handled = true;
						}

						return true;
					}
				}
			}
		}

		// Check if it's an opening emphasis
		let canOpen =
			leftFlanking &&
			// "Emphasis with _ is not allowed inside words"
			(char !== "_" || spaceBefore || punctuationBefore);
		if (canOpen) {
			// Add a new text node which may turn into emphasis
			let text = newNode("text", false, start, state.line, 1, markup, 0);
			parent.children!.push(text);

			state.i += markup.length;
			state.delimiters.push({ markup: char, start, length: markup.length });

			return true;
		}

		addMarkupAsText(markup, state, parent);

		return true;
	}

	return false;
}
