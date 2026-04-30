import type MarkdownNode from "./MarkdownNode";
import type Renderer from "./Renderer";

export default interface RendererState {
	renderersMap: Map<string, Renderer>;

	output: string;
	footnotes: MarkdownNode[];

	listDepth: number;
}
