import type BlockRule from "./BlockRule";
import type InlineRule from "./InlineRule";
import type Renderer from "./Renderer";

export default interface RuleSet {
	blocks: Map<string, BlockRule>;
	inlines: Map<string, InlineRule>;
	renderers: Map<string, Renderer>;
}
