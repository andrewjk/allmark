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
import deletionRule from "../inline/deletionRule";
import emphasisRule from "../inline/emphasisRule";
import extendedAutolinkRule from "../inline/extendedAutolinkRule";
import footnoteRule from "../inline/footnoteRule";
import hardBreakRule from "../inline/hardBreakRule";
import highlightRule from "../inline/highlightRule";
import htmlSpanRule from "../inline/htmlSpanRule";
import insertionRule from "../inline/insertionRule";
import lineBreakRule from "../inline/lineBreakRule";
import linkRule from "../inline/linkRule";
import strikethroughRule from "../inline/strikethroughRule";
import subscriptRule from "../inline/subscriptRule";
import superscriptRule from "../inline/superscriptRule";
import textRule from "../inline/textRule";
import alertRenderer from "../render/alertRenderer";
import blockQuoteRenderer from "../render/blockQuoteRenderer";
import codeBlockRenderer from "../render/codeBlockRenderer";
import codeFenceRenderer from "../render/codeFenceRenderer";
import codeSpanRenderer from "../render/codeSpanRenderer";
import deletionRenderer from "../render/deletionRenderer";
import emphasisRenderer from "../render/emphasisRenderer";
import footnoteRenderer from "../render/footnoteRenderer";
import hardBreakRenderer from "../render/hardBreakRenderer";
import headingRenderer from "../render/headingRenderer";
import headingUnderlineRenderer from "../render/headingUnderlineRenderer";
import highlightRenderer from "../render/highlightRenderer";
import htmlBlockRenderer from "../render/htmlBlockRenderer";
import htmlSpanRenderer from "../render/htmlSpanRenderer";
import imageRenderer from "../render/imageRenderer";
import insertionRenderer from "../render/insertionRenderer";
import linkRenderer from "../render/linkRenderer";
import listBulletedRenderer from "../render/listBulletedRenderer";
import listOrderedRenderer from "../render/listOrderedRenderer";
import listTaskItemRenderer from "../render/listTaskItemRenderer";
import paragraphRenderer from "../render/paragraphRenderer";
import strikethroughRenderer from "../render/strikethroughRenderer";
import strongRenderer from "../render/strongRenderer";
import subscriptRenderer from "../render/subscriptRenderer";
import superscriptRenderer from "../render/superscriptRenderer";
import tableCellRenderer from "../render/tableCellRenderer";
import tableHeaderRenderer from "../render/tableHeaderRenderer";
import tableRenderer from "../render/tableRenderer";
import tableRowRenderer from "../render/tableRowRenderer";
import textRenderer from "../render/textRenderer";
import thematicBreakRenderer from "../render/thematicBreakRenderer";
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
		[lineBreakRule.name, lineBreakRule],
		[insertionRule.name, insertionRule],
		[deletionRule.name, deletionRule],
		[textRule.name, textRule],
	]),
	renderers: new Map([
		[alertRenderer.name, alertRenderer],
		[blockQuoteRenderer.name, blockQuoteRenderer],
		[codeBlockRenderer.name, codeBlockRenderer],
		[codeFenceRenderer.name, codeFenceRenderer],
		[codeSpanRenderer.name, codeSpanRenderer],
		[deletionRenderer.name, deletionRenderer],
		[emphasisRenderer.name, emphasisRenderer],
		[footnoteRenderer.name, footnoteRenderer],
		[hardBreakRenderer.name, hardBreakRenderer],
		[headingRenderer.name, headingRenderer],
		[headingUnderlineRenderer.name, headingUnderlineRenderer],
		[highlightRenderer.name, highlightRenderer],
		[htmlBlockRenderer.name, htmlBlockRenderer],
		[htmlSpanRenderer.name, htmlSpanRenderer],
		[imageRenderer.name, imageRenderer],
		[insertionRenderer.name, insertionRenderer],
		[linkRenderer.name, linkRenderer],
		[listBulletedRenderer.name, listBulletedRenderer],
		[listOrderedRenderer.name, listOrderedRenderer],
		[listTaskItemRenderer.name, listTaskItemRenderer],
		[paragraphRenderer.name, paragraphRenderer],
		[strikethroughRenderer.name, strikethroughRenderer],
		[strongRenderer.name, strongRenderer],
		[subscriptRenderer.name, subscriptRenderer],
		[superscriptRenderer.name, superscriptRenderer],
		[tableRenderer.name, tableRenderer],
		[tableCellRenderer.name, tableCellRenderer],
		[tableHeaderRenderer.name, tableHeaderRenderer],
		[tableRowRenderer.name, tableRowRenderer],
		[textRenderer.name, textRenderer],
		[thematicBreakRenderer.name, thematicBreakRenderer],
	]),
};

export default gfm;
