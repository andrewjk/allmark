import BlockRule from "./BlockRule";
import type FootnoteReference from "./FootnoteReference";
import type LinkReference from "./LinkReference";
import type MarkdownNode from "./MarkdownNode";

export default interface BlockParserState {
	rules: BlockRule[];
	rulesMap: Map<string, BlockRule>;

	src: string;
	i: number;
	line: number;
	lineStart: number;
	indent: number;
	spaces: string;
	openNodes: MarkdownNode[];
	isEscaped: boolean;
	maybeContinue: boolean;
	hasBlankLine: boolean;
	refs: Record<string, LinkReference>;
	footnotes: Record<string, FootnoteReference>;
}
