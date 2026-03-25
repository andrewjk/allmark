import type ConsoleRendererState from "../types/ConsoleRendererState";
import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";

const renderer: Renderer = {
	name: "text",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	const s = state as ConsoleRendererState;
	s.output += node.content;
}
