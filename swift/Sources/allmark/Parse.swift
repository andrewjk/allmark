import Foundation

@MainActor
func parse(src: String, rules: RuleSet, debug: Bool = false) -> MarkdownNode {
	var document = MarkdownNode(
		type: "document",
		block: true,
		index: 0,
		line: 1,
		column: 1,
		markup: "",
		indent: 0,
		children: []
	)

	var state = BlockParserState(
		rules: rules.blocks,
		src: src,
		i: 0,
		line: 0,
		lineStart: 0,
		indent: 0,
		openNodes: [document],
		maybeContinue: false,
		hasBlankLine: false,
		refs: [:],
		footnotes: [:],
		debug: debug
	)

	while state.i < state.src.count {
		parseLine(state: &state)
	}

	parseBlockInlines(parent: &document, rules: rules.inlines, refs: state.refs, footnotes: state.footnotes)

	return document
}
