import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import renderChildren from "./renderChildren";
import { endNewLine, startNewLine } from "./utils";

const renderer: Renderer = {
	name: "alert",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	startNewLine(node, state);
	state.output += `<div class="markdown-alert markdown-alert-${node.markup}">
<p class="markdown-alert-title">${node.markup.substring(0, 1).toUpperCase() + node.markup.substring(1)}</p>`;
	renderChildren(node, state);
	state.output += "</div>";
	endNewLine(node, state);
}
