using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allmark.Rulesets;

namespace Allmark.Tests;

[TestClass]
public class ExtCommentTests
{
    [TestMethod]
    public void CommentBasic()
    {
        var input = @"
This text was {>>commented<<} recently.
 
";
        var expected = @"
<p>This text was <span class=""markdown-comment"">commented</span> recently.</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void CommentSingleCharacter()
    {
        var input = @"
text {>>a<<} more
";
        var expected = @"
<p>text <span class=""markdown-comment"">a</span> more</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void CommentWithSpaces()
    {
        var input = @"
text {>>with spaces<<} more
";
        var expected = @"
<p>text <span class=""markdown-comment"">with spaces</span> more</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void CommentAtStartOfParagraph()
    {
        var input = @"
{>>commented<<} This is new.
";
        var expected = @"
<p><span class=""markdown-comment"">commented</span> This is new.</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void CommentAtEndOfParagraph()
    {
        var input = @"
This is {>>commented<<}
";
        var expected = @"
<p>This is <span class=""markdown-comment"">commented</span></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void CommentWithPunctuation()
    {
        var input = @"
text {>>word!<<} more
";
        var expected = @"
<p>text <span class=""markdown-comment"">word!</span> more</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void CommentWithSpecialCharacters()
    {
        var input = @"
text {>>a-b<<} more
";
        var expected = @"
<p>text <span class=""markdown-comment"">a-b</span> more</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void CommentAdjacentToText()
    {
        var input = @"
test{>>ing<<}test
";
        var expected = @"
<p>test<span class=""markdown-comment"">ing</span>test</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void EmptyComment()
    {
        var input = @"
text{>><<}text
";
        var expected = @"
<p>text{&gt;&gt;&lt;&lt;}text</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void CommentWithMarkdownInside()
    {
        var input = @"
text {>>**bold**<<}
";
        var expected = @"
<p>text <span class=""markdown-comment""><strong>bold</strong></span></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void CommentWithCodeInside()
    {
        var input = @"
text {>>`code`<<}
";
        var expected = @"
<p>text <span class=""markdown-comment""><code>code</code></span></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void EscapedBracesShouldNotBeComment()
    {
        var input = @"
text \{>>not comment<<\}
";
        var expected = @"
<p>text {&gt;&gt;not comment&lt;&lt;}</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void UnmatchedOpeningComment()
    {
        var input = @"
text {>>not closed
";
        var expected = @"
<p>text {&gt;&gt;not closed</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void UnmatchedClosingComment()
    {
        var input = @"
text not opened<<}
";
        var expected = @"
<p>text not opened&lt;&lt;}</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void CommentInListItem()
    {
        var input = @"
- Item with {>>comment<<}
";
        var expected = @"
<ul>
<li>Item with <span class=""markdown-comment"">comment</span></li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void CommentInBlockquote()
    {
        var input = @"
> Quote with {>>comment<<}
";
        var expected = @"
<blockquote>
<p>Quote with <span class=""markdown-comment"">comment</span></p>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void CommentWithAngleBracketsInside()
    {
        var input = @"
text {>>some <text> inside<<}
";
        var expected = @"
<p>text <span class=""markdown-comment"">some <text> inside</span></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void CommentAtBeginningOfDocument()
    {
        var input = @"
{>>Start<<} of document.
";
        var expected = @"
<p><span class=""markdown-comment"">Start</span> of document.</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void CommentAtEndOfDocument()
    {
        var input = @"
End of {>>document<<}
";
        var expected = @"
<p>End of <span class=""markdown-comment"">document</span></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void MultipleCommentsInOneLine()
    {
        var input = @"
{>>first<<} and {>>second<<} and {>>third<<}
";
        var expected = @"
<p><span class=""markdown-comment"">first</span> and <span class=""markdown-comment"">second</span> and <span class=""markdown-comment"">third</span></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void CommentWithStartingEmphasis()
    {
        var input = @"
{>>comment *text<<} that shouldn't be bold*
";
        var expected = @"
<p><span class=""markdown-comment"">comment *text</span> that shouldn't be bold*</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void CommentWithEndingEmphasis()
    {
        var input = @"
*this text should be {>>commented but not bold*<<}
";
        var expected = @"
<p>*this text should be <span class=""markdown-comment"">commented but not bold*</span></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void CommentWithPlusSignsInside()
    {
        var input = @"
text {>>plus + sign<<}
";
        var expected = @"
<p>text <span class=""markdown-comment"">plus + sign</span></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void CommentWithMinusSignsInside()
    {
        var input = @"
text {>>minus - sign<<}
";
        var expected = @"
<p>text <span class=""markdown-comment"">minus - sign</span></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void CommentNestedWithOtherCriticMarks()
    {
        var input = @"
text {+insertion {>>comment<<} end+}
";
        var expected = @"
<p>text <ins class=""markdown-insertion"">insertion <span class=""markdown-comment"">comment</span> end</ins></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }
}