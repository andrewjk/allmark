import type BlockRule from "./BlockRule";
import type InlineRule from "./InlineRule";

export default interface RuleSet {
	blocks: BlockRule[];
	inlines: InlineRule[];
}
