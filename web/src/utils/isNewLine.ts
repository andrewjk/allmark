import { NEW_LINE_CODE } from "./charCodes";

export default function isNewLine(charCode: number): boolean {
	return charCode === NEW_LINE_CODE;
}
