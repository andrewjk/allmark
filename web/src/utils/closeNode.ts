import type BlockParserState from "../types/BlockParserState";
import type MarkdownNode from "../types/MarkdownNode";

export default function closeNode(state: BlockParserState, node: MarkdownNode): void {
	node.length = state.i - node.index;
	let rule = state.rulesMap.get(node.type)!;
	if (rule.closeNode !== undefined) {
		rule.closeNode(state, node);
	}
}
