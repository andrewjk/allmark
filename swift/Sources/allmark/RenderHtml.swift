import Foundation
import Collections

@MainActor
func renderHtml(doc: MarkdownNode, renderers: OrderedDictionary<String, Renderer>) -> String {
	var state = RendererState(
		renderers: renderers,
		output: "",
		footnotes: [],
		depth: 0,
		quoteDepth: 0
	)

	renderChildren(node: doc, state: &state)

	if !state.footnotes.isEmpty {
		renderFootnoteList(state: &state)
	}

	if !state.output.isEmpty && !state.output.hasSuffix("\n") {
		state.output += "\n"
	}

	return state.output
}
