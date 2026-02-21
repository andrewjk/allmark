using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allmark.Rulesets;

namespace Allmark.Tests;

[TestClass]
public class RenderToConsoleTests
{
    [TestMethod]
    public void RenderConsoleParagraph()
    {
        var input = "Hello, world!";
        var root = Parser.Execute(input, Core.RuleSet, false);
        var output = RenderToConsole.Execute(root, Core.RuleSet);
        Assert.IsTrue(output.Contains("Hello, world!"));
    }

    [TestMethod]
    public void RenderConsoleHeading()
    {
        var input = "# Heading 1\n## Heading 2";
        var root = Parser.Execute(input, Core.RuleSet, false);
        var output = RenderToConsole.Execute(root, Core.RuleSet);
        Assert.IsTrue(output.Contains("# Heading 1"));
        Assert.IsTrue(output.Contains("## Heading 2"));
    }

    [TestMethod]
    public void RenderConsoleBulletedList()
    {
        var input = "- Item 1\n- Item 2";
        var root = Parser.Execute(input, Core.RuleSet, false);
        var output = RenderToConsole.Execute(root, Core.RuleSet);
        Assert.IsTrue(output.Contains("• Item 1"));
        Assert.IsTrue(output.Contains("• Item 2"));
    }

    [TestMethod]
    public void RenderConsoleOrderedList()
    {
        var input = "1. First\n2. Second";
        var root = Parser.Execute(input, Core.RuleSet, false);
        var output = RenderToConsole.Execute(root, Core.RuleSet);
        Assert.IsTrue(output.Contains("1. First"));
        Assert.IsTrue(output.Contains("2. Second"));
    }

    [TestMethod]
    public void RenderConsoleCodeBlock()
    {
        var input = "```\ncode\n```";
        var root = Parser.Execute(input, Core.RuleSet, false);
        var output = RenderToConsole.Execute(root, Core.RuleSet);
        Assert.IsTrue(output.Contains("┌─"));
        Assert.IsTrue(output.Contains("│"));
        Assert.IsTrue(output.Contains("└─"));
    }

    [TestMethod]
    public void RenderConsoleThematicBreak()
    {
        var input = "---";
        var root = Parser.Execute(input, Core.RuleSet, false);
        var output = RenderToConsole.Execute(root, Core.RuleSet);
        Assert.IsTrue(output.Contains("─"));
    }

    [TestMethod]
    public void RenderConsoleTaskList()
    {
        var input = "- [x] Done\n- [ ] Todo";
        var root = Parser.Execute(input, Gfm.RuleSet, false);
        var output = RenderToConsole.Execute(root, Gfm.RuleSet);
        Assert.IsTrue(output.Contains("[✓]"));
        Assert.IsTrue(output.Contains("[ ]"));
    }

    [TestMethod]
    public void RenderConsoleStrongText()
    {
        var input = "**bold**";
        var root = Parser.Execute(input, Core.RuleSet, false);
        var output = RenderToConsole.Execute(root, Core.RuleSet);
        Assert.IsTrue(output.Contains("bold"));
    }

    [TestMethod]
    public void RenderConsoleEmphasisText()
    {
        var input = "*italic*";
        var root = Parser.Execute(input, Core.RuleSet, false);
        var output = RenderToConsole.Execute(root, Core.RuleSet);
        Assert.IsTrue(output.Contains("italic"));
    }

    [TestMethod]
    public void RenderConsoleLink()
    {
        var input = "[text](url)";
        var root = Parser.Execute(input, Core.RuleSet, false);
        var output = RenderToConsole.Execute(root, Core.RuleSet);
        Assert.IsTrue(output.Contains("text"));
        Assert.IsTrue(output.Contains("url"));
    }

    [TestMethod]
    public void RenderConsoleImage()
    {
        var input = "![alt](url)";
        var root = Parser.Execute(input, Core.RuleSet, false);
        var output = RenderToConsole.Execute(root, Core.RuleSet);
        Assert.IsTrue(output.Contains("Image"));
    }

    [TestMethod]
    public void RenderConsoleStrikethrough()
    {
        var input = "~~deleted~~";
        var root = Parser.Execute(input, Gfm.RuleSet, false);
        var output = RenderToConsole.Execute(root, Gfm.RuleSet);
        Assert.IsTrue(output.Contains("\x1b[9mdeleted\x1b[29m"));
    }

    [TestMethod]
    public void RenderConsoleAlert()
    {
        var input = "> [!NOTE]\n> Note content";
        var root = Parser.Execute(input, Gfm.RuleSet, false);
        var output = RenderToConsole.Execute(root, Gfm.RuleSet);
        Assert.IsTrue(output.Contains("📝"));
        Assert.IsTrue(output.Contains("Note:"));
    }

    [TestMethod]
    public void RenderConsoleNestedList()
    {
        var input = "- Level 1\n  - Level 2\n    - Level 3";
        var root = Parser.Execute(input, Core.RuleSet, false);
        var output = RenderToConsole.Execute(root, Core.RuleSet);
        Assert.IsTrue(output.Contains("• Level 1"));
        Assert.IsTrue(output.Contains("◦ Level 2"));
    }
}
