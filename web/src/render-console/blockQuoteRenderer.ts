import type ConsoleRendererState from "../types/ConsoleRendererState";
import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";

const renderer: Renderer = {
	name: "block_quote",
	render,
};
export default renderer;

export function createRenderer(style: string, reset: string): Renderer {
	return {
		name: "block_quote",
		render(node: MarkdownNode, state: RendererState) {
			renderNode(node, state, style, reset);
		},
	};
}

function render(node: MarkdownNode, state: RendererState): void {
	const style = "\x1b[90m";
	const reset = "\x1b[0m";
	renderNode(node, state, style, reset);
}

function renderNode(node: MarkdownNode, state: RendererState, style: string, reset: string): void {
	const s = state as ConsoleRendererState;
	s.quoteDepth++;
	if (s.output.length && !s.output.endsWith("\n")) {
		s.output += "\n";
	}
	for (const line of node.content.split("\n")) {
		if (line !== "") {
			s.output += `${style}┃ ${reset}${line}\n`;
		}
	}
	if (node.children) {
		for (const child of node.children) {
			const lines = renderNodeToString(child, s, s.quoteDepth);
			for (const line of lines.split("\n")) {
				if (line) {
					s.output += `${style}┃${reset} ${line}\n`;
				}
			}
		}
	}
	s.quoteDepth--;
}

function renderNodeToString(node: MarkdownNode, state: ConsoleRendererState, _depth: number): string {
	const output = state.output;
	state.output = "";
	const renderer = state.renderers.get(node.type);
	if (renderer) {
		renderer.render(node, state);
	}
	const result = state.output;
	state.output = output;
	return result;
}
