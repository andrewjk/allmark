import type BlockParserState from "../types/BlockParserState";
import { CARRIAGE_RETURN_CODE, NEW_LINE_CODE } from "./charCodes";

export default function getLineEnding(state: BlockParserState, endOfLine: number): string {
	return state.src.charCodeAt(endOfLine) === NEW_LINE_CODE
		? "\n"
		: state.src.charCodeAt(endOfLine) === CARRIAGE_RETURN_CODE
			? state.src.charCodeAt(endOfLine + 1) === NEW_LINE_CODE
				? "\r\n"
				: "\r"
			: "";
}
