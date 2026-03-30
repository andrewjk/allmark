import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import renderChildren from "./renderChildren";
import { endNewLine, startNewLine } from "./renderUtils";

const renderer: Renderer = {
	name: "heading",
	render,
};
export default renderer;

export function render(node: MarkdownNode, state: RendererState): void {
	startNewLine(node, state);
	let level = node.markup.length;
	state.output += `<h${level}>`;
	if (node.children !== undefined && node.children.length > 0) {
		renderChildren(node.children[0], state);
	}
	state.output += `</h${level}>`;
	endNewLine(node, state);
}
