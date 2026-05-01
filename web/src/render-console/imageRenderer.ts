import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import { forEachChild } from "../utils/nodeUtils";
import ANSI from "./ansi";

const renderer: Renderer = {
	name: "image",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	const style = ANSI.gray;
	const reset = ANSI.reset;
	let alt = getChildText(node);
	state.output += `${style}[Image: ${alt || node.info || ""}]${reset}\n`;
}

function getChildText(node: MarkdownNode): string {
	let text = "";
	forEachChild(node, (child) => {
		if (child.type === "text") {
			text += child.content;
		} else {
			text += getChildText(child);
		}
	});
	return text;
}
