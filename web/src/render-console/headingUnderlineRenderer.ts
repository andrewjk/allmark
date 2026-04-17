import type ConsoleRendererState from "../types/ConsoleRendererState";
import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import ANSI from "./ansi";
import renderChildren from "./renderChildren";

const renderer: Renderer = {
	name: "heading_underline",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	const s = state as ConsoleRendererState;
	const style = ANSI.bold + ANSI.magenta;
	const reset = ANSI.reset;

	let level = 0;
	if (node.markup.includes("=")) {
		level = 1;
	} else if (node.markup.includes("-")) {
		level = 2;
	}

	const originalLength = s.output.length;
	renderChildren(node, state);
	const headingText = s.output.slice(originalLength);

	const plainTextLength = getPlainTextLength(node);
	const underlineChar = level === 1 ? "=" : "-";

	s.output = s.output.slice(0, originalLength);
	s.output += `${style}${headingText}${reset}\n`;
	s.output += `${ANSI.dim}${underlineChar.repeat(plainTextLength)}${reset}\n\n`;
}

function getPlainTextLength(node: MarkdownNode): number {
	if (node.type === "text") {
		return node.content.length || 0;
	}
	if (node.children) {
		return node.children.reduce((sum, child) => sum + getPlainTextLength(child), 0);
	}
	return 0;
}
