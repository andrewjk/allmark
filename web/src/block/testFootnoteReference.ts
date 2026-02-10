import parseBlock from "../parse/parseBlock";
import type BlockParserState from "../types/BlockParserState";
import type BlockRule from "../types/BlockRule";
import type MarkdownNode from "../types/MarkdownNode";
import isEscaped from "../utils/isEscaped";
import newNode from "../utils/newNode";
import normalizeLabel from "../utils/normalizeLabel";

const rule: BlockRule = {
	name: "footnote_ref",
	testStart,
	testContinue,
};
export default rule;

/**
 * A footnote definition has a label that starts with ^, followed by a colon
 * and the footnote content.
 *
 * [^1]: This is the footnote content.
 * [^label]: Footnote content can span
 *   multiple lines with proper indentation.
 */

function testStart(state: BlockParserState, parent: MarkdownNode) {
	if (parent.acceptsContent) {
		return false;
	}

	let char = state.src[state.i];
	if (state.indent <= 3 && char === "[" && !isEscaped(state.src, state.i)) {
		// "A footnote definition cannot interrupt a paragraph"
		if (parent.type === "paragraph" && !parent.blankAfter) {
			return false;
		}

		let start = state.i + 1;

		// Check for ^ that indicates a footnote (not a regular link reference)
		if (state.src[start] !== "^") {
			return false;
		}
		start++;

		// Get the label
		let label = "";
		for (let i = start; i < state.src.length; i++) {
			if (!isEscaped(state.src, i)) {
				if (state.src[i] === "]") {
					label = state.src.substring(start, i);
					start = i + 1;
					break;
				}

				// "Labels cannot contain brackets, unless they are
				// backslash-escaped"
				if (state.src[i] === "[") {
					return false;
				}
			}
		}
		// "A label must contain at least one non-whitespace character"
		if (!label || !/[^\s]/.test(label)) {
			return false;
		}

		if (state.src[start] !== ":") {
			return false;
		}
		start++;

		// Skip whitespace after colon
		while (start < state.src.length && isSpace(state.src.charCodeAt(start))) {
			start++;
		}

		state.i = start;

		// "Matching of labels is case-insensitive"
		label = normalizeLabel(label);

		// "If there are several matching definitions, the first one takes
		// precedence"
		if (state.footnotes[label]) {
			return true;
		}

		let ref = newNode("footnote_ref", true, state.i, state.line, 1, "", 0, []);
		state.footnotes[label] = {
			label,
			content: ref,
		};

		if (state.hasBlankLine && parent.children !== undefined && parent.children.length > 0) {
			parent.children.at(-1)!.blankAfter = true;
			state.hasBlankLine = false;
		}

		parent.children!.push(ref);
		state.openNodes.push(ref);

		state.hasBlankLine = false;
		parseBlock(state, ref);

		return true;
	}

	return false;
}

function testContinue(state: BlockParserState, node: MarkdownNode) {
	if (state.hasBlankLine) {
		return false;
	}

	let openNode = state.openNodes.at(-1)!;
	if (openNode.type === "paragraph") {
		if (
			state.indent >= 4 ||
			openNode.content.endsWith("  \n") ||
			// GitHub swallows link references after footnote references
			(state.src[state.i] === "[" && state.src[state.i + 1] !== "^")
		) {
			// We won't know until we try more things
			state.maybeContinue = true;
			node.maybeContinuing = true;
			return true;
		}
	}

	return false;
}

function isSpace(charCode: number): boolean {
	return charCode === 0x20 || charCode === 0x09;
}
