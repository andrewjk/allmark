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
		let trim =
			node.type !== "code_block" && node.type !== "code_fence" && node.type !== "code_span";
		for (let [i, child] of children.entries()) {
			let first = i === 0;
			let last = i === children.length - 1;
			renderNode(child, state, trim && first, trim && last, decode);
		}
	}
}
