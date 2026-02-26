import Foundation
import Collections

@MainActor
func renderToConsole(doc: MarkdownNode, renderers: OrderedDictionary<String, Renderer> = consoleRenderers) -> String {
	var state = RendererState(
		renderers: renderers,
		output: "",
		footnotes: [],
		depth: 0,
		quoteDepth: 0
	)

	renderChildren(node: doc, state: &state)

	state.output = state.output.trimmingCharacters(in: .newlines)

	return state.output
}
