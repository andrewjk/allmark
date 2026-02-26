import Foundation

@MainActor
let consoleCodeBlockRenderer = Renderer(
	name: "code_block",
	render: renderConsoleCodeBlock
)

@MainActor
func renderConsoleCodeBlock(_ node: MarkdownNode, _ state: inout RendererState, _ first: Bool?, _ last: Bool?, _ decode: Bool?) {
	let styles = getConsoleStyles()
	let style = styles["codeBlock"] ?? ""
	if !state.output.isEmpty && !state.output.hasSuffix("\n") {
		state.output += "\n"
	}
	state.output += "\(style)┌─\(ansiReset)\n"
	for line in node.content.split(separator: "\n", omittingEmptySubsequences: false) {
		state.output += "\(style)│\(ansiReset) \(line)\n"
	}
	state.output += "\(style)└─\(ansiReset)\n"
}
