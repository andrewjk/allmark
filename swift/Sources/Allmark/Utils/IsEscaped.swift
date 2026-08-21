@inlinable func isEscaped(text: [Character], i: Int) -> Bool {
	if i == 0 {
		return false
	}
	return text[i - 1] == "\\" && (i <= 1 || text[i - 2] != "\\")
}

@inlinable func isEscaped(text: String, i: Int) -> Bool {
	if i == 0 {
		return false
	}
	return isEscaped(text: text, index: text.index(text.startIndex, offsetBy: i))
}

@inlinable func isEscaped(text: String, index: String.Index) -> Bool {
	if index == text.startIndex {
		return false
	}
	let prev = text.index(before: index)
	if text[prev] != "\\" {
		return false
	}
	if prev == text.startIndex {
		return true
	}
	return text[text.index(before: prev)] != "\\"
}
