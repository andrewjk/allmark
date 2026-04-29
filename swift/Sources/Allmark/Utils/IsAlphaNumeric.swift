@inlinable func isAlpha(code: Int) -> Bool {
	return (code > 64 && code < 91) || (code > 96 && code < 123)
}

@inlinable func isNumeric(code: Int) -> Bool {
	return code > 47 && code < 58
}

@inlinable func isAlphaNumeric(code: Int) -> Bool {
	return isAlpha(code: code) || isNumeric(code: code)
}
