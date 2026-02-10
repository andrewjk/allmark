import Foundation

@MainActor
func renderFootnoteList(state: inout RendererState) {
	state.output += "<section class=\"footnotes\">\n<ol>\n"
	var number = 1
	for node in state.footnotes {
		let label = number
		number += 1
		let id = "fn\(label)"
		let href = "#fnref\(label)"
		state.output += "<li id=\"\(id)\">"
		renderChildren(node: node, state: &state)
		if state.output.hasSuffix("</p>\n") {
			state.output = String(state.output.dropLast(5))
		}
		state.output += " <a href=\"\(href)\" class=\"footnote-backref\">↩</a></p>\n</li>\n"
	}
	state.output += "</ol>\n</section>"
}
