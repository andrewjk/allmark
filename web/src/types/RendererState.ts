import type MarkdownNode from "./MarkdownNode";
import type Renderer from "./Renderer";

export default interface RendererState {
	renderers: Map<string, Renderer>;

	output: string;
	footnotes: MarkdownNode[];
}
