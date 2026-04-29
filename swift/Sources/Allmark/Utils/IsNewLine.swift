@inlinable func isNewLine(char: Character) -> Bool {
	return char == "\r" || char == "\n"
}

func isNewLine(char: String) -> Bool {
	return char == "\r" || char == "\n"
}
