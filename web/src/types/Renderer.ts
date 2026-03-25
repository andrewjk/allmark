import type MarkdownNode from "./MarkdownNode";
import type RendererState from "./RendererState";

export default interface Renderer {
	name: string;
	render: (node: MarkdownNode, state: RendererState, decode?: boolean) => void;
}
