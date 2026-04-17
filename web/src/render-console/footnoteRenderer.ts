import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import ANSI from "./ansi";

const renderer: Renderer = {
	name: "footnote",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	const style = ANSI.dim;
	const reset = ANSI.reset;
	if (state.footnotes.find((f) => f.info === node.info) === undefined) {
		state.footnotes.push(node);
	}
	const label = state.footnotes.length;
	state.output += `${style}[${label}]${reset}`;
}
