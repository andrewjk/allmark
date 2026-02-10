import Foundation

@MainActor
let codeBlockRenderer = Renderer(
	name: "code_block",
	render: renderCodeBlock
)

@MainActor
func renderCodeBlock(_ node: MarkdownNode, _ state: inout RendererState, _ first: Bool?, _ last: Bool?, _ decode: Bool?) {
	startNewLine(node: node, state: &state)
	var lang = ""
	if let info = node.info {
		let langParts = info.trimmingCharacters(in: .whitespacesAndNewlines).split(separator: " ", maxSplits: 1)
		if (!langParts.isEmpty) {
			lang = " class=\"language-\(langParts[0])\""
		}
	}
	state.output += "<pre><code\(lang)>"
	renderChildren(node: node, state: &state, decode: false)
	state.output += "</code></pre>"
	endNewLine(node: node, state: &state)
}
