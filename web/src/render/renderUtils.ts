import type MarkdownNode from "../types/MarkdownNode";
import type RendererState from "../types/RendererState";

export function startNewLine(node: MarkdownNode, state: RendererState): void {
	if (state.output.length && node.block) {
		if (state.output.endsWith("\n")) {
			state.output = state.output.slice(0, -1);
		}
		if (state.output.endsWith("\r")) {
			state.output = state.output.slice(0, -1);
		}
		state.output += "\n";
	}
}

export function innerNewLine(node: MarkdownNode, state: RendererState): void {
	if (node.block && node.children && node.children[0]?.block) {
		state.output += "\n";
	}
}

export function endNewLine(node: MarkdownNode, state: RendererState): void {
	if (node.block) {
		state.output += "\n";
	}
}
