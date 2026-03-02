import type ConsoleRendererState from "../types/ConsoleRendererState";
import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import ANSI from "./ansi";

const renderer: Renderer = {
	name: "footnote",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	const s = state as ConsoleRendererState;
	const style = ANSI.dim;
	const reset = ANSI.reset;
	if (s.footnotes.find((f) => f.info === node.info) === undefined) {
		s.footnotes.push(node);
	}
	const label = s.footnotes.length;
	s.output += `${style}[${label}]${reset}`;
}
