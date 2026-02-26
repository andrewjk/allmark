import Foundation

@MainActor
let consoleStrongRenderer = Renderer(
	name: "strong",
	render: renderConsoleStrong
)

@MainActor
func renderConsoleStrong(_ node: MarkdownNode, _ state: inout RendererState, _ first: Bool?, _ last: Bool?, _ decode: Bool?) {
	let styles = getConsoleStyles()
	let style = styles["strong"] ?? ""
	state.output += style
	renderChildren(node: node, state: &state)
	state.output += ansiReset
}
