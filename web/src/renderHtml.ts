import renderFootnoteList from "./render/footnoteListRenderer";
import renderChildren from "./render/renderChildren";
import type MarkdownNode from "./types/MarkdownNode";
import type Renderer from "./types/Renderer";
import type RendererState from "./types/RendererState";

export default function renderHtml(doc: MarkdownNode, renderers: Map<string, Renderer>): string {
	let state: RendererState = {
		renderers,
		output: "",
		footnotes: [],
	};

	renderChildren(doc, state);

	if (state.footnotes.length > 0) {
		renderFootnoteList(state);
	}

	if (!state.output.endsWith("\n")) {
		state.output += "\n";
	}

	return state.output;
}
