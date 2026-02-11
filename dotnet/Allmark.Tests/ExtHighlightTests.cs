using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allmark.Rulesets;

namespace Allmark.Tests;

[TestClass]
public class ExtHighlightTests
{
	[TestMethod]
	public void HighlightSingle()
	{
		var input = @"
This should be =highlighted= as it is important.
";
		var expected = @"
<p>This should be <mark>highlighted</mark> as it is important.</p>
";
		var root = Parser.Execute(input.Substring(1, input.Length - 1), Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void HighlightDouble()
	{
		var input = @"
This should be ==highlighted== as it is important.
";
		var expected = @"
<p>This should be <mark>highlighted</mark> as it is important.</p>
";
		var root = Parser.Execute(input.Substring(1, input.Length - 1), Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void HighlightTriple()
	{
		var input = @"
This should be ===highlighted=== as it is important.
";
		var expected = @"
<p>This should be ===highlighted=== as it is important.</p>
";
		var root = Parser.Execute(input.Substring(1, input.Length - 1), Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void HighlightSingleCharacter()
	{
		var input = @"text =a= more";
		var expected = @"<p>text <mark>a</mark> more</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void MultipleHighlightsInOneLine()
	{
		var input = @"=first= and =second= and =third=";
		var expected = @"<p><mark>first</mark> and <mark>second</mark> and <mark>third</mark></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void HighlightAtStartOfParagraph()
	{
		var input = @"=highlighted= This is important.";
		var expected = @"<p><mark>highlighted</mark> This is important.</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void HighlightAtEndOfParagraph()
	{
		var input = @"This is =highlighted=";
		var expected = @"<p>This is <mark>highlighted</mark></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void HighlightWithPunctuation()
	{
		var input = @"text =word!= more";
		var expected = @"<p>text <mark>word!</mark> more</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void HighlightWithSpaces()
	{
		var input = @"text =with spaces= more";
		var expected = @"<p>text <mark>with spaces</mark> more</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void HighlightWithSpecialCharacters()
	{
		var input = @"text =a+b= more";
		var expected = @"<p>text <mark>a+b</mark> more</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void HighlightAdjacentToText()
	{
		var input = @"test=ing=test";
		var expected = @"<p>test<mark>ing</mark>test</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void EmptyHighlight()
	{
		var input = @"text==text";
		var expected = @"<p>text==text</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void HighlightWithMarkdownInside()
	{
		var input = @"text =**bold**=";
		var expected = @"<p>text <mark><strong>bold</strong></mark></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void HighlightWithCodeInside()
	{
		var input = @"text =`code`=";
		var expected = @"<p>text <mark><code>code</code></mark></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void EscapedEqualsShouldNotBeHighlight()
	{
		var input = @"text \=not highlight\=";
		var expected = @"<p>text =not highlight=</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void UnmatchedOpeningEquals()
	{
		var input = @"text =not closed";
		var expected = @"<p>text =not closed</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void UnmatchedClosingEquals()
	{
		var input = @"text not opened=";
		var expected = @"<p>text not opened=</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void HighlightInListItem()
	{
		var input = @"- Item with =highlight=";
		var expected = @"<ul>
<li>Item with <mark>highlight</mark></li>
</ul>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void HighlightInBlockquote()
	{
		var input = @"> Quote with =highlight=";
		var expected = @"<blockquote>
<p>Quote with <mark>highlight</mark></p>
</blockquote>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void HighlightWithEqualsInside()
	{
		var input = @"text =equals = inside=";
		var expected = @"<p>text <mark>equals = inside</mark></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void HighlightAtBeginningOfDocument()
	{
		var input = @"=Start= of document.";
		var expected = @"<p><mark>Start</mark> of document.</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void HighlightAtEndOfDocument()
	{
		var input = @"End of =document=";
		var expected = @"<p>End of <mark>document</mark></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}
}
