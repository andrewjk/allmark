import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";

const renderer: Renderer = {
	name: "footnote",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	if (state.footnotes.find((f) => f.info === node.info) === undefined) {
		state.footnotes.push(node);
	}
	let label = state.footnotes.length;
	let id = `fnref${label}`;
	let href = `#fn${label}`;
	state.output += `<sup class="footnote-ref"><a href="${href}" id="${id}">${label}</a></sup>`;
}
