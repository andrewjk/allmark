import type BlockParserState from "../types/BlockParserState";
import type MarkdownNode from "../types/MarkdownNode";

export default function closeNode(state: BlockParserState, node: MarkdownNode): void {
	node.length = state.i - node.index;

	// Call the close function of each open child after (and including) this one
	let i = state.openNodes.length;
	while (i-- > 1) {
		let openNode = state.openNodes[i];
		let rule = state.rules.get(openNode.type)!;
		if (rule.closeNode !== undefined) {
			rule.closeNode(state, openNode);
		}
		//if (state.debug) {
		//	console.log("Closed node", openNode.type);
		//}
		if (openNode === node) {
			break;
		}
	}
}
