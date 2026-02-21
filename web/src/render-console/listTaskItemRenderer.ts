import type ConsoleRendererState from "../types/ConsoleRendererState";
import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";

const renderer: Renderer = {
	name: "list_task_item",
	render,
};
export default renderer;

export function createRenderer(green: string, reset: string): Renderer {
	return {
		name: "list_task_item",
		render(node: MarkdownNode, state: RendererState) {
			renderNode(node, state, green, reset);
		},
	};
}

function render(node: MarkdownNode, state: RendererState): void {
	const green = "\x1b[32m";
	const reset = "\x1b[0m";
	renderNode(node, state, green, reset);
}

function renderNode(node: MarkdownNode, state: RendererState, _green: string, _reset: string): void {
	const s = state as ConsoleRendererState;
	const isChecked = node.markup?.[1] !== " ";
	const emoji = isChecked ? "[✓]" : "[ ]";
	s.output += `${emoji} `;
}
