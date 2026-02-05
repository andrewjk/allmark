import isSpace from "./isSpace";

export default function skipSpaces(text: string, start: number): number {
	for (; start < text.length; start++) {
		if (!isSpace(text.charCodeAt(start))) {
			break;
		}
	}
	return start;
}
