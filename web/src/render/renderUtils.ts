import type MarkdownNode from "../types/MarkdownNode";
import type RendererState from "../types/RendererState";
import { getFirstChild } from "../utils/nodeUtils";

export function startNewLine(node: MarkdownNode, state: RendererState): void {
	if (state.output.length && node.block && !state.output.endsWith("\n")) {
		state.output += "\n";
	}
}

export function innerNewLine(node: MarkdownNode, state: RendererState): void {
	if (node.block) {
		let firstChild = getFirstChild(node);
		if (firstChild !== undefined && firstChild.block) {
			state.output += "\n";
		}
	}
}

export function endNewLine(node: MarkdownNode, state: RendererState): void {
	if (node.block) {
		state.output += "\n";
	}
}
