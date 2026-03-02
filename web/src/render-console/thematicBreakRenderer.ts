import type ConsoleRendererState from "../types/ConsoleRendererState";
import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import ANSI from "./ansi";

const renderer: Renderer = {
	name: "thematic_break",
	render,
};
export default renderer;

function render(_node: MarkdownNode, state: RendererState): void {
	const s = state as ConsoleRendererState;
	const style = ANSI.dim;
	const reset = ANSI.reset;
	if (s.output.length && !s.output.endsWith("\n")) {
		s.output += "\n";
	}
	if (s.output.length && !s.output.endsWith("\n\n")) {
		s.output += "\n";
	}
	s.output += `${style}───${reset}\n\n`;
}
