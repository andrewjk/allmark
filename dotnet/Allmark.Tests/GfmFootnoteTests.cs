using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allmark.Rulesets;

namespace Allmark.Tests;

[TestClass]
public class GfmFootnoteTests
{
	[TestMethod]
	public void SpecFootnote()
	{
		var input = @"
Here is a simple footnote[^1].

A footnote can also have multiple lines[^2].

[^1]: My reference.
[^2]: To add line breaks within a footnote, add 2 spaces to the end of a line.  
This is a second line.
";
		var expected = @"
<p>Here is a simple footnote<sup class=""footnote-ref""><a href=""#fn1"" id=""fnref1"">1</a></sup>.</p>
<p>A footnote can also have multiple lines<sup class=""footnote-ref""><a href=""#fn2"" id=""fnref2"">2</a></sup>.</p>
<section class=""footnotes"">
<ol>
<li id=""fn1"">
<p>My reference. <a href=""#fnref1"" class=""footnote-backref"">↩</a></p>
</li>
<li id=""fn2"">
<p>To add line breaks within a footnote, add 2 spaces to the end of a line.<br />
This is a second line. <a href=""#fnref2"" class=""footnote-backref"">↩</a></p>
</li>
</ol>
</section>
";
		var root = Parser.Execute(input.Substring(1, input.Length - 1), Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SimpleFootnoteReference()
	{
		var input = @"Text with a footnote[^1].

[^1]: This is the footnote content.";
		var expected = @"
<p>Text with a footnote<sup class=""footnote-ref""><a href=""#fn1"" id=""fnref1"">1</a></sup>.</p>
<section class=""footnotes"">
<ol>
<li id=""fn1"">
<p>This is the footnote content. <a href=""#fnref1"" class=""footnote-backref"">↩</a></p>
</li>
</ol>
</section>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void MultipleFootnoteReferences()
	{
		var input = @"First reference[^1] and second[^2].

[^1]: First footnote.
[^2]: Second footnote.";
		var expected = @"
<p>First reference<sup class=""footnote-ref""><a href=""#fn1"" id=""fnref1"">1</a></sup> and second<sup class=""footnote-ref""><a href=""#fn2"" id=""fnref2"">2</a></sup>.</p>
<section class=""footnotes"">
<ol>
<li id=""fn1"">
<p>First footnote. <a href=""#fnref1"" class=""footnote-backref"">↩</a></p>
</li>
<li id=""fn2"">
<p>Second footnote. <a href=""#fnref2"" class=""footnote-backref"">↩</a></p>
</li>
</ol>
</section>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void FootnoteWithInlineFormatting()
	{
		var input = @"Text[^1].

[^1]: Footnote with **bold** and *italic* text.";
		var expected = @"
<p>Text<sup class=""footnote-ref""><a href=""#fn1"" id=""fnref1"">1</a></sup>.</p>
<section class=""footnotes"">
<ol>
<li id=""fn1"">
<p>Footnote with <strong>bold</strong> and <em>italic</em> text. <a href=""#fnref1"" class=""footnote-backref"">↩</a></p>
</li>
</ol>
</section>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void FootnoteWithCode()
	{
		var input = @"Code reference[^1].

[^1]: Footnote with `inline code`.";
		var expected = @"
<p>Code reference<sup class=""footnote-ref""><a href=""#fn1"" id=""fnref1"">1</a></sup>.</p>
<section class=""footnotes"">
<ol>
<li id=""fn1"">
<p>Footnote with <code>inline code</code>. <a href=""#fnref1"" class=""footnote-backref"">↩</a></p>
</li>
</ol>
</section>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void FootnoteWithLink()
	{
		var input = @"Link reference[^1].

[^1]: See [example](http://example.com).";
		var expected = @"
<p>Link reference<sup class=""footnote-ref""><a href=""#fn1"" id=""fnref1"">1</a></sup>.</p>
<section class=""footnotes"">
<ol>
<li id=""fn1"">
<p>See <a href=""http://example.com"">example</a>. <a href=""#fnref1"" class=""footnote-backref"">↩</a></p>
</li>
</ol>
</section>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void FootnoteReferenceNotAtDefinition()
	{
		var input = @"Unknown footnote[^99].";
		var expected = @"
<p>Unknown footnote[^99].</p>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void FootnoteWithMultilineContent()
	{
		var input = @"Multiline[^1].

[^1]: First line
    Second line
    Third line";
		var expected = @"
<p>Multiline<sup class=""footnote-ref""><a href=""#fn1"" id=""fnref1"">1</a></sup>.</p>
<section class=""footnotes"">
<ol>
<li id=""fn1"">
<p>First line
Second line
Third line <a href=""#fnref1"" class=""footnote-backref"">↩</a></p>
</li>
</ol>
</section>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void RepeatedFootnoteReference()
	{
		var input = @"First[^1] and second[^1] use same footnote.

[^1]: Shared footnote content.";
		var expected = @"
<p>First<sup class=""footnote-ref""><a href=""#fn1"" id=""fnref1"">1</a></sup> and second<sup class=""footnote-ref""><a href=""#fn1"" id=""fnref1"">1</a></sup> use same footnote.</p>
<section class=""footnotes"">
<ol>
<li id=""fn1"">
<p>Shared footnote content. <a href=""#fnref1"" class=""footnote-backref"">↩</a></p>
</li>
</ol>
</section>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void FootnoteInList()
	{
		var input = @"- Item with footnote[^1]
- Another item[^2]

[^1]: First footnote.
[^2]: Second footnote.";
		var expected = @"
<ul>
<li>Item with footnote<sup class=""footnote-ref""><a href=""#fn1"" id=""fnref1"">1</a></sup></li>
<li>Another item<sup class=""footnote-ref""><a href=""#fn2"" id=""fnref2"">2</a></sup></li>
</ul>
<section class=""footnotes"">
<ol>
<li id=""fn1"">
<p>First footnote. <a href=""#fnref1"" class=""footnote-backref"">↩</a></p>
</li>
<li id=""fn2"">
<p>Second footnote. <a href=""#fnref2"" class=""footnote-backref"">↩</a></p>
</li>
</ol>
</section>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void FootnoteInBlockquote()
	{
		var input = @"> Quoted text with footnote[^1]

[^1]: Footnote for quote.";
		var expected = @"
<blockquote>
<p>Quoted text with footnote<sup class=""footnote-ref""><a href=""#fn1"" id=""fnref1"">1</a></sup></p>
</blockquote>
<section class=""footnotes"">
<ol>
<li id=""fn1"">
<p>Footnote for quote. <a href=""#fnref1"" class=""footnote-backref"">↩</a></p>
</li>
</ol>
</section>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void FootnoteWithSpecialCharactersInLabel()
	{
		var input = @"Special label[^a-b_c].

[^a-b_c]: Footnote with special label.";
		var expected = @"
<p>Special label[^a-b_c].</p>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void CaseInsensitiveFootnoteLabels()
	{
		var input = @"Mixed case[^ABC].

[^abc]: Should match.";
		var expected = @"
<p>Mixed case<sup class=""footnote-ref""><a href=""#fn1"" id=""fnref1"">1</a></sup>.</p>
<section class=""footnotes"">
<ol>
<li id=""fn1"">
<p>Should match. <a href=""#fnref1"" class=""footnote-backref"">↩</a></p>
</li>
</ol>
</section>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void FootnoteThenList()
	{
		var input = @"Text[^1]

[^1]: Here is the content  
- and here is a list";
		var expected = @"
<p>Text<sup class=""footnote-ref""><a href=""#fn1"" id=""fnref1"">1</a></sup></p>
<ul>
<li>and here is a list</li>
</ul>
<section class=""footnotes"">
<ol>
<li id=""fn1"">
<p>Here is the content <a href=""#fnref1"" class=""footnote-backref"">↩</a></p>
</li>
</ol>
</section>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void TitleAfterFootnoteLabel()
	{
		var input = @"Text[^1]

[^1]: https://example.com test";
		var expected = @"
<p>Text<sup class=""footnote-ref""><a href=""#fn1"" id=""fnref1"">1</a></sup></p>
<section class=""footnotes"">
<ol>
<li id=""fn1"">
<p><a href=""https://example.com"">https://example.com</a> test <a href=""#fnref1"" class=""footnote-backref"">↩</a></p>
</li>
</ol>
</section>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void LinkThenFootnote()
	{
		var input = @"Text[^1] [foo]

[foo]: https://example.com/foo
[^1]: https://example.com/1 test";
		var expected = @"
<p>Text<sup class=""footnote-ref""><a href=""#fn1"" id=""fnref1"">1</a></sup> <a href=""https://example.com/foo"">foo</a></p>
<section class=""footnotes"">
<ol>
<li id=""fn1"">
<p><a href=""https://example.com/1"">https://example.com/1</a> test <a href=""#fnref1"" class=""footnote-backref"">↩</a></p>
</li>
</ol>
</section>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void FootnoteThenLink()
	{
		var input = @"Text[^1] [foo]

[^1]: https://example.com/1 test
[foo]: https://example.com/foo";
		var expected = @"
<p>Text<sup class=""footnote-ref""><a href=""#fn1"" id=""fnref1"">1</a></sup> [foo]</p>
<section class=""footnotes"">
<ol>
<li id=""fn1"">
<p><a href=""https://example.com/1"">https://example.com/1</a> test
[foo]: <a href=""https://example.com/foo"">https://example.com/foo</a> <a href=""#fnref1"" class=""footnote-backref"">↩</a></p>
</li>
</ol>
</section>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SwallowFollowingBrackets()
	{
		var input = @"[^1][asd]f]

[^1]: /footnote";
		var expected = @"
<p><sup class=""footnote-ref""><a href=""#fn1"" id=""fnref1"">1</a></sup>f]</p>
<section class=""footnotes"">
<ol>
<li id=""fn1"">
<p>/footnote <a href=""#fnref1"" class=""footnote-backref"">↩</a></p>
</li>
</ol>
</section>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void LinkReferenceTakesPrecedence()
	{
		var input = @"[^1][foo]

[^1]: /footnote

[foo]: /url";
		var expected = @"
<p><a href=""/url"">^1</a></p>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void MultipleParagraphs()
	{
		var input = @"Footnote 1 link[^first].

[^first]: Footnote **can have markup**

    and multiple paragraphs.";
		var expected = @"
<p>Footnote 1 link<sup class=""footnote-ref""><a href=""#fn1"" id=""fnref1"">1</a></sup>.</p>
<section class=""footnotes"">
<ol>
<li id=""fn1"">
<p>Footnote <strong>can have markup</strong></p>
<p>and multiple paragraphs. <a href=""#fnref1"" class=""footnote-backref"">↩</a></p>
</li>
</ol>
</section>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}
}
