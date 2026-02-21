import type RendererState from "../types/RendererState";

export default interface ConsoleRendererState extends RendererState {
	depth: number;
	quoteDepth: number;
}
