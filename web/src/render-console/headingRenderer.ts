import type ConsoleRendererState from "../types/ConsoleRendererState";
import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import ANSI from "./ansi";
import renderChildren from "./renderChildren";

const renderer: Renderer = {
	name: "heading",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	const s = state as ConsoleRendererState;
	const style = ANSI.bold + ANSI.magenta;
	const reset = ANSI.reset;

	let level = 0;
	const isSetext = node.markup.includes("=") || node.markup.includes("-");
	if (node.markup.startsWith("#")) {
		level = node.markup.length;
	} else if (isSetext) {
		if (node.markup.includes("=")) {
			level = 1;
		} else {
			level = 2;
		}
	}

	if (s.output.length && !s.output.endsWith("\n")) {
		s.output += "\n";
	}

	if (isSetext) {
		const plainTextLength = getPlainTextLength(node);
		const underlineChar = level === 1 ? "=" : "-";
		s.output += `${style}`;
		renderChildren(node, state);
		s.output += `\n${reset}${ANSI.dim}${underlineChar.repeat(plainTextLength)}${reset}\n`;
	} else {
		s.output += `${ANSI.dim}${"#".repeat(level)}${reset} ${style}`;
		renderChildren(node, state);
		s.output += `${reset}\n`;
	}
}

function getPlainTextLength(node: MarkdownNode): number {
	if (node.type === "text") {
		return node.markup?.length || 0;
	}
	if (node.children) {
		return node.children.reduce((sum, child) => sum + getPlainTextLength(child), 0);
	}
	return 0;
}
