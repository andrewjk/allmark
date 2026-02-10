import type Delimiter from "../types/Delimiter";
import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type LinkReference from "../types/LinkReference";
import type MarkdownNode from "../types/MarkdownNode";
import isEscaped from "../utils/isEscaped";
import newNode from "../utils/newNode";
import normalizeLabel from "../utils/normalizeLabel";
import parseLinkInline from "../utils/parseLinkInline";

const rule: InlineRule = {
	name: "link",
	test: testLink,
};
export default rule;

function testLink(state: InlineParserState, parent: MarkdownNode): boolean {
	let char = state.src[state.i];

	if (!isEscaped(state.src, state.i)) {
		if (char === "[") {
			return testLinkOpen(state, parent);
		}

		if (char === "!" && state.src[state.i + 1] === "[") {
			return testImageOpen(state, parent);
		}

		if (char === "]") {
			return testLinkClose(state, parent);
		}
	}

	return false;
}

function testLinkOpen(state: InlineParserState, parent: MarkdownNode) {
	let start = state.i;
	let markup = "[";

	// Add a new text node which may turn into a link
	let text = newNode("text", false, start, state.line, 1, markup, 0);
	parent.children!.push(text);

	state.i++;
	state.delimiters.push({ markup, start, length: 1 });

	return true;
}

function testImageOpen(state: InlineParserState, parent: MarkdownNode) {
	let start = state.i;
	let markup = "![";

	// Add a new text node which may turn into an image
	let text = newNode("text", false, start, state.line, 1, markup, 0);
	parent.children!.push(text);

	state.i += markup.length;
	state.delimiters.push({ markup, start, length: 1 });

	return true;
}

function testLinkClose(state: InlineParserState, parent: MarkdownNode) {
	let markup = "]";

	// TODO: Standardize precedence
	let startDelimiter: Delimiter | undefined;
	let i = state.delimiters.length;
	while (i--) {
		let prevDelimiter = state.delimiters[i];
		if (!prevDelimiter.handled) {
			if (prevDelimiter.markup === "[" || prevDelimiter.markup === "![") {
				startDelimiter = prevDelimiter;
				break;
			} else if (prevDelimiter.markup === "*" || prevDelimiter.markup === "_") {
				continue;
			} else {
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
			if (lastNode.index === startDelimiter.start) {
				let start = state.i + 1;
				let label = state.src.substring(
					startDelimiter.start + startDelimiter.markup.length,
					state.i,
				);

				// "The link text may contain balanced brackets, but not
				// unbalanced ones, unless they are escaped"
				let level = 0;
				for (let i = 0; i < label.length; i++) {
					if (label[i] === "\\") {
						i++;
					} else if (label[i] === "[") {
						level++;
					} else if (label[i] === "]") {
						level--;
					}
				}
				if (level != 0) {
					return false;
				}

				let isLink = startDelimiter.markup === "[";

				let hasInfo = state.src[state.i + 1] === "(";
				let hasRef = state.src[state.i + 1] === "[";

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
						if (state.src[i] === "]") {
							// Lookup using the text between the [], or if there
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
					let text = newNode("text", false, lastNode.index, lastNode.line, 1, markup, 0);
					text.markup = lastNode.markup.slice(startDelimiter.markup.length);

					lastNode.type = isLink ? "link" : "image";
					lastNode.info = link.url;
					lastNode.title = link.title;
					lastNode.children = [text, ...parent.children!.splice(i + 1)];

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
				// weren't closed between the start and end

				startDelimiter.handled = true;
				break;
			}
		}
	}

	return false;
}
