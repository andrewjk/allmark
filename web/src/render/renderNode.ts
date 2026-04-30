import type MarkdownNode from "../types/MarkdownNode";
import type RendererState from "../types/RendererState";

export default function renderNode(node: MarkdownNode, state: RendererState, decode = true): void {
	const renderer = state.renderersMap.get(node.type);
	if (renderer !== undefined) {
		//throw new Error(`No renderer for node type '${node.type}'`);
		renderer.render(node, state, decode);
	}
}
