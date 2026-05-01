import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import { forEachChild } from "../utils/nodeUtils";
import ANSI from "./ansi";
import renderChildren from "./renderChildren";

const renderer: Renderer = {
	name: "heading_underline",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	const style = ANSI.bold + ANSI.magenta;
	const reset = ANSI.reset;

	let level = 0;
	if (node.markup.includes("=")) {
		level = 1;
	} else if (node.markup.includes("-")) {
		level = 2;
	}

	const originalLength = state.output.length;
	renderChildren(node, state);
	const headingText = state.output.slice(originalLength);

	const plainTextLength = getPlainTextLength(node);
	const underlineChar = level === 1 ? "=" : "-";

	state.output = state.output.slice(0, originalLength);
	state.output += `${style}${headingText}${reset}\n`;
	state.output += `${ANSI.dim}${underlineChar.repeat(plainTextLength)}${reset}\n\n`;
}

function getPlainTextLength(node: MarkdownNode): number {
	if (node.type === "text") {
		return node.content.length || 0;
	}
	let length = 0;
	forEachChild(node, (child) => {
		length += getPlainTextLength(child);
	});
	return length;
}
