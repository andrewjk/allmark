import type ConsoleRendererState from "../types/ConsoleRendererState";
import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import renderChildren from "./renderChildren";

const renderer: Renderer = {
	name: "strikethrough",
	render,
};
export default renderer;

export function createRenderer(style: string, reset: string): Renderer {
	return {
		name: "strikethrough",
		render(node: MarkdownNode, state: RendererState) {
			renderNode(node, state, style, reset);
		},
	};
}

function render(node: MarkdownNode, state: RendererState): void {
	const style = "\x1b[2m";
	const reset = "\x1b[0m";
	renderNode(node, state, style, reset);
}

function renderNode(node: MarkdownNode, state: RendererState, style: string, reset: string): void {
	const s = state as ConsoleRendererState;
	s.output += `${style}\x1b[9m`;
	renderChildren(node, state);
	s.output += `\x1b[29m${reset}`;
}
