import alertRule from "../block/alertRule";
import blockQuoteRule from "../block/blockQuoteRule";
import codeBlockRule from "../block/codeBlockRule";
import codeFenceRule from "../block/codeFenceRule";
import contentRule from "../block/contentRule";
import footnoteReferenceRule from "../block/footnoteReferenceRule";
import headingRule from "../block/headingRule";
import headingUnderlineRule from "../block/headingUnderlineRule";
import htmlBlockRule from "../block/htmlBlockRule";
import indentRule from "../block/indentRule";
import linkReferenceRule from "../block/linkReferenceRule";
import listBulletedRule from "../block/listBulletedRule";
import listItemRule from "../block/listItemRule";
import listOrderedRule from "../block/listOrderedRule";
import listTaskItemRule from "../block/listTaskItemRule";
import paragraphRule from "../block/paragraphRule";
import tableRule from "../block/tableRule";
import thematicBreakRule from "../block/thematicBreakRule";
import autolinkRule from "../inline/autolinkRule";
import codeSpanRule from "../inline/codeSpanRule";
import commentRule from "../inline/commentRule";
import deletionRule from "../inline/deletionRule";
import emphasisRule from "../inline/emphasisRule";
import extendedAutolinkRule from "../inline/extendedAutolinkRule";
import footnoteRule from "../inline/footnoteRule";
import hardBreakRule from "../inline/hardBreakRule";
import highlightRule from "../inline/highlightRule";
import htmlSpanRule from "../inline/htmlSpanRule";
import insertionRule from "../inline/insertionRule";
import linkRule from "../inline/linkRule";
import strikethroughRule from "../inline/strikethroughRule";
import subscriptRule from "../inline/subscriptRule";
import superscriptRule from "../inline/superscriptRule";
import textRule from "../inline/textRule";
import type RuleSet from "../types/RuleSet";

/**
 * The rules from [CommonMark](https://spec.commonmark.org/) and [GitHub
 * Flavored Markdown](https://github.github.com/gfm/) plus some extended rules
 * from various note taking apps.
 */
const gfm: RuleSet = {
	blocks: new Map([
		[indentRule.name, indentRule],
		[headingRule.name, headingRule],
		[headingUnderlineRule.name, headingUnderlineRule],
		[thematicBreakRule.name, thematicBreakRule],
		[alertRule.name, alertRule],
		[blockQuoteRule.name, blockQuoteRule],
		[listOrderedRule.name, listOrderedRule],
		[listBulletedRule.name, listBulletedRule],
		[listItemRule.name, listItemRule],
		[listTaskItemRule.name, listTaskItemRule],
		[footnoteReferenceRule.name, footnoteReferenceRule],
		[codeBlockRule.name, codeBlockRule],
		[codeFenceRule.name, codeFenceRule],
		[htmlBlockRule.name, htmlBlockRule],
		[linkReferenceRule.name, linkReferenceRule],
		[tableRule.name, tableRule],
		[paragraphRule.name, paragraphRule],
		[contentRule.name, contentRule],
	]),
	inlines: new Map([
		[autolinkRule.name, autolinkRule],
		[extendedAutolinkRule.name, extendedAutolinkRule],
		[htmlSpanRule.name, htmlSpanRule],
		[codeSpanRule.name, codeSpanRule],
		[emphasisRule.name, emphasisRule],
		[subscriptRule.name, subscriptRule],
		[superscriptRule.name, superscriptRule],
		[strikethroughRule.name, strikethroughRule],
		[highlightRule.name, highlightRule],
		[footnoteRule.name, footnoteRule],
		[linkRule.name, linkRule],
		[hardBreakRule.name, hardBreakRule],
		[insertionRule.name, insertionRule],
		[deletionRule.name, deletionRule],
		[commentRule.name, commentRule],
		[textRule.name, textRule],
	]),
};

export default gfm;
