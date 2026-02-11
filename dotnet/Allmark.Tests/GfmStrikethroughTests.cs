using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allmark.Rulesets;

namespace Allmark.Tests;

[TestClass]
public class GfmStrikethroughTests
{
	[TestMethod]
	public void SpecStrikethrough()
	{
		var input = @"
~~Hi~~ Hello, world!
";
		var expected = @"<p><del>Hi</del> Hello, world!</p>";
		var root = Parser.Execute(input.Substring(1, input.Length - 1), Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void StrikethroughSingleWord()
	{
		var input = @"~~deleted~~";
		var expected = @"<p><del>deleted</del></p>";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void StrikethroughMultipleWords()
	{
		var input = @"~~this is deleted~~";
		var expected = @"<p><del>this is deleted</del></p>";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void StrikethroughWithSpacesInside()
	{
		var input = @"~~  spaces  ~~";
		var expected = @"<p>~~  spaces  ~~</p>";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void StrikethroughWithEmphasis()
	{
		var input = @"~~*bold and deleted*~~";
		var expected = @"<p><del><em>bold and deleted</em></del></p>";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void StrikethroughInsideEmphasis()
	{
		var input = @"*~~deleted in italic~~*";
		var expected = @"<p><em><del>deleted in italic</del></em></p>";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void StrikethroughWithCode()
	{
		var input = @"~~code: `var x` here~~";
		var expected = @"<p><del>code: <code>var x</code> here</del></p>";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void StrikethroughWithLink()
	{
		var input = @"~~[link text](http://example.com)~~";
		var expected = @"<p><del><a href=""http://example.com"">link text</a></del></p>";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void MultipleStrikethroughsInOneLine()
	{
		var input = @"~~first~~ and ~~second~~ and ~~third~~";
		var expected = @"<p><del>first</del> and <del>second</del> and <del>third</del></p>";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void StrikethroughAtStartOfParagraph()
	{
		var input = @"~~deleted~~ followed by normal text.";
		var expected = @"<p><del>deleted</del> followed by normal text.</p>";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void StrikethroughAtEndOfParagraph()
	{
		var input = @"Normal text followed by ~~deleted~~";
		var expected = @"<p>Normal text followed by <del>deleted</del></p>";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void StrikethroughInListItem()
	{
		var input = @"- ~~deleted item~~
- normal item";
		var expected = @"
<ul>
<li><del>deleted item</del></li>
<li>normal item</li>
</ul>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void StrikethroughWithTildesInside()
	{
		var input = @"~~text with ~ tilde~~";
		var expected = @"<p><del>text with ~ tilde</del></p>";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void StrikethroughWithMultipleTildes()
	{
		var input = @"~~~~double~~~~";
		var expected = @"<pre><code class=""language-double~~~~""></code></pre>";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void StrikethroughAcrossLines()
	{
		var input = @"~~line one
line two~~";
		var expected = @"<p><del>line one
line two</del></p>";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void StrikethroughWithPunctuation()
	{
		var input = @"~~Hello, world!~~";
		var expected = @"<p><del>Hello, world!</del></p>";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void StrikethroughWithNumbers()
	{
		var input = @"~~12345~~";
		var expected = @"<p><del>12345</del></p>";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void StrikethroughInTableCell()
	{
		var input = @"| col1 | col2 |
| ---- | ---- |
| ~~deleted~~ | normal |";
		var expected = @"
<table>
<thead>
<tr>
<th>col1</th>
<th>col2</th>
</tr>
</thead>
<tbody>
<tr>
<td><del>deleted</del></td>
<td>normal</td>
</tr>
</tbody>
</table>
";
		var root = Parser.Execute(input.Substring(1, input.Length - 1), Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void StrikethroughAdjacentToRegularText()
	{
		var input = @"normal~~deleted~~normal";
		var expected = @"<p>normal<del>deleted</del>normal</p>";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void StrikethroughWithEscapedCharacters()
	{
		var input = @"~~text with \*asterisk\*~~";
		var expected = @"<p><del>text with *asterisk*</del></p>";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}
}
