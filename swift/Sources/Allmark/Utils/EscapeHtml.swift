import Foundation

func escapeHtml(text: String) -> String {
	// Fast path: nothing to escape, return the original string
	var needsEscaping = false
	for byte in text.utf8 {
		if byte == 38 || byte == 60 || byte == 62 || byte == 34 { // & < > "
			needsEscaping = true
			break
		}
	}
	if !needsEscaping {
		return text
	}

	var result = ""
	result.reserveCapacity(text.count + 4)
	for char in text {
		switch char {
		case "&":
			result.append("&amp;")
		case "<":
			result.append("&lt;")
		case ">":
			result.append("&gt;")
		case "\"":
			result.append("&quot;")
		default:
			result.append(char)
		}
	}
	return result
}
