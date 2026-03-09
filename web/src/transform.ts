import parse from "./parse";
import render from "./render";
import type Renderer from "./types/Renderer";
import type RuleSet from "./types/RuleSet";

export default function transform(
	src: string,
	rules: RuleSet,
	renderers: Map<string, Renderer>,
): string {
	const doc = parse(src, rules);
	return render(doc, renderers);
}
