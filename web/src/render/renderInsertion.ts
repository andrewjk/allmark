import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import renderChildren from "./renderChildren";
import { endNewLine, startNewLine } from "./utils";

const renderer: Renderer = {
	name: "insertion",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	startNewLine(node, state);
	state.output += `<ins class="markdown-insertion">`;
	renderChildren(node, state);
	state.output += "</ins>";
	endNewLine(node, state);
}
