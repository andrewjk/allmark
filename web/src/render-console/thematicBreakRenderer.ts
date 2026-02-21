import type ConsoleRendererState from "../types/ConsoleRendererState";
import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";

const renderer: Renderer = {
	name: "thematic_break",
	render,
};
export default renderer;

export function createRenderer(style: string, reset: string): Renderer {
	return {
		name: "thematic_break",
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

function renderNode(_node: MarkdownNode, state: RendererState, style: string, reset: string): void {
	const s = state as ConsoleRendererState;
	if (s.output.length && !s.output.endsWith("\n")) {
		s.output += "\n";
	}
	if (s.output.length && !s.output.endsWith("\n\n")) {
		s.output += "\n";
	}
	s.output += `${style}───${reset}\n\n`;
}
