import type MarkdownNode from "../types/MarkdownNode";
import type RendererState from "../types/RendererState";
import { forEachChild } from "../utils/nodeUtils";
import renderNode from "./renderNode";

export default function renderChildren(
	node: MarkdownNode,
	state: RendererState,
	decode = true,
): void {
	forEachChild(node, (child) => {
		renderNode(child, state, decode);
	});
}
