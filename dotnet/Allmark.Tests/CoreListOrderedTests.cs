using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allmark.Rulesets;

namespace Allmark.Tests;

[TestClass]
public class ListOrderedTests
{
    [TestMethod]
    public void SimpleOrderedListWithPeriodDelimiter()
    {
        var input = @"
1. Item
";
        var expected = @"
<ol>
<li>Item</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void SimpleOrderedListWithParenDelimiter()
    {
        var input = @"
1) Item
";
        var expected = @"
<ol>
<li>Item</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void OrderedListStartingAt1()
    {
        var input = @"
1. Item
";
        var expected = @"
<ol>
<li>Item</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void OrderedListStartingAt2()
    {
        var input = @"
2. Item
";
        var expected = @"
<ol start=""2"">
<li>Item</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void OrderedListStartingAt10()
    {
        var input = @"
10. Item
";
        var expected = @"
<ol start=""10"">
<li>Item</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void OrderedListStartingAt0()
    {
        var input = @"
0. Item
";
        var expected = @"
<ol start=""0"">
<li>Item</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void OrderedListWithLargeStartNumber()
    {
        var input = @"
123456789. Item
";
        var expected = @"
<ol start=""123456789"">
<li>Item</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void OrderedListWithTooLargeNumber()
    {
        var input = @"
1234567890. Item
";
        var expected = @"
<p>1234567890. Item</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void OrderedListWithLeadingZeros()
    {
        var input = @"
003. Item
";
        var expected = @"
<ol start=""3"">
<li>Item</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void OrderedListWithMultipleItems()
    {
        var input = @"
1. Item 1
2. Item 2
3. Item 3
";
        var expected = @"
<ol>
<li>Item 1</li>
<li>Item 2</li>
<li>Item 3</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void OrderedListWithSequentialNumbersDisregarded()
    {
        var input = @"
1. Item 1
1. Item 2
1. Item 3
";
        var expected = @"
<ol>
<li>Item 1</li>
<li>Item 2</li>
<li>Item 3</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void OrderedListWithMixedNumbersDisregarded()
    {
        var input = @"
1. Item 1
5. Item 2
3. Item 3
";
        var expected = @"
<ol>
<li>Item 1</li>
<li>Item 2</li>
<li>Item 3</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void TightOrderedList()
    {
        var input = @"
1. Item 1
2. Item 2
";
        var expected = @"
<ol>
<li>Item 1</li>
<li>Item 2</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void LooseOrderedListWithBlankLines()
    {
        var input = @"
1. Item 1

2. Item 2
";
        var expected = @"
<ol>
<li>
<p>Item 1</p>
</li>
<li>
<p>Item 2</p>
</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void NestedOrderedList()
    {
        var input = @"
1. Item 1
   1. Nested item
2. Item 2
";
        var expected = @"
<ol>
<li>Item 1
<ol>
<li>Nested item</li>
</ol>
</li>
<li>Item 2</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void DeepNestedOrderedList()
    {
        var input = @"
1. Level 1
   1. Level 2
      1. Level 3
";
        var expected = @"
<ol>
<li>Level 1
<ol>
<li>Level 2
<ol>
<li>Level 3</li>
</ol>
</li>
</ol>
</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void OrderedListInBlockquote()
    {
        var input = @"
> 1. Item 1
> 2. Item 2
";
        var expected = @"
<blockquote>
<ol>
<li>Item 1</li>
<li>Item 2</li>
</ol>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void EmptyOrderedListItem()
    {
        var input = @"
1.
";
        var expected = @"
<ol>
<li></li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void OrderedListWithParagraphs()
    {
        var input = @"
1. Item 1

   Paragraph in item 1

2. Item 2
";
        var expected = @"
<ol>
<li>
<p>Item 1</p>
<p>Paragraph in item 1</p>
</li>
<li>
<p>Item 2</p>
</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void OrderedListPrecededByParagraph()
    {
        var input = @"
Paragraph

1. Item
";
        var expected = @"
<p>Paragraph</p>
<ol>
<li>Item</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void OrderedListFollowedByParagraph()
    {
        var input = @"
1. Item

Paragraph
";
        var expected = @"
<ol>
<li>Item</li>
</ol>
<p>Paragraph</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void MixedDelimitersShouldNotBeSameList()
    {
        var input = @"
1. Item 1
1) Item 2
";
        var expected = @"
<ol>
<li>Item 1</li>
</ol>
<ol>
<li>Item 2</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void OrderedListWithCodeBlock()
    {
        var input = @"
1. Item

   ```
   code
   ```
";
        var expected = @"
<ol>
<li>
<p>Item</p>
<pre><code>code
</code></pre>
</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void OrderedListWithInlineFormatting()
    {
        var input = @"
1. Item with *emphasis*
";
        var expected = @"
<ol>
<li>Item with <em>emphasis</em></li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void OrderedListWithBold()
    {
        var input = @"
1. Item with **bold**
";
        var expected = @"
<ol>
<li>Item with <strong>bold</strong></li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void OrderedListItemWithMultipleParagraphs()
    {
        var input = @"
1. Item 1

   Second paragraph

2. Item 2
";
        var expected = @"
<ol>
<li>
<p>Item 1</p>
<p>Second paragraph</p>
</li>
<li>
<p>Item 2</p>
</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void OrderedListWithLinks()
    {
        var input = @"
1. [Link](https://example.com)
";
        var expected = @"
<ol>
<li><a href=""https://example.com"">Link</a></li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void OrderedListWithCodeSpan()
    {
        var input = @"
1. `inline code`
";
        var expected = @"
<ol>
<li><code>inline code</code></li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void OrderedListAtEndOfDocument()
    {
        var input = @"
1. Item 1
2. Item 2
";
        var expected = @"
<ol>
<li>Item 1</li>
<li>Item 2</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void MultipleSeparateOrderedLists()
    {
        var input = @"
1. List 1 item 1
2. List 1 item 2

1. List 2 item 1
2. List 2 item 2
";
        var expected = @"
<ol>
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
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void OrderedListItemWithLeadingSpaces()
    {
        var input = @"
   1. Item
";
        var expected = @"
<ol>
<li>Item</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void OrderedListItemWith4SpacesIndentShouldBeCode()
    {
        var input = @"
    1. Item
";
        var expected = @"
<pre><code>1. Item
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void OrderedListWithOnlySpacesAfterMarker()
    {
        var input = @"
1.    Item
";
        var expected = @"
<ol>
<li>Item</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void NestedOrderedAndBulletedLists()
    {
        var input = @"
1. Ordered
   - Bulleted
      1. Nested ordered
";
        var expected = @"
<ol>
<li>Ordered
<ul>
<li>Bulleted
<ol>
<li>Nested ordered</li>
</ol>
</li>
</ul>
</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void OrderedListFollowedByBulletedList()
    {
        var input = @"
1. Item 1
2. Item 2
- Bullet 1
- Bullet 2
";
        var expected = @"
<ol>
<li>Item 1</li>
<li>Item 2</li>
</ol>
<ul>
<li>Bullet 1</li>
<li>Bullet 2</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void OrderedListWithParenDelimiterMultipleItems()
    {
        var input = @"
1) Item 1
2) Item 2
3) Item 3
";
        var expected = @"
<ol>
<li>Item 1</li>
<li>Item 2</li>
<li>Item 3</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void OrderedListWithParenDelimiterStartingAt5()
    {
        var input = @"
5) Item
";
        var expected = @"
<ol start=""5"">
<li>Item</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void OrderedListItemWithNestedBulletedList()
    {
        var input = @"
1. Item
   - Nested bullet
   - Another bullet
";
        var expected = @"
<ol>
<li>Item
<ul>
<li>Nested bullet</li>
<li>Another bullet</li>
</ul>
</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void NotAnOrderedListTextAfterNumber()
    {
        var input = @"
1.5 is a number
";
        var expected = @"
<p>1.5 is a number</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void NotAnOrderedListNoSpaceAfterDelimiter()
    {
        var input = @"
1.Item
";
        var expected = @"
<p>1.Item</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void OrderedListAtEndOfLineWithoutSpace()
    {
        var input = @"
1.
";
        var expected = @"
<ol>
<li></li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }

    [TestMethod]
    public void OrderedListWithThematicBreakInItem()
    {
        var input = @"
1. Item 1

   ---

2. Item 2
";
        var expected = @"
<ol>
<li>
<p>Item 1</p>
<hr />
</li>
<li>
<p>Item 2</p>
</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);
    }
}
