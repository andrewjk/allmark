import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import { forEachChild } from "../utils/nodeUtils";
import ANSI from "./ansi";

const renderer: Renderer = {
	name: "block_quote",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	const style = ANSI.dim;
	const reset = ANSI.reset;
	for (const line of node.content.split("\n")) {
		if (line !== "") {
			state.output += `${style}┃ ${reset}${line}\n`;
		}
	}
	forEachChild(node, (child) => {
		const lines = renderNodeToString(child, state);
		for (const line of lines.split("\n")) {
			if (line) {
				state.output += `${style}┃${reset} ${line}\n`;
			}
		}
	});
	state.output += "\n";
}

function renderNodeToString(node: MarkdownNode, state: RendererState): string {
	const output = state.output;
	state.output = "";
	const renderer = state.renderersMap.get(node.type);
	if (renderer) {
		renderer.render(node, state);
	}
	const result = state.output;
	state.output = output;
	return result;
}
