using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allmark.Rulesets;

namespace Allmark.Tests;

[TestClass]
public class GfmAlertTests
{
    [TestMethod]
    public void SpecAlert()
    {
        var input = @"
> [!NOTE]
> Useful information that users should know, even when skimming content.
";
        var expected = @"
<div class=""markdown-alert markdown-alert-note"">
<p class=""markdown-alert-title"">Note</p>
<p>Useful information that users should know, even when skimming content.</p>
</div>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r\n", "\n").Replace("\r", "\n"));
    }

    [TestMethod]
    public void AlertTip()
    {
        var input = @"
> [!TIP]
> Helpful advice for doing things better or more easily.
";
        var expected = @"
<div class=""markdown-alert markdown-alert-tip"">
<p class=""markdown-alert-title"">Tip</p>
<p>Helpful advice for doing things better or more easily.</p>
</div>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r\n", "\n").Replace("\r", "\n"));
    }

    [TestMethod]
    public void AlertImportant()
    {
        var input = @"
> [!IMPORTANT]
> Key information users need to know to achieve their goal.
";
        var expected = @"
<div class=""markdown-alert markdown-alert-important"">
<p class=""markdown-alert-title"">Important</p>
<p>Key information users need to know to achieve their goal.</p>
</div>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r\n", "\n").Replace("\r", "\n"));
    }

    [TestMethod]
    public void AlertWarning()
    {
        var input = @"
> [!WARNING]
> Urgent info that needs immediate user attention to avoid problems.
";
        var expected = @"
<div class=""markdown-alert markdown-alert-warning"">
<p class=""markdown-alert-title"">Warning</p>
<p>Urgent info that needs immediate user attention to avoid problems.</p>
</div>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r\n", "\n").Replace("\r", "\n"));
    }

    [TestMethod]
    public void AlertCaution()
    {
        var input = @"
> [!CAUTION]
> Advises about risks or negative outcomes of certain actions.
";
        var expected = @"
<div class=""markdown-alert markdown-alert-caution"">
<p class=""markdown-alert-title"">Caution</p>
<p>Advises about risks or negative outcomes of certain actions.</p>
</div>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r\n", "\n").Replace("\r", "\n"));
    }

    [TestMethod]
    public void AlertWithMultipleParagraphs()
    {
        var input = @"
> [!NOTE]
> First paragraph of note.
>
> Second paragraph of note.
";
        var expected = @"
<div class=""markdown-alert markdown-alert-note"">
<p class=""markdown-alert-title"">Note</p>
<p>First paragraph of note.</p>
<p>Second paragraph of note.</p>
</div>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r\n", "\n").Replace("\r", "\n"));
    }

    [TestMethod]
    public void AlertWithInlineFormatting()
    {
        var input = @"
> [!NOTE]
> This is **bold** and this is *italic* and this is `code`.
";
        var expected = @"
<div class=""markdown-alert markdown-alert-note"">
<p class=""markdown-alert-title"">Note</p>
<p>This is <strong>bold</strong> and this is <em>italic</em> and this is <code>code</code>.</p>
</div>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r\n", "\n").Replace("\r", "\n"));
    }

    [TestMethod]
    public void AlertWithList()
    {
        var input = @"
> [!NOTE]
> Some important points:
> - First point
> - Second point
> - Third point
";
        var expected = @"
<div class=""markdown-alert markdown-alert-note"">
<p class=""markdown-alert-title"">Note</p>
<p>Some important points:</p>
<ul>
<li>First point</li>
<li>Second point</li>
<li>Third point</li>
</ul>
</div>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r\n", "\n").Replace("\r", "\n"));
    }

    [TestMethod]
    public void AlertWithCodeBlock()
    {
        var input = @"
> [!NOTE]
> Example code:
>
> ```
> console.log(""Hello World"");
> ```
";
        var expected = @"
<div class=""markdown-alert markdown-alert-note"">
<p class=""markdown-alert-title"">Note</p>
<p>Example code:</p>
<pre><code>console.log(&quot;Hello World&quot;);
</code></pre>
</div>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r\n", "\n").Replace("\r", "\n"));
    }

    [TestMethod]
    public void AlertWithLink()
    {
        var input = @"
> [!NOTE]
> Check out [documentation](https://example.com) for more info.
";
        var expected = @"
<div class=""markdown-alert markdown-alert-note"">
<p class=""markdown-alert-title"">Note</p>
<p>Check out <a href=""https://example.com"">documentation</a> for more info.</p>
</div>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r\n", "\n").Replace("\r", "\n"));
    }

    [TestMethod]
    public void AlertCaseInsensitive()
    {
        var input = @"
> [!note]
> This should work with lowercase.
";
        var expected = @"
<div class=""markdown-alert markdown-alert-note"">
<p class=""markdown-alert-title"">Note</p>
<p>This should work with lowercase.</p>
</div>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r\n", "\n").Replace("\r", "\n"));
    }

    [TestMethod]
    public void NonAlertBlockquote()
    {
        var input = @"
> This is just a regular blockquote.
> It should not be treated as an alert.
";
        var expected = @"
<blockquote>
<p>This is just a regular blockquote.
It should not be treated as an alert.</p>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r\n", "\n").Replace("\r", "\n"));
    }

    [TestMethod]
    public void BlockquoteWithBracketsButNotAlert()
    {
        var input = @"
> [NOTE] This is not an alert syntax.
> It should be a regular blockquote.
";
        var expected = @"
<blockquote>
<p>[NOTE] This is not an alert syntax.
It should be a regular blockquote.</p>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r\n", "\n").Replace("\r", "\n"));
    }

    [TestMethod]
    public void AlertWithNestedBlockquote()
    {
        var input = @"
> [!NOTE]
> Outer alert content.
>
> > Nested blockquote inside alert.
";
        var expected = @"
<div class=""markdown-alert markdown-alert-note"">
<p class=""markdown-alert-title"">Note</p>
<p>Outer alert content.</p>
<blockquote>
<p>Nested blockquote inside alert.</p>
</blockquote>
</div>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r\n", "\n").Replace("\r", "\n"));
    }

    [TestMethod]
    public void ConsecutiveAlerts()
    {
        var input = @"
> [!NOTE]
> First alert.

> [!WARNING]
> Second alert.
";
        var expected = @"
<div class=""markdown-alert markdown-alert-note"">
<p class=""markdown-alert-title"">Note</p>
<p>First alert.</p>
</div>
<div class=""markdown-alert markdown-alert-warning"">
<p class=""markdown-alert-title"">Warning</p>
<p>Second alert.</p>
</div>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r\n", "\n").Replace("\r", "\n"));
    }

    [TestMethod]
    public void AlertWithEmptyContent()
    {
        var input = @"
> [!NOTE]
>
>
> Content after empty line.
";
        var expected = @"
<div class=""markdown-alert markdown-alert-note"">
<p class=""markdown-alert-title"">Note</p>
<p>Content after empty line.</p>
</div>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r\n", "\n").Replace("\r", "\n"));
    }
}
