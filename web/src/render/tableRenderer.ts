import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import { getChildren, getFirstChild } from "../utils/nodeUtils";
import renderChildren from "./renderChildren";
import { endNewLine, startNewLine } from "./renderUtils";

const renderer: Renderer = {
	name: "table",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	startNewLine(node, state);
	state.output += "<table>\n<thead>\n<tr>\n";
	let children = getChildren(node);
	if (children[0]) {
		let headerCells = getChildren(children[0]);
		for (let cell of headerCells) {
			renderTableCell(cell, state, "th");
		}
	}
	state.output += "</tr>\n</thead>\n";
	if (children.length > 1) {
		state.output += "<tbody>\n";
		for (let i = 1; i < children.length; i++) {
			let row = children[i];
			state.output += "<tr>\n";
			let cells = getChildren(row);
			for (let cell of cells) {
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
	let firstChild = getFirstChild(node);
	if (firstChild !== undefined) {
		renderChildren(firstChild, state);
	}
	state.output += `</${tag}>`;
	endNewLine(node, state);
}
