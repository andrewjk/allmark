import type ConsoleRendererState from "../types/ConsoleRendererState";
import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";

const renderer: Renderer = {
	name: "code_block",
	render,
};
export default renderer;

export function render(node: MarkdownNode, state: RendererState): void {
	const style = "\x1b[2m";
	const reset = "\x1b[0m";
	renderNode(node, state, style, reset);
}

function renderNode(node: MarkdownNode, state: RendererState, style: string, reset: string): void {
	const s = state as ConsoleRendererState;
	if (s.output.length && !s.output.endsWith("\n")) {
		s.output += "\n";
	}
	s.output += `${style}┌─${reset}\n`;
	let lines = node.content.split("\n");
	if (lines.length && !lines[lines.length - 1].length) {
		lines.pop();
	}
	for (const line of lines) {
		s.output += `${style}│${reset} ${line}\n`;
	}
	s.output += `${style}└─${reset}\n`;
}
