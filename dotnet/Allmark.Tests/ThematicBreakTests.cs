using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allmark.Rulesets;

namespace Allmark.Tests;

[TestClass]
public class ThematicBreakTests
{
    [TestMethod]
    public void SimpleThematicBreakWithDashes()
    {
        var input = "---";
        var expected = """
		<hr />
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void SimpleThematicBreakWithAsterisks()
    {
        var input = "***";
        var expected = """
		<hr />
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void SimpleThematicBreakWithUnderscores()
    {
        var input = "___";
        var expected = """
		<hr />
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakWith4Dashes()
    {
        var input = "----";
        var expected = """
		<hr />
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakWith5Asterisks()
    {
        var input = "*****";
        var expected = """
		<hr />
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakWithSpacesBetweenCharacters()
    {
        var input = "- - -";
        var expected = """
		<hr />
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakWithTabsBetweenCharacters()
    {
        var input = "*\t*\t*";
        var expected = """
		<hr />
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakWith1SpaceIndent()
    {
        var input = " ---";
        var expected = """
		<hr />
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakWith3SpaceIndent()
    {
        var input = "   ---";
        var expected = """
		<hr />
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakWith4SpaceIndentShouldBeCode()
    {
        var input = "    ---";
        var expected = """
		<pre><code>---
		</code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakFollowedByParagraphWithoutBlankLine()
    {
        var input = """
		---
		Paragraph
		""";
        var expected = """
		<hr />
		<p>Paragraph</p>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void MultipleThematicBreaks()
    {
        var input = """
		---

		***
		""";
        var expected = """
		<hr />
		<hr />
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakNotValidOnly2Dashes()
    {
        var input = "--";
        var expected = """
		<p>--</p>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakNotValidOnly2Asterisks()
    {
        var input = "**";
        var expected = """
		<p>**</p>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakNotValidOnly2Underscores()
    {
        var input = "__";
        var expected = """
		<p>__</p>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakNotValidMixedCharacters()
    {
        var input = "-*-";
        var expected = """
		<p>-*-</p>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakNotValidMixedDashesAndAsterisks()
    {
        var input = "---***";
        var expected = """
		<p>---***</p>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakInBlockquote()
    {
        var input = "> ---";
        var expected = """
		<blockquote>
		<hr />
		</blockquote>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakInListItem()
    {
        var input = """
		- Item
		---
		""";
        var expected = """
		<ul>
		<li>Item</li>
		</ul>
		<hr />
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakWithTrailingSpaces()
    {
        var input = "---   ";
        var expected = """
		<hr />
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakWithTrailingTabs()
    {
        var input = "***\t\t";
        var expected = """
		<hr />
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakAfterListWithoutBlankLine()
    {
        var input = """
		- Item 1
		- Item 2
		---
		""";
        var expected = """
		<ul>
		<li>Item 1</li>
		<li>Item 2</li>
		</ul>
		<hr />
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakBeforeListWithoutBlankLine()
    {
        var input = """
		---
		- Item
		""";
        var expected = """
		<hr />
		<ul>
		<li>Item</li>
		</ul>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakAtEndOfDocument()
    {
        var input = """
		> Quote
		---
		""";
        var expected = """
		<blockquote>
		<p>Quote</p>
		</blockquote>
		<hr />
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakBetweenParagraphs()
    {
        var input = """
		Paragraph 1

		---

		Paragraph 2
		""";
        var expected = """
		<p>Paragraph 1</p>
		<hr />
		<p>Paragraph 2</p>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakBetweenParagraphsWithoutBlankLines()
    {
        var input = """
		Paragraph 1

		---
		Paragraph 2
		""";
        var expected = """
		<p>Paragraph 1</p>
		<hr />
		<p>Paragraph 2</p>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakAfterHeading()
    {
        var input = """
		# Heading
		---
		""";
        var expected = """
		<h1>Heading</h1>
		<hr />
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakBeforeHeading()
    {
        var input = """
		---
		# Heading
		""";
        var expected = """
		<hr />
		<h1>Heading</h1>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakWithCodeBlockAbove()
    {
        var input = """
		```
		code
		```
		---
		""";
        var expected = """
		<pre><code>code
		</code></pre>
		<hr />
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakWithCodeBlockBelow()
    {
        var input = """
		---
		```
		code
		```
		""";
        var expected = """
		<hr />
		<pre><code>code
		</code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakInNestedBlockquote()
    {
        var input = """
		> Quote
		>
		> ---
		> More quote
		""";
        var expected = """
		<blockquote>
		<p>Quote</p>
		<hr />
		<p>More quote</p>
		</blockquote>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakWithVeryLongSequence()
    {
        var input = "--------------------------------------------------";
        var expected = """
		<hr />
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakNotValidStartsWithDashButHasSpaces()
    {
        var input = "-   -   -";
        var expected = """
		<hr />
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakWithInlineElementsAbove()
    {
        var input = """
		Text with *emphasis*

		---
		""";
        var expected = """
		<p>Text with <em>emphasis</em></p>
		<hr />
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakWithInlineElementsBelow()
    {
        var input = """
		---
		Text with **bold**
		""";
        var expected = """
		<hr />
		<p>Text with <strong>bold</strong></p>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakBetweenBlockquotes()
    {
        var input = """
		> Quote 1

		---

		> Quote 2
		""";
        var expected = """
		<blockquote>
		<p>Quote 1</p>
		</blockquote>
		<hr />
		<blockquote>
		<p>Quote 2</p>
		</blockquote>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakWithSetextHeading()
    {
        var input = """
		Heading
		=======
		---
		""";
        var expected = """
		<h1>Heading</h1>
		<hr />
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakAfterOrderedList()
    {
        var input = """
		1. First
		2. Second
		---
		""";
        var expected = """
		<ol>
		<li>First</li>
		<li>Second</li>
		</ol>
		<hr />
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void TextThatLooksLikeThematicBreakButHasOtherContent()
    {
        var input = "--- text";
        var expected = """
		<p>--- text</p>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakPrecededByCodeFence()
    {
        var input = """
		```
		code
		```
		---
		""";
        var expected = """
		<pre><code>code
		</code></pre>
		<hr />
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakNotValidLessThan3CharsWithSpaces()
    {
        var input = "- -";
        var expected = """
		<ul>
		<li>
		<ul>
		<li></li>
		</ul>
		</li>
		</ul>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakAfterLooseList()
    {
        var input = """
		- Item 1

		- Item 2
		---
		""";
        var expected = """
		<ul>
		<li>
		<p>Item 1</p>
		</li>
		<li>
		<p>Item 2</p>
		</li>
		</ul>
		<hr />
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakInFencedCodeBlock()
    {
        var input = """
		```
		---
		```
		""";
        var expected = """
		<pre><code>---
		</code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakWithMixedSpacing()
    {
        var input = "  *  *  *  ";
        var expected = """
		<hr />
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ThematicBreakNotValidTextAfterSpaces()
    {
        var input = "---   text";
        var expected = """
		<p>---   text</p>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void EmptyThematicBreakShouldNotMatch()
    {
        var input = "";
        var expected = "";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, html.Trim());
    }
}
