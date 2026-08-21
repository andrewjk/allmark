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
 *with* bold stuff

* Tight item 1
* Tight item 2

- Loose item 1

- Loose item 2

## Subtest

Here is some more text
";

        var expected = @"
<h1>Test ☺️</h1>
<p>Here is some text
<em>with</em> bold stuff</p>
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
        Assert.AreEqual(9, doc.Children[0].Length);

        var start = doc.Children[0].Index;
        var length = doc.Children[0].Length;
        Assert.AreEqual("# Test ☺️", input.Substring(start, length));

        var input2 = input.Replace("\r\n", "\r").Replace("\n", "\r");
        var doc2 = Parser.Execute(input2, Core.RuleSet);
        var html2 = Renderer.Execute(doc2, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html2.Replace("\r\n", "\n").Replace("\r", "\n").Trim());

        Assert.IsNotNull(doc2.Children);
        Assert.AreEqual("heading", doc2.Children[0].Type);
        Assert.AreEqual(1, doc2.Children[0].Index);
        Assert.AreEqual(9, doc2.Children[0].Length);

        var start2 = doc2.Children[0].Index;
        var length2 = doc2.Children[0].Length;
        Assert.AreEqual("# Test ☺️", input2.Substring(start2, length2));
    }
}
