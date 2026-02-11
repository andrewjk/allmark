using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allmark.Rulesets;
using System.Reflection;

namespace Allmark.Tests;

[TestClass]
public class GfmTasklistTests
{
	[TestMethod]
	public void SpecTasklist()
	{
		var input = @"
- [ ] foo
- [x] bar
";
		var expected = @"
<ul>
<li><input type=""checkbox"" disabled="""" /> foo</li>
<li><input type=""checkbox"" checked="""" disabled="""" /> bar</li>
</ul>
";
		var root = Parser.Execute(input.Substring(1, input.Length - 1), Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void TasklistWithAsteriskMarker()
	{
		var input = @"* [ ] unchecked
* [x] checked";
		var expected = @"
<ul>
<li><input type=""checkbox"" disabled="""" /> unchecked</li>
<li><input type=""checkbox"" checked="""" disabled="""" /> checked</li>
</ul>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void TasklistWithPlusMarker()
	{
		var input = @"+ [ ] unchecked
+ [x] checked";
		var expected = @"
<ul>
<li><input type=""checkbox"" disabled="""" /> unchecked</li>
<li><input type=""checkbox"" checked="""" disabled="""" /> checked</li>
</ul>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void TasklistInOrderedList()
	{
		var input = @"1. [ ] unchecked item
2. [x] checked item";
		var expected = @"
<ol>
<li><input type=""checkbox"" disabled="""" /> unchecked item</li>
<li><input type=""checkbox"" checked="""" disabled="""" /> checked item</li>
</ol>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void TasklistWithInlineFormatting()
	{
		var input = @"- [ ] **bold** task
- [x] *italic* task
- [ ] ~~strikethrough~~ task";
		var expected = @"
<ul>
<li><input type=""checkbox"" disabled="""" /> <strong>bold</strong> task</li>
<li><input type=""checkbox"" checked="""" disabled="""" /> <em>italic</em> task</li>
<li><input type=""checkbox"" disabled="""" /> <del>strikethrough</del> task</li>
</ul>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void TasklistWithCode()
	{
		var input = @"- [ ] task with `code`
- [x] another `code` task";
		var expected = @"
<ul>
<li><input type=""checkbox"" disabled="""" /> task with <code>code</code></li>
<li><input type=""checkbox"" checked="""" disabled="""" /> another <code>code</code> task</li>
</ul>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void TasklistWithLinks()
	{
		var input = @"- [ ] task with [link](http://example.com)
- [x] checked [link](http://example.com) task";
		var expected = @"
<ul>
<li><input type=""checkbox"" disabled="""" /> task with <a href=""http://example.com"">link</a></li>
<li><input type=""checkbox"" checked="""" disabled="""" /> checked <a href=""http://example.com"">link</a> task</li>
</ul>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void NestedTasklist()
	{
		var input = @"- [ ] parent task
   - [ ] child task 1
   - [x] child task 2
- [x] another parent";
		var expected = @"
<ul>
<li><input type=""checkbox"" disabled="""" /> parent task
<ul>
<li><input type=""checkbox"" disabled="""" /> child task 1</li>
<li><input type=""checkbox"" checked="""" disabled="""" /> child task 2</li>
</ul>
</li>
<li><input type=""checkbox"" checked="""" disabled="""" /> another parent</li>
</ul>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void MixedTasksAndRegularItems()
	{
		var input = @"- [ ] task item
- regular item
- [x] checked task
- another regular item";
		var expected = @"
<ul>
<li><input type=""checkbox"" disabled="""" /> task item</li>
<li>regular item</li>
<li><input type=""checkbox"" checked="""" disabled="""" /> checked task</li>
<li>another regular item</li>
</ul>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void TasklistWithSingleCharacter()
	{
		var input = @"- [ ] a
- [x] b";
		var expected = @"
<ul>
<li><input type=""checkbox"" disabled="""" /> a</li>
<li><input type=""checkbox"" checked="""" disabled="""" /> b</li>
</ul>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void TasklistWithEmptyBrackets()
	{
		var input = @"- [ ] 
- [x] ";
		var expected = @"
<ul>
<li><input type=""checkbox"" disabled="""" /> </li>
<li><input type=""checkbox"" checked="""" disabled="""" /> </li>
</ul>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void TasklistWithUppercaseX()
	{
		var input = @"- [ ] unchecked
- [X] checked with uppercase";
		var expected = @"
<ul>
<li><input type=""checkbox"" disabled="""" /> unchecked</li>
<li><input type=""checkbox"" checked="""" disabled="""" /> checked with uppercase</li>
</ul>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void TasklistInBlockquote()
	{
		var input = @"> - [ ] quoted task
> - [x] checked quoted task";
		var expected = @"
<blockquote>
<ul>
<li>[ ] quoted task</li>
<li>[x] checked quoted task</li>
</ul>
</blockquote>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void TasklistWithMultipleParagraphs()
	{
		var input = @"- [ ] task with paragraph

   continuation paragraph
- [x] another task";
		var expected = @"
<ul>
<li><input type=""checkbox"" disabled="""" /> 
<p>task with paragraph</p>
<p>continuation paragraph</p>
</li>
<li><input type=""checkbox"" checked="""" disabled="""" /> 
<p>another task</p>
</li>
</ul>".Trim();
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers).Trim();
		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void TasklistWithSublist()
	{
		var input = @"- [ ] task with sublist
   - subitem 1
   - subitem 2
- [x] checked task";
		var expected = @"
<ul>
<li><input type=""checkbox"" disabled="""" /> task with sublist
<ul>
<li>subitem 1</li>
<li>subitem 2</li>
</ul>
</li>
<li><input type=""checkbox"" checked="""" disabled="""" /> checked task</li>
</ul>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void TasklistWithHtmlEntities()
	{
		var input = @"- [ ] task with &amp; entity
- [x] task with &lt;HTML&gt;";
		var expected = @"
<ul>
<li><input type=""checkbox"" disabled="""" /> task with &amp; entity</li>
<li><input type=""checkbox"" checked="""" disabled="""" /> task with &lt;HTML&gt;</li>
</ul>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}

	[TestMethod]
	public void TasklistWithVariousWhitespace()
	{
		var input = @"- [ ]one
- [  ] two
- [ x] three";
		var expected = @"
<ul>
<li>[ ]one</li>
<li>[  ] two</li>
<li>[ x] three</li>
</ul>
";
		var root = Parser.Execute(input, Gfm.RuleSet);
		var html = RenderHtml.Execute(root, Gfm.RuleSet.Renderers);

		Assert.AreEqual(expected.Trim(), html.Trim());
	}
}
