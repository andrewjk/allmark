import type BlockParserState from "../types/BlockParserState";
import { NEW_LINE_CODE } from "./charCodes";

export default function getEndOfLine(state: BlockParserState): number {
	let endOfLine = state.i;
	for (; endOfLine < state.src.length; endOfLine++) {
		let code = state.src.charCodeAt(endOfLine);
		if (code === NEW_LINE_CODE) {
			endOfLine++;
			state.lineStart = endOfLine;
			break;
		}
	}
	return endOfLine;
}
