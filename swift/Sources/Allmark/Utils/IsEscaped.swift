@inlinable func isEscaped(text: [Character], i: Int) -> Bool {
	if i == 0 {
		return false
	}
	return text[i - 1] == "\\" && (i <= 1 || text[i - 2] != "\\")
}

func isEscaped(text: String, i: Int) -> Bool {
	if i == 0 {
		return false
	}
	let chars = Array(text)
	return chars[i - 1] == "\\" && (i <= 1 || chars[i - 2] != "\\")
}
