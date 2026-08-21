import type FootnoteReference from "../types/FootnoteReference";
import type InlineParserState from "../types/InlineParserState";
import type InlineRule from "../types/InlineRule";
import type LinkReference from "../types/LinkReference";
import type MarkdownNode from "../types/MarkdownNode";
import newText from "../utils/newText";
import skipSpaces from "../utils/skipSpaces";
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
			content = content.replaceAll(/(^(\r?\n|\r)\s+(\r?\n|\r)|(\r?\n|\r)\s*(\r?\n|\r)$)/g, "");
			// TODO: Should be treating EOF as a newline
			if (!content.endsWith("\n") && !content.endsWith("\r")) {
				content += "\n";
			}
		}
		let text = newText(parent.index, parent.line, content, 0);
		parent.children!.push(text);
		return;
	} else if (parent.type === "code_fence") {
		// "Fences can be indented. If the opening fence is indented, content lines will
		// have equivalent opening indentation removed, if present"
		let content = parent.content;
		if (/[^\s]/.test(content)) {
			if (parent.indent > 0) {
				content = content.replaceAll(new RegExp(`(^|\\r?\\n|\\r) {1,${parent.indent}}`, "g"), "$1");
			}
			// HACK: Not sure about this logic:
			content = content.replaceAll(/^(\r?\n|\r)\s+\1/g, "");
			// TODO: Should be treating EOF as a newline
			if (!content.endsWith("\n") && !content.endsWith("\r")) {
				content += "\n";
			}
		}
		let text = newText(parent.index, parent.line, content, 0);
		parent.children!.push(text);
		return;
	}

	let state: InlineParserState = {
		rules,
		// "Final spaces are stripped before inline parsing"
		src: parent.content.trimEnd(),
		// Skip the start spaces, but keep them in src so it can be properly mapped
		i: skipSpaces(parent.content, 0),
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

	// TODO: Do this first so we don't have to check whether it's a block?
	if (parent.children !== undefined) {
		for (let child of parent.children) {
			if (child.block) {
				parseBlockInlines(child, rules, refs, footnotes);
			}
		}
	}
}
