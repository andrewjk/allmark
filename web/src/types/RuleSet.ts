import type BlockRule from "./BlockRule";
import type InlineRule from "./InlineRule";

export default interface RuleSet {
	blocks: Map<string, BlockRule>;
	inlines: Map<string, InlineRule>;
}
