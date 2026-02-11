import type BlockParserState from "../types/BlockParserState";
import type BlockRule from "../types/BlockRule";
import type MarkdownNode from "../types/MarkdownNode";
import getEndOfLine from "../utils/getEndOfLine";
import isEscaped from "../utils/isEscaped";
import isNewLine from "../utils/isNewLine";
import newNode from "../utils/newNode";
import normalizeLabel from "../utils/normalizeLabel";
import parseLinkBlock from "../utils/parseLinkBlock";

const rule: BlockRule = {
	name: "link_ref",
	testStart,
	testContinue,
};
export default rule;

/**
 * "A link reference definition consists of a link label, indented up to three
 * spaces, followed by a colon (:), optional whitespace (including up to one
 * line ending), a link destination, optional whitespace (including up to one
 * line ending), and an optional link title, which if it is present must be
 * separated from the link destination by whitespace. No further non-whitespace
 * characters may occur on the line.
 *
 * A link reference definition does not correspond to a structural element of a
 * document. Instead, it defines a label which can be used in reference links
 * and reference-style images elsewhere in the document. Link reference
 * definitions can come either before or after the links that use them."
 */

function testStart(state: BlockParserState, parent: MarkdownNode) {
	if (parent.acceptsContent) {
		return false;
	}

	let char = state.src[state.i];
	if (state.indent <= 3 && char === "[" && !isEscaped(state.src, state.i)) {
		// "A link reference definition cannot interrupt a paragraph"
		if (parent.type === "paragraph" && !parent.blankAfter) {
			return false;
		}

		let start = state.i + 1;

		// Get the label
		let label = "";
		for (let i = start; i < state.src.length; i++) {
			if (!isEscaped(state.src, i)) {
				if (state.src[i] === "]") {
					label = state.src.substring(start, i);
					start = i + 1;
					break;
				}

				// "Link labels cannot contain brackets, unless they are
				// backslash-escaped"
				if (state.src[i] === "[") {
					return false;
				}
			}
		}
		// "A link label must contain at least one non-whitespace character"
		if (!label || !/[^\s]/.test(label)) {
			return false;
		}

		if (state.src[start] !== ":") {
			return false;
		}

		start++;

		let linkInfo = parseLinkBlock(state, start, "\n");
		if (linkInfo === undefined) {
			return false;
		}

		// "As noted in the section on Links, matching of labels is
		// case-insensitive (see matches)"
		label = normalizeLabel(label);

		// "If there are several matching definitions, the first one takes
		// precedence"
		if (state.refs[label]) {
			return true;
		}

		state.refs[label] = linkInfo;

		let ref = newNode("link_ref", true, state.i, state.line, 1, "", 0, []);

		if (state.hasBlankLine && parent.children !== undefined && parent.children.length > 0) {
			parent.children.at(-1)!.blankAfter = true;
			state.hasBlankLine = false;
		}

		parent.children!.push(ref);

		if (!isNewLine(state.src[state.i - 1])) {
			state.i = getEndOfLine(state);
		}

		return true;
	}

	return false;
}

function testContinue(_state: BlockParserState, _node: MarkdownNode) {
	return false;
}
