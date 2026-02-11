import parseBlock from "../parse/parseBlock";
import type BlockParserState from "../types/BlockParserState";
import type BlockRule from "../types/BlockRule";
import type MarkdownNode from "../types/MarkdownNode";
import closeNode from "../utils/closeNode";
import isNewLine from "../utils/isNewLine";
import newNode from "../utils/newNode";

const rule: BlockRule = {
	name: "code_block",
	testStart,
	testContinue,
};
export default rule;

/**
 * "An indented code block is composed of one or more indented chunks separated
 * by blank lines. An indented chunk is a sequence of non-blank lines, each
 * indented four or more spaces. The contents of the code block are the literal
 * contents of the lines, including trailing line endings, minus four spaces of
 * indentation. An indented code block has no info string.
 *
 * An indented code block cannot interrupt a paragraph, so there must be a blank
 * line between a paragraph and a following indented code block. (A blank line
 * is not needed, however, between a code block and a following paragraph.)"
 */

function testStart(state: BlockParserState, parent: MarkdownNode) {
	if (parent.acceptsContent) {
		return false;
	}

	// "An indented code block cannot interrupt a paragraph, so there must be a
	// blank line between a paragraph and a following indented code block"
	if (parent.type === "paragraph" && !parent.blankAfter) {
		return false;
	}

	if (state.indent >= 4 && !isNewLine(state.src[state.i])) {
		let closedNode: MarkdownNode | undefined;

		// TODO: rule.canContain?? e.g. list_ordered.canContain = ["list_item"] etc
		if (parent.type === "list_ordered" || parent.type === "list_bulleted") {
			closedNode = state.openNodes.pop();
			parent = state.openNodes.at(-1)!;
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

		if (closedNode !== undefined) {
			closeNode(state, closedNode);
		}

		let codeIndent = state.indent - 4;

		let code = newNode("code_block", true, state.i, state.line, 1, "    ", codeIndent, []);
		code.acceptsContent = true;
		code.content += " ".repeat(codeIndent);

		if (state.hasBlankLine && parent.children !== undefined && parent.children.length > 0) {
			parent.children.at(-1)!.blankAfter = true;
			state.hasBlankLine = false;
		}

		parent.children!.push(code);
		state.openNodes.push(code);

		state.indent = 0;
		state.hasBlankLine = false;
		parseBlock(state, code);

		return true;
	}

	return false;
}

function testContinue(state: BlockParserState, node: MarkdownNode) {
	if (state.hasBlankLine && state.indent >= 4) {
		// "Any initial spaces beyond four will be included in the content,
		// even in interior blank lines"
		node.content += " ".repeat(state.indent - 4);
	}

	if (state.indent >= 4) {
		state.indent -= 4;
		return true;
	}

	if (state.hasBlankLine) {
		return true;
	}

	return false;
}
