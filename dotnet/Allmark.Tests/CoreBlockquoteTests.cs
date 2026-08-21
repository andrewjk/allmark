using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allmark.Rulesets;

namespace Allmark.Tests;

[TestClass]
public class CoreBlockquoteTests
{
    [TestMethod]
    public void SimpleBlockquote()
    {
        var input = @"
> Simple quote
";
        var expected = @"
<blockquote>
<p>Simple quote</p>
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
    public void BlockquoteWithMultipleLines()
    {
        var input = @"
> Line 1
> Line 2
> Line 3
";
        var expected = @"
<blockquote>
<p>Line 1
Line 2
Line 3</p>
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
    public void BlockquoteWithLazyContinuation()
    {
        var input = @"
> Line 1
Line 2
> Line 3
";
        var expected = @"
<blockquote>
<p>Line 1
Line 2
Line 3</p>
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
    public void BlockquoteWithSpaceAfter()
    {
        var input = @"
> With space
";
        var expected = @"
<blockquote>
<p>With space</p>
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
    public void BlockquoteWithoutSpaceAfter()
    {
        var input = @"
>Without space
";
        var expected = @"
<blockquote>
<p>Without space</p>
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
    public void BlockquoteWithMultipleParagraphs()
    {
        var input = @"
> Paragraph 1
>
> Paragraph 2
";
        var expected = @"
<blockquote>
<p>Paragraph 1</p>
<p>Paragraph 2</p>
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
    public void BlockquoteWithCodeBlock()
    {
        var input = @"
>     code block
>     more code
";
        var expected = @"
<blockquote>
<pre><code>code block
more code
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
    public void BlockquoteWithList()
    {
        var input = @"
> - Item 1
> - Item 2
";
        var expected = @"
<blockquote>
<ul>
<li>Item 1</li>
<li>Item 2</li>
</ul>
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
    public void BlockquoteWithNestedBlockquote()
    {
        var input = @"
> Outer
>> Inner
>>> Innerer
";
        var expected = @"
<blockquote>
<p>Outer</p>
<blockquote>
<p>Inner</p>
<blockquote>
<p>Innerer</p>
</blockquote>
</blockquote>
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
    public void BlockquoteWithHeading()
    {
        var input = @"
> # Heading
";
        var expected = @"
<blockquote>
<h1>Heading</h1>
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
    public void BlockquoteWithInlineEmphasis()
    {
        var input = @"
> *italic* and **bold**
";
        var expected = @"
<blockquote>
<p><em>italic</em> and <strong>bold</strong></p>
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
    public void BlockquoteWithInlineCode()
    {
        var input = @"
> `code` inside quote
";
        var expected = @"
<blockquote>
<p><code>code</code> inside quote</p>
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
    public void BlockquoteWithLink()
    {
        var input = @"
> [link](https://example.com)
";
        var expected = @"
<blockquote>
<p><a href=""https://example.com"">link</a></p>
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
    public void BlockquoteWith1SpaceIndent()
    {
        var input = @"
 > Indented quote
";
        var expected = @"
<blockquote>
<p>Indented quote</p>
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
    public void BlockquoteWith3SpaceIndent()
    {
        var input = @"
   > Indented quote
";
        var expected = @"
<blockquote>
<p>Indented quote</p>
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
    public void BlockquoteWith4SpaceIndentShouldBeCode()
    {
        var input = @"
    > Not a quote
";
        var expected = @"
<pre><code>&gt; Not a quote
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
    public void MultipleConsecutiveBlockquotes()
    {
        var input = @"
> Quote 1

> Quote 2
";
        var expected = @"
<blockquote>
<p>Quote 1</p>
</blockquote>
<blockquote>
<p>Quote 2</p>
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
    public void BlockquotePrecededByParagraphWithoutBlankLine()
    {
        var input = @"
Paragraph
> Quote
";
        var expected = @"
<p>Paragraph</p>
<blockquote>
<p>Quote</p>
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
    public void BlockquoteWithThematicBreak()
    {
        var input = @"
> Text
>
> ---
";
        var expected = @"
<blockquote>
<p>Text</p>
<hr />
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
    public void BlockquoteWithMultipleBlocks()
    {
        var input = @"
> Paragraph
>
> - List item
>
> Code:
>     code
";
        var expected = @"
<blockquote>
<p>Paragraph</p>
<ul>
<li>List item</li>
</ul>
<p>Code:
code</p>
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
    public void BlockquoteWithComplexNestedContent()
    {
        var input = @"
> Quote
>
>> Nested quote
>>
>> - List in nested
> Back to outer
";
        var expected = @"
<blockquote>
<p>Quote</p>
<blockquote>
<p>Nested quote</p>
<ul>
<li>List in nested
Back to outer</li>
</ul>
</blockquote>
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
    public void EmptyBlockquote()
    {
        var input = @"
>
";
        var expected = @"
<blockquote>
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
    public void BlockquoteWithOnlySpace()
    {
        var input = @"
> 
";
        var expected = @"
<blockquote>
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
    public void BlockquoteAtEndOfDocument()
    {
        var input = @"
> Last quote
";
        var expected = @"
<blockquote>
<p>Last quote</p>
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
    public void BlockquoteWithFencedCodeBlock()
    {
        var input = @"
> ```
> code
> ```
";
        var expected = @"
<blockquote>
<pre><code>code
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
    public void BlockquoteWithOrderedList()
    {
        var input = @"
> 1. First
> 2. Second
";
        var expected = @"
<blockquote>
<ol>
<li>First</li>
<li>Second</li>
</ol>
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
    public void BlockquoteWithSetextHeading()
    {
        var input = @"
> Heading
> =======
";
        var expected = @"
<blockquote>
<h1>Heading</h1>
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
    public void BlockquoteWithHTMLBlock()
    {
        var input = @"
> <div>HTML</div>
";
        var expected = @"
<blockquote>
<div>HTML</div>
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
    public void BlockquoteWithHardLineBreaks()
    {
        var input = @"
> Line 1  
> Line 2
";
        var expected = @"
<blockquote>
<p>Line 1<br />
Line 2</p>
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
    public void BlockquoteWithImage()
    {
        var input = @"
> ![alt](image.png)
";
        var expected = @"
<blockquote>
<p><img src=""image.png"" alt=""alt"" /></p>
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
    public void DeeplyNestedBlockquotes()
    {
        var input = @"
> Level 1
>> Level 2
>>> Level 3
>>>> Level 4
";
        var expected = @"
<blockquote>
<p>Level 1</p>
<blockquote>
<p>Level 2</p>
<blockquote>
<p>Level 3</p>
<blockquote>
<p>Level 4</p>
</blockquote>
</blockquote>
</blockquote>
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
    public void BlockquoteWithMixedLazyContinuation()
    {
        var input = @"
> Line 1
> Line 2
Line 3 (lazy)
> Line 4
";
        var expected = @"
<blockquote>
<p>Line 1
Line 2
Line 3 (lazy)
Line 4</p>
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
    public void BlockquoteWithLooseList()
    {
        var input = @"
> - Item 1
>
> - Item 2
";
        var expected = @"
<blockquote>
<ul>
<li>
<p>Item 1</p>
</li>
<li>
<p>Item 2</p>
</li>
</ul>
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
    public void BlockquoteWithTightList()
    {
        var input = @"
> - Item 1
> - Item 2
";
        var expected = @"
<blockquote>
<ul>
<li>Item 1</li>
<li>Item 2</li>
</ul>
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
}