import type MarkdownNode from "../types/MarkdownNode";
import type RendererState from "../types/RendererState";
import renderChildren from "./renderChildren";
import { endNewLine, innerNewLine, startNewLine } from "./utils";

export default function render(
	node: MarkdownNode,
	state: RendererState,
	tag: string,
	decode = true,
): void {
	startNewLine(node, state);
	state.output += `<${tag}>`;
	// Block nodes with no children still need a newline
	if (node.block && node.children?.length === 0) {
		state.output += "\n";
	} else {
		innerNewLine(node, state);
	}
	renderChildren(node, state, decode);
	state.output += `</${tag}>`;
	endNewLine(node, state);
}
