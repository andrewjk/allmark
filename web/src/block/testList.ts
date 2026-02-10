import parseBlock from "../parse/parseBlock";
import type BlockParserState from "../types/BlockParserState";
import type MarkdownNode from "../types/MarkdownNode";
import closeNode from "../utils/closeNode";
import isNewLine from "../utils/isNewLine";
import isSpace from "../utils/isSpace";
import movePastMarker from "../utils/movePastMarker";
import newNode from "../utils/newNode";

/**
 * "A list is a sequence of one or more list items of the same type. The list
 * items may be separated by any number of blank lines.
 *
 * Two list items are of the same type if they begin with a list marker of the
 * same type. Two list markers are of the same type if (a) they are bullet list
 * markers using the same character (-, +, or *) or (b) they are ordered list
 * numbers with the same delimiter (either . or )).
 *
 * A list is an ordered list if its constituent list items begin with ordered
 * list markers, and a bullet list if its constituent list items begin with
 * bullet list markers.
 *
 * The start number of an ordered list is determined by the list number of its
 * initial list item. The numbers of subsequent list items are disregarded.
 *
 * A list is loose if any of its constituent list items are separated by blank
 * lines, or if any of its constituent list items directly contain two
 * block-level elements with a blank line between them. Otherwise a list is
 * tight. (The difference in HTML output is that paragraphs in a loose list are
 * wrapped in <p> tags, while paragraphs in a tight list are not.)"
 */

interface ListInfo {
	delimiter: string;
	markup: string;
	isBlank: boolean;
	type: string;
}

export function testListStart(
	state: BlockParserState,
	parent: MarkdownNode,
	info?: ListInfo,
): boolean {
	if (info === undefined) {
		return false;
	}

	let closedNode: MarkdownNode | undefined;

	// TODO: Move the things that refer to "list_[un]ordered" into their rules

	// "When the first list item in a list interrupts a paragraph—that is, when
	// it starts on a line that would otherwise count as paragraph continuation
	// text—then (a) the lines Ls must not begin with a blank line, and (b) if
	// the list item is ordered, the start number must be 1."
	// HACK: Only at the top level??
	if (parent.type === "paragraph" && state.openNodes.length === 2) {
		// "[A]n empty list item cannot interrupt a paragraph"
		if (info.isBlank) {
			return false;
		}

		// "[W]e allow only lists starting with 1 to interrupt paragraphs"
		if (info.type === "list_ordered" && info.markup !== "1" + info.delimiter) {
			return false;
		}
	}

	// "[L]ist items may not be indented more than three spaces"
	let openIndent = state.indent;
	let i = state.openNodes.length;
	while (i--) {
		if (isList(state.openNodes[i].type)) {
			openIndent -= state.openNodes[i].indent;
			break;
		}
	}
	if (openIndent >= 4) {
		return false;
	}

	// If this was possibly a continuation, it no longer is
	// If there was a blank line after the paragraph, move it to the continued node
	if (state.maybeContinue) {
		state.maybeContinue = false;
		let i = state.openNodes.length;
		while (i--) {
			let node = state.openNodes[i];
			if (node.maybeContinuing) {
				node.maybeContinuing = false;
				closedNode = node;
				state.openNodes.length = i;
			}
		}
		parent = state.openNodes.at(-1)!;
	}

	// If there's an open paragraph, close it
	// TODO: Is there a better way to do this??
	if (parent.type === "paragraph") {
		closedNode = state.openNodes.pop();
		parent = state.openNodes.at(-1)!;
	}

	// If there's an open list of a different type, and this node is not nested, close it
	// TODO: Is there a better way to do this??
	if (isList(parent.type) && parent.delimiter !== info.delimiter) {
		let lastItem = parent.children?.at(-1)!;
		if (lastItem.type === "list_item" && state.indent < lastItem.subindent) {
			closedNode = state.openNodes.pop();
			parent = state.openNodes.at(-1)!;
		}
	}

	if (closedNode !== undefined) {
		closeNode(state, closedNode);
	}

	// Count spaces after the marker
	let spaces = 0;
	let blank = true;
	for (let i = state.i + info.markup.length; i < state.src.length; i++) {
		if (isNewLine(state.src[i])) {
			break;
		} else if (isSpace(state.src.charCodeAt(i))) {
			spaces++;
		} else {
			blank = false;
			break;
		}
	}

	// If there's a newline after the marker, treat it as one space
	if (blank) {
		spaces = 1;
	}

	// "If the first block in the list item is an indented code block, then
	// by rule #2, the contents must be indented one space after the list
	// marker:"
	if (spaces > 4) {
		spaces = 1;
	}

	let haveList = parent.type === info.type;
	let list = haveList
		? parent
		: newNode(info.type, true, state.i, state.line, 1, info.markup, state.indent, []);
	list.delimiter = info.delimiter;
	let item = newNode("list_item", true, state.i, state.line, 1, info.markup, state.indent, []);
	item.delimiter = info.delimiter;
	item.subindent = state.indent + info.markup.length + spaces;

	if (!haveList) {
		if (state.hasBlankLine && parent.children !== undefined && parent.children.length > 0) {
			parent.children.at(-1)!.blankAfter = true;
			state.hasBlankLine = false;
		}

		parent.children!.push(list);
		state.openNodes.push(list);
	}

	if (state.hasBlankLine && parent.children !== undefined && parent.children.length > 0) {
		parent.children.at(-1)!.blankAfter = true;
		state.hasBlankLine = false;
	}

	list.children?.push(item);
	state.openNodes.push(item);

	movePastMarker(info.markup.length, state);

	state.hasBlankLine = false;
	parseBlock(state, item);

	return true;
}

export function testListContinue(
	state: BlockParserState,
	node: MarkdownNode,
	info?: ListInfo,
): boolean {
	let char = state.src[state.i];

	// If there's the same list marker and the indent is not too far, we can continue
	if (info !== undefined) {
		if (state.hasBlankLine && state.indent >= 4) {
			return false;
		}
		if (info.delimiter === node.delimiter) {
			return true;
		}
	}

	// Can't continue if there's only one item, it's blank and there's a blank line after the list
	// HACK: This is messy
	if (state.hasBlankLine && node.children?.length === 1 && !node.children![0].children?.length) {
		return false;
	}

	// TODO: Not sure about this one
	// Also, do the state.isEmpty check with indent like on the other branch
	if (isNewLine(char)) {
		return true;
	}

	let openNode = state.openNodes.at(-1)!;
	if (openNode.type === "paragraph") {
		// We won't know until we try more things
		return true;
	}

	let lastItem = node.children?.at(-1);
	if (lastItem && lastItem.type === "list_item" && state.indent >= lastItem.subindent) {
		return true;
	}

	return false;
}

// HACK: Not great
function isList(nodeType: string) {
	return nodeType.startsWith("list_") && nodeType !== "list_item";
}
