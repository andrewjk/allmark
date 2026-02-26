import type ConsoleRendererState from "../types/ConsoleRendererState";
import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import type { Styles } from "./ansi";
import renderChildren from "./renderChildren";

const renderer: Renderer = {
	name: "alert",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	const styles = defaultStyles();
	const reset = "\x1b[0m";
	renderNode(node, state, styles, reset);
}

function renderNode(node: MarkdownNode, state: RendererState, styles: Styles, reset: string): void {
	const s = state as ConsoleRendererState;
	const type = node.markup?.toLowerCase() || "note";
	const style =
		(styles[
			`alert${type.charAt(0).toUpperCase() + type.slice(1)}` as keyof typeof styles
		] as string) || styles.alertNote;
	const icons: Record<string, string> = {
		note: "📝",
		tip: "💡",
		important: "❗",
		warning: "⚠️",
		caution: "🚨",
	};
	const icon = icons[type] || icons.note;
	if (s.output.length && !s.output.endsWith("\n")) {
		s.output += "\n";
	}
	s.output += `${style}${icon} ${type.charAt(0).toUpperCase() + type.slice(1)}:${reset}\n`;
	renderChildren(node, state);
}

function defaultStyles(): Styles {
	return {
		heading1: "",
		heading2: "",
		heading3: "",
		heading4: "",
		heading5: "",
		heading6: "",
		strong: "",
		emphasis: "",
		code: "",
		link: "",
		blockQuote: "",
		codeBlock: "",
		thematicBreak: "",
		alertNote: "\x1b[34m",
		alertTip: "\x1b[32m",
		alertImportant: "\x1b[35m",
		alertWarning: "\x1b[33m",
		alertCaution: "\x1b[31m",
		reset: "\x1b[0m",
	};
}
