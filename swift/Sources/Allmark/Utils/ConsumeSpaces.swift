import Foundation

func consumeSpaces(text: [Character], i: Int) -> String {
	var result = ""
	var index = i
	while index < text.count {
		let char = text[index]
		if isSpace(code: Int(char.asciiValue ?? 0)) {
			result.append(char)
			index += 1
		} else {
			break
		}
	}
	return result
}
