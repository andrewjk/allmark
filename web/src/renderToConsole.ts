import renderChildren from "./render/renderChildren";
import consoleRenderers from "./rulesets/consoleRenderers";
import type ConsoleRendererState from "./types/ConsoleRendererState";
import type MarkdownNode from "./types/MarkdownNode";
import type Renderer from "./types/Renderer";

export default function renderToConsole(
	doc: MarkdownNode,
	renderers?: Map<string, Renderer>,
): string {
	renderers ??= consoleRenderers;

	let state: ConsoleRendererState = {
		renderers: consoleRenderers,
		output: "",
		footnotes: [],
		depth: 0,
		quoteDepth: 0,
	};

	renderChildren(doc, state);

	state.output = state.output.replace(/\n+$/, "");

	return state.output;
}
