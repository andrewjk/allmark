import { BACKSLASH_CODE } from "./charCodes";

export default function isEscaped(text: string, i: number): boolean {
	return text.charCodeAt(i - 1) === BACKSLASH_CODE && text.charCodeAt(i - 2) !== BACKSLASH_CODE;
}
