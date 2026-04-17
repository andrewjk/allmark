import type ConsoleRendererState from "../types/ConsoleRendererState";
import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import ANSI from "./ansi";
import renderChildren from "./renderChildren";

const renderer: Renderer = {
	name: "heading",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	const s = state as ConsoleRendererState;
	const style = ANSI.bold + ANSI.magenta;
	const reset = ANSI.reset;

	let level = node.markup.length;

	s.output += `${ANSI.dim}${"#".repeat(level)}${reset} ${style}`;
	if (node.children !== undefined && node.children.length > 0) {
		renderChildren(node.children[0], state);
	}
	s.output += `${reset}\n\n`;
}
