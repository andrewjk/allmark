import type ConsoleRendererState from "../types/ConsoleRendererState";
import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";

const renderer: Renderer = {
	name: "image",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	const style = "\x1b[2m";
	const reset = "\x1b[0m";
	renderNode(node, state, style, reset);
}

function renderNode(node: MarkdownNode, state: RendererState, style: string, reset: string): void {
	const s = state as ConsoleRendererState;
	let alt = "";
	if (node.children) {
		for (const child of node.children) {
			if (child.type === "text") {
				alt += child.markup;
			}
		}
	}
	s.output += `${style}[Image: ${alt || node.info || ""}]${reset}`;
}
