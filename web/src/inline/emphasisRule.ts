import type Delimiter from "../types/Delimiter";
import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type MarkdownNode from "../types/MarkdownNode";
import addMarkupAsText from "../utils/addMarkupAsText";
import { ASTERISK_CODE, UNDERSCORE_CODE } from "../utils/charCodes";
import isUnicodePunctuation from "../utils/isUnicodePunctuation";
import isUnicodeSpace from "../utils/isUnicodeSpace";
import newInline from "../utils/newInline";
import newText from "../utils/newText";
import { appendChild, getLastDescendant, spliceTextNode } from "../utils/nodeUtils";

const rule: InlineRule = {
	name: "emphasis",
	test: testEmphasis,
	precedence: 10,
};
export default rule;

function testEmphasis(state: InlineParserState, parent: MarkdownNode): boolean {
	let charCode = state.src.charCodeAt(state.i);
	if (!state.isEscaped && (charCode === ASTERISK_CODE || charCode === UNDERSCORE_CODE)) {
		let char = state.src[state.i];
		let start = state.i;
		let end = state.i;

		// Get the markup
		let markup = char;
		for (let i = start + 1; i < state.src.length; i++) {
			if (state.src.charCodeAt(i) === charCode) {
				markup += char;
				end++;
			} else {
				break;
			}
		}
		//let markup = state.src.substring(start, end);

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
				} else if ((prevDelimiter.precedence ?? 0) <= rule.precedence!) {
					// Same or lower precedence delimiters can be skipped over
					continue;
				} else {
					// Higher precedence delimiters block
					break;
				}
			}
		}

		// Check if it's a closing emphasis
		if (startDelimiter !== undefined) {
			let canClose =
				(rightFlanking ||
					// Check if it's a continuing part of a three-run delimiter
					state.src.charCodeAt(state.i - 1) === charCode) &&
				startDelimiter.markup === char &&
				// "Emphasis with _ is not allowed inside words"
				(charCode !== UNDERSCORE_CODE || spaceAfter || punctuationAfter) &&
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
				let lastDescendant = getLastDescendant(parent)!;
				let lastNode = lastDescendant;
				while (lastNode !== parent) {
					if (
						lastNode.depth === parent.depth + 1 &&
						lastNode.index === state.parentIndex + startDelimiter.start
					) {
						// If it's longer than the last delimiter, or longer
						// than two, save some for the next go-round
						markup = markup.substring(0, Math.min(startDelimiter.length, 2));

						let content = lastNode.content.slice(startDelimiter.length);
						let text = newText(lastNode.index, lastNode.line, content, 0);
						text.length = text.content.length;

						if (markup.length < startDelimiter.length) {
							let remainingStart = lastNode.index + startDelimiter.length - markup.length;
							lastNode.content = lastNode.content.substring(
								0,
								startDelimiter.length - markup.length,
							);
							lastNode.length = lastNode.content.length;
							let emphasis = newInline(
								markup.length === 2 ? "strong" : "emphasis",
								remainingStart,
								lastNode.line,
								markup,
								0,
							);
							emphasis.length = state.parentIndex + state.i - remainingStart + markup.length;

							text.previousNode = emphasis;
							text.nextNode = lastNode.nextNode;
							emphasis.previousNode = lastNode;
							emphasis.nextNode = text;
							lastNode.nextNode = emphasis;

							let nextNode = lastNode;
							while (nextNode !== lastDescendant) {
								nextNode.depth++;
								nextNode = nextNode.nextNode!;
							}
							lastDescendant.depth++;

							lastNode.depth = parent.depth + 1;
							emphasis.depth = parent.depth + 1;
							text.depth = emphasis.depth + 1;
						} else {
							lastNode.type = markup.length === 2 ? "strong" : "emphasis";
							lastNode.markup = markup;
							spliceTextNode(parent, text, lastNode, lastDescendant);
							lastNode.length = state.parentIndex + state.i - lastNode.index + markup.length;
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

					lastNode = lastNode.previousNode!;
				}
			}
		}

		// Check if it's an opening emphasis
		let canOpen =
			leftFlanking &&
			// "Emphasis with _ is not allowed inside words"
			(charCode !== UNDERSCORE_CODE || spaceBefore || punctuationBefore);
		if (canOpen) {
			// Add a new text node which may turn into emphasis
			let text = newText(state.parentIndex + start, state.line, markup, 0);
			appendChild(parent, text);

			state.i += markup.length;
			state.delimiters.push({
				markup: char,
				start,
				length: markup.length,
				precedence: rule.precedence,
			});

			return true;
		}

		addMarkupAsText(markup, state, parent);

		return true;
	}

	return false;
}
