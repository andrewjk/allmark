import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";

const renderer: Renderer = {
	name: "html_block",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	state.output += node.content;
}
