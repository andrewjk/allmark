import type MarkdownNode from "../types/MarkdownNode";

export default function newText(
	index: number,
	line: number,
	content: string,
	indent: number,
): MarkdownNode {
	return {
		type: "text",
		block: false,
		index,
		length: 0,
		line,
		markup: "",
		delimiter: "",
		content,
		indent,
		subindent: 0,
		loose: false,
		acceptsContent: false,
		maybeContinuing: false,
		blankAfter: false,
	};
}
