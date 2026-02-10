import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import renderChildren from "./renderChildren";
import { endNewLine, innerNewLine, startNewLine } from "./utils";

const renderer: Renderer = {
	name: "table",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	startNewLine(node, state);
	state.output += "<table>\n<thead>\n<tr>\n";
	for (let cell of node.children![0].children!) {
		renderTableCell(cell, state, "th");
	}
	state.output += "</tr>\n</thead>\n";
	if (node.children!.length > 1) {
		state.output += "<tbody>\n";
		for (let row of node.children!.slice(1)) {
			state.output += "<tr>\n";
			for (let cell of row.children!) {
				renderTableCell(cell, state, "td");
			}
			state.output += "</tr>\n";
		}
		state.output += "</tbody>\n";
	}
	state.output += "</table>";
	endNewLine(node, state);
}

function renderTableCell(node: MarkdownNode, state: RendererState, tag: string) {
	startNewLine(node, state);
	let align = node.info ? ` align="${node.info}"` : "";
	state.output += `<${tag}${align}>`;
	innerNewLine(node, state);
	renderChildren(node, state);
	state.output += `</${tag}>`;
	endNewLine(node, state);
}
