using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allmark.Rulesets;

namespace Allmark.Tests;

[TestClass]
public class IndentedCodeTests
{
	[TestMethod]
	public void Simple4SpaceIndentedCode()
	{
		var input = "    code here";
		var expected = """
		<pre><code>code here
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void TabIndentedCode()
	{
		var input = "\tcode here";
		var expected = """
		<pre><code>code here
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void MultilineIndentedCode()
	{
		var input = """
		    line 1
		    line 2
		    line 3
		""";
		var expected = """
		<pre><code>line 1
		line 2
		line 3
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void LessThan4SpacesShouldBeParagraph()
	{
		var input = "   code here";
		var expected = """
		<p>code here</p>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void FiveSpaceIndentedCode()
	{
		var input = "     code here";
		var expected = """
		<pre><code> code here
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void EightSpaceIndentedCode()
	{
		var input = "        code here";
		var expected = """
		<pre><code>    code here
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodeBlockWithBlankLineInMiddle()
	{
		var input = """
		    line 1

		    line 2
		""";
		var expected = """
		<pre><code>line 1

		line 2
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodeBlockInterruptsParagraphWithBlankLine()
	{
		var input = """
		Paragraph

		    code here
		""";
		var expected = """
		<p>Paragraph</p>
		<pre><code>code here
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodeBlockDoesNotInterruptParagraphWithoutBlankLine()
	{
		var input = """
		Paragraph
		    code here
		""";
		var expected = """
		<p>Paragraph
		code here</p>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodeBlockWithTrailingSpaces()
	{
		var input = "    code here  ";
		var expected = """
		<pre><code>code here  
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void Mixed4SpaceAnd8SpaceIndentation()
	{
		var input = """
		    line 1
		        line 2
		""";
		var expected = """
		<pre><code>line 1
		    line 2
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodeWithBackticks()
	{
		var input = "    `code`";
		var expected = """
		<pre><code>`code`
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodeWithTildes()
	{
		var input = "    ~code~";
		var expected = """
		<pre><code>~code~
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodeWithAsterisks()
	{
		var input = "    **bold**";
		var expected = """
		<pre><code>**bold**
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodeInBlockquote()
	{
		var input = ">     code here";
		var expected = """
		<blockquote>
		<pre><code>code here
		</code></pre>
		</blockquote>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodeInListItem()
	{
		var input = "-     code here";
		var expected = """
		<ul>
		<li>
		<pre><code>code here
		</code></pre>
		</li>
		</ul>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodeInOrderedList()
	{
		var input = "1.     code here";
		var expected = """
		<ol>
		<li>
		<pre><code>code here
		</code></pre>
		</li>
		</ol>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodeFollowedByParagraph()
	{
		var input = """
		    code here

		Paragraph
		""";
		var expected = """
		<pre><code>code here
		</code></pre>
		<p>Paragraph</p>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void ParagraphFollowedByIndentedCode()
	{
		var input = """
		Paragraph

		    code here
		""";
		var expected = """
		<p>Paragraph</p>
		<pre><code>code here
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void MultipleIndentedCodeBlocks()
	{
		var input = """
		    code 1

		    code 2
		""";
		var expected = """
		<pre><code>code 1

		code 2
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodeWithSpecialCharacters()
	{
		var input = "    <>& \"'\\";
		var expected = """
		<pre><code>&lt;&gt;&amp; &quot;'\
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodeWithMixedIndentation()
	{
		var input = """
		    line 1
		      line 2
		  line 3
		""";
		var expected = """
		<pre><code>line 1
		  line 2
		</code></pre>
		<p>line 3</p>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodeBlockAfterHeading()
	{
		var input = """
		# Heading

		    code here
		""";
		var expected = """
		<h1>Heading</h1>
		<pre><code>code here
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodeBlockBeforeHeading()
	{
		var input = """
		    code here

		# Heading
		""";
		var expected = """
		<pre><code>code here
		</code></pre>
		<h1>Heading</h1>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodeBlockAfterThematicBreak()
	{
		var input = """
		---

		    code here
		""";
		var expected = """
		<hr />
		<pre><code>code here
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodeBlockBeforeThematicBreak()
	{
		var input = """
		    code here

		---
		""";
		var expected = """
		<pre><code>code here
		</code></pre>
		<hr />
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodeWithFencedCodeBlockAbove()
	{
		var input = """
		```
		 fenced code
		```
		    indented code
		""";
		var expected = """
		<pre><code> fenced code
		</code></pre>
		<pre><code>indented code
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodeWithFencedCodeBlockBelow()
	{
		var input = """
		    indented code
		```
		 fenced code
		```
		""";
		var expected = """
		<pre><code>indented code
		</code></pre>
		<pre><code> fenced code
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodeWithAtxHeadingAbove()
	{
		var input = """
		# Heading

		    code here
		""";
		var expected = """
		<h1>Heading</h1>
		<pre><code>code here
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodeWithAtxHeadingBelow()
	{
		var input = """
		    code here

		# Heading
		""";
		var expected = """
		<pre><code>code here
		</code></pre>
		<h1>Heading</h1>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodeWithSetextHeadingAbove()
	{
		var input = """
		Heading
		=======

		    code here
		""";
		var expected = """
		<h1>Heading</h1>
		<pre><code>code here
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodeWithSetextHeadingBelow()
	{
		var input = """
		    code here

		Heading
		=======
		""";
		var expected = """
		<pre><code>code here
		</code></pre>
		<h1>Heading</h1>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodePrecededByParagraphWithoutBlankLine()
	{
		var input = """
		Paragraph
		    code here
		""";
		var expected = """
		<p>Paragraph
		code here</p>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void ParagraphPrecededByIndentedCodeWithoutBlankLine()
	{
		var input = """
		    code here
		Paragraph
		""";
		var expected = """
		<pre><code>code here
		</code></pre>
		<p>Paragraph</p>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodeWithHTMLEntities()
	{
		var input = "    &lt;code&gt;";
		var expected = """
		<pre><code>&amp;lt;code&amp;gt;
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodeBlockInNestedList()
	{
		var input = """
		-     code 1
		-     code 2
		""";
		var expected = """
		<ul>
		<li>
		<pre><code>code 1
		</code></pre>
		</li>
		<li>
		<pre><code>code 2
		</code></pre>
		</li>
		</ul>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodeBlockAtEndOfDocument()
	{
		var input = "    code here";
		var expected = """
		<pre><code>code here
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodeBlockWithVaryingIndentation()
	{
		var input = """
		    level 1
		      level 2
		  level 3
		""";
		var expected = """
		<pre><code>level 1
		  level 2
		</code></pre>
		<p>level 3</p>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SingleTabIndented()
	{
		var input = "\tcode here";
		var expected = """
		<pre><code>code here
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void MixedTabAndSpaceIndentation()
	{
		var input = "\t    code here";
		var expected = """
		<pre><code>    code here
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void ThreeSpacesShouldBeParagraph()
	{
		var input = "   code here";
		var expected = """
		<p>code here</p>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SixSpacesIndentedCode()
	{
		var input = "      code here";
		var expected = """
		<pre><code>  code here
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void TwelveSpacesIndentedCode()
	{
		var input = "            code here";
		var expected = """
		<pre><code>        code here
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void CodeBlockWithUnicodeCharacters()
	{
		var input = "    hello 世界";
		var expected = """
		<pre><code>hello 世界
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodeWithInlineLink()
	{
		var input = "    [link](https://example.com)";
		var expected = """
		<pre><code>[link](https://example.com)
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodeWithInlineImage()
	{
		var input = "    ![alt](image.png)";
		var expected = """
		<pre><code>![alt](image.png)
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodeWithEmphasis()
	{
		var input = "    *italic*";
		var expected = """
		<pre><code>*italic*
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void IndentedCodeWithStrong()
	{
		var input = "    **bold**";
		var expected = """
		<pre><code>**bold**
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void EmptyIndentedCodeBlock()
	{
		var input = "    \n    ";
		var expected = "";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected, html.Trim());
	}

	[TestMethod]
	public void IndentedCodeBlockWithOnlyWhitespace()
	{
		var input = "    \n    \n    ";
		var expected = "";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected, html.Trim());
	}

	[TestMethod]
	public void IndentedCodeWithInlineCode()
	{
		var input = "    `inline code`";
		var expected = """
		<pre><code>`inline code`
		</code></pre>
		""";
		var doc = Parser.Execute(input, Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}
}
