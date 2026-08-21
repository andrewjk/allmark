using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allmark.Rulesets;

namespace Allmark.Tests;

[TestClass]
public class CoreFrontmatterTests
{
    [TestMethod]
    public void FrontmatterWithYAMLDelimiters()
    {
        var input = @"
---
title: Test
date: 2024-01-01
---

# Heading

Content
";
        var expected = @"
<h1>Heading</h1>
<p>Content</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void FrontmatterAtDocumentStartOnly()
    {
        var input = @"
# Heading

---
title: Test
---

Content
";
        var expected = @"
<h1>Heading</h1>
<hr />
<h2>title: Test</h2>
<p>Content</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void FrontmatterWithSingleLineContent()
    {
        var input = @"
---
title: Test
---

# Heading
";
        var expected = @"
<h1>Heading</h1>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void FrontmatterWithMultipleLines()
    {
        var input = @"
---
title: Test
date: 2024-01-01
author: John Doe
tags:
  - one
  - two
---

# Heading
";
        var expected = @"
<h1>Heading</h1>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void FrontmatterWithClosingDelimiterOnSeparateLine()
    {
        var input = @"
---
title: Test

---

# Heading
";
        var expected = @"
<h1>Heading</h1>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void FrontmatterWithContentFollowingClosingDelimiter()
    {
        var input = @"
---
title: Test
---
Content here
";
        var expected = @"
<p>Content here</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Core.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }
}