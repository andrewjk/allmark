import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import renderChildren from "./renderChildren";
import { endNewLine, startNewLine } from "./renderUtils";

const renderer: Renderer = {
	name: "code_block",
	render,
};
export default renderer;

export function render(node: MarkdownNode, state: RendererState): void {
	startNewLine(node, state);
	let lang = node.info ? ` class="language-${node.info.trim().split(" ")[0]}"` : "";
	state.output += `<pre><code${lang}>`;
	renderChildren(node, state, false);
	state.output += "</code></pre>";
	endNewLine(node, state);
}
