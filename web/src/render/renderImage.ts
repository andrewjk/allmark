import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import { endNewLine, startNewLine } from "./utils";

const renderer: Renderer = {
	name: "image",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	startNewLine(node, state);
	let alt = getChildText(node);
	let title = node.title ? ` title="${node.title}"` : "";
	state.output += `<img src="${node.info}" alt="${alt}"${title} />`;
	endNewLine(node, state);
}

function getChildText(node: MarkdownNode): string {
	let text = "";
	if (node.children) {
		for (let child of node.children) {
			if (child.type === "text") {
				text += child.markup;
			} else {
				text += getChildText(child);
			}
		}
	}
	return text;
}
