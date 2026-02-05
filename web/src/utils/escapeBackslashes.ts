import isPunctuation from "../utils/isPunctuation";

export default function escapeBackslashes(text: string): string {
	let result = "";
	for (let i = 0; i < text.length; i++) {
		let char = text[i];
		if (char === "\\" && isPunctuation(text.charCodeAt(i + 1))) {
			i++;
			char = text[i];
		}
		result += char;
	}
	return result;
}
