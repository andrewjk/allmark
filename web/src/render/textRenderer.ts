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
	const content = node.content;
	const scanDecode = decode === true;

	// Fast path: if none of the special characters are present, output as-is
	const needsProcessing = scanDecode ? /[&<>"\\]/.test(content) : /[&<>"]/.test(content);
	if (!needsProcessing) {
		state.output += content;
		return;
	}

	let result = content;
	if (scanDecode) {
		result = decodeEntities(result);
		result = escapePunctuation(result);
	}
	result = escapeHtml(result);
	state.output += result;
}
