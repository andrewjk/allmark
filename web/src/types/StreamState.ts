import type BlockParserState from "./BlockParserState";
import type MarkdownNode from "./MarkdownNode";
import type Renderer from "./Renderer";
import type RuleSet from "./RuleSet";

export default interface StreamState {
	rules: RuleSet;
	renderers: Renderer[];
	src: string;
	document: MarkdownNode;
	parserState: BlockParserState;
	output: string;
	mark: string;
	prevKeptCount: number;
}
