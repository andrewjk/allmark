import testBlockQuote from "../block/testBlockQuote";
import testCodeBlock from "../block/testCodeBlock";
import testCodeFence from "../block/testCodeFence";
import testContent from "../block/testContent";
import testHeading from "../block/testHeading";
import testHeadingUnderline from "../block/testHeadingUnderline";
import testHtmlBlock from "../block/testHtmlBlock";
import testIndent from "../block/testIndent";
import testLinkReference from "../block/testLinkReference";
import testListBulleted from "../block/testListBulleted";
import testListItem from "../block/testListItem";
import testListOrdered from "../block/testListOrdered";
import testParagraph from "../block/testParagraph";
import testThematicBreak from "../block/testThematicBreak";
import testAutolink from "../inline/testAutolink";
import testCodeSpan from "../inline/testCodeSpan";
import testEmphasis from "../inline/testEmphasis";
import testHardBreak from "../inline/testHardBreak";
import testHtmlSpan from "../inline/testHtmlSpan";
import testLineBreak from "../inline/testLineBreak";
import testLink from "../inline/testLink";
import testText from "../inline/testText";
import renderBlockQuote from "../render/renderBlockQuote";
import renderCodeBlock from "../render/renderCodeBlock";
import renderCodeFence from "../render/renderCodeFence";
import renderCodeSpan from "../render/renderCodeSpan";
import renderEmphasis from "../render/renderEmphasis";
import renderHardBreak from "../render/renderHardBreak";
import renderHeading from "../render/renderHeading";
import renderHeadingUnderline from "../render/renderHeadingUnderline";
import renderHtmlBlock from "../render/renderHtmlBlock";
import renderHtmlSpan from "../render/renderHtmlSpan";
import renderImage from "../render/renderImage";
import renderLink from "../render/renderLink";
import renderListBulleted from "../render/renderListBulleted";
import renderListOrdered from "../render/renderListOrdered";
import renderParagraph from "../render/renderParagraph";
import renderStrong from "../render/renderStrong";
import renderText from "../render/renderText";
import renderThematicBreak from "../render/renderThematicBreak";
import type RuleSet from "../types/RuleSet";

/**
 * The core rules from [CommonMark](https://spec.commonmark.org/).
 */
const core: RuleSet = {
	blocks: new Map([
		[testIndent.name, testIndent],
		[testHeading.name, testHeading],
		[testHeadingUnderline.name, testHeadingUnderline],
		[testThematicBreak.name, testThematicBreak],
		[testBlockQuote.name, testBlockQuote],
		[testListOrdered.name, testListOrdered],
		[testListBulleted.name, testListBulleted],
		[testListItem.name, testListItem],
		[testCodeBlock.name, testCodeBlock],
		[testCodeFence.name, testCodeFence],
		[testHtmlBlock.name, testHtmlBlock],
		[testLinkReference.name, testLinkReference],
		[testParagraph.name, testParagraph],
		[testContent.name, testContent],
	]),
	inlines: new Map([
		[testAutolink.name, testAutolink],
		[testHtmlSpan.name, testHtmlSpan],
		[testCodeSpan.name, testCodeSpan],
		[testEmphasis.name, testEmphasis],
		[testLink.name, testLink],
		[testHardBreak.name, testHardBreak],
		[testLineBreak.name, testLineBreak],
		[testText.name, testText],
	]),
	renderers: new Map([
		[renderBlockQuote.name, renderBlockQuote],
		[renderCodeBlock.name, renderCodeBlock],
		[renderCodeFence.name, renderCodeFence],
		[renderCodeSpan.name, renderCodeSpan],
		[renderEmphasis.name, renderEmphasis],
		[renderHardBreak.name, renderHardBreak],
		[renderHeading.name, renderHeading],
		[renderHeadingUnderline.name, renderHeadingUnderline],
		[renderHtmlBlock.name, renderHtmlBlock],
		[renderHtmlSpan.name, renderHtmlSpan],
		[renderImage.name, renderImage],
		[renderLink.name, renderLink],
		[renderListBulleted.name, renderListBulleted],
		[renderListOrdered.name, renderListOrdered],
		[renderParagraph.name, renderParagraph],
		[renderStrong.name, renderStrong],
		[renderText.name, renderText],
		[renderThematicBreak.name, renderThematicBreak],
	]),
};

export default core;
