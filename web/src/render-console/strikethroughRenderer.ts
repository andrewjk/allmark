import type ConsoleRendererState from "../types/ConsoleRendererState";
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
	const s = state as ConsoleRendererState;
	const style = ANSI.dim;
	const reset = ANSI.reset;
	s.output += `${style}\x1b[9m`;
	renderChildren(node, state);
	s.output += `\x1b[29m${reset}`;
}
