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
	let markup = node.markup;
	if (decode === true) {
		markup = decodeEntities(markup);
		markup = escapePunctuation(markup);
	}
	markup = escapeHtml(markup);
	state.output += markup;
}
