import type InlineParserState from "../types/InlineParserState";
import type MarkdownNode from "../types/MarkdownNode";
import newInline from "./newInline";

export default function addMarkupAsText(
	markup: string,
	state: InlineParserState,
	parent: MarkdownNode,
): void {
	let lastNode = parent.children!.at(-1);
	let haveText = lastNode && lastNode.type === "text";
	let text = haveText ? lastNode! : newInline("text", state.i, state.line, "", 0);
	text.markup += markup;
	if (!haveText) {
		parent.children!.push(text);
	}
	state.i += markup.length;
}
