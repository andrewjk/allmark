using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allmark.Rulesets;

namespace Allmark.Tests;

[TestClass]
public class HtmlBlockTests
{
    [TestMethod]
    public void HtmlScriptTagSingleLine()
    {
        var input = @"<script>alert('hi');</script>";
        var expected = @"<script>alert('hi');</script>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlScriptTagMultiLine()
    {
        var input = @"<script>
alert('hi');
alert('bye');
</script>";
        var expected = @"<script>
alert('hi');
alert('bye');
</script>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlPreTag()
    {
        var input = @"<pre>code here</pre>";
        var expected = @"<pre>code here</pre>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlStyleTag()
    {
        var input = @"<style>body { color: red; }</style>";
        var expected = @"<style>body { color: red; }</style>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlCommentSingleLine()
    {
        var input = @"<!-- This is a comment -->";
        var expected = @"<!-- This is a comment -->";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlCommentMultiLine()
    {
        var input = @"<!--
This is a
multi-line comment
-->";
        var expected = @"<!--
This is a
multi-line comment
-->";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlProcessingInstruction()
    {
        var input = @"<?php echo 'hello'; ?>";
        var expected = @"<?php echo 'hello'; ?>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlDeclarationDOCTYPE()
    {
        var input = @"<!DOCTYPE html>";
        var expected = @"<!DOCTYPE html>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlCDATASection()
    {
        var input = @"<![CDATA[<greeting>Hello</greeting>]]>";
        var expected = @"<![CDATA[<greeting>Hello</greeting>]]>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlBlockLevelDivTag()
    {
        var input = @"<div>Content</div>";
        var expected = @"<div>Content</div>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlDivWithBlankLineAfter()
    {
        var input = @"<div>Content</div>

Next paragraph";
        var expected = @"<div>Content</div>
<p>Next paragraph</p>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlParagraphTag()
    {
        var input = @"<p>HTML paragraph</p>";
        var expected = @"<p>HTML paragraph</p>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlHeadingTags()
    {
        var input = @"<h1>Heading</h1>";
        var expected = @"<h1>Heading</h1>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlListTags()
    {
        var input = @"<ul>
<li>Item</li>
</ul>";
        var expected = @"<ul>
<li>Item</li>
</ul>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlTableTag()
    {
        var input = @"<table><tr><td>Cell</td></tr></table>";
        var expected = @"<table><tr><td>Cell</td></tr></table>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlBlockWithIndentationLessThan4Spaces()
    {
        var input = @"   <div>Indented</div>";
        var expected = @"   <div>Indented</div>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlBlockWith4SpaceIndentShouldBeCode()
    {
        var input = @"    <div>Code</div>";
        var expected = @"<pre><code>&lt;div&gt;Code&lt;/div&gt;
</code></pre>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlClosingTagAlone()
    {
        var input = @"</div>";
        var expected = @"</div>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlSelfClosingTag()
    {
        var input = @"<br />";
        var expected = @"<br />";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlImgTag()
    {
        var input = @"<img src=""image.jpg"" alt=""Image"">";
        var expected = @"<img src=""image.jpg"" alt=""Image"">";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlHrTag()
    {
        var input = @"<hr />";
        var expected = @"<hr />";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlBlockFollowedByMarkdown()
    {
        var input = @"<div>HTML</div>

# Markdown Heading";
        var expected = @"<div>HTML</div>
<h1>Markdown Heading</h1>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void ParagraphBeforeHtmlBlockType7ShouldNotInterrupt()
    {
        var input = @"Paragraph text
<span>inline</span>";
        var expected = @"<p>Paragraph text
<span>inline</span></p>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlCommentInParagraph()
    {
        var input = @"Text <!-- comment --> more text";
        var expected = @"<p>Text <!-- comment --> more text</p>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void MultipleHtmlBlocks()
    {
        var input = @"<div>First</div>

<div>Second</div>";
        var expected = @"<div>First</div>
<div>Second</div>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlBlockWithAttributes()
    {
        var input = @"<div class=""container"" id=""main"">Content</div>";
        var expected = @"<div class=""container"" id=""main"">Content</div>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlScriptTagWithAttributes()
    {
        var input = @"<script src=""script.js"" async></script>";
        var expected = @"<script src=""script.js"" async></script>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlFormTag()
    {
        var input = @"<form action=""/submit""> <input type=""text""> </form>";
        var expected = @"<form action=""/submit""> <input type=""text""> </form>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlBlockquoteTag()
    {
        var input = @"<blockquote>Quote</blockquote>";
        var expected = @"<blockquote>Quote</blockquote>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlAddressTag()
    {
        var input = @"<address>123 Main St</address>";
        var expected = @"<address>123 Main St</address>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlArticleTag()
    {
        var input = @"<article>Content</article>";
        var expected = @"<article>Content</article>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlAsideTag()
    {
        var input = @"<aside>Sidebar</aside>";
        var expected = @"<aside>Sidebar</aside>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlSectionTag()
    {
        var input = @"<section>Section</section>";
        var expected = @"<section>Section</section>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlNavTag()
    {
        var input = @"<nav>Menu</nav>";
        var expected = @"<nav>Menu</nav>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlFooterTag()
    {
        var input = @"<footer>Copyright</footer>";
        var expected = @"<footer>Copyright</footer>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlHeaderTag()
    {
        var input = @"<header>Header</header>";
        var expected = @"<header>Header</header>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlMainTag()
    {
        var input = @"<main>Main</main>";
        var expected = @"<main>Main</main>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlFigureTag()
    {
        var input = @"<figure><img src=""img.jpg""></figure>";
        var expected = @"<figure><img src=""img.jpg""></figure>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlFigcaptionTag()
    {
        var input = @"<figcaption>Caption</figcaption>";
        var expected = @"<figcaption>Caption</figcaption>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlDetailsAndSummaryTags()
    {
        var input = @"<details><summary>Click</summary>Content</details>";
        var expected = @"<details><summary>Click</summary>Content</details>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlDialogTag()
    {
        var input = @"<dialog>Dialog content</dialog>";
        var expected = @"<dialog>Dialog content</dialog>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlFieldsetTag()
    {
        var input = @"<fieldset>Field</fieldset>";
        var expected = @"<fieldset>Field</fieldset>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlLegendTag()
    {
        var input = @"<legend>Legend</legend>";
        var expected = @"<legend>Legend</legend>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlDlDtDdTags()
    {
        var input = @"<dl><dt>Term</dt><dd>Definition</dd></dl>";
        var expected = @"<dl><dt>Term</dt><dd>Definition</dd></dl>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlLinkTagEmpty()
    {
        var input = @"<link rel=""stylesheet"" href=""style.css"">";
        var expected = @"<link rel=""stylesheet"" href=""style.css"">";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlBaseTag()
    {
        var input = @"<base href=""https://example.com/"">";
        var expected = @"<base href=""https://example.com/"">";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlBasefontTag()
    {
        var input = @"<basefont face=""Arial"">";
        var expected = @"<basefont face=""Arial"">";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlCenterTag()
    {
        var input = @"<center>Centered</center>";
        var expected = @"<center>Centered</center>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlColAndColgroupTags()
    {
        var input = @"<colgroup><col span=""2""></colgroup>";
        var expected = @"<colgroup><col span=""2""></colgroup>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlTbodyTheadTfootTrThTdTags()
    {
        var input = @"<table><thead><tr><th>Header</th></tr></thead><tbody><tr><td>Data</td></tr></tbody></table>";
        var expected = @"<table><thead><tr><th>Header</th></tr></thead><tbody><tr><td>Data</td></tr></tbody></table>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlCaptionTag()
    {
        var input = @"<caption>Table caption</caption>";
        var expected = @"<caption>Table caption</caption>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlSourceTag()
    {
        var input = @"<source src=""video.mp4"" type=""video/mp4"">";
        var expected = @"<source src=""video.mp4"" type=""video/mp4"">";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlTrackTag()
    {
        var input = @"<track src=""captions.vtt"" kind=""captions"">";
        var expected = @"<track src=""captions.vtt"" kind=""captions"">";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlFramesetFrameNoframesTags()
    {
        var input = @"<frameset><frame src=""frame.html""></frameset>";
        var expected = @"<frameset><frame src=""frame.html""></frameset>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlNoframesTag()
    {
        var input = @"<noframes>No frames</noframes>";
        var expected = @"<noframes>No frames</noframes>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlIframeTag()
    {
        var input = @"<iframe src=""page.html""></iframe>";
        var expected = @"<iframe src=""page.html""></iframe>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlParamTag()
    {
        var input = @"<param name=""autoplay"" value=""true"">";
        var expected = @"<param name=""autoplay"" value=""true"">";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlDirTag()
    {
        var input = @"<dir><li>Item</li></dir>";
        var expected = @"<dir><li>Item</li></dir>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlMenuAndMenuitemTags()
    {
        var input = @"<menu><menuitem>Item</menuitem></menu>";
        var expected = @"<menu><menuitem>Item</menuitem></menu>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlOptgroupAndOptionTags()
    {
        var input = @"<select><optgroup label=""Group""><option>Item</option></optgroup></select>";
        var expected = @"<p><select><optgroup label=""Group""><option>Item</option></optgroup></select></p>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlTitleTagInBodyNotHead()
    {
        var input = @"<title>Page Title</title>";
        var expected = @"<title>Page Title</title>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlBlockWithLineBreaksInside()
    {
        var input = @"<div>
Line 1
Line 2
Line 3
</div>";
        var expected = @"<div>
Line 1
Line 2
Line 3
</div>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlBlockInsideList()
    {
        var input = @"- Item

  <div>HTML</div>";
        var expected = @"<ul>
<li>
<p>Item</p>
<div>HTML</div>
</li>
</ul>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlBlockInsideBlockquote()
    {
        var input = @"> <div>HTML</div>";
        var expected = @"<blockquote>
<div>HTML</div>
</blockquote>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlCaseInsensitiveUppercase()
    {
        var input = @"<DIV>Content</DIV>";
        var expected = @"<DIV>Content</DIV>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlScriptTagWithMixedCase()
    {
        var input = @"<SCRIPT>alert('hi');</SCRIPT>";
        var expected = @"<SCRIPT>alert('hi');</SCRIPT>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlBlockWithNoContent()
    {
        var input = @"<div></div>";
        var expected = @"<div></div>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlCustomTagNonBlockLevel()
    {
        var input = @"<custom-tag>Content</custom-tag>";
        var expected = @"<p><custom-tag>Content</custom-tag></p>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlCommentEndingOnSameLine()
    {
        var input = @"<!-- comment -->";
        var expected = @"<!-- comment -->";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlCommentWithMultipleDashes()
    {
        var input = @"<!-- --- comment --- -->";
        var expected = @"<!-- --- comment --- -->";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlCDATAWithEmbeddedBrackets()
    {
        var input = @"<![CDATA[<test>data</test>]]>";
        var expected = @"<![CDATA[<test>data</test>]]>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlDOCTYPEWithPublicIdentifier()
    {
        var input = @"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.01//EN"" ""http://www.w3.org/TR/html4/strict.dtd"">";
        var expected = @"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.01//EN"" ""http://www.w3.org/TR/html4/strict.dtd"">";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void HtmlBlockContinuesUntilBlankLineType6()
    {
        var input = @"<div>Line 1
Line 2
Line 3

Next";
        var expected = @"<div>Line 1
Line 2
Line 3
<p>Next</p>";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }


}
