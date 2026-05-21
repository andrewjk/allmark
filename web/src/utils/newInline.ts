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
		loose: false,
		acceptsContent: false,
		maybeContinuing: false,
		blankAfter: false,
	};
}
