import type ConsoleRendererState from "../types/ConsoleRendererState";
import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import ANSI from "./ansi";
import renderChildren from "./renderChildren";

const renderer: Renderer = {
	name: "alert",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	const s = state as ConsoleRendererState;
	const reset = ANSI.reset;
	const type = node.markup?.toLowerCase() || "note";
	const styles: Record<string, string> = {
		note: ANSI.blue,
		tip: ANSI.green,
		important: ANSI.magenta,
		warning: ANSI.yellow,
		caution: ANSI.red,
	};
	const style = styles[type] || styles.note;
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
