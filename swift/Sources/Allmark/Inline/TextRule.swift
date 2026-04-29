import Foundation

let textRule = InlineRule(
	name: "text",
	test: testText
)

/**
 * The text inline rule handles any character that hasn't been handled by a
 * previous rule
 */
func testText(state: inout InlineParserState, parent: inout MarkdownNode) -> Bool {
	let src = state.src
	guard state.i < src.count else { return false }

	let char = src[state.i]

	let lastNode = parent.children.last
	if lastNode == nil || lastNode?.type != "text" {
		let newTextNode = newText(
			index: state.parentIndex + state.i,
			line: state.line,
			content: "",
			indent: 0
		)
		parent.children.append(newTextNode)
	} else if isNewLine(char: char) {
		if let last = lastNode {
			var content = last.content
			while content.last?.isWhitespace == true {
				content.removeLast()
			}
			last.content = content
			last.length = content.count
		}
	}

	let currentLast = parent.children.last!
	let code = Int(char.asciiValue ?? 0)
	if isAlphaNumeric(code: code) {
		let start = state.i
		state.i += 1
		while state.i < src.count {
			let nextCode = Int(src[state.i].asciiValue ?? 0)
			if isAlphaNumeric(code: nextCode) {
				state.i += 1
			} else {
				break
			}
		}
		currentLast.content += charToString(src, from: start, to: state.i)
	} else {
		state.i += 1
		currentLast.content += String(char)
	}

	currentLast.length = currentLast.content.count

	return true
}
