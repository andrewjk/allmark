using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allmark.Rulesets;

namespace Allmark.Tests;

[TestClass]
public class FencedCodeTests
{
    [TestMethod]
    public void SimpleCodeFenceWithBackticks()
    {
        var input = """
		```
		code
		```
		""";
        var expected = """
		<pre><code>code
		</code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void SimpleCodeFenceWithTildes()
    {
        var input = """
		~~~
		code
		~~~
		""";
        var expected = """
		<pre><code>code
		</code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceWith4Backticks()
    {
        var input = """
		````
		code
		````
		""";
        var expected = """
		<pre><code>code
		</code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceWith5Tildes()
    {
        var input = """
		~~~~~
		code
		~~~~~
		""";
        var expected = """
		<pre><code>code
		</code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceWithLanguageSpecifier()
    {
        var input = """
		```javascript
		const x = 1;
		```
		""";
        var expected = """
		<pre><code class="language-javascript">const x = 1;
		</code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceWithLanguageSpecifierAndExtraText()
    {
        var input = """
		```javascript extra
		const x = 1;
		```
		""";
        var expected = """
		<pre><code class="language-javascript">const x = 1;
		</code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceWithEmptyContent()
    {
        var input = """
		```
		```
		""";
        var expected = """
		<pre><code></code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceWithMultilineContent()
    {
        var input = """
		```
		line 1
		line 2
		line 3
		```
		""";
        var expected = """
		<pre><code>line 1
		line 2
		line 3
		</code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceWith1SpaceIndent()
    {
        var input = """
		 ```
		code
		```
		""";
        var expected = """
		<pre><code>code
		</code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceWith3SpaceIndent()
    {
        var input = """
		   ```
		code
		```
		""";
        var expected = """
		<pre><code>code
		</code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceWith4SpaceIndentShouldBeCode()
    {
        var input = """
		    ```
		code
		```
		""";
        var expected = """
		<pre><code>```
		</code></pre>
		<p>code</p>
		<pre><code></code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceInterruptsParagraph()
    {
        var input = """
		Paragraph
		```
		code
		```
		""";
        var expected = """
		<p>Paragraph</p>
		<pre><code>code
		</code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceWithoutSpaceAfterOpening()
    {
        var input = """
		```code
		```
		""";
        var expected = """
		<pre><code class="language-code"></code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceWithBlankLineInContent()
    {
        var input = """
		```
		line 1

		line 2
		```
		""";
        var expected = """
		<pre><code>line 1

		line 2
		</code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceNotValidOnly2Backticks()
    {
        var input = """
		``
		code
		``
		""";
        var expected = """
		<p><code>code</code></p>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceNotValidOnly2Tildes()
    {
        var input = """
		~~
		code
		~~
		""";
        var expected = """
		<p>~~
		code
		~~</p>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceNotValidMixedBackticksAndTildes()
    {
        var input = """
		`~`
		code
		`~`
		""";
        var expected = """
		<p><code>~</code>
		code
		<code>~</code></p>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceNotValidInfoStringWithBackticks()
    {
        var input = """
		```code with backtick`
		code
		```
		""";
        var expected = """
		<p>```code with backtick`
		code</p>
		<pre><code></code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceWithBackticksInContent()
    {
        var input = """
		```
		code with `backticks`
		```
		""";
        var expected = """
		<pre><code>code with `backticks`
		</code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceWithTildesInContent()
    {
        var input = """
		~~~
		code with ~tildes~
		~~~
		""";
        var expected = """
		<pre><code>code with ~tildes~
		</code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFencePrecededByParagraphWithoutBlankLine()
    {
        var input = """
		Paragraph
		```
		code
		```
		""";
        var expected = """
		<p>Paragraph</p>
		<pre><code>code
		</code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceFollowedByParagraphWithoutBlankLine()
    {
        var input = """
		```
		code
		```
		Paragraph
		""";
        var expected = """
		<pre><code>code
		</code></pre>
		<p>Paragraph</p>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void MultipleCodeFences()
    {
        var input = """
		```
		code1
		```

		```
		code2
		```
		""";
        var expected = """
		<pre><code>code1
		</code></pre>
		<pre><code>code2
		</code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceWithInlineMarkdownInContent()
    {
        var input = """
		```
		*not italic*
		**not bold**
		```
		""";
        var expected = """
		<pre><code>*not italic*
		**not bold**
		</code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceAtEndOfDocument()
    {
        var input = """
		```
		code
		```
		""";
        var expected = """
		<pre><code>code
		</code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceWithTrailingSpacesAfterClosing()
    {
        var input = """
		```
		code
		```   
		""";
        var expected = """
		<pre><code>code
		</code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceWithVeryLongOpening()
    {
        var input = """
		``````````````
		code
		``````````````
		""";
        var expected = """
		<pre><code>code
		</code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceWithShorterClosing()
    {
        var input = """
		`````
		code
		```
		""";
        var expected = """
		<pre><code>code
		```
		</code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceNotValidClosingFenceShorterThanOpening()
    {
        var input = """
		```
		code
		``
		""";
        var expected = """
		<pre><code>code
		``
		</code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceWithLanguageContainingNumbers()
    {
        var input = """
		```python3
		import x
		```
		""";
        var expected = """
		<pre><code class="language-python3">import x
		</code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceWithLanguageContainingDashes()
    {
        var input = """
		```c++
		int main() {}
		```
		""";
        var expected = """
		<pre><code class="language-c++">int main() {}
		</code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceBetweenParagraphs()
    {
        var input = """
		Paragraph 1

		```
		code
		```

		Paragraph 2
		""";
        var expected = """
		<p>Paragraph 1</p>
		<pre><code>code
		</code></pre>
		<p>Paragraph 2</p>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceWithBackslashInInfoString()
    {
        var input = """
		```javascript\test
		code
		```
		""";
        var expected = """
		<pre><code class="language-javascript\test">code
		</code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceWithIndentedContentLines()
    {
        var input = """
		```
		   indented
		not indented
		```
		""";
        var expected = """
		<pre><code>   indented
		not indented
		</code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceNotValidSpaceBetweenFenceChars()
    {
        var input = """
		` ` `
		code
		` ` `
		""";
        var expected = """
		<p><code> </code> <code>code</code> <code> </code></p>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceWithOnlyInfoString()
    {
        var input = """
		```javascript
		```
		""";
        var expected = """
		<pre><code class="language-javascript"></code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceWithSetextHeadingAbove()
    {
        var input = """
		Heading
		=======
		```
		code
		```
		""";
        var expected = """
		<h1>Heading</h1>
		<pre><code>code
		</code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceWithTrailingSpacesAfterOpening()
    {
        var input = """
		```   
		code
		```
		""";
        var expected = """
		<pre><code>code
		</code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceWithHTMLEntitiesInInfo()
    {
        var input = """
		```&lt;test&gt;
		code
		```
		""";
        var expected = """
		<pre><code class="language-&lt;test&gt;">code
		</code></pre>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceWithAtxHeadingBelow()
    {
        var input = """
		```
		code
		```
		# Heading
		""";
        var expected = """
		<pre><code>code
		</code></pre>
		<h1>Heading</h1>
		""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceInBlockquote()
    {
        var input = """
> ```
code
```
""";
        var expected = """
<blockquote>
<pre><code></code></pre>
</blockquote>
<p>code</p>
<pre><code></code></pre>
""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceInListItem()
    {
        var input = """
- ```
code
```
""";
        var expected = """
<ul>
<li>
<pre><code></code></pre>
</li>
</ul>
<p>code</p>
<pre><code></code></pre>
""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }

    [TestMethod]
    public void CodeFenceWithTrailingWhitespaceOnClosingFence()
    {
        var input = """
```
code
```   
""";
        var expected = """
<pre><code>code
</code></pre>
""";
        var doc = Parser.Execute(input, Core.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);
        Assert.AreEqual(expected.Trim(), html.Trim());
    }
}
