import type InlineParserState from "../types/InlineParserState";
import type MarkdownNode from "../types/MarkdownNode";
import newNode from "./newNode";

export default function addMarkupAsText(
	markup: string,
	state: InlineParserState,
	parent: MarkdownNode,
): void {
	let lastNode = parent.children!.at(-1);
	let haveText = lastNode && lastNode.type === "text";
	let text = haveText ? lastNode! : newNode("text", false, state.i, state.line, 1, "", 0);
	text.markup += markup;
	if (!haveText) {
		parent.children!.push(text);
	}
	state.i += markup.length;
}
