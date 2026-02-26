import Foundation

@MainActor
let consoleCodeSpanRenderer = Renderer(
	name: "code_span",
	render: renderConsoleCodeSpan
)

@MainActor
func renderConsoleCodeSpan(_ node: MarkdownNode, _ state: inout RendererState, _ first: Bool?, _ last: Bool?, _ decode: Bool?) {
	let styles = getConsoleStyles()
	let style = styles["code"] ?? ""
	state.output += style
	state.output += "`"
	renderChildren(node: node, state: &state)
	state.output += "`"
	state.output += ansiReset
}
