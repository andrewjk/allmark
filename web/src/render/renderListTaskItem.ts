import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import isSpace from "../utils/isSpace";

const renderer: Renderer = {
	name: "list_task_item",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	let checked = !isSpace(node.markup.charCodeAt(1)) ? ` checked=""` : ``;
	state.output += `<input type="checkbox"${checked} disabled="" /> `;
}
