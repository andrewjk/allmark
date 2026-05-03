import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";

const renderer: Renderer = {
	name: "footnote_ref",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	if (node.info !== undefined) {
		state.footnoteRefs[node.info] = node;
	}
}
