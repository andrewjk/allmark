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
import lineBreakRule from "../inline/lineBreakRule";
import linkRule from "../inline/linkRule";
import textRule from "../inline/textRule";
import type RuleSet from "../types/RuleSet";

/**
 * The core rules from [CommonMark](https://spec.commonmark.org/).
 */
const core: RuleSet = {
	blocks: new Map([
		[indentRule.name, indentRule],
		[headingRule.name, headingRule],
		[headingUnderlineRule.name, headingUnderlineRule],
		[thematicBreakRule.name, thematicBreakRule],
		[blockQuoteRule.name, blockQuoteRule],
		[listOrderedRule.name, listOrderedRule],
		[listBulletedRule.name, listBulletedRule],
		[listItemRule.name, listItemRule],
		[codeBlockRule.name, codeBlockRule],
		[codeFenceRule.name, codeFenceRule],
		[htmlBlockRule.name, htmlBlockRule],
		[linkReferenceRule.name, linkReferenceRule],
		[paragraphRule.name, paragraphRule],
		[contentRule.name, contentRule],
	]),
	inlines: new Map([
		[autolinkRule.name, autolinkRule],
		[htmlSpanRule.name, htmlSpanRule],
		[codeSpanRule.name, codeSpanRule],
		[emphasisRule.name, emphasisRule],
		[linkRule.name, linkRule],
		[hardBreakRule.name, hardBreakRule],
		[lineBreakRule.name, lineBreakRule],
		[textRule.name, textRule],
	]),
};

export default core;
