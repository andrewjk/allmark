import type ConsoleRendererState from "../types/ConsoleRendererState";
import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import ANSI from "./ansi";

const renderer: Renderer = {
	name: "block_quote",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	const s = state as ConsoleRendererState;
	const style = ANSI.dim;
	const reset = ANSI.reset;
	for (const line of node.content.split("\n")) {
		if (line !== "") {
			s.output += `${style}┃ ${reset}${line}\n`;
		}
	}
	if (node.children) {
		for (const child of node.children) {
			const lines = renderNodeToString(child, s);
			for (const line of lines.split("\n")) {
				if (line) {
					s.output += `${style}┃${reset} ${line}\n`;
				}
			}
		}
	}
	s.output += "\n";
}

function renderNodeToString(node: MarkdownNode, state: ConsoleRendererState): string {
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
