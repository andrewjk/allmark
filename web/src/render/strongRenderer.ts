import MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import renderTag from "./renderTag";

const renderer: Renderer = {
	name: "strong",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState) {
	renderTag(node, state, "strong");
}
