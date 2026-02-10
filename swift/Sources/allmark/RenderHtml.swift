import Foundation
import Collections

@MainActor
func renderHtml(doc: MarkdownNode, renderers: OrderedDictionary<String, Renderer>) -> String {
	var state = RendererState(
		renderers: renderers,
		output: "",
		footnotes: []
	)

	renderChildren(node: doc, state: &state)

	if !state.footnotes.isEmpty {
		renderFootnoteList(state: &state)
	}

	if !state.output.hasSuffix("\n") {
		state.output += "\n"
	}

	return state.output
}
