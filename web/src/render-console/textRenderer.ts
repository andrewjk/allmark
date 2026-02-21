import type ConsoleRendererState from "../types/ConsoleRendererState";
import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";

const renderer: Renderer = {
	name: "text",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState, first?: boolean, last?: boolean): void {
	const s = state as ConsoleRendererState;
	let text = node.markup;
	if (first === true) {
		text = text.trimStart();
	}
	if (last === true) {
		text = text.trimEnd();
	}
	s.output += text;
}
