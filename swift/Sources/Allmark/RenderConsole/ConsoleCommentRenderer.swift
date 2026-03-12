import Foundation


let consoleCommentRenderer = Renderer(
	name: "comment",
	render: renderConsoleComment
)


func renderConsoleComment(_ node: MarkdownNode, _ state: inout RendererState, _ first: Bool?, _ last: Bool?, _ decode: Bool?) {
	state.output += "<!--\(node.content)-->"
}
