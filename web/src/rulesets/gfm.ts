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
import emphasisRule from "../inline/emphasisRule";
import extendedAutolinkRule from "../inline/extendedAutolinkRule";
import footnoteRule from "../inline/footnoteRule";
import hardBreakRule from "../inline/hardBreakRule";
import htmlSpanRule from "../inline/htmlSpanRule";
import linkRule from "../inline/linkRule";
import strikethroughRule from "../inline/strikethroughRule";
import textRule from "../inline/textRule";
import type RuleSet from "../types/RuleSet";

/**
 * The rules from [CommonMark](https://spec.commonmark.org/) plus the [GitHub
 * Flavored Markdown](https://github.github.com/gfm/) rules.
 */
const gfm: RuleSet = {
	blocks: [
		indentRule,
		headingRule,
		headingUnderlineRule,
		thematicBreakRule,
		alertRule,
		blockQuoteRule,
		listOrderedRule,
		listBulletedRule,
		listItemRule,
		listTaskItemRule,
		footnoteReferenceRule,
		codeBlockRule,
		codeFenceRule,
		htmlBlockRule,
		linkReferenceRule,
		tableRule,
		paragraphRule,
		contentRule,
	],
	inlines: [
		autolinkRule,
		extendedAutolinkRule,
		htmlSpanRule,
		codeSpanRule,
		emphasisRule,
		strikethroughRule,
		footnoteRule,
		linkRule,
		hardBreakRule,
		textRule,
	],
};

export default gfm;
