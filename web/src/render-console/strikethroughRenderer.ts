import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import ANSI from "./ansi";
import renderChildren from "./renderChildren";

const renderer: Renderer = {
	name: "strikethrough",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	const style = ANSI.dim;
	const reset = ANSI.reset;
	state.output += `${style}\x1b[9m`;
	renderChildren(node, state);
	state.output += `\x1b[29m${reset}`;
}
