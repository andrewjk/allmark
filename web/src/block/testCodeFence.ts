import type BlockParserState from "../types/BlockParserState";
import type BlockRule from "../types/BlockRule";
import type MarkdownNode from "../types/MarkdownNode";
import closeNode from "../utils/closeNode";
import decodeEntities from "../utils/decodeEntities";
import escapeBackslashes from "../utils/escapeBackslashes";
import getEndOfLine from "../utils/getEndOfLine";
import isNewLine from "../utils/isNewLine";
import isSpace from "../utils/isSpace";
import newNode from "../utils/newNode";

const rule: BlockRule = {
	name: "code_fence",
	testStart,
	testContinue,
};
export default rule;

/**
 * "A code fence is a sequence of at least three consecutive backtick characters
 * (`) or tildes (~). (Tildes and backticks cannot be mixed.) A fenced code
 * block begins with a code fence, indented no more than three spaces.
 *
 * The line with the opening code fence may optionally contain some text
 * following the code fence; this is trimmed of leading and trailing whitespace
 * and called the info string. If the info string comes after a backtick fence,
 * it may not contain any backtick characters. (The reason for this restriction
 * is that otherwise some inline code would be incorrectly interpreted as the
 * beginning of a fenced code block.)
 *
 * The content of the code block consists of all subsequent lines, until a
 * closing code fence of the same type as the code block began with (backticks
 * or tildes), and with at least as many backticks or tildes as the opening code
 * fence. If the leading code fence is indented N spaces, then up to N spaces of
 * indentation are removed from each line of the content (if present). (If a
 * content line is not indented, it is preserved unchanged. If it is indented
 * less than N spaces, all of the indentation is removed.)
 *
 * The closing code fence may be indented up to three spaces, and may be
 * followed only by spaces, which are ignored. If the end of the containing
 * block (or document) is reached and no closing code fence has been found, the
 * code block contains all of the lines after the opening code fence until the
 * end of the containing block (or document). (An alternative spec would require
 * backtracking in the event that a closing code fence is not found. But this
 * makes parsing much less efficient, and there seems to be no real down side to
 * the behavior described here.)
 *
 * A fenced code block may interrupt a paragraph, and does not require a blank
 * line either before or after.
 *
 * The content of a code fence is treated as literal text, not parsed as
 * inlines. The first word of the info string is typically used to specify the
 * language of the code sample, and rendered in the class attribute of the code
 * tag. However, this spec does not mandate any particular treatment of the info
 * string."
 */

function testStart(state: BlockParserState, parent: MarkdownNode) {
	// A fenced code block can't be started in a block that accepts content
	if (parent.acceptsContent) {
		return false;
	}

	let char = state.src[state.i];
	if (state.indent <= 3 && (char === "`" || char === "~")) {
		let matched = 1;
		let end = state.i + 1;
		let haveSpace = false;
		for (; end < state.src.length; end++) {
			let nextChar = state.src[end];
			if (nextChar === char) {
				if (haveSpace) {
					return false;
				}
				matched++;
			} else if (isNewLine(nextChar)) {
				break;
			} else if (isSpace(state.src.charCodeAt(end))) {
				haveSpace = true;
			} else {
				break;
			}
		}
		if (matched >= 3) {
			let closedNode: MarkdownNode | undefined;

			let markup = char.repeat(matched);

			let info = "";
			if (!isNewLine(state.src[state.i + matched])) {
				end = getEndOfLine(state);
				info = state.src.substring(state.i + matched, end);

				// "Info strings for backtick code blocks cannot contain backticks"
				// But "Info strings for tilde code blocks can contain backticks and tildes"?!
				// TODO: What if it's escaped?
				if (char === "`" && info.includes("`")) {
					return false;
				}

				info = decodeEntities(info);
				info = escapeBackslashes(info);
			} else {
				// TODO: I think the \n should go into the content and then it
				// can be rendered without getting fancy about calculating where
				// newlines go?
				end++;
			}

			if (state.maybeContinue) {
				state.maybeContinue = false;
				let i = state.openNodes.length;
				while (i-- > 1) {
					let node = state.openNodes[i];
					if (node.maybeContinuing) {
						node.maybeContinuing = false;
						closedNode = node;
						state.openNodes.length = i;
						break;
					}
				}
				parent = state.openNodes.at(-1)!;
			}

			// If there's an open paragraph, close it
			// TODO: Is there a better way to do this??
			if (parent.type === "paragraph") {
				closedNode = state.openNodes.pop();
				parent = state.openNodes.at(-1)!;
			}

			if (closedNode !== undefined) {
				closeNode(state, closedNode);
			}

			let code = newNode("code_fence", true, state.i, state.line, 1, markup, state.indent, []);
			code.acceptsContent = true;
			code.info = info;

			state.i = end;

			if (state.hasBlankLine && parent.children !== undefined && parent.children.length > 0) {
				parent.children.at(-1)!.blankAfter = true;
				state.hasBlankLine = false;
			}

			parent.children!.push(code);
			state.openNodes.push(code);

			return true;
		}
	}

	return false;
}

function testContinue(state: BlockParserState, node: MarkdownNode) {
	if (state.hasBlankLine) {
		node.content += " ".repeat(state.indent);
		return true;
	}

	let char = state.src[state.i];
	if (state.indent <= 3 && (char === "`" || char === "~")) {
		// This might be a closing fence, and if so, we can close the block
		if (node.markup.startsWith(char)) {
			let endMatched = 0;
			let end = state.i;
			for (; end < state.src.length; end++) {
				let nextChar = state.src[end];
				if (nextChar === char) {
					endMatched++;
				} else {
					break;
				}
			}
			if (endMatched >= node.markup.length) {
				// "Closing code fences cannot have info strings"
				for (; end < state.src.length; end++) {
					let nextChar = state.src[end];
					if (isNewLine(nextChar)) {
						break;
					} else if (isSpace(state.src.charCodeAt(end))) {
						continue;
					} else {
						return true;
					}
				}

				state.i = end;
				return false;
			}
		}
	}

	return true;
}
