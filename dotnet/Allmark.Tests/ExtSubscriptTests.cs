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
";
		var root = Parser.Execute(input.Substring(1, input.Length - 1), Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SubscriptDouble()
	{
		var input = @"
This should be ~~down~~ below everything else.
";
		var expected = @"
<p>This should be <del>down</del> below everything else.</p>
";
		var root = Parser.Execute(input.Substring(1, input.Length - 1), Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SubscriptTriple()
	{
		var input = @"
This should be ~~~down~~~ below everything else.
";
		var expected = @"
<p>This should be ~~~down~~~ below everything else.</p>
";
		var root = Parser.Execute(input.Substring(1, input.Length - 1), Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SubscriptSingleCharacter()
	{
		var input = @"H~2~O";
		var expected = @"<p>H<sub>2</sub>O</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SubscriptWithNumbers()
	{
		var input = @"x~1~ + x~2~";
		var expected = @"<p>x<sub>1</sub> + x<sub>2</sub></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void MultipleSubscriptsInOneLine()
	{
		var input = @"a~i~ + b~j~ = c~k~";
		var expected = @"<p>a<sub>i</sub> + b<sub>j</sub> = c<sub>k</sub></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SubscriptAtStartOfParagraph()
	{
		var input = @"~note~ This is important.";
		var expected = @"<p><sub>note</sub> This is important.</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SubscriptAtEndOfParagraph()
	{
		var input = @"See index~1~";
		var expected = @"<p>See index<sub>1</sub></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SubscriptWithPunctuation()
	{
		var input = @"Hello~world!~";
		var expected = @"<p>Hello<sub>world!</sub></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SubscriptWithSpaces()
	{
		var input = @"text ~with spaces~ more";
		var expected = @"<p>text <sub>with spaces</sub> more</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SubscriptWithSpecialCharacters()
	{
		var input = @"math~i+j~";
		var expected = @"<p>math<sub>i+j</sub></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SubscriptAdjacentToText()
	{
		var input = @"test~ing~test";
		var expected = @"<p>test<sub>ing</sub>test</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void EmptySubscript()
	{
		var input = @"text~~text";
		var expected = @"<p>text~~text</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SubscriptWithMarkdownInside()
	{
		var input = @"text ~**bold**~";
		var expected = @"<p>text <sub><strong>bold</strong></sub></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SubscriptWithCodeInside()
	{
		var input = @"text ~`code`~";
		var expected = @"<p>text <sub><code>code</code></sub></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void EscapedTildeShouldNotBeSubscript()
	{
		var input = @"text \~not subscript\~";
		var expected = @"<p>text ~not subscript~</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void UnmatchedOpeningTilde()
	{
		var input = @"text ~not closed";
		var expected = @"<p>text ~not closed</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void UnmatchedClosingTilde()
	{
		var input = @"text not opened~";
		var expected = @"<p>text not opened~</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SubscriptInListItem()
	{
		var input = @"- Item with ~subscript~";
		var expected = @"<ul>
<li>Item with <sub>subscript</sub></li>
</ul>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SubscriptInBlockquote()
	{
		var input = @"> Quote with ~subscript~";
		var expected = @"<blockquote>
<p>Quote with <sub>subscript</sub></p>
</blockquote>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void StrikethroughVsSubscriptPrecedence()
	{
		var input = @"This is ~~deleted~~ text.";
		var expected = @"<p>This is <del>deleted</del> text.</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SubscriptWithTildeInside()
	{
		var input = @"text ~tilde ~ inside~";
		var expected = @"<p>text <sub>tilde ~ inside</sub></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void StrikethroughStillWorks()
	{
		var input = @"text ~~struck~~, not subscripted";
		var expected = @"<p>text <del>struck</del>, not subscripted</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}
}
