import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import renderChildren from "./renderChildren";
import { endNewLine, startNewLine } from "./renderUtils";

const renderer: Renderer = {
	name: "link",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	startNewLine(node, state);
	let title = node.title ? ` title="${node.title}"` : "";
	state.output += `<a href="${node.info}"${title}>`;
	renderChildren(node, state);
	state.output += "</a>";
	endNewLine(node, state);
}
