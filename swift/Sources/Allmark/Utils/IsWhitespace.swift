@inlinable func isWhitespace(code: Character) -> Bool {
	switch code {
	case " ", "\t", "\n", "\r":
		return true
	case "\r\n":
		return true
	default:
		return false
	}
}

@inlinable func hasNonWhitespace(_ text: String) -> Bool {
	for char in text {
		if !isWhitespace(code: char) {
			return true
		}
	}
	return false
}
