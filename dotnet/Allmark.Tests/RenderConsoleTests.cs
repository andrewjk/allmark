using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allmark.Rulesets;

namespace Allmark.Tests;

[TestClass]
public class RenderConsoleTests
{
    [TestMethod]
    public void RendersParagraphToConsole()
    {
        var input = "Hello, world!";
        var expected = "Hello, world!";
        var doc = Parser.Execute(input, Core.RuleSet, false);
        var output = RenderToConsole.Execute(doc, Allmark.Rulesets.ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersHeadingToConsoleWithColor()
    {
        var input = "# Heading 1\n## Heading 2";
        var expected = "\x1b[1m\x1b[36m# Heading 1\x1b[0m\n\x1b[1m\x1b[34m## Heading 2\x1b[0m";
        var doc = Parser.Execute(input, Core.RuleSet, false);
        var output = RenderToConsole.Execute(doc, Allmark.Rulesets.ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersBulletedListWithUnicodeBullets()
    {
        var input = "- Item 1\n- Item 2";
        var expected = "• Item 1\n• Item 2";
        var doc = Parser.Execute(input, Core.RuleSet, false);
        var output = RenderToConsole.Execute(doc, Allmark.Rulesets.ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersOrderedList()
    {
        var input = "1. First\n2. Second";
        var expected = "1. First\n2. Second";
        var doc = Parser.Execute(input, Core.RuleSet, false);
        var output = RenderToConsole.Execute(doc, Allmark.Rulesets.ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersCodeFenceWithBoxDrawing()
    {
        var input = "```\ncode\n```";
        var expected = "\x1b[2m┌─\x1b[0m\n\x1b[2m│\x1b[0m code\n\x1b[2m└─\x1b[0m";
        var doc = Parser.Execute(input, Core.RuleSet, false);
        var output = RenderToConsole.Execute(doc, Allmark.Rulesets.ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersInlineCode()
    {
        var input = "`code`";
        var expected = "\x1b[32m`code`\x1b[0m";
        var doc = Parser.Execute(input, Core.RuleSet, false);
        var output = RenderToConsole.Execute(doc, Allmark.Rulesets.ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersBlockQuoteWithVerticalLine()
    {
        var input = "> Quote text";
        var expected = "┃ Quote text";
        var doc = Parser.Execute(input, Core.RuleSet, false);
        var output = RenderToConsole.Execute(doc, Allmark.Rulesets.ConsoleRenderers.Renderers);
        var stripped = System.Text.RegularExpressions.Regex.Replace(output, @"\x1b\[[0-9;]*m", "");
        Assert.AreEqual(expected, stripped);
    }

    [TestMethod]
    public void RendersThematicBreak()
    {
        var input = "---";
        var expected = "\x1b[2m───\x1b[0m";
        var doc = Parser.Execute(input, Core.RuleSet, false);
        var output = RenderToConsole.Execute(doc, Allmark.Rulesets.ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersTaskListWithEmojis()
    {
        var input = "- [x] Done\n- [ ] Todo";
        var expected = "• [✓] Done\n• [ ] Todo";
        var doc = Parser.Execute(input, Gfm.RuleSet, false);
        var output = RenderToConsole.Execute(doc, Allmark.Rulesets.ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersTableWithUnicodeBorders()
    {
        var input = "| A | B |\n|---|---|\n| 1 | 2 |";
        var expected = "\x1b[2m┌───┬───┐\x1b[0m\n\x1b[2m│\x1b[0m A \x1b[2m│\x1b[0m B \x1b[2m│\x1b[0m\n\x1b[2m├───┼───┤\x1b[0m\n\x1b[2m│\x1b[0m 1 \x1b[2m│\x1b[0m 2 \x1b[2m│\x1b[0m\n\x1b[2m└───┴───┘\x1b[0m";
        var doc = Parser.Execute(input, Gfm.RuleSet, false);
        var output = RenderToConsole.Execute(doc, Allmark.Rulesets.ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersStrongText()
    {
        var input = "**bold**";
        var expected = "\x1b[1m\x1b[33mbold\x1b[0m";
        var doc = Parser.Execute(input, Core.RuleSet, false);
        var output = RenderToConsole.Execute(doc, Allmark.Rulesets.ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersEmphasisText()
    {
        var input = "*italic*";
        var expected = "\x1b[3m\x1b[33mitalic\x1b[0m";
        var doc = Parser.Execute(input, Core.RuleSet, false);
        var output = RenderToConsole.Execute(doc, Allmark.Rulesets.ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersLink()
    {
        var input = "[text](url)";
        var expected = "\x1b[34m\x1b[4mtext\x1b[0m (url)";
        var doc = Parser.Execute(input, Core.RuleSet, false);
        var output = RenderToConsole.Execute(doc, Allmark.Rulesets.ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersImage()
    {
        var input = "![alt](url)";
        var expected = "\x1b[2m[Image: alt]\x1b[0m";
        var doc = Parser.Execute(input, Core.RuleSet, false);
        var output = RenderToConsole.Execute(doc, Allmark.Rulesets.ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersStrikethrough()
    {
        var input = "~~deleted~~";
        var expected = "\x1b[2m\x1b[9mdeleted\x1b[29m\x1b[0m";
        var doc = Parser.Execute(input, Gfm.RuleSet, false);
        var output = RenderToConsole.Execute(doc, Allmark.Rulesets.ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersAlertWithEmoji()
    {
        var input = "> [!NOTE]\n> Note content";
        var expected = "\x1b[34m📝 Note:\x1b[0m\n\nNote content";
        var doc = Parser.Execute(input, Gfm.RuleSet, false);
        var output = RenderToConsole.Execute(doc, Allmark.Rulesets.ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersNestedListWithDifferentBullets()
    {
        var input = "- Level 1\n  - Level 2\n    - Level 3";
        var expected = "• Level 1\n  ◦ Level 2\n    ▪ Level 3";
        var doc = Parser.Execute(input, Core.RuleSet, false);
        var output = RenderToConsole.Execute(doc, Allmark.Rulesets.ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersHardBreak()
    {
        var input = "Line 1\n\nLine 2";
        var expected = "Line 1\n\nLine 2";
        var doc = Parser.Execute(input, Core.RuleSet, false);
        var output = RenderToConsole.Execute(doc, Allmark.Rulesets.ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersHeadingWithUnderlineSetextStyle()
    {
        var input = "Heading\n=======\n\nSubheading\n-------";
        var expected = "\x1b[1m\x1b[36mHeading\n=======\x1b[0m\n\x1b[1m\x1b[34mSubheading\n----------\x1b[0m";
        var doc = Parser.Execute(input, Core.RuleSet, false);
        var output = RenderToConsole.Execute(doc, Allmark.Rulesets.ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersHtmlBlock()
    {
        var input = "<div>html</div>";
        var expected = "<div>html</div>";
        var doc = Parser.Execute(input, Core.RuleSet, false);
        var output = RenderToConsole.Execute(doc, Allmark.Rulesets.ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersHtmlSpanInline()
    {
        var input = "test <span>html</span> test";
        var expected = "test <span>html</span> test";
        var doc = Parser.Execute(input, Core.RuleSet, false);
        var output = RenderToConsole.Execute(doc, Allmark.Rulesets.ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersComment()
    {
        var input = "<!-- comment -->";
        var expected = "<!-- comment -->";
        var doc = Parser.Execute(input, Extended.RuleSet, false);
        var output = RenderToConsole.Execute(doc, Allmark.Rulesets.ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersDeletionStrikethroughAlternative()
    {
        var input = "~~deleted~~";
        var expected = "\x1b[2m\x1b[9mdeleted\x1b[29m\x1b[0m";
        var doc = Parser.Execute(input, Extended.RuleSet, false);
        var output = RenderToConsole.Execute(doc, Allmark.Rulesets.ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersFootnote()
    {
        var input = "Text [^1]\n\n[^1]: http://example.com";
        var expected = "Text \x1b[2m[1]\x1b[0m";
        var doc = Parser.Execute(input, Gfm.RuleSet, false);
        var output = RenderToConsole.Execute(doc, Allmark.Rulesets.ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersHighlight()
    {
        var input = "==highlighted==";
        var expected = "\x1b[43m\x1b[30mhighlighted\x1b[0m";
        var doc = Parser.Execute(input, Extended.RuleSet, false);
        var output = RenderToConsole.Execute(doc, Allmark.Rulesets.ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }

    [TestMethod]
    public void RendersInsertion()
    {
        var input = "++inserted++";
        var expected = "++inserted++";
        var doc = Parser.Execute(input, Extended.RuleSet, false);
        var output = RenderToConsole.Execute(doc, Allmark.Rulesets.ConsoleRenderers.Renderers);
        Assert.AreEqual(expected, output);
    }
}
