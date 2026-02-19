import type MarkdownNode from "../types/MarkdownNode";
import type RendererState from "../types/RendererState";
import renderChildren from "./renderChildren";
import { endNewLine, innerNewLine, startNewLine } from "./renderUtils";

export default function render(
	node: MarkdownNode,
	state: RendererState,
	tag: string,
	decode = true,
): void {
	startNewLine(node, state);
	state.output += `<${tag}>`;
	if (node.block && node.children?.length === 0) {
		// Block nodes with no children still need a newline
		state.output += "\n";
	} else {
		innerNewLine(node, state);
		renderChildren(node, state, decode);
		if (node.block && !state.output.endsWith("\n")) {
			state.output += "\n";
		}
	}
	state.output += `</${tag}>`;
	endNewLine(node, state);
}
