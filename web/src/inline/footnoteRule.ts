import parseInline from "../parse/parseInline";
import type Delimiter from "../types/Delimiter";
import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type MarkdownNode from "../types/MarkdownNode";
import {
	BACKSLASH_CODE,
	BRACKET_OPEN_CODE,
	BRACKET_CLOSE_CODE,
	CARET_CODE,
} from "../utils/charCodes";
import newText from "../utils/newText";
import normalizeLabel from "../utils/normalizeLabel";

const rule: InlineRule = {
	name: "footnote",
	test: testFootnote,
};
export default rule;

function testFootnote(state: InlineParserState, parent: MarkdownNode): boolean {
	if (!state.isEscaped) {
		let charCode = state.src.charCodeAt(state.i);

		if (charCode === BRACKET_OPEN_CODE) {
			return testFootnoteOpen(state, parent);
		}

		if (charCode === BRACKET_CLOSE_CODE) {
			return testFootnoteClose(state, parent);
		}
	}

	return false;
}

function testFootnoteOpen(state: InlineParserState, parent: MarkdownNode) {
	let start = state.i;

	// Check for [^ pattern which indicates a footnote reference
	if (state.src.charCodeAt(start + 1) !== CARET_CODE) {
		return false;
	}

	let markup = "[^";

	// Add a new text node which may turn into a footnote
	let text = newText(state.parentIndex + start, state.line, markup, 0);
	parent.children!.push(text);

	state.i += 2;
	state.delimiters.push({ markup, start, length: 2 });

	return true;
}

function testFootnoteClose(state: InlineParserState, parent: MarkdownNode) {
	// Find the matching footnote delimiter
	let startDelimiter: Delimiter | undefined;
	let i = state.delimiters.length;
	while (i--) {
		let prevDelimiter = state.delimiters[i];
		if (!prevDelimiter.handled) {
			if (prevDelimiter.markup === "[^") {
				startDelimiter = prevDelimiter;
				break;
			}
		}
	}

	if (startDelimiter !== undefined) {
		// Convert the text node into a footnote node
		let i = parent.children!.length;
		while (i--) {
			let lastNode = parent.children![i];
			if (lastNode.index === state.parentIndex + startDelimiter.start) {
				let label = state.src.substring(
					startDelimiter.start + startDelimiter.markup.length,
					state.i,
				);

				// No special characters
				if (/[^a-zA-Z0-9]/.test(label)) {
					return false;
				}

				// Check for balanced brackets
				let level = 0;
				for (let i = 0; i < label.length; i++) {
					if (label.charCodeAt(i) === BACKSLASH_CODE) {
						i++;
					} else if (label.charCodeAt(i) === BRACKET_OPEN_CODE) {
						level++;
					} else if (label.charCodeAt(i) === BRACKET_CLOSE_CODE) {
						level--;
					}
				}
				if (level != 0) {
					return false;
				}

				// Swallow anything in brackets afterwards
				// Unless it's a link reference, in which case it should be treated as a link instead
				if (state.src.charCodeAt(state.i + 1) === BRACKET_OPEN_CODE) {
					const start = state.i + 2;
					for (let i = start; i < state.src.length; i++) {
						if (state.src.charCodeAt(i) === BRACKET_CLOSE_CODE) {
							let linkRef = state.src.substring(start, i);
							linkRef = normalizeLabel(linkRef);
							if (state.refs[linkRef] !== undefined) {
								startDelimiter.markup = "[";
								return false;
							}
							state.i = i;
							break;
						}
					}
				}

				// Normalize the label and look it up
				label = normalizeLabel(label);
				let footnote = state.footnotes[label];

				if (footnote !== undefined) {
					state.i++;
					startDelimiter.handled = true;

					// Create a footnote reference node with parsed children
					lastNode.type = "footnote";
					lastNode.info = label;
					lastNode.markup = `[^${label}]`;
					lastNode.children = footnote.content.children;
					lastNode.length = state.parentIndex + state.i - lastNode.index;

					// Parse the footnote content for inline elements
					let tempState: InlineParserState = {
						rules: state.rules,
						src: footnote.content.content,
						i: 0,
						line: lastNode.line,
						lineStart: 0,
						indent: 0,
						isEscaped: false,
						delimiters: [],
						refs: state.refs,
						footnotes: state.footnotes,
						parentIndex: lastNode.index,
					};
					parseInline(tempState, lastNode);

					return true;
				}

				startDelimiter.handled = true;
				break;
			}
		}
	}

	return false;
}
