import parseBlockInlines from "./parse/parseBlockInlines";
import { parseLine } from "./parse/parseLine";
import type BlockParserState from "./types/BlockParserState";
import type MarkdownNode from "./types/MarkdownNode";
import type RuleSet from "./types/RuleSet";
import newNode from "./utils/newNode";

export default function parse(src: string, rules: RuleSet, debug = false): MarkdownNode {
	let document = newNode("document", true, 0, 1, 1, "", 0, []);

	let state: BlockParserState = {
		rules: rules.blocks,
		src,
		i: 0,
		line: 0,
		lineStart: 0,
		indent: 0,
		maybeContinue: false,
		hasBlankLine: false,
		openNodes: [document],
		refs: {},
		debug,
	};

	// Stage 1 -- parse each line into blocks
	while (state.i < state.src.length) {
		parseLine(state);
	}

	// TODO: Close the open nodes?

	// Stage 2 -- parse the inlines for each block
	parseBlockInlines(document, rules.inlines, state.refs);

	return document;
}
