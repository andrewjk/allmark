import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import ANSI from "./ansi";

const renderer: Renderer = {
	name: "image",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	const style = ANSI.gray;
	const reset = ANSI.reset;
	let alt = "";
	if (node.children) {
		for (const child of node.children) {
			if (child.type === "text") {
				alt += child.content;
			}
		}
	}
	state.output += `${style}[Image: ${alt || node.info || ""}]${reset}`;
}
