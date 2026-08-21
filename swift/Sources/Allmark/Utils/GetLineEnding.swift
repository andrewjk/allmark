import Foundation

func getLineEnding(state: BlockParserState, endOfLine: Int) -> String {
	if endOfLine < state.src.count {
		let char = state.src[endOfLine]
		if isNewLine(char: char) {
			return String(char)
		}
	}
	return ""
}
