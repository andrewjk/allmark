import Foundation

@inlinable func charToString(_ chars: [Character], from: Int, to: Int) -> String {
	var result = ""
	result.reserveCapacity(to - from)
	var i = from
	while i < to {
		result.append(chars[i])
		i += 1
	}
	return result
}

@inlinable func charToString(_ chars: [Character], from: Int) -> String {
	return charToString(chars, from: from, to: chars.count)
}

/// The index of the end of the line starting at `from` (not including the newline).
@inlinable func endOfLineIndex(_ chars: [Character], _ from: Int) -> Int {
	var end = from
	while end < chars.count, !isNewLine(char: chars[end]) {
		end += 1
	}
	return end
}

@inlinable func charToString(_ string: String, from: Int, to: Int) -> String {
	let start = string.index(string.startIndex, offsetBy: from)
	let end = string.index(string.startIndex, offsetBy: to)
	return String(string[start ..< end])
}

/// Removes trailing ASCII whitespace from a string (equivalent to `\s+$`).
func trimTrailingWhitespace(_ text: String) -> String {
	var end = text.endIndex
	while end > text.startIndex {
		let prev = text.index(before: end)
		let char = text[prev]
		if char == " " || char == "\t" || char == "\n" || char == "\r\n" || char == "\r" || char == "\u{0B}" || char == "\u{0C}" {
			end = prev
		} else {
			break
		}
	}
	return String(text[..<end])
}
