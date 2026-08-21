import type MarkdownNode from "../types/MarkdownNode";
import type RendererState from "../types/RendererState";
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

	for (let item of node.children!) {
		state.output += "<li>";
		for (let [i, child] of item.children!.entries()) {
			if (!node.loose && child.type === "paragraph") {
				// Skip paragraphs under list items to make the list tight
				renderChildren(child, state);
			} else {
				if (i === 0) {
					innerNewLine(item, state);
				}
				renderNode(child, state);
				if (i === item.children!.length - 1 && child.block) {
					if (state.output.endsWith("\n")) {
						state.output = state.output.slice(0, -1);
					}
					if (state.output.endsWith("\r")) {
						state.output = state.output.slice(0, -1);
					}
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
