using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allmark.Rulesets;

namespace Allmark.Tests;

[TestClass]
public class ParserTests
{
	[TestMethod]
	public void BasicParse()
	{
		var input = @"
# Test

Here is some text

* Tight item 1
* Tight item 2

- Loose item 1

- Loose item 2
";

		var expected = @"
<h1>Test</h1>
<p>Here is some text</p>
<ul>
<li>Tight item 1</li>
<li>Tight item 2</li>
</ul>
<ul>
<li>
<p>Loose item 1</p>
</li>
<li>
<p>Loose item 2</p>
</li>
</ul>
".TrimStart();

		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.AreEqual(expected.Trim(), html.Trim());
	}
}
