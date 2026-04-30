import blockQuoteRule from "../block/blockQuoteRule";
import codeBlockRule from "../block/codeBlockRule";
import codeFenceRule from "../block/codeFenceRule";
import contentRule from "../block/contentRule";
import headingRule from "../block/headingRule";
import headingUnderlineRule from "../block/headingUnderlineRule";
import htmlBlockRule from "../block/htmlBlockRule";
import indentRule from "../block/indentRule";
import linkReferenceRule from "../block/linkReferenceRule";
import listBulletedRule from "../block/listBulletedRule";
import listItemRule from "../block/listItemRule";
import listOrderedRule from "../block/listOrderedRule";
import paragraphRule from "../block/paragraphRule";
import thematicBreakRule from "../block/thematicBreakRule";
import autolinkRule from "../inline/autolinkRule";
import codeSpanRule from "../inline/codeSpanRule";
import emphasisRule from "../inline/emphasisRule";
import hardBreakRule from "../inline/hardBreakRule";
import htmlSpanRule from "../inline/htmlSpanRule";
import linkRule from "../inline/linkRule";
import textRule from "../inline/textRule";
import type RuleSet from "../types/RuleSet";

/**
 * The core rules from [CommonMark](https://spec.commonmark.org/).
 */
const core: RuleSet = {
	blocks: [
		indentRule,
		headingRule,
		headingUnderlineRule,
		thematicBreakRule,
		blockQuoteRule,
		listOrderedRule,
		listBulletedRule,
		listItemRule,
		codeBlockRule,
		codeFenceRule,
		htmlBlockRule,
		linkReferenceRule,
		paragraphRule,
		contentRule,
	],
	inlines: [
		autolinkRule,
		htmlSpanRule,
		codeSpanRule,
		emphasisRule,
		linkRule,
		hardBreakRule,
		textRule,
	],
};

export default core;
