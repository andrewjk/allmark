using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allmark.Rulesets;

namespace Allmark.Tests;

[TestClass]
public class RenderConsoleTests
{
    private static string StripAnsiCodes(string input)
    {
        var result = input;
        var patterns = new[]
        {
            "\x1b[0m", "\x1b[1m", "\x1b[2m", "\x1b[3m", "\x1b[4m",
            "\x1b[9m", "\x1b[29m", "\x1b[31m", "\x1b[32m", "\x1b[33m",
            "\x1b[34m", "\x1b[35m", "\x1b[36m", "\x1b[38;5;208m",
            "\x1b[43m", "\x1b[30m", "\x1b[90m", "\x1B[0m", "\x1B[1m",
            "\x1B[2m", "\x1B[35m"
        };
        foreach (var pattern in patterns)
        {
            result = result.Replace(pattern, "");
        }
        return result;
    }

    [TestMethod]
    public void RendersParagraphToConsole()
    {
        var input = "Hello, world!";
        var expected = "Hello, world!\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = Renderer.Execute(doc, ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersParagraphThenParagraphToConsole()
    {
        var input = "Hello, world!\n\nHello again";
        var expected = "Hello, world!\n\nHello again\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = Renderer.Execute(doc, ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersParagraphX3ToConsole()
    {
        var input = "First\n\nSecond\n\nThird";
        var expected = "First\n\nSecond\n\nThird\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = StripAnsiCodes(Renderer.Execute(doc, ConsoleRenderers.Renderers));
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersHeadingToConsoleWithColor()
    {
        var input = "# Heading 1\n## Heading 2";
        var expected = "\x1b[2m#\x1b[0m \x1b[1m\x1b[35mHeading 1\x1b[0m\n\n\x1b[2m##\x1b[0m \x1b[1m\x1b[35mHeading 2\x1b[0m\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = Renderer.Execute(doc, ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersHeadingThenHeadingToConsole()
    {
        var input = "# Heading 1\n## Heading 2";
        var expected = "# Heading 1\n\n## Heading 2\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = StripAnsiCodes(Renderer.Execute(doc, ConsoleRenderers.Renderers));
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersHeadingThenParagraph()
    {
        var input = "# Heading\n\nParagraph text";
        var expected = "# Heading\n\nParagraph text\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = StripAnsiCodes(Renderer.Execute(doc, ConsoleRenderers.Renderers));
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersParagraphThenHeading()
    {
        var input = "Paragraph text\n\n# Heading";
        var expected = "Paragraph text\n\n# Heading\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = StripAnsiCodes(Renderer.Execute(doc, ConsoleRenderers.Renderers));
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersBulletedListWithUnicodeBullets()
    {
        var input = "- Item 1\n- Item 2";
        var expected = "\x1b[2m•\x1b[0m Item 1\n\x1b[2m•\x1b[0m Item 2\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = Renderer.Execute(doc, ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersOrderedList()
    {
        var input = "1. First\n2. Second";
        var expected = "\x1b[2m1.\x1b[0m First\n\x1b[2m2.\x1b[0m Second\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = Renderer.Execute(doc, ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersTightBulletedList()
    {
        var input = "- Item 1\n- Item 2\n- Item 3";
        var expected = "• Item 1\n• Item 2\n• Item 3\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = StripAnsiCodes(Renderer.Execute(doc, ConsoleRenderers.Renderers));
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersLooseBulletedList()
    {
        var input = "- Item 1\n\n- Item 2\n\n- Item 3";
        var expected = "• Item 1\n\n• Item 2\n\n• Item 3\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = StripAnsiCodes(Renderer.Execute(doc, ConsoleRenderers.Renderers));
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersTightOrderedList()
    {
        var input = "1. First\n2. Second\n3. Third";
        var expected = "1. First\n2. Second\n3. Third\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = StripAnsiCodes(Renderer.Execute(doc, ConsoleRenderers.Renderers));
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersLooseOrderedList()
    {
        var input = "1. First\n\n2. Second\n\n3. Third";
        var expected = "1. First\n\n2. Second\n\n3. Third\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = StripAnsiCodes(Renderer.Execute(doc, ConsoleRenderers.Renderers));
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersOrderedListWithNestedBulletedList()
    {
        var input = "1. First\n   - Nested A\n   - Nested B\n2. Second";
        var expected = "1. First\n  ◦ Nested A\n  ◦ Nested B\n2. Second\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = StripAnsiCodes(Renderer.Execute(doc, ConsoleRenderers.Renderers));
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersBulletedListWithNestedOrderedList()
    {
        var input = "- First\n  1. Nested A\n  2. Nested B\n- Second";
        var expected = "• First\n  1. Nested A\n  2. Nested B\n• Second\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = StripAnsiCodes(Renderer.Execute(doc, ConsoleRenderers.Renderers));
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersCodeFenceWithBoxDrawing()
    {
        var input = "```\ncode\n```";
        var expected = "\x1b[2m┌─\x1b[0m\n\x1b[2m│\x1b[0m code\n\x1b[2m└─\x1b[0m\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = Renderer.Execute(doc, ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersHeadingThenList()
    {
        var input = "# Heading\n\n- Item 1\n- Item 2";
        var expected = "# Heading\n\n• Item 1\n• Item 2\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = StripAnsiCodes(Renderer.Execute(doc, ConsoleRenderers.Renderers));
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersListThenHeading()
    {
        var input = "- Item 1\n- Item 2\n\n# Heading";
        var expected = "• Item 1\n• Item 2\n\n# Heading\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = StripAnsiCodes(Renderer.Execute(doc, ConsoleRenderers.Renderers));
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersParagraphThenList()
    {
        var input = "Paragraph\n\n- Item 1\n- Item 2";
        var expected = "Paragraph\n\n• Item 1\n• Item 2\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = StripAnsiCodes(Renderer.Execute(doc, ConsoleRenderers.Renderers));
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersListThenParagraph()
    {
        var input = "- Item 1\n- Item 2\n\nParagraph";
        var expected = "• Item 1\n• Item 2\n\nParagraph\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = StripAnsiCodes(Renderer.Execute(doc, ConsoleRenderers.Renderers));
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersHeadingThenCodeBlock()
    {
        var input = "# Heading\n\n```\ncode\n```";
        var expected = "# Heading\n\n┌─\n│ code\n└─\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = StripAnsiCodes(Renderer.Execute(doc, ConsoleRenderers.Renderers));
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersCodeBlockThenHeading()
    {
        var input = "```\ncode\n```\n\n# Heading";
        var expected = "┌─\n│ code\n└─\n\n# Heading\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = StripAnsiCodes(Renderer.Execute(doc, ConsoleRenderers.Renderers));
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersHeadingThenBlockQuote()
    {
        var input = "# Heading\n\n> Quote text";
        var expected = "# Heading\n\n┃ Quote text\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = StripAnsiCodes(Renderer.Execute(doc, ConsoleRenderers.Renderers));
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersBlockQuoteThenHeading()
    {
        var input = "> Quote text\n\n# Heading";
        var expected = "┃ Quote text\n\n# Heading\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = StripAnsiCodes(Renderer.Execute(doc, ConsoleRenderers.Renderers));
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersHeadingThenThematicBreak()
    {
        var input = "# Heading\n\n---";
        var expected = "# Heading\n\n───\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = StripAnsiCodes(Renderer.Execute(doc, ConsoleRenderers.Renderers));
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersThematicBreakThenHeading()
    {
        var input = "---\n\n# Heading";
        var expected = "───\n\n# Heading\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = StripAnsiCodes(Renderer.Execute(doc, ConsoleRenderers.Renderers));
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersMultipleBlockTypes()
    {
        var input = "# Heading 1\n\nParagraph 1\n\n---\n\n## Heading 2\n\nParagraph 2";
        var expected = "# Heading 1\n\nParagraph 1\n\n───\n\n## Heading 2\n\nParagraph 2\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = StripAnsiCodes(Renderer.Execute(doc, ConsoleRenderers.Renderers));
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersInlineCode()
    {
        var input = "`code`";
        var expected = "\x1b[32mcode\x1b[0m\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = Renderer.Execute(doc, ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersBlockQuoteWithVerticalLine()
    {
        var input = "> Quote text";
        var expected = "┃ Quote text\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = Renderer.Execute(doc, ConsoleRenderers.Renderers);
        var stripped = System.Text.RegularExpressions.Regex.Replace(output, @"\x1b\[[0-9;]*m", "");
        Assert.AreEqual(expected, stripped);
    }

    [TestMethod]
    public void RendersThematicBreak()
    {
        var input = "---";
        var expected = "\x1b[2m───\x1b[0m\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = Renderer.Execute(doc, ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersTaskListWithEmojis()
    {
        var input = "- [x] Done\n- [ ] Todo";
        var expected = "\x1b[2m•\x1b[0m [✓] Done\n\x1b[2m•\x1b[0m [ ] Todo\n";
        var doc = Parser.Execute(input, Gfm.RuleSet);
        var output = Renderer.Execute(doc, ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersTableWithUnicodeBorders()
    {
        var input = "| A | B |\n|---|---|\n| 1 | 2 |";
        var expected = "\x1b[2m┌───┬───┐\x1b[0m\n\x1b[2m│\x1b[0m A \x1b[2m│\x1b[0m B \x1b[2m│\x1b[0m\n\x1b[2m├───┼───┤\x1b[0m\n\x1b[2m│\x1b[0m 1 \x1b[2m│\x1b[0m 2 \x1b[2m│\x1b[0m\n\x1b[2m└───┴───┘\x1b[0m\n";
        var doc = Parser.Execute(input, Gfm.RuleSet);
        var output = Renderer.Execute(doc, ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersTableThenParagraph()
    {
        var input = "| A |\n|---|\n| 1 |\n\nParagraph";
        var expected = "┌───┐\n│ A │\n├───┤\n│ 1 │\n└───┘\nParagraph\n";
        var doc = Parser.Execute(input, Gfm.RuleSet);
        var output = StripAnsiCodes(Renderer.Execute(doc, ConsoleRenderers.Renderers));
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersParagraphThenTable()
    {
        var input = "Paragraph\n\n| A |\n|---|\n| 1 |";
        var expected = "Paragraph\n\n┌───┐\n│ A │\n├───┤\n│ 1 │\n└───┘\n";
        var doc = Parser.Execute(input, Gfm.RuleSet);
        var output = StripAnsiCodes(Renderer.Execute(doc, ConsoleRenderers.Renderers));
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersTableWithPadding()
    {
        var input = "| A | B |\n| - | - |\n| 1 | hello |";
        var expected = """
┌───┬───────┐
│ A │ B     │
├───┼───────┤
│ 1 │ hello │
└───┴───────┘
""";
        var doc = Parser.Execute(input, Gfm.RuleSet);
        var output = StripAnsiCodes(Renderer.Execute(doc, ConsoleRenderers.Renderers));
        Assert.AreEqual(expected.Trim(), output.Trim());
    }

    [TestMethod]
    public void RendersTableWithCorrectlyAlignedPadding()
    {
        var input = "| A | B |\n| - | -: |\n| x | 1 |\n| y | 200 |";
        var expected = """
┌───┬─────┐
│ A │   B │
├───┼─────┤
│ x │   1 │
│ y │ 200 │
└───┴─────┘
""";
        var doc = Parser.Execute(input, Gfm.RuleSet);
        var output = StripAnsiCodes(Renderer.Execute(doc, ConsoleRenderers.Renderers));
        Assert.AreEqual(expected.Trim(), output.Trim());
    }

    [TestMethod]
    public void RendersStrongText()
    {
        var input = "**bold**";
        var expected = "\x1b[1m\x1b[33mbold\x1b[0m\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = Renderer.Execute(doc, ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersEmphasisText()
    {
        var input = "*italic*";
        var expected = "\x1b[3m\x1b[33mitalic\x1b[0m\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = Renderer.Execute(doc, ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersLink()
    {
        var input = "[text](url)";
        var expected = "\x1b[34m\x1b[4mtext\x1b[0m \x1b[2m(url)\x1b[0m\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = Renderer.Execute(doc, ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersImage()
    {
        var input = "![alt](url)";
        var expected = "\x1b[90m[Image: alt]\x1b[0m\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = Renderer.Execute(doc, ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersStrikethrough()
    {
        var input = "~~deleted~~";
        var expected = "\x1b[2m\x1b[9mdeleted\x1b[29m\x1b[0m\n";
        var doc = Parser.Execute(input, Gfm.RuleSet);
        var output = Renderer.Execute(doc, ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersAlertWithEmoji()
    {
        var input = "> [!NOTE]\n> Note content";
        var expected = "\x1b[34m📝 Note:\x1b[0m\n\nNote content\n";
        var doc = Parser.Execute(input, Gfm.RuleSet);
        var output = Renderer.Execute(doc, ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersAlertThenParagraph()
    {
        var input = "> [!NOTE]\n> Note\n\nParagraph";
        var expected = "📝 Note:\n\nNote\n\nParagraph\n";
        var doc = Parser.Execute(input, Gfm.RuleSet);
        var output = StripAnsiCodes(Renderer.Execute(doc, ConsoleRenderers.Renderers));
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersParagraphThenAlert()
    {
        var input = "Paragraph\n\n> [!NOTE]\n> Note";
        var expected = "Paragraph\n\n📝 Note:\n\nNote\n";
        var doc = Parser.Execute(input, Gfm.RuleSet);
        var output = StripAnsiCodes(Renderer.Execute(doc, ConsoleRenderers.Renderers));
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersNestedListWithDifferentBullets()
    {
        var input = "- Level 1\n  - Level 2\n    - Level 3";
        var expected = "\x1b[2m•\x1b[0m Level 1\n  \x1b[2m◦\x1b[0m Level 2\n    \x1b[2m▪\x1b[0m Level 3\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = Renderer.Execute(doc, ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersHardBreak()
    {
        var input = "Line 1\n\nLine 2";
        var expected = "Line 1\n\nLine 2\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = Renderer.Execute(doc, ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersHeadingWithUnderlineSetextStyle()
    {
        var input = "Heading\n=======\n\nSubheading\n-------";
        var expected = "\x1B[1m\x1B[35mHeading\x1B[0m\n\x1B[2m=======\x1B[0m\n\x1B[1m\x1B[35mSubheading\x1B[0m\n\x1B[2m----------\x1B[0m\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = Renderer.Execute(doc, ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersHtmlBlock()
    {
        var input = "<div>html</div>";
        var expected = "<div>html</div>\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = Renderer.Execute(doc, ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersHtmlSpanInline()
    {
        var input = "test <span>html</span> test";
        var expected = "test <span>html</span> test\n";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = Renderer.Execute(doc, ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersComment()
    {
        var input = "<!-- comment -->";
        var expected = "<!-- comment -->\n";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var output = Renderer.Execute(doc, ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersDeletionStrikethroughAlternative()
    {
        var input = "~~deleted~~";
        var expected = "\x1b[2m\x1b[9mdeleted\x1b[29m\x1b[0m\n";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var output = Renderer.Execute(doc, ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersFootnote()
    {
        var input = "Text [^1]\n\n[^1]: http://example.com";
        var expected = "Text \x1b[2m[1]\x1b[0m\n";
        var doc = Parser.Execute(input, Gfm.RuleSet);
        var output = Renderer.Execute(doc, ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersHighlight()
    {
        var input = "==highlighted==";
        var expected = "\x1b[43m\x1b[30mhighlighted\x1b[0m\n";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var output = Renderer.Execute(doc, ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersInsertion()
    {
        var input = "++inserted++";
        var expected = "++inserted++\n";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var output = Renderer.Execute(doc, ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void BasicParseAndRender()
    {
        var input = """
# Test

Here is some text

* Tight item 1
  * Nested item 1
* Tight item 2

- Loose item 1

- Loose item 2

## Subtest

Here is some more text
""";
        var expected = """
# Test

Here is some text

• Tight item 1
  ◦ Nested item 1
• Tight item 2

• Loose item 1

• Loose item 2

## Subtest

Here is some more text
""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var output = StripAnsiCodes(Renderer.Execute(doc, ConsoleRenderers.Renderers));
        Assert.AreEqual(expected.Trim(), output.Trim());
    }
}
