import type MarkdownNode from "../types/MarkdownNode";
import type RendererState from "../types/RendererState";

export default function renderNode(
	node: MarkdownNode,
	state: RendererState,
	first = false,
	last = false,
	decode = true,
): void {
	const renderer = state.renderers.get(node.type);
	if (renderer !== undefined) {
		//throw new Error(`No renderer for node type '${node.type}'`);
		renderer.render(node, state, first, last, decode);
	}
}
