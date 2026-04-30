import type Delimiter from "../types/Delimiter";
import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type LinkReference from "../types/LinkReference";
import type MarkdownNode from "../types/MarkdownNode";
import {
	BACKSLASH_CODE,
	BRACKET_OPEN_CODE,
	BRACKET_CLOSE_CODE,
	PAREN_OPEN_CODE,
	EXCLAMATION_CODE,
} from "../utils/charCodes";
import newText from "../utils/newText";
import normalizeLabel from "../utils/normalizeLabel";
import parseLinkInline from "../utils/parseLinkInline";

const rule: InlineRule = {
	name: "link",
	test: testLink,
	precedence: 15,
};
export default rule;

function testLink(state: InlineParserState, parent: MarkdownNode): boolean {
	if (!state.isEscaped) {
		let charCode = state.src.charCodeAt(state.i);

		if (charCode === BRACKET_OPEN_CODE) {
			return testLinkOpen(state, parent);
		}

		if (charCode === EXCLAMATION_CODE && state.src.charCodeAt(state.i + 1) === BRACKET_OPEN_CODE) {
			return testImageOpen(state, parent);
		}

		if (charCode === BRACKET_CLOSE_CODE) {
			return testLinkClose(state, parent);
		}
	}

	return false;
}

function testLinkOpen(state: InlineParserState, parent: MarkdownNode) {
	let start = state.i;
	let markup = "[";

	// Add a new text node which may turn into a link
	let text = newText(state.parentIndex + start, state.line, markup, 0);
	parent.children!.push(text);

	state.i++;
	state.delimiters.push({ markup, start, length: 1, precedence: rule.precedence });

	return true;
}

function testImageOpen(state: InlineParserState, parent: MarkdownNode) {
	let start = state.i;
	let markup = "![";

	// Add a new text node which may turn into an image
	let text = newText(state.parentIndex + start, state.line, markup, 0);
	parent.children!.push(text);

	state.i += markup.length;
	state.delimiters.push({ markup, start, length: 1, precedence: rule.precedence });

	return true;
}

function testLinkClose(state: InlineParserState, parent: MarkdownNode) {
	// Loop backwards through delimiters to find a matching one that does
	// not take precedence
	let startDelimiter: Delimiter | undefined;
	let i = state.delimiters.length;
	while (i--) {
		let prevDelimiter = state.delimiters[i];
		if (!prevDelimiter.handled) {
			if (prevDelimiter.markup === "[" || prevDelimiter.markup === "![") {
				startDelimiter = prevDelimiter;
				break;
			} else if ((prevDelimiter.precedence ?? 0) <= rule.precedence!) {
				// Same or lower precedence delimiters can be skipped over
				continue;
			} else {
				// Higher precedence delimiters block
				break;
			}
		}
	}

	if (startDelimiter !== undefined) {
		// Convert the text node into a link node with a new text child
		// followed by the other children of the parent (if any)
		let i = parent.children!.length;
		while (i--) {
			let lastNode = parent.children![i];
			if (lastNode.index === state.parentIndex + startDelimiter.start) {
				let start = state.i + 1;
				let label = state.src.substring(
					startDelimiter.start + startDelimiter.markup.length,
					state.i,
				);

				// "The link text may contain balanced brackets, but not
				// unbalanced ones, unless they are escaped"
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

				let isLink = startDelimiter.markup === "[";

				let hasInfo = state.src.charCodeAt(state.i + 1) === PAREN_OPEN_CODE;
				let hasRef = state.src.charCodeAt(state.i + 1) === BRACKET_OPEN_CODE;

				// "Full and compact references take precedence over shortcut references"
				// "Inline links also take precedence"
				let link: LinkReference | undefined;
				if (hasInfo) {
					start++;
					link = parseLinkInline(state, start, ")");
					if (link !== undefined) {
						//state.i = ??
					}
				} else if (hasRef) {
					start++;
					for (let i = start; i < state.src.length; i++) {
						if (state.src.charCodeAt(i) === BRACKET_CLOSE_CODE) {
							// Lookup using the text between [], or if there
							// is no text, use the label
							label = i - start > 0 ? state.src.substring(start, i) : label;
							label = normalizeLabel(label);
							link = state.refs[label];
							if (link !== undefined) {
								state.i = i + 1;
							}
							break;
						}
					}
				}

				if (link === undefined) {
					label = normalizeLabel(label);
					link = state.refs[label];
					if (link !== undefined) {
						state.i++;
					}
				}

				if (link !== undefined) {
					let content = lastNode.content.slice(startDelimiter.markup.length);
					let text = newText(lastNode.index, lastNode.line, content, 0);
					text.length = text.content.length;

					lastNode.type = isLink ? "link" : "image";
					lastNode.info = link.url;
					lastNode.title = link.title;
					lastNode.children = [text, ...parent.children!.splice(i + 1)];
					lastNode.length = state.parentIndex + state.i - lastNode.index;

					// "[L]inks may not contain other links, at any level of nesting"
					if (isLink) {
						// Remove all the opening delimiters so they won't be picked up in future
						let d = state.delimiters.length;
						while (d--) {
							let prevDelimiter = state.delimiters[d];
							if (prevDelimiter.markup === "[" || prevDelimiter.markup === "]") {
								prevDelimiter.handled = true;
							}
						}
					}

					startDelimiter.handled = true;
					return true;
				}

				// TODO: If it's not a link, go back and close delimiters that
				// weren't closed between start and end

				startDelimiter.handled = true;
				break;
			}
		}
	}

	return false;
}
