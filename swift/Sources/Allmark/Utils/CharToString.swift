import Foundation

func charToString(_ chars: [Character], from: Int, to: Int) -> String {
	return String(chars[from ..< to])
}

func charToString(_ chars: [Character], from: Int) -> String {
	return String(chars[from...])
}

func charToString(_ chars: ArraySlice<Character>) -> String {
	return String(chars)
}
