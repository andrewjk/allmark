import Foundation

func getLineEnding(state: BlockParserState, endOfLine: Int) -> String {
	if endOfLine < state.src.count {
		let char = state.src[endOfLine]
		if char == "\n" {
			return "\n"
		} else if char == "\r\n" {
			return "\r\n"
		} else if char == "\r" {
			return "\r"
		}
	}
	return ""
}
