export default function isEscaped(text: string, i: number): boolean {
	return text[i - 1] === "\\" && text[i - 2] !== "\\";
}
