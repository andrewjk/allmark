using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allmark.Rulesets;

namespace Allmark.Tests;

[TestClass]
public class SpecCmTests
{
	[TestMethod]
	public void Example1()
	{
		var input = @"
	foo	baz		bim
";
		var expected = @"
<pre><code>foo	baz		bim
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example2()
	{
		var input = @"
  	foo	baz		bim
";
		var expected = @"
<pre><code>foo	baz		bim
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example3()
	{
		var input = @"
    a	a
    ὐ	a
";
		var expected = @"
<pre><code>a	a
ὐ	a
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example4()
	{
		var input = @"
  - foo

	bar
";
		var expected = @"
<ul>
<li>
<p>foo</p>
<p>bar</p>
</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example5()
	{
		var input = @"
- foo

		bar
";
		var expected = @"
<ul>
<li>
<p>foo</p>
<pre><code>  bar
</code></pre>
</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example6()
	{
		var input = @"
>		foo
";
		var expected = @"
<blockquote>
<pre><code>  foo
</code></pre>
</blockquote>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example7()
	{
		var input = @"
-		foo
";
		var expected = @"
<ul>
<li>
<pre><code>  foo
</code></pre>
</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example8()
	{
		var input = @"
    foo
	bar
";
		var expected = @"
<pre><code>foo
bar
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example9()
	{
		var input = @"
 - foo
   - bar
	 - baz
";
		var expected = @"
<ul>
<li>foo
<ul>
<li>bar
<ul>
<li>baz</li>
</ul>
</li>
</ul>
</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example10()
	{
		var input = @"
#	Foo
";
		var expected = @"
<h1>Foo</h1>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example11()
	{
		var input = @"
*	*	*	
";
		var expected = @"
<hr />
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example12()
	{
		var input = @"
\!\""\#\$\%\&\'\(\)\*\+\,\-\.\/\:\;\<\=\>\?\@\[\\\]\^\_\`\{\|\}\~
";
		var expected = @"
<p>!&quot;#$%&amp;'()*+,-./:;&lt;=&gt;?@[\]^_`{|}~</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example13()
	{
		var input = @"
\	\A\a\ \3\φ\«
";
		var expected = @"
<p>\	\A\a\ \3\φ\«</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example14()
	{
		var input = @"
\*not emphasized*
\<br/> not a tag
\[not a link](/foo)
\`not code`
1\. not a list
\* not a list
\# not a heading
\[foo]: /url ""not a reference""
\&ouml; not a character entity
";
		var expected = @"
<p>*not emphasized*
&lt;br/&gt; not a tag
[not a link](/foo)
`not code`
1. not a list
* not a list
# not a heading
[foo]: /url &quot;not a reference&quot;
&amp;ouml; not a character entity</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example15()
	{
		var input = @"
\\*emphasis*
";
		var expected = @"
<p>\<em>emphasis</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example16()
	{
		var input = @"
foo\
bar
";
		var expected = @"
<p>foo<br />
bar</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example17()
	{
		var input = @"
`` \[\` ``
";
		var expected = @"
<p><code>\[\`</code></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example18()
	{
		var input = @"
    \[\]
";
		var expected = @"
<pre><code>\[\]
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example19()
	{
		var input = @"
~~~
\[\]
~~~
";
		var expected = @"
<pre><code>\[\]
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example20()
	{
		var input = @"
<https://example.com?find=\*>
";
		var expected = @"
<p><a href=""https://example.com?find=%5C*"">https://example.com?find=\*</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example21()
	{
		var input = @"
<a href=""/bar\/)"">
";
		var expected = @"
<a href=""/bar\/)"">
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example22()
	{
		var input = @"
[foo](/bar\* ""ti\*tle"")
";
		var expected = @"
<p><a href=""/bar*"" title=""ti*tle"">foo</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example23()
	{
		var input = @"
[foo]

[foo]: /bar\* ""ti\*tle""
";
		var expected = @"
<p><a href=""/bar*"" title=""ti*tle"">foo</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example24()
	{
		var input = @"
``` foo\+bar
foo
```
";
		var expected = @"
<pre><code class=""language-foo+bar"">foo
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example25()
	{
		var input = @"
&nbsp; &amp; &copy; &AElig; &Dcaron;
&frac34; &HilbertSpace; &DifferentialD;
&ClockwiseContourIntegral; &ngE;
";
		var expected = @"
<p>  &amp; © Æ Ď
¾ ℋ ⅆ
∲ ≧̸</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example26()
	{
		var input = @"
&#35; &#1234; &#992; &#0;
";
		var expected = @"
<p># Ӓ Ϡ �</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example27()
	{
		var input = @"
&#X22; &#XD06; &#xcab;
";
		var expected = @"
<p>&quot; ആ ಫ</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example28()
	{
		var input = @"
&nbsp &x; &#; &#x;
&#87654321;
&#abcdef0;
&ThisIsNotDefined; &hi?;
";
		var expected = @"
<p>&amp;nbsp &amp;x; &amp;#; &amp;#x;
&amp;#87654321;
&amp;#abcdef0;
&amp;ThisIsNotDefined; &amp;hi?;</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example29()
	{
		var input = @"
&copy
";
		var expected = @"
<p>&amp;copy</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example30()
	{
		var input = @"
&MadeUpEntity;
";
		var expected = @"
<p>&amp;MadeUpEntity;</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example31()
	{
		var input = @"
<a href=""&ouml;&ouml;.html"">
";
		var expected = @"
<a href=""&ouml;&ouml;.html"">
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example32()
	{
		var input = @"
[foo](/f&ouml;&ouml; ""f&ouml;&ouml;"")
";
		var expected = @"
<p><a href=""/f%C3%B6%C3%B6"" title=""föö"">foo</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example33()
	{
		var input = @"
[foo]

[foo]: /f&ouml;&ouml; ""f&ouml;&ouml;""
";
		var expected = @"
<p><a href=""/f%C3%B6%C3%B6"" title=""föö"">foo</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example34()
	{
		var input = @"
``` f&ouml;&ouml;
foo
```
";
		var expected = @"
<pre><code class=""language-föö"">foo
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example35()
	{
		var input = @"
`f&ouml;&ouml;`
";
		var expected = @"
<p><code>f&amp;ouml;&amp;ouml;</code></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example36()
	{
		var input = @"
    f&ouml;f&ouml;
";
		var expected = @"
<pre><code>f&amp;ouml;f&amp;ouml;
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example37()
	{
		var input = @"
&#42;foo&#42;
*foo*
";
		var expected = @"
<p>*foo*
<em>foo</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example38()
	{
		var input = @"
&#42; foo

* foo
";
		var expected = @"
<p>* foo</p>
<ul>
<li>foo</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example39()
	{
		var input = @"
foo&#10;&#10;bar
";
		var expected = @"
<p>foo

bar</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example40()
	{
		var input = @"
&#9;foo
";
		var expected = @"
<p>	foo</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example41()
	{
		var input = @"
[a](url &quot;tit&quot;)
";
		var expected = @"
<p>[a](url &quot;tit&quot;)</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example42()
	{
		var input = @"
- `one
- two`
";
		var expected = @"
<ul>
<li>`one</li>
<li>two`</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example43()
	{
		var input = @"
***
---
___
";
		var expected = @"
<hr />
<hr />
<hr />
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example44()
	{
		var input = @"
+++
";
		var expected = @"
<p>+++</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example45()
	{
		var input = @"
===
";
		var expected = @"
<p>===</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example46()
	{
		var input = @"
--
**
__
";
		var expected = @"
<p>--
**
__</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example47()
	{
		var input = @"
 ***
  ***
   ***
";
		var expected = @"
<hr />
<hr />
<hr />
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example48()
	{
		var input = @"
    ***
";
		var expected = @"
<pre><code>***
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example49()
	{
		var input = @"
Foo
    ***
";
		var expected = @"
<p>Foo
***</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example50()
	{
		var input = @"
_____________________________________
";
		var expected = @"
<hr />
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example51()
	{
		var input = @"
 - - -
";
		var expected = @"
<hr />
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example52()
	{
		var input = @"
 **  * ** * ** * **
";
		var expected = @"
<hr />
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example53()
	{
		var input = @"
-     -      -      -
";
		var expected = @"
<hr />
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example54()
	{
		var input = @"
- - - -    
";
		var expected = @"
<hr />
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example55()
	{
		var input = @"
_ _ _ _ a

a------

---a---
";
		var expected = @"
<p>_ _ _ _ a</p>
<p>a------</p>
<p>---a---</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example56()
	{
		var input = @"
 *-*
";
		var expected = @"
<p><em>-</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example57()
	{
		var input = @"
- foo
***
- bar
";
		var expected = @"
<ul>
<li>foo</li>
</ul>
<hr />
<ul>
<li>bar</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example58()
	{
		var input = @"
Foo
***
bar
";
		var expected = @"
<p>Foo</p>
<hr />
<p>bar</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example59()
	{
		var input = @"
Foo
---
bar
";
		var expected = @"
<h2>Foo</h2>
<p>bar</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example60()
	{
		var input = @"
* Foo
* * *
* Bar
";
		var expected = @"
<ul>
<li>Foo</li>
</ul>
<hr />
<ul>
<li>Bar</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example61()
	{
		var input = @"
- Foo
- * * *
";
		var expected = @"
<ul>
<li>Foo</li>
<li>
<hr />
</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example62()
	{
		var input = @"
# foo
## foo
### foo
#### foo
##### foo
###### foo
";
		var expected = @"
<h1>foo</h1>
<h2>foo</h2>
<h3>foo</h3>
<h4>foo</h4>
<h5>foo</h5>
<h6>foo</h6>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example63()
	{
		var input = @"
####### foo
";
		var expected = @"
<p>####### foo</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example64()
	{
		var input = @"
#5 bolt

#hashtag
";
		var expected = @"
<p>#5 bolt</p>
<p>#hashtag</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example65()
	{
		var input = @"
\## foo
";
		var expected = @"
<p>## foo</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example66()
	{
		var input = @"
# foo *bar* \*baz\*
";
		var expected = @"
<h1>foo <em>bar</em> *baz*</h1>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example67()
	{
		var input = @"
#                  foo                     
";
		var expected = @"
<h1>foo</h1>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example68()
	{
		var input = @"
 ### foo
  ## foo
   # foo
";
		var expected = @"
<h3>foo</h3>
<h2>foo</h2>
<h1>foo</h1>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example69()
	{
		var input = @"
    # foo
";
		var expected = @"
<pre><code># foo
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example70()
	{
		var input = @"
foo
    # bar
";
		var expected = @"
<p>foo
# bar</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example71()
	{
		var input = @"
## foo ##
  ###   bar    ###
";
		var expected = @"
<h2>foo</h2>
<h3>bar</h3>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example72()
	{
		var input = @"
# foo ##################################
##### foo ##
";
		var expected = @"
<h1>foo</h1>
<h5>foo</h5>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example73()
	{
		var input = @"
### foo ###     
";
		var expected = @"
<h3>foo</h3>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example74()
	{
		var input = @"
### foo ### b
";
		var expected = @"
<h3>foo ### b</h3>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example75()
	{
		var input = @"
# foo#
";
		var expected = @"
<h1>foo#</h1>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example76()
	{
		var input = @"
### foo \###
## foo #\##
# foo \#
";
		var expected = @"
<h3>foo ###</h3>
<h2>foo ###</h2>
<h1>foo #</h1>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example77()
	{
		var input = @"
****
## foo
****
";
		var expected = @"
<hr />
<h2>foo</h2>
<hr />
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example78()
	{
		var input = @"
Foo bar
# baz
Bar foo
";
		var expected = @"
<p>Foo bar</p>
<h1>baz</h1>
<p>Bar foo</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example79()
	{
		var input = @"
## 
#
### ###
";
		var expected = @"
<h2></h2>
<h1></h1>
<h3></h3>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example80()
	{
		var input = @"
Foo *bar*
=========

Foo *bar*
---------
";
		var expected = @"
<h1>Foo <em>bar</em></h1>
<h2>Foo <em>bar</em></h2>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example81()
	{
		var input = @"
Foo *bar
baz*
====
";
		var expected = @"
<h1>Foo <em>bar
baz</em></h1>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example82()
	{
		var input = @"
  Foo *bar
baz*	
====
";
		var expected = @"
<h1>Foo <em>bar
baz</em></h1>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example83()
	{
		var input = @"
Foo
-------------------------

Foo
=
";
		var expected = @"
<h2>Foo</h2>
<h1>Foo</h1>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example84()
	{
		var input = @"
   Foo
---

  Foo
-----

  Foo
  ===
";
		var expected = @"
<h2>Foo</h2>
<h2>Foo</h2>
<h1>Foo</h1>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example85()
	{
		var input = @"
    Foo
    ---

    Foo
---
";
		var expected = @"
<pre><code>Foo
---

Foo
</code></pre>
<hr />
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example86()
	{
		var input = @"
Foo
   ----      
";
		var expected = @"
<h2>Foo</h2>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example87()
	{
		var input = @"
Foo
    ---
";
		var expected = @"
<p>Foo
---</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example88()
	{
		var input = @"
Foo
= =

Foo
--- -
";
		var expected = @"
<p>Foo
= =</p>
<p>Foo</p>
<hr />
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example89()
	{
		var input = @"
Foo  
-----
";
		var expected = @"
<h2>Foo</h2>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example90()
	{
		var input = @"
Foo\
----
";
		var expected = @"
<h2>Foo\</h2>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example91()
	{
		var input = @"
`Foo
----
`

<a title=""a lot
---
of dashes""/>
";
		var expected = @"
<h2>`Foo</h2>
<p>`</p>
<h2>&lt;a title=&quot;a lot</h2>
<p>of dashes&quot;/&gt;</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example92()
	{
		var input = @"
> Foo
---
";
		var expected = @"
<blockquote>
<p>Foo</p>
</blockquote>
<hr />
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example93()
	{
		var input = @"
> foo
bar
===
";
		var expected = @"
<blockquote>
<p>foo
bar
===</p>
</blockquote>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example94()
	{
		var input = @"
- Foo
---
";
		var expected = @"
<ul>
<li>Foo</li>
</ul>
<hr />
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example95()
	{
		var input = @"
Foo
Bar
---
";
		var expected = @"
<h2>Foo
Bar</h2>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example96()
	{
		var input = @"
---
Foo
---
Bar
---
Baz
";
		var expected = @"
<hr />
<h2>Foo</h2>
<h2>Bar</h2>
<p>Baz</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example97()
	{
		var input = @"

====
";
		var expected = @"
<p>====</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example98()
	{
		var input = @"
---
---
";
		var expected = @"
<hr />
<hr />
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example99()
	{
		var input = @"
- foo
-----
";
		var expected = @"
<ul>
<li>foo</li>
</ul>
<hr />
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example100()
	{
		var input = @"
    foo
---
";
		var expected = @"
<pre><code>foo
</code></pre>
<hr />
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example101()
	{
		var input = @"
> foo
-----
";
		var expected = @"
<blockquote>
<p>foo</p>
</blockquote>
<hr />
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example102()
	{
		var input = @"
\> foo
------
";
		var expected = @"
<h2>&gt; foo</h2>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example103()
	{
		var input = @"
Foo

bar
---
baz
";
		var expected = @"
<p>Foo</p>
<h2>bar</h2>
<p>baz</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example104()
	{
		var input = @"
Foo
bar

---

baz
";
		var expected = @"
<p>Foo
bar</p>
<hr />
<p>baz</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example105()
	{
		var input = @"
Foo
bar
* * *
baz
";
		var expected = @"
<p>Foo
bar</p>
<hr />
<p>baz</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example106()
	{
		var input = @"
Foo
bar
\---
baz
";
		var expected = @"
<p>Foo
bar
---
baz</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example107()
	{
		var input = @"
    a simple
      indented code block
";
		var expected = @"
<pre><code>a simple
  indented code block
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example108()
	{
		var input = @"
  - foo

    bar
";
		var expected = @"
<ul>
<li>
<p>foo</p>
<p>bar</p>
</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example109()
	{
		var input = @"
1.  foo

    - bar
";
		var expected = @"
<ol>
<li>
<p>foo</p>
<ul>
<li>bar</li>
</ul>
</li>
</ol>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example110()
	{
		var input = @"
    <a/>
    *hi*

    - one
";
		var expected = @"
<pre><code>&lt;a/&gt;
*hi*

- one
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example111()
	{
		var input = @"
    chunk1

    chunk2
  
 
 
    chunk3
";
		var expected = @"
<pre><code>chunk1

chunk2



chunk3
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example112()
	{
		var input = @"
    chunk1
      
      chunk2
";
		var expected = @"
<pre><code>chunk1
  
  chunk2
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example113()
	{
		var input = @"
Foo
    bar

";
		var expected = @"
<p>Foo
bar</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example114()
	{
		var input = @"
    foo
bar
";
		var expected = @"
<pre><code>foo
</code></pre>
<p>bar</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example115()
	{
		var input = @"
# Heading
    foo
Heading
------
    foo
----
";
		var expected = @"
<h1>Heading</h1>
<pre><code>foo
</code></pre>
<h2>Heading</h2>
<pre><code>foo
</code></pre>
<hr />
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example116()
	{
		var input = @"
        foo
    bar
";
		var expected = @"
<pre><code>    foo
bar
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example117()
	{
		var input = @"

    
    foo
    

";
		var expected = @"
<pre><code>foo
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example118()
	{
		var input = @"
    foo  
";
		var expected = @"
<pre><code>foo  
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example119()
	{
		var input = @"
```
<
 >
```
";
		var expected = @"
<pre><code>&lt;
 &gt;
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example120()
	{
		var input = @"
~~~
<
 >
~~~
";
		var expected = @"
<pre><code>&lt;
 &gt;
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example121()
	{
		var input = @"
``
foo
``
";
		var expected = @"
<p><code>foo</code></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example122()
	{
		var input = @"
```
aaa
~~~
```
";
		var expected = @"
<pre><code>aaa
~~~
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example123()
	{
		var input = @"
~~~
aaa
```
~~~
";
		var expected = @"
<pre><code>aaa
```
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example124()
	{
		var input = @"
````
aaa
```
``````
";
		var expected = @"
<pre><code>aaa
```
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example125()
	{
		var input = @"
~~~~
aaa
~~~
~~~~
";
		var expected = @"
<pre><code>aaa
~~~
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example126()
	{
		var input = @"
```
";
		var expected = @"
<pre><code></code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example127()
	{
		var input = @"
`````

```
aaa
";
		var expected = @"
<pre><code>
```
aaa
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example128()
	{
		var input = @"
> ```
> aaa

bbb
";
		var expected = @"
<blockquote>
<pre><code>aaa
</code></pre>
</blockquote>
<p>bbb</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example129()
	{
		var input = @"
```

  
```
";
		var expected = @"
<pre><code>
  
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example130()
	{
		var input = @"
```
```
";
		var expected = @"
<pre><code></code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example131()
	{
		var input = @"
 ```
 aaa
aaa
```
";
		var expected = @"
<pre><code>aaa
aaa
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example132()
	{
		var input = @"
  ```
aaa
  aaa
aaa
  ```
";
		var expected = @"
<pre><code>aaa
aaa
aaa
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example133()
	{
		var input = @"
   ```
   aaa
    aaa
  aaa
   ```
";
		var expected = @"
<pre><code>aaa
 aaa
aaa
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example134()
	{
		var input = @"
    ```
    aaa
    ```
";
		var expected = @"
<pre><code>```
aaa
```
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example135()
	{
		var input = @"
```
aaa
  ```
";
		var expected = @"
<pre><code>aaa
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example136()
	{
		var input = @"
   ```
aaa
  ```
";
		var expected = @"
<pre><code>aaa
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example137()
	{
		var input = @"
```
aaa
    ```
";
		var expected = @"
<pre><code>aaa
    ```
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example138()
	{
		var input = @"
``` ```
aaa
";
		var expected = @"
<p><code> </code>
aaa</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example139()
	{
		var input = @"
~~~~~~
aaa
~~~ ~~
";
		var expected = @"
<pre><code>aaa
~~~ ~~
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example140()
	{
		var input = @"
foo
```
bar
```
baz
";
		var expected = @"
<p>foo</p>
<pre><code>bar
</code></pre>
<p>baz</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example141()
	{
		var input = @"
foo
---
~~~
bar
~~~
# baz
";
		var expected = @"
<h2>foo</h2>
<pre><code>bar
</code></pre>
<h1>baz</h1>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example142()
	{
		var input = @"
```ruby
def foo(x)
  return 3
end
```
";
		var expected = @"
<pre><code class=""language-ruby"">def foo(x)
  return 3
end
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example143()
	{
		var input = @"
~~~~    ruby startline=3 $%@#$
def foo(x)
  return 3
end
~~~~~~~
";
		var expected = @"
<pre><code class=""language-ruby"">def foo(x)
  return 3
end
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example144()
	{
		var input = @"
```;
````
";
		var expected = @"
<pre><code class=""language-;""></code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example145()
	{
		var input = @"
``` aa ```
foo
";
		var expected = @"
<p><code>aa</code>
foo</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example146()
	{
		var input = @"
~~~ aa ``` ~~~
foo
~~~
";
		var expected = @"
<pre><code class=""language-aa"">foo
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example147()
	{
		var input = @"
```
``` aaa
```
";
		var expected = @"
<pre><code>``` aaa
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example148()
	{
		var input = @"
<table><tr><td>
<pre>
**Hello**,

_world_.
</pre>
</td></tr></table>
";
		var expected = @"
<table><tr><td>
<pre>
**Hello**,
<p><em>world</em>.
</pre></p>
</td></tr></table>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example149()
	{
		var input = @"
<table>
  <tr>
    <td>
           hi
    </td>
  </tr>
</table>

okay.
";
		var expected = @"
<table>
  <tr>
    <td>
           hi
    </td>
  </tr>
</table>
<p>okay.</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example150()
	{
		var input = @"
 <div>
  *hello*
         <foo><a>
";
		var expected = @"
 <div>
  *hello*
         <foo><a>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example151()
	{
		var input = @"
</div>
*foo*
";
		var expected = @"
</div>
*foo*
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example152()
	{
		var input = @"
<DIV CLASS=""foo"">

*Markdown*

</DIV>
";
		var expected = @"
<DIV CLASS=""foo"">
<p><em>Markdown</em></p>
</DIV>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example153()
	{
		var input = @"
<div id=""foo""
  class=""bar"">
</div>
";
		var expected = @"
<div id=""foo""
  class=""bar"">
</div>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example154()
	{
		var input = @"
<div id=""foo"" class=""bar
  baz"">
</div>
";
		var expected = @"
<div id=""foo"" class=""bar
  baz"">
</div>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example155()
	{
		var input = @"
<div>
*foo*

*bar*
";
		var expected = @"
<div>
*foo*
<p><em>bar</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example156()
	{
		var input = @"
<div id=""foo""
*hi*
";
		var expected = @"
<div id=""foo""
*hi*
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example157()
	{
		var input = @"
<div class
foo
";
		var expected = @"
<div class
foo
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example158()
	{
		var input = @"
<div *???-&&&-<---
*foo*
";
		var expected = @"
<div *???-&&&-<---
*foo*
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example159()
	{
		var input = @"
<div><a href=""bar"">*foo*</a></div>
";
		var expected = @"
<div><a href=""bar"">*foo*</a></div>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example160()
	{
		var input = @"
<table><tr><td>
foo
</td></tr></table>
";
		var expected = @"
<table><tr><td>
foo
</td></tr></table>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example161()
	{
		var input = @"
<div></div>
``` c
int x = 33;
```
";
		var expected = @"
<div></div>
``` c
int x = 33;
```
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example162()
	{
		var input = @"
<a href=""foo"">
*bar*
</a>
";
		var expected = @"
<a href=""foo"">
*bar*
</a>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example163()
	{
		var input = @"
<Warning>
*bar*
</Warning>
";
		var expected = @"
<Warning>
*bar*
</Warning>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example164()
	{
		var input = @"
<i class=""foo"">
*bar*
</i>
";
		var expected = @"
<i class=""foo"">
*bar*
</i>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example165()
	{
		var input = @"
</ins>
*bar*
";
		var expected = @"
</ins>
*bar*
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example166()
	{
		var input = @"
<del>
*foo*
</del>
";
		var expected = @"
<del>
*foo*
</del>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example167()
	{
		var input = @"
<del>

*foo*

</del>
";
		var expected = @"
<del>
<p><em>foo</em></p>
</del>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example168()
	{
		var input = @"
<del>*foo*</del>
";
		var expected = @"
<p><del><em>foo</em></del></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example169()
	{
		var input = @"
<pre language=""haskell""><code>
import Text.HTML.TagSoup

main :: IO ()
main = print $ parseTags tags
</code></pre>
okay
";
		var expected = @"
<pre language=""haskell""><code>
import Text.HTML.TagSoup

main :: IO ()
main = print $ parseTags tags
</code></pre>
<p>okay</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example170()
	{
		var input = @"
<script type=""text/javascript"">
// JavaScript example

document.getElementById(""demo"").innerHTML = ""Hello JavaScript!"";
</script>
okay
";
		var expected = @"
<script type=""text/javascript"">
// JavaScript example

document.getElementById(""demo"").innerHTML = ""Hello JavaScript!"";
</script>
<p>okay</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example171()
	{
		var input = @"
<textarea>

*foo*

_bar_

</textarea>
";
		var expected = @"
<textarea>

*foo*

_bar_

</textarea>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example172()
	{
		var input = @"
<style
  type=""text/css"">
h1 {color:red;}

p {color:blue;}
</style>
okay
";
		var expected = @"
<style
  type=""text/css"">
h1 {color:red;}

p {color:blue;}
</style>
<p>okay</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example173()
	{
		var input = @"
<style
  type=""text/css"">

foo
";
		var expected = @"
<style
  type=""text/css"">

foo
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example174()
	{
		var input = @"
> <div>
> foo

bar
";
		var expected = @"
<blockquote>
<div>
foo
</blockquote>
<p>bar</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example175()
	{
		var input = @"
- <div>
- foo
";
		var expected = @"
<ul>
<li>
<div>
</li>
<li>foo</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example176()
	{
		var input = @"
<style>p{color:red;}</style>
*foo*
";
		var expected = @"
<style>p{color:red;}</style>
<p><em>foo</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example177()
	{
		var input = @"
<!-- foo -->*bar*
*baz*
";
		var expected = @"
<!-- foo -->*bar*
<p><em>baz</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example178()
	{
		var input = @"
<script>
foo
</script>1. *bar*
";
		var expected = @"
<script>
foo
</script>1. *bar*
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example179()
	{
		var input = @"
<!-- Foo

bar
   baz -->
okay
";
		var expected = @"
<!-- Foo

bar
   baz -->
<p>okay</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example180()
	{
		var input = @"
<?php

  echo '>';

?>
okay
";
		var expected = @"
<?php

  echo '>';

?>
<p>okay</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example181()
	{
		var input = @"
<!DOCTYPE html>
";
		var expected = @"
<!DOCTYPE html>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example182()
	{
		var input = @"
<![CDATA[
function matchwo(a,b)
{
  if (a < b && a < 0) then {
    return 1;

  } else {

    return 0;
  }
}
]]>
okay
";
		var expected = @"
<![CDATA[
function matchwo(a,b)
{
  if (a < b && a < 0) then {
    return 1;

  } else {

    return 0;
  }
}
]]>
<p>okay</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example183()
	{
		var input = @"
  <!-- foo -->

    <!-- foo -->
";
		var expected = @"
  <!-- foo -->
<pre><code>&lt;!-- foo --&gt;
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example184()
	{
		var input = @"
  <div>

    <div>
";
		var expected = @"
  <div>
<pre><code>&lt;div&gt;
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example185()
	{
		var input = @"
Foo
<div>
bar
</div>
";
		var expected = @"
<p>Foo</p>
<div>
bar
</div>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example186()
	{
		var input = @"
<div>
bar
</div>
*foo*
";
		var expected = @"
<div>
bar
</div>
*foo*
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example187()
	{
		var input = @"
Foo
<a href=""bar"">
baz
";
		var expected = @"
<p>Foo
<a href=""bar"">
baz</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example188()
	{
		var input = @"
<div>

*Emphasized* text.

</div>
";
		var expected = @"
<div>
<p><em>Emphasized</em> text.</p>
</div>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example189()
	{
		var input = @"
<div>
*Emphasized* text.
</div>
";
		var expected = @"
<div>
*Emphasized* text.
</div>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example190()
	{
		var input = @"
<table>

<tr>

<td>
Hi
</td>

</tr>

</table>
";
		var expected = @"
<table>
<tr>
<td>
Hi
</td>
</tr>
</table>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example191()
	{
		var input = @"
<table>

  <tr>

    <td>
      Hi
    </td>

  </tr>

</table>
";
		var expected = @"
<table>
  <tr>
<pre><code>&lt;td&gt;
  Hi
&lt;/td&gt;
</code></pre>
  </tr>
</table>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example192()
	{
		var input = @"
[foo]: /url ""title""

[foo]
";
		var expected = @"
<p><a href=""/url"" title=""title"">foo</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example193()
	{
		var input = @"
   [foo]: 
      /url  
           'the title'  

[foo]
";
		var expected = @"
<p><a href=""/url"" title=""the title"">foo</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example194()
	{
		var input = @"
[Foo*bar\]]:my_(url) 'title (with parens)'

[Foo*bar\]]
";
		var expected = @"
<p><a href=""my_(url)"" title=""title (with parens)"">Foo*bar]</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example195()
	{
		var input = @"
[Foo bar]:
<my url>
'title'

[Foo bar]
";
		var expected = @"
<p><a href=""my%20url"" title=""title"">Foo bar</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example196()
	{
		var input = @"
[foo]: /url '
title
line1
line2
'

[foo]
";
		var expected = @"
<p><a href=""/url"" title=""
title
line1
line2
"">foo</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example197()
	{
		var input = @"
[foo]: /url 'title

with blank line'

[foo]
";
		var expected = @"
<p>[foo]: /url 'title</p>
<p>with blank line'</p>
<p>[foo]</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example198()
	{
		var input = @"
[foo]:
/url

[foo]
";
		var expected = @"
<p><a href=""/url"">foo</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example199()
	{
		var input = @"
[foo]:

[foo]
";
		var expected = @"
<p>[foo]:</p>
<p>[foo]</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example200()
	{
		var input = @"
[foo]: <>

[foo]
";
		var expected = @"
<p><a href="""">foo</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example201()
	{
		var input = @"
[foo]: <bar>(baz)

[foo]
";
		var expected = @"
<p>[foo]: <bar>(baz)</p>
<p>[foo]</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example202()
	{
		var input = @"
[foo]: /url\bar\*baz ""foo\""bar\baz""

[foo]
";
		var expected = @"
<p><a href=""/url%5Cbar*baz"" title=""foo&quot;bar\baz"">foo</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example203()
	{
		var input = @"
[foo]

[foo]: url
";
		var expected = @"
<p><a href=""url"">foo</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example204()
	{
		var input = @"
[foo]

[foo]: first
[foo]: second
";
		var expected = @"
<p><a href=""first"">foo</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example205()
	{
		var input = @"
[FOO]: /url

[Foo]
";
		var expected = @"
<p><a href=""/url"">Foo</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example206()
	{
		var input = @"
[ΑΓΩ]: /φου

[αγω]
";
		var expected = @"
<p><a href=""/%CF%86%CE%BF%CF%85"">αγω</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example207()
	{
		var input = @"
[foo]: /url
";
		var expected = @"

";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example208()
	{
		var input = @"
[
foo
]: /url
bar
";
		var expected = @"
<p>bar</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example209()
	{
		var input = @"
[foo]: /url ""title"" ok
";
		var expected = @"
<p>[foo]: /url &quot;title&quot; ok</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example210()
	{
		var input = @"
[foo]: /url
""title"" ok
";
		var expected = @"
<p>&quot;title&quot; ok</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example211()
	{
		var input = @"
    [foo]: /url ""title""

[foo]
";
		var expected = @"
<pre><code>[foo]: /url &quot;title&quot;
</code></pre>
<p>[foo]</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example212()
	{
		var input = @"
```
[foo]: /url
```

[foo]
";
		var expected = @"
<pre><code>[foo]: /url
</code></pre>
<p>[foo]</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example213()
	{
		var input = @"
Foo
[bar]: /baz

[bar]
";
		var expected = @"
<p>Foo
[bar]: /baz</p>
<p>[bar]</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example214()
	{
		var input = @"
# [Foo]
[foo]: /url
> bar
";
		var expected = @"
<h1><a href=""/url"">Foo</a></h1>
<blockquote>
<p>bar</p>
</blockquote>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example215()
	{
		var input = @"
[foo]: /url
bar
===
[foo]
";
		var expected = @"
<h1>bar</h1>
<p><a href=""/url"">foo</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example216()
	{
		var input = @"
[foo]: /url
===
[foo]
";
		var expected = @"
<p>===
<a href=""/url"">foo</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example217()
	{
		var input = @"
[foo]: /foo-url ""foo""
[bar]: /bar-url
  ""bar""
[baz]: /baz-url

[foo],
[bar],
[baz]
";
		var expected = @"
<p><a href=""/foo-url"" title=""foo"">foo</a>,
<a href=""/bar-url"" title=""bar"">bar</a>,
<a href=""/baz-url"">baz</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example218()
	{
		var input = @"
[foo]

> [foo]: /url
";
		var expected = @"
<p><a href=""/url"">foo</a></p>
<blockquote>
</blockquote>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example219()
	{
		var input = @"
aaa

bbb
";
		var expected = @"
<p>aaa</p>
<p>bbb</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example220()
	{
		var input = @"
aaa
bbb

ccc
ddd
";
		var expected = @"
<p>aaa
bbb</p>
<p>ccc
ddd</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example221()
	{
		var input = @"
aaa


bbb
";
		var expected = @"
<p>aaa</p>
<p>bbb</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example222()
	{
		var input = @"
  aaa
 bbb
";
		var expected = @"
<p>aaa
bbb</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example223()
	{
		var input = @"
aaa
             bbb
                                       ccc
";
		var expected = @"
<p>aaa
bbb
ccc</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example224()
	{
		var input = @"
   aaa
bbb
";
		var expected = @"
<p>aaa
bbb</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example225()
	{
		var input = @"
    aaa
bbb
";
		var expected = @"
<pre><code>aaa
</code></pre>
<p>bbb</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example226()
	{
		var input = @"
aaa     
bbb     
";
		var expected = @"
<p>aaa<br />
bbb</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example227()
	{
		var input = @"
  

aaa
  

# aaa

  
";
		var expected = @"
<p>aaa</p>
<h1>aaa</h1>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example228()
	{
		var input = @"
> # Foo
> bar
> baz
";
		var expected = @"
<blockquote>
<h1>Foo</h1>
<p>bar
baz</p>
</blockquote>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example229()
	{
		var input = @"
># Foo
>bar
> baz
";
		var expected = @"
<blockquote>
<h1>Foo</h1>
<p>bar
baz</p>
</blockquote>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example230()
	{
		var input = @"
   > # Foo
   > bar
 > baz
";
		var expected = @"
<blockquote>
<h1>Foo</h1>
<p>bar
baz</p>
</blockquote>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example231()
	{
		var input = @"
    > # Foo
    > bar
    > baz
";
		var expected = @"
<pre><code>&gt; # Foo
&gt; bar
&gt; baz
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example232()
	{
		var input = @"
> # Foo
> bar
baz
";
		var expected = @"
<blockquote>
<h1>Foo</h1>
<p>bar
baz</p>
</blockquote>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example233()
	{
		var input = @"
> bar
baz
> foo
";
		var expected = @"
<blockquote>
<p>bar
baz
foo</p>
</blockquote>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example234()
	{
		var input = @"
> foo
---
";
		var expected = @"
<blockquote>
<p>foo</p>
</blockquote>
<hr />
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example235()
	{
		var input = @"
> - foo
- bar
";
		var expected = @"
<blockquote>
<ul>
<li>foo</li>
</ul>
</blockquote>
<ul>
<li>bar</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example236()
	{
		var input = @"
>     foo
    bar
";
		var expected = @"
<blockquote>
<pre><code>foo
</code></pre>
</blockquote>
<pre><code>bar
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example237()
	{
		var input = @"
> ```
foo
```
";
		var expected = @"
<blockquote>
<pre><code></code></pre>
</blockquote>
<p>foo</p>
<pre><code></code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example238()
	{
		var input = @"
> foo
    - bar
";
		var expected = @"
<blockquote>
<p>foo
- bar</p>
</blockquote>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example239()
	{
		var input = @"
>
";
		var expected = @"
<blockquote>
</blockquote>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example240()
	{
		var input = @"
>
>  
> 
";
		var expected = @"
<blockquote>
</blockquote>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example241()
	{
		var input = @"
>
> foo
>  
";
		var expected = @"
<blockquote>
<p>foo</p>
</blockquote>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example242()
	{
		var input = @"
> foo

> bar
";
		var expected = @"
<blockquote>
<p>foo</p>
</blockquote>
<blockquote>
<p>bar</p>
</blockquote>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example243()
	{
		var input = @"
> foo
> bar
";
		var expected = @"
<blockquote>
<p>foo
bar</p>
</blockquote>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example244()
	{
		var input = @"
> foo
>
> bar
";
		var expected = @"
<blockquote>
<p>foo</p>
<p>bar</p>
</blockquote>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example245()
	{
		var input = @"
foo
> bar
";
		var expected = @"
<p>foo</p>
<blockquote>
<p>bar</p>
</blockquote>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example246()
	{
		var input = @"
> aaa
***
> bbb
";
		var expected = @"
<blockquote>
<p>aaa</p>
</blockquote>
<hr />
<blockquote>
<p>bbb</p>
</blockquote>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example247()
	{
		var input = @"
> bar
baz
";
		var expected = @"
<blockquote>
<p>bar
baz</p>
</blockquote>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example248()
	{
		var input = @"
> bar

baz
";
		var expected = @"
<blockquote>
<p>bar</p>
</blockquote>
<p>baz</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example249()
	{
		var input = @"
> bar
>
baz
";
		var expected = @"
<blockquote>
<p>bar</p>
</blockquote>
<p>baz</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example250()
	{
		var input = @"
> > > foo
bar
";
		var expected = @"
<blockquote>
<blockquote>
<blockquote>
<p>foo
bar</p>
</blockquote>
</blockquote>
</blockquote>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example251()
	{
		var input = @"
>>> foo
> bar
>>baz
";
		var expected = @"
<blockquote>
<blockquote>
<blockquote>
<p>foo
bar
baz</p>
</blockquote>
</blockquote>
</blockquote>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example252()
	{
		var input = @"
>     code

>    not code
";
		var expected = @"
<blockquote>
<pre><code>code
</code></pre>
</blockquote>
<blockquote>
<p>not code</p>
</blockquote>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example253()
	{
		var input = @"
A paragraph
with two lines.

    indented code

> A block quote.
";
		var expected = @"
<p>A paragraph
with two lines.</p>
<pre><code>indented code
</code></pre>
<blockquote>
<p>A block quote.</p>
</blockquote>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example254()
	{
		var input = @"
1.  A paragraph
    with two lines.

        indented code

    > A block quote.
";
		var expected = @"
<ol>
<li>
<p>A paragraph
with two lines.</p>
<pre><code>indented code
</code></pre>
<blockquote>
<p>A block quote.</p>
</blockquote>
</li>
</ol>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example255()
	{
		var input = @"
- one

 two
";
		var expected = @"
<ul>
<li>one</li>
</ul>
<p>two</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example256()
	{
		var input = @"
- one

  two
";
		var expected = @"
<ul>
<li>
<p>one</p>
<p>two</p>
</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example257()
	{
		var input = @"
 -    one

     two
";
		var expected = @"
<ul>
<li>one</li>
</ul>
<pre><code> two
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example258()
	{
		var input = @"
 -    one

      two
";
		var expected = @"
<ul>
<li>
<p>one</p>
<p>two</p>
</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example259()
	{
		var input = @"
   > > 1.  one
>>
>>     two
";
		var expected = @"
<blockquote>
<blockquote>
<ol>
<li>
<p>one</p>
<p>two</p>
</li>
</ol>
</blockquote>
</blockquote>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example260()
	{
		var input = @"
>>- one
>>
  >  > two
";
		var expected = @"
<blockquote>
<blockquote>
<ul>
<li>one</li>
</ul>
<p>two</p>
</blockquote>
</blockquote>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example261()
	{
		var input = @"
-one

2.two
";
		var expected = @"
<p>-one</p>
<p>2.two</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example262()
	{
		var input = @"
- foo


  bar
";
		var expected = @"
<ul>
<li>
<p>foo</p>
<p>bar</p>
</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example263()
	{
		var input = @"
1.  foo

    ```
    bar
    ```

    baz

    > bam
";
		var expected = @"
<ol>
<li>
<p>foo</p>
<pre><code>bar
</code></pre>
<p>baz</p>
<blockquote>
<p>bam</p>
</blockquote>
</li>
</ol>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example264()
	{
		var input = @"
- Foo

      bar


      baz
";
		var expected = @"
<ul>
<li>
<p>Foo</p>
<pre><code>bar


baz
</code></pre>
</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example265()
	{
		var input = @"
123456789. ok
";
		var expected = @"
<ol start=""123456789"">
<li>ok</li>
</ol>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example266()
	{
		var input = @"
1234567890. not ok
";
		var expected = @"
<p>1234567890. not ok</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example267()
	{
		var input = @"
0. ok
";
		var expected = @"
<ol start=""0"">
<li>ok</li>
</ol>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example268()
	{
		var input = @"
003. ok
";
		var expected = @"
<ol start=""3"">
<li>ok</li>
</ol>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example269()
	{
		var input = @"
-1. not ok
";
		var expected = @"
<p>-1. not ok</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example270()
	{
		var input = @"
- foo

      bar
";
		var expected = @"
<ul>
<li>
<p>foo</p>
<pre><code>bar
</code></pre>
</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example271()
	{
		var input = @"
  10.  foo

           bar
";
		var expected = @"
<ol start=""10"">
<li>
<p>foo</p>
<pre><code>bar
</code></pre>
</li>
</ol>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example272()
	{
		var input = @"
    indented code

paragraph

    more code
";
		var expected = @"
<pre><code>indented code
</code></pre>
<p>paragraph</p>
<pre><code>more code
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example273()
	{
		var input = @"
1.     indented code

   paragraph

       more code
";
		var expected = @"
<ol>
<li>
<pre><code>indented code
</code></pre>
<p>paragraph</p>
<pre><code>more code
</code></pre>
</li>
</ol>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example274()
	{
		var input = @"
1.      indented code

   paragraph

       more code
";
		var expected = @"
<ol>
<li>
<pre><code> indented code
</code></pre>
<p>paragraph</p>
<pre><code>more code
</code></pre>
</li>
</ol>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example275()
	{
		var input = @"
   foo

bar
";
		var expected = @"
<p>foo</p>
<p>bar</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example276()
	{
		var input = @"
-    foo

  bar
";
		var expected = @"
<ul>
<li>foo</li>
</ul>
<p>bar</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example277()
	{
		var input = @"
-  foo

   bar
";
		var expected = @"
<ul>
<li>
<p>foo</p>
<p>bar</p>
</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example278()
	{
		var input = @"
-
  foo
-
  ```
  bar
  ```
-
      baz
";
		var expected = @"
<ul>
<li>foo</li>
<li>
<pre><code>bar
</code></pre>
</li>
<li>
<pre><code>baz
</code></pre>
</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example279()
	{
		var input = @"
-   
  foo
";
		var expected = @"
<ul>
<li>foo</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example280()
	{
		var input = @"
-

  foo
";
		var expected = @"
<ul>
<li></li>
</ul>
<p>foo</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example281()
	{
		var input = @"
- foo
-
- bar
";
		var expected = @"
<ul>
<li>foo</li>
<li></li>
<li>bar</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example282()
	{
		var input = @"
- foo
-   
- bar
";
		var expected = @"
<ul>
<li>foo</li>
<li></li>
<li>bar</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example283()
	{
		var input = @"
1. foo
2.
3. bar
";
		var expected = @"
<ol>
<li>foo</li>
<li></li>
<li>bar</li>
</ol>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example284()
	{
		var input = @"
*
";
		var expected = @"
<ul>
<li></li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example285()
	{
		var input = @"
foo
*

foo
1.
";
		var expected = @"
<p>foo
*</p>
<p>foo
1.</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example286()
	{
		var input = @"
 1.  A paragraph
     with two lines.

         indented code

     > A block quote.
";
		var expected = @"
<ol>
<li>
<p>A paragraph
with two lines.</p>
<pre><code>indented code
</code></pre>
<blockquote>
<p>A block quote.</p>
</blockquote>
</li>
</ol>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example287()
	{
		var input = @"
  1.  A paragraph
      with two lines.

          indented code

      > A block quote.
";
		var expected = @"
<ol>
<li>
<p>A paragraph
with two lines.</p>
<pre><code>indented code
</code></pre>
<blockquote>
<p>A block quote.</p>
</blockquote>
</li>
</ol>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example288()
	{
		var input = @"
   1.  A paragraph
       with two lines.

           indented code

       > A block quote.
";
		var expected = @"
<ol>
<li>
<p>A paragraph
with two lines.</p>
<pre><code>indented code
</code></pre>
<blockquote>
<p>A block quote.</p>
</blockquote>
</li>
</ol>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example289()
	{
		var input = @"
    1.  A paragraph
        with two lines.

            indented code

        > A block quote.
";
		var expected = @"
<pre><code>1.  A paragraph
    with two lines.

        indented code

    &gt; A block quote.
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example290()
	{
		var input = @"
  1.  A paragraph
with two lines.

          indented code

      > A block quote.
";
		var expected = @"
<ol>
<li>
<p>A paragraph
with two lines.</p>
<pre><code>indented code
</code></pre>
<blockquote>
<p>A block quote.</p>
</blockquote>
</li>
</ol>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example291()
	{
		var input = @"
  1.  A paragraph
    with two lines.
";
		var expected = @"
<ol>
<li>A paragraph
with two lines.</li>
</ol>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example292()
	{
		var input = @"
> 1. > Blockquote
continued here.
";
		var expected = @"
<blockquote>
<ol>
<li>
<blockquote>
<p>Blockquote
continued here.</p>
</blockquote>
</li>
</ol>
</blockquote>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example293()
	{
		var input = @"
> 1. > Blockquote
> continued here.
";
		var expected = @"
<blockquote>
<ol>
<li>
<blockquote>
<p>Blockquote
continued here.</p>
</blockquote>
</li>
</ol>
</blockquote>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example294()
	{
		var input = @"
- foo
  - bar
    - baz
      - boo
";
		var expected = @"
<ul>
<li>foo
<ul>
<li>bar
<ul>
<li>baz
<ul>
<li>boo</li>
</ul>
</li>
</ul>
</li>
</ul>
</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example295()
	{
		var input = @"
- foo
 - bar
  - baz
   - boo
";
		var expected = @"
<ul>
<li>foo</li>
<li>bar</li>
<li>baz</li>
<li>boo</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example296()
	{
		var input = @"
10) foo
    - bar
";
		var expected = @"
<ol start=""10"">
<li>foo
<ul>
<li>bar</li>
</ul>
</li>
</ol>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example297()
	{
		var input = @"
10) foo
   - bar
";
		var expected = @"
<ol start=""10"">
<li>foo</li>
</ol>
<ul>
<li>bar</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example298()
	{
		var input = @"
- - foo
";
		var expected = @"
<ul>
<li>
<ul>
<li>foo</li>
</ul>
</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example299()
	{
		var input = @"
1. - 2. foo
";
		var expected = @"
<ol>
<li>
<ul>
<li>
<ol start=""2"">
<li>foo</li>
</ol>
</li>
</ul>
</li>
</ol>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example300()
	{
		var input = @"
- # Foo
- Bar
  ---
  baz
";
		var expected = @"
<ul>
<li>
<h1>Foo</h1>
</li>
<li>
<h2>Bar</h2>
baz</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example301()
	{
		var input = @"
- foo
- bar
+ baz
";
		var expected = @"
<ul>
<li>foo</li>
<li>bar</li>
</ul>
<ul>
<li>baz</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example302()
	{
		var input = @"
1. foo
2. bar
3) baz
";
		var expected = @"
<ol>
<li>foo</li>
<li>bar</li>
</ol>
<ol start=""3"">
<li>baz</li>
</ol>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example303()
	{
		var input = @"
Foo
- bar
- baz
";
		var expected = @"
<p>Foo</p>
<ul>
<li>bar</li>
<li>baz</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example304()
	{
		var input = @"
The number of windows in my house is
14.  The number of doors is 6.
";
		var expected = @"
<p>The number of windows in my house is
14.  The number of doors is 6.</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example305()
	{
		var input = @"
The number of windows in my house is
1.  The number of doors is 6.
";
		var expected = @"
<p>The number of windows in my house is</p>
<ol>
<li>The number of doors is 6.</li>
</ol>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example306()
	{
		var input = @"
- foo

- bar


- baz
";
		var expected = @"
<ul>
<li>
<p>foo</p>
</li>
<li>
<p>bar</p>
</li>
<li>
<p>baz</p>
</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example307()
	{
		var input = @"
- foo
  - bar
    - baz


      bim
";
		var expected = @"
<ul>
<li>foo
<ul>
<li>bar
<ul>
<li>
<p>baz</p>
<p>bim</p>
</li>
</ul>
</li>
</ul>
</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example308()
	{
		var input = @"
- foo
- bar

<!-- -->

- baz
- bim
";
		var expected = @"
<ul>
<li>foo</li>
<li>bar</li>
</ul>
<!-- -->
<ul>
<li>baz</li>
<li>bim</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example309()
	{
		var input = @"
-   foo

    notcode

-   foo

<!-- -->

    code
";
		var expected = @"
<ul>
<li>
<p>foo</p>
<p>notcode</p>
</li>
<li>
<p>foo</p>
</li>
</ul>
<!-- -->
<pre><code>code
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example310()
	{
		var input = @"
- a
 - b
  - c
   - d
  - e
 - f
- g
";
		var expected = @"
<ul>
<li>a</li>
<li>b</li>
<li>c</li>
<li>d</li>
<li>e</li>
<li>f</li>
<li>g</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example311()
	{
		var input = @"
1. a

  2. b

   3. c
";
		var expected = @"
<ol>
<li>
<p>a</p>
</li>
<li>
<p>b</p>
</li>
<li>
<p>c</p>
</li>
</ol>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example312()
	{
		var input = @"
- a
 - b
  - c
   - d
    - e
";
		var expected = @"
<ul>
<li>a</li>
<li>b</li>
<li>c</li>
<li>d
- e</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example313()
	{
		var input = @"
1. a

  2. b

    3. c
";
		var expected = @"
<ol>
<li>
<p>a</p>
</li>
<li>
<p>b</p>
</li>
</ol>
<pre><code>3. c
</code></pre>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example314()
	{
		var input = @"
- a
- b

- c
";
		var expected = @"
<ul>
<li>
<p>a</p>
</li>
<li>
<p>b</p>
</li>
<li>
<p>c</p>
</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example315()
	{
		var input = @"
* a
*

* c
";
		var expected = @"
<ul>
<li>
<p>a</p>
</li>
<li></li>
<li>
<p>c</p>
</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example316()
	{
		var input = @"
- a
- b

  c
- d
";
		var expected = @"
<ul>
<li>
<p>a</p>
</li>
<li>
<p>b</p>
<p>c</p>
</li>
<li>
<p>d</p>
</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example317()
	{
		var input = @"
- a
- b

  [ref]: /url
- d
";
		var expected = @"
<ul>
<li>
<p>a</p>
</li>
<li>
<p>b</p>
</li>
<li>
<p>d</p>
</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example318()
	{
		var input = @"
- a
- ```
  b


  ```
- c
";
		var expected = @"
<ul>
<li>a</li>
<li>
<pre><code>b


</code></pre>
</li>
<li>c</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example319()
	{
		var input = @"
- a
  - b

    c
- d
";
		var expected = @"
<ul>
<li>a
<ul>
<li>
<p>b</p>
<p>c</p>
</li>
</ul>
</li>
<li>d</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example320()
	{
		var input = @"
* a
  > b
  >
* c
";
		var expected = @"
<ul>
<li>a
<blockquote>
<p>b</p>
</blockquote>
</li>
<li>c</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example321()
	{
		var input = @"
- a
  > b
  ```
  c
  ```
- d
";
		var expected = @"
<ul>
<li>a
<blockquote>
<p>b</p>
</blockquote>
<pre><code>c
</code></pre>
</li>
<li>d</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example322()
	{
		var input = @"
- a
";
		var expected = @"
<ul>
<li>a</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example323()
	{
		var input = @"
- a
  - b
";
		var expected = @"
<ul>
<li>a
<ul>
<li>b</li>
</ul>
</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example324()
	{
		var input = @"
1. ```
   foo
   ```

   bar
";
		var expected = @"
<ol>
<li>
<pre><code>foo
</code></pre>
<p>bar</p>
</li>
</ol>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example325()
	{
		var input = @"
* foo
  * bar

  baz
";
		var expected = @"
<ul>
<li>
<p>foo</p>
<ul>
<li>bar</li>
</ul>
<p>baz</p>
</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example326()
	{
		var input = @"
- a
  - b
  - c

- d
  - e
  - f
";
		var expected = @"
<ul>
<li>
<p>a</p>
<ul>
<li>b</li>
<li>c</li>
</ul>
</li>
<li>
<p>d</p>
<ul>
<li>e</li>
<li>f</li>
</ul>
</li>
</ul>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example327()
	{
		var input = @"
`hi`lo`
";
		var expected = @"
<p><code>hi</code>lo`</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example328()
	{
		var input = @"
`foo`
";
		var expected = @"
<p><code>foo</code></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example329()
	{
		var input = @"
`` foo ` bar ``
";
		var expected = @"
<p><code>foo ` bar</code></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example330()
	{
		var input = @"
` `` `
";
		var expected = @"
<p><code>``</code></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example331()
	{
		var input = @"
`  ``  `
";
		var expected = @"
<p><code> `` </code></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example332()
	{
		var input = @"
` a`
";
		var expected = @"
<p><code> a</code></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example333()
	{
		var input = @"
` b `
";
		var expected = @"
<p><code> b </code></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example334()
	{
		var input = @"
` `
`  `
";
		var expected = @"
<p><code> </code>
<code>  </code></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example335()
	{
		var input = @"
``
foo
bar  
baz
``
";
		var expected = @"
<p><code>foo bar   baz</code></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example336()
	{
		var input = @"
``
foo 
``
";
		var expected = @"
<p><code>foo </code></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example337()
	{
		var input = @"
`foo   bar 
baz`
";
		var expected = @"
<p><code>foo   bar  baz</code></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example338()
	{
		var input = @"
`foo\`bar`
";
		var expected = @"
<p><code>foo\</code>bar`</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example339()
	{
		var input = @"
``foo`bar``
";
		var expected = @"
<p><code>foo`bar</code></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example340()
	{
		var input = @"
` foo `` bar `
";
		var expected = @"
<p><code>foo `` bar</code></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example341()
	{
		var input = @"
*foo`*`
";
		var expected = @"
<p>*foo<code>*</code></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example342()
	{
		var input = @"
[not a `link](/foo`)
";
		var expected = @"
<p>[not a <code>link](/foo</code>)</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example343()
	{
		var input = @"
`<a href=""`"">`
";
		var expected = @"
<p><code>&lt;a href=&quot;</code>&quot;&gt;`</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example344()
	{
		var input = @"
<a href=""`"">`
";
		var expected = @"
<p><a href=""`"">`</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example345()
	{
		var input = @"
`<https://foo.bar.`baz>`
";
		var expected = @"
<p><code>&lt;https://foo.bar.</code>baz&gt;`</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example346()
	{
		var input = @"
<https://foo.bar.`baz>`
";
		var expected = @"
<p><a href=""https://foo.bar.%60baz"">https://foo.bar.`baz</a>`</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example347()
	{
		var input = @"
```foo``
";
		var expected = @"
<p>```foo``</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example348()
	{
		var input = @"
`foo
";
		var expected = @"
<p>`foo</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example349()
	{
		var input = @"
`foo``bar``
";
		var expected = @"
<p>`foo<code>bar</code></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example350()
	{
		var input = @"
*foo bar*
";
		var expected = @"
<p><em>foo bar</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example351()
	{
		var input = @"
a * foo bar*
";
		var expected = @"
<p>a * foo bar*</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example352()
	{
		var input = @"
a*""foo""*
";
		var expected = @"
<p>a*&quot;foo&quot;*</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example353()
	{
		var input = @"
* a *
";
		var expected = @"
<p>* a *</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example354()
	{
		var input = @"
*$*alpha.

*£*bravo.

*€*charlie.
";
		var expected = @"
<p>*$*alpha.</p>
<p>*£*bravo.</p>
<p>*€*charlie.</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example355()
	{
		var input = @"
foo*bar*
";
		var expected = @"
<p>foo<em>bar</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example356()
	{
		var input = @"
5*6*78
";
		var expected = @"
<p>5<em>6</em>78</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example357()
	{
		var input = @"
_foo bar_
";
		var expected = @"
<p><em>foo bar</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example358()
	{
		var input = @"
_ foo bar_
";
		var expected = @"
<p>_ foo bar_</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example359()
	{
		var input = @"
a_""foo""_
";
		var expected = @"
<p>a_&quot;foo&quot;_</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example360()
	{
		var input = @"
foo_bar_
";
		var expected = @"
<p>foo_bar_</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example361()
	{
		var input = @"
5_6_78
";
		var expected = @"
<p>5_6_78</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example362()
	{
		var input = @"
пристаням_стремятся_
";
		var expected = @"
<p>пристаням_стремятся_</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example363()
	{
		var input = @"
aa_""bb""_cc
";
		var expected = @"
<p>aa_&quot;bb&quot;_cc</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example364()
	{
		var input = @"
foo-_(bar)_
";
		var expected = @"
<p>foo-<em>(bar)</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example365()
	{
		var input = @"
_foo*
";
		var expected = @"
<p>_foo*</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example366()
	{
		var input = @"
*foo bar *
";
		var expected = @"
<p>*foo bar *</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example367()
	{
		var input = @"
*foo bar
*
";
		var expected = @"
<p>*foo bar
*</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example368()
	{
		var input = @"
*(*foo)
";
		var expected = @"
<p>*(*foo)</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example369()
	{
		var input = @"
*(*foo*)*
";
		var expected = @"
<p><em>(<em>foo</em>)</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example370()
	{
		var input = @"
*foo*bar
";
		var expected = @"
<p><em>foo</em>bar</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example371()
	{
		var input = @"
_foo bar _
";
		var expected = @"
<p>_foo bar _</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example372()
	{
		var input = @"
_(_foo)
";
		var expected = @"
<p>_(_foo)</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example373()
	{
		var input = @"
_(_foo_)_
";
		var expected = @"
<p><em>(<em>foo</em>)</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example374()
	{
		var input = @"
_foo_bar
";
		var expected = @"
<p>_foo_bar</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example375()
	{
		var input = @"
_пристаням_стремятся
";
		var expected = @"
<p>_пристаням_стремятся</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example376()
	{
		var input = @"
_foo_bar_baz_
";
		var expected = @"
<p><em>foo_bar_baz</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example377()
	{
		var input = @"
_(bar)_.
";
		var expected = @"
<p><em>(bar)</em>.</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example378()
	{
		var input = @"
**foo bar**
";
		var expected = @"
<p><strong>foo bar</strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example379()
	{
		var input = @"
** foo bar**
";
		var expected = @"
<p>** foo bar**</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example380()
	{
		var input = @"
a**""foo""**
";
		var expected = @"
<p>a**&quot;foo&quot;**</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example381()
	{
		var input = @"
foo**bar**
";
		var expected = @"
<p>foo<strong>bar</strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example382()
	{
		var input = @"
__foo bar__
";
		var expected = @"
<p><strong>foo bar</strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example383()
	{
		var input = @"
__ foo bar__
";
		var expected = @"
<p>__ foo bar__</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example384()
	{
		var input = @"
__
foo bar__
";
		var expected = @"
<p>__
foo bar__</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example385()
	{
		var input = @"
a__""foo""__
";
		var expected = @"
<p>a__&quot;foo&quot;__</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example386()
	{
		var input = @"
foo__bar__
";
		var expected = @"
<p>foo__bar__</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example387()
	{
		var input = @"
5__6__78
";
		var expected = @"
<p>5__6__78</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example388()
	{
		var input = @"
пристаням__стремятся__
";
		var expected = @"
<p>пристаням__стремятся__</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example389()
	{
		var input = @"
__foo, __bar__, baz__
";
		var expected = @"
<p><strong>foo, <strong>bar</strong>, baz</strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example390()
	{
		var input = @"
foo-__(bar)__
";
		var expected = @"
<p>foo-<strong>(bar)</strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example391()
	{
		var input = @"
**foo bar **
";
		var expected = @"
<p>**foo bar **</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example392()
	{
		var input = @"
**(**foo)
";
		var expected = @"
<p>**(**foo)</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example393()
	{
		var input = @"
*(**foo**)*
";
		var expected = @"
<p><em>(<strong>foo</strong>)</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example394()
	{
		var input = @"
**Gomphocarpus (*Gomphocarpus physocarpus*, syn.
*Asclepias physocarpa*)**
";
		var expected = @"
<p><strong>Gomphocarpus (<em>Gomphocarpus physocarpus</em>, syn.
<em>Asclepias physocarpa</em>)</strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example395()
	{
		var input = @"
**foo ""*bar*"" foo**
";
		var expected = @"
<p><strong>foo &quot;<em>bar</em>&quot; foo</strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example396()
	{
		var input = @"
**foo**bar
";
		var expected = @"
<p><strong>foo</strong>bar</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example397()
	{
		var input = @"
__foo bar __
";
		var expected = @"
<p>__foo bar __</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example398()
	{
		var input = @"
__(__foo)
";
		var expected = @"
<p>__(__foo)</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example399()
	{
		var input = @"
_(__foo__)_
";
		var expected = @"
<p><em>(<strong>foo</strong>)</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example400()
	{
		var input = @"
__foo__bar
";
		var expected = @"
<p>__foo__bar</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example401()
	{
		var input = @"
__пристаням__стремятся
";
		var expected = @"
<p>__пристаням__стремятся</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example402()
	{
		var input = @"
__foo__bar__baz__
";
		var expected = @"
<p><strong>foo__bar__baz</strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example403()
	{
		var input = @"
__(bar)__.
";
		var expected = @"
<p><strong>(bar)</strong>.</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example404()
	{
		var input = @"
*foo [bar](/url)*
";
		var expected = @"
<p><em>foo <a href=""/url"">bar</a></em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example405()
	{
		var input = @"
*foo
bar*
";
		var expected = @"
<p><em>foo
bar</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example406()
	{
		var input = @"
_foo __bar__ baz_
";
		var expected = @"
<p><em>foo <strong>bar</strong> baz</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example407()
	{
		var input = @"
_foo _bar_ baz_
";
		var expected = @"
<p><em>foo <em>bar</em> baz</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example408()
	{
		var input = @"
__foo_ bar_
";
		var expected = @"
<p><em><em>foo</em> bar</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example409()
	{
		var input = @"
*foo *bar**
";
		var expected = @"
<p><em>foo <em>bar</em></em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example410()
	{
		var input = @"
*foo **bar** baz*
";
		var expected = @"
<p><em>foo <strong>bar</strong> baz</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example411()
	{
		var input = @"
*foo**bar**baz*
";
		var expected = @"
<p><em>foo<strong>bar</strong>baz</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example412()
	{
		var input = @"
*foo**bar*
";
		var expected = @"
<p><em>foo**bar</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example413()
	{
		var input = @"
***foo** bar*
";
		var expected = @"
<p><em><strong>foo</strong> bar</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example414()
	{
		var input = @"
*foo **bar***
";
		var expected = @"
<p><em>foo <strong>bar</strong></em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example415()
	{
		var input = @"
*foo**bar***
";
		var expected = @"
<p><em>foo<strong>bar</strong></em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example416()
	{
		var input = @"
foo***bar***baz
";
		var expected = @"
<p>foo<em><strong>bar</strong></em>baz</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example417()
	{
		var input = @"
foo******bar*********baz
";
		var expected = @"
<p>foo<strong><strong><strong>bar</strong></strong></strong>***baz</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example418()
	{
		var input = @"
*foo **bar *baz* bim** bop*
";
		var expected = @"
<p><em>foo <strong>bar <em>baz</em> bim</strong> bop</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example419()
	{
		var input = @"
*foo [*bar*](/url)*
";
		var expected = @"
<p><em>foo <a href=""/url""><em>bar</em></a></em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example420()
	{
		var input = @"
** is not an empty emphasis
";
		var expected = @"
<p>** is not an empty emphasis</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example421()
	{
		var input = @"
**** is not an empty strong emphasis
";
		var expected = @"
<p>**** is not an empty strong emphasis</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example422()
	{
		var input = @"
**foo [bar](/url)**
";
		var expected = @"
<p><strong>foo <a href=""/url"">bar</a></strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example423()
	{
		var input = @"
**foo
bar**
";
		var expected = @"
<p><strong>foo
bar</strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example424()
	{
		var input = @"
__foo _bar_ baz__
";
		var expected = @"
<p><strong>foo <em>bar</em> baz</strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example425()
	{
		var input = @"
__foo __bar__ baz__
";
		var expected = @"
<p><strong>foo <strong>bar</strong> baz</strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example426()
	{
		var input = @"
____foo__ bar__
";
		var expected = @"
<p><strong><strong>foo</strong> bar</strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example427()
	{
		var input = @"
**foo **bar****
";
		var expected = @"
<p><strong>foo <strong>bar</strong></strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example428()
	{
		var input = @"
**foo *bar* baz**
";
		var expected = @"
<p><strong>foo <em>bar</em> baz</strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example429()
	{
		var input = @"
**foo*bar*baz**
";
		var expected = @"
<p><strong>foo<em>bar</em>baz</strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example430()
	{
		var input = @"
***foo* bar**
";
		var expected = @"
<p><strong><em>foo</em> bar</strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example431()
	{
		var input = @"
**foo *bar***
";
		var expected = @"
<p><strong>foo <em>bar</em></strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example432()
	{
		var input = @"
**foo *bar **baz**
bim* bop**
";
		var expected = @"
<p><strong>foo <em>bar <strong>baz</strong>
bim</em> bop</strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example433()
	{
		var input = @"
**foo [*bar*](/url)**
";
		var expected = @"
<p><strong>foo <a href=""/url""><em>bar</em></a></strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example434()
	{
		var input = @"
__ is not an empty emphasis
";
		var expected = @"
<p>__ is not an empty emphasis</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example435()
	{
		var input = @"
____ is not an empty strong emphasis
";
		var expected = @"
<p>____ is not an empty strong emphasis</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example436()
	{
		var input = @"
foo ***
";
		var expected = @"
<p>foo ***</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example437()
	{
		var input = @"
foo *\**
";
		var expected = @"
<p>foo <em>*</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example438()
	{
		var input = @"
foo *_*
";
		var expected = @"
<p>foo <em>_</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example439()
	{
		var input = @"
foo *****
";
		var expected = @"
<p>foo *****</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example440()
	{
		var input = @"
foo **\***
";
		var expected = @"
<p>foo <strong>*</strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example441()
	{
		var input = @"
foo **_**
";
		var expected = @"
<p>foo <strong>_</strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example442()
	{
		var input = @"
**foo*
";
		var expected = @"
<p>*<em>foo</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example443()
	{
		var input = @"
*foo**
";
		var expected = @"
<p><em>foo</em>*</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example444()
	{
		var input = @"
***foo**
";
		var expected = @"
<p>*<strong>foo</strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example445()
	{
		var input = @"
****foo*
";
		var expected = @"
<p>***<em>foo</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example446()
	{
		var input = @"
**foo***
";
		var expected = @"
<p><strong>foo</strong>*</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example447()
	{
		var input = @"
*foo****
";
		var expected = @"
<p><em>foo</em>***</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example448()
	{
		var input = @"
foo ___
";
		var expected = @"
<p>foo ___</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example449()
	{
		var input = @"
foo _\__
";
		var expected = @"
<p>foo <em>_</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example450()
	{
		var input = @"
foo _*_
";
		var expected = @"
<p>foo <em>*</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example451()
	{
		var input = @"
foo _____
";
		var expected = @"
<p>foo _____</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example452()
	{
		var input = @"
foo __\___
";
		var expected = @"
<p>foo <strong>_</strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example453()
	{
		var input = @"
foo __*__
";
		var expected = @"
<p>foo <strong>*</strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example454()
	{
		var input = @"
__foo_
";
		var expected = @"
<p>_<em>foo</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example455()
	{
		var input = @"
_foo__
";
		var expected = @"
<p><em>foo</em>_</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example456()
	{
		var input = @"
___foo__
";
		var expected = @"
<p>_<strong>foo</strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example457()
	{
		var input = @"
____foo_
";
		var expected = @"
<p>___<em>foo</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example458()
	{
		var input = @"
__foo___
";
		var expected = @"
<p><strong>foo</strong>_</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example459()
	{
		var input = @"
_foo____
";
		var expected = @"
<p><em>foo</em>___</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example460()
	{
		var input = @"
**foo**
";
		var expected = @"
<p><strong>foo</strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example461()
	{
		var input = @"
*_foo_*
";
		var expected = @"
<p><em><em>foo</em></em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example462()
	{
		var input = @"
__foo__
";
		var expected = @"
<p><strong>foo</strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example463()
	{
		var input = @"
_*foo*_
";
		var expected = @"
<p><em><em>foo</em></em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example464()
	{
		var input = @"
****foo****
";
		var expected = @"
<p><strong><strong>foo</strong></strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example465()
	{
		var input = @"
____foo____
";
		var expected = @"
<p><strong><strong>foo</strong></strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example466()
	{
		var input = @"
******foo******
";
		var expected = @"
<p><strong><strong><strong>foo</strong></strong></strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example467()
	{
		var input = @"
***foo***
";
		var expected = @"
<p><em><strong>foo</strong></em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example468()
	{
		var input = @"
_____foo_____
";
		var expected = @"
<p><em><strong><strong>foo</strong></strong></em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example469()
	{
		var input = @"
*foo _bar* baz_
";
		var expected = @"
<p><em>foo _bar</em> baz_</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example470()
	{
		var input = @"
*foo __bar *baz bim__ bam*
";
		var expected = @"
<p><em>foo <strong>bar *baz bim</strong> bam</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example471()
	{
		var input = @"
**foo **bar baz**
";
		var expected = @"
<p>**foo <strong>bar baz</strong></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example472()
	{
		var input = @"
*foo *bar baz*
";
		var expected = @"
<p>*foo <em>bar baz</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example473()
	{
		var input = @"
*[bar*](/url)
";
		var expected = @"
<p>*<a href=""/url"">bar*</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example474()
	{
		var input = @"
_foo [bar_](/url)
";
		var expected = @"
<p>_foo <a href=""/url"">bar_</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example475()
	{
		var input = @"
*<img src=""foo"" title=""*""/>
";
		var expected = @"
<p>*<img src=""foo"" title=""*""/></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example476()
	{
		var input = @"
**<a href=""**"">
";
		var expected = @"
<p>**<a href=""**""></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example477()
	{
		var input = @"
__<a href=""__"">
";
		var expected = @"
<p>__<a href=""__""></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example478()
	{
		var input = @"
*a `*`*
";
		var expected = @"
<p><em>a <code>*</code></em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example479()
	{
		var input = @"
_a `_`_
";
		var expected = @"
<p><em>a <code>_</code></em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example480()
	{
		var input = @"
**a<https://foo.bar/?q=**>
";
		var expected = @"
<p>**a<a href=""https://foo.bar/?q=**"">https://foo.bar/?q=**</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example481()
	{
		var input = @"
__a<https://foo.bar/?q=__>
";
		var expected = @"
<p>__a<a href=""https://foo.bar/?q=__"">https://foo.bar/?q=__</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example482()
	{
		var input = @"
[link](/uri ""title"")
";
		var expected = @"
<p><a href=""/uri"" title=""title"">link</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example483()
	{
		var input = @"
[link](/uri)
";
		var expected = @"
<p><a href=""/uri"">link</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example484()
	{
		var input = @"
[](./target.md)
";
		var expected = @"
<p><a href=""./target.md""></a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example485()
	{
		var input = @"
[link]()
";
		var expected = @"
<p><a href="""">link</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example486()
	{
		var input = @"
[link](<>)
";
		var expected = @"
<p><a href="""">link</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example487()
	{
		var input = @"
[]()
";
		var expected = @"
<p><a href=""""></a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example488()
	{
		var input = @"
[link](/my uri)
";
		var expected = @"
<p>[link](/my uri)</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example489()
	{
		var input = @"
[link](</my uri>)
";
		var expected = @"
<p><a href=""/my%20uri"">link</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example490()
	{
		var input = @"
[link](foo
bar)
";
		var expected = @"
<p>[link](foo
bar)</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example491()
	{
		var input = @"
[link](<foo
bar>)
";
		var expected = @"
<p>[link](<foo
bar>)</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example492()
	{
		var input = @"
[a](<b)c>)
";
		var expected = @"
<p><a href=""b)c"">a</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example493()
	{
		var input = @"
[link](<foo\>)
";
		var expected = @"
<p>[link](&lt;foo&gt;)</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example494()
	{
		var input = @"
[a](<b)c
[a](<b)c>
[a](<b>c)
";
		var expected = @"
<p>[a](&lt;b)c
[a](&lt;b)c&gt;
[a](<b>c)</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example495()
	{
		var input = @"
[link](\(foo\))
";
		var expected = @"
<p><a href=""(foo)"">link</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example496()
	{
		var input = @"
[link](foo(and(bar)))
";
		var expected = @"
<p><a href=""foo(and(bar))"">link</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example497()
	{
		var input = @"
[link](foo(and(bar))
";
		var expected = @"
<p>[link](foo(and(bar))</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example498()
	{
		var input = @"
[link](foo\(and\(bar\))
";
		var expected = @"
<p><a href=""foo(and(bar)"">link</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example499()
	{
		var input = @"
[link](<foo(and(bar)>)
";
		var expected = @"
<p><a href=""foo(and(bar)"">link</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example500()
	{
		var input = @"
[link](foo\)\:)
";
		var expected = @"
<p><a href=""foo):"">link</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example501()
	{
		var input = @"
[link](#fragment)

[link](https://example.com#fragment)

[link](https://example.com?foo=3#frag)
";
		var expected = @"
<p><a href=""#fragment"">link</a></p>
<p><a href=""https://example.com#fragment"">link</a></p>
<p><a href=""https://example.com?foo=3#frag"">link</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example502()
	{
		var input = @"
[link](foo\bar)
";
		var expected = @"
<p><a href=""foo%5Cbar"">link</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example503()
	{
		var input = @"
[link](foo%20b&auml;)
";
		var expected = @"
<p><a href=""foo%20b%C3%A4"">link</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example504()
	{
		var input = @"
[link](""title"")
";
		var expected = @"
<p><a href=""%22title%22"">link</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example505()
	{
		var input = @"
[link](/url ""title"")
[link](/url 'title')
[link](/url (title))
";
		var expected = @"
<p><a href=""/url"" title=""title"">link</a>
<a href=""/url"" title=""title"">link</a>
<a href=""/url"" title=""title"">link</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example506()
	{
		var input = @"
[link](/url ""title \""&quot;"")
";
		var expected = @"
<p><a href=""/url"" title=""title &quot;&quot;"">link</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example507()
	{
		var input = @"
[link](/url ""title"")
";
		var expected = @"
<p><a href=""/url%C2%A0%22title%22"">link</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example508()
	{
		var input = @"
[link](/url ""title ""and"" title"")
";
		var expected = @"
<p>[link](/url &quot;title &quot;and&quot; title&quot;)</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example509()
	{
		var input = @"
[link](/url 'title ""and"" title')
";
		var expected = @"
<p><a href=""/url"" title=""title &quot;and&quot; title"">link</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example510()
	{
		var input = @"
[link](   /uri
  ""title""  )
";
		var expected = @"
<p><a href=""/uri"" title=""title"">link</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example511()
	{
		var input = @"
[link] (/uri)
";
		var expected = @"
<p>[link] (/uri)</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example512()
	{
		var input = @"
[link [foo [bar]]](/uri)
";
		var expected = @"
<p><a href=""/uri"">link [foo [bar]]</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example513()
	{
		var input = @"
[link] bar](/uri)
";
		var expected = @"
<p>[link] bar](/uri)</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example514()
	{
		var input = @"
[link [bar](/uri)
";
		var expected = @"
<p>[link <a href=""/uri"">bar</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example515()
	{
		var input = @"
[link \[bar](/uri)
";
		var expected = @"
<p><a href=""/uri"">link [bar</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example516()
	{
		var input = @"
[link *foo **bar** `#`*](/uri)
";
		var expected = @"
<p><a href=""/uri"">link <em>foo <strong>bar</strong> <code>#</code></em></a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example517()
	{
		var input = @"
[![moon](moon.jpg)](/uri)
";
		var expected = @"
<p><a href=""/uri""><img src=""moon.jpg"" alt=""moon"" /></a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example518()
	{
		var input = @"
[foo [bar](/uri)](/uri)
";
		var expected = @"
<p>[foo <a href=""/uri"">bar</a>](/uri)</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example519()
	{
		var input = @"
[foo *[bar [baz](/uri)](/uri)*](/uri)
";
		var expected = @"
<p>[foo <em>[bar <a href=""/uri"">baz</a>](/uri)</em>](/uri)</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example520()
	{
		var input = @"
![[[foo](uri1)](uri2)](uri3)
";
		var expected = @"
<p><img src=""uri3"" alt=""[foo](uri2)"" /></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example521()
	{
		var input = @"
*[foo*](/uri)
";
		var expected = @"
<p>*<a href=""/uri"">foo*</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example522()
	{
		var input = @"
[foo *bar](baz*)
";
		var expected = @"
<p><a href=""baz*"">foo *bar</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example523()
	{
		var input = @"
*foo [bar* baz]
";
		var expected = @"
<p><em>foo [bar</em> baz]</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example524()
	{
		var input = @"
[foo <bar attr=""](baz)"">
";
		var expected = @"
<p>[foo <bar attr=""](baz)""></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example525()
	{
		var input = @"
[foo`](/uri)`
";
		var expected = @"
<p>[foo<code>](/uri)</code></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example526()
	{
		var input = @"
[foo<https://example.com/?search=](uri)>
";
		var expected = @"
<p>[foo<a href=""https://example.com/?search=%5D(uri)"">https://example.com/?search=](uri)</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example527()
	{
		var input = @"
[foo][bar]

[bar]: /url ""title""
";
		var expected = @"
<p><a href=""/url"" title=""title"">foo</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example528()
	{
		var input = @"
[link [foo [bar]]][ref]

[ref]: /uri
";
		var expected = @"
<p><a href=""/uri"">link [foo [bar]]</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example529()
	{
		var input = @"
[link \[bar][ref]

[ref]: /uri
";
		var expected = @"
<p><a href=""/uri"">link [bar</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example530()
	{
		var input = @"
[link *foo **bar** `#`*][ref]

[ref]: /uri
";
		var expected = @"
<p><a href=""/uri"">link <em>foo <strong>bar</strong> <code>#</code></em></a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example531()
	{
		var input = @"
[![moon](moon.jpg)][ref]

[ref]: /uri
";
		var expected = @"
<p><a href=""/uri""><img src=""moon.jpg"" alt=""moon"" /></a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example532()
	{
		var input = @"
[foo [bar](/uri)][ref]

[ref]: /uri
";
		var expected = @"
<p>[foo <a href=""/uri"">bar</a>]<a href=""/uri"">ref</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example533()
	{
		var input = @"
[foo *bar [baz][ref]*][ref]

[ref]: /uri
";
		var expected = @"
<p>[foo <em>bar <a href=""/uri"">baz</a></em>]<a href=""/uri"">ref</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example534()
	{
		var input = @"
*[foo*][ref]

[ref]: /uri
";
		var expected = @"
<p>*<a href=""/uri"">foo*</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example535()
	{
		var input = @"
[foo *bar][ref]*

[ref]: /uri
";
		var expected = @"
<p><a href=""/uri"">foo *bar</a>*</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example536()
	{
		var input = @"
[foo <bar attr=""][ref]"">

[ref]: /uri
";
		var expected = @"
<p>[foo <bar attr=""][ref]""></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example537()
	{
		var input = @"
[foo`][ref]`

[ref]: /uri
";
		var expected = @"
<p>[foo<code>][ref]</code></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example538()
	{
		var input = @"
[foo<https://example.com/?search=][ref]>

[ref]: /uri
";
		var expected = @"
<p>[foo<a href=""https://example.com/?search=%5D%5Bref%5D"">https://example.com/?search=][ref]</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example539()
	{
		var input = @"
[foo][BaR]

[bar]: /url ""title""
";
		var expected = @"
<p><a href=""/url"" title=""title"">foo</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example540()
	{
		var input = @"
[ẞ]

[SS]: /url
";
		var expected = @"
<p><a href=""/url"">ẞ</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example541()
	{
		var input = @"
[Foo
  bar]: /url

[Baz][Foo bar]
";
		var expected = @"
<p><a href=""/url"">Baz</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example542()
	{
		var input = @"
[foo] [bar]

[bar]: /url ""title""
";
		var expected = @"
<p>[foo] <a href=""/url"" title=""title"">bar</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example543()
	{
		var input = @"
[foo]
[bar]

[bar]: /url ""title""
";
		var expected = @"
<p>[foo]
<a href=""/url"" title=""title"">bar</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example544()
	{
		var input = @"
[foo]: /url1

[foo]: /url2

[bar][foo]
";
		var expected = @"
<p><a href=""/url1"">bar</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example545()
	{
		var input = @"
[bar][foo\!]

[foo!]: /url
";
		var expected = @"
<p>[bar][foo!]</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example546()
	{
		var input = @"
[foo][ref[]

[ref[]: /uri
";
		var expected = @"
<p>[foo][ref[]</p>
<p>[ref[]: /uri</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example547()
	{
		var input = @"
[foo][ref[bar]]

[ref[bar]]: /uri
";
		var expected = @"
<p>[foo][ref[bar]]</p>
<p>[ref[bar]]: /uri</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example548()
	{
		var input = @"
[[[foo]]]

[[[foo]]]: /url
";
		var expected = @"
<p>[[[foo]]]</p>
<p>[[[foo]]]: /url</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example549()
	{
		var input = @"
[foo][ref\[]

[ref\[]: /uri
";
		var expected = @"
<p><a href=""/uri"">foo</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example550()
	{
		var input = @"
[bar\\]: /uri

[bar\\]
";
		var expected = @"
<p><a href=""/uri"">bar\</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example551()
	{
		var input = @"
[]

[]: /uri
";
		var expected = @"
<p>[]</p>
<p>[]: /uri</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example552()
	{
		var input = @"
[
 ]

[
 ]: /uri
";
		var expected = @"
<p>[
]</p>
<p>[
]: /uri</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example553()
	{
		var input = @"
[foo][]

[foo]: /url ""title""
";
		var expected = @"
<p><a href=""/url"" title=""title"">foo</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example554()
	{
		var input = @"
[*foo* bar][]

[*foo* bar]: /url ""title""
";
		var expected = @"
<p><a href=""/url"" title=""title""><em>foo</em> bar</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example555()
	{
		var input = @"
[Foo][]

[foo]: /url ""title""
";
		var expected = @"
<p><a href=""/url"" title=""title"">Foo</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example556()
	{
		var input = @"
[foo] 
[]

[foo]: /url ""title""
";
		var expected = @"
<p><a href=""/url"" title=""title"">foo</a>
[]</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example557()
	{
		var input = @"
[foo]

[foo]: /url ""title""
";
		var expected = @"
<p><a href=""/url"" title=""title"">foo</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example558()
	{
		var input = @"
[*foo* bar]

[*foo* bar]: /url ""title""
";
		var expected = @"
<p><a href=""/url"" title=""title""><em>foo</em> bar</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example559()
	{
		var input = @"
[[*foo* bar]]

[*foo* bar]: /url ""title""
";
		var expected = @"
<p>[<a href=""/url"" title=""title""><em>foo</em> bar</a>]</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example560()
	{
		var input = @"
[[bar [foo]

[foo]: /url
";
		var expected = @"
<p>[[bar <a href=""/url"">foo</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example561()
	{
		var input = @"
[Foo]

[foo]: /url ""title""
";
		var expected = @"
<p><a href=""/url"" title=""title"">Foo</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example562()
	{
		var input = @"
[foo] bar

[foo]: /url
";
		var expected = @"
<p><a href=""/url"">foo</a> bar</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example563()
	{
		var input = @"
\[foo]

[foo]: /url ""title""
";
		var expected = @"
<p>[foo]</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example564()
	{
		var input = @"
[foo*]: /url

*[foo*]
";
		var expected = @"
<p>*<a href=""/url"">foo*</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example565()
	{
		var input = @"
[foo][bar]

[foo]: /url1
[bar]: /url2
";
		var expected = @"
<p><a href=""/url2"">foo</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example566()
	{
		var input = @"
[foo][]

[foo]: /url1
";
		var expected = @"
<p><a href=""/url1"">foo</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example567()
	{
		var input = @"
[foo]()

[foo]: /url1
";
		var expected = @"
<p><a href="""">foo</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example568()
	{
		var input = @"
[foo](not a link)

[foo]: /url1
";
		var expected = @"
<p><a href=""/url1"">foo</a>(not a link)</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example569()
	{
		var input = @"
[foo][bar][baz]

[baz]: /url
";
		var expected = @"
<p>[foo]<a href=""/url"">bar</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example570()
	{
		var input = @"
[foo][bar][baz]

[baz]: /url1
[bar]: /url2
";
		var expected = @"
<p><a href=""/url2"">foo</a><a href=""/url1"">baz</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example571()
	{
		var input = @"
[foo][bar][baz]

[baz]: /url1
[foo]: /url2
";
		var expected = @"
<p>[foo]<a href=""/url1"">bar</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example572()
	{
		var input = @"
![foo](/url ""title"")
";
		var expected = @"
<p><img src=""/url"" alt=""foo"" title=""title"" /></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example573()
	{
		var input = @"
![foo *bar*]

[foo *bar*]: train.jpg ""train & tracks""
";
		var expected = @"
<p><img src=""train.jpg"" alt=""foo bar"" title=""train &amp; tracks"" /></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example574()
	{
		var input = @"
![foo ![bar](/url)](/url2)
";
		var expected = @"
<p><img src=""/url2"" alt=""foo bar"" /></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example575()
	{
		var input = @"
![foo [bar](/url)](/url2)
";
		var expected = @"
<p><img src=""/url2"" alt=""foo bar"" /></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example576()
	{
		var input = @"
![foo *bar*][]

[foo *bar*]: train.jpg ""train & tracks""
";
		var expected = @"
<p><img src=""train.jpg"" alt=""foo bar"" title=""train &amp; tracks"" /></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example577()
	{
		var input = @"
![foo *bar*][foobar]

[FOOBAR]: train.jpg ""train & tracks""
";
		var expected = @"
<p><img src=""train.jpg"" alt=""foo bar"" title=""train &amp; tracks"" /></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example578()
	{
		var input = @"
![foo](train.jpg)
";
		var expected = @"
<p><img src=""train.jpg"" alt=""foo"" /></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example579()
	{
		var input = @"
My ![foo bar](/path/to/train.jpg  ""title""   )
";
		var expected = @"
<p>My <img src=""/path/to/train.jpg"" alt=""foo bar"" title=""title"" /></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example580()
	{
		var input = @"
![foo](<url>)
";
		var expected = @"
<p><img src=""url"" alt=""foo"" /></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example581()
	{
		var input = @"
![](/url)
";
		var expected = @"
<p><img src=""/url"" alt="""" /></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example582()
	{
		var input = @"
![foo][bar]

[bar]: /url
";
		var expected = @"
<p><img src=""/url"" alt=""foo"" /></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example583()
	{
		var input = @"
![foo][bar]

[BAR]: /url
";
		var expected = @"
<p><img src=""/url"" alt=""foo"" /></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example584()
	{
		var input = @"
![foo][]

[foo]: /url ""title""
";
		var expected = @"
<p><img src=""/url"" alt=""foo"" title=""title"" /></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example585()
	{
		var input = @"
![*foo* bar][]

[*foo* bar]: /url ""title""
";
		var expected = @"
<p><img src=""/url"" alt=""foo bar"" title=""title"" /></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example586()
	{
		var input = @"
![Foo][]

[foo]: /url ""title""
";
		var expected = @"
<p><img src=""/url"" alt=""Foo"" title=""title"" /></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example587()
	{
		var input = @"
![foo] 
[]

[foo]: /url ""title""
";
		var expected = @"
<p><img src=""/url"" alt=""foo"" title=""title"" />
[]</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example588()
	{
		var input = @"
![foo]

[foo]: /url ""title""
";
		var expected = @"
<p><img src=""/url"" alt=""foo"" title=""title"" /></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example589()
	{
		var input = @"
![*foo* bar]

[*foo* bar]: /url ""title""
";
		var expected = @"
<p><img src=""/url"" alt=""foo bar"" title=""title"" /></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example590()
	{
		var input = @"
![[foo]]

[[foo]]: /url ""title""
";
		var expected = @"
<p>![[foo]]</p>
<p>[[foo]]: /url &quot;title&quot;</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example591()
	{
		var input = @"
![Foo]

[foo]: /url ""title""
";
		var expected = @"
<p><img src=""/url"" alt=""Foo"" title=""title"" /></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example592()
	{
		var input = @"
!\[foo]

[foo]: /url ""title""
";
		var expected = @"
<p>![foo]</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example593()
	{
		var input = @"
\![foo]

[foo]: /url ""title""
";
		var expected = @"
<p>!<a href=""/url"" title=""title"">foo</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example594()
	{
		var input = @"
<http://foo.bar.baz>
";
		var expected = @"
<p><a href=""http://foo.bar.baz"">http://foo.bar.baz</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example595()
	{
		var input = @"
<https://foo.bar.baz/test?q=hello&id=22&boolean>
";
		var expected = @"
<p><a href=""https://foo.bar.baz/test?q=hello&amp;id=22&amp;boolean"">https://foo.bar.baz/test?q=hello&amp;id=22&amp;boolean</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example596()
	{
		var input = @"
<irc://foo.bar:2233/baz>
";
		var expected = @"
<p><a href=""irc://foo.bar:2233/baz"">irc://foo.bar:2233/baz</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example597()
	{
		var input = @"
<MAILTO:FOO@BAR.BAZ>
";
		var expected = @"
<p><a href=""MAILTO:FOO@BAR.BAZ"">MAILTO:FOO@BAR.BAZ</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example598()
	{
		var input = @"
<a+b+c:d>
";
		var expected = @"
<p><a href=""a+b+c:d"">a+b+c:d</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example599()
	{
		var input = @"
<made-up-scheme://foo,bar>
";
		var expected = @"
<p><a href=""made-up-scheme://foo,bar"">made-up-scheme://foo,bar</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example600()
	{
		var input = @"
<https://../>
";
		var expected = @"
<p><a href=""https://../"">https://../</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example601()
	{
		var input = @"
<localhost:5001/foo>
";
		var expected = @"
<p><a href=""localhost:5001/foo"">localhost:5001/foo</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example602()
	{
		var input = @"
<https://foo.bar/baz bim>
";
		var expected = @"
<p>&lt;https://foo.bar/baz bim&gt;</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example603()
	{
		var input = @"
<https://example.com/\[\>
";
		var expected = @"
<p><a href=""https://example.com/%5C%5B%5C"">https://example.com/\[\</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example604()
	{
		var input = @"
<foo@bar.example.com>
";
		var expected = @"
<p><a href=""mailto:foo@bar.example.com"">foo@bar.example.com</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example605()
	{
		var input = @"
<foo+special@Bar.baz-bar0.com>
";
		var expected = @"
<p><a href=""mailto:foo+special@Bar.baz-bar0.com"">foo+special@Bar.baz-bar0.com</a></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example606()
	{
		var input = @"
<foo\+@bar.example.com>
";
		var expected = @"
<p>&lt;foo+@bar.example.com&gt;</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example607()
	{
		var input = @"
<>
";
		var expected = @"
<p>&lt;&gt;</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example608()
	{
		var input = @"
< https://foo.bar >
";
		var expected = @"
<p>&lt; https://foo.bar &gt;</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example609()
	{
		var input = @"
<m:abc>
";
		var expected = @"
<p>&lt;m:abc&gt;</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example610()
	{
		var input = @"
<foo.bar.baz>
";
		var expected = @"
<p>&lt;foo.bar.baz&gt;</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example611()
	{
		var input = @"
https://example.com
";
		var expected = @"
<p>https://example.com</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example612()
	{
		var input = @"
foo@bar.example.com
";
		var expected = @"
<p>foo@bar.example.com</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example613()
	{
		var input = @"
<a><bab><c2c>
";
		var expected = @"
<p><a><bab><c2c></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example614()
	{
		var input = @"
<a/><b2/>
";
		var expected = @"
<p><a/><b2/></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example615()
	{
		var input = @"
<a  /><b2
data=""foo"" >
";
		var expected = @"
<p><a  /><b2
data=""foo"" ></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example616()
	{
		var input = @"
<a foo=""bar"" bam = 'baz <em>""</em>'
_boolean zoop:33=zoop:33 />
";
		var expected = @"
<p><a foo=""bar"" bam = 'baz <em>""</em>'
_boolean zoop:33=zoop:33 /></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example617()
	{
		var input = @"
Foo <responsive-image src=""foo.jpg"" />
";
		var expected = @"
<p>Foo <responsive-image src=""foo.jpg"" /></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example618()
	{
		var input = @"
<33> <__>
";
		var expected = @"
<p>&lt;33&gt; &lt;__&gt;</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example619()
	{
		var input = @"
<a h*#ref=""hi"">
";
		var expected = @"
<p>&lt;a h*#ref=&quot;hi&quot;&gt;</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example620()
	{
		var input = @"
<a href=""hi'> <a href=hi'>
";
		var expected = @"
<p>&lt;a href=&quot;hi'&gt; &lt;a href=hi'&gt;</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example621()
	{
		var input = @"
< a><
foo><bar/ >
<foo bar=baz
bim!bop />
";
		var expected = @"
<p>&lt; a&gt;&lt;
foo&gt;&lt;bar/ &gt;
&lt;foo bar=baz
bim!bop /&gt;</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example622()
	{
		var input = @"
<a href='bar'title=title>
";
		var expected = @"
<p>&lt;a href='bar'title=title&gt;</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example623()
	{
		var input = @"
</a></foo >
";
		var expected = @"
<p></a></foo ></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example624()
	{
		var input = @"
</a href=""foo"">
";
		var expected = @"
<p>&lt;/a href=&quot;foo&quot;&gt;</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example625()
	{
		var input = @"
foo <!-- this is a --
comment - with hyphens -->
";
		var expected = @"
<p>foo <!-- this is a --
comment - with hyphens --></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example626()
	{
		var input = @"
foo <!--> foo -->

foo <!---> foo -->
";
		var expected = @"
<p>foo <!--> foo --&gt;</p>
<p>foo <!---> foo --&gt;</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example627()
	{
		var input = @"
foo <?php echo $a; ?>
";
		var expected = @"
<p>foo <?php echo $a; ?></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example628()
	{
		var input = @"
foo <!ELEMENT br EMPTY>
";
		var expected = @"
<p>foo <!ELEMENT br EMPTY></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example629()
	{
		var input = @"
foo <![CDATA[>&<]]>
";
		var expected = @"
<p>foo <![CDATA[>&<]]></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example630()
	{
		var input = @"
foo <a href=""&ouml;"">
";
		var expected = @"
<p>foo <a href=""&ouml;""></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example631()
	{
		var input = @"
foo <a href=""\*"">
";
		var expected = @"
<p>foo <a href=""\*""></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example632()
	{
		var input = @"
<a href=""\"""">
";
		var expected = @"
<p>&lt;a href=&quot;&quot;&quot;&gt;</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example633()
	{
		var input = @"
foo  
baz
";
		var expected = @"
<p>foo<br />
baz</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example634()
	{
		var input = @"
foo\
baz
";
		var expected = @"
<p>foo<br />
baz</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example635()
	{
		var input = @"
foo       
baz
";
		var expected = @"
<p>foo<br />
baz</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example636()
	{
		var input = @"
foo  
     bar
";
		var expected = @"
<p>foo<br />
bar</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example637()
	{
		var input = @"
foo\
     bar
";
		var expected = @"
<p>foo<br />
bar</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example638()
	{
		var input = @"
*foo  
bar*
";
		var expected = @"
<p><em>foo<br />
bar</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example639()
	{
		var input = @"
*foo\
bar*
";
		var expected = @"
<p><em>foo<br />
bar</em></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example640()
	{
		var input = @"
`code  
span`
";
		var expected = @"
<p><code>code   span</code></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example641()
	{
		var input = @"
`code\
span`
";
		var expected = @"
<p><code>code\ span</code></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example642()
	{
		var input = @"
<a href=""foo  
bar"">
";
		var expected = @"
<p><a href=""foo  
bar""></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example643()
	{
		var input = @"
<a href=""foo\
bar"">
";
		var expected = @"
<p><a href=""foo\
bar""></p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example644()
	{
		var input = @"
foo\
";
		var expected = @"
<p>foo\</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example645()
	{
		var input = @"
foo  
";
		var expected = @"
<p>foo</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example646()
	{
		var input = @"
### foo\
";
		var expected = @"
<h3>foo\</h3>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example647()
	{
		var input = @"
### foo  
";
		var expected = @"
<h3>foo</h3>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example648()
	{
		var input = @"
foo
baz
";
		var expected = @"
<p>foo
baz</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example649()
	{
		var input = @"
foo 
 baz
";
		var expected = @"
<p>foo
baz</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example650()
	{
		var input = @"
hello $.;'there
";
		var expected = @"
<p>hello $.;'there</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example651()
	{
		var input = @"
Foo χρῆν
";
		var expected = @"
<p>Foo χρῆν</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	[TestMethod]
	public void Example652()
	{
		var input = @"
Multiple     spaces
";
		var expected = @"
<p>Multiple     spaces</p>
";
		var doc = Parser.Execute(input[1..^1], Core.RuleSet, false);
		var html = RenderHtml.Execute(doc, Core.RuleSet.Renderers);
		Assert.IsTrue(expected.Trim() == html.Trim(), Message(expected.Trim(), html.Trim()));
	}

	private string Message(string expected, string html)
	{
		return $"expected vs actual =>\n=\n{expected.Trim()}\n=\n{html.Trim()}\n";
	}
}
