using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allmark.Rulesets;

namespace Allmark.Tests;

[TestClass]
public class ExtSubscriptTests
{
    [TestMethod]
    public void SubscriptSingle()
    {
        var input = @"
This should be ~down~ below everything else.
";
        var expected = @"
<p>This should be <sub>down</sub> below everything else.</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void SubscriptDouble()
    {
        var input = @"
This should be ~~down~~ below everything else.
";
        var expected = @"
<p>This should be <del>down</del> below everything else.</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void SubscriptTriple()
    {
        var input = @"
This should be ~~~down~~~ below everything else.
";
        var expected = @"
<p>This should be ~~~down~~~ below everything else.</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void SubscriptSingleCharacter()
    {
        var input = @"
H~2~O
";
        var expected = @"
<p>H<sub>2</sub>O</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void SubscriptWithNumbers()
    {
        var input = @"
x~1~ + x~2~
";
        var expected = @"
<p>x<sub>1</sub> + x<sub>2</sub></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void MultipleSubscriptsInOneLine()
    {
        var input = @"
a~i~ + b~j~ = c~k~
";
        var expected = @"
<p>a<sub>i</sub> + b<sub>j</sub> = c<sub>k</sub></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void SubscriptAtStartOfParagraph()
    {
        var input = @"
~note~ This is important.
";
        var expected = @"
<p><sub>note</sub> This is important.</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void SubscriptAtEndOfParagraph()
    {
        var input = @"
See index~1~
";
        var expected = @"
<p>See index<sub>1</sub></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void SubscriptWithPunctuation()
    {
        var input = @"
Hello~world!~
";
        var expected = @"
<p>Hello<sub>world!</sub></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void SubscriptWithSpaces()
    {
        var input = @"
text ~with spaces~ more
";
        var expected = @"
<p>text <sub>with spaces</sub> more</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void SubscriptWithSpecialCharacters()
    {
        var input = @"
math~i+j~
";
        var expected = @"
<p>math<sub>i+j</sub></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void SubscriptAdjacentToText()
    {
        var input = @"
test~ing~test
";
        var expected = @"
<p>test<sub>ing</sub>test</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void EmptySubscript()
    {
        var input = @"
text~~text
";
        var expected = @"
<p>text~~text</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void SubscriptWithMarkdownInside()
    {
        var input = @"
text ~**bold**~
";
        var expected = @"
<p>text <sub><strong>bold</strong></sub></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void SubscriptWithCodeInside()
    {
        var input = @"
text ~`code`~
";
        var expected = @"
<p>text <sub><code>code</code></sub></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void EscapedTildeShouldNotBeSubscript()
    {
        var input = @"
text \~not subscript\~
";
        var expected = @"
<p>text ~not subscript~</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void UnmatchedOpeningTilde()
    {
        var input = @"
text ~not closed
";
        var expected = @"
<p>text ~not closed</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void UnmatchedClosingTilde()
    {
        var input = @"
text not opened~
";
        var expected = @"
<p>text not opened~</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void SubscriptInListItem()
    {
        var input = @"
- Item with ~subscript~
";
        var expected = @"
<ul>
<li>Item with <sub>subscript</sub></li>
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
    public void SubscriptInBlockquote()
    {
        var input = @"
> Quote with ~subscript~
";
        var expected = @"
<blockquote>
<p>Quote with <sub>subscript</sub></p>
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
    public void StrikethroughVsSubscriptPrecedence()
    {
        var input = @"
This is ~~deleted~~ text.
";
        var expected = @"
<p>This is <del>deleted</del> text.</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void SubscriptWithTildeInside()
    {
        var input = @"
text ~tilde ~ inside~
";
        var expected = @"
<p>text <sub>tilde ~ inside</sub></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void StrikethroughStillWorks()
    {
        var input = @"
text ~~struck~~, not subscripted
";
        var expected = @"
<p>text <del>struck</del>, not subscripted</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Extended.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }
}