using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allmark.Rulesets;

namespace Allmark.Tests;

[TestClass]
public class ExtInsertionTests
{
	[TestMethod]
	public void InsertionSingle()
	{
		var input = @"
This text was {+inserted+} recently.
";
		var expected = @"
<p>This text was <ins class=""markdown-insertion"">inserted</ins> recently.</p>
";
		var root = Parser.Execute(input.Substring(1, input.Length - 1), Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void InsertionDouble()
	{
		var input = @"
This text was {++inserted++} recently.
";
		var expected = @"
<p>This text was <ins class=""markdown-insertion"">inserted</ins> recently.</p>
";
		var root = Parser.Execute(input.Substring(1, input.Length - 1), Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void InsertionTriple()
	{
		var input = @"
This text was {+++inserted+++} recently.
";
		var expected = @"
<p>This text was {+++inserted+++} recently.</p>
";
		var root = Parser.Execute(input.Substring(1, input.Length - 1), Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void InsertionSingleCharacter()
	{
		var input = @"text {+a+} more";
		var expected = @"<p>text <ins class=""markdown-insertion"">a</ins> more</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void InsertionWithSpaces()
	{
		var input = @"text {+with spaces+} more";
		var expected = @"<p>text <ins class=""markdown-insertion"">with spaces</ins> more</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void InsertionAtStartOfParagraph()
	{
		var input = @"{+inserted+} This is new.";
		var expected = @"<p><ins class=""markdown-insertion"">inserted</ins> This is new.</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void InsertionAtEndOfParagraph()
	{
		var input = @"This is {+inserted+}";
		var expected = @"<p>This is <ins class=""markdown-insertion"">inserted</ins></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void InsertionWithPunctuation()
	{
		var input = @"text {+word!+} more";
		var expected = @"<p>text <ins class=""markdown-insertion"">word!</ins> more</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void InsertionWithSpecialCharacters()
	{
		var input = @"text {+a+b+} more";
		var expected = @"<p>text <ins class=""markdown-insertion"">a+b</ins> more</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void InsertionAdjacentToText()
	{
		var input = @"test{+ing+}test";
		var expected = @"<p>test<ins class=""markdown-insertion"">ing</ins>test</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void EmptyInsertion()
	{
		var input = @"text{++}text";
		var expected = @"<p>text{++}text</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void InsertionWithMarkdownInside()
	{
		var input = @"text {+**bold**+}";
		var expected = @"<p>text <ins class=""markdown-insertion""><strong>bold</strong></ins></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void InsertionWithCodeInside()
	{
		var input = @"text {+`code`+}";
		var expected = @"<p>text <ins class=""markdown-insertion""><code>code</code></ins></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void EscapedBracesShouldNotBeInsertion()
	{
		var input = @"text \{+not insertion\+\}";
		var expected = @"<p>text {+not insertion+}</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void UnmatchedOpeningInsertion()
	{
		var input = @"text {+not closed";
		var expected = @"<p>text {+not closed</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void UnmatchedClosingInsertion()
	{
		var input = @"text not opened+}";
		var expected = @"<p>text not opened+}</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void InsertionInListItem()
	{
		var input = @"- Item with {+insertion+}";
		var expected = @"<ul>
<li>Item with <ins class=""markdown-insertion"">insertion</ins></li>
</ul>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void InsertionInBlockquote()
	{
		var input = @"> Quote with {+insertion+}";
		var expected = @"<blockquote>
<p>Quote with <ins class=""markdown-insertion"">insertion</ins></p>
</blockquote>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void InsertionWithPlusInside()
	{
		var input = @"text {+plus + inside+}";
		var expected = @"<p>text <ins class=""markdown-insertion"">plus + inside</ins></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void InsertionAtBeginningOfDocument()
	{
		var input = @"{+Start+} of document.";
		var expected = @"<p><ins class=""markdown-insertion"">Start</ins> of document.</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void InsertionAtEndOfDocument()
	{
		var input = @"End of {+document+}";
		var expected = @"<p>End of <ins class=""markdown-insertion"">document</ins></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void MultipleInsertionsInOneLine()
	{
		var input = @"{+first+} and {+second+} and {+third+}";
		var expected = @"<p><ins class=""markdown-insertion"">first</ins> and <ins class=""markdown-insertion"">second</ins> and <ins class=""markdown-insertion"">third</ins></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void InsertionWithStartingEmphasis()
	{
		var input = @"{+inserted *text+} that shouldn't be bold*";
		var expected = @"<p><ins class=""markdown-insertion"">inserted *text</ins> that shouldn't be bold*</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void InsertionWithEndingEmphasis()
	{
		var input = @"*this text should be {+inserted but not bold*+}";
		var expected = @"<p>*this text should be <ins class=""markdown-insertion"">inserted but not bold*</ins></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}
}
