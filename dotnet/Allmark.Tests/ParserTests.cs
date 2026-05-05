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
# Test ☺️

Here is some text

* Tight item 1
* Tight item 2

- Loose item 1

- Loose item 2

## Subtest

Here is some more text
";

        var expected = @"
<h1>Test ☺️</h1>
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
<h2>Subtest</h2>
<p>Here is some more text</p>
";

        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());

        Assert.IsNotNull(doc.Children);
        Assert.AreEqual("heading", doc.Children[0].Type);
        Assert.AreEqual(1, doc.Children[0].Index);
        Assert.AreEqual(10, doc.Children[0].Length);

        var start = doc.Children[0].Index;
        var length = doc.Children[0].Length;
        Assert.AreEqual("# Test ☺️\n", input.Substring(start, length));
    }
}
