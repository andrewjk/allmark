import parseBlockInlines from "./parse/parseBlockInlines";
import { parseLine } from "./parse/parseLine";
import type BlockParserState from "./types/BlockParserState";
import type MarkdownNode from "./types/MarkdownNode";
import type RuleSet from "./types/RuleSet";
import { DASH_CODE, NEW_LINE_CODE } from "./utils/charCodes";
import isNewLine from "./utils/isNewLine";
import isSpace from "./utils/isSpace";
import newBlock from "./utils/newBlock";

export default function parse(src: string, rules: RuleSet): MarkdownNode {
	let document = newBlock("document", 0, 1, "", 0);

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

	// Process frontmatter if found
	let frontmatter: string | undefined;
	if (src.charCodeAt(index) === DASH_CODE) {
		frontmatter = extractFrontMatter(document, src, index);
		if (frontmatter !== undefined) {
			i = index + frontmatter.length;
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
	parseBlockInlines(document, rules.inlines, state.refs, state.footnotes);

	if (frontmatter !== undefined) {
		document.info = frontmatter;
	}

	return document;
}

function extractFrontMatter(document: MarkdownNode, src: string, index: number) {
	let frontmatter: string | undefined;

	if (src.charCodeAt(index) === DASH_CODE && /^---\s*\r?\n/.test(src.substring(index))) {
		let contentEnd = -1;
		for (let j = index + 3; j < src.length; j++) {
			if (src.charCodeAt(j) === DASH_CODE && /^---\s*\r?\n/.test(src.substring(j))) {
				contentEnd = src.length;
				for (let k = j + 3; k < src.length; k++) {
					if (src.charCodeAt(k) === NEW_LINE_CODE) {
						contentEnd = k;
						break;
					}
				}
			}
		}
		if (contentEnd !== -1) {
			frontmatter = src.substring(index, contentEnd);
			let i = contentEnd;
			document.line = src.substring(0, i).split("\n").length;
		}
	}

	return frontmatter;
}
