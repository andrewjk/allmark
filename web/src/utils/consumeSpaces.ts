import isSpace from "../utils/isSpace";

export default function consumeSpaces(text: string, i: number): string {
	let result = "";
	for (; i < text.length; i++) {
		let char = text[i];
		if (isSpace(text.charCodeAt(i))) {
			result += char;
		} else {
			break;
		}
	}
	return result;
}
