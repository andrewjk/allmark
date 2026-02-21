import Testing
@testable import allmark

@Test func renderConsoleParagraph() async {
	let input = "Hello, world!"

	await MainActor.run {
		let root = parse(src: input, rules: coreRuleSet, debug: false)
		let output = renderToConsole(doc: root, rules: coreRuleSet)
		#expect(output == "Hello, world!")
	}
}

@Test func renderConsoleHeading() async {
	let input = "# Heading 1\n## Heading 2"

	await MainActor.run {
		let root = parse(src: input, rules: coreRuleSet, debug: false)
		let output = renderToConsole(doc: root, rules: coreRuleSet)
		#expect(output.contains("# Heading 1"))
		#expect(output.contains("## Heading 2"))
	}
}

@Test func renderConsoleBulletedList() async {
	let input = "- Item 1\n- Item 2"

	await MainActor.run {
		let root = parse(src: input, rules: coreRuleSet, debug: false)
		let output = renderToConsole(doc: root, rules: coreRuleSet)
		#expect(output.contains("• Item 1"))
		#expect(output.contains("• Item 2"))
	}
}

@Test func renderConsoleOrderedList() async {
	let input = "1. First\n2. Second"

	await MainActor.run {
		let root = parse(src: input, rules: coreRuleSet, debug: false)
		let output = renderToConsole(doc: root, rules: coreRuleSet)
		#expect(output.contains("1. First"))
		#expect(output.contains("2. Second"))
	}
}

@Test func renderConsoleCodeBlock() async {
	let input = "```\ncode\n```"

	await MainActor.run {
		let root = parse(src: input, rules: coreRuleSet, debug: false)
		let output = renderToConsole(doc: root, rules: coreRuleSet)
		#expect(output.contains("┌─"))
		#expect(output.contains("│"))
		#expect(output.contains("└─"))
	}
}

@Test func renderConsoleInlineCode() async {
	let input = "`code`"

	await MainActor.run {
		let root = parse(src: input, rules: coreRuleSet, debug: false)
		let output = renderToConsole(doc: root, rules: coreRuleSet)
		#expect(output.contains("`code`"))
	}
}

@Test func renderConsoleBlockQuote() async {
	let input = "> Quote text"
 
	await MainActor.run {
		let root = parse(src: input, rules: coreRuleSet, debug: false)
		let output = renderToConsole(doc: root, rules: coreRuleSet)
		var stripped = ""
		var i = output.startIndex
		while i < output.endIndex {
			let char = output[i]
			if char == "\u{001B}" {
				let j = output.index(i, offsetBy: 1)
				if j < output.endIndex && output[j] == "[" {
					var k = output.index(j, offsetBy: 1)
					while k < output.endIndex {
						let nextChar = output[k]
						if nextChar == "m" {
							i = output.index(k, offsetBy: 1)
							break
						}
						if nextChar != ";" && !("0"..."9").contains(nextChar) {
							i = k
							break
						}
						k = output.index(k, offsetBy: 1)
					}
					continue
				}
			}
			stripped.append(char)
			i = output.index(i, offsetBy: 1)
		}
		#expect(stripped.contains("┃ Quote text"))
	}
}

@Test func renderConsoleThematicBreak() async {
	let input = "---"

	await MainActor.run {
		let root = parse(src: input, rules: coreRuleSet, debug: false)
		let output = renderToConsole(doc: root, rules: coreRuleSet)
		#expect(output.contains("─"))
	}
}

@Test func renderConsoleTaskList() async {
	let input = "- [x] Done\n- [ ] Todo"
 
	await MainActor.run {
		let root = parse(src: input, rules: gfmRuleSet, debug: false)
		let output = renderToConsole(doc: root, rules: gfmRuleSet)
		#expect(output.contains("[✓]"))
		#expect(output.contains("[ ]"))
	}
}

@Test func renderConsoleTable() async {
	let input = "| A | B |\n|---|---|\n| 1 | 2 |"

	await MainActor.run {
		let root = parse(src: input, rules: gfmRuleSet, debug: false)
		let output = renderToConsole(doc: root, rules: gfmRuleSet)
		#expect(output.contains("┌"))
		#expect(output.contains("┬"))
		#expect(output.contains("┐"))
		#expect(output.contains("┼"))
		#expect(output.contains("│"))
		#expect(output.contains("├"))
		#expect(output.contains("┤"))
		#expect(output.contains("└"))
		#expect(output.contains("┴"))
		#expect(output.contains("┘"))
	}
}

@Test func renderConsoleStrongText() async {
	let input = "**bold**"

	await MainActor.run {
		let root = parse(src: input, rules: coreRuleSet, debug: false)
		let output = renderToConsole(doc: root, rules: coreRuleSet)
		#expect(output.contains("bold"))
	}
}

@Test func renderConsoleEmphasisText() async {
	let input = "*italic*"

	await MainActor.run {
		let root = parse(src: input, rules: coreRuleSet, debug: false)
		let output = renderToConsole(doc: root, rules: coreRuleSet)
		#expect(output.contains("italic"))
	}
}

@Test func renderConsoleLink() async {
	let input = "[text](url)"

	await MainActor.run {
		let root = parse(src: input, rules: coreRuleSet, debug: false)
		let output = renderToConsole(doc: root, rules: coreRuleSet)
		#expect(output.contains("text"))
		#expect(output.contains("url"))
	}
}

@Test func renderConsoleImage() async {
	let input = "![alt](url)"

	await MainActor.run {
		let root = parse(src: input, rules: coreRuleSet, debug: false)
		let output = renderToConsole(doc: root, rules: coreRuleSet)
		#expect(output.contains("Image"))
	}
}

@Test func renderConsoleStrikethrough() async {
	let input = "~~deleted~~"

	await MainActor.run {
		let root = parse(src: input, rules: gfmRuleSet, debug: false)
		let output = renderToConsole(doc: root, rules: gfmRuleSet)
		#expect(output.contains("~~deleted~~"))
	}
}

@Test func renderConsoleAlert() async {
	let input = "> [!NOTE]\n> Note content"

	await MainActor.run {
		let root = parse(src: input, rules: gfmRuleSet, debug: false)
		let output = renderToConsole(doc: root, rules: gfmRuleSet)
		#expect(output.contains("📝"))
		#expect(output.contains("Note:"))
	}
}

@Test func renderConsoleNestedList() async {
	let input = "- Level 1\n  - Level 2\n    - Level 3"

	await MainActor.run {
		let root = parse(src: input, rules: coreRuleSet, debug: false)
		let output = renderToConsole(doc: root, rules: coreRuleSet)
		#expect(output.contains("• Level 1"))
		#expect(output.contains("◦ Level 2"))
	}
}
