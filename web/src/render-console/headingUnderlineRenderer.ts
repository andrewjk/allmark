import type ConsoleRendererState from "../types/ConsoleRendererState";
import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import renderChildren from "./renderChildren";
import type { Styles } from "./ansi";

const renderer: Renderer = {
	name: "heading_underline",
	render,
};
export default renderer;

export function createRenderer(styles: Styles): Renderer {
	return {
		name: "heading_underline",
		render(node: MarkdownNode, state: RendererState) {
			renderNode(node, state, styles);
		},
	};
}

function render(node: MarkdownNode, state: RendererState): void {
	const styles = defaultStyles();
	renderNode(node, state, styles);
}

function renderNode(node: MarkdownNode, state: RendererState, styles: Styles): void {
	const s = state as ConsoleRendererState;
	let level = 0;
	if (node.markup.includes("=")) {
		level = 1;
	} else if (node.markup.includes("-")) {
		level = 2;
	}

	const style = (styles[`heading${level}` as keyof typeof styles] as string) || "";
	if (s.output.length && !s.output.endsWith("\n")) {
		s.output += "\n";
	}

	const originalLength = s.output.length;
	renderChildren(node, state);
	const headingText = s.output.slice(originalLength);

	const plainTextLength = getPlainTextLength(node);
	const underlineChar = level === 1 ? "=" : "-";

	s.output = s.output.slice(0, originalLength);
	s.output += `${style}${headingText}${styles.reset}\n`;
	s.output += `${style}${underlineChar.repeat(plainTextLength)}${styles.reset}\n`;
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

function defaultStyles(): Styles {
	return {
		heading1: "\x1b[1m\x1b[36m",
		heading2: "\x1b[1m\x1b[34m",
		heading3: "\x1b[1m\x1b[35m",
		heading4: "\x1b[1m",
		heading5: "\x1b[2m\x1b[1m",
		heading6: "\x1b[2m\x1b[1m",
		strong: "",
		emphasis: "",
		code: "",
		link: "",
		blockQuote: "",
		codeBlock: "",
		thematicBreak: "",
		alertNote: "",
		alertTip: "",
		alertImportant: "",
		alertWarning: "",
		alertCaution: "",
		reset: "\x1b[0m",
	};
}
