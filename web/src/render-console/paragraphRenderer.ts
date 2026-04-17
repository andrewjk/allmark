import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import renderChildren from "./renderChildren";

const renderer: Renderer = {
	name: "paragraph",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	renderChildren(node, state);
	state.output += "\n\n";
}
