import Foundation

@MainActor
let consoleHighlightRenderer = Renderer(
	name: "highlight",
	render: renderConsoleHighlight
)

@MainActor
func renderConsoleHighlight(_ node: MarkdownNode, _ state: inout RendererState, _ first: Bool?, _ last: Bool?, _ decode: Bool?) {
	state.output += "\u{001B}[43m\u{001B}[30m"
	renderChildren(node: node, state: &state)
	state.output += "\u{001B}[0m"
}
