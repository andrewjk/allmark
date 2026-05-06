import Foundation

func getEndOfLine(state: inout BlockParserState) -> Int {
	var endOfLine = state.i
	while endOfLine < state.src.count {
		let char = state.src[endOfLine]
		if char == "\n" || char == "\r\n" || char == "\r" {
			endOfLine += 1
			state.lineStart = endOfLine
			break
		}
		endOfLine += 1
	}
	return endOfLine
}
