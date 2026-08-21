import type BlockParserState from "../types/BlockParserState";
import { CARRIAGE_RETURN_CODE, NEW_LINE_CODE } from "../utils/charCodes";
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
		let rule = state.rulesMap.get(node.type)!;
		//if (state.debug && !rule) {
		//	console.log("RULE NOT FOUND:", node.type);
		//}
		if (rule.testContinue(state, node)) {
			// TODO: Is there a rule that shouldn't do this?
			parseIndent(state);
		} else {
			// Call the close function of each open child after (and including) this one
			let j = state.openNodes.length;
			while (j-- > i) {
				let openNode = state.openNodes[j];
				closeNode(state, openNode);
			}
			state.openNodes.length = i;
			break;
		}
	}

	// Get the end of the line
	let endOfLine = state.i;
	let nextIndex = state.src.length;
	for (; endOfLine < state.src.length; endOfLine++) {
		let code = state.src.charCodeAt(endOfLine);
		if (code === NEW_LINE_CODE) {
			nextIndex = endOfLine + 1;
			break;
		} else if (code === CARRIAGE_RETURN_CODE) {
			nextIndex = endOfLine + 1;
			if (state.src.charCodeAt(endOfLine + 1) === NEW_LINE_CODE) {
				nextIndex++;
			}
			break;
		}
	}

	let parent = state.openNodes.at(-1)!;
	parseBlock(state, parent, endOfLine);

	// NOTE: a rule can move state.i past the next line
	// (e.g. for a HTML block or link reference containing a newline)
	if (state.i < nextIndex) {
		state.i = nextIndex;
		state.lineStart = nextIndex;
	}
}
