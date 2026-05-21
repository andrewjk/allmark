import type MarkdownNode from "../types/MarkdownNode";
import type RendererState from "../types/RendererState";
import ANSI from "./ansi";
import renderChildren from "./renderChildren";

const bullets = ["•", "◦", "▪", "‣"];

export default function render(node: MarkdownNode, state: RendererState, ordered: boolean): void {
	state.listDepth++;

	const style = ANSI.dim;
	const reset = ANSI.reset;

	let counter = 1;
	if (ordered && node.markup) {
		const match = node.markup.match(/^(\d+)/);
		if (match) {
			counter = parseInt(match[1], 10);
		}
	}

	for (const item of node.children ?? []) {
		const prefix = ordered
			? `${counter}.`
			: bullets[Math.min(state.listDepth - 1, bullets.length - 1)] || "•";
		if (ordered) counter++;

		if (item.children) {
			for (const [i, child] of item.children.entries()) {
				if (!node.loose && child.type === "paragraph") {
					const indent = "  ".repeat(state.listDepth - 1);
					if (i === 0) {
						state.output += `${indent}${style}${prefix}${reset} `;
					}
					renderChildren(child, state);
					state.output += "\n";
				} else {
					const indent = "  ".repeat(state.listDepth - 1);
					if (i === 0) {
						state.output += `${indent}${style}${prefix}${reset} `;
					}
					const renderer = state.renderersMap.get(child.type);
					if (renderer) {
						renderer.render(child, state);
					}
					if (!node.loose && state.output.endsWith("\n\n")) {
						state.output = state.output.slice(0, state.output.length - 1);
					}
				}
			}
		}
	}

	state.listDepth--;

	// Loose lists will already have a double newline
	if (!node.loose) {
		state.output += "\n";
	}
}
