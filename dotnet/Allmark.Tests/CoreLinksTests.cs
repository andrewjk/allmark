using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allmark.Rulesets;

namespace Allmark.Tests;

[TestClass]
public class CoreLinksTests
{
    [TestMethod]
    public void BasicInlineLink()
    {
        var input = @"
[Google](https://google.com)
";
        var expected = @"
<p><a href=""https://google.com"">Google</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void LinkWithTitle()
    {
        var input = @"
[Google](https://google.com ""Search Engine"")
";
        var expected = @"
<p><a href=""https://google.com"" title=""Search Engine"">Google</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void LinkWithSingleQuotedTitle()
    {
        var input = @"
[Google](https://google.com 'Search Engine')
";
        var expected = @"
<p><a href=""https://google.com"" title=""Search Engine"">Google</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void LinkInParagraph()
    {
        var input = @"
Visit [Google](https://google.com) for search.
";
        var expected = @"
<p>Visit <a href=""https://google.com"">Google</a> for search.</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void MultipleLinksInOneLine()
    {
        var input = @"
[Google](https://google.com) and [GitHub](https://github.com)
";
        var expected = @"
<p><a href=""https://google.com"">Google</a> and <a href=""https://github.com"">GitHub</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void LinkWithEmphasis()
    {
        var input = @"
[*Google*](https://google.com)
";
        var expected = @"
<p><a href=""https://google.com""><em>Google</em></a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void EmphasisAroundLink()
    {
        var input = @"
*[Google](https://google.com)*
";
        var expected = @"
<p><em><a href=""https://google.com"">Google</a></em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void LinkWithCodeInText()
    {
        var input = @"
[`const`](https://example.com)
";
        var expected = @"
<p><a href=""https://example.com""><code>const</code></a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void LinkInListItem()
    {
        var input = @"
- [Link](https://example.com)
";
        var expected = @"
<ul>
<li><a href=""https://example.com"">Link</a></li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void LinkInHeading()
    {
        var input = @"
# See [Google](https://google.com)
";
        var expected = @"
<h1>See <a href=""https://google.com"">Google</a></h1>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void ReferenceLinkDefinitionAndUsage()
    {
        var input = @"
[Google][google]

[google]: https://google.com
";
        var expected = @"
<p><a href=""https://google.com"">Google</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void ReferenceLinkWithImplicitLabel()
    {
        var input = @"
[Google][]

[Google]: https://google.com
";
        var expected = @"
<p><a href=""https://google.com"">Google</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void ReferenceLinkWithTitle()
    {
        var input = @"
[Google][google]

[google]: https://google.com ""Search Engine""
";
        var expected = @"
<p><a href=""https://google.com"" title=""Search Engine"">Google</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void MultipleReferenceLinks()
    {
        var input = @"
[Google][google] and [GitHub][github]

[google]: https://google.com
[github]: https://github.com
";
        var expected = @"
<p><a href=""https://google.com"">Google</a> and <a href=""https://github.com"">GitHub</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void AutolinkWithHttp()
    {
        var input = @"
<http://example.com>
";
        var expected = @"
<p><a href=""http://example.com"">http://example.com</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void AutolinkWithHttps()
    {
        var input = @"
<https://example.com>
";
        var expected = @"
<p><a href=""https://example.com"">https://example.com</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void AutolinkWithFtp()
    {
        var input = @"
<ftp://example.com>
";
        var expected = @"
<p><a href=""ftp://example.com"">ftp://example.com</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void EmailAutolink()
    {
        var input = @"
<user@example.com>
";
        var expected = @"
<p><a href=""mailto:user@example.com"">user@example.com</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void LinkWithSpecialCharactersInURL()
    {
        var input = @"
[Link](https://example.com/path?query=value&other=123#anchor)
";
        var expected = @"
<p><a href=""https://example.com/path?query=value&amp;other=123#anchor"">Link</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void LinkWithParenthesesInURL()
    {
        var input = @"
[Link](https://example.com/path(with)parentheses)
";
        var expected = @"
<p><a href=""https://example.com/path(with)parentheses"">Link</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void LinkWithSpacesInTitle()
    {
        var input = @"
[Link](https://example.com ""This is a title"")
";
        var expected = @"
<p><a href=""https://example.com"" title=""This is a title"">Link</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void LinkWithEscapedBracketsInText()
    {
        var input = @"
[[link]](https://example.com)
";
        var expected = @"
<p><a href=""https://example.com"">[link]</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void EmptyLinkText()
    {
        var input = @"
[](https://example.com)
";
        var expected = @"
<p><a href=""https://example.com""></a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void LinkWithUnderscoreInURL()
    {
        var input = @"
[Link](https://example.com/path_with_underscore)
";
        var expected = @"
<p><a href=""https://example.com/path_with_underscore"">Link</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void RelativeURL()
    {
        var input = @"
[Link](/path/to/page)
";
        var expected = @"
<p><a href=""/path/to/page"">Link</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void LinkWithPercentEncoding()
    {
        var input = @"
[Link](https://example.com/path%20with%20spaces)
";
        var expected = @"
<p><a href=""https://example.com/path%20with%20spaces"">Link</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }
}