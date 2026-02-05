import type BlockParserState from "./BlockParserState";
import type MarkdownNode from "./MarkdownNode";

// TODO: Try to arrange rules so that they don't have to refer to other rules
// e.g. checking for code in lists
export default interface BlockRule {
	/**
	 * The name for this rule, which must also be used for nodes created by this
	 * rule.
	 */
	name: string;
	/**
	 * The names of rules that this node closes.
	 */
	// TODO: closes: string[];
	/**
	 * Tests whether a node should start e.g. a block quote should start when we
	 * find a '>'.
	 * @param state
	 * @param parent
	 * @returns
	 */
	testStart: (state: BlockParserState, parent: MarkdownNode) => boolean;
	/**
	 * Creates a node for this rule.
	 * @param state
	 * @param parent
	 * @returns
	 */
	// TODO: createNode: (state: ParserState, parent: MarkdownNode) => void;
	/**
	 * Tests whether a node should continue after being started e.g. a block
	 * quote should continue if we find a '>'.
	 * @param state
	 * @param parent
	 * @returns
	 */
	testContinue: (state: BlockParserState, parent: MarkdownNode) => boolean;
	/**
	 * Does any cleanup for this rule's node when it is closed.
	 * @param state
	 * @param parent
	 * @returns
	 */
	closeNode?: (state: BlockParserState, parent: MarkdownNode) => void;
}
