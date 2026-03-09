import type ConsoleRendererState from "../types/ConsoleRendererState";
import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import ANSI from "./ansi";
import renderChildren from "./renderChildren";

const bullets = ["•", "◦", "▪", "‣"];

const renderer: Renderer = {
	name: "list_bulleted",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	renderNode(node, state, false, bullets);
}

function renderNode(
	node: MarkdownNode,
	state: RendererState,
	ordered: boolean,
	bullets: string[],
): void {
	const s = state as ConsoleRendererState;
	s.listDepth++;

	const style = ANSI.dim;
	const reset = ANSI.reset;

	const loose = isLooseList(node);

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
			: bullets[Math.min(s.listDepth - 1, bullets.length - 1)] || "•";
		if (ordered) counter++;

		if (item.children) {
			for (const [i, child] of item.children.entries()) {
				if (!loose && child.type === "paragraph") {
					const indent = "  ".repeat(s.listDepth - 1);
					if (i === 0) {
						s.output += `${indent}${style}${prefix}${reset} `;
					}
					renderChildren(child, state);
					s.output += "\n";
				} else {
					const indent = "  ".repeat(s.listDepth - 1);
					if (i === 0) {
						s.output += `${indent}${style}${prefix}${reset} `;
					}
					const renderer = state.renderers.get(child.type);
					if (renderer) {
						renderer.render(child, state);
					}
				}
			}
		}
	}

	s.listDepth--;
}

function isLooseList(node: MarkdownNode): boolean {
	for (let i = 0; i < (node.children?.length ?? 0) - 1; i++) {
		const child = node.children![i]!;
		const grandchild = child.children?.at(-1);
		if (grandchild?.blankAfter) {
			return true;
		}
	}
	return false;
}
