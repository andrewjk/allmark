import type BlockParserState from "../types/BlockParserState";
import closeNode from "../utils/closeNode";
import parseBlock from "./parseBlock";
import parseIndent from "./parseIndent";

export function parseLine(state: BlockParserState): void {
	state.indent = 0;
	state.line++;
	state.lineStart = state.i;
	state.maybeContinue = false;

	//if (state.debug) {
	//	console.log(
	//		`Parsing line ${state.line} at ${state.i} with open nodes [${state.openNodes.map((n) => n.type).join(", ")}]`,
	//	);
	//}
	parseIndent(state);

	// Skip document -- it's always going to continue
	for (let i = 1; i < state.openNodes.length; i++) {
		let node = state.openNodes[i];
		// TODO: Fallback rule??
		let rule = state.rules.get(node.type)!;
		//if (state.debug && !rule) {
		//	console.log("RULE NOT FOUND:", node.type);
		//}
		if (rule.testContinue(state, node)) {
			// TODO: Is there a rule that shouldn't do this?
			parseIndent(state);
		} else {
			closeNode(state, node);
			state.openNodes.length = i;
			break;
		}
	}

	let parent = state.openNodes.at(-1)!;
	parseBlock(state, parent);
}
