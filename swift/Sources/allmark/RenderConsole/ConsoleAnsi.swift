import Foundation

let ansiReset = "\u{001B}[0m"
let ansiBold = "\u{001B}[1m"
let ansiItalic = "\u{001B}[3m"
let ansiDim = "\u{001B}[2m"
let ansiGray = "\u{001B}[90m"
let ansiRed = "\u{001B}[31m"
let ansiGreen = "\u{001B}[32m"
let ansiYellow = "\u{001B}[33m"
let ansiBlue = "\u{001B}[34m"
let ansiMagenta = "\u{001B}[35m"
let ansiCyan = "\u{001B}[36m"
let ansiOrange = "\u{001B}[38;5;208m"
let ansiUnderline = "\u{001B}[4m"

	let consoleBullets = ["•", "◦", "▪", "‣"]

func getConsoleStyles() -> [String: String] {
	return [
		"heading1": "\(ansiBold)\(ansiCyan)",
		"heading2": "\(ansiBold)\(ansiBlue)",
		"heading3": "\(ansiBold)\(ansiMagenta)",
		"heading4": "\(ansiBold)",
		"heading5": "\(ansiDim)\(ansiBold)",
		"heading6": "\(ansiDim)\(ansiBold)",
		"strong": "\(ansiBold)\(ansiYellow)",
		"emphasis": "\(ansiItalic)\(ansiYellow)",
		"code": ansiGreen,
		"link": "\(ansiBlue)\(ansiUnderline)",
		"blockQuote": ansiGray,
		"codeBlock": ansiDim,
		"thematicBreak": ansiDim,
		"alertNote": ansiBlue,
		"alertTip": ansiGreen,
		"alertImportant": ansiMagenta,
		"alertWarning": ansiYellow,
		"alertCaution": ansiRed,
	]
}
