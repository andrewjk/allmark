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
import blockQuoteRenderer from "../render/blockQuoteRenderer";
import codeBlockRenderer from "../render/codeBlockRenderer";
import codeFenceRenderer from "../render/codeFenceRenderer";
import codeSpanRenderer from "../render/codeSpanRenderer";
import emphasisRenderer from "../render/emphasisRenderer";
import hardBreakRenderer from "../render/hardBreakRenderer";
import headingRenderer from "../render/headingRenderer";
import headingUnderlineRenderer from "../render/headingUnderlineRenderer";
import htmlBlockRenderer from "../render/htmlBlockRenderer";
import htmlSpanRenderer from "../render/htmlSpanRenderer";
import imageRenderer from "../render/imageRenderer";
import linkRenderer from "../render/linkRenderer";
import listBulletedRenderer from "../render/listBulletedRenderer";
import listOrderedRenderer from "../render/listOrderedRenderer";
import paragraphRenderer from "../render/paragraphRenderer";
import strongRenderer from "../render/strongRenderer";
import textRenderer from "../render/textRenderer";
import thematicBreakRenderer from "../render/thematicBreakRenderer";
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
	renderers: new Map([
		[blockQuoteRenderer.name, blockQuoteRenderer],
		[codeBlockRenderer.name, codeBlockRenderer],
		[codeFenceRenderer.name, codeFenceRenderer],
		[codeSpanRenderer.name, codeSpanRenderer],
		[emphasisRenderer.name, emphasisRenderer],
		[hardBreakRenderer.name, hardBreakRenderer],
		[headingRenderer.name, headingRenderer],
		[headingUnderlineRenderer.name, headingUnderlineRenderer],
		[htmlBlockRenderer.name, htmlBlockRenderer],
		[htmlSpanRenderer.name, htmlSpanRenderer],
		[imageRenderer.name, imageRenderer],
		[linkRenderer.name, linkRenderer],
		[listBulletedRenderer.name, listBulletedRenderer],
		[listOrderedRenderer.name, listOrderedRenderer],
		[paragraphRenderer.name, paragraphRenderer],
		[strongRenderer.name, strongRenderer],
		[textRenderer.name, textRenderer],
		[thematicBreakRenderer.name, thematicBreakRenderer],
	]),
};

export default core;
