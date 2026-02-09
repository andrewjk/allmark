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
import testEmphasis from "../inline/testEmphasis";
import testExtendedAutolink from "../inline/testExtendedAutolink";
import testFootnote from "../inline/testFootnote";
import testHardBreak from "../inline/testHardBreak";
import testHtmlSpan from "../inline/testHtmlSpan";
import testLineBreak from "../inline/testLineBreak";
import testLink from "../inline/testLink";
import testStrikethrough from "../inline/testStrikethrough";
import testText from "../inline/testText";
import type RuleSet from "../types/RuleSet";

/**
 * The rules from [CommonMark](https://spec.commonmark.org/) plus the [GitHub
 * Flavored Markdown](https://github.github.com/gfm/) rules.
 */
const gfm: RuleSet = {
	blocks: new Map([
		[testIndent.name, testIndent],
		[testHeading.name, testHeading],
		[testHeadingUnderline.name, testHeadingUnderline],
		[testThematicBreak.name, testThematicBreak],
		[testBlockQuote.name, testBlockQuote],
		[testListOrdered.name, testListOrdered],
		[testListBulleted.name, testListBulleted],
		[testListItem.name, testListItem],
		[testListTaskItem.name, testListTaskItem],
		[testCodeBlock.name, testCodeBlock],
		[testCodeFence.name, testCodeFence],
		[testHtmlBlock.name, testHtmlBlock],
		[testFootnoteReference.name, testFootnoteReference],
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
		[testStrikethrough.name, testStrikethrough],
		[testFootnote.name, testFootnote],
		[testLink.name, testLink],
		[testHardBreak.name, testHardBreak],
		[testLineBreak.name, testLineBreak],
		[testText.name, testText],
	]),
};

export default gfm;
