import Foundation

@MainActor
let consoleLinkRenderer = Renderer(
	name: "link",
	render: renderConsoleLink
)

@MainActor
func renderConsoleLink(_ node: MarkdownNode, _ state: inout RendererState, _ first: Bool?, _ last: Bool?, _ decode: Bool?) {
	let styles = getConsoleStyles()
	let style = styles["link"] ?? ""
	state.output += style
	renderChildren(node: node, state: &state)
	if let info = node.info {
		state.output += "\(ansiReset) (\(info))"
	} else {
		state.output += ansiReset
	}
}
