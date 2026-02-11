import MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import renderSelfClosedTag from "./renderSelfClosedTag";

const renderer: Renderer = {
	name: "thematic_break",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState) {
	renderSelfClosedTag(node, state, "hr");
}
