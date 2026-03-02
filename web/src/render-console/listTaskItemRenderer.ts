import type ConsoleRendererState from "../types/ConsoleRendererState";
import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import ANSI from "./ansi";

const renderer: Renderer = {
	name: "list_task_item",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	const s = state as ConsoleRendererState;
	const style = ANSI.dim;
	const reset = ANSI.reset;

	const isChecked = node.markup?.[1] !== " ";
	const emoji = isChecked ? "✓" : " ";
	s.output += `${style}[${reset}${emoji}${style}]${reset} `;
}
