import Foundation

func addMarkupAsText(
	markup: String,
	state: inout InlineParserState,
	parent: inout MarkdownNode
) {
	let lastNode = parent.children?.last
	let haveText = lastNode?.type == "text"
	let text = haveText ? lastNode! : newInline(
		type: "text",
		index: state.i,
		line: state.line,
		markup: "",
		indent: 0
	)
	text.markup += markup
	if !haveText {
		parent.children?.append(text)
	}
	state.i += markup.count
}
