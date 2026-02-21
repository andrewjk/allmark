import renderFootnoteList from "./render/footnoteListRenderer";
import renderChildren from "./render/renderChildren";
import htmlRenderers from "./rulesets/renderers";
import type MarkdownNode from "./types/MarkdownNode";
import type Renderer from "./types/Renderer";
import type RendererState from "./types/RendererState";

export default function renderHtml(doc: MarkdownNode, renderers?: Map<string, Renderer>): string {
	renderers ??= htmlRenderers;

	let state: RendererState = {
		renderers,
		output: "",
		footnotes: [],
	};

	renderChildren(doc, state);

	if (state.footnotes.length > 0) {
		renderFootnoteList(state);
	}

	if (state.output.length && !state.output.endsWith("\n")) {
		state.output += "\n";
	}

	return state.output;
}
