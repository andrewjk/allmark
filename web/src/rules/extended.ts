import testAlert from "../block/testAlert";
import testBlockQuote from "../block/testBlockQuote";
import testCodeBlock from "../block/testCodeBlock";
import testCodeFence from "../block/testCodeFence";
import testContent from "../block/testContent";
import testFootnoteReference from "../block/testFootnoteReference";
import testHeading from "../block/testHeading";
import testHeadingUnderline from "../block/testHeadingUnderline";
import testHtmlBlock from "../block/testHtmlBlock";
import testIndent from "../block/testIndent";
import testLinkReference from "../block/testLinkReference";
import testListBulleted from "../block/testListBulleted";
import testListItem from "../block/testListItem";
import testListOrdered from "../block/testListOrdered";
import testListTaskItem from "../block/testListTaskItem";
import testParagraph from "../block/testParagraph";
import testTable from "../block/testTable";
import testThematicBreak from "../block/testThematicBreak";
import testAutolink from "../inline/testAutolink";
import testCodeSpan from "../inline/testCodeSpan";
import testDeletion from "../inline/testDeletion";
import testEmphasis from "../inline/testEmphasis";
import testExtendedAutolink from "../inline/testExtendedAutolink";
import testFootnote from "../inline/testFootnote";
import testHardBreak from "../inline/testHardBreak";
import testHighlight from "../inline/testHighlight";
import testHtmlSpan from "../inline/testHtmlSpan";
import testInsertion from "../inline/testInsertion";
import testLineBreak from "../inline/testLineBreak";
import testLink from "../inline/testLink";
import testStrikethrough from "../inline/testStrikethrough";
import testSubscript from "../inline/testSubscript";
import testSuperscript from "../inline/testSuperscript";
import testText from "../inline/testText";
import renderAlert from "../render/renderAlert";
import renderBlockQuote from "../render/renderBlockQuote";
import renderCodeBlock from "../render/renderCodeBlock";
import renderCodeFence from "../render/renderCodeFence";
import renderCodeSpan from "../render/renderCodeSpan";
import renderDeletion from "../render/renderDeletion";
import renderEmphasis from "../render/renderEmphasis";
import renderFootnote from "../render/renderFootnote";
import renderHardBreak from "../render/renderHardBreak";
import renderHeading from "../render/renderHeading";
import renderHeadingUnderline from "../render/renderHeadingUnderline";
import renderHighlight from "../render/renderHighlight";
import renderHtmlBlock from "../render/renderHtmlBlock";
import renderHtmlSpan from "../render/renderHtmlSpan";
import renderImage from "../render/renderImage";
import renderInsertion from "../render/renderInsertion";
import renderLink from "../render/renderLink";
import renderListBulleted from "../render/renderListBulleted";
import renderListOrdered from "../render/renderListOrdered";
import renderListTaskItem from "../render/renderListTaskItem";
import renderParagraph from "../render/renderParagraph";
import renderStrikethrough from "../render/renderStrikethrough";
import renderStrong from "../render/renderStrong";
import renderSubscript from "../render/renderSubscript";
import renderSuperscript from "../render/renderSuperscript";
import renderTable from "../render/renderTable";
import renderTableCell from "../render/renderTableCell";
import renderTableHeader from "../render/renderTableHeader";
import renderTableRow from "../render/renderTableRow";
import renderText from "../render/renderText";
import renderThematicBreak from "../render/renderThematicBreak";
import type RuleSet from "../types/RuleSet";

/**
 * The rules from [CommonMark](https://spec.commonmark.org/) and [GitHub
 * Flavored Markdown](https://github.github.com/gfm/) plus some extended rules
 * from various note taking apps.
 */
const gfm: RuleSet = {
	blocks: new Map([
		[testIndent.name, testIndent],
		[testHeading.name, testHeading],
		[testHeadingUnderline.name, testHeadingUnderline],
		[testThematicBreak.name, testThematicBreak],
		[testAlert.name, testAlert],
		[testBlockQuote.name, testBlockQuote],
		[testListOrdered.name, testListOrdered],
		[testListBulleted.name, testListBulleted],
		[testListItem.name, testListItem],
		[testListTaskItem.name, testListTaskItem],
		[testFootnoteReference.name, testFootnoteReference],
		[testCodeBlock.name, testCodeBlock],
		[testCodeFence.name, testCodeFence],
		[testHtmlBlock.name, testHtmlBlock],
		[testLinkReference.name, testLinkReference],
		[testTable.name, testTable],
		[testParagraph.name, testParagraph],
		[testContent.name, testContent],
	]),
	inlines: new Map([
		[testAutolink.name, testAutolink],
		[testExtendedAutolink.name, testExtendedAutolink],
		[testHtmlSpan.name, testHtmlSpan],
		[testCodeSpan.name, testCodeSpan],
		[testEmphasis.name, testEmphasis],
		[testSubscript.name, testSubscript],
		[testSuperscript.name, testSuperscript],
		[testStrikethrough.name, testStrikethrough],
		[testHighlight.name, testHighlight],
		[testFootnote.name, testFootnote],
		[testLink.name, testLink],
		[testHardBreak.name, testHardBreak],
		[testLineBreak.name, testLineBreak],
		[testInsertion.name, testInsertion],
		[testDeletion.name, testDeletion],
		[testText.name, testText],
	]),
	renderers: new Map([
		[renderAlert.name, renderAlert],
		[renderBlockQuote.name, renderBlockQuote],
		[renderCodeBlock.name, renderCodeBlock],
		[renderCodeFence.name, renderCodeFence],
		[renderCodeSpan.name, renderCodeSpan],
		[renderDeletion.name, renderDeletion],
		[renderEmphasis.name, renderEmphasis],
		[renderFootnote.name, renderFootnote],
		[renderHardBreak.name, renderHardBreak],
		[renderHeading.name, renderHeading],
		[renderHeadingUnderline.name, renderHeadingUnderline],
		[renderHighlight.name, renderHighlight],
		[renderHtmlBlock.name, renderHtmlBlock],
		[renderHtmlSpan.name, renderHtmlSpan],
		[renderImage.name, renderImage],
		[renderInsertion.name, renderInsertion],
		[renderLink.name, renderLink],
		[renderListBulleted.name, renderListBulleted],
		[renderListOrdered.name, renderListOrdered],
		[renderListTaskItem.name, renderListTaskItem],
		[renderParagraph.name, renderParagraph],
		[renderStrikethrough.name, renderStrikethrough],
		[renderStrong.name, renderStrong],
		[renderSubscript.name, renderSubscript],
		[renderSuperscript.name, renderSuperscript],
		[renderTable.name, renderTable],
		[renderTableCell.name, renderTableCell],
		[renderTableHeader.name, renderTableHeader],
		[renderTableRow.name, renderTableRow],
		[renderText.name, renderText],
		[renderThematicBreak.name, renderThematicBreak],
	]),
};

export default gfm;
