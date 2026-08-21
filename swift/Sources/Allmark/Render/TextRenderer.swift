import Foundation

let textRenderer = Renderer(
	name: "text",
	render: renderText
)

func renderText(_ node: MarkdownNode, _ state: inout RendererState, _ decode: Bool?) {
	let content = node.content
	let scanDecode = decode == true

	// Fast path: if none of the special characters are present, output as-is
	let needsProcessing = content.utf8.contains { byte in
		byte == 38 /* & */ || byte == 60 /* < */ || byte == 62 /* > */ || byte == 34 /* " */
			|| (scanDecode && byte == 92 /* \ */ )
	}
	if !needsProcessing {
		state.output += content
		return
	}

	var processed = content
	if scanDecode {
		processed = decodeEntities(text: processed)
		processed = escapePunctuation(text: processed)
	}
	processed = escapeHtml(text: processed)
	state.output += processed
}
