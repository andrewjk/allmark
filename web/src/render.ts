import renderChildren from "./render/renderChildren";
import htmlRenderers from "./rulesets/htmlRenderers";
import type MarkdownNode from "./types/MarkdownNode";
import type Renderer from "./types/Renderer";
import type RendererState from "./types/RendererState";

export default function render(doc: MarkdownNode, renderers?: Map<string, Renderer>): string {
	renderers ??= htmlRenderers;

	let state: RendererState = {
		renderers,
		output: "",
		footnotes: [],
		listDepth: 0,
	};

	renderChildren(doc, state);

	if (state.footnotes.length > 0 && renderers.has("footnote_list")) {
		const footnoteListRenderer = renderers.get("footnote_list");
		footnoteListRenderer!.render(doc, state);
	}

	if (state.output.length > 0) {
		state.output = state.output.replace(/\n*$/, "\n");
	}

	return state.output;
}
