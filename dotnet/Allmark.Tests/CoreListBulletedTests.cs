using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allmark.Rulesets;

namespace Allmark.Tests;

[TestClass]
public class CoreListBulletedTests
{
    [TestMethod]
    public void SimpleBulletedListWithDashes()
    {
        var input = @"
- Item
";
        var expected = @"
<ul>
<li>Item</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void SimpleBulletedListWithPlus()
    {
        var input = @"
+ Item
";
        var expected = @"
<ul>
<li>Item</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void SimpleBulletedListWithAsterisks()
    {
        var input = @"
* Item
";
        var expected = @"
<ul>
<li>Item</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void BulletedListWithMultipleItems()
    {
        var input = @"
- Item 1
- Item 2
- Item 3
";
        var expected = @"
<ul>
<li>Item 1</li>
<li>Item 2</li>
<li>Item 3</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void TightBulletedList()
    {
        var input = @"
- Item 1
- Item 2
";
        var expected = @"
<ul>
<li>Item 1</li>
<li>Item 2</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void LooseBulletedListWithBlankLines()
    {
        var input = @"
- Item 1

- Item 2
";
        var expected = @"
<ul>
<li>
<p>Item 1</p>
</li>
<li>
<p>Item 2</p>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void NestedBulletedLists()
    {
        var input = @"
- Item 1
  - Nested item
- Item 2
";
        var expected = @"
<ul>
<li>Item 1
<ul>
<li>Nested item</li>
</ul>
</li>
<li>Item 2</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void DeepNestedBulletedLists()
    {
        var input = @"
- Level 1
  - Level 2
    - Level 3
";
        var expected = @"
<ul>
<li>Level 1
<ul>
<li>Level 2
<ul>
<li>Level 3</li>
</ul>
</li>
</ul>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void BulletedListInBlockquote()
    {
        var input = @"
> - Item 1
> - Item 2
";
        var expected = @"
<blockquote>
<ul>
<li>Item 1</li>
<li>Item 2</li>
</ul>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void EmptyListItem()
    {
        var input = @"
-
";
        var expected = @"
<ul>
<li></li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void BulletedListWithParagraphs()
    {
        var input = @"
- Item 1

  Paragraph in item 1

- Item 2
";
        var expected = @"
<ul>
<li>
<p>Item 1</p>
<p>Paragraph in item 1</p>
</li>
<li>
<p>Item 2</p>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void BulletedListPrecededByParagraph()
    {
        var input = @"
Paragraph

- Item
";
        var expected = @"
<p>Paragraph</p>
<ul>
<li>Item</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void BulletedListFollowedByParagraph()
    {
        var input = @"
- Item

Paragraph
";
        var expected = @"
<ul>
<li>Item</li>
</ul>
<p>Paragraph</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void MixedBulletMarkersShouldNotBeSameList()
    {
        var input = @"
- Item 1
+ Item 2
";
        var expected = @"
<ul>
<li>Item 1</li>
</ul>
<ul>
<li>Item 2</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void BulletedListWithCodeBlock()
    {
        var input = @"
- Item

  ```
  code
  ```
";
        var expected = @"
<ul>
<li>
<p>Item</p>
<pre><code>code
</code></pre>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void BulletedListWithInlineFormatting()
    {
        var input = @"
- Item with *emphasis*
";
        var expected = @"
<ul>
<li>Item with <em>emphasis</em></li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void BulletedListWithBold()
    {
        var input = @"
- Item with **bold**
";
        var expected = @"
<ul>
<li>Item with <strong>bold</strong></li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void BulletedListItemWithMultipleParagraphsLoose()
    {
        var input = @"
- Item 1

  Second paragraph

- Item 2
";
        var expected = @"
<ul>
<li>
<p>Item 1</p>
<p>Second paragraph</p>
</li>
<li>
<p>Item 2</p>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void BulletedListWithLinks()
    {
        var input = @"
- [Link](https://example.com)
";
        var expected = @"
<ul>
<li><a href=""https://example.com"">Link</a></li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void BulletedListWithCodeSpan()
    {
        var input = @"
- `inline code`
";
        var expected = @"
<ul>
<li><code>inline code</code></li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void BulletedListAtEndOfDocument()
    {
        var input = @"
- Item 1
- Item 2
";
        var expected = @"
<ul>
<li>Item 1</li>
<li>Item 2</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void MultipleSeparateBulletedLists()
    {
        var input = @"
- List 1 item 1
- List 1 item 2

- List 2 item 1
- List 2 item 2
";
        var expected = @"
<ul>
<li>
<p>List 1 item 1</p>
</li>
<li>
<p>List 1 item 2</p>
</li>
<li>
<p>List 2 item 1</p>
</li>
<li>
<p>List 2 item 2</p>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void BulletedListItemWithLeadingSpacesStillAList()
    {
        var input = @"
   - Item
";
        var expected = @"
<ul>
<li>Item</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void BulletedListItemWith4SpacesIndentShouldBeCode()
    {
        var input = @"
    - Item
";
        var expected = @"
<pre><code>- Item
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void BulletedListWithOnlySpacesAfterMarker()
    {
        var input = @"
-    Item
";
        var expected = @"
<ul>
<li>Item</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void NestedListsWithDifferentMarkers()
    {
        var input = @"
- Dash
  + Plus
    * Star
";
        var expected = @"
<ul>
<li>Dash
<ul>
<li>Plus
<ul>
<li>Star</li>
</ul>
</li>
</ul>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void BulletedListFollowedImmediatelyByOrderedList()
    {
        var input = @"
- Item 1
- Item 2
1. Ordered 1
2. Ordered 2
";
        var expected = @"
<ul>
<li>Item 1</li>
<li>Item 2</li>
</ul>
<ol>
<li>Ordered 1</li>
<li>Ordered 2</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }

    [TestMethod]
    public void BulletedListWithThematicBreakInItem()
    {
        var input = @"
- Item 1

  ---

- Item 2
";
        var expected = @"
<ul>
<li>
<p>Item 1</p>
<hr />
</li>
<li>
<p>Item 2</p>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));
    }
}