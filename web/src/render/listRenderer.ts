import type MarkdownNode from "../types/MarkdownNode";
import type RendererState from "../types/RendererState";
import renderChildren from "./renderChildren";
import renderNode from "./renderNode";
import { endNewLine, innerNewLine, startNewLine } from "./renderUtils";

export default function render(node: MarkdownNode, state: RendererState): void {
	// TODO: Can we remove paragraphs when parsing instead?
	let ordered = node.type === "list_ordered";
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

	// "A list is loose if any of its constituent list items are separated by
	// blank lines, or if any of its constituent list items directly contain two
	// block-level elements with a blank line between them. Otherwise a list is
	// tight."
	let loose = false;
	for (let i = 0; i < node.children!.length - 1; i++) {
		let child = node.children![i]!;

		// A list item has a blank line after if its last child has a blank line after
		let grandchild = child.children?.at(-1);
		if (grandchild?.blankAfter) {
			child.blankAfter = true;
		}

		if (child.blankAfter) {
			loose = true;
			break;
		}
	}
	for (let i = 0; i < node.children!.length; i++) {
		let child = node.children![i]!;
		for (let j = 0; j < child.children!.length - 1; j++) {
			let first = child.children![j];
			let second = child.children![j + 1];
			if (first.block && first.blankAfter && second.block) {
				loose = true;
				break;
			}
		}
	}

	for (let item of node.children!) {
		state.output += "<li>";
		for (let [i, child] of item.children!.entries()) {
			if (!loose && child.type === "paragraph") {
				// Skip paragraphs under list items to make the list tight
				renderChildren(child, state);
			} else {
				if (i === 0) {
					innerNewLine(item, state);
				}
				renderNode(child, state, i === item.children!.length - 1);
			}
		}
		state.output += "</li>";
		endNewLine(node, state);
	}

	state.output += `</${ordered ? `ol` : `ul`}>`;
	endNewLine(node, state);
}
