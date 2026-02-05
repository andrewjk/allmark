export default interface MarkdownNode {
	type: string;
	block: boolean;
	index: number;
	/** The line number */
	line: number;
	/** The column number */
	column: number;
	/** The markdown-specific markup for this node as it has been entered by the user */
	markup: string;
	/** The delimiter that has determined this node's type */
	delimiter: string;
	/** The text content for this node */
	content: string;
	/** The number of (logical, not physical) spaces this node starts after */
	indent: number;
	/** For list item nodes, the number of (logical, not physical) spaces its content starts after */
	subindent: number;
	/** Whether this node is a tight list item */
	//tight: boolean;
	// TODO: booleans should be flags! Also, I'm not sure whether all combinations are needed?
	/** Whether this node is followed by a blank line */
	blankAfter: boolean;
	/** Whether this node contains plain text content, rather than parsed Markdown */
	acceptsContent: boolean;
	/** Whether this node ends with a paragraph that may lazily continue */
	maybeContinuing: boolean;
	/** Info for a fenced code block, or the URL for a link */
	info?: string;
	/** The title for a link */
	title?: string;
	children?: MarkdownNode[];
}
