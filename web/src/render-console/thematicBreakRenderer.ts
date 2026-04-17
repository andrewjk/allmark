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
	const style = ANSI.dim;
	const reset = ANSI.reset;
	state.output += `${style}───${reset}\n\n`;
}
