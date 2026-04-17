import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import ANSI from "./ansi";

const renderer: Renderer = {
	name: "code_block",
	render,
};
export default renderer;

export function render(node: MarkdownNode, state: RendererState): void {
	const style = ANSI.dim;
	const reset = ANSI.reset;
	state.output += `${style}┌─${reset}\n`;
	let lines = node.content.split("\n");
	if (lines.length && !lines[lines.length - 1].length) {
		lines.pop();
	}
	for (const line of lines) {
		state.output += `${style}│${reset} ${line}\n`;
	}
	state.output += `${style}└─${reset}\n\n`;
}
