import type FootnoteReference from "../types/FootnoteReference";
import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type LinkReference from "../types/LinkReference";
import type MarkdownNode from "../types/MarkdownNode";
import newText from "../utils/newText";
import { appendChild } from "../utils/nodeUtils";
import parseInline from "./parseInline";

export default function parseBlockInlines(
	parent: MarkdownNode,
	rules: InlineRule[],
	refs: Record<string, LinkReference>,
	footnotes: Record<string, FootnoteReference>,
): void {
	if (parent.type === "html_block") {
		return;
	}

	// TODO: These should be done in rules
	if (parent.type === "code_block") {
		// "Blank lines preceding or following a code block are not included in it"
		// "A code block can have all empty lines as its content"
		let content = parent.content;
		if (/[^\s]/.test(content)) {
			// HACK: Not sure about this logic:
			content = content.replaceAll(/(^\n\s+\n|\n\s*\n$)/g, "");
			// TODO: Should be treating EOF as a newline
			if (!content.endsWith("\n")) {
				content += "\n";
			}
		}
		let text = newText(parent.index, parent.line, content, 0);
		appendChild(parent, text);
		return;
	} else if (parent.type === "code_fence") {
		// "Fences can be indented. If the opening fence is indented, content lines will
		// have equivalent opening indentation removed, if present"
		let content = parent.content;
		if (/[^\s]/.test(content)) {
			if (parent.indent > 0) {
				content = content.replaceAll(new RegExp(`(^|\\n) {1,${parent.indent}}`, "g"), "$1");
			}
			// HACK: Not sure about this logic:
			content = content.replaceAll(/^\n\s+\n/g, "");
			// TODO: Should be treating EOF as a newline
			if (!content.endsWith("\n")) {
				content += "\n";
			}
		}
		let text = newText(parent.index, parent.line, content, 0);
		appendChild(parent, text);
		return;
	}

	let state: InlineParserState = {
		rules,
		// "Final spaces are stripped before inline parsing"
		src: parent.content.trim(),
		i: 0,
		line: parent.line,
		lineStart: 0,
		indent: 0,
		isEscaped: false,
		delimiters: [],
		refs,
		footnotes,
		parentIndex: parent.index,
	};

	parseInline(state, parent);
}
