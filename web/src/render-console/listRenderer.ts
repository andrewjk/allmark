import type MarkdownNode from "../types/MarkdownNode";
import type RendererState from "../types/RendererState";
import { getChildren, getLastChild } from "../utils/nodeUtils";
import ANSI from "./ansi";
import renderChildren from "./renderChildren";

const bullets = ["•", "◦", "▪", "‣"];

export default function render(node: MarkdownNode, state: RendererState, ordered: boolean): void {
	state.listDepth++;

	const style = ANSI.dim;
	const reset = ANSI.reset;

	const loose = isLooseList(node);

	let counter = 1;
	if (ordered && node.markup) {
		const match = node.markup.match(/^(\d+)/);
		if (match) {
			counter = parseInt(match[1], 10);
		}
	}

	let items = getChildren(node);
	for (const item of items) {
		const prefix = ordered
			? `${counter}.`
			: bullets[Math.min(state.listDepth - 1, bullets.length - 1)] || "•";
		if (ordered) counter++;

		let itemChildren = getChildren(item);
		for (let i = 0; i < itemChildren.length; i++) {
			let child = itemChildren[i];
			if (!loose && child.type === "paragraph") {
				const indent = "  ".repeat(state.listDepth - 1);
				if (i === 0) {
					state.output += `${indent}${style}${prefix}${reset} `;
				}
				renderChildren(child, state);
				state.output += "\n";
			} else {
				const indent = "  ".repeat(state.listDepth - 1);
				if (i === 0) {
					state.output += `${indent}${style}${prefix}${reset} `;
				}
				const renderer = state.renderersMap.get(child.type);
				if (renderer) {
					renderer.render(child, state);
				}
				if (!loose && state.output.endsWith("\n\n")) {
					state.output = state.output.slice(0, state.output.length - 1);
				}
			}
		}
	}

	state.listDepth--;

	// Loose lists will already have a double newline
	if (!loose) {
		state.output += "\n";
	}
}

function isLooseList(node: MarkdownNode) {
	let loose = false;

	// "A list is loose if any of its constituent list items are separated by
	// blank lines, or if any of its constituent list items directly contain two
	// block-level elements with a blank line between them. Otherwise a list is
	// tight."
	let children = getChildren(node);
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
