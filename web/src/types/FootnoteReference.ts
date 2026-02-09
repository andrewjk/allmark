import MarkdownNode from "./MarkdownNode";

export default interface FootnoteReference {
	label: string;
	content: MarkdownNode;
}
