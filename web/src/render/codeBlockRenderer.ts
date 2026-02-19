import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import escapeHtml from "../utils/escapeHtml";
import renderChildren from "./renderChildren";
import { endNewLine, startNewLine } from "./renderUtils";

const renderer: Renderer = {
	name: "code_block",
	render,
};
export default renderer;

export function render(node: MarkdownNode, state: RendererState): void {
	// HACK: Probably shouldn't even be in the tree by this point...
	if (node.type === "code_block" && !node.content.length) {
		return;
	}

	startNewLine(node, state);
	let lang = "";
	if (node.info) {
		let trimmed = node.info.trim().split(" ")[0];
		if (trimmed) {
			lang = ` class="language-${escapeHtml(trimmed)}"`;
		}
	}
	state.output += `<pre><code${lang}>`;
	renderChildren(node, state, false);
	state.output += "</code></pre>";
	endNewLine(node, state);
}
