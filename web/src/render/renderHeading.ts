import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import renderChildren from "./renderChildren";
import { endNewLine, innerNewLine, startNewLine } from "./utils";

const renderer: Renderer = {
	name: "heading",
	render,
};
export default renderer;

export function render(node: MarkdownNode, state: RendererState): void {
	startNewLine(node, state);
	let level = 0;
	if (node.markup.startsWith("#")) {
		level = node.markup.length;
	} else if (node.markup.includes("=")) {
		level = 1;
	} else if (node.markup.includes("-")) {
		level = 2;
	}
	state.output += `<h${level}>`;
	innerNewLine(node, state);
	renderChildren(node, state);
	state.output += `</h${level}>`;
	endNewLine(node, state);
}
