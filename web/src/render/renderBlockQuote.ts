import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import renderTag from "./renderTag";

const renderer: Renderer = {
	name: "block_quote",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	renderTag(node, state, "blockquote");
}
