import type ConsoleRendererState from "../types/ConsoleRendererState";
import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import ANSI from "./ansi";
import renderChildren from "./renderChildren";

const renderer: Renderer = {
	name: "link",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	const s = state as ConsoleRendererState;
	const style = ANSI.underline + ANSI.blue;
	const reset = ANSI.reset;
	s.output += style;
	renderChildren(node, state);
	if (node.info) {
		s.output += `${reset} ${ANSI.dim}(${node.info})${reset}`;
	} else {
		s.output += reset;
	}
}
