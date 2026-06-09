import parse from "./parse";
import render from "./render";
import type Renderer from "./types/Renderer";
import type RenderOptions from "./types/RenderOptions";
import type RuleSet from "./types/RuleSet";

export default function transform(
	src: string,
	rules: RuleSet,
	renderers: Renderer[],
	options?: RenderOptions,
): string {
	const doc = parse(src, rules);
	return render(doc, renderers, options);
}
