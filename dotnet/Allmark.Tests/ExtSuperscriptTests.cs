using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allmark.Rulesets;

namespace Allmark.Tests;

[TestClass]
public class ExtSuperscriptTests
{
	[TestMethod]
	public void SuperscriptSingle()
	{
		var input = @"
This should be ^up^ above everything else.
";
		var expected = @"
<p>This should be <sup>up</sup> above everything else.</p>
";
		var root = Parser.Execute(input.Substring(1, input.Length - 1), Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SuperscriptDouble()
	{
		var input = @"
This should be ^^up^^ above everything else.
";
		var expected = @"
<p>This should be <sup>up</sup> above everything else.</p>
";
		var root = Parser.Execute(input.Substring(1, input.Length - 1), Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SuperscriptTriple()
	{
		var input = @"
This should be ^^^up^^^ above everything else.
";
		var expected = @"
<p>This should be ^^^up^^^ above everything else.</p>
";
		var root = Parser.Execute(input.Substring(1, input.Length - 1), Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SuperscriptSingleCharacter()
	{
		var input = @"x^2^";
		var expected = @"<p>x<sup>2</sup></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SuperscriptWithNumbers()
	{
		var input = @"E=mc^2^";
		var expected = @"<p>E=mc<sup>2</sup></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void MultipleSuperscriptsInOneLine()
	{
		var input = @"x^2^ + y^2^ = z^2^";
		var expected = @"<p>x<sup>2</sup> + y<sup>2</sup> = z<sup>2</sup></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SuperscriptAtStartOfParagraph()
	{
		var input = @"^note^ This is important.";
		var expected = @"<p><sup>note</sup> This is important.</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SuperscriptAtEndOfParagraph()
	{
		var input = @"See footnote^1^";
		var expected = @"<p>See footnote<sup>1</sup></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SuperscriptWithPunctuation()
	{
		var input = @"Hello^world!^";
		var expected = @"<p>Hello<sup>world!</sup></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SuperscriptWithSpaces()
	{
		var input = @"text ^with spaces^ more";
		var expected = @"<p>text <sup>with spaces</sup> more</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SuperscriptWithSpecialCharacters()
	{
		var input = @"math^2+3^";
		var expected = @"<p>math<sup>2+3</sup></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SuperscriptAdjacentToText()
	{
		var input = @"test^ing^test";
		var expected = @"<p>test<sup>ing</sup>test</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void EmptySuperscript()
	{
		var input = @"text^^text";
		var expected = @"<p>text^^text</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SuperscriptWithMarkdownInside()
	{
		var input = @"text ^**bold**^";
		var expected = @"<p>text <sup><strong>bold</strong></sup></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SuperscriptWithCodeInside()
	{
		var input = @"text ^`code`^";
		var expected = @"<p>text <sup><code>code</code></sup></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void EscapedCaretShouldNotBeSuperscript()
	{
		var input = @"text \^not superscript\^";
		var expected = @"<p>text ^not superscript^</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void UnmatchedOpeningCaret()
	{
		var input = @"text ^not closed";
		var expected = @"<p>text ^not closed</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void UnmatchedClosingCaret()
	{
		var input = @"text not opened^";
		var expected = @"<p>text not opened^</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SuperscriptInListItem()
	{
		var input = @"- Item with ^superscript^";
		var expected = @"<ul>
<li>Item with <sup>superscript</sup></li>
</ul>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SuperscriptInBlockquote()
	{
		var input = @"> Quote with ^superscript^";
		var expected = @"<blockquote>
<p>Quote with <sup>superscript</sup></p>
</blockquote>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void NestedSuperscript()
	{
		var input = @"x^y^z^";
		var expected = @"<p>x<sup>y</sup>z^</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void SuperscriptWithCaretInside()
	{
		var input = @"text ^caret ^ inside^";
		var expected = @"<p>text <sup>caret ^ inside</sup></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}
}
