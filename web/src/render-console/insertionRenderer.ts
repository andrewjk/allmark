import type ConsoleRendererState from "../types/ConsoleRendererState";
import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import renderChildren from "./renderChildren";

const renderer: Renderer = {
	name: "insertion",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	const style = "\x1b[32m";
	const reset = "\x1b[0m";
	renderNode(node, state, style, reset);
}

function renderNode(node: MarkdownNode, state: RendererState, style: string, reset: string): void {
	const s = state as ConsoleRendererState;
	s.output += `${style}++`;
	renderChildren(node, state);
	s.output += `++${reset}`;
}
