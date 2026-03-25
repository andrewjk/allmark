import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import decodeEntities from "../utils/decodeEntities";
import escapeHtml from "../utils/escapeHtml";
import escapePunctuation from "../utils/escapePunctuation";

const renderer: Renderer = {
	name: "text",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState, decode?: boolean): void {
	let content = node.content;
	if (decode === true) {
		content = decodeEntities(content);
		content = escapePunctuation(content);
	}
	content = escapeHtml(content);
	state.output += content;
}
