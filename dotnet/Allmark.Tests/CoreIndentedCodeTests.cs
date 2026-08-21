using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allmark.Rulesets;

namespace Allmark.Tests;

[TestClass]
public class CoreIndentedCodeTests
{
    [TestMethod]
    public void Simple4SpaceIndentedCode()
    {
        var input = @"
    code here
";
        var expected = @"
<pre><code>code here
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void TabIndentedCode()
    {
        var input = @"
	code here
";
        var expected = @"
<pre><code>code here
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void MultiLineIndentedCode()
    {
        var input = @"
    line 1
    line 2
    line 3
";
        var expected = @"
<pre><code>line 1
line 2
line 3
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void LessThan4SpacesShouldBeParagraph()
    {
        var input = @"
   code here
";
        var expected = @"
<p>code here</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void _5SpaceIndentedCode()
    {
        var input = @"
     code here
";
        var expected = @"
<pre><code> code here
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void _8SpaceIndentedCode()
    {
        var input = @"
        code here
";
        var expected = @"
<pre><code>    code here
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void EmptyIndentedCodeBlock()
    {
        var input = @"
    
    
";
        var expected = @"
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeBlockWithBlankLineInMiddle()
    {
        var input = @"
    line 1

    line 2
";
        var expected = @"
<pre><code>line 1

line 2
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeBlockInterruptsParagraphWithBlankLine()
    {
        var input = @"
Paragraph

    code here
";
        var expected = @"
<p>Paragraph</p>
<pre><code>code here
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeBlockDoesNotInterruptParagraphWithoutBlankLine()
    {
        var input = @"
Paragraph
    code here
";
        var expected = @"
<p>Paragraph
code here</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeBlockWithTrailingSpaces()
    {
        var input = @"
    code here  
";
        var expected = @"
<pre><code>code here  
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Mixed4SpaceAnd8SpaceIndentation()
    {
        var input = @"
    line 1
        line 2
";
        var expected = @"
<pre><code>line 1
    line 2
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    // TODO:
    [Ignore]
    [TestMethod]
    public void TabAfter4Spaces8SpacesTotal()
    {
        var input = @"
    	code here
";
        var expected = @"
<pre><code>	code here
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeWithBackticks()
    {
        var input = @"
    `code`
";
        var expected = @"
<pre><code>`code`
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeWithTildes()
    {
        var input = @"
    ~code~
";
        var expected = @"
<pre><code>~code~
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeWithAsterisks()
    {
        var input = @"
    **bold**
";
        var expected = @"
<pre><code>**bold**
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeInBlockquote()
    {
        var input = @"
>     code here
";
        var expected = @"
<blockquote>
<pre><code>code here
</code></pre>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeInListItem()
    {
        var input = @"
-     code here
";
        var expected = @"
<ul>
<li>
<pre><code>code here
</code></pre>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeInOrderedList()
    {
        var input = @"
1.     code here
";
        var expected = @"
<ol>
<li>
<pre><code>code here
</code></pre>
</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeFollowedByParagraph()
    {
        var input = @"
    code here

Paragraph
";
        var expected = @"
<pre><code>code here
</code></pre>
<p>Paragraph</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void ParagraphFollowedByIndentedCode()
    {
        var input = @"
Paragraph

    code here
";
        var expected = @"
<p>Paragraph</p>
<pre><code>code here
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void MultipleIndentedCodeBlocks()
    {
        var input = @"
    code 1

    code 2
";
        var expected = @"
<pre><code>code 1

code 2
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeWithSpecialCharacters()
    {
        var input = @"
    <>& ""'\
";
        var expected = @"
<pre><code>&lt;&gt;&amp; &quot;'\
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeWithMixedIndentation()
    {
        var input = @"
    line 1
      line 2
  line 3
";
        var expected = @"
<pre><code>line 1
  line 2
</code></pre>
<p>line 3</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeBlockAfterHeading()
    {
        var input = @"
# Heading

    code here
";
        var expected = @"
<h1>Heading</h1>
<pre><code>code here
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeBlockBeforeHeading()
    {
        var input = @"
    code here

# Heading
";
        var expected = @"
<pre><code>code here
</code></pre>
<h1>Heading</h1>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeBlockAfterThematicBreak()
    {
        var input = @"
---

    code here
";
        var expected = @"
<hr />
<pre><code>code here
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeBlockBeforeThematicBreak()
    {
        var input = @"
    code here

---
";
        var expected = @"
<pre><code>code here
</code></pre>
<hr />
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeWithFencedCodeBlockAbove()
    {
        var input = @"
```
 fenced code
```
    indented code
";
        var expected = @"
<pre><code> fenced code
</code></pre>
<pre><code>indented code
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeWithFencedCodeBlockBelow()
    {
        var input = @"
    indented code
```
 fenced code
```
";
        var expected = @"
<pre><code>indented code
</code></pre>
<pre><code> fenced code
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeWithATXHeadingAbove()
    {
        var input = @"
# Heading

    code here
";
        var expected = @"
<h1>Heading</h1>
<pre><code>code here
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeWithATXHeadingBelow()
    {
        var input = @"
    code here

# Heading
";
        var expected = @"
<pre><code>code here
</code></pre>
<h1>Heading</h1>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeWithSetextHeadingAbove()
    {
        var input = @"
Heading
=======

    code here
";
        var expected = @"
<h1>Heading</h1>
<pre><code>code here
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeWithSetextHeadingBelow()
    {
        var input = @"
    code here

Heading
=======
";
        var expected = @"
<pre><code>code here
</code></pre>
<h1>Heading</h1>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodePrecededByParagraphWithoutBlankLine()
    {
        var input = @"
Paragraph
    code here
";
        var expected = @"
<p>Paragraph
code here</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void ParagraphPrecededByIndentedCodeWithoutBlankLine()
    {
        var input = @"
    code here
Paragraph
";
        var expected = @"
<pre><code>code here
</code></pre>
<p>Paragraph</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeWithHTMLEntities()
    {
        var input = @"
    &lt;code&gt;
";
        var expected = @"
<pre><code>&amp;lt;code&amp;gt;
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeBlockInNestedList()
    {
        var input = @"
-     code 1
-     code 2
";
        var expected = @"
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
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeBlockAtEndOfDocument()
    {
        var input = @"
    code here
";
        var expected = @"
<pre><code>code here
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeBlockWithOnlyWhitespace()
    {
        var input = @"
    
    
    
";
        var expected = @"
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeBlockWithVaryingIndentation()
    {
        var input = @"
    level 1
      level 2
  level 3
";
        var expected = @"
<pre><code>level 1
  level 2
</code></pre>
<p>level 3</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void SingleTabIndented()
    {
        var input = @"
	code here
";
        var expected = @"
<pre><code>code here
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    // TODO:
    [Ignore]
    [TestMethod]
    public void DoubleTabIndented()
    {
        var input = @"
		code here
";
        var expected = @"
<pre><code>	code here
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void MixedTabAndSpaceIndentation()
    {
        var input = @"
	    code here
";
        var expected = @"
<pre><code>    code here
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void _3SpacesShouldBeParagraph()
    {
        var input = @"
   code here
";
        var expected = @"
<p>code here</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void _6SpacesIndentedCode()
    {
        var input = @"
      code here
";
        var expected = @"
<pre><code>  code here
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void _12SpacesIndentedCode()
    {
        var input = @"
            code here
";
        var expected = @"
<pre><code>        code here
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void CodeBlockWithUnicodeCharacters()
    {
        var input = @"
    hello 世界
";
        var expected = @"
<pre><code>hello 世界
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeWithInlineLink()
    {
        var input = @"
    [link](https://example.com)
";
        var expected = @"
<pre><code>[link](https://example.com)
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeWithInlineImage()
    {
        var input = @"
    ![alt](image.png)
";
        var expected = @"
<pre><code>![alt](image.png)
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeWithEmphasis()
    {
        var input = @"
    *italic*
";
        var expected = @"
<pre><code>*italic*
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeWithStrong()
    {
        var input = @"
    **bold**
";
        var expected = @"
<pre><code>**bold**
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void IndentedCodeWithInlineCode()
    {
        var input = @"
    `inline code`
";
        var expected = @"
<pre><code>`inline code`
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }
}