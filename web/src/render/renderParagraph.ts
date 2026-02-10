import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import renderChildren from "./renderChildren";
import { endNewLine, innerNewLine, startNewLine } from "./utils";

const renderer: Renderer = {
	name: "paragraph",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	startNewLine(node, state);
	state.output += "<p>";
	innerNewLine(node, state);
	renderChildren(node, state);
	state.output += "</p>";
	endNewLine(node, state);
}
