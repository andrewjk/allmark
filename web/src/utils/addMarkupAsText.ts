import type InlineParserState from "../types/InlineParserState";
import type MarkdownNode from "../types/MarkdownNode";
import newText from "./newText";
import { appendChild, getLastChild } from "./nodeUtils";

export default function addMarkupAsText(
	markup: string,
	state: InlineParserState,
	parent: MarkdownNode,
): void {
	let lastNode = getLastChild(parent);
	let haveText = lastNode !== undefined && lastNode.type === "text";
	let text = haveText ? lastNode! : newText(state.i, state.line, "", 0);
	text.content += markup;
	if (!haveText) {
		appendChild(parent, text);
	}
	state.i += markup.length;
}
