import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import { getFirstChild } from "../utils/nodeUtils";
import ANSI from "./ansi";
import renderChildren from "./renderChildren";

const renderer: Renderer = {
	name: "heading",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	const style = ANSI.bold + ANSI.magenta;
	const reset = ANSI.reset;

	let level = node.markup.length;

	state.output += `${ANSI.dim}${"#".repeat(level)}${reset} ${style}`;
	let firstChild = getFirstChild(node);
	if (firstChild !== undefined) {
		renderChildren(firstChild, state);
	}
	state.output += `${reset}\n\n`;
}
