import type MarkdownNode from "../types/MarkdownNode";
import type RendererState from "../types/RendererState";
import { getChildren, getLastChild } from "../utils/nodeUtils";
import renderChildren from "./renderChildren";
import renderNode from "./renderNode";
import { endNewLine, innerNewLine, startNewLine } from "./renderUtils";

export default function render(node: MarkdownNode, state: RendererState, ordered: boolean): void {
	// TODO: Can we remove paragraphs when parsing instead?
	let start = "";
	if (ordered) {
		let startNumber = parseInt(node.markup.substring(0, node.markup.length - 1));
		if (startNumber !== 1) {
			start = ` start="${startNumber}"`;
		}
	}

	startNewLine(node, state);
	state.output += `<${ordered ? `ol${start}` : `ul`}>`;
	innerNewLine(node, state);

	let loose = isLooseList(node);
	let items = getChildren(node);

	for (let item of items) {
		state.output += "<li>";
		let itemChildren = getChildren(item);
		for (let i = 0; i < itemChildren.length; i++) {
			let child = itemChildren[i];
			if (!loose && child.type === "paragraph") {
				// Skip paragraphs under list items to make the list tight
				renderChildren(child, state);
			} else {
				if (i === 0) {
					innerNewLine(item, state);
				}
				renderNode(child, state);
				if (i === itemChildren.length - 1 && child.block && !state.output.endsWith("\n")) {
					state.output += "\n";
				}
			}
		}
		state.output += "</li>";
		endNewLine(node, state);
	}

	state.output += `</${ordered ? `ol` : `ul`}>`;
	endNewLine(node, state);
}

function isLooseList(node: MarkdownNode) {
	let loose = false;
	let children = getChildren(node);

	// "A list is loose if any of its constituent list items are separated by
	// blank lines, or if any of its constituent list items directly contain two
	// block-level elements with a blank line between them. Otherwise a list is
	// tight."
	for (let i = 0; i < children.length - 1; i++) {
		let child = children[i];

		// A list item has a blank line after if its last child has a blank line after
		let grandchild = getLastChild(child);
		if (grandchild !== undefined && grandchild.blankAfter) {
			child.blankAfter = true;
		}

		if (child.blankAfter) {
			loose = true;
			break;
		}
	}

	for (let i = 0; i < children.length; i++) {
		let child = children[i];
		let childChildren = getChildren(child);
		for (let j = 0; j < childChildren.length - 1; j++) {
			let first = childChildren[j];
			let second = childChildren[j + 1];
			if (first.block && first.blankAfter && second.block) {
				loose = true;
				break;
			}
		}
	}

	return loose;
}
