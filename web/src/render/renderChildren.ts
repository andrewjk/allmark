import type MarkdownNode from "../types/MarkdownNode";
import type RendererState from "../types/RendererState";
import renderNode from "./renderNode";

export default function renderChildren(
	node: MarkdownNode,
	state: RendererState,
	decode = true,
): void {
	let children = node.children;
	if (children && children.length) {
		for (let child of children) {
			renderNode(child, state, decode);
		}
	}
}
