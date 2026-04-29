import Foundation

func skipSpaces(text: [Character], start: Int) -> Int {
	var index = start
	while index < text.count {
		if !isSpace(code: Int(text[index].asciiValue ?? 0)) {
			break
		}
		index += 1
	}
	return index
}
