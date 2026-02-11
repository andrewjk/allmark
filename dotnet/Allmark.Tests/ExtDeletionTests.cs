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
";
		var root = Parser.Execute(input.Substring(1, input.Length - 1), Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void DeletionDouble()
	{
		var input = @"
This text was {--deleted--} recently.
";
		var expected = @"
<p>This text was <del class=""markdown-deletion"">deleted</del> recently.</p>
";
		var root = Parser.Execute(input.Substring(1, input.Length - 1), Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void DeletionTriple()
	{
		var input = @"
This text was {---deleted---} recently.
";
		var expected = @"
<p>This text was {---deleted---} recently.</p>
";
		var root = Parser.Execute(input.Substring(1, input.Length - 1), Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void DeletionSingleCharacter()
	{
		var input = @"text {-a-} more";
		var expected = @"<p>text <del class=""markdown-deletion"">a</del> more</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void DeletionWithSpaces()
	{
		var input = @"text {-with spaces-} more";
		var expected = @"<p>text <del class=""markdown-deletion"">with spaces</del> more</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void DeletionAtStartOfParagraph()
	{
		var input = @"{-deleted-} This is new.";
		var expected = @"<p><del class=""markdown-deletion"">deleted</del> This is new.</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void DeletionAtEndOfParagraph()
	{
		var input = @"This is {-deleted-}";
		var expected = @"<p>This is <del class=""markdown-deletion"">deleted</del></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void DeletionWithPunctuation()
	{
		var input = @"text {-word!-} more";
		var expected = @"<p>text <del class=""markdown-deletion"">word!</del> more</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void DeletionWithSpecialCharacters()
	{
		var input = @"text {-a-b-} more";
		var expected = @"<p>text <del class=""markdown-deletion"">a-b</del> more</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void DeletionAdjacentToText()
	{
		var input = @"test{-ing-}test";
		var expected = @"<p>test<del class=""markdown-deletion"">ing</del>test</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void EmptyDeletion()
	{
		var input = @"text{--}text";
		var expected = @"<p>text{--}text</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void DeletionWithMarkdownInside()
	{
		var input = @"text {-**bold**-}";
		var expected = @"<p>text <del class=""markdown-deletion""><strong>bold</strong></del></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void DeletionWithCodeInside()
	{
		var input = @"text {-`code`-}";
		var expected = @"<p>text <del class=""markdown-deletion""><code>code</code></del></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void EscapedBracesShouldNotBeDeletion()
	{
		var input = @"text \{-not deletion\-\}";
		var expected = @"<p>text {-not deletion-}</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void UnmatchedOpeningDeletion()
	{
		var input = @"text {-not closed";
		var expected = @"<p>text {-not closed</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void UnmatchedClosingDeletion()
	{
		var input = @"text not opened-}";
		var expected = @"<p>text not opened-}</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void DeletionInListItem()
	{
		var input = @"- Item with {-deletion-}";
		var expected = @"<ul>
<li>Item with <del class=""markdown-deletion"">deletion</del></li>
</ul>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void DeletionInBlockquote()
	{
		var input = @"> Quote with {-deletion-}";
		var expected = @"<blockquote>
<p>Quote with <del class=""markdown-deletion"">deletion</del></p>
</blockquote>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void DeletionWithPlusInside()
	{
		var input = @"text {-plus - inside-}";
		var expected = @"<p>text <del class=""markdown-deletion"">plus - inside</del></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void DeletionAtBeginningOfDocument()
	{
		var input = @"{-Start-} of document.";
		var expected = @"<p><del class=""markdown-deletion"">Start</del> of document.</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void DeletionAtEndOfDocument()
	{
		var input = @"End of {-document-}";
		var expected = @"<p>End of <del class=""markdown-deletion"">document</del></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void MultipleDeletionsInOneLine()
	{
		var input = @"{-first-} and {-second-} and {-third-}";
		var expected = @"<p><del class=""markdown-deletion"">first</del> and <del class=""markdown-deletion"">second</del> and <del class=""markdown-deletion"">third</del></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void DeletionWithStartingEmphasis()
	{
		var input = @"{-deleted *text-} that shouldn't be bold*";
		var expected = @"<p><del class=""markdown-deletion"">deleted *text</del> that shouldn't be bold*</p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void DeletionWithEndingEmphasis()
	{
		var input = @"*this text should be {-deleted but not bold*-}";
		var expected = @"<p>*this text should be <del class=""markdown-deletion"">deleted but not bold*</del></p>";
		var root = Parser.Execute(input, Extended.RuleSet);
		var html = RenderHtml.Execute(root, Extended.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}
}
