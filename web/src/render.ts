import renderChildren from "./render/renderChildren";
import htmlRenderers from "./rulesets/htmlRenderers";
import type MarkdownNode from "./types/MarkdownNode";
import type Renderer from "./types/Renderer";
import type RendererState from "./types/RendererState";

export default function render(doc: MarkdownNode, renderers?: Renderer[]): string {
	renderers ??= htmlRenderers;

	let state: RendererState = {
		renderersMap: new Map(renderers.map((r) => [r.name, r])),
		output: "",
		footnotes: [],
		listDepth: 0,
	};

	renderChildren(doc, state);

	if (state.footnotes.length > 0 && state.renderersMap.has("footnote_list")) {
		const footnoteListRenderer = state.renderersMap.get("footnote_list");
		footnoteListRenderer!.render(doc, state);
	}

	if (state.output.length > 0) {
		state.output = state.output.replace(/\n*$/, "\n");
	}

	return state.output;
}
