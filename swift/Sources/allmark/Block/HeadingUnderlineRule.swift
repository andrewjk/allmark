import Foundation

/// A setext heading consists of one or more lines of text, each containing at
/// least one non-whitespace character, with no more than 3 spaces indentation,
/// followed by a setext heading underline.
@MainActor
let headingUnderlineRule = BlockRule(
	name: "heading_underline",
	testStart: testHeadingUnderlineStart,
	testContinue: testHeadingUnderlineContinue,
	closeNode: { _, _ in }
)

func testHeadingUnderlineStart(state: inout BlockParserState, parent: MarkdownNode) -> Bool {
	if state.maybeContinue {
		var i = state.openNodes.count - 1
		while i > 0 {
			let node = state.openNodes[i]
			if node.maybeContinuing {
				return false
			}
			i -= 1
		}
	}
	
	let src = state.src
	if state.i >= src.count {
		return false
	}
	
	let index = src.index(src.startIndex, offsetBy: state.i)
	let char = src[index]
	
	if state.indent <= 3 && (char == "=" || char == "-") {
		var matched = 1
		var end = state.i + 1
		
		while end < src.count {
			let endIndex = src.index(src.startIndex, offsetBy: end)
			let nextChar = src[endIndex]
			
			if nextChar == char {
				// The setext heading underline cannot contain internal spaces
				if matched > 0 && end > 0 {
					let prevIndex = src.index(src.startIndex, offsetBy: end - 1)
					if isSpace(code: Int(src[prevIndex].asciiValue ?? 0)) {
						return false
					}
				}
				matched += 1
			} else if isNewLine(char: String(nextChar)) {
				end += 1
				break
			} else if isSpace(code: Int(nextChar.asciiValue ?? 0)) {
				// continue
			} else {
				return false
			}
			end += 1
		}
		
		// HACK: Special case for an underlined heading in a list
		var currentParent = parent
		if currentParent.type == "list_item" && !currentParent.blankAfter && state.indent == currentParent.indent {
			state.openNodes.removeLast()
			state.openNodes.removeLast()
			currentParent = state.openNodes.last!
		}
		
		let contentPattern = try! NSRegularExpression(pattern: "[^\\s]")
		let contentRange = NSRange(location: 0, length: currentParent.content.utf16.count)
		let haveParagraph = currentParent.type == "paragraph" && !currentParent.blankAfter && contentPattern.firstMatch(in: currentParent.content, options: [], range: contentRange) != nil
		
		if haveParagraph {
			currentParent.type = "heading"
			let markupStart = src.index(src.startIndex, offsetBy: state.i)
			let markupEnd = src.index(src.startIndex, offsetBy: end)
			currentParent.markup = String(src[markupStart..<markupEnd])
			state.i = end
			return true
		}
	}
	
	return false
}

func testHeadingUnderlineContinue(state: inout BlockParserState, node: MarkdownNode) -> Bool {
	return false
}
