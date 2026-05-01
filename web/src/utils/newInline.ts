import type MarkdownNode from "../types/MarkdownNode";

export default function newInline(
	type: string,
	index: number,
	line: number,
	markup: string,
	indent: number,
): MarkdownNode {
	return {
		type,
		block: false,
		index,
		length: 0,
		line,
		markup,
		delimiter: "",
		content: "",
		indent,
		subindent: 0,
		acceptsContent: false,
		maybeContinuing: false,
		blankAfter: false,
		depth: 0,
		previousNode: undefined,
		nextNode: undefined,
	};
}
