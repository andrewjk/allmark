import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import ANSI from "./ansi";
import renderChildren from "./renderChildren";

const renderer: Renderer = {
	name: "footnote_list",
	render,
};
export default renderer;

function render(_node: MarkdownNode, state: RendererState): void {
	if (state.footnotes.length === 0) {
		return;
	}
	state.output += `\n${ANSI.dim}---${ANSI.reset}\n`;
	let number = 1;
	for (let node of state.footnotes) {
		let label = number++;
		state.output += `${ANSI.dim}[${label}]${ANSI.reset} `;
		let refNode = state.footnoteRefs[node.info!];
		if (refNode !== undefined) {
			renderChildren(refNode, state);
		}
		if (state.output.endsWith("\n")) {
			state.output = state.output.slice(0, -1);
		}
		if (state.output.endsWith("\r")) {
			state.output = state.output.slice(0, -1);
		}
		state.output += "\n";
	}
}
