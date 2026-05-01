import parseBlock from "../parse/parseBlock";
import type BlockParserState from "../types/BlockParserState";
import type BlockRule from "../types/BlockRule";
import type MarkdownNode from "../types/MarkdownNode";
import { BRACKET_OPEN_CODE, BRACKET_CLOSE_CODE, CARET_CODE, COLON_CODE } from "../utils/charCodes";
import isEscaped from "../utils/isEscaped";
import isSpace from "../utils/isSpace";
import newBlock from "../utils/newBlock";
import { appendChild, getLastChild, hasChildren } from "../utils/nodeUtils";
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

	if (
		!state.isEscaped &&
		state.indent <= 3 &&
		state.src.charCodeAt(state.i) === BRACKET_OPEN_CODE
	) {
		// "A footnote definition cannot interrupt a paragraph"
		if (parent.type === "paragraph" && !parent.blankAfter) {
			return false;
		}

		let start = state.i;
		let footnoteStart = state.i + 1;

		// Check for ^ that indicates a footnote (not a regular link reference)
		if (state.src.charCodeAt(footnoteStart) !== CARET_CODE) {
			return false;
		}
		footnoteStart++;

		// Get the label
		let label = "";
		for (let i = footnoteStart; i < state.src.length; i++) {
			if (!isEscaped(state.src, i)) {
				let nextCharCode = state.src.charCodeAt(i);
				if (nextCharCode === BRACKET_CLOSE_CODE) {
					label = state.src.substring(footnoteStart, i);
					footnoteStart = i + 1;
					break;
				}

				// "Labels cannot contain brackets, unless they are
				// backslash-escaped"
				if (nextCharCode === BRACKET_OPEN_CODE) {
					return false;
				}
			}
		}
		// "A label must contain at least one non-whitespace character"
		if (!label || !/[^\s]/.test(label)) {
			return false;
		}

		if (state.src.charCodeAt(footnoteStart) !== COLON_CODE) {
			return false;
		}
		footnoteStart++;

		// Skip whitespace after colon
		while (footnoteStart < state.src.length && isSpace(state.src.charCodeAt(footnoteStart))) {
			footnoteStart++;
		}

		state.i = footnoteStart;

		// "Matching of labels is case-insensitive"
		label = normalizeLabel(label);

		// "If there are several matching definitions, the first one takes
		// precedence"
		if (state.footnotes[label]) {
			return true;
		}

		let ref = newBlock("footnote_ref", start, state.line, "", 0);
		ref.info = label;
		state.footnotes[label] = {
			label,
			content: ref,
		};

		if (state.hasBlankLine && hasChildren(parent)) {
			getLastChild(parent)!.blankAfter = true;
			state.hasBlankLine = false;
		}

		appendChild(parent, ref);
		state.openNodes.push(ref);

		state.hasBlankLine = false;
		parseBlock(state, ref);

		ref.length = state.i - ref.index;

		return true;
	}

	// Add another paragraph if there is an indent of at least 4 characters
	if (state.hasBlankLine && state.indent >= 4) {
		let lastChild = getLastChild(parent);
		if (lastChild !== undefined && lastChild.type === "footnote_ref") {
			state.indent = 0;
			parseBlock(state, lastChild);
			return true;
		}
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
			(state.src.charCodeAt(state.i) === BRACKET_OPEN_CODE &&
				state.src.charCodeAt(state.i + 1) !== CARET_CODE)
		) {
			// We won't know until we try more things
			state.maybeContinue = true;
			node.maybeContinuing = true;
			return true;
		}
	}

	return false;
}
