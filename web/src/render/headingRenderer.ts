import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import { getFirstChild } from "../utils/nodeUtils";
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
	let firstChild = getFirstChild(node);
	if (firstChild !== undefined) {
		renderChildren(firstChild, state);
	}
	state.output += `</h${level}>`;
	endNewLine(node, state);
}
