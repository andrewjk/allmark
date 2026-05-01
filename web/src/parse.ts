import parseBlockInlines from "./parse/parseBlockInlines";
import { parseLine } from "./parse/parseLine";
import type BlockParserState from "./types/BlockParserState";
import type MarkdownNode from "./types/MarkdownNode";
import type RuleSet from "./types/RuleSet";
import isNewLine from "./utils/isNewLine";
import isSpace from "./utils/isSpace";
import newBlock from "./utils/newBlock";

export default function parse(src: string, rules: RuleSet): MarkdownNode {
	let document = newBlock("document", 0, 1, "", 0);
	document.depth = 0;

	// Skip empty lines at the start
	let i = 0;
	let index = 0;
	for (; index < src.length; index++) {
		let nextCharCode = src.charCodeAt(index);
		if (!isSpace(nextCharCode)) {
			break;
		} else if (isNewLine(nextCharCode)) {
			i = index + 1;
		}
	}

	let state: BlockParserState = {
		rules: rules.blocks,
		rulesMap: new Map(rules.blocks.map((b) => [b.name, b])),
		src,
		i,
		line: 0,
		lineStart: 0,
		indent: 0,
		isEscaped: false,
		maybeContinue: false,
		hasBlankLine: false,
		openNodes: [document],
		refs: {},
		footnotes: {},
	};

	// Stage 1 -- parse each line into blocks
	while (state.i < state.src.length) {
		parseLine(state);
	}

	// Close the remaining open nodes
	let j = state.openNodes.length;
	while (j--) {
		let openNode = state.openNodes[j];
		openNode.length = state.i - openNode.index;
		let rule = state.rulesMap.get(openNode.type);
		if (rule?.closeNode !== undefined) {
			rule.closeNode(state, openNode);
		}
	}

	// Stage 2 -- parse the inlines for each block
	let node = document.nextNode;
	while (node !== undefined) {
		if (node.block) {
			parseBlockInlines(node, rules.inlines, state.refs, state.footnotes);
		}
		node = node.nextNode;
	}

	return document;
}
