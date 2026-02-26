import type ConsoleRendererState from "../types/ConsoleRendererState";
import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import type { Styles } from "./ansi";
import renderChildren from "./renderChildren";

const renderer: Renderer = {
	name: "heading",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	const styles = defaultStyles();
	renderNode(node, state, styles);
}

function renderNode(node: MarkdownNode, state: RendererState, styles: Styles): void {
	const s = state as ConsoleRendererState;
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

	const style = (styles[`heading${level}` as keyof typeof styles] as string) || "";
	if (s.output.length && !s.output.endsWith("\n")) {
		s.output += "\n";
	}

	if (isSetext) {
		const plainTextLength = getPlainTextLength(node);
		const underlineChar = level === 1 ? "=" : "-";
		s.output += `${style}`;
		renderChildren(node, state);
		s.output += `\n${underlineChar.repeat(plainTextLength)}${styles.reset}\n`;
	} else {
		s.output += `${style}${"#".repeat(level)} `;
		renderChildren(node, state);
		s.output += `${styles.reset}\n`;
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
