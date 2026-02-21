import Foundation
import Collections

let ansiReset = "\u{001B}[0m"
let ansiBold = "\u{001B}[1m"
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

@MainActor
func renderToConsole(doc: MarkdownNode, rules: RuleSet) -> String {
	let consoleRenderers = getConsoleRenderers(htmlRenderers: rules.renderers)

	var state = RendererState(
		renderers: consoleRenderers,
		output: "",
		footnotes: [],
		depth: 0,
		quoteDepth: 0
	)

	renderChildren(node: doc, state: &state)
 
	state.output = state.output.trimmingCharacters(in: .newlines)
 
	return state.output
}

@MainActor
private func getConsoleRenderers(htmlRenderers: OrderedDictionary<String, Renderer>) -> OrderedDictionary<String, Renderer> {
	var consoleRenderers = OrderedDictionary<String, Renderer>()

	let styles = [
		"heading1": "\(ansiBold)\(ansiCyan)",
		"heading2": "\(ansiBold)\(ansiBlue)",
		"heading3": "\(ansiBold)\(ansiMagenta)",
		"heading4": "\(ansiBold)",
		"heading5": "\(ansiDim)\(ansiBold)",
		"heading6": "\(ansiDim)\(ansiBold)",
		"strong": "\(ansiBold)\(ansiOrange)",
		"emphasis": ansiYellow,
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

	consoleRenderers["paragraph"] = Renderer(name: "paragraph", render: { node, state, _, _, _ in renderConsoleParagraph(node, &state) })
	consoleRenderers["heading"] = Renderer(name: "heading", render: { node, state, _, _, _ in renderConsoleHeading(node, &state, styles) })
	consoleRenderers["heading_underline"] = Renderer(name: "heading_underline", render: { node, state, _, _, _ in renderConsoleHeading(node, &state, styles) })
	consoleRenderers["thematic_break"] = Renderer(name: "thematic_break", render: { node, state, _, _, _ in renderConsoleThematicBreak(node, &state, styles["thematicBreak"] ?? "") })
	consoleRenderers["block_quote"] = Renderer(name: "block_quote", render: { node, state, _, _, _ in renderConsoleBlockQuote(node, &state, styles["blockQuote"] ?? "") })
	consoleRenderers["list_bulleted"] = Renderer(name: "list_bulleted", render: { node, state, _, _, _ in renderConsoleList(node, &state, ordered: false) })
	consoleRenderers["list_ordered"] = Renderer(name: "list_ordered", render: { node, state, _, _, _ in renderConsoleList(node, &state, ordered: true) })
	consoleRenderers["list_task_item"] = Renderer(name: "list_task_item", render: renderConsoleTaskItem)
	consoleRenderers["code_block"] = Renderer(name: "code_block", render: { node, state, _, _, _ in renderConsoleCodeBlock(node, &state, styles["codeBlock"] ?? "") })
	consoleRenderers["code_fence"] = Renderer(name: "code_fence", render: { node, state, _, _, _ in renderConsoleCodeBlock(node, &state, styles["codeBlock"] ?? "") })
	consoleRenderers["code_span"] = Renderer(name: "code_span", render: { node, state, _, _, _ in renderConsoleCodeSpan(node, &state, styles["code"] ?? "") })
	consoleRenderers["strong"] = Renderer(name: "strong", render: { node, state, _, _, _ in renderConsoleInline(node, &state, style: styles["strong"] ?? "") })
	consoleRenderers["emphasis"] = Renderer(name: "emphasis", render: { node, state, _, _, _ in renderConsoleInline(node, &state, style: styles["emphasis"] ?? "") })
	consoleRenderers["strikethrough"] = Renderer(name: "strikethrough", render: { node, state, _, _, _ in renderConsoleStrikethrough(node, &state, style: ansiDim) })
	consoleRenderers["link"] = Renderer(name: "link", render: { node, state, _, _, _ in renderConsoleLink(node, &state, style: styles["link"] ?? "") })
	consoleRenderers["image"] = Renderer(name: "image", render: { node, state, _, _, _ in renderConsoleImage(node, &state, style: ansiDim) })
	consoleRenderers["text"] = Renderer(name: "text", render: renderConsoleText)
	consoleRenderers["hard_break"] = Renderer(name: "hard_break", render: renderConsoleHardBreak)
	consoleRenderers["alert"] = Renderer(name: "alert", render: { node, state, _, _, _ in renderConsoleAlert(node, &state, styles) })
	consoleRenderers["footnote"] = Renderer(name: "footnote", render: { node, state, _, _, _ in renderConsoleFootnote(node, &state, style: ansiDim) })
	consoleRenderers["table"] = Renderer(name: "table", render: { node, state, _, _, _ in renderConsoleTable(node, &state, style: ansiDim) })
	consoleRenderers["html_block"] = Renderer(name: "html_block", render: renderConsoleHtml)
	consoleRenderers["html_span"] = Renderer(name: "html_span", render: renderConsoleHtml)

	return consoleRenderers
}

@MainActor
private func renderConsoleParagraph(_ node: MarkdownNode, _ state: inout RendererState) {
	if !state.output.isEmpty && !state.output.hasSuffix("\n") {
		state.output += "\n"
	}
	let hasDoubleNewline = state.output.count >= 2 && state.output.suffix(2) == "\n\n"
	if !state.output.isEmpty && !hasDoubleNewline {
		state.output += "\n"
	}
	renderChildren(node: node, state: &state)
	state.output += "\n\n"
}

@MainActor
private func renderConsoleHeading(_ node: MarkdownNode, _ state: inout RendererState, _ styles: [String: String]) {
	var level = 0
	if node.markup.hasPrefix("#") {
		level = node.markup.count
	} else if node.markup.contains("=") {
		level = 1
	} else if node.markup.contains("-") {
		level = 2
	}

	let style = styles["heading\(level)"] ?? ""
	if !state.output.isEmpty && !state.output.hasSuffix("\n") {
		state.output += "\n"
	}
	state.output += "\(style)\(String(repeating: "#", count: level)) "
	renderChildren(node: node, state: &state)
	state.output += "\(ansiReset)\n"
}

@MainActor
private func renderConsoleThematicBreak(_ node: MarkdownNode, _ state: inout RendererState, _ style: String) {
	if !state.output.isEmpty && !state.output.hasSuffix("\n") {
		state.output += "\n"
	}
	state.output += "\(style)─\(ansiReset)\n"
}

@MainActor
private func renderConsoleBlockQuote(_ node: MarkdownNode, _ state: inout RendererState, _ style: String) {
	state.quoteDepth += 1
	if !state.output.isEmpty && !state.output.hasSuffix("\n") {
		state.output += "\n"
	}
	for line in node.content.split(separator: "\n", omittingEmptySubsequences: false) {
		if !line.isEmpty {
			state.output += "\(style)┃\(ansiReset) \(line)\n"
		}
	}
	if let children = node.children {
		for child in children {
			let lines = renderNodeToStringConsole(node: child, state: &state)
			for line in lines.split(separator: "\n", omittingEmptySubsequences: false) {
				if !line.isEmpty {
					state.output += "\(style)┃\(ansiReset) \(line)\n"
				}
			}
		}
	}
	state.quoteDepth -= 1
}

@MainActor
private func renderNodeToStringConsole(node: MarkdownNode, state: inout RendererState) -> String {
	let output = state.output
	state.output = ""
	if let renderer = state.renderers[node.type] {
		renderer.render(node, &state, false, false, true)
	}
	let result = state.output
	state.output = output
	return result
}

@MainActor
private func renderConsoleList(_ node: MarkdownNode, _ state: inout RendererState, ordered: Bool) {
	state.depth += 1

	let loose = isLooseList(node: node)

	var counter = 1
	if ordered && !node.markup.isEmpty {
		let digits = node.markup.prefix(while: { $0.isNumber })
		if let num = Int(digits) {
			counter = num
		}
	}

	if let children = node.children {
		for item in children {
			let prefix: String
			if ordered {
				prefix = "\(counter)."
				counter += 1
			} else {
				prefix = consoleBullets[min(state.depth - 1, consoleBullets.count - 1)]
			}

			if let itemChildren = item.children {
				for (i, child) in itemChildren.enumerated() {
					if !loose && child.type == "paragraph" {
						let indent = String(repeating: "  ", count: state.depth - 1)
						if i == 0 {
							state.output += "\(indent)\(prefix) "
						}
						renderChildren(node: child, state: &state)
						state.output += "\n"
					} else {
						let indent = String(repeating: "  ", count: state.depth - 1)
						if i == 0 {
							state.output += "\(indent)\(prefix) "
						}
						if let renderer = state.renderers[child.type] {
							renderer.render(child, &state, false, false, true)
						}
					}
				}
			}
		}
	}

	state.depth -= 1
}

@MainActor
private func isLooseList(node: MarkdownNode) -> Bool {
	if let children = node.children {
		for i in 0..<(children.count - 1) {
			let child = children[i]
			if let grandchild = child.children?.last, grandchild.blankAfter {
				return true
			}
		}
	}
	return false
}

@MainActor
private func renderConsoleTaskItem(_ node: MarkdownNode, _ state: inout RendererState, _ first: Bool?, _ last: Bool?, _ decode: Bool?) {
	let isChecked = node.markup.count > 1 && node.markup[node.markup.index(node.markup.startIndex, offsetBy: 1)] != " "
	let emoji = isChecked ? "[✓]" : "[ ]"
	state.output += "\(emoji) "
}

@MainActor
private func renderConsoleCodeBlock(_ node: MarkdownNode, _ state: inout RendererState, _ style: String) {
	if !state.output.isEmpty && !state.output.hasSuffix("\n") {
		state.output += "\n"
	}
	state.output += "\(style)┌─\(ansiReset)\n"
	for line in node.content.split(separator: "\n", omittingEmptySubsequences: false) {
		state.output += "\(style)│\(ansiReset) \(line)\n"
	}
	state.output += "\(style)└─\(ansiReset)\n"
}

@MainActor
private func renderConsoleCodeSpan(_ node: MarkdownNode, _ state: inout RendererState, _ style: String) {
	state.output += style
	state.output += "`"
	renderChildren(node: node, state: &state)
	state.output += "`"
	state.output += ansiReset
}

@MainActor
private func renderConsoleInline(_ node: MarkdownNode, _ state: inout RendererState, style: String) {
	state.output += style
	renderChildren(node: node, state: &state)
	state.output += ansiReset
}

@MainActor
private func renderConsoleStrikethrough(_ node: MarkdownNode, _ state: inout RendererState, style: String) {
	state.output += "\(style)~~"
	renderChildren(node: node, state: &state)
	state.output += "~~\(ansiReset)"
}

@MainActor
private func renderConsoleLink(_ node: MarkdownNode, _ state: inout RendererState, style: String) {
	state.output += style
	renderChildren(node: node, state: &state)
	if let info = node.info {
		state.output += "\(ansiReset) (\(info))"
	} else {
		state.output += ansiReset
	}
}

@MainActor
private func renderConsoleImage(_ node: MarkdownNode, _ state: inout RendererState, style: String) {
	var alt = ""
	if let children = node.children {
		for child in children {
			if child.type == "text" {
				alt += child.markup
			}
		}
	}
	state.output += "\(style)[Image: \(alt.isEmpty ? (node.info ?? "") : alt)]\(ansiReset)"
}

@MainActor
private func renderConsoleText(_ node: MarkdownNode, _ state: inout RendererState, _ first: Bool?, _ last: Bool?, _ decode: Bool?) {
	var text = node.markup
	if first == true {
		text = text.trimmingCharacters(in: .whitespacesAndNewlines).trimmingCharacters(in: .newlines)
	}
	if last == true {
		text = text.trimmingCharacters(in: .whitespacesAndNewlines).trimmingCharacters(in: .newlines)
	}
	state.output += text
}

@MainActor
private func renderConsoleHardBreak(_ node: MarkdownNode, _ state: inout RendererState, _ first: Bool?, _ last: Bool?, _ decode: Bool?) {
	state.output += "\n"
}

@MainActor
private func renderConsoleAlert(_ node: MarkdownNode, _ state: inout RendererState, _ styles: [String: String]) {
	let type = node.markup.lowercased()
	let style = styles["alert\(type.capitalized)"] ?? styles["alertNote"] ?? ""
	let icons: [String: String] = [
		"note": "📝",
		"tip": "💡",
		"important": "❗",
		"warning": "⚠️",
		"caution": "🚨",
	]
	let icon = icons[type] ?? icons["note"]
	if !state.output.isEmpty && !state.output.hasSuffix("\n") {
		state.output += "\n"
	}
	state.output += "\(style)\(icon ?? "📝") \(type.capitalized):\(ansiReset)\n"
	renderChildren(node: node, state: &state)
}

@MainActor
private func renderConsoleFootnote(_ node: MarkdownNode, _ state: inout RendererState, style: String) {
	if state.footnotes.first(where: { $0.info == node.info }) == nil {
		state.footnotes.append(node)
	}
	let label = state.footnotes.count
	state.output += "\(style)[\(label)]\(ansiReset)"
}

@MainActor
private func renderConsoleTable(_ node: MarkdownNode, _ state: inout RendererState, style: String) {
	if !state.output.isEmpty && !state.output.hasSuffix("\n") {
		state.output += "\n"
	}

	guard let children = node.children, !children.isEmpty else { return }

	let headerRow = children[0]
	let dataRows = Array(children.dropFirst())

	let headerCells = headerRow.children ?? []
	var cellTexts: [[String]] = []

	let maxColumns = max(headerCells.count, dataRows.map { $0.children?.count ?? 0 }.max() ?? 0)
	var columnWidths = [Int](repeating: 0, count: maxColumns)

	for i in 0..<headerCells.count {
		let text = getTextFromNode(node: headerCells[i])
		if cellTexts.isEmpty {
			cellTexts.append([])
		}
		cellTexts[0].append(text)
		columnWidths[i] = max(columnWidths[i], text.count + 2)
	}

	for r in 0..<dataRows.count {
		let row = dataRows[r]
		let rowCells = row.children ?? []
		if cellTexts.count <= r {
			cellTexts.append([])
		}
		for c in 0..<rowCells.count {
			let text = getTextFromNode(node: rowCells[c])
			while cellTexts[r + 1].count <= c {
				cellTexts[r + 1].append("")
			}
			cellTexts[r + 1][c] = text
			if c < columnWidths.count {
				columnWidths[c] = max(columnWidths[c], text.count + 2)
			}
		}
	}

	func makeLine(left: String, mid: String, right: String, sep: String) -> String {
		var line = left
		for i in 0..<columnWidths.count {
			line += String(repeating: "─", count: columnWidths[i])
			if i < columnWidths.count - 1 {
				line += (i == 0) ? mid : sep
			}
		}
		line += right
		return "\(style)\(line)\(ansiReset)\n"
	}
 
	state.output += makeLine(left: "┌", mid: "┬", right: "┐", sep: "┬")
 
	if !headerCells.isEmpty {
		state.output += "\(style)│\(ansiReset)"
		for i in 0..<headerCells.count {
			let text = i < cellTexts[0].count ? cellTexts[0][i] : ""
			let padding = String(repeating: " ", count: columnWidths[i] - text.count - 1)
			state.output += " \(text)\(padding)\(style)│\(ansiReset)"
		}
		state.output += "\n"
	}

	state.output += makeLine(left: "├", mid: "┼", right: "┤", sep: "┼")

	for r in 0..<dataRows.count {
		state.output += "\(style)│\(ansiReset)"
		for c in 0..<columnWidths.count {
			let text = (r + 1) < cellTexts.count && c < cellTexts[r + 1].count ? cellTexts[r + 1][c] : ""
			let padding = String(repeating: " ", count: columnWidths[c] - text.count - 1)
			state.output += " \(text)\(padding)\(style)│\(ansiReset)"
		}
		state.output += "\n"
	}

	state.output += makeLine(left: "└", mid: "┴", right: "┘", sep: "┴")
}

@MainActor
private func getTextFromNode(node: MarkdownNode) -> String {
	if node.type == "text" {
		return node.markup
	}
	if let children = node.children {
		return children.map { getTextFromNode(node: $0) }.joined()
	}
	return ""
}

@MainActor
private func renderConsoleHtml(_ node: MarkdownNode, _ state: inout RendererState, _ first: Bool?, _ last: Bool?, _ decode: Bool?) {
	state.output += node.content
}
