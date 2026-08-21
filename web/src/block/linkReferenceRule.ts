import type BlockParserState from "../types/BlockParserState";
import type BlockRule from "../types/BlockRule";
import type MarkdownNode from "../types/MarkdownNode";
import { BRACKET_OPEN_CODE, BRACKET_CLOSE_CODE, COLON_CODE } from "../utils/charCodes";
import isEscaped from "../utils/isEscaped";
import newBlock from "../utils/newBlock";
import normalizeLabel from "../utils/normalizeLabel";
import parseLinkReference from "../utils/parseLinkReference";

const rule: BlockRule = {
	name: "link_ref",
	testStart,
	testContinue: () => false,
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

function testStart(state: BlockParserState, parent: MarkdownNode, _endOfLine: number) {
	if (parent.acceptsContent) {
		return false;
	}

	if (
		!state.isEscaped &&
		state.indent <= 3 &&
		state.src.charCodeAt(state.i) === BRACKET_OPEN_CODE
	) {
		// "A link reference definition cannot interrupt a paragraph"
		if (parent.type === "paragraph" && !parent.blankAfter) {
			return false;
		}

		let start = state.i;
		let linkStart = state.i + 1;

		// Get the label
		let label = "";
		for (let i = linkStart; i < state.src.length; i++) {
			if (!isEscaped(state.src, i)) {
				let nextCharCode = state.src.charCodeAt(i);

				if (nextCharCode === BRACKET_CLOSE_CODE) {
					label = state.src.substring(linkStart, i);
					linkStart = i + 1;
					break;
				}

				// "Link labels cannot contain brackets, unless they are
				// backslash-escaped"
				if (nextCharCode === BRACKET_OPEN_CODE) {
					return false;
				}
			}
		}
		// "A link label must contain at least one non-whitespace character"
		if (!label || !/[^\s]/.test(label)) {
			return false;
		}

		if (state.src.charCodeAt(linkStart) !== COLON_CODE) {
			return false;
		}

		linkStart++;

		let linkInfo = parseLinkReference(state, linkStart);
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

		let ref = newBlock("link_ref", start, state.line, "", 0);

		if (state.hasBlankLine && parent.children !== undefined && parent.children.length > 0) {
			parent.children.at(-1)!.blankAfter = true;
			state.hasBlankLine = false;
		}

		parent.children!.push(ref);

		ref.length = state.i - ref.index;

		return true;
	}

	return false;
}
