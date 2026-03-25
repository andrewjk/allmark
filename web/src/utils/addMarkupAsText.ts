import type InlineParserState from "../types/InlineParserState";
import type MarkdownNode from "../types/MarkdownNode";
import newText from "./newText";

export default function addMarkupAsText(
	markup: string,
	state: InlineParserState,
	parent: MarkdownNode,
): void {
	let lastNode = parent.children!.at(-1);
	let haveText = lastNode && lastNode.type === "text";
	let text = haveText ? lastNode! : newText(state.i, state.line, "", 0);
	text.content += markup;
	if (!haveText) {
		parent.children!.push(text);
	}
	state.i += markup.length;
}
