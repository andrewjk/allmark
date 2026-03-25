import Foundation

let textRenderer = Renderer(
	name: "text",
	render: renderText
)

func renderText(_ node: MarkdownNode, _ state: inout RendererState, _ decode: Bool?) {
	var markup = node.markup
	if decode == true {
		markup = decodeEntities(text: markup)
		markup = escapePunctuation(text: markup)
	}
	markup = escapeHtml(text: markup)
	state.output += markup
}
