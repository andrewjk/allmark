import type MarkdownNode from "../types/MarkdownNode";
import type RendererState from "../types/RendererState";
import { endNewLine, startNewLine } from "./utils";

export default function render(node: MarkdownNode, state: RendererState, tag: string): void {
	startNewLine(node, state);
	state.output += `<${tag} />`;
	endNewLine(node, state);
}
