import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";

const renderer: Renderer = {
	name: "hard_break",
	render,
};
export default renderer;

function render(_node: MarkdownNode, state: RendererState): void {
	state.output += "<br />\n";
}
