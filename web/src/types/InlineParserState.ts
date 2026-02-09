import type Delimiter from "./Delimiter";
import type FootnoteReference from "./FootnoteReference";
import type InlineRule from "./InlineRule";
import type LinkReference from "./LinkReference";

export default interface InlineParserState {
	rules: Map<string, InlineRule>;

	src: string;
	i: number;
	line: number;
	lineStart: number;
	indent: number;
	delimiters: Delimiter[];
	refs: Record<string, LinkReference>;
	footnotes: Record<string, FootnoteReference>;

	// HACK:
	debug?: boolean;
}
