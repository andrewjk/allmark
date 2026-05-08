using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allmark.Rulesets;

namespace Allmark.Tests;

[TestClass]
public class ExtDeletionTests
{
    [TestMethod]
    public void DeletionSingle()
    {
        var input = @"
This text was {-deleted-} recently.
";
        var expected = @"
<p>This text was <del class=""markdown-deletion"">deleted</del> recently.</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void DeletionDouble()
    {
        var input = @"
This text was {--deleted--} recently.
";
        var expected = @"
<p>This text was <del class=""markdown-deletion"">deleted</del> recently.</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void DeletionTriple()
    {
        var input = @"
This text was {---deleted---} recently.
";
        var expected = @"
<p>This text was {---deleted---} recently.</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void DeletionSingleCharacter()
    {
        var input = @"
text {-a-} more
";
        var expected = @"
<p>text <del class=""markdown-deletion"">a</del> more</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void DeletionWithSpaces()
    {
        var input = @"
text {-with spaces-} more
";
        var expected = @"
<p>text <del class=""markdown-deletion"">with spaces</del> more</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void DeletionAtStartOfParagraph()
    {
        var input = @"
{-deleted-} This is new.
";
        var expected = @"
<p><del class=""markdown-deletion"">deleted</del> This is new.</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void DeletionAtEndOfParagraph()
    {
        var input = @"
This is {-deleted-}
";
        var expected = @"
<p>This is <del class=""markdown-deletion"">deleted</del></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void DeletionWithPunctuation()
    {
        var input = @"
text {-word!-} more
";
        var expected = @"
<p>text <del class=""markdown-deletion"">word!</del> more</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void DeletionWithSpecialCharacters()
    {
        var input = @"
text {-a-b-} more
";
        var expected = @"
<p>text <del class=""markdown-deletion"">a-b</del> more</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void DeletionAdjacentToText()
    {
        var input = @"
test{-ing-}test
";
        var expected = @"
<p>test<del class=""markdown-deletion"">ing</del>test</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void EmptyDeletion()
    {
        var input = @"
text{--}text
";
        var expected = @"
<p>text{--}text</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void DeletionWithMarkdownInside()
    {
        var input = @"
text {-**bold**-}
";
        var expected = @"
<p>text <del class=""markdown-deletion""><strong>bold</strong></del></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void DeletionWithCodeInside()
    {
        var input = @"
text {-`code`-}
";
        var expected = @"
<p>text <del class=""markdown-deletion""><code>code</code></del></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void EscapedBracesShouldNotBeDeletion()
    {
        var input = @"
text \{-not deletion\-}
";
        var expected = @"
<p>text {-not deletion-}</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void UnmatchedOpeningDeletion()
    {
        var input = @"
text {-not closed
";
        var expected = @"
<p>text {-not closed</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void UnmatchedClosingDeletion()
    {
        var input = @"
text not opened-}
";
        var expected = @"
<p>text not opened-}</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void DeletionInListItem()
    {
        var input = @"
- Item with {-deletion-}
";
        var expected = @"
<ul>
<li>Item with <del class=""markdown-deletion"">deletion</del></li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void DeletionInBlockquote()
    {
        var input = @"
> Quote with {-deletion-}
";
        var expected = @"
<blockquote>
<p>Quote with <del class=""markdown-deletion"">deletion</del></p>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void DeletionWithPlusInside()
    {
        var input = @"
text {-plus - inside-}
";
        var expected = @"
<p>text <del class=""markdown-deletion"">plus - inside</del></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void DeletionAtBeginningOfDocument()
    {
        var input = @"
{-Start-} of document.
";
        var expected = @"
<p><del class=""markdown-deletion"">Start</del> of document.</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void DeletionAtEndOfDocument()
    {
        var input = @"
End of {-document-}
";
        var expected = @"
<p>End of <del class=""markdown-deletion"">document</del></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void MultipleDeletionsInOneLine()
    {
        var input = @"
{-first-} and {-second-} and {-third-}
";
        var expected = @"
<p><del class=""markdown-deletion"">first</del> and <del class=""markdown-deletion"">second</del> and <del class=""markdown-deletion"">third</del></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void DeletionWithStartingEmphasis()
    {
        var input = @"
{-deleted *text-} that shouldn't be bold*
";
        var expected = @"
<p><del class=""markdown-deletion"">deleted *text</del> that shouldn't be bold*</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void DeletionWithEndingEmphasis()
    {
        var input = @"
*this text should be {-deleted but not bold*-}
";
        var expected = @"
<p>*this text should be <del class=""markdown-deletion"">deleted but not bold*</del></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }
}