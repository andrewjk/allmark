import parseBlockInlines from "./parse/parseBlockInlines";
import { parseLine } from "./parse/parseLine";
import type BlockParserState from "./types/BlockParserState";
import type MarkdownNode from "./types/MarkdownNode";
import type RuleSet from "./types/RuleSet";
import isNewLine from "./utils/isNewLine";
import isSpace from "./utils/isSpace";
import newNode from "./utils/newNode";

export default function parse(src: string, rules: RuleSet, debug = false): MarkdownNode {
	let document = newNode("document", true, 0, 1, 1, "", 0, []);

	// Skip empty lines at the start
	let i = 0;
	let index = 0;
	for (; index < src.length; index++) {
		if (!isSpace(src.charCodeAt(index))) {
			break;
		} else if (isNewLine(src[index])) {
			i = index + 1;
		}
	}

	let state: BlockParserState = {
		rules: rules.blocks,
		src,
		i,
		line: 0,
		lineStart: 0,
		indent: 0,
		maybeContinue: false,
		hasBlankLine: false,
		openNodes: [document],
		refs: {},
		footnotes: {},
		debug,
	};

	// Stage 1 -- parse each line into blocks
	while (state.i < state.src.length) {
		parseLine(state);
	}

	// TODO: Close the open nodes?

	// Stage 2 -- parse the inlines for each block
	parseBlockInlines(document, rules.inlines, state.refs, state.footnotes);

	return document;
}
