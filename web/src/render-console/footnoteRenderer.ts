import type ConsoleRendererState from "../types/ConsoleRendererState";
import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";

const renderer: Renderer = {
	name: "footnote",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	const style = "\x1b[2m";
	const reset = "\x1b[0m";
	renderNode(node, state, style, reset);
}

function renderNode(node: MarkdownNode, state: RendererState, style: string, reset: string): void {
	const s = state as ConsoleRendererState;
	if (s.footnotes.find((_f) => _f.info === node.info) === undefined) {
		s.footnotes.push(node);
	}
	const label = s.footnotes.length;
	s.output += `${style}[${label}]${reset}`;
}
