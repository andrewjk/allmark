import type MarkdownNode from "../types/MarkdownNode";

export function hasChildren(node: MarkdownNode): boolean {
	return node.nextNode !== undefined && node.nextNode.depth > node.depth;
}

export function getFirstChild(node: MarkdownNode): MarkdownNode | undefined {
	if (node.nextNode !== undefined && node.nextNode.depth > node.depth) {
		return node.nextNode;
	}
	return undefined;
}

export function getLastChild(node: MarkdownNode): MarkdownNode | undefined {
	let current = node.nextNode;
	let lastChild: MarkdownNode | undefined = undefined;

	while (current !== undefined && current.depth > node.depth) {
		if (current.depth === node.depth + 1) {
			lastChild = current;
		}
		current = current.nextNode;
	}

	return lastChild;
}

export function getLastDescendant(node: MarkdownNode): MarkdownNode | undefined {
	let current = node.nextNode;
	let lastChild: MarkdownNode | undefined = undefined;

	while (current !== undefined && current.depth > node.depth) {
		lastChild = current;
		current = current.nextNode;
	}

	return lastChild;
}

export function appendChild(parent: MarkdownNode, child: MarkdownNode): void {
	let previousNode = getLastDescendant(parent) ?? parent;

	child.nextNode = previousNode.nextNode;
	child.previousNode = previousNode;
	previousNode.nextNode = child;

	child.depth = parent.depth + 1;
}

export function appendChildWithText(
	parent: MarkdownNode,
	child: MarkdownNode,
	text: MarkdownNode,
): void {
	let previousNode = getLastDescendant(parent) ?? parent;

	text.nextNode = previousNode.nextNode;
	text.previousNode = child;
	child.nextNode = text;
	child.previousNode = previousNode;
	previousNode.nextNode = child;

	child.depth = parent.depth + 1;
	text.depth = child.depth + 1;
}

export function spliceTextNode(
	parent: MarkdownNode,
	text: MarkdownNode,
	after: MarkdownNode,
	lastDescendant: MarkdownNode,
): void {
	// Move the parent's subsequent children under the new link node
	// previousNode -> text -> parent's children
	text.previousNode = after;
	text.nextNode = after.nextNode;
	after.nextNode = text;

	let nextNode = after;
	while (nextNode !== lastDescendant) {
		nextNode.depth++;
		nextNode = nextNode.nextNode!;
	}
	lastDescendant.depth++;

	after.depth = parent.depth + 1;
	text.depth = after.depth + 1;
}

export function forEachChild(
	node: MarkdownNode,
	callback: (child: MarkdownNode, index: number) => void,
): void {
	let child = node.nextNode;
	let index = 0;

	while (child !== undefined && child.depth > node.depth) {
		if (child.depth === node.depth + 1) {
			callback(child, index);
			index++;
		}
		child = child.nextNode;
	}
}

export function getChildren(node: MarkdownNode): MarkdownNode[] {
	const children: MarkdownNode[] = [];
	let child = node.nextNode;
	while (child !== undefined && child.depth > node.depth) {
		if (child.depth === node.depth + 1) {
			children.push(child);
		}
		child = child.nextNode;
	}
	return children;
}

export function getChildCount(node: MarkdownNode): number {
	let count = 0;
	let child = node.nextNode;
	while (child !== undefined && child.depth > node.depth) {
		if (child.depth === node.depth + 1) {
			count++;
		}
		child = child.nextNode;
	}
	return count;
}
